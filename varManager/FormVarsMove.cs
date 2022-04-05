using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace varManager
{
   
    public partial class FormVarsMove : Form
    {
        private string tidiedDirName;
        private string movetoDirName;
        private List<string> varsToMove;
        public FormVarsMove()
        {
            InitializeComponent();
        }

        public string TidiedDirName { get => tidiedDirName; set => tidiedDirName = value; }
        public string MovetoDirName { get => movetoDirName; set => movetoDirName = value; }
        public List<string> VarsToMove { get => varsToMove; set => varsToMove = value; }

        private void FormVarsMove_Load(object sender, EventArgs e)
        {
            labelTided.Text ="\\"+ tidiedDirName+"\\";
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
