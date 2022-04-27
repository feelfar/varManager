using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace varManager
{

    public partial class FormVarsMove : Form
    {
        private string varlinkDirName;
        private string movetoDirName;
        private List<string> varsToMove;
        public FormVarsMove()
        {
            InitializeComponent();
        }

        public string VarlinkDirName { get => varlinkDirName; set => varlinkDirName = value; }
        public string MovetoDirName { get => movetoDirName; set => movetoDirName = value; }
        public List<string> VarsToMove { get => varsToMove; set => varsToMove = value; }

        private void FormVarsMove_Load(object sender, EventArgs e)
        {
            labelTided.Text = "\\AddonPackages\\" + varlinkDirName + "\\";
            foreach (string var in varsToMove)
                listView1.Items.Add(var);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMoveto.Text.Trim()))
                this.DialogResult = DialogResult.None;
            else
                movetoDirName = textBoxMoveto.Text;

        }

        private void FormVarsMove_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                e.Cancel = true;
        }
    }
}
