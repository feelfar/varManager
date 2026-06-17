using System;
using System.Windows.Forms;

namespace varManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // Match the original .NET Framework app, which declared no DPI awareness
            // (Windows bitmap-scales it). .NET 8 WinForms is DPI-aware by default and would
            // re-run AutoScaleMode.Font scaling, which shifts/clips the fixed-size controls
            // (e.g. the 70x42 bottom buttons designed for two-line Cambria 9 text).
            // DPI mode (DpiUnaware) + visual styles + GDI text rendering are declared via the
            // csproj Application* properties and applied by this generated initializer, which
            // reproduces the original layout exactly across every form.
            ApplicationConfiguration.Initialize();
            try
            {
                Db.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库初始化失败:\n" + ex.Message +
                    "\n\n数据目录: " + (Db.DataDirectory ?? "(未确定)"),
                    "varManager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (args != null && args.Length > 0 &&
                string.Equals(args[0], "--migrate", StringComparison.OrdinalIgnoreCase))
            {
                Application.Run(new FormMigrate());
                return;
            }
            Application.Run(new Form1());
        }
    }
}
