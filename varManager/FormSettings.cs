using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string varspath = new DirectoryInfo(textBoxVarspath.Text).FullName;
            string packpath = new DirectoryInfo(Path.Combine(textBoxVamPath.Text, "AddonPackages")).FullName;
            if (varspath == packpath)
            {
                MessageBox.Show("Vars Path can't be {VamInstallDir}\\AddonPackages");
                return;
            }
            Properties.Settings.Default.varspath = textBoxVarspath.Text;
            Properties.Settings.Default.vampath = textBoxVamPath.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

    }
}
