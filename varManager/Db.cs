using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Data.SQLite;

namespace varManager
{
    /// <summary>
    /// Central SQLite bootstrap for varManager. Replaces the old Access/ACE-OLEDB database.
    ///
    /// The database lives in a *writable* data directory chosen at startup:
    ///   1. next to the executable, if that folder is writable (portable / local installs);
    ///   2. otherwise %LOCALAPPDATA%\varManager (used when the exe sits on a read-only path,
    ///      or on a mapped network drive that is invisible to the elevated process — this app
    ///      runs as administrator, and mapped drives are not shared across logon sessions).
    ///
    /// On startup <see cref="Initialize"/>:
    ///   - resolves the data directory and connection string,
    ///   - creates the schema if needed (idempotent),
    ///   - on a brand-new database, attempts a one-time import of a legacy varManager.mdb
    ///     (best effort; the DB is otherwise rebuilt by rescanning the var repository).
    /// </summary>
    public static class Db
    {
        public static string DataDirectory { get; private set; }
        public static string DatabasePath { get; private set; }
        public static string ConnectionString { get; private set; }

        /// <summary>Set if the one-time .mdb import failed; the UI may surface this.</summary>
        public static string LastMigrationError { get; private set; }

        /// <summary>Number of rows imported during the one-time migration (0 if none/fresh).</summary>
        public static int MigratedRowCount { get; private set; }

        public static void Initialize()
        {
            Initialize(null);
        }

        /// <summary>Initialize with an explicit data directory (tests/sandboxes). The normal
        /// app path passes null and gets the standard resolution (exe dir / LocalAppData).</summary>
        public static void Initialize(string dataDirectoryOverride)
        {
            DataDirectory = string.IsNullOrEmpty(dataDirectoryOverride)
                ? ResolveWritableDataDirectory()
                : Directory.CreateDirectory(dataDirectoryOverride).FullName;
            DatabasePath = Path.Combine(DataDirectory, "varManager.sqlite");
            ConnectionString = BuildConnectionString(DatabasePath);

            bool freshDatabase = !File.Exists(DatabasePath);

            // System.Data.SQLite creates the file automatically on Open.
            CreateSchema();

            if (freshDatabase)
            {
                string legacyMdb = FindLegacyMdb();
                if (legacyMdb != null)
                {
                    try
                    {
                        MigratedRowCount = MdbToSqliteMigrator.Migrate(legacyMdb, ConnectionString);
                    }
                    catch (Exception ex)
                    {
                        // ACE driver missing / unreadable mdb: keep the empty DB, let the
                        // app rebuild by rescanning. Record the reason for the user.
                        LastMigrationError = ex.Message;
                        TryLog("Legacy .mdb import failed: " + ex);
                    }
                }
            }
        }

        /// <summary>
        /// Pick a directory we can actually write to. Prefer the exe folder (portable),
        /// fall back to %LOCALAPPDATA%\varManager when it is read-only or unreachable
        /// (e.g. exe on a network share while running elevated).
        /// </summary>
        private static string ResolveWritableDataDirectory()
        {
            string exeDir = AppContext.BaseDirectory;
            // Prefer the exe folder only for LOCAL, writable installs. Never put the SQLite
            // file on a network share (SMB locking is unreliable, and a mapped drive is
            // invisible to this elevated process anyway).
            if (!IsNetworkPath(exeDir) && IsDirectoryWritable(exeDir))
                return exeDir;

            string local = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "varManager");
            Directory.CreateDirectory(local);
            return local;
        }

        private static bool IsDirectoryWritable(string dir)
        {
            try
            {
                if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir)) return false;
                string probe = Path.Combine(dir, ".wtest_" + Guid.NewGuid().ToString("N"));
                using (File.Create(probe)) { }
                File.Delete(probe);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsNetworkPath(string dir)
        {
            try
            {
                if (string.IsNullOrEmpty(dir)) return false;
                if (dir.StartsWith(@"\\")) return true; // UNC
                string root = Path.GetPathRoot(dir);
                if (string.IsNullOrEmpty(root)) return false;
                return new DriveInfo(root).DriveType == DriveType.Network;
            }
            catch
            {
                return false; // undetermined: let the write-test decide
            }
        }

        /// <summary>Look for a legacy varManager.mdb in the data dir, then next to the exe.</summary>
        private static string FindLegacyMdb()
        {
            foreach (var dir in new[] { DataDirectory, AppContext.BaseDirectory })
            {
                try
                {
                    string p = Path.Combine(dir, "varManager.mdb");
                    if (File.Exists(p)) return p;
                }
                catch { /* inaccessible path (e.g. unmapped network drive) -> ignore */ }
            }
            return null;
        }

        private static void CreateSchema() => EnsureSchema(ConnectionString);

        /// <summary>Create the varManager schema (tables/indexes/views) on the given SQLite
        /// connection if missing. Idempotent. Used both on startup and by the migration tool.</summary>
        public static void EnsureSchema(string connectionString)
        {
            string sql = ReadEmbeddedSchema();
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                using (var cmd = new SQLiteCommand(sql, conn, tx))
                {
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                }
            }
        }

        /// <summary>
        /// Build the SQLite connection string used everywhere (startup DB, migration targets,
        /// tests) so every database gets the same performance profile:
        ///   WAL          — readers don't block the writer; no rollback-journal churn.
        ///   Synchronous=Normal — with WAL, commits skip the per-commit fsync (safe: the DB
        ///                  cannot corrupt; this is rebuildable index data).
        ///   Cache Size=-8192 — 8 MB page cache cap (negative = KB). Deliberately small and
        ///                  deliberately NOT pooled: every form owns adapter instances with a
        ///                  connection each, so pooled connections would keep their native page
        ///                  caches alive for the whole process (opening the scene manager once
        ///                  pinned hundreds of MB). Unpooled, the cache is freed at Close and
        ///                  the OS file cache serves re-reads.
        /// </summary>
        public static string BuildConnectionString(string databaseFilePath)
            => "Data Source=" + databaseFilePath +
               ";Version=3;Journal Mode=WAL;Synchronous=Normal;Cache Size=-8192;";

        private static string ReadEmbeddedSchema()
        {
            var asm = typeof(Db).Assembly;
            string name = asm.GetManifestResourceNames()
                             .FirstOrDefault(n => n.EndsWith("schema.sql", StringComparison.OrdinalIgnoreCase));
            if (name == null)
                throw new InvalidOperationException("Embedded schema.sql not found in assembly manifest.");
            using (var s = asm.GetManifestResourceStream(name))
            using (var r = new StreamReader(s))
                return r.ReadToEnd();
        }

        private static void TryLog(string message)
        {
            try
            {
                string dir = DataDirectory ?? AppContext.BaseDirectory;
                File.AppendAllText(
                    Path.Combine(dir, "varManager.migration.log"),
                    DateTime.Now.ToString("s") + "  " + message + Environment.NewLine);
            }
            catch { /* logging must never throw */ }
        }
    }
}
