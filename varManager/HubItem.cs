using SimpleJSON;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace varManager
{
    
    public partial class HubItem : UserControl
    {
        private JSONClass resource;
        private string paytype, category, title, version, tagLine, imageUrl, creatorName, creatorIcon, resource_id,download_url,packageName;
        private double ratingAvg;
        private int ratingCount,downloads;
        private DateTime lastUpdated;
        private string inRepository = "";

        public string InRepository { get => inRepository; set => inRepository = value; }
        public string PackageName { get => packageName; set => packageName = value; }

        public HubItem()
        {
            InitializeComponent();
        }

        private void HubItem_Load(object sender, EventArgs e)
        {
            RefreshItem();
        }
        private bool GetResourceDetail()
        {
            bool success = false;
            try
            {
                string reponse = Task<int>.Run(()=>GetResourceDetail(resource_id)).Result;
                if (string.IsNullOrEmpty(reponse)) return false;
                JSONNode jsonResult = JSON.Parse(reponse);
                Dictionary<string, string> varDownloadUrl = new Dictionary<string, string>();
                var hubFiles = jsonResult["hubFiles"].AsArray;

                foreach (JSONClass hubFile in hubFiles)
                {
                    var filename = hubFile["filename"].Value;
                    if (filename.EndsWith(".var")) filename = filename.Substring(0, filename.Length - 4);
                    varDownloadUrl[filename] = hubFile["urlHosted"];
                }
                JSONClass dependenciesFiles = jsonResult["dependencies"] as JSONClass;
                foreach (var hubFile in dependenciesFiles.Keys)
                {
                    JSONArray dependencies = dependenciesFiles[hubFile].AsArray;
                    foreach (JSONClass dependencie in dependencies)
                    {
                        if (!string.IsNullOrEmpty(dependencie["downloadUrl"].Value)&&!(dependencie["downloadUrl"].Value=="null"))
                        varDownloadUrl[dependencie["filename"]] = dependencie["downloadUrl"];
                    }
                }
                RaiseGenLinkListFilterEvent(varDownloadUrl);
                success = true;
            }
            catch (Exception)
            {

            }
            return success;
        }
        private static async Task<string> GetResourceDetail(string resourceid)
        {
            string url = "https://hub.virtamate.com/citizenx/api.php";

            JSONClass jns = new JSONClass();
            jns.Add("source", "VaM");
            jns.Add("action", "getResourceDetail");
            jns.Add("latest_image", "Y");
            jns.Add("resource_id",  resourceid);
             
            var data = new StringContent(jns.ToString(), Encoding.UTF8, "application/json");
            return await GetResponse(url, data);
        }
        private static async Task<string> GetResponse(string url, StringContent data)
        {
            //using (var client = new HttpClient())
            //{
            string strresponse = "";
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            try
            {
                var response = await httpClient.PostAsync(url, data);

                strresponse = response.Content.ReadAsStringAsync().Result;
            }
            catch
            {

            }
            return strresponse;
            //}
        }
        private void buttonInRepository_Click(object sender, EventArgs e)
        {
            if (buttonInRepository.Text.Contains("Generate Download List")|| buttonInRepository.Text.Contains("Upgrade to")) 
            {
                GetResourceDetail();
            }
            if (buttonInRepository.Text.Contains("In Repository"))
            {
                RaisePackageNameEvent(packageName);
            }
            if (buttonInRepository.Text.StartsWith("Go To "))
            {
                if (!string.IsNullOrEmpty(download_url))
                    System.Diagnostics.Process.Start($"{download_url}");
            }
        }
        void RaiseGenLinkListFilterEvent(Dictionary<string, string> varDownloadUrl)
        {
            DownloadLinkListEventArgs newEventArgs =
                    new DownloadLinkListEventArgs();
            newEventArgs.DownloadLinks = varDownloadUrl;
            if (GenLinkList != null)
                GenLinkList(this, newEventArgs);
        } 
        
        public delegate void GenLinkListHandle(object sender, DownloadLinkListEventArgs e);
        //Event name 
        public event GenLinkListHandle GenLinkList;

        public delegate void ClickFilterHandle(object sender, HubItemFilterEventArgs e);
        //Event name 
        public event ClickFilterHandle ClickFilter;

        public delegate void RetPackageNameHandle(object sender, PackageNameEventArgs e);
        //Event name 
        public event RetPackageNameHandle RetPackageName;
        public void RefreshItem()
        {
            buttonType.Text = $"{paytype} {category}";
            labelTitle.Text = title;
            labelVersion.Text = version;
            labelTagLine.Text = tagLine;
            PictureBox[] ratingCtls = { picRating1, picRating2, picRating3, picRating4, picRating5 };
            double rating = ratingAvg + 1;
            foreach (PictureBox rctl in ratingCtls)
            {
                toolTip1.SetToolTip(rctl, $"{ratingAvg}/5");
                rating--;
                if (rating < 0.125)
                {
                    rctl.Image = global::varManager.Properties.Resources.starEmpty;
                    continue;
                }
                if (rating < 0.375)
                {
                    rctl.Image = global::varManager.Properties.Resources.starOneQuarter;
                    continue;
                }
                if (rating < 0.625)
                {
                    rctl.Image = global::varManager.Properties.Resources.starHalf;
                    continue;
                }
                if (rating < 0.875)
                {
                    rctl.Image = global::varManager.Properties.Resources.starTriQuarter;
                    continue;
                }
                rctl.Image = global::varManager.Properties.Resources.starFull;
            }
            labelRatingCount.Text = $"{ratingCount} ratings";
            labelDownloads.Text = $"{downloads}";
            labelLastUpdated.Text = $"{lastUpdated.ToString("MMM d,yyyy", CultureInfo.CreateSpecificCulture("en-US"))}";
            try
            {
                if (!string.IsNullOrEmpty(imageUrl))
                    pictureBoxImage.LoadAsync(imageUrl);
            }
            catch
            {

            }
            try
            {
                if (!string.IsNullOrEmpty(creatorIcon))
                    pictureBoxUser.LoadAsync(creatorIcon);
            }
            catch
            {

            }
            buttonUser.Text = creatorName;
            buttonInRepository.Text = inRepository;
            switch (inRepository)
            {
                case "In Repository":
                    buttonInRepository.BackColor = Color.DarkCyan;
                    toolTip1.SetToolTip(buttonInRepository, "You already own this package, click to locate it in the main window");
                    break; 
                case "Go To Download":
                    buttonInRepository.BackColor = Color.MediumOrchid;
                    toolTip1.SetToolTip(buttonInRepository, "Clicking will open the download page with your browser");
                    break;
                default:
                    buttonInRepository.BackColor = Color.SteelBlue;
                    if (buttonInRepository.Text.Contains("Generate Download List") || buttonInRepository.Text.Contains("Upgrade to"))
                    {
                        toolTip1.SetToolTip(buttonInRepository, "A list of downloads will be generated");
                    }
                     break;

                   
            }
          
        }

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start($"https://hub.virtamate.com/resources/{resource_id}/");
        }
        void RaiseClickFilterEvent(string filterType,string payType,string category,string  creator)
        {
            HubItemFilterEventArgs newEventArgs =
                    new HubItemFilterEventArgs();
            newEventArgs.FilterType = filterType;
            newEventArgs.PayType = payType;
            newEventArgs.Category = category;   
            newEventArgs.Creator = creator; 
            if (ClickFilter!=null)
               ClickFilter(this,newEventArgs);
        }

        void RaisePackageNameEvent(string packageName)
        {
            PackageNameEventArgs newEventArgs = new PackageNameEventArgs();
            newEventArgs.PackageName = packageName;
            if (RetPackageName != null)
                RetPackageName(this, newEventArgs);
        }

        private void buttonType_Click(object sender, EventArgs e)
        {
            RaiseClickFilterEvent("category", paytype, category, creatorName);
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            RaiseClickFilterEvent("creator", paytype, category, creatorName);
        }

        private void pictureBoxUser_Click(object sender, EventArgs e)
        {
            RaiseClickFilterEvent("creator", paytype, category, creatorName);
        }


        public void SetResource(JSONClass json) 
        {
            this.resource = json;
            this.paytype = resource["category"].Value;
            this.category = resource["type"].Value;
            this.title = resource["title"].Value;
            this.version = resource["version_string"].Value;
            this.tagLine = resource["tag_line"].Value;
            this.ratingAvg = double.Parse(resource["rating_avg"].Value);
            this.ratingCount = int.Parse(resource["rating_count"].Value);
            this.downloads = int.Parse(resource["download_count"].Value);
            long unixTimeStamp = long.Parse(resource["last_update"].Value);
            DateTime dt1 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            this.lastUpdated = dt1.AddSeconds(unixTimeStamp).ToLocalTime();
            this.imageUrl = resource["image_url"].Value;
            this.creatorIcon = resource["icon_url"].Value;
            this.creatorName = resource["username"].Value;
            this.resource_id = resource["resource_id"].Value;
            this.download_url = resource["download_url"].Value;
        }
    }
    public class HubItemFilterEventArgs : EventArgs
    {
        private string filterType,payType,category,creator;
        public string FilterType { get => filterType; set => filterType = value; }
        public string PayType { get => payType; set => payType = value; }
        public string Category { get => category; set => category = value; }
        public string Creator { get => creator; set => creator = value; }
    }
    public class DownloadLinkListEventArgs : EventArgs
    {
        private Dictionary<string, string>  downloadLinks;

        public Dictionary<string, string>  DownloadLinks { get => downloadLinks; set => downloadLinks = value; }
    }

    public class GotoDownloadEventArgs : EventArgs
    {
        private string gotoDownload;

        public string GotoDownload { get => gotoDownload; set => gotoDownload = value; }
    }
    public class PackageNameEventArgs : EventArgs
    {
        private string packageName;

        public string PackageName { get => packageName; set => packageName = value; }
    }
}
