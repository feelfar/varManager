using System;
using System.Windows.Forms;

namespace varManager
{
    public partial class FormSwitchRename : Form
    {
        private string newName;

        public string NewName { get => newName; set => newName = value; }
        private string oldName;

        public string OldName { get => oldName; set => oldName = value; }
        public FormSwitchRename()
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
            if (string.IsNullOrWhiteSpace(textBoxSwitchNewName.Text.Trim()))
                this.DialogResult = DialogResult.None;
            else if (textBoxSwitchNewName.Text.ToLower() == textBoxSwitchOldName.Text.ToLower())
            {
                this.DialogResult = DialogResult.None;
            }
            else
            {
                NewName = textBoxSwitchNewName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FormSwitchRename_Load(object sender, EventArgs e)
        {
            textBoxSwitchOldName.Text = oldName;
        }
    }
}
