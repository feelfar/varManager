using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;

namespace varManager
{
    /// <summary>Per-table outcome of a migration run.</summary>
    public sealed class TableMigrateResult
    {
        public string Table;
        public int Read;
        public int Inserted;
        public string Error;
    }

    /// <summary>Aggregate result of a migration run.</summary>
    public sealed class MigrateResult
    {
        public readonly List<TableMigrateResult> Tables = new List<TableMigrateResult>();
        public int TotalInserted;
        public bool HasErrors
        {
            get { return Tables.Exists(t => t.Error != null); }
        }
    }

    /// <summary>
    /// Imports the five persistent tables from a legacy Access <c>varManager.mdb</c> into a
    /// SQLite database. Reading the .mdb requires the Microsoft ACE OLEDB provider.
    ///
    /// Original ID values are preserved (explicit insert into AUTOINCREMENT columns) so any
    /// references stay consistent. The only user-authored state (hide/favourite marks) lives
    /// in files under the VaM folder, not in the DB, so it is unaffected by migration.
    /// </summary>
    public static class MdbToSqliteMigrator
    {
        // Order: parents before children (no FK enforcement, but keeps things tidy).
        private static readonly string[] Tables = { "vars", "installStatus", "scenes", "dependencies", "savedepens" };

        /// <summary>Simple importer used on first run (fresh DB, no clearing, no progress).</summary>
        public static int Migrate(string mdbPath, string sqliteConnectionString)
        {
            return MigrateFull(mdbPath, sqliteConnectionString, clearFirst: false, progress: null).TotalInserted;
        }

        /// <summary>
        /// Full importer for the migration tool: ensures the target schema exists, optionally
        /// clears the target tables (full overwrite), and reports progress per table.
        /// </summary>
        public static MigrateResult MigrateFull(string mdbPath, string sqliteConnectionString,
                                                bool clearFirst, Action<string> progress)
        {
            if (string.IsNullOrWhiteSpace(mdbPath) || !File.Exists(mdbPath))
                throw new FileNotFoundException("找不到旧数据库文件: " + mdbPath);

            Report(progress, "确保目标库结构…");
            Db.EnsureSchema(sqliteConnectionString);

            var result = new MigrateResult();
            string oleConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbPath + ";";

            Report(progress, "打开旧数据库(Access)…");
            using (var ole = new OleDbConnection(oleConnStr))
            {
                ole.Open();
                using (var sq = new SQLiteConnection(sqliteConnectionString))
                {
                    sq.Open();
                    // Bulk-load speedups (safe: a fresh import; re-run on failure).
                    using (var pragma = new SQLiteCommand("PRAGMA synchronous=OFF; PRAGMA journal_mode=MEMORY; PRAGMA temp_store=MEMORY;", sq))
                        pragma.ExecuteNonQuery();

                    foreach (var table in Tables)
                    {
                        var tr = new TableMigrateResult { Table = table };
                        try
                        {
                            if (clearFirst)
                            {
                                Report(progress, "清空目标表 " + table + " …");
                                using (var del = new SQLiteCommand("DELETE FROM \"" + table + "\"", sq))
                                    del.ExecuteNonQuery();
                            }

                            Report(progress, "读取 " + table + " …");
                            var dt = new DataTable();
                            using (var da = new OleDbDataAdapter("SELECT * FROM [" + table + "]", ole))
                                da.Fill(dt);
                            tr.Read = dt.Rows.Count;

                            tr.Inserted = InsertRows(sq, table, dt, progress);
                        }
                        catch (Exception ex)
                        {
                            tr.Error = ex.Message;
                            Report(progress, "  ! " + table + " 失败: " + ex.Message);
                        }
                        result.Tables.Add(tr);
                        result.TotalInserted += tr.Inserted;
                        Report(progress, string.Format("  {0}: 读取 {1}, 写入 {2}{3}",
                            table, tr.Read, tr.Inserted, tr.Error != null ? "  (错误)" : ""));
                    }
                }
            }

            Report(progress, "完成,共写入 " + result.TotalInserted + " 行。");
            return result;
        }

        private static int InsertRows(SQLiteConnection sq, string table, DataTable dt, Action<string> progress)
        {
            if (dt.Rows.Count == 0) return 0;

            int n = dt.Columns.Count;
            var cols = new string[n];
            for (int i = 0; i < n; i++) cols[i] = dt.Columns[i].ColumnName;
            string colList = string.Join(", ", Array.ConvertAll(cols, c => "\"" + c + "\""));
            string placeholders = string.Join(", ", Array.ConvertAll(cols, _ => "?"));
            string insertSql = "INSERT INTO \"" + table + "\" (" + colList + ") VALUES (" + placeholders + ")";

            int count = 0;
            int total = dt.Rows.Count;
            using (var tx = sq.BeginTransaction())
            using (var cmd = new SQLiteCommand(insertSql, sq, tx))
            {
                for (int i = 0; i < n; i++) cmd.Parameters.Add(new SQLiteParameter());
                cmd.Prepare();

                int done = 0;
                foreach (DataRow row in dt.Rows)
                {
                    for (int c = 0; c < n; c++)
                        cmd.Parameters[c].Value = row[c] ?? DBNull.Value;
                    count += cmd.ExecuteNonQuery();
                    if ((++done % 25000) == 0)
                        Report(progress, "    " + table + ": " + done + "/" + total + " …");
                }
                tx.Commit();
            }
            return count;
        }

        private static void Report(Action<string> progress, string message)
        {
            if (progress != null) progress(message);
        }
    }
}
