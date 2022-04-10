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
    public partial class FormSwitchAdd : Form
    {
        private string switchName;

        public string SwitchName { get => switchName; set => switchName = value; }

        public FormSwitchAdd()
        {
            InitializeComponent();
        }

        private void FormSwitchAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                e.Cancel = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSwitchName.Text.Trim()))
                this.DialogResult = DialogResult.None;
            else
            {
                SwitchName = textBoxSwitchName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
