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
using varManager.Properties;

namespace varManager
{
    public partial class FormVarDetail : Form
    {
        public string strVarName;
        public Form1 form1;
        public string strAction;
        public Dictionary<string,string> dependencies;
        public List<string> DependentVarList;
        public List<string> DependentJsonList;
        public FormVarDetail()
        {
            InitializeComponent();
        }

        private void FormVarDetail_Load(object sender, EventArgs e)
        {
            textBoxVarName.Text = strVarName;

            foreach (var dep in dependencies)
            {
                if (dep.Value == "missing")
                    dataGridViewDependency.Rows.Add("search", dep.Key);
                else
                    dataGridViewDependency.Rows.Add("locate", dep.Key);
            }
            foreach (DataGridViewRow deprow in dataGridViewDependency.Rows)
            {

                string dependName = (string)deprow.Cells["ColumnDependName"].Value;
                string dependVar = dependencies[dependName];
                if (dependVar == "missing")
                {
                    deprow.DefaultCellStyle.BackColor = Color.Red;
                }
                else if (!dependName.ToLower().EndsWith("latest"))
                {
                    if (dependName.ToLower() != dependVar.ToLower())
                        deprow.DefaultCellStyle.BackColor = Color.Yellow;
                }

            }
            foreach (string dependentvar in DependentVarList)
            {
                dataGridViewDependentVar.Rows.Add("locate", dependentvar);
            }
            foreach (DataGridViewRow deprow in dataGridViewDependentVar.Rows)
            {

                string dependentVar = (string)deprow.Cells["ColumnDependentVar"].Value;
               
                if (form1.IsVarInstalled(dependentVar))
                {
                    deprow.DefaultCellStyle.BackColor = Color.Green;
                }

            }
            foreach (string dependentsaved in DependentJsonList)
            {
                dataGridViewDependentSaved.Rows.Add("locate", dependentsaved);
            }
        }

        private void buttonLocate_Click(object sender, EventArgs e)
        {
            form1.LocateVar(strVarName);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            strAction = "filter";
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridViewDependency_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDependency.Columns[e.ColumnIndex].Name == "ColumnAction1" && e.RowIndex >= 0)
            {
                string dependName = dataGridViewDependency.Rows[e.RowIndex].Cells["ColumnDependName"].Value.ToString();
                string dependVar = dependencies[dependName];
                if (dependVar == "missing")
                {
                    string varname = dependName.Replace(".latest", ".1");
                    System.Diagnostics.Process.Start("https://www.google.com/search?q=" + varname + " var");
                }
                else
                {
                    form1.SelectVarInList(dependVar);
                    form1.Activate();
                }
            }
        }

        private void dataGridViewDependentVar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDependentVar.Columns[e.ColumnIndex].Name == "ColumnAction2" && e.RowIndex >= 0)
            {
                string varName = dataGridViewDependentVar.Rows[e.RowIndex].Cells["ColumnDependentVar"].Value.ToString();

                form1.SelectVarInList(varName);
                form1.Activate();
            }
        }

        private void dataGridViewDependentSaved_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDependentSaved.Columns[e.ColumnIndex].Name == "ColumnAction3" && e.RowIndex >= 0)
            {
                string saved = dataGridViewDependentSaved.Rows[e.RowIndex].Cells["ColumnDependentSaved"].Value.ToString();
                if(saved.StartsWith("\\"))
                    saved = saved.Substring(1);

                string destsavedfile = Path.Combine(Settings.Default.vampath, saved);
                Comm.LocateFile(destsavedfile);

            }
        }
    }
}
