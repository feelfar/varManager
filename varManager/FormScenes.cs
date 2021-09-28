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
    public partial class FormScenes : Form
    {
        private static string previewpicsDirName = "___PreviewPics___";
        private static string installLinkDirName = "___VarsLink___";
        private static int maxitemPerpage = 200;
        public FormScenes()
        {
            InitializeComponent();
        }

        private void scenesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.scenesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.varManagerDataSet);

        }
        public struct InstalledScene
        {
            public InstalledScene(string atomtype,string varname,DateTime installdate, string scenepath, string picpath, bool installed)
            {
                Atomtype = atomtype;
                Varname = varname;
                Installdate = installdate;
                Scenepath = scenepath;
                Picpath = picpath;
                Installed = installed;
            }
            public string Atomtype { get; }
            public string Varname { get; }
            public DateTime Installdate { get; }
            public string Scenepath { get; }
            public string Picpath { get; }
            public bool Installed { get; }
        }

        private List<InstalledScene> listInstalledScene = new List<InstalledScene>();
        private List<InstalledScene> listFilterScene = new List<InstalledScene>();
        private List<InstalledScene> listFilterCreatorScene = new List<InstalledScene>();
        private List<InstalledScene> listFilterPageScene = new List<InstalledScene>();
        private void FormScenes_Load(object sender, EventArgs e)
        {
            toolStripComboBoxCategory.SelectedIndex = 0;
            // TODO: 这行代码将数据加载到表“varManagerDataSet.scenes”中。您可以根据需要移动或删除它。
            this.scenesTableAdapter.Fill(this.varManagerDataSet.scenes);
            
            AllInstalledVars();
            
        }
        
        private void AllInstalledVars()
        {
            DirectoryInfo dirinfoInstalled = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName));

            IOrderedEnumerable<FileSystemInfo> fileinfos =  dirinfoInstalled.GetFileSystemInfos("*.var").OrderBy(f => f.CreationTime);
            foreach (var fileinfo in fileinfos)
            {
                string varName = Path.GetFileNameWithoutExtension(fileinfo.Name);
                foreach (var varscene in  this.varManagerDataSet.scenes.Where(q=>q.varName==varName))
                {
                    listInstalledScene.Add(new InstalledScene(varscene.atomType, varscene.varName, fileinfo.CreationTime, varscene.scenePath, varscene.previewPic, false));
                }
            }

        }

        private void toolStripComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void FilterVars()
        {
            if (!string.IsNullOrWhiteSpace(toolStripComboBoxCategory.Text))
                listFilterScene = listInstalledScene.Where(q => q.Atomtype == toolStripComboBoxCategory.Text).ToList();
            if (!string.IsNullOrWhiteSpace(toolStripTextBoxFilter.Text))
            {
                string filtertext = toolStripTextBoxFilter.Text.Trim().ToLower();
                listFilterScene = listFilterScene.Where(q => q.Varname.ToLower().IndexOf(filtertext) >= 0 ||
                                     Path.GetFileNameWithoutExtension(q.Scenepath).ToLower().IndexOf(filtertext) >= 0).ToList();
            }
            toolStripComboBoxCreator.SelectedIndexChanged -= new System.EventHandler(toolStripComboBoxCreator_SelectedIndexChanged);
            string creatorname = toolStripComboBoxCreator.Text;
            var Creators = listFilterScene.Select(q => q.Varname.Substring(0, q.Varname.IndexOf('.'))).Distinct().OrderBy(o => o).ToArray();
            toolStripComboBoxCreator.Items.Clear();
            toolStripComboBoxCreator.Items.Add("____ALL");
            toolStripComboBoxCreator.Items.AddRange(Creators);
            if (!string.IsNullOrEmpty(creatorname) && creatorname != "____ALL")
            {
                if (toolStripComboBoxCreator.Items.Contains(creatorname))
                {
                    toolStripComboBoxCreator.SelectedItem = creatorname;
                    listFilterCreatorScene = listFilterScene.Where(q => q.Varname.StartsWith(creatorname)).ToList();
                }
            }
            else
            {
                toolStripComboBoxCreator.SelectedIndex = 0;
                listFilterCreatorScene = listFilterScene.ToList();
            }
            toolStripComboBoxCreator.SelectedIndexChanged += new System.EventHandler(toolStripComboBoxCreator_SelectedIndexChanged);
        }

        private void toolStripTextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            FilterVars();
        }

        private void toolStripComboBoxCreator_SelectedIndexChanged(object sender, EventArgs e)
        {
            string creatorname = toolStripComboBoxCreator.Text;
           
            if (!string.IsNullOrEmpty(creatorname) && creatorname != "____ALL")
            {
                    listFilterCreatorScene = listFilterScene.Where(q => q.Varname.StartsWith(creatorname)).ToList();
            }
            else
            {
                listFilterCreatorScene = listFilterScene.ToList();
            }
            toolStripComboBoxScenePage.SelectedIndexChanged -= new System.EventHandler(toolStripComboBoxScenePage_SelectedIndexChanged);

            int pages = (int)((listFilterCreatorScene.Count + maxitemPerpage - 1) / maxitemPerpage);
            toolStripComboBoxScenePage.SelectedIndexChanged += new System.EventHandler(toolStripComboBoxScenePage_SelectedIndexChanged);
        }

        private void toolStripComboBoxScenePage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
