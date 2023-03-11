using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SimpleLogger;

namespace varManager
{
    public partial class FormHub : Form
    {
        private static SimpleLogger simpLog = new SimpleLogger();
        public Form1 form1;
        //private varManagerDataSet varManagerDataSet;
        private static HttpClient httpClient;
        private static CancellationToken cancellationToken;
        //public varManagerDataSet VarManagerDataSet { get => varManagerDataSet; set => varManagerDataSet = value; }
        private List<string> listPayType, listLocation, listSort, listTags, listCategory, listCreator;
        private bool downlistHide = true;
        Dictionary<string, string> downloadUrls = new Dictionary<string, string>();
        private const int intPerPage = 48;
        private InvokeAddLoglist addlog;
        public FormHub()
        {
            InitializeComponent();
            addlog = new InvokeAddLoglist(UpdateAddLoglist);
            httpClient = new HttpClient();
            cancellationToken = new CancellationToken();
        }
        private void buttonScanHub_Click(object sender, EventArgs e)
        {
            string message = "Scan hub for missing Depends from All organized vars. A download link list that will be generated. You must be logged in at hub.virtamate.com before you can download these links, It is recommended to use Chrono for Chrome to download.";

            const string caption = "AllMissingDepends";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                AllMissingDepends();
            }
        }
        public delegate void InvokeAddLoglist(string message, LogLevel logLevel);

        public void UpdateAddLoglist(string message, LogLevel logLevel)
        {
            string msg = simpLog.WriteFormattedLog(logLevel, message);
            listBoxLog.Items.Add(msg);
            listBoxLog.TopIndex = listBoxLog.Items.Count - 1;
        }
        private async void AllMissingDepends()
        {
           
            this.BeginInvoke(addlog, new Object[] { "Search for dependencies...", LogLevel.INFO });
           
            List<string> missingvars = form1.MissingDependencies();
            if (missingvars.Count > 0)
            {
                string packages = string.Join(",", missingvars);
                this.BeginInvoke(addlog, new Object[] { " Search downloadable dependencies in the HUB.", LogLevel.INFO });
                var reponse = await FindPackages(packages);
                JSONNode jsonResult = JSON.Parse(reponse);
                JSONClass packageArray = jsonResult["packages"] as JSONClass;
                // List<string> downloadurls = new List<string>();
                //Dictionary< string, string> downloadurls = new Dictionary<string, string>();
                if (packageArray.Count > 0)
                {
                    this.BeginInvoke(addlog, new Object[] { $"{packageArray.Count} return records will be analyzed", LogLevel.INFO });
                    foreach (var package in packageArray.Childs)
                    {
                        string downloadurl = package["downloadUrl"];
                        string filename = package["filename"];
                        if (!string.IsNullOrEmpty(downloadurl) && downloadurl != "null")
                        {
                            if (!string.IsNullOrEmpty(filename) && filename != "null")
                            {
                                filename = filename.Substring(0, filename.IndexOf(".var"));
                                if (!form1.FindByvarName(filename))
                                {
                                    this.BeginInvoke(addlog, new Object[] { $"Find {filename} in the HUB", LogLevel.INFO });
                                    //if (!downloadUrls.ContainsKey(filename))
                                    downloadUrls[filename] = downloadurl;
                                }
                            }
                        }
                    }
                    //downloadurls = downloadurls.Distinct();
                }
                if (downloadUrls.Count > 0)
                {
                    this.BeginInvoke(addlog, new Object[] { $"Total {downloadUrls.Count} download links found", LogLevel.INFO });
                    ShowDownList();
                    DrawDownloadListView();
                    // listBoxDownList.Items.AddRange(downloadurls.ToArray());
                    //textBoxDownList.Text += string.Join("\r\n", downloadurls);
                    /*
                    string hubvars = Guid.NewGuid().ToString() + ".txt";
                    StreamWriter sw = new StreamWriter(hubvars);
                    foreach (var downloadurl in downloadurls)
                    {
                        sw.WriteLine(downloadurl);
                    }
                    sw.Close();
                    System.Diagnostics.Process.Start(hubvars);
                    */

                }
                else
                {
                    this.BeginInvoke(addlog, new Object[] { "No download link found", LogLevel.INFO });
                }
            }
        }

