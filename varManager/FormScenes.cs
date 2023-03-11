using SimpleJSON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using varManager.Properties;
using static DragNDrop.DragAndDropListView;

namespace varManager
{
    public partial class FormScenes : Form
    {
        private static string previewpicsDirName = "___PreviewPics___";
        //private static string installLinkDirName = "___VarsLink___";
        private static string missingLinkDirName = "___MissingVarLink___";
        // private static int maxitemPerpage = 100;
        private string strCategory = "scenes";
        private string strOrderBy = "_";
        public FormScenes()
        {
            InitializeComponent();
        }
        public Form1 form1;
        /*
        public struct InstalledScene
        {
            public InstalledScene(string location, string atomtype, string varname, string destvarname, DateTime installdate, string scenepath, string picpath, int hidefav)
            {
                Location = location;
                Atomtype = atomtype;
                Varname = varname;
                Destvarname = destvarname;
                Installdate = installdate;
                Scenepath = scenepath;
                Picpath = picpath;
                Hidefav = hidefav;
            }
            public string Location { get; }
            public string Atomtype { get; }
            public string Varname { get; }
            public string Destvarname { get; }
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
        */
        //private List<varManagerDataSet.scenesViewRow> listInstalledScene = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterScene1 = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterScene2 = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterScene3 = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterCreatorScene = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterCreatorSceneHide = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterCreatorSceneNormal = new List<varManagerDataSet.scenesViewRow>();
        private List<varManagerDataSet.scenesViewRow> listFilterCreatorSceneFav = new List<varManagerDataSet.scenesViewRow>();

