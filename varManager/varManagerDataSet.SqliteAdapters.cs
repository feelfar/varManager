// ============================================================================
//  SQLite TableAdapters for varManagerDataSet.
//
//  Replaces the original Visual-Studio-generated OleDb/Access TableAdapters
//  (namespace varManager.varManagerDataSetTableAdapters, formerly in
//   varManagerDataSet.Designer.cs lines 6457..end) with hand-written adapters
//  that talk to SQLite via System.Data.SQLite.
//
//  The strongly-typed DataSet / DataTable / DataRow classes are UNCHANGED and
//  still live in varManagerDataSet.Designer.cs. Only the data-access layer
//  (connection + commands) was swapped from ACE-OLEDB to SQLite, so all the
//  Forms code keeps calling the exact same adapter methods it always did:
//      Fill(table) / GetData() / Update(...) / FillByXxx(...) / DeleteAll()
//
//  CRUD for the writable base tables is produced on demand by
//  SQLiteCommandBuilder (PK-based, ConflictOption.OverwriteChanges) which is
//  appropriate for this single-user desktop application.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace varManager.varManagerDataSetTableAdapters
{
    /// <summary>Shared SQLite plumbing for all typed table adapters.</summary>
    public abstract class SqliteAdapterBase : global::System.ComponentModel.Component
    {
        private SQLiteConnection _connection;

        /// <summary>When true (default) Fill clears the table before loading, matching the original generated behaviour.</summary>
        public bool ClearBeforeFill { get; set; } = true;

        internal SQLiteConnection Connection
        {
            get { return _connection ?? (_connection = new SQLiteConnection(global::varManager.Db.ConnectionString)); }
            set
            {
                _connection = value;
                // Cached CRUD commands are bound to the old connection — rebuild on demand.
                InvalidateUpdateAdapter();
            }
        }

        internal SQLiteTransaction Transaction { get; set; }

        /// <summary>The single-table SELECT used to derive CRUD commands for writable adapters. Null for read-only views.</summary>
        protected virtual string SelectCommandText { get { return null; } }

        /// <summary>Fill an existing typed table from a SELECT (optionally parameterised with positional '?').</summary>
        protected int FillTable(DataTable dataTable, string sql, bool clearFirst, params object[] args)
        {
            if (clearFirst) dataTable.Clear();
            using (var cmd = new SQLiteCommand(sql, Connection))
            {
                if (Transaction != null) cmd.Transaction = Transaction;
                foreach (var a in args)
                    cmd.Parameters.Add(new SQLiteParameter { Value = a ?? (object)DBNull.Value });
                using (var da = new SQLiteDataAdapter(cmd))
                    return da.Fill(dataTable);   // opens/closes Connection automatically when not in a transaction
            }
        }

        /// <summary>Persist Added/Modified/Deleted rows of a typed table using auto-generated PK-based CRUD.</summary>
        protected int UpdateTable(DataTable dataTable)
        {
            return UpdateCore(da => da.Update(dataTable));
        }

        protected int UpdateRows(DataRow[] dataRows)
        {
            return UpdateCore(da => da.Update(dataRows));
        }

        private SQLiteDataAdapter _updateAdapter;
        private SQLiteCommandBuilder _updateBuilder;

        private void InvalidateUpdateAdapter()
        {
            if (_updateBuilder != null) { _updateBuilder.Dispose(); _updateBuilder = null; }
            if (_updateAdapter != null)
            {
                _updateAdapter.SelectCommand?.Dispose();
                _updateAdapter.Dispose();
                _updateAdapter = null;
            }
        }

        /// <summary>CRUD adapter with commands generated once and reused — the CommandBuilder
        /// schema round-trip used to run on every Update call. Connection must be open.</summary>
        private SQLiteDataAdapter GetUpdateAdapter()
        {
            if (_updateAdapter == null)
            {
                var select = new SQLiteCommand(SelectCommandText, Connection);
                _updateAdapter = new SQLiteDataAdapter(select);
                _updateBuilder = new SQLiteCommandBuilder(_updateAdapter) { ConflictOption = ConflictOption.OverwriteChanges };
                _updateAdapter.InsertCommand = _updateBuilder.GetInsertCommand();
                _updateAdapter.UpdateCommand = _updateBuilder.GetUpdateCommand();
                _updateAdapter.DeleteCommand = _updateBuilder.GetDeleteCommand();
            }
            return _updateAdapter;
        }

        private int UpdateCore(Func<SQLiteDataAdapter, int> update)
        {
            if (string.IsNullOrEmpty(SelectCommandText))
                throw new NotSupportedException(GetType().Name + " is read-only.");

            var conn = Connection;
            bool opened = conn.State != ConnectionState.Open;
            if (opened) conn.Open();
            try
            {
                var da = GetUpdateAdapter();
                // One transaction per batch: without it every changed row is its own
                // autocommit transaction (journal + fsync per row), which is what made
                // large scans hammer the system drive. All-or-nothing on failure.
                bool ownTx = Transaction == null;
                var tx = ownTx ? conn.BeginTransaction() : Transaction;
                try
                {
                    da.SelectCommand.Transaction = tx;
                    da.InsertCommand.Transaction = tx;
                    da.UpdateCommand.Transaction = tx;
                    da.DeleteCommand.Transaction = tx;
                    int affected = update(da);
                    if (ownTx) tx.Commit();
                    return affected;
                }
                catch
                {
                    if (ownTx) tx.Rollback();
                    throw;
                }
                finally
                {
                    if (ownTx) tx.Dispose();
                    da.SelectCommand.Transaction = null;
                    da.InsertCommand.Transaction = null;
                    da.UpdateCommand.Transaction = null;
                    da.DeleteCommand.Transaction = null;
                }
            }
            finally { if (opened) conn.Close(); }
        }

        /// <summary>Deterministically release the cached CRUD commands and the native
        /// connection when the owning form disposes its components — without this, native
        /// SQLite memory waits for finalizers.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                InvalidateUpdateAdapter();
                if (_connection != null) { _connection.Dispose(); _connection = null; }
            }
            base.Dispose(disposing);
        }

        /// <summary>Run a non-query (e.g. DELETE *) on the adapter's connection.</summary>
        protected int ExecuteNonQuery(string sql)
        {
            var conn = Connection;
            bool opened = false;
            if (conn.State != ConnectionState.Open) { conn.Open(); opened = true; }
            try
            {
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (Transaction != null) cmd.Transaction = Transaction;
                    return cmd.ExecuteNonQuery();
                }
            }
            finally { if (opened) conn.Close(); }
        }
    }

    // -----------------------------------------------------------------------
    public partial class dependenciesTableAdapter : SqliteAdapterBase
    {
        protected override string SelectCommandText { get { return "SELECT ID, varName, dependency FROM dependencies"; } }

        public virtual int Fill(varManagerDataSet.dependenciesDataTable dataTable) { return FillTable(dataTable, SelectCommandText, ClearBeforeFill); }
        public virtual varManagerDataSet.dependenciesDataTable GetData() { var dt = new varManagerDataSet.dependenciesDataTable(); FillTable(dt, SelectCommandText, false); return dt; }
        public virtual int Update(varManagerDataSet.dependenciesDataTable dataTable) { return UpdateTable(dataTable); }
        public virtual int Update(varManagerDataSet dataSet) { return UpdateTable(dataSet.dependencies); }
        public virtual int Update(DataRow dataRow) { return UpdateRows(new[] { dataRow }); }
        public virtual int Update(DataRow[] dataRows) { return UpdateRows(dataRows); }
    }

    // -----------------------------------------------------------------------
    public partial class installStatusTableAdapter : SqliteAdapterBase
    {
        protected override string SelectCommandText { get { return "SELECT varName, Installed, Disabled FROM installStatus"; } }

        public virtual int Fill(varManagerDataSet.installStatusDataTable dataTable) { return FillTable(dataTable, SelectCommandText, ClearBeforeFill); }
        public virtual varManagerDataSet.installStatusDataTable GetData() { var dt = new varManagerDataSet.installStatusDataTable(); FillTable(dt, SelectCommandText, false); return dt; }
        public virtual int Update(varManagerDataSet.installStatusDataTable dataTable) { return UpdateTable(dataTable); }
        public virtual int Update(varManagerDataSet dataSet) { return UpdateTable(dataSet.installStatus); }
        public virtual int Update(DataRow dataRow) { return UpdateRows(new[] { dataRow }); }
        public virtual int Update(DataRow[] dataRows) { return UpdateRows(dataRows); }

        /// <summary>Original Access query was "DELETE * FROM installStatus".</summary>
        public virtual int DeleteAll() { return ExecuteNonQuery("DELETE FROM installStatus"); }

        /// <summary>
        /// Atomically replaces the entire installStatus table in one transaction:
        /// DELETE all existing rows then INSERT the new set.  Because DELETE and INSERT
        /// share the same transaction, concurrent readers (e.g. the varsView Fill on the
        /// UI thread) always see either the complete old state or the complete new state —
        /// never an empty gap that would make all vars appear uninstalled.
        /// </summary>
        public virtual int BulkReplace(IEnumerable<(string varName, bool installed, bool disabled)> rows)
        {
            var conn = Connection;
            bool opened = conn.State != ConnectionState.Open;
            if (opened) conn.Open();
            try
            {
                using (var tx = conn.BeginTransaction())
                {
                    using (var del = new SQLiteCommand("DELETE FROM installStatus", conn, tx))
                        del.ExecuteNonQuery();
                    using (var cmd = new SQLiteCommand(
                        "INSERT INTO installStatus(varName, Installed, Disabled) VALUES(?,?,?)", conn, tx))
                    {
                        var p1 = new SQLiteParameter(); cmd.Parameters.Add(p1);
                        var p2 = new SQLiteParameter(); cmd.Parameters.Add(p2);
                        var p3 = new SQLiteParameter(); cmd.Parameters.Add(p3);
                        int count = 0;
                        foreach (var (varName, installed, disabled) in rows)
                        {
                            p1.Value = varName;
                            p2.Value = installed;
                            p3.Value = disabled;
                            cmd.ExecuteNonQuery();
                            count++;
                        }
                        tx.Commit();
                        return count;
                    }
                }
            }
            finally { if (opened) conn.Close(); }
        }
    }

    // -----------------------------------------------------------------------
    public partial class savedepensTableAdapter : SqliteAdapterBase
    {
        protected override string SelectCommandText { get { return "SELECT ID, savepath, modidate, dependency FROM savedepens"; } }
        private const string ByDepensSql = "SELECT ID, savepath, modidate, dependency FROM savedepens WHERE (dependency = ?)";

        public virtual int Fill(varManagerDataSet.savedepensDataTable dataTable) { return FillTable(dataTable, SelectCommandText, ClearBeforeFill); }
        public virtual varManagerDataSet.savedepensDataTable GetData() { var dt = new varManagerDataSet.savedepensDataTable(); FillTable(dt, SelectCommandText, false); return dt; }
        public virtual int FillByDepens(varManagerDataSet.savedepensDataTable dataTable, string dependency) { return FillTable(dataTable, ByDepensSql, ClearBeforeFill, dependency); }
        public virtual varManagerDataSet.savedepensDataTable GetDataByDepens(string dependency) { var dt = new varManagerDataSet.savedepensDataTable(); FillTable(dt, ByDepensSql, false, dependency); return dt; }
        public virtual int Update(varManagerDataSet.savedepensDataTable dataTable) { return UpdateTable(dataTable); }
        public virtual int Update(varManagerDataSet dataSet) { return UpdateTable(dataSet.savedepens); }
        public virtual int Update(DataRow dataRow) { return UpdateRows(new[] { dataRow }); }
        public virtual int Update(DataRow[] dataRows) { return UpdateRows(dataRows); }
    }

    // -----------------------------------------------------------------------
    public partial class varsTableAdapter : SqliteAdapterBase
    {
        protected override string SelectCommandText { get { return "SELECT varName, varPath, creatorName, packageName, version, metaDate, varDate, filesize, scenes, looks, clothing, hairstyle, plugins, assets, morphs, pose, skin FROM vars"; } }

        public virtual int Fill(varManagerDataSet.varsDataTable dataTable) { return FillTable(dataTable, SelectCommandText, ClearBeforeFill); }
        public virtual varManagerDataSet.varsDataTable GetData() { var dt = new varManagerDataSet.varsDataTable(); FillTable(dt, SelectCommandText, false); return dt; }
        public virtual int Update(varManagerDataSet.varsDataTable dataTable) { return UpdateTable(dataTable); }
        public virtual int Update(varManagerDataSet dataSet) { return UpdateTable(dataSet.vars); }
        public virtual int Update(DataRow dataRow) { return UpdateRows(new[] { dataRow }); }
        public virtual int Update(DataRow[] dataRows) { return UpdateRows(dataRows); }
    }

    // -----------------------------------------------------------------------
    public partial class scenesTableAdapter : SqliteAdapterBase
    {
        protected override string SelectCommandText { get { return "SELECT ID, varName, atomType, isPreset, scenePath, previewPic FROM scenes"; } }
        private const string ByVarNameSql = "SELECT ID, varName, atomType, isPreset, scenePath, previewPic FROM scenes WHERE (varName = ?)";
        private const string LoadableSql = "SELECT ID, varName, atomType, isPreset, scenePath, previewPic FROM scenes WHERE (atomType = 'scenes') OR (isPreset = 1)";

        public virtual int Fill(varManagerDataSet.scenesDataTable dataTable) { return FillTable(dataTable, SelectCommandText, ClearBeforeFill); }
        public virtual varManagerDataSet.scenesDataTable GetData() { var dt = new varManagerDataSet.scenesDataTable(); FillTable(dt, SelectCommandText, false); return dt; }
        public virtual int FillBy(varManagerDataSet.scenesDataTable dataTable, string varName) { return FillTable(dataTable, ByVarNameSql, ClearBeforeFill, varName); }
        public virtual varManagerDataSet.scenesDataTable GetDataByVarName(string varName) { var dt = new varManagerDataSet.scenesDataTable(); FillTable(dt, ByVarNameSql, false, varName); return dt; }
        public virtual int FillByLoadable(varManagerDataSet.scenesDataTable dataTable) { return FillTable(dataTable, LoadableSql, ClearBeforeFill); }
        public virtual varManagerDataSet.scenesDataTable GetByLoadable() { var dt = new varManagerDataSet.scenesDataTable(); FillTable(dt, LoadableSql, false); return dt; }
        public virtual int Update(varManagerDataSet.scenesDataTable dataTable) { return UpdateTable(dataTable); }
        public virtual int Update(varManagerDataSet dataSet) { return UpdateTable(dataSet.scenes); }
        public virtual int Update(DataRow dataRow) { return UpdateRows(new[] { dataRow }); }
        public virtual int Update(DataRow[] dataRows) { return UpdateRows(dataRows); }
    }

    // -----------------------------------------------------------------------
    //  Read-only view adapters (installedScenes, varsView). Fill / GetData only.
    // -----------------------------------------------------------------------
    public partial class installedScenesTableAdapter : SqliteAdapterBase
    {
        private const string Sql = "SELECT ID, atomType, varName, scenePath, previewPic, metaDate, varDate, Installed FROM installedScenes";
        public virtual int Fill(varManagerDataSet.installedScenesDataTable dataTable) { return FillTable(dataTable, Sql, ClearBeforeFill); }
        public virtual varManagerDataSet.installedScenesDataTable GetData() { var dt = new varManagerDataSet.installedScenesDataTable(); FillTable(dt, Sql, false); return dt; }
    }

    public partial class varsViewTableAdapter : SqliteAdapterBase
    {
        private const string Sql = "SELECT varName, varPath, creatorName, packageName, version, metaDate, varDate, filesize, scenes, looks, clothing, hairstyle, plugins, assets, morphs, pose, skin, Installed, Disabled, fsize FROM varsView";
        public virtual int Fill(varManagerDataSet.varsViewDataTable dataTable) { return FillTable(dataTable, Sql, ClearBeforeFill); }
        public virtual varManagerDataSet.varsViewDataTable GetData() { var dt = new varManagerDataSet.varsViewDataTable(); FillTable(dt, Sql, false); return dt; }
    }

    // -----------------------------------------------------------------------
    //  TableAdapterManager — configured by the Form designers (it sets the
    //  adapter properties + BackupDataSetBeforeUpdate + UpdateOrder). No form
    //  actually calls UpdateAll, but a working sequential implementation is
    //  provided for completeness.
    // -----------------------------------------------------------------------
    public partial class TableAdapterManager : global::System.ComponentModel.Component
    {
        public enum UpdateOrderOption { InsertUpdateDelete = 0, UpdateInsertDelete = 1 }

        public bool BackupDataSetBeforeUpdate { get; set; }
        public UpdateOrderOption UpdateOrder { get; set; }

        public dependenciesTableAdapter dependenciesTableAdapter { get; set; }
        public installStatusTableAdapter installStatusTableAdapter { get; set; }
        public savedepensTableAdapter savedepensTableAdapter { get; set; }
        public varsTableAdapter varsTableAdapter { get; set; }
        public scenesTableAdapter scenesTableAdapter { get; set; }

        public virtual int UpdateAll(varManagerDataSet dataSet)
        {
            if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
            if (!dataSet.HasChanges()) return 0;
            int result = 0;
            if (varsTableAdapter != null) result += varsTableAdapter.Update(dataSet);
            if (installStatusTableAdapter != null) result += installStatusTableAdapter.Update(dataSet);
            if (scenesTableAdapter != null) result += scenesTableAdapter.Update(dataSet);
            if (dependenciesTableAdapter != null) result += dependenciesTableAdapter.Update(dataSet);
            if (savedepensTableAdapter != null) result += savedepensTableAdapter.Update(dataSet);
            return result;
        }
    }
}