        private void DrawDownloadListView()
        {
            listViewDownList.Items.Clear();
            foreach (var varname in downloadUrls.Keys)
            {
                ListViewItem downloaditem = new ListViewItem();
                downloaditem.Text = varname;
                downloaditem.SubItems.Add(downloadUrls[varname]);
                listViewDownList.Items.Add(downloaditem);
            }
        }

        private static async Task<string> FindPackages(string packages)
        {
            string url = "https://hub.virtamate.com/citizenx/api.php";
            JSONClass jns = new JSONClass();
            jns.Add("source", "VaM");
            jns.Add("action", "findPackages");
            jns.Add("packages", packages);
            var data = new StringContent(jns.ToString(), Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, data);
                string reponse = response.Content.ReadAsStringAsync().Result;
                return reponse;
            }
        }

        private void FormHub_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            this.UseWaitCursor = true;
           if(!GetInfoList())
            {
                MessageBox.Show("Error getting HUB information!");
                this.Close();
                return;
            }
            comboBoxHosted.Items.Add("All");
            foreach (string item in listLocation)
            {
                comboBoxHosted.Items.Add(item);
            }

            comboBoxPayType.Items.Add("All");
            foreach (string item in listPayType)
            {
                comboBoxPayType.Items.Add(item);
            }


            comboBoxCategory.Items.Add("All");
            foreach (string item in listCategory)
            {
                comboBoxCategory.Items.Add(item);
            }

            comboBoxCreator.Items.Add("All");
            foreach (string item in listCreator)
            {
                comboBoxCreator.Items.Add(item);
            }

            comboBoxTags.Items.Add("All");
            foreach (string item in listTags)
            {
                comboBoxTags.Items.Add(item);
            }
            foreach (string item in listSort)
            {
                comboBoxPriSort.Items.Add(item);
            }
            comboBoxSecSort.Items.Add("");
            foreach (string item in listSort)
            {
                comboBoxSecSort.Items.Add(item);
            }
            downlistHide = true;
            splitContainer1.SplitterDistance = splitContainer1.Size.Width - 80;
            List<HubItem> items = new List<HubItem>();

            for (int i = 0; i < intPerPage; i++)
            {
                HubItem item = new HubItem();
                item.ClickFilter += Item_ClickFilter;
                item.GenLinkList += Item_GenLinkList;
                item.RetPackageName += Item_RetPackageName;


                flowLayoutPanelHubItems.Controls.Add(item);
                items.Add(item);
            }
            ClearFilter();
            this.UseWaitCursor = false;
            this.Enabled = true;
            GenerateHabItems();
            EnableFilterEvent();
            
        }

        private void Item_RetPackageName(object sender, PackageNameEventArgs e)
        {
            form1.SelectVarInList(e.PackageName);
            form1.Activate();
            //form1.LocateVar(e.PackageName);
        }

        private void Item_GenLinkList(object sender, DownloadLinkListEventArgs e)
        {
            foreach (string varname in e.DownloadLinks.Keys)
            {
                var exitname = form1.VarExistName(varname);
                if (exitname == "missing"|| exitname.EndsWith("$"))
                {
                    downloadUrls[varname] = e.DownloadLinks[varname] ;
                }
            }
            DrawDownloadListView();
        }

        private void DisableFilterEvent()
        {
            comboBoxHosted.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxPayType.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxCategory.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxCreator.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxTags.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxPriSort.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxSecSort.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxPages.SelectedIndexChanged -= ComboBoxSelectedChanged;
            textBoxSearch.TextChanged -= ComboBoxSelectedChanged;
        }

        private void EnableFilterEvent()
        {
            comboBoxHosted.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxPayType.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxCategory.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxCreator.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxTags.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxPriSort.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxSecSort.SelectedIndexChanged += ComboBoxSelectedChanged;
            comboBoxPages.SelectedIndexChanged += ComboBoxSelectedChanged;
            textBoxSearch.TextChanged += ComboBoxSelectedChanged;
        }

        private void ClearFilter()
        {
           // if (comboBoxHosted.Items.Contains("Hub And Dependencies"))
           //     comboBoxHosted.SelectedItem = "Hub And Dependencies";
           // else
                comboBoxHosted.SelectedIndex = 0;

            if (comboBoxPayType.Items.Contains("Free"))
                comboBoxPayType.SelectedItem = "Free";
            else
                comboBoxPayType.SelectedIndex = 0;

            comboBoxCategory.SelectedIndex = 0;
            comboBoxCreator.SelectedIndex = 0;
            comboBoxTags.SelectedIndex = 0;
            comboBoxPriSort.SelectedIndex = 0;
            comboBoxSecSort.SelectedIndex = 0;

            textBoxSearch.Text = "Search...";
            textBoxSearch.ForeColor = SystemColors.GrayText;

        }

        private void Item_ClickFilter(object sender, HubItemFilterEventArgs e)
        {
            DisableFilterEvent();
            
            HubItem hubItem = sender as HubItem;
            if (e.FilterType == "category")
            {
                ClearFilter();
                comboBoxPayType.Text = e.PayType;
                comboBoxCategory.Text = e.Category;
            }
            else if (e.FilterType == "creator")
            {
                ClearFilter();
                comboBoxCreator.Text = e.Creator;
            }
            EnableFilterEvent();
            GenerateHabItems();
        }

        private void ComboBoxSelectedChanged(object sender, EventArgs e)
        {

            if (comboBoxPages == sender)
            {
                GenerateHabItems(false);
            }
            else
            {
                GenerateHabItems();
            }
        }

        private void GenerateHabItems(bool genePages = true)
        {
            string location = comboBoxHosted.Text, paytype = comboBoxPayType.Text,
             category = comboBoxCategory.Text, username = comboBoxCreator.Text,
                 tags = comboBoxTags.Text, search = textBoxSearch.Text;
            if(search == "Search...")
            {
                search = "";
            }
            string sort = comboBoxPriSort.Text;
            if (!string.IsNullOrEmpty(comboBoxSecSort.Text))
                sort = sort + "," + comboBoxSecSort.Text;
            int page = 1;
            if (comboBoxPages.Items.Count > 0)
            {
                if (comboBoxPages.SelectedIndex >= 0)
                    page = comboBoxPages.SelectedIndex + 1;
            }
            // string reponse = Task<int>.Run(() => GetResources(48, location, paytype, category, username, tags, search, sort, page)).Result;
            try { GetResources(intPerPage, location, paytype, category, username, tags, search, sort, page); }
            catch (Exception)
            {

            }
        }

        private bool GetInfoList()
        {
            bool success = false;
            try
            {
                string reponse = Task<int>.Run(GetInfo).Result;
                if(string.IsNullOrEmpty(reponse)) return false;
                JSONNode jsonResult = JSON.Parse(reponse);
                JSONArray jArray = jsonResult["category"] as JSONArray;
                listPayType = new List<string>();
                foreach (var item in jArray.Childs)
                {
                    listPayType.Add(item.Value);
                }
                jArray = jsonResult["location"] as JSONArray;
                listLocation = new List<string>();
                foreach (var item in jArray.Childs)
                {
                    listLocation.Add(item.Value);
                }
                jArray = jsonResult["type"] as JSONArray;
                listCategory = new List<string>();
                foreach (var item in jArray.Childs)
                {
                    listCategory.Add(item.Value);
                }
                jArray = jsonResult["sort"] as JSONArray;
                listSort = new List<string>();
                foreach (var item in jArray.Childs)
                {
                    listSort.Add(item.Value);
                }

                JSONClass jClass = jsonResult["tags"] as JSONClass;
                listTags = new List<string>();
                foreach (var item in jClass.Keys)
                {
                    listTags.Add(item);
                }

                jClass = jsonResult["users"] as JSONClass;
                listCreator = new List<string>();
                foreach (var item in jClass.Keys)
                {
                    listCreator.Add(item);
                }
                success=true;
            }
            catch (Exception)
            {

            }
            return success;
        }

        private void buttonFirstPage_Click(object sender, EventArgs e)
        {
            if (comboBoxPages.SelectedIndex > 0) comboBoxPages.SelectedIndex = 0;
        }

        private void buttonPrevPage_Click(object sender, EventArgs e)
        {
            if (comboBoxPages.SelectedIndex > 0) comboBoxPages.SelectedIndex--;
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            if (comboBoxPages.SelectedIndex < comboBoxPages.Items.Count - 1) comboBoxPages.SelectedIndex++;
        }

        private void buttonLastPage_Click(object sender, EventArgs e)
        {
            if (comboBoxPages.SelectedIndex < comboBoxPages.Items.Count - 1) comboBoxPages.SelectedIndex = comboBoxPages.Items.Count - 1;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            GenerateHabItems();
        }

        private void GetResources(int perpage = intPerPage,
            string location = "Hub And Dependencies", string paytype = "Free",
            string category = "", string username = "", string tags = "", string search = "",
            string sort = "Latest Update",
            int page = 1)
        {
            string url = "https://hub.virtamate.com/citizenx/api.php";

            JSONClass jns = new JSONClass();
            jns.Add("source", "VaM");
            jns.Add("action", "getResources");
            jns.Add("latest_image", "Y");

            //if(!string.IsNullOrEmpty(location) && location != "All")
            jns.Add("location", location);
            //if (!string.IsNullOrEmpty(paytype) && paytype != "All")
            jns.Add("category", paytype);
            if (!string.IsNullOrEmpty(category) && category != "All")
                jns.Add("type", category);
            if (!string.IsNullOrEmpty(username) && username != "All")
                jns.Add("username", username);
            if (!string.IsNullOrEmpty(tags) && tags != "All")
                jns.Add("tags", tags);
            if (!string.IsNullOrEmpty(search))
                jns.Add("search", search);
            jns.Add("searchall", "true");
            jns.Add("sort", sort);
            jns.Add("perpage", perpage.ToString());
            jns.Add("page", page.ToString());

            var data = new StringContent(jns.ToString(), Encoding.UTF8, "application/json");
            try
            {
                GetResponseAsync(url, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GetResponseAsync(string url, StringContent data)
        {
            httpClient.CancelPendingRequests();
            httpClient.PostAsync(url, data).ContinueWith(
              (requestTask) =>
              {
                  if (requestTask.Status == TaskStatus.RanToCompletion)
                  {
                      HttpResponseMessage response = requestTask.Result;                    // 确认响应成功，否则抛出异常
                      if (response.IsSuccessStatusCode)
                          // 异步读取响应为字符串
                          response.Content.ReadAsStringAsync().ContinueWith(ResponseTask);
                  }
              });
        }

        public delegate void InvokeRefreshResource(string response);

        private void buttonClearFilters_Click(object sender, EventArgs e)
        {
            DisableFilterEvent();
            ClearFilter();
            EnableFilterEvent();
            GenerateHabItems();
        }

        private void buttonEmptySearch_Click(object sender, EventArgs e)
        {
            textBoxSearch.Text = "Search...";
            textBoxSearch.ForeColor = SystemColors.GrayText;
        }

        private void buttonScanHubUpdate_Click(object sender, EventArgs e)
        {
            string message = "Scan Hub For Packages With Updates.A download link list that will be generated. You must be logged in at hub.virtamate.com before you can download these links, It is recommended to use Chrono for Chrome to download.";

            const string caption = "AllMissingDepends";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                UpdatAllPackages();
            }
        }
        struct PackVerDownID
        {
            public int ver;
            public string downloadid;

            public PackVerDownID(int ver, string downloadid)
            {
                this.ver = ver;
                this.downloadid = downloadid;
            }
        }
        private async void UpdatAllPackages()
        {
            this.BeginInvoke(addlog, new Object[] { "Search for upgradable vars...", LogLevel.INFO });
            var reponse = await GetHubPackages();
            JSONClass jsonPackages = JSON.Parse(reponse) as JSONClass;
            this.BeginInvoke(addlog, new Object[] { $"Total {jsonPackages.Count} Packages found in the HUB.", LogLevel.INFO }); 
            Dictionary<string, PackVerDownID> hubPackages = new Dictionary<string, PackVerDownID>();
            foreach (string package in jsonPackages.Keys)
            {
                string  packageIdenty = package;
                if (packageIdenty.EndsWith(".var"))
                    packageIdenty = packageIdenty.Substring(0, packageIdenty.Length - 4);
                int splitindex = packageIdenty.LastIndexOf('.');
                if (splitindex >= 0)
                {
                    string packageName= packageIdenty.Substring(0, splitindex);
                    int version = -1;
                    int.TryParse(packageIdenty.Substring(splitindex + 1), out version);
                    if (hubPackages.ContainsKey(packageName))
                    {
                        if (version > hubPackages[packageName].ver)
                        {
                            string downid = jsonPackages[package].Value;
                            hubPackages[packageName]= new PackVerDownID(version, downid);
                        }
                    }
                    else
                    {
                        string downid = jsonPackages[package].Value;
                        hubPackages.Add(packageName, new PackVerDownID(version, downid));
                    }
                }
            }
            List<string> toBeUpdVars= new List<string>(); 
            foreach (var hubpackage in hubPackages)
            {
                string varlastname = form1.VarExistName(hubpackage.Key + ".latest");
                if (varlastname != "missing")
                {
                    int splitindex = varlastname.LastIndexOf('.');
                    if (splitindex >= 0)
                    {
                        int version = -1;
                        int.TryParse(varlastname.Substring(splitindex + 1), out version);
                        if (hubpackage.Value.ver > version)
                        {
                            this.BeginInvoke(addlog, new Object[] { $"Find the upgradeable package,{varlastname} ", LogLevel.INFO });
                            toBeUpdVars.Add(hubpackage.Key + ".latest");
                        }
                    }
                }
            }
            if (toBeUpdVars.Count > 0)
            {
                string packages = string.Join(",", toBeUpdVars);
                this.BeginInvoke(addlog, new Object[] { $"Total {toBeUpdVars.Count} upgradable var files found in the HUB.", LogLevel.INFO });
                reponse = await FindPackages(packages);
                JSONNode jsonResult = JSON.Parse(reponse);
                JSONClass packageArray = jsonResult["packages"] as JSONClass;
                //List<string> downloadurls = new List<string>();
                if (packageArray.Count > 0)
                {
                    this.BeginInvoke(addlog, new Object[] { $"{packageArray.Count} return records will be analyzed", LogLevel.INFO });
                    foreach (var package in packageArray.Childs)
                    {
                        string downloadurl = package["downloadUrl"];
                        string filename = package["filename"];
                        if (!string.IsNullOrEmpty(downloadurl) && downloadurl != "null")
                        {
                            if (!string.IsNullOrEmpty(filename) && filename != "null")
                            {
                                filename = filename.Substring(0, filename.IndexOf(".var"));
                                if (!form1.FindByvarName(filename))
                                {
                                    this.BeginInvoke(addlog, new Object[] { $"Find {filename} in the HUB", LogLevel.INFO });
                                    if (!downloadUrls.ContainsKey(filename))
                                        downloadUrls.Add(filename, downloadurl);
                                }
                            }
                        }
                    }
                    //downloadurls = downloadurls.Distinct().ToList();
                }
                if (downloadUrls.Count > 0)
                {
                    this.BeginInvoke(addlog, new Object[] { $"Total {downloadUrls.Count} updatable download links found", LogLevel.INFO });
                    ShowDownList();
                    DrawDownloadListView();
                    
                }
                else
                {
                    this.BeginInvoke(addlog, new Object[] { "No download link found", LogLevel.WARNING });
                }
            }
        }

     

        private static async Task<string> GetHubPackages()
        {
            string url = "https://s3cdn.virtamate.com/data/packages.json";
            
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string reponse = response.Content.ReadAsStringAsync().Result;
                return reponse;
            }
        }

        private void ShowDownList()
        {
            if (downlistHide)
            {
                int splitterDistance = splitContainer1.Size.Width - 500;
                if (splitterDistance < 80) splitterDistance = 80;
                splitContainer1.SplitterDistance = splitterDistance;
                downlistHide = false;
            }
        }

        private void HideDownLoadList(object sender, EventArgs e)
        {
            if (!downlistHide)
            {
                splitContainer1.SplitterDistance = splitContainer1.Size.Width - 80;
                downlistHide = true;
            }
        }

        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if(textBoxSearch.Text=="Search...")
            {
                textBoxSearch.Text = "";
            }
            textBoxSearch.ForeColor = SystemColors.WindowText;
        }

        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text)||textBoxSearch.Text == "Search...")
            {
                textBoxSearch.Text = "Search...";
                textBoxSearch.ForeColor = SystemColors.GrayText;
            }
        }

        private void DownList_MouseEnter(object sender, EventArgs e)
        {
            ShowDownList();
        }

        private void buttonCopytoClip_Click(object sender, EventArgs e)
        {
            if (downloadUrls.Count > 0)
            {
                Clipboard.SetText(string.Join("\r\n", downloadUrls.Values));
                MessageBox.Show("Copied to clipboard, you can paste to chrono for chrome(edge) to download");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            downloadUrls.Clear();
            DrawDownloadListView();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FormHub_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (listViewDownList.Items.Count > 0)
            {
                if (MessageBox.Show("Download list data will be lost, confirm exit?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public void RefreshResource(string response)
        {
            JSONNode jsonResult = JSON.Parse(response);

            JSONClass pagination = jsonResult["pagination"] as JSONClass;
            int totalFound = int.Parse(pagination["total_found"].Value);

            labelTotal.Text = $"Total: {totalFound}";
            int totalPages = int.Parse(pagination["total_pages"].Value);
            int curPage = int.Parse(pagination["page"].Value);
            if (curPage > totalPages) curPage = totalPages;
            if (curPage < 1) curPage = 1;
            comboBoxPages.SelectedIndexChanged -= ComboBoxSelectedChanged;
            comboBoxPages.Items.Clear();
            for (int i = 0; i < totalPages; i++)
            {
                comboBoxPages.Items.Add($"{i + 1 } of {totalPages}");
            }
            if (comboBoxPages.Items.Count > 0)
                comboBoxPages.SelectedIndex = curPage - 1;
            comboBoxPages.SelectedIndexChanged += ComboBoxSelectedChanged;

            var resources = jsonResult["resources"].AsArray;

            for (int index = 0; index < intPerPage; index++)
            {
                HubItem hubItem = (HubItem)flowLayoutPanelHubItems.Controls[index];
                if (resources.Count > index)
                {
                    hubItem.Visible = true;
                    JSONClass resource = resources[index] as JSONClass;
                    hubItem.SetResource(resource); 
                    string inRepository = "Unknown Status";
                    if (resource.HasKey("hubFiles"))
                    {
                        var hubfiles = resource["hubFiles"].AsArray;
                        if (hubfiles.Count > 0)
                        {
                            int inrepons = -1;
                            //JSONClass hubfile = hubfiles[0] as JSONClass;
                            foreach (JSONClass hubfile in hubfiles)
                            {
                                string filename = hubfile["filename"];
                                if (filename.EndsWith(".var"))
                                    filename = filename.Substring(0, filename.Length - 4);
                                hubItem.PackageName = filename;
                                //int splitindex = filename.LastIndexOf('.');
                                string[] filenameparts = filename.Split(('.'));
                                if (filenameparts.Length >= 2)
                                {
                                    string hubpackageName = filenameparts[0] + "." + filenameparts[1];
                                    int hubversion = 1;
                                    if (filenameparts.Length >= 3)
                                        int.TryParse(filenameparts[2], out hubversion); 
                                    string varlastname = form1.VarExistName(hubpackageName + ".latest");
                                    if (varlastname != "missing")
                                    {
                                        int lastversion = int.Parse(varlastname.Substring(filename.LastIndexOf('.') + 1));
                                        if (lastversion >= hubversion)
                                        {
                                            if (inrepons < 0)
                                            {
                                                inRepository = "In Repository";
                                                inrepons = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (inrepons < 1)
                                            {
                                                inRepository = $"{lastversion} Upgrade to {hubversion}";
                                                inrepons = 1;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        inRepository = "Generate Download List";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (resource.HasKey("download_url"))
                        {
                            inRepository = "Go To Download";
                        }
                    }
                    hubItem.InRepository=inRepository;
                    hubItem.RefreshItem();
                }
                else
                    hubItem.Visible = false;
            }
        }
        private void ResponseTask(Task<string> responseTask)
        {
            try
            {
                InvokeRefreshResource refreshResource = new InvokeRefreshResource(RefreshResource);
                string response = responseTask.Result;
                this.BeginInvoke(refreshResource, new Object[] { response });
            }
            catch (Exception) { }
        }

        private static async Task<string> GetResponse(string url, StringContent data)
        {
            //using (var client = new HttpClient())
            //{
            string strresponse = "";
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            try
            {
                var response = await httpClient.PostAsync(url, data, cancellationToken);

                strresponse = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {

            }
            return strresponse;
            //}
        }

        private static async Task<string> GetInfo()
        {
            string url = "https://hub.virtamate.com/citizenx/api.php";

            JSONClass jns = new JSONClass();
            jns.Add("source", "VaM");
            jns.Add("action", "getInfo");
            var data = new StringContent(jns.ToString(), Encoding.UTF8, "application/json");
            return await GetResponse(url, data);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