        //private List<varManagerDataSet.scenesViewRow> listFilterPageScene = new List<varManagerDataSet.scenesViewRow>();
        private void FormScenes_Load(object sender, EventArgs e)
        {
            panelImage.Dock = DockStyle.Fill;
            comboBoxOrderBy.SelectedIndex = 0;
            progressBar1.Visible = true;
            chklistLocation.SetItemChecked(0,true);
            chklistLocation.SetItemChecked(1, true);
            chklistLocation.SetItemChecked(3, true);
            checkedListBoxHideFav.SetItemChecked(0,true);
            checkedListBoxHideFav.SetItemChecked(1, true);
            checkedListBoxHideFav.SetItemChecked(2, true);

            //backgroundWorkerGenerate.RunWorkerAsync(); 
            AllInstalledVars();
            //UpdateFileHidefav();
            FilterVars();
        }
        public delegate void InvokeProgressBarSetValue(int value);
        public void ProgressBarSetValue(int value)
        {
            progressBar1.Value = value;
        }
        private void AllInstalledVars()
        {
            //DirectoryInfo dirinfoInstalled = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages", installLinkDirName));

            //IOrderedEnumerable<FileSystemInfo> fileinfos = dirinfoInstalled.GetFileSystemInfos("*.var").OrderBy(f => f.CreationTime);
            //progressBar1.Dock = DockStyle.Top;
            //progressBar1.Visible = true;
            int i = 0;
            this.varsTableAdapter.Fill(this.varManagerDataSet.vars);

            //this.varManagerDataSet.HideFav.Clear();
            Dictionary<string, int> dictHideFav = new Dictionary<string, int>();
            DirectoryInfo dirPrefs = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs"));
            foreach (var hidefav in dirPrefs.GetFiles("*.vap.fav", SearchOption.AllDirectories))
            {
                string fullname = hidefav.FullName;
                fullname = fullname.Substring(fullname.IndexOf("AddonPackagesFilePrefs\\") + 23);
                fullname = fullname.Substring(0, fullname.Length - 4);
                string varname = fullname.Substring(0, fullname.IndexOf("\\"));
                string scenepath = fullname.Substring(fullname.IndexOf("\\") + 1).Replace("\\", "/");
                //this.varManagerDataSet.HideFav.AddHideFavRow(varname, scenepath, 1);
                dictHideFav[varname + ":" + scenepath] = 1;
            }
            foreach (var hidefav in dirPrefs.GetFiles("*.json.fav", SearchOption.AllDirectories))
            {
                string fullname = hidefav.FullName;
                fullname = fullname.Substring(fullname.IndexOf("AddonPackagesFilePrefs\\") + 23);
                fullname = fullname.Substring(0, fullname.Length - 4);
                string varname = fullname.Substring(0, fullname.IndexOf("\\"));
                string scenepath = fullname.Substring(fullname.IndexOf("\\") + 1).Replace("\\", "/");
                //this.varManagerDataSet.HideFav.AddHideFavRow(varname, scenepath, 1);
                dictHideFav[varname + ":" + scenepath] = 1;
            }
            foreach (var hidefav in dirPrefs.GetFiles("*.vap.hide", SearchOption.AllDirectories))
            {
                string fullname = hidefav.FullName;
                fullname = fullname.Substring(fullname.IndexOf("AddonPackagesFilePrefs\\") + 23);
                fullname = fullname.Substring(0, fullname.Length - 5);
                string varname = fullname.Substring(0, fullname.IndexOf("\\"));
                string scenepath = fullname.Substring(fullname.IndexOf("\\") + 1).Replace("\\", "/");
                //this.varManagerDataSet.HideFav.AddHideFavRow(varname, scenepath, -1);
                dictHideFav[varname + ":" + scenepath] = -1;
            }
            foreach (var hidefav in dirPrefs.GetFiles("*.json.hide", SearchOption.AllDirectories))
            {
                string fullname = hidefav.FullName;
                fullname = fullname.Substring(fullname.IndexOf("AddonPackagesFilePrefs\\") + 23);
                fullname = fullname.Substring(0, fullname.Length - 5);
                string varname = fullname.Substring(0, fullname.IndexOf("\\"));
                string scenepath = fullname.Substring(fullname.IndexOf("\\") + 1).Replace("\\", "/");
                //this.varManagerDataSet.HideFav.AddHideFavRow(varname, scenepath, -1);
                dictHideFav[varname + ":" + scenepath] = -1;
            }
            //this.varManagerDataSet.HideFav.AcceptChanges();

            this.installStatusTableAdapter.Fill(this.varManagerDataSet.installStatus);

            this.scenesTableAdapter.FillByLoadable(this.varManagerDataSet.scenes);
            //InvokeProgressBarSetValue progressBarSetValue = new InvokeProgressBarSetValue(ProgressBarSetValue);
            //this.BeginInvoke(progressBarSetValue, 5 );
            progressBar1.Value = 5;
            foreach (var varscene in this.varManagerDataSet.scenes)
            {
                int hidefav = 0;
                if (dictHideFav.ContainsKey(varscene.varName + ":" + varscene.scenePath))
                    hidefav = dictHideFav[varscene.varName + ":" + varscene.scenePath];
                //var hidefavrows = this.varManagerDataSet.HideFav.Where(q => q.varName == varscene.varName && q.scenePath == varscene.scenePath);
                // if (hidefavrows.Count() > 0) 
                //     hidefav= hidefavrows.First().hidefav;
                varManagerDataSet.varsRow varrow = this.varManagerDataSet.vars.FindByvarName(varscene.varName);

                bool installed = false;
                var installedrow = this.varManagerDataSet.installStatus.FindByvarName(varscene.varName);

                if (installedrow != null && installedrow.Installed)
                    installed = true;
                this.varManagerDataSet.scenesView.AddscenesViewRow(installed ? "installed" : "not Installed", varscene.varName, varscene.atomType, varscene.isPreset,
                    varscene.scenePath, varscene.previewPic, installed, varrow.creatorName, varrow.packageName, varrow.varPath, varrow.filesize,
                    varrow.metaDate, varrow.varDate, hidefav);
                //listInstalledScene.Add(new InstalledScene("VarsLink", varscene.atomType, varscene.varName, varscene.varName, varscene.metaDate, varscene.scenePath, varscene.previewPic, 0));
                progressBar1.Value = 5 + (i * 55 / this.varManagerDataSet.scenes.Rows.Count);
                //this.BeginInvoke(progressBarSetValue, 5 + (i * 55 / this.varManagerDataSet.scenes.Rows.Count));

                i++;
            }
            this.varManagerDataSet.scenesView.AcceptChanges();
            DirectoryInfo dirinfoMissinglink = new DirectoryInfo(Path.Combine(Settings.Default.vampath, "AddonPackages", missingLinkDirName));
            if (dirinfoMissinglink.Exists)
            {
                i = 0;
                IOrderedEnumerable<FileSystemInfo> misslinks = dirinfoMissinglink.GetFileSystemInfos("*.var").OrderBy(f => f.CreationTime);
                foreach (FileSystemInfo fileinfo in misslinks)
                {
                    string varName = Path.GetFileNameWithoutExtension(fileinfo.Name);
                    string destfilename = varName;
                    if (fileinfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        destfilename = Comm.ReparsePoint(fileinfo.FullName);
                        destfilename = Path.GetFileNameWithoutExtension(destfilename);
                    }

                    foreach (varManagerDataSet.scenesRow varscene in this.scenesTableAdapter.GetDataByVarName(destfilename))
                    {
                        int hidefav = 0;
                        if (dictHideFav.ContainsKey(varName + ":" + varscene.scenePath))
                            hidefav = dictHideFav[varName + ":" + varscene.scenePath];

                        //var hidefavrows = this.varManagerDataSet.HideFav.Where(q => q.varName == varName && q.scenePath == varscene.scenePath);
                        //if (hidefavrows.Count() > 0)
                        //    hidefav = hidefavrows.First().hidefav;
                        varManagerDataSet.varsRow varrow = this.varManagerDataSet.vars.FindByvarName(varscene.varName);

                        this.varManagerDataSet.scenesView.AddscenesViewRow("missingLink", varscene.varName, varscene.atomType, varscene.isPreset,
                            varscene.scenePath, varscene.previewPic, true, varrow.creatorName, varrow.packageName, varrow.varPath, varrow.filesize,
                            varrow.metaDate, varrow.varDate, hidefav);
                        //listInstalledScene.Add(new InstalledScene("VarsLink", varscene.atomType, varscene.varName, varscene.varName, varscene.metaDate, varscene.scenePath, varscene.previewPic, 0));

                    }
                    progressBar1.Value = 60 + (i * 20 / misslinks.Count());
                    //this.BeginInvoke(progressBarSetValue, 60 + (i * 20 / misslinks.Count()));
                    i++;
                }
            }
            this.varManagerDataSet.scenesView.AcceptChanges();

            List<SaveScene> savescenes=new List<SaveScene>();
            foreach(SaveScene ss in SaveScenes("scenes", "Saves\\scene", "*.json"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("looks", "Saves\\Person\\full", "*.json"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("looks", "Saves\\Person\\appearance", "*.json"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("looks", "Custom\\Atom\\Person\\Appearance", "*.vap"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("pose", "Saves\\Person\\pose", "*.json"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("pose", "Custom\\Atom\\Person\\Pose", "*.vap"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("clothing", "Custom\\Atom\\Person\\Clothing", "*.vap"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("clothing", "Custom\\Clothing", "*.vap"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("hairstyle", "Custom\\Atom\\Person\\Hair", "*.vap"))
            {
                savescenes.Add(ss);
            }
            foreach (SaveScene ss in SaveScenes("hairstyle", "Custom\\Hair", "*.vap"))
            {
                savescenes.Add(ss);
            } 
            foreach (SaveScene ss in SaveScenes("morphs", "Custom\\Atom\\Person\\Morphs", "*.vap"))
            {
                savescenes.Add(ss);
            } 
            foreach (SaveScene ss in SaveScenes("skin", "Custom\\Atom\\Person\\Skin", "*.vap"))
            {
                savescenes.Add(ss);
            }
            i = 0;
            foreach (var savescene in savescenes)
            {
                this.varManagerDataSet.scenesView.AddscenesViewRow("Save", "(save).", savescene.strType, true,
                           savescene.strPath, savescene.strPreviewPic, true, "(save)", "_", "_", savescene.filesize,
                           savescene.fdate, savescene.fdate, savescene.hidefav);
                progressBar1.Value = 80 + (i * 20 / savescenes.Count());
                //this.BeginInvoke(progressBarSetValue, 80 + (i * 20 / savescenes.Count()));
                i++;
            }
            this.varManagerDataSet.scenesView.AcceptChanges();
            progressBar1.Visible = false;
        }

        private static List<SaveScene> SaveScenes(string strType,string strPath,string searchPattern)
        {
            List<SaveScene> savescenes = new List<SaveScene>();
            DirectoryInfo dirinfoscenes = new DirectoryInfo(Path.Combine(Settings.Default.vampath, strPath));
            if (dirinfoscenes.Exists)
            {
                IOrderedEnumerable<FileInfo> scenes = dirinfoscenes.GetFiles(searchPattern).OrderBy(f => f.CreationTime);
                foreach (FileInfo fileinfo in scenes)
                {
                    string path = strPath + fileinfo.FullName.Replace(dirinfoscenes.FullName, "");

                    string strPreviewPic = fileinfo.FullName.Substring(0, fileinfo.FullName.LastIndexOf('.')) + ".jpg";
                    if (!File.Exists(strPreviewPic))
                        strPreviewPic = "";
                    int hidefav = 0;
                    if (File.Exists(fileinfo.FullName + ".hide"))
                        hidefav = -1;
                    if (File.Exists(fileinfo.FullName + ".fav"))
                        hidefav = 1;
                    var saveScene = new SaveScene();
                    saveScene.strType = strType;
                    saveScene.strName = Path.GetFileNameWithoutExtension(path);
                    saveScene.strPath = path;
                    saveScene.filesize = fileinfo.Length;
                    saveScene.fdate = fileinfo.LastWriteTime;
                    saveScene.strPreviewPic = strPreviewPic;
                    saveScene.hidefav = hidefav;
                    savescenes.Add(saveScene);
                }
            }

            return savescenes;
        }

        struct SaveScene
        {
            public string strType;
            public string strName;
            public string strPath;
            public string strPreviewPic;
            public long filesize;
            public DateTime fdate;
            public int hidefav;
        }
        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
        }
        /*
        private void UpdateFileHidefav()
        {
            for (int i = 0; i < listInstalledScene.Count; i++)
            {
                var InstalledScene = listInstalledScene[i];
                InstalledScene.Hidefav = GetHideFav(listInstalledScene[i].Varname, listInstalledScene[i].Scenepath);
                listInstalledScene[i] = InstalledScene;
            }
            //strOrderBy = "";
            // FilterVars();
        }
        */

        private void FilterVars(int filterAt = 10)
        {
            if (filterAt >= 10)
            {
                if (!string.IsNullOrEmpty(strCategory))
                    listFilterScene1 = this.varManagerDataSet.scenesView.Where(q => q.atomType == strCategory).ToList();
                
                List<string> locations = new List<string>();
                foreach (var locationitem in chklistLocation.CheckedItems)
                {
                    locations.Add(locationitem.ToString());
                }
                listFilterScene1 = listFilterScene1.Where(q => locations.IndexOf(q.location) >= 0).ToList();
            }
            if (filterAt >= 9)
            {
                List<int> hideFavs = new List<int>();
                if (checkedListBoxHideFav.GetItemChecked(0)) hideFavs.Add(-1);
                if (checkedListBoxHideFav.GetItemChecked(1)) hideFavs.Add(0);
                if (checkedListBoxHideFav.GetItemChecked(2)) hideFavs.Add(1);
                listFilterScene2 = listFilterScene1.Where(q => hideFavs.IndexOf(q.hidefav) >= 0).ToList();

            }
            if (filterAt >= 8)
            {
                if (!string.IsNullOrWhiteSpace(textBoxFilter.Text))
                {
                    string filtertext = textBoxFilter.Text.Trim().ToLower();
                    listFilterScene3 = listFilterScene2.Where(q => q.varName.ToLower().IndexOf(filtertext) >= 0 ||
                                         Path.GetFileNameWithoutExtension(q.scenePath).ToLower().IndexOf(filtertext) >= 0).ToList();
                }
                else
                    listFilterScene3 = listFilterScene2;
            }
            comboBoxCreator.SelectedIndexChanged -= new System.EventHandler(comboBoxCreator_SelectedIndexChanged);
            string creatorname = comboBoxCreator.Text;
            var Creators = listFilterScene3.Select(q => q.creatorName).Distinct().OrderBy(o => o).ToArray();
            comboBoxCreator.Items.Clear();
            comboBoxCreator.Items.Add("____ALL");
            comboBoxCreator.Items.AddRange(Creators);
            if (!string.IsNullOrEmpty(creatorname) && creatorname != "____ALL")
            {
                if (comboBoxCreator.Items.Contains(creatorname))
                {
                    comboBoxCreator.SelectedItem = creatorname;
                    listFilterCreatorScene = listFilterScene3.Where(q => q.varName.StartsWith(creatorname + ".")).ToList();
                }
            }
            else
            {
                comboBoxCreator.SelectedIndex = 0;
                listFilterCreatorScene = listFilterScene3.ToList();
            }
            comboBoxCreator.SelectedIndexChanged += new System.EventHandler(comboBoxCreator_SelectedIndexChanged);
            FilterVarsCreator();
        }

        private void FilterVarsCreator()
        {
            string creatorname = comboBoxCreator.Text;

            if (!string.IsNullOrEmpty(creatorname) && creatorname != "____ALL")
            {
                listFilterCreatorScene = listFilterScene3.Where(q => q.varName.StartsWith(creatorname+".")).ToList();
            }
            else
            {
                listFilterCreatorScene = listFilterScene3.ToList();
            }
            strOrderBy = "none";
            /*
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
            */
            GenerateItems();
        }

        private void toolStripComboBoxScenePage_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateItems();
        }

        private List<ListViewItem> listSceneItem = new List<ListViewItem>();

        private string GetPreviewPicPath(string atomType, string varName, string picName)
        {

            string fullpicname = Path.Combine(Settings.Default.varspath, previewpicsDirName, atomType, varName, picName);
            if (File.Exists(fullpicname))
                return fullpicname;
            else
                return "";
        }
        private int GetHideFav(string varName, string scenepath)
        {
            string pathhide = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", varName, scenepath + ".hide");
            string pathfav = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", varName, scenepath + ".fav");

            if (string.IsNullOrEmpty(varName))
            {
                pathhide = Path.Combine(Settings.Default.vampath, scenepath + ".hide");
                pathfav = Path.Combine(Settings.Default.vampath, scenepath + ".fav");

            }
            if (File.Exists(pathhide))
            {
                return -1;
            }
            if (File.Exists(pathfav))
            {
                return 1;
            }
            return 0;
        }

        private void UpdateHidefav(string varName, string scenepath)
        {
            this.varManagerDataSet.scenesView.Where(q => q.varName == varName && q.scenePath == scenepath).First().hidefav = GetHideFav(varName, scenepath);
            this.varManagerDataSet.scenesView.AcceptChanges();
        }
        private void GenerateItems()
        {
            panelImage.Visible = false;
            if (comboBoxOrderBy.Text != strOrderBy)
            {
                strOrderBy = comboBoxOrderBy.Text;
                switch (strOrderBy)
                {
                    case "New To Old":
                        listFilterCreatorScene = listFilterCreatorScene.OrderByDescending(q => q.varDate).ToList();
                        break;
                    case "Old To New":
                        listFilterCreatorScene = listFilterCreatorScene.OrderBy(q => q.varDate).ToList();
                        break;
                    case "VarName":
                        listFilterCreatorScene = listFilterCreatorScene.OrderBy(q => q.varName).ToList();
                        break;
                    case "SceneName":
                        listFilterCreatorScene = listFilterCreatorScene.OrderBy(q => Path.GetFileNameWithoutExtension(q.scenePath)).ToList();
                        break;
                }
            }
            listFilterCreatorSceneHide = listFilterCreatorScene.Where(q => q.hidefav == -1).ToList();
            listViewHide.VirtualListSize = listFilterCreatorSceneHide.Count;
            listFilterCreatorSceneNormal = listFilterCreatorScene.Where(q => q.hidefav == 0).ToList();
            listViewNormal.VirtualListSize = listFilterCreatorSceneNormal.Count;
            listFilterCreatorSceneFav = listFilterCreatorScene.Where(q => q.hidefav == 1).ToList();
            listViewFav.VirtualListSize = listFilterCreatorSceneFav.Count;
            labelHide.Text = listFilterCreatorSceneHide.Count.ToString();
            labelNormal.Text = listFilterCreatorSceneNormal.Count.ToString();
            labelFav.Text = listFilterCreatorSceneFav.Count.ToString();
            listViewHide.Invalidate();
            listViewNormal.Invalidate();
            listViewFav.Invalidate();
            /*
            int startpic = maxitemPerpage * toolStripComboBoxScenePage.SelectedIndex;
            imageListScenes.Images.Clear();
            listSceneItem.Clear();
            for (int i = 0; i < maxitemPerpage; i++)
            {
                int cur = startpic + i;
                if (cur >= listFilterCreatorScene.Count()) break;
                string scenePath = listFilterCreatorScene[cur].scenePath;
                string sceneName = listFilterCreatorScene[cur].varName+ " - " + Path.GetFileNameWithoutExtension(scenePath);
                //Image previwPic = Image.FromHbitmap(Properties.Resources.icoVarManager.ToBitmap().GetHbitmap());
                //Image previwPic = Image.FromFile("vam.png");
                string picpath = GetPreviewPicPath(listFilterCreatorScene[cur].atomType, listFilterCreatorScene[cur].varName, listFilterCreatorScene[cur].previewPic); 
                if (string.IsNullOrEmpty(picpath))
                {
                     picpath = "vam.png";
                    //previwPic = Image.FromFile(picpath);
                }
                imageListScenes.Images.Add(Image.FromFile(picpath));
                ListViewItem item = new ListViewItem(sceneName, i);
                item.SubItems.Add(listFilterCreatorScene[cur].varName);
                item.SubItems.Add(listFilterCreatorScene[cur].scenePath);
                item.SubItems.Add(picpath);
                item.SubItems.Add(listFilterCreatorScene[cur].varDate.ToString());
                item.SubItems.Add(listFilterCreatorScene[cur].hidefav.ToString());
                item.SubItems.Add(listFilterCreatorScene[cur].atomType.ToString());
                item.SubItems.Add(listFilterCreatorScene[cur].location.ToString());
                listSceneItem.Add(item);
            }
            FillItems();
            */
        }
        /*
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
            int countHide = 0, countNormal = 0, countFav = 0;
            
           // List<string> locations = new List<string>();
          //  foreach (var locationitem in chklistLocation.CheckedItems)
          //  {
          //      locations.Add(locationitem.ToString());
          //  }
            List<varManagerDataSet.scenesViewRow> sceneviewarray =this.varManagerDataSet.scenesView.Where(q => locations.IndexOf(q.location)>=0).ToList();
            
            foreach (var item in listSceneItem)
            {
                Thread.Sleep(1);
                
                switch (int.Parse(item.SubItems[5].Text))
                {
                    case -1: listViewHide.Items.Add(item); countHide++; break;
                    case 0: listViewNormal.Items.Add(item); countNormal++; break;
                    case 1: listViewFav.Items.Add(item); countFav++; break;
                }
            }
            labelHide.Text = countHide.ToString();
            labelNormal.Text = countNormal.ToString();
            labelFav.Text = countFav.ToString();
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
             */

        private void pictureBoxPreview_Click(object sender, EventArgs e)
        {
            panelImage.Visible = false;
        }


        private void buttonAddHide_Click(object sender, EventArgs e)
        {
            if (listViewNormal.SelectedIndices.Count > 0)
            {
                foreach (int itemindex in listViewNormal.SelectedIndices)
                {
                    string varName = listFilterCreatorSceneNormal[itemindex].varName;
                    string scene = listFilterCreatorSceneNormal[itemindex].scenePath;
                    SetHideFav(varName, scene,-1);
                    UpdateHidefav(varName, scene);
                }
                //UpdateFileHidefav();
                //FilterVars();
                GenerateItems();
            }
        }

        private static void SetHideFav(string varName, string scene,int hidefav)
        {
            string scenepath = Path.GetDirectoryName(scene);
            string scenename = Path.GetFileName(scene);
            string pathhide = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", varName, scenepath, scenename + ".hide");
            string pathfav = Path.Combine(Settings.Default.vampath, "AddonPackagesFilePrefs", varName, scenepath, scenename + ".fav");
            if (string.IsNullOrEmpty(varName))
            {
                pathhide = Path.Combine(Settings.Default.vampath, scenepath, scenename + ".hide");
                pathfav = Path.Combine(Settings.Default.vampath, scenepath, scenename + ".fav");

            }
            switch (hidefav)
            {
                case -1:
                    if (File.Exists(pathfav))
                        File.Delete(pathfav);
                    if (!File.Exists(pathhide))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pathhide));
                        using (File.Create(pathhide)) { }
                    }
                    break;
                case 0:
                    if (File.Exists(pathfav))
                        File.Delete(pathfav);
                    if (File.Exists(pathhide))
                        File.Delete(pathhide);
                    break;
                case 1:
                    if (File.Exists(pathhide))
                        File.Delete(pathhide);
                    if (!File.Exists(pathfav))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pathfav));
                        using (File.Create(pathfav)) { }
                    }
                    break;
            }

        }

