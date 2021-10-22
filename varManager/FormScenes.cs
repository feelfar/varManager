using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using varManager.Properties;

namespace varManager
{
    public partial class FormScenes : Form
    {
        private static string previewpicsDirName = "___PreviewPics___";
        private static string installLinkDirName = "___VarsLink___";
        private static int maxitemPerpage = 100;
        private string strOrderBy = "_";
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
            public InstalledScene(string atomtype, string varname, DateTime installdate, string scenepath, string picpath, int hidefav)
            {
                Atomtype = atomtype;
                Varname = varname;
                Installdate = installdate;
                Scenepath = scenepath;
                Picpath = picpath;
                Hidefav = hidefav;
            }
            public string Atomtype { get; }
            public string Varname { get; }
            public DateTime Installdate { get; }
            public string Scenepath { get; }
            public string Picpath { get; }
            public int Hidefav { get; set; }
        }

        private List<InstalledScene> listInstalledScene = new List<InstalledScene>();
        private List<InstalledScene> listFilterScene1 = new List<InstalledScene>();
        private List<InstalledScene> listFilterScene2 = new List<InstalledScene>();
        private List<InstalledScene> listFilterScene3 = new List<InstalledScene>();
        private List<InstalledScene> listFilterCreatorScene = new List<InstalledScene>();
        private List<InstalledScene> listFilterPageScene = new List<InstalledScene>();
        private void FormScenes_Load(object sender, EventArgs e)
        {
            panelImage.Dock = DockStyle.Fill;
            toolStripComboBoxCategory.SelectedIndex = 0;
            toolStripComboBoxHideFav.SelectedIndex = 0;
            toolStripComboBoxOrderBy.SelectedIndex = 0;
            // TODO: 这行代码将数据加载到表“varManagerDataSet.scenes”中。您可以根据需要移动或删除它。
            this.scenesTableAdapter.Fill(this.varManagerDataSet.scenes);
           
            AllInstalledVars();
            UpdateFileHidefav();
            FilterVars();
        }

