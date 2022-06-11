using System;
using System.IO;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void buttonVarspath_Click(object sender, EventArgs e)
        {
            folderBrowserDialogVars.SelectedPath = textBoxVarspath.Text;
            folderBrowserDialogVars.ShowDialog();
            textBoxVarspath.Text = folderBrowserDialogVars.SelectedPath;
            //
        }

        private void buttonVamPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialogVam.SelectedPath = textBoxVamPath.Text;
            folderBrowserDialogVam.ShowDialog();
            textBoxVamPath.Text = folderBrowserDialogVam.SelectedPath;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            textBoxVarspath.Text = Properties.Settings.Default.varspath;
            textBoxVamPath.Text = Properties.Settings.Default.vampath;

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string varspath = new DirectoryInfo(textBoxVarspath.Text).FullName.ToLower();
            string packpath = new DirectoryInfo(Path.Combine(textBoxVamPath.Text, "AddonPackages")).FullName.ToLower();
            if (!File.Exists(Path.Combine(textBoxVamPath.Text, "VaM.exe")))
            {
                MessageBox.Show("VAM path is incorrect.");
                this.DialogResult = DialogResult.None;
                return;
            }
            if (varspath == packpath)
            {
                MessageBox.Show("Vars Path can't be {VamInstallDir}\\AddonPackages");
                this.DialogResult = DialogResult.None;
                return;
            }
            Properties.Settings.Default.varspath = textBoxVarspath.Text;
            Properties.Settings.Default.vampath = textBoxVamPath.Text;
            Properties.Settings.Default.Save();
            if(!Directory.Exists(Properties.Settings.Default.varspath))
                Directory.CreateDirectory(Properties.Settings.Default.varspath);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                e.Cancel = true;
        }
    }
}