        private void buttonAddFav_Click(object sender, EventArgs e)
        {
            if (listViewNormal.SelectedIndices.Count > 0)
            {
                foreach (int itemindex in listViewNormal.SelectedIndices)
                {
                    string varName = listFilterCreatorSceneNormal[itemindex].varName;
                    string scene = listFilterCreatorSceneNormal[itemindex].scenePath;
                    SetHideFav(varName, scene, 1);
                    UpdateHidefav(varName, scene);
                }
                //UpdateFileHidefav();
                GenerateItems();
            }
        }

        private void buttonRemoveHide_Click(object sender, EventArgs e)
        {
            if (listViewHide.SelectedIndices.Count > 0)
            {
                foreach (int itemindex in listViewHide.SelectedIndices)
                {
                    string varName = listFilterCreatorSceneHide[itemindex].varName;
                    string scene = listFilterCreatorSceneHide[itemindex].scenePath;
                    SetHideFav(varName, scene, 0);
                    UpdateHidefav(varName, scene);
                }
                //UpdateFileHidefav();
                GenerateItems();
            }
        }

        private void buttonRemoveFav_Click(object sender, EventArgs e)
        {
            if (listViewFav.SelectedIndices.Count > 0)
            {
                foreach (int itemindex in listViewFav.SelectedIndices)
                {
                    string varName = listFilterCreatorSceneFav[itemindex].varName;
                    string scene = listFilterCreatorSceneFav[itemindex].scenePath;
                    SetHideFav(varName, scene, 0);
                    UpdateHidefav(varName, scene);
                }
                //UpdateFileHidefav();
                GenerateItems();
            }
        }
        private string curVarName = "", curEntryName = "";
        private JSONClass jsonLoadScene;

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            ListView listView=(ListView)sender;
            if (listView.SelectedIndices.Count == 1)
            {
                panelImage.Parent = listView;
                int index = listView.SelectedIndices[0];
                var item = listView.Items[index];
                if (item != null)
                {
                    curVarName = item.SubItems[1].Text;
                    curEntryName = item.SubItems[2].Text;
                    string varname = "";
                    if (!string.IsNullOrEmpty(curVarName) && curVarName != "(save).")
                    {
                        varname = item.SubItems[1].Text + ":/";
                    }
                    jsonLoadScene = new JSONClass();
                    jsonLoadScene.Add("rescan", item.SubItems[7].Text == "not Installed" ? "true" : "false");

                    jsonLoadScene.Add("resources", new JSONArray());
                    JSONArray resources = jsonLoadScene["resources"].AsArray;
                    resources.Add(new JSONClass());
                    JSONClass resource = (JSONClass)resources[resources.Count - 1];
                    resource.Add("type", item.SubItems[6].Text);
                    resource.Add("saveName", varname + curEntryName.Replace('\\', '/'));
                    UpdateButtonClearCache();
                    if (!string.IsNullOrEmpty(item.SubItems[3].Text))
                    {
                        labelPreviewVarName.Text = item.Text;
                        checkBoxMerge.Checked = false;
                        buttonLoadscene.Text = "Load " + item.SubItems[6].Text;
                        pictureBoxPreview.Image = Image.FromFile(item.SubItems[3].Text);

                        panelImage.Visible = true;
                        radioButtonPersonOrder1.Checked = true;
                        if (item.SubItems[6].Text.ToLower() == "scenes" || item.SubItems[6].Text.ToLower() == "looks")
                        {
                            buttonAnalysis.Visible = true;
                        }
                        else
                        {
                            buttonAnalysis.Visible = false;
                        }
                        if (item.SubItems[6].Text.ToLower() == "looks" || item.SubItems[6].Text.ToLower() == "clothing" ||
                            item.SubItems[6].Text.ToLower() == "morphs" || item.SubItems[6].Text.ToLower() == "hairstyle" ||
                            item.SubItems[6].Text.ToLower() == "skin" || item.SubItems[6].Text.ToLower() == "pose")
                        {
                            groupBoxPersonOrder.Visible = true;
                            checkBoxIgnoreGender.Visible = true;
                        }
                        else
                        {
                            groupBoxPersonOrder.Visible = false;
                            checkBoxIgnoreGender.Visible = false;
                        }
                        if (item.SubItems[6].Text.ToLower() == "morphs" ||
                           item.SubItems[6].Text.ToLower() == "skin" ||
                           item.SubItems[6].Text.ToLower() == "pose")
                        {
                            checkBoxForMale.Visible = true;
                        }
                        else
                        {
                            checkBoxForMale.Visible = false;
                        }
                    }
                }
            }
        }

