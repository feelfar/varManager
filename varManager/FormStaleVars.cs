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
    public partial class FormStaleVars : Form
    {
        public bool removeOldVersion;
        public FormStaleVars()
        {
            InitializeComponent();
            removeOldVersion = false;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            removeOldVersion = checkBoxRemoveOldVersion.Checked;
        }
    }
}