        private void AllInstalledVars()
        {
            DirectoryInfo dirinfoInstalled = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName));

            IOrderedEnumerable<FileSystemInfo> fileinfos = dirinfoInstalled.GetFileSystemInfos("*.var").OrderBy(f => f.CreationTime);
            progressBar1.Dock = DockStyle.Top;
            progressBar1.Visible = true;
            int i = 0;
            foreach (var fileinfo in fileinfos)
            {
                string varName = Path.GetFileNameWithoutExtension(fileinfo.Name);
                foreach (var varscene in this.varManagerDataSet.scenes.Where(q => q.varName == varName))
                {
                    listInstalledScene.Add(new InstalledScene(varscene.atomType, varscene.varName, fileinfo.CreationTime, varscene.scenePath, varscene.previewPic, 0));
                }
                i++;
                progressBar1.Value = (i * 100 / fileinfos.Count());
               
            }
            progressBar1.Visible = false;
        }

        private void toolStripComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
        }
        
        private void UpdateFileHidefav()
        {
            for (int i=0;i< listInstalledScene.Count;i++)
            {
                var InstalledScene = listInstalledScene[i];
                InstalledScene.Hidefav = GetHighFav(listInstalledScene[i].Varname, listInstalledScene[i].Scenepath);
                listInstalledScene[i] = InstalledScene;
            }
            //strOrderBy = "";
           // FilterVars();
        }


        private void FilterVars(int filterAt=10)
        {
            if (filterAt >= 10)
            {
                if (!string.IsNullOrWhiteSpace(toolStripComboBoxCategory.Text))
                    listFilterScene1 = listInstalledScene.Where(q => q.Atomtype == toolStripComboBoxCategory.Text).ToList();
            }
            if (filterAt >= 9)
            {
                switch (toolStripComboBoxHideFav.Text)
                {
                    case "Hide":
                        listFilterScene2 = listFilterScene1.Where(q => q.Hidefav == -1).ToList(); break;
                    case "Fav":
                        listFilterScene2 = listFilterScene1.Where(q => q.Hidefav == 1).ToList(); break;
                    case "Normal":
                        listFilterScene2 = listFilterScene1.Where(q => q.Hidefav == 0).ToList(); break;
                    default:
                        listFilterScene2 = listFilterScene1; break;

                }
            }
            if (filterAt >= 8)
            {
                if (!string.IsNullOrWhiteSpace(toolStripTextBoxFilter.Text))
                {
                    string filtertext = toolStripTextBoxFilter.Text.Trim().ToLower();
                    listFilterScene3 = listFilterScene2.Where(q => q.Varname.ToLower().IndexOf(filtertext) >= 0 ||
                                         Path.GetFileNameWithoutExtension(q.Scenepath).ToLower().IndexOf(filtertext) >= 0).ToList();
                }
                else
                    listFilterScene3 = listFilterScene2;
            }
            toolStripComboBoxCreator.SelectedIndexChanged -= new System.EventHandler(toolStripComboBoxCreator_SelectedIndexChanged);
            string creatorname = toolStripComboBoxCreator.Text;
            var Creators = listFilterScene3.Select(q => q.Varname.Substring(0, q.Varname.IndexOf('.'))).Distinct().OrderBy(o => o).ToArray();
            toolStripComboBoxCreator.Items.Clear();
            toolStripComboBoxCreator.Items.Add("____ALL");
            toolStripComboBoxCreator.Items.AddRange(Creators);
            if (!string.IsNullOrEmpty(creatorname) && creatorname != "____ALL")
            {
                if (toolStripComboBoxCreator.Items.Contains(creatorname))
                {
                    toolStripComboBoxCreator.SelectedItem = creatorname;
                    listFilterCreatorScene = listFilterScene3.Where(q => q.Varname.StartsWith(creatorname)).ToList();
                }
            }
            else
            {
                toolStripComboBoxCreator.SelectedIndex = 0;
                listFilterCreatorScene = listFilterScene3.ToList();
            }
            toolStripComboBoxCreator.SelectedIndexChanged += new System.EventHandler(toolStripComboBoxCreator_SelectedIndexChanged);
            FilterVarsCreator();
        }

        private void toolStripTextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            FilterVars(8);
        }

        private void toolStripComboBoxCreator_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVarsCreator();
        }

        private void FilterVarsCreator()
        {
            string creatorname = toolStripComboBoxCreator.Text;

            if (!string.IsNullOrEmpty(creatorname) && creatorname != "____ALL")
            {
                listFilterCreatorScene = listFilterScene3.Where(q => q.Varname.StartsWith(creatorname)).ToList();
            }
            else
            {
                listFilterCreatorScene = listFilterScene3.ToList();
            }
            toolStripComboBoxScenePage.SelectedIndexChanged -= new System.EventHandler(toolStripComboBoxScenePage_SelectedIndexChanged);
            toolStripLabelSceneCount.Text = "/" + listFilterCreatorScene.Count.ToString();
            toolStripComboBoxScenePage.Items.Clear();
            int pages = (int)((listFilterCreatorScene.Count + maxitemPerpage - 1) / maxitemPerpage);
            
            for (int page = 0; page < pages; page++)
            {
                int min = page * maxitemPerpage + 1;
                int max = (page + 1) * maxitemPerpage;
                if (max > listFilterCreatorScene.Count) max = listFilterCreatorScene.Count;
                string strpage = min.ToString("000") + " - " + max.ToString("000");
                toolStripComboBoxScenePage.Items.Add(strpage);
            }

            if (pages > 0)
                toolStripComboBoxScenePage.SelectedIndex = 0;
            else
            {
                imageListScenes.Images.Clear();
                listSceneItem.Clear();
                listViewHide.Items.Clear();
                listViewNormal.Items.Clear();
                listViewFav.Items.Clear();
            }
            toolStripComboBoxScenePage.SelectedIndexChanged += new System.EventHandler(toolStripComboBoxScenePage_SelectedIndexChanged);
            GenerateItems();
        }

        private void toolStripComboBoxScenePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateItems();
        }

        private List<ListViewItem> listSceneItem = new List<ListViewItem>();

        private string  GetPreviewPicPath(string atomType,string varName,string picName)
        {
            
            string fullpicname = Path.Combine(Settings.Default.varspath, previewpicsDirName, atomType, varName,picName);
            if (File.Exists(fullpicname))
                return fullpicname;
            else
                return "";
        }
        private int GetHighFav( string varName, string scenepath)
        {
            if (File.Exists(Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", varName, scenepath + ".hide")))
            {
                return -1;
            }
            if (File.Exists(Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", varName, scenepath + ".fav")))
            {
                return 1;
            }
            return 0;
        }
        private void UpdateHidefav(string varName, string scenepath)
        {

            int index = listInstalledScene.FindIndex(q => q.Varname == varName && q.Scenepath == scenepath);
            var installedScene = listInstalledScene[index];
            installedScene.Hidefav = GetHighFav(varName, scenepath);
            listInstalledScene[index] = installedScene;
        }
        private void GenerateItems()
        {
            if (toolStripComboBoxScenePage.SelectedIndex < 0) return;
            if (toolStripComboBoxOrderBy.Text != strOrderBy)
            {
                strOrderBy = toolStripComboBoxOrderBy.Text;
                switch (strOrderBy)
                {
                    case "Installdate": listFilterCreatorScene = listFilterCreatorScene.OrderByDescending(q => q.Installdate).ToList();
                        break; 
                    case "VarName": listFilterCreatorScene = listFilterCreatorScene.OrderBy(q => q.Varname).ToList();
                        break;
                    case "SceneName": listFilterCreatorScene = listFilterCreatorScene.OrderBy(q => Path.GetFileNameWithoutExtension(q.Scenepath)).ToList();
                        break;
                }
            }
            int startpic = maxitemPerpage * toolStripComboBoxScenePage.SelectedIndex;
            imageListScenes.Images.Clear();
            listSceneItem.Clear();
            for (int i = 0; i < maxitemPerpage; i++)
            {
                int cur = startpic + i;
                if (cur >= listFilterCreatorScene.Count()) break;
                string scenePath = listFilterCreatorScene[cur].Scenepath;
                string sceneName = listFilterCreatorScene[cur].Varname + " - " + Path.GetFileNameWithoutExtension(scenePath);
                Image previwPic = Image.FromHbitmap(Properties.Resources.icoVarManager.ToBitmap().GetHbitmap());
                string picpath = GetPreviewPicPath(listFilterCreatorScene[cur].Atomtype, listFilterCreatorScene[cur].Varname, listFilterCreatorScene[cur].Picpath);
                if(!string.IsNullOrEmpty(picpath))
                      previwPic = Image.FromFile(picpath);
                
                imageListScenes.Images.Add(previwPic);
                ListViewItem item = new ListViewItem(sceneName, i);
                item.SubItems.Add(listFilterCreatorScene[cur].Varname);
                item.SubItems.Add(listFilterCreatorScene[cur].Scenepath);
                item.SubItems.Add(picpath);

               // item.SubItems.Add(listFilterCreatorScene[cur].Hidefav.ToString());
                listSceneItem.Add(item);
            }
            FillItems();
        }

        private void FillItems()
        {
            int hideIndex = 0;
            for (hideIndex = 0; hideIndex < listViewHide.Items.Count; hideIndex++)
            {
                var rect = listViewHide.GetItemRect(hideIndex);
                if (rect.Bottom + rect.Height >= listViewHide.Height)
                    break;
            }
            int nomalIndex = 0;
            for (nomalIndex = 0; nomalIndex < listViewNormal.Items.Count; nomalIndex++)
            {
                var rect = listViewNormal.GetItemRect(nomalIndex);
                if (rect.Bottom + rect.Height >= listViewNormal.Height)
                    break;
            }
            int favIndex = 0;
            for (favIndex = 0; favIndex < listViewFav.Items.Count; favIndex++)
            {
                var rect = listViewFav.GetItemRect(favIndex);
                if (rect.Bottom + rect.Height >= listViewFav.Height)
                    break;
            }
            while (backgroundWorkerFillListView.IsBusy)
            {
                backgroundWorkerFillListView.CancelAsync();
                // Keep UI messages moving, so the form remains 
                // responsive during the asynchronous operation.
                Application.DoEvents();
            }
            backgroundWorkerFillListView.RunWorkerAsync(new int[] { hideIndex, nomalIndex, favIndex });

        }

        private void backgroundWorkerFillListView_DoWork(object sender, DoWorkEventArgs e)
        {
            InvokeFillListView fillListView = new InvokeFillListView(FillListView);
            int[] index = (int[])e.Argument;
            this.BeginInvoke(fillListView, new Object[] { index[0], index[1], index[2] });

        }
        public delegate void InvokeFillListView(ref int hideIndex, ref int nomalIndex, ref int favIndex);
        public void FillListView(ref int hideIndex, ref int nomalIndex, ref int favIndex)
        {
            listViewHide.Items.Clear();
            listViewNormal.Items.Clear();
            listViewFav.Items.Clear();
            foreach (var item in listSceneItem)
            {
                Thread.Sleep(1);
                if (backgroundWorkerFillListView.CancellationPending)
                {
                    //Tell the Backgroundworker you are canceling and exit the for-loop
                    //e.Cancel = true;
                    return;
                }
                switch (GetHighFav(item.SubItems[1].Text, item.SubItems[2].Text))
                {
                    case -1: listViewHide.Items.Add(item); break;
                    case 0: listViewNormal.Items.Add(item); break;
                    case 1: listViewFav.Items.Add(item); break;
                }
            }
            if (hideIndex >= listViewHide.Items.Count)
                hideIndex = listViewHide.Items.Count - 1;
            if (hideIndex >= 0) listViewHide.EnsureVisible(hideIndex);
            if (nomalIndex >= listViewNormal.Items.Count)
                nomalIndex = listViewNormal.Items.Count - 1;
            if (nomalIndex >= 0) listViewNormal.EnsureVisible(nomalIndex);
            if (favIndex >= listViewFav.Items.Count)
                favIndex = listViewFav.Items.Count - 1;
            if (favIndex >= 0) listViewFav.EnsureVisible(favIndex);
        }

        private void toolStripButtonSceneFirst_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxScenePage.SelectedIndex > 0) toolStripComboBoxScenePage.SelectedIndex = 0;
        }

        private void toolStripButtonScenePrev_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxScenePage.SelectedIndex > 0) toolStripComboBoxScenePage.SelectedIndex--;
        }

        private void toolStripButtonSceneNext_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxScenePage.SelectedIndex < toolStripComboBoxScenePage.Items.Count - 1) toolStripComboBoxScenePage.SelectedIndex++;
        }

        private void toolStripButtonSceneLast_Click(object sender, EventArgs e)
        {
            if (toolStripComboBoxScenePage.SelectedIndex < toolStripComboBoxScenePage.Items.Count - 1) toolStripComboBoxScenePage.SelectedIndex= toolStripComboBoxScenePage.Items.Count - 1;
        }

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            panelImage.Visible = false;
        }


        private void buttonAddHide_Click(object sender, EventArgs e)
        {
            if (listViewNormal.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewNormal.SelectedItems)
                {
                    string scenepath = Path.GetDirectoryName(item.SubItems[2].Text);
                    string scenename = Path.GetFileName(item.SubItems[2].Text);
                    string hide = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".hide");
                    string fav = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".fav");

                    if (File.Exists(fav))
                        File.Delete(fav);
                    if (!File.Exists(hide))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(hide));
                        using (File.Create(hide)) { }
                    }
                    UpdateHidefav(item.SubItems[1].Text, item.SubItems[2].Text);
                }
                //UpdateFileHidefav();
                //FilterVars();
                FillItems();
            }
        }

        private void buttonAddFav_Click(object sender, EventArgs e)
        {
            if (listViewNormal.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewNormal.SelectedItems)
                {
                    string scenepath = Path.GetDirectoryName(item.SubItems[2].Text);
                    string scenename = Path.GetFileName(item.SubItems[2].Text);
                    string hide = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".hide");
                    string fav = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".fav");

                    if (File.Exists(hide))
                        File.Delete(hide);
                    if (!File.Exists(fav))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fav));
                        using (File.Create(fav)) { }
                    }
                    UpdateHidefav(item.SubItems[1].Text, item.SubItems[2].Text);
                }
                //UpdateFileHidefav();
                FillItems();
            }
        }

        private void buttonRemoveHide_Click(object sender, EventArgs e)
        {
            if (listViewHide.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewHide.SelectedItems)
                {
                    string scenepath = Path.GetDirectoryName(item.SubItems[2].Text);
                    string scenename = Path.GetFileName(item.SubItems[2].Text);
                    string hide = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".hide");
                    string fav = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".fav");

                    if (File.Exists(hide))
                        File.Delete(hide);
                    UpdateHidefav(item.SubItems[1].Text, item.SubItems[2].Text);
                }
                //UpdateFileHidefav();
                FillItems();
            }
        }

        private void buttonRemoveFav_Click(object sender, EventArgs e)
        {
            if (listViewFav.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewFav.SelectedItems)
                {
                    string scenepath = Path.GetDirectoryName(item.SubItems[2].Text);
                    string scenename = Path.GetFileName(item.SubItems[2].Text);
                    string hide = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".hide");
                    string fav = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", item.SubItems[1].Text, scenepath, scenename + ".fav");

                    if (File.Exists(fav))
                        File.Delete(fav);
                    UpdateHidefav(item.SubItems[1].Text, item.SubItems[2].Text);
                }
                //UpdateFileHidefav();
                FillItems();
            }
        }

        private void listViewHide_ItemActivate(object sender, EventArgs e)
        {
            if (listViewHide.SelectedIndices.Count == 1)
            {
                int index = listViewHide.SelectedIndices[0];
                var item = listViewHide.Items[index];
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.SubItems[3].Text))
                    {
                        labelPreviewVarName.Text = item.SubItems[1].Text;
                        pictureBoxPreview.Image = Image.FromFile(item.SubItems[3].Text);
                        panelImage.Visible = true;
                    }
                }
            }
        }

        private void listViewNormal_ItemActivate(object sender, EventArgs e)
        {
            if (listViewNormal.SelectedIndices.Count == 1)
            {
                int index = listViewNormal.SelectedIndices[0];
                var item = listViewNormal.Items[index];
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.SubItems[3].Text))
                    {
                        labelPreviewVarName.Text = item.SubItems[1].Text;
                        pictureBoxPreview.Image = Image.FromFile(item.SubItems[3].Text);
                        panelImage.Visible = true;
                    }
                }
            }
        }

        private void listViewFav_ItemActivate(object sender, EventArgs e)
        {
            if (listViewFav.SelectedIndices.Count == 1)
            {
                int index = listViewFav.SelectedIndices[0];
                var item = listViewFav.Items[index];
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.SubItems[3].Text))
                    {
                        labelPreviewVarName.Text = item.SubItems[1].Text;
                        pictureBoxPreview.Image = Image.FromFile(item.SubItems[3].Text);
                        panelImage.Visible = true;
                    }
                }
            }
        }

        private void toolStripComboBoxHideFav_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars(9);
        }

        private void toolStripComboBoxOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFilterCreatorScene != null)
            {
                if (listFilterCreatorScene.Count() > 0)
                    GenerateItems();
            }
        }

    }
}