        private void UpdateButtonClearCache()
        {
            string sceneCacheFolderName = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                                       Comm.ValidFileName(curVarName == "(save)." ? "save" : curVarName), Comm.ValidFileName(curEntryName.Replace('\\', '_').Replace('/', '_')));
            if (Directory.Exists(sceneCacheFolderName))
            {
                buttonClearCache.Visible = true;
            }
            else
            {
                buttonClearCache.Visible = false;
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

        private void listViewNormal_ListViewDragDrop(object sender, DragEventArgs e)
        {
            DragItemData data = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
            List<varManagerDataSet.scenesViewRow> listfilter = listFilterCreatorSceneNormal;
            if (data.ListView == listViewHide)
            {
                listfilter = listFilterCreatorSceneHide;
            }
            if (data.ListView == listViewFav)
            {
                listfilter = listFilterCreatorSceneFav;
            }
            foreach (int itemindex in data.ListView.SelectedIndices)
            {
                string varName = listfilter[itemindex].varName;
                string scene = listfilter[itemindex].scenePath;
                SetHideFav(varName, scene, 0);
                UpdateHidefav(varName, scene);
            }
            GenerateItems();
        }

        private void listViewHide_ListViewDragDrop(object sender, DragEventArgs e)
        {
            DragItemData data = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
            List<varManagerDataSet.scenesViewRow> listfilter = listFilterCreatorSceneNormal;
            if (data.ListView == listViewHide)
            {
                listfilter = listFilterCreatorSceneHide;
            }
            if (data.ListView == listViewFav)
            {
                listfilter = listFilterCreatorSceneFav;
            }
            foreach (int itemindex in data.ListView.SelectedIndices)
            {
                string varName = listfilter[itemindex].varName;
                string scene = listfilter[itemindex].scenePath;
                SetHideFav(varName, scene, -1);
                UpdateHidefav(varName, scene);
            }
            GenerateItems();
        }

        private void listViewFav_ListViewDragDrop(object sender, DragEventArgs e)
        {
            DragItemData data = (DragItemData)e.Data.GetData(typeof(DragItemData).ToString());
            List<varManagerDataSet.scenesViewRow> listfilter = listFilterCreatorSceneNormal;
            if (data.ListView == listViewHide)
            {
                listfilter = listFilterCreatorSceneHide;
            }
            if (data.ListView == listViewFav)
            {
                listfilter = listFilterCreatorSceneFav;
            }
            foreach (int itemindex in data.ListView.SelectedIndices)
            {
                string varName = listfilter[itemindex].varName;
                string scene = listfilter[itemindex].scenePath;
                SetHideFav(varName, scene, 1);
                UpdateHidefav(varName, scene);
            }
            GenerateItems();
        }

        private void buttonLoadscene_Click(object sender, EventArgs e)
        {
            bool merge = false;
            if (checkBoxMerge.Checked) merge = true;
            bool ignoreGender = false;
            if(checkBoxIgnoreGender.Checked) ignoreGender = true;
            string characterGender = "unknown";
            if (checkBoxForMale.Visible)
            {
                if (checkBoxForMale.Checked) characterGender = "male";
                else characterGender = "female";
            }
            int personOrder = 1;
            foreach (RadioButton rbperson in groupBoxPersonOrder.Controls)
            {
                if (rbperson.Checked)
                {
                    personOrder = int.Parse(rbperson.Text);
                    break;
                }
            }
            panelImage.Visible = false;
            Cursor = Cursors.WaitCursor;
            
            JSONArray resources = jsonLoadScene["resources"].AsArray;
            string saveName = "";

            if (resources.Count > 0)
            {
                JSONClass resource = (JSONClass)resources[0];
                saveName = resource["saveName"].Value;
            }
            
            form1.LoadScene(jsonLoadScene, merge, ignoreGender, characterGender, personOrder);
            Cursor = Cursors.Arrow;
            UpdateButtonClearCache();
        }

        private void chklistLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars();
            //FillItems();
        }

