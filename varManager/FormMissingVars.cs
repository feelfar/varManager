using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class FormMissingVars : Form
    {
        private static string missingVarLinkDirName = "___MissingVarLink___";
        private List<string> missingVars;
        public Form1 form1;
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
            toolStripComboBoxIgnoreVersion.SelectedIndex = 0;
            FillMissVarGridView();
        }

        private void FillMissVarGridView()
        {
            if (missingVars != null)
            {
                dataGridViewMissingVars.Rows.Clear();
                foreach (string missingvar in missingVars)
                {
                    string missingvarname = missingvar;
                    if(missingvarname.EndsWith("$"))
                    {
                        if (toolStripComboBoxIgnoreVersion.SelectedIndex == 1)
                        {
                            missingvarname = missingvarname.Substring(0, missingvarname.Length - 1);
                        }
                        else
                            continue;
                    }
                    if (missingvarname.LastIndexOf('/') > 1)
                        missingvarname = missingvarname.Substring(missingvarname.LastIndexOf('/') + 1);
                    string searchPattern = missingvarname + ".var";
                    if (missingvarname.IndexOf(".latest") > 0)
                        searchPattern = missingvarname.Substring(0, missingvarname.LastIndexOf('.') + 1) + "*.var";
                    var files = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName), searchPattern, SearchOption.AllDirectories).OrderByDescending(q => Path.GetFileNameWithoutExtension(q)).ToArray();
                    if (files.Length == 0)
                        dataGridViewMissingVars.Rows.Add(new string[] { missingvarname, "", "UnLink", "Google" });
                    else
                    {
                        string destfilename = Path.GetFileNameWithoutExtension(Comm.ReparsePoint(files[0]));
                        dataGridViewMissingVars.Rows.Add(new string[] { missingvarname, destfilename, "UnLink", "Google" });
                    }

                }
                bindingNavigatorCountItem.Text = "/" + dataGridViewMissingVars.Rows.Count;
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
                strFilter += " AND varName Like '%" + Regex.Replace(Regex.Replace(textBoxFilter.Text.Trim(), @"[\x5B\x5D]", "[$0]", RegexOptions.Multiline), @"[\x27]", @"\x27\x27", RegexOptions.Multiline) + "*'";
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

                List<string> depends = form1.GetDependents(missingVarName);
                dataGridViewDependent.Rows.Clear();
                foreach (string depend in depends)
                {
                    dataGridViewDependent.Rows.Add(depend, "locate");
                }
            }
        }

        private void buttonLinkto_Click(object sender, EventArgs e)
        {
            string missingvar = textBoxMissingVar.Text;
            string linkvar = textBoxLinkVar.Text;
            foreach (DataGridViewRow row in dataGridViewMissingVars.Rows)
            {
                if (row.Cells[0].Value.ToString() == missingvar)
                {
                    row.Cells[1].Value = linkvar;
                    break;
                }
            }
        }

        private void dataGridViewMissingVars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dataGridViewMissingVars.Rows[e.RowIndex].Cells[1].Value = "";
            }
            if (e.ColumnIndex == 3)
            {
                string varname = dataGridViewMissingVars.Rows[e.RowIndex].Cells[0].Value.ToString().Replace(".latest", ".1");
                System.Diagnostics.Process.Start("https://www.google.com/search?q=" + varname + " var");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Createlink();

            this.Close();
        }

        private void Createlink()
        {
            foreach (DataGridViewRow row in dataGridViewMissingVars.Rows)
            {
                string missingvarname = row.Cells[0].Value.ToString();
                string destvarname = row.Cells[1].Value.ToString();
                string searchPattern = missingvarname + ".var";
                if (missingvarname.IndexOf(".latest") > 0)
                    searchPattern = missingvarname.Substring(0, missingvarname.LastIndexOf('.') + 1) + "*.var";
                var files = Directory.GetFiles(Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName), searchPattern, SearchOption.AllDirectories).OrderByDescending(q => Path.GetFileNameWithoutExtension(q)).ToArray();
                if (files.Length > 0)
                {
                    File.Delete(files[0]);
                    if (File.Exists(files[0] + ".disabled"))
                        File.Delete(files[0] + ".disabled");
                }

                if (!string.IsNullOrEmpty(destvarname))
                {
                    varManagerDataSet.varsRow varsrow = varManagerDataSet.vars.FindByvarName(destvarname);
                    if (missingvarname.Substring(missingvarname.LastIndexOf('.')) == ".latest")
                    {
                        missingvarname = missingvarname.Substring(0, missingvarname.LastIndexOf('.')) + destvarname.Substring(destvarname.LastIndexOf('.'));
                    }
                    if (varsrow != null)
                    {
                        string missingvar = Path.Combine(Settings.Default.vampath, "AddonPackages", missingVarLinkDirName, missingvarname + ".var");
                        string destvarfile = Path.Combine(Settings.Default.varspath, varsrow.varPath, varsrow.varName + ".var");
                        Comm.CreateSymbolicLink(missingvar, destvarfile, Comm.SYMBOLIC_LINK_FLAG.File);
                        Comm.SetSymboLinkFileTime(missingvar, File.GetCreationTime(destvarfile), File.GetLastWriteTime(destvarfile));
                        //File.SetCreationTime(missingvar, File.GetCreationTime(destvarfile));
                        //File.SetLastWriteTime(missingvar, File.GetLastWriteTime(destvarfile));
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewMissingVars_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            bindingNavigatorPositionItem.Text = (e.RowIndex + 1).ToString();
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            int nRow = dataGridViewMissingVars.CurrentCell.RowIndex;
            if (nRow < dataGridViewMissingVars.RowCount - 1)
            {
                dataGridViewMissingVars.CurrentCell = dataGridViewMissingVars.Rows[++nRow].Cells[0];
            }
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            int nRow = dataGridViewMissingVars.CurrentCell.RowIndex;
            if (nRow > 0)
            {
                dataGridViewMissingVars.CurrentCell = dataGridViewMissingVars.Rows[--nRow].Cells[0];
            }
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            int nRow = dataGridViewMissingVars.CurrentCell.RowIndex;
            if (nRow < dataGridViewMissingVars.RowCount - 1)
            {
                dataGridViewMissingVars.CurrentCell = dataGridViewMissingVars.Rows[dataGridViewMissingVars.RowCount - 1].Cells[0];
            }
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            int nRow = dataGridViewMissingVars.CurrentCell.RowIndex;
            if (nRow > 0)
            {
                dataGridViewMissingVars.CurrentCell = dataGridViewMissingVars.Rows[0].Cells[0];
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSaveTxt.ShowDialog() == DialogResult.OK)
            {
                List<string> varNames = new List<string>();
                foreach (var varstatus in this.varManagerDataSet.installStatus)
                {
                    if (varstatus.Installed) varNames.Add(varstatus.varName);
                }
                File.WriteAllLines(saveFileDialogSaveTxt.FileName, varNames.ToArray());
            }
        }
        private void buttonSaveTxt_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSaveTxt.ShowDialog() == DialogResult.OK)
            {
                List<string> varlinktoList = new List<string>();
                foreach (DataGridViewRow row in dataGridViewMissingVars.Rows)
                {
                    string missingvarname = row.Cells[0].Value.ToString();
                    string destvarname = row.Cells[1].Value.ToString();
                    if (!string.IsNullOrEmpty(destvarname))
                    {
                        varlinktoList.Add($"{missingvarname}|{destvarname}");
                    }
                }

                File.WriteAllLines(saveFileDialogSaveTxt.FileName, varlinktoList.ToArray());
            }
        }
        private void buttonLoadTxt_Click(object sender, EventArgs e)
        {
            if (openFileDialogLoadTXT.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, string> varlinktoDict = new Dictionary<string, string>();
                foreach (string varlinkto in File.ReadAllLines(openFileDialogLoadTXT.FileName))
                {
                    string[] varlinktos = varlinkto.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (varlinktos.Length == 2)
                    {
                        varlinktoDict[varlinktos[0]] = varlinktos[1];
                    }
                }
                foreach (DataGridViewRow row in dataGridViewMissingVars.Rows)
                {
                    string missingvarname = row.Cells[0].Value.ToString();
                    if (varlinktoDict.ContainsKey(missingvarname))
                    {
                        row.Cells[1].Value = varlinktoDict[missingvarname];
                    }
                }
            }

        }

        private void toolStripComboBoxIgnoreVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillMissVarGridView();
        }

        private void dataGridViewDependent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDependent.Columns[e.ColumnIndex].Name == "ColumnLocate" && e.RowIndex >= 0)
            {
                string dependentName = dataGridViewDependent.Rows[e.RowIndex].Cells["ColumnDependentName"].Value.ToString();
                if (dependentName.StartsWith("\\"))
                {
                    dependentName = dependentName.Substring(1);
                    string destsavedfile = Path.Combine(Settings.Default.vampath, dependentName);
                    Comm.LocateFile(destsavedfile);

                }
                else
                {
                    if (Form1.ComplyVarName(dependentName))
                    {
                        form1.LocateVar(dependentName);
                        form1.SelectVarInList(dependentName);
                        form1.Activate();
                    }
                }

            }
        }

        private void varsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (varsDataGridView.Columns[e.ColumnIndex].Name == "ColumnLocateExistVar" && e.RowIndex >= 0)
            {
                string varName = varsDataGridView.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumnvarName"].Value.ToString();
                form1.LocateVar(varName);
                form1.SelectVarInList(varName);
               
                form1.Activate();
            }
        }
    }
}
