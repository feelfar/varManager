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
    public partial class FormMissingVars : Form
    {
        private static string missingVarLinkDirName = "___MissingVarLink___";
        private List<string> missingVars;
        public FormMissingVars()
        {
            InitializeComponent();
        }

        public List<string> MissingVars { get => missingVars; set => missingVars = value; }
        public static string MissingVarLinkDirName { get => missingVarLinkDirName; set => missingVarLinkDirName = value; }

        private void FormMissingVars_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName));
            // TODO: 这行代码将数据加载到表“varManagerDataSet.vars”中。您可以根据需要移动或删除它。
            this.varsTableAdapter.Fill(this.varManagerDataSet.vars);
            if ( missingVars!=null)
            {
                foreach(var missingvar in missingVars)
                {
                    string missingvarname = missingvar;
                    if (missingvarname.LastIndexOf('/') > 1)
                        missingvarname = missingvarname.Substring(missingvarname.LastIndexOf('/') + 1);
                    string searchPattern = missingvarname.Substring(0, missingvarname.LastIndexOf('.')+1) + "*.var";
                    
                    if (Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName), searchPattern, SearchOption.AllDirectories).Length == 0)
                        dataGridViewMissingVars.Rows.Add(new string[] { missingvarname, "", "UnLink", "Google" });
                }
            }
        }


        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void FilterVars()
        {
            string strFilter = "1=1";
            if (comboBoxCreater.SelectedItem != null)
                if (comboBoxCreater.SelectedItem.ToString() != "____ALL")
                    strFilter += " AND creatorName = '" + comboBoxCreater.SelectedItem.ToString() + "'";
            if (textBoxFilter.Text.Trim() != "")
            {
                strFilter += " AND varName Like '%" + textBoxFilter.Text.Trim() + "*'";
            }

            this.comboBoxCreater.SelectedIndexChanged -= new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);
            var creators = this.varManagerDataSet.vars.GroupBy(g => g.creatorName);
            if (textBoxFilter.Text.Trim() != "")
                creators = this.varManagerDataSet.vars.Where(q => q.varName.ToLower().IndexOf(textBoxFilter.Text.Trim().ToLower()) >= 0).GroupBy(g => g.creatorName);
            string curcreator = comboBoxCreater.Text;
            comboBoxCreater.Items.Clear();
            comboBoxCreater.Items.Add("____ALL");
            foreach (var creator in creators)
            {
                comboBoxCreater.Items.Add(creator.Key);
            }
            if (comboBoxCreater.Items.Contains(curcreator))
                comboBoxCreater.SelectedItem = curcreator;
            else
                comboBoxCreater.SelectedIndex = 0;
            this.comboBoxCreater.SelectedIndexChanged += new System.EventHandler(this.comboBoxCreater_SelectedIndexChanged);

            varsBindingSource.Filter = strFilter;
            varsDataGridView.Update();
        }

        private void comboBoxCreater_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void varsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (varsDataGridView.SelectedRows.Count > 0)
                textBoxLinkVar.Text = varsDataGridView.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void dataGridViewMissingVars_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewMissingVars.SelectedRows.Count > 0)
            {
                string missingVarName = dataGridViewMissingVars.SelectedRows[0].Cells[0].Value.ToString();
                string missingvarnamepart = missingVarName.Split('.')[1];
                textBoxMissingVar.Text = missingVarName;
                textBoxFilter.Text = missingvarnamepart;
            }
        }

        private void buttonLinkto_Click(object sender, EventArgs e)
        {
            string missingvar = textBoxMissingVar.Text;
            string linkvar = textBoxLinkVar.Text;
            foreach (DataGridViewRow row in dataGridViewMissingVars.Rows)
            {
              if(  row.Cells[0].Value.ToString()== missingvar)
                {
                    row.Cells[1].Value = linkvar;
                    break;
                }
            }
        }

        private void dataGridViewMissingVars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          if(  e.ColumnIndex==2)
            {
                dataGridViewMissingVars.Rows[e.RowIndex].Cells[1].Value = "";
            }
            if (e.ColumnIndex == 3)
            {
                string varname = dataGridViewMissingVars.Rows[e.RowIndex].Cells[0].Value.ToString().Replace(".latest", ".1");
                System.Diagnostics.Process.Start("https://www.google.com/search?q="+ varname + " var");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewMissingVars.Rows)
            {
                string missingvarname = row.Cells[0].Value.ToString();
                string destvarname = row.Cells[1].Value.ToString();
                if(!string.IsNullOrEmpty(destvarname))
                {
                    varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(destvarname);
                    if(missingvarname.Substring(missingvarname.LastIndexOf('.'))==".latest")
                    {
                        missingvarname = missingvarname.Substring(0, missingvarname.LastIndexOf('.')) + destvarname.Substring(destvarname.LastIndexOf('.'));
                    }
                    if (varsrow != null)
                    {
                        string missingvar = Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName, missingvarname + ".var");
                        if (File.Exists(missingvar + ".disabled"))
                            File.Delete(missingvar + ".disabled");
                        if (File.Exists(missingvar)) continue ; 

                        string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");

                        Comm.CreateSymbolicLink(missingvar, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File);
                    }
                }
            }

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