        private void backgroundWorkerGenerate_DoWork(object sender, DoWorkEventArgs e)
        {
            AllInstalledVars();
        }

        private void backgroundWorkerGenerate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //progressBar1.Visible = false;
        }

        private void checkedListBoxHideFav_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVars(9);
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            FilterVars(8);
        }

        private void comboBoxCreator_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterVarsCreator();
        }

        private void buttonLocate_Click(object sender, EventArgs e)
        {
            
            JSONArray resources = jsonLoadScene["resources"].AsArray;
            JSONClass resource = (JSONClass)resources[resources.Count - 1];
            string varName = resource["saveName"];
            
            if (varName.IndexOf(":/") > 0)
            {
                varName = varName.Substring(0, varName.IndexOf(":/"));
                form1.LocateVar(varName);
                form1.SelectVarInList(varName);
                form1.Activate();
            }
            else
            {
                varName = Path.Combine(Settings.Default.vampath, varName.Replace('/','\\'));
                Comm.LocateFile(varName);
            }
        }

        int layoutPanelWidthMode = 0;
        private void buttonFav_Click(object sender, EventArgs e)
        {
            if (layoutPanelWidthMode == 3)
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 33;
                tableLayoutPanel1.ColumnStyles[2].Width = 33;
                tableLayoutPanel1.ColumnStyles[4].Width = 33;
                layoutPanelWidthMode = 0;
                buttonFav.Text = "◀Fav▶";
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 10;
                tableLayoutPanel1.ColumnStyles[2].Width = 10;
                tableLayoutPanel1.ColumnStyles[4].Width = 80;
                layoutPanelWidthMode = 3;
                buttonFav.Text = "▶Fav◀";
            }
        }

        private void buttonNormal_Click(object sender, EventArgs e)
        {
            if (layoutPanelWidthMode == 2)
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 33;
                tableLayoutPanel1.ColumnStyles[2].Width = 33;
                tableLayoutPanel1.ColumnStyles[4].Width = 33;
                layoutPanelWidthMode = 0;
                buttonNormal.Text = "◀Normal▶";
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 10;
                tableLayoutPanel1.ColumnStyles[2].Width = 80;
                tableLayoutPanel1.ColumnStyles[4].Width = 10;
                layoutPanelWidthMode = 2;
                buttonNormal.Text = "▶Normal◀";
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            if (layoutPanelWidthMode == 1)
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 33;
                tableLayoutPanel1.ColumnStyles[2].Width = 33;
                tableLayoutPanel1.ColumnStyles[4].Width = 33;
                layoutPanelWidthMode = 0;
                buttonHide.Text = "◀Hide▶";
            }
            else
            {
                tableLayoutPanel1.ColumnStyles[0].Width = 80;
                tableLayoutPanel1.ColumnStyles[2].Width = 10;
                tableLayoutPanel1.ColumnStyles[4].Width = 10;
                layoutPanelWidthMode = 1;
                buttonHide.Text = "▶Hide◀";
            }
        }

        private void listViewHide_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var curpriviewscene = listFilterCreatorSceneHide[e.ItemIndex];
            RetrieveVirtualItem(e, curpriviewscene);
        }


        private void listViewNormal_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {

            var curpriviewscene = listFilterCreatorSceneNormal[e.ItemIndex]; 
            RetrieveVirtualItem(e, curpriviewscene);
        }

        private void listViewFav_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            var curpriviewscene = listFilterCreatorSceneFav[e.ItemIndex];
            RetrieveVirtualItem(e, curpriviewscene);
        }

        private void RetrieveVirtualItem(RetrieveVirtualItemEventArgs e, varManagerDataSet.scenesViewRow curpriviewscene)
        {
            string key = "vam.png";
            if (!string.IsNullOrWhiteSpace(curpriviewscene.previewPic))
            {
                string picpath = Path.Combine(Settings.Default.varspath, previewpicsDirName, curpriviewscene.atomType, curpriviewscene.varName, curpriviewscene.previewPic);
                if (File.Exists(picpath)) key = picpath;
            }
            if (!imageListScenes.Images.ContainsKey(key))
            {
                imageListScenes.Images.Add(key, Image.FromFile(key));
                if (imageListScenes.Images.Count > 40) imageListScenes.Images.RemoveAt(0);
            }


            string scenePath = curpriviewscene.scenePath;
            string sceneName = curpriviewscene.varName + " - " + Path.GetFileNameWithoutExtension(scenePath);

            e.Item = new ListViewItem(sceneName, imageListScenes.Images.IndexOfKey(key));
            e.Item.SubItems.Add(curpriviewscene.varName);
            e.Item.SubItems.Add(curpriviewscene.scenePath);
            e.Item.SubItems.Add(key);
            e.Item.SubItems.Add(curpriviewscene.varDate.ToString());
            e.Item.SubItems.Add(curpriviewscene.hidefav.ToString());
            e.Item.SubItems.Add(curpriviewscene.atomType.ToString());
            e.Item.SubItems.Add(curpriviewscene.location.ToString());
        }


        private void comboBoxOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFilterCreatorScene != null&& listFilterCreatorScene.Count() > 0)
            {
                GenerateItems();
            }
        }

        private void radioButtonCategory_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb == null)
            {
                //MessageBox.Show("Sender is not a RadioButton");
                return;
            }
            if (strCategory != rb.Text)
            {
                strCategory = rb.Text;
                FilterVars();
            }
        }

        private void buttonAnalysis_Click(object sender, EventArgs e)
        {
            form1.Analysisscene(jsonLoadScene);
            UpdateButtonClearCache();
        }

        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            textBoxFilter.Text = "";
            comboBoxCreator.SelectedItem = "____ALL";
            comboBoxOrderBy.SelectedItem = "New To Old";
        }

        private void buttonClearCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("The cache can improve the speed of secondary analysis, normally you don't need to clear it, unless you modify the scene file. This operation only clears the cache of the current scene, if you need to clear all the cache, please delete the cache directory manually.", "Clear Cache", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string sceneFoldername = Path.Combine(Directory.GetCurrentDirectory(), "Cache",
                          Comm.ValidFileName(curVarName == "(save)." ? "save" : curVarName), Comm.ValidFileName(curEntryName.Replace('\\', '_').Replace('/', '_')));
                try { Directory.Delete(sceneFoldername, true); }
                catch { }
                UpdateButtonClearCache();
            }
        }

        private void buttonFilterByCreator_Click(object sender, EventArgs e)
        {
            JSONArray resources = jsonLoadScene["resources"].AsArray;
            JSONClass resource = (JSONClass)resources[resources.Count - 1];
            string varName = resource["saveName"];
            if (varName.IndexOf(":/") > 0)
            {
                varName = varName.Substring(0, varName.IndexOf(":/"));
                string[] varnamepart = varName.Split('.');

                if (varnamepart.Length == 3)
                {
                    comboBoxCreator.SelectedItem = varnamepart[0];
                    textBoxFilter.Text = "";
                    panelImage.Visible = false;
                }
            }
        }
    }
}
