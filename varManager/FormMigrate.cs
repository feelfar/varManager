using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace varManager
{
    /// <summary>
    /// In-app tool to import data from a legacy Access <c>varManager.mdb</c> into the new
    /// SQLite database. The user picks the source (.mdb or its folder) and the target
    /// (.sqlite) explicitly, so it works regardless of where the old/new installs live.
    /// Runs on a background thread with live progress. Requires the ACE OLEDB provider to
    /// read the .mdb.
    /// </summary>
    public partial class FormMigrate : Form
    {
        private bool _running;

        public FormMigrate()
        {
            InitializeComponent();
            // Default the target to the database the running app actually uses.
            txtTarget.Text = Db.DatabasePath ?? "";
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "选择旧版 Access 数据库";
                dlg.Filter = "varManager 数据库 (varManager.mdb;*.mdb)|varManager.mdb;*.mdb|所有文件 (*.*)|*.*";
                dlg.CheckFileExists = true;
                try { if (!string.IsNullOrEmpty(txtSource.Text)) dlg.InitialDirectory = Path.GetDirectoryName(txtSource.Text); } catch { }
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    txtSource.Text = dlg.FileName;
            }
        }

        private void btnBrowseTarget_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "选择目标 SQLite 数据库(可选择已有文件或输入新文件名)";
                dlg.Filter = "SQLite 数据库 (*.sqlite)|*.sqlite|所有文件 (*.*)|*.*";
                dlg.CheckFileExists = false;
                dlg.CheckPathExists = true;
                try
                {
                    if (!string.IsNullOrEmpty(txtTarget.Text))
                    {
                        dlg.InitialDirectory = Path.GetDirectoryName(txtTarget.Text);
                        dlg.FileName = Path.GetFileName(txtTarget.Text);
                    }
                }
                catch { }
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    txtTarget.Text = dlg.FileName;
            }
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            string src = (txtSource.Text ?? "").Trim().Trim('"');
            string dst = (txtTarget.Text ?? "").Trim().Trim('"');

            // Allow picking the old program folder instead of the file itself.
            if (Directory.Exists(src))
                src = Path.Combine(src, "varManager.mdb");

            if (!File.Exists(src))
            {
                MessageBox.Show(this, "找不到源 .mdb 文件:\n" + src, "varManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(dst))
            {
                MessageBox.Show(this, "请填写目标 SQLite 文件路径。", "varManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool clear = chkClear.Checked;
            if (clear && MessageBox.Show(this,
                    "将先清空目标库的 vars / scenes / installStatus / dependencies / savedepens 等表,再从旧库全量导入。\n\n确定继续?",
                    "确认全量覆盖", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;

            SetRunning(true);
            txtLog.Clear();
            Log("源 : " + src);
            Log("目标: " + dst);
            Log(clear ? "模式: 全量覆盖(先清空目标表)" : "模式: 追加导入");
            Log("----------------------------------------");

            string connStr = Db.BuildConnectionString(dst);
            MigrateResult result = null;
            Exception error = null;
            try
            {
                result = await Task.Run(() => MdbToSqliteMigrator.MigrateFull(src, connStr, clear, Log));
            }
            catch (Exception ex) { error = ex; }

            SetRunning(false);

            if (error != null)
            {
                Log("迁移失败: " + error.Message);
                MessageBox.Show(this,
                    "迁移失败:\n" + error.Message +
                    "\n\n(读取 .mdb 需要安装 Microsoft Access Database Engine / ACE OLEDB 驱动。)",
                    "varManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Log("========== 汇总 ==========");
            foreach (var t in result.Tables)
                Log(string.Format("{0,-14} 读取 {1,8}  写入 {2,8}{3}",
                    t.Table, t.Read, t.Inserted, t.Error != null ? "  错误: " + t.Error : ""));
            Log("合计写入 " + result.TotalInserted + " 行。");

            string msg = "迁移完成,共写入 " + result.TotalInserted + " 行。";
            try
            {
                if (!string.IsNullOrEmpty(Db.DatabasePath) &&
                    string.Equals(Path.GetFullPath(dst), Path.GetFullPath(Db.DatabasePath), StringComparison.OrdinalIgnoreCase))
                    msg += "\n\n目标即当前程序数据库——请重启 varManager 以加载新数据。";
            }
            catch { }
            MessageBox.Show(this, msg, "varManager", MessageBoxButtons.OK,
                result.HasErrors ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!_running) Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_running)
            {
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }

        private void SetRunning(bool running)
        {
            _running = running;
            txtSource.Enabled = !running;
            btnBrowseSource.Enabled = !running;
            txtTarget.Enabled = !running;
            btnBrowseTarget.Enabled = !running;
            chkClear.Enabled = !running;
            btnStart.Enabled = !running;
            btnClose.Enabled = !running;
            progressBar.Style = running ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            progressBar.MarqueeAnimationSpeed = running ? 30 : 0;
            Cursor = running ? Cursors.AppStarting : Cursors.Default;
        }

        private void Log(string message)
        {
            if (txtLog.IsHandleCreated && txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke((Action)(() => Log(message)));
                return;
            }
            txtLog.AppendText(message + Environment.NewLine);
        }
    }
}
