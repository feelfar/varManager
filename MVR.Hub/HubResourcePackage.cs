using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MVR.FileManagement;
using SimpleJSON;

namespace MVR.Hub
{
	// Token: 0x02000C93 RID: 3219
	public class HubResourcePackage
	{
		// Token: 0x06005FBB RID: 24507 RVA: 0x00242574 File Offset: 0x00240974
		public HubResourcePackage(JSONClass package, HubBrowse hubBrowse, bool isDependency)
		{
			this.browser = hubBrowse;
			this.package_id = package["package_id"];
			this.resource_id = package["resource_id"];
			string text = package["filename"];
			text = Regex.Replace(text, ".var$", string.Empty);
			this.GroupName = Regex.Replace(text, "(.*)\\..*", "$1");
			this.Creator = Regex.Replace(this.GroupName, "(.*)\\..*", "$1");
			this.Version = package["version"];
			if (this.Version == null)
			{
				this.Version = Regex.Replace(text, ".*\\.([0-9]+)$", "$1");
			}
			this.resolvedVarName = this.GroupName + "." + this.Version + ".var";
			string text2 = package["latest_version"];
			if (text2 == null)
			{
				text2 = this.Version;
			}
			if (text2 != null)
			{
				int latestVersion;
				if (int.TryParse(text2, out latestVersion))
				{
					this.LatestVersion = latestVersion;
				}
				else
				{
					this.LatestVersion = -1;
				}
			}
			string startingValue = package["licenseType"];
			string s = package["file_size"];
			this.SyncFileSize(s);
			string startingValue2 = HubResourcePackage.SizeSuffix(this.FileSize, 1);
			this.downloadUrl = package["downloadUrl"];
			if (this.downloadUrl == null)
			{
				this.downloadUrl = package["urlHosted"];
			}
			this.latestUrl = package["latestUrl"];
			if (this.latestUrl == null)
			{
				this.latestUrl = this.downloadUrl;
			}
			bool startingValue3 = this.downloadUrl == "null";
			this.promotionalUrl = package["promotional_link"];
			this.goToResourceAction = new JSONStorableAction("GoToResource", new JSONStorableAction.ActionCallback(this.GoToResource));
			this.isDependencyJSON = new JSONStorableBool("isDependency", isDependency);
			this.nameJSON = new JSONStorableString("name", text);
			this.licenseTypeJSON = new JSONStorableString("licenseType", startingValue);
			this.fileSizeJSON = new JSONStorableString("fileSize", startingValue2);
			this.alreadyHaveJSON = new JSONStorableBool("alreadyHave", false);
			this.alreadyHaveSceneJSON = new JSONStorableBool("alreadyHaveScene", false);
			this.updateAvailableJSON = new JSONStorableBool("updateAvailable", false);
			this.updateMsgJSON = new JSONStorableString("updateMsg", "Update");
			this.updateAction = new JSONStorableAction("Update", new JSONStorableAction.ActionCallback(this.Update));
			this.notOnHubJSON = new JSONStorableBool("notOnHub", startingValue3);
			this.downloadAction = new JSONStorableAction("Download", new JSONStorableAction.ActionCallback(this.Download));
			this.isDownloadQueuedJSON = new JSONStorableBool("isDownloadQueued", false);
			this.isDownloadingJSON = new JSONStorableBool("isDownloading", false);
			this.isDownloadedJSON = new JSONStorableBool("isDownloaded", false);
			this.downloadProgressJSON = new JSONStorableFloat("downloadProgress", 0f, 0f, 1f, true, false);
			this.openInPackageManagerAction = new JSONStorableAction("OpenInPackageManager", new JSONStorableAction.ActionCallback(this.OpenInPackageManager));
			this.openSceneAction = new JSONStorableAction("OpenScene", new JSONStorableAction.ActionCallback(this.OpenScene));
			this.Refresh();
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x002428F4 File Offset: 0x00240CF4
		private static string SizeSuffix(int value, int decimalPlaces = 1)
		{
			if (value < 0)
			{
				return "-" + HubResourcePackage.SizeSuffix(-value, 1);
			}
			if (value == 0)
			{
				return string.Format("{0:n" + decimalPlaces + "} bytes", 0);
			}
			int num = (int)Math.Log((double)value, 1024.0);
			decimal num2 = value / (1L << num * 10);
			if (Math.Round(num2, decimalPlaces) >= 1000m)
			{
				num++;
				num2 /= 1024m;
			}
			return string.Format("{0:n" + decimalPlaces + "} {1}", num2, HubResourcePackage.SizeSuffixes[num]);
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x002429C6 File Offset: 0x00240DC6
		protected void GoToResource()
		{
			if (this.resource_id != "null")
			{
				this.browser.OpenDetail(this.resource_id, false);
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06005FBE RID: 24510 RVA: 0x002429EF File Offset: 0x00240DEF
		// (set) Token: 0x06005FBF RID: 24511 RVA: 0x002429F7 File Offset: 0x00240DF7
		public string GroupName { get; protected set; }

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06005FC0 RID: 24512 RVA: 0x00242A00 File Offset: 0x00240E00
		// (set) Token: 0x06005FC1 RID: 24513 RVA: 0x00242A08 File Offset: 0x00240E08
		public string Creator { get; protected set; }

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06005FC2 RID: 24514 RVA: 0x00242A11 File Offset: 0x00240E11
		// (set) Token: 0x06005FC3 RID: 24515 RVA: 0x00242A19 File Offset: 0x00240E19
		public string Version { get; protected set; }

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x00242A22 File Offset: 0x00240E22
		// (set) Token: 0x06005FC5 RID: 24517 RVA: 0x00242A2A File Offset: 0x00240E2A
		public int LatestVersion { get; protected set; }

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x00242A33 File Offset: 0x00240E33
		public string Name
		{
			get
			{
				return this.nameJSON.val;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06005FC7 RID: 24519 RVA: 0x00242A40 File Offset: 0x00240E40
		public string LicenseType
		{
			get
			{
				return this.licenseTypeJSON.val;
			}
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x00242A50 File Offset: 0x00240E50
		protected void SyncFileSize(string s)
		{
			int fileSize;
			if (int.TryParse(s, out fileSize))
			{
				this.FileSize = fileSize;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06005FC9 RID: 24521 RVA: 0x00242A71 File Offset: 0x00240E71
		// (set) Token: 0x06005FCA RID: 24522 RVA: 0x00242A79 File Offset: 0x00240E79
		public int FileSize { get; protected set; }

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06005FCB RID: 24523 RVA: 0x00242A82 File Offset: 0x00240E82
		public bool NeedsDownload
		{
			get
			{
				return !this.alreadyHaveJSON.val || this.updateAvailableJSON.val;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06005FCC RID: 24524 RVA: 0x00242AA2 File Offset: 0x00240EA2
		public bool IsDownloading
		{
			get
			{
				return this.isDownloadingJSON.val;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06005FCD RID: 24525 RVA: 0x00242AAF File Offset: 0x00240EAF
		public bool IsDownloadQueued
		{
			get
			{
				return this.isDownloadQueuedJSON.val;
			}
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x00242ABC File Offset: 0x00240EBC
		protected void DownloadStarted()
		{
			this.isDownloadQueuedJSON.val = false;
			this.isDownloadingJSON.val = true;
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x00242AD6 File Offset: 0x00240ED6
		protected void DownloadProgress(float f)
		{
			this.downloadProgressJSON.val = f;
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x00242AE4 File Offset: 0x00240EE4
		protected void DownloadComplete(byte[] data, Dictionary<string, string> responseHeaders)
		{
			this.isDownloadingJSON.val = false;
			this.isDownloadedJSON.val = true;
			string input;
			string str;
			if (responseHeaders.TryGetValue("Content-Disposition", out input))
			{
				input = Regex.Replace(input, ";$", string.Empty);
				str = Regex.Replace(input, ".*filename=\"?([^\"]+)\"?.*", "$1");
			}
			else
			{
				str = this.resolvedVarName;
			}
			try
			{
				FileManager.WriteAllBytes("AddonPackages/" + str, data);
			}
			catch (Exception ex)
			{
				SuperController.LogError("Error while trying to save file AddonPackages/" + str + " after download");
				this.isDownloadQueuedJSON.val = false;
				this.isDownloadingJSON.val = false;
			}
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x00242BA4 File Offset: 0x00240FA4
		protected void DownloadError(string err)
		{
			this.isDownloadQueuedJSON.val = false;
			this.isDownloadingJSON.val = false;
			SuperController.LogError("Error while downloading " + this.Name + ": " + err);
		}

		// Token: 0x06005FD2 RID: 24530 RVA: 0x00242BDC File Offset: 0x00240FDC
		public void Download()
		{
			if (this.browser != null && this.downloadUrl != null && this.downloadUrl != string.Empty && this.downloadUrl != "null" && !this.isDownloadQueuedJSON.val && (!this.alreadyHaveJSON.val || this.updateAvailableJSON.val))
			{
				if (!this.alreadyHaveJSON.val)
				{
					this.isDownloadQueuedJSON.val = true;
					this.browser.QueueDownload(this.downloadUrl, this.promotionalUrl, new HubBrowse.BinaryRequestStartedCallback(this.DownloadStarted), new HubBrowse.RequestProgressCallback(this.DownloadProgress), new HubBrowse.BinaryRequestSuccessCallback(this.DownloadComplete), new HubBrowse.RequestErrorCallback(this.DownloadError));
				}
				else if (this.updateAvailableJSON.val && this.latestUrl != null && this.latestUrl != string.Empty && this.latestUrl != "null")
				{
					this.isDownloadQueuedJSON.val = true;
					this.browser.QueueDownload(this.latestUrl, this.promotionalUrl, new HubBrowse.BinaryRequestStartedCallback(this.DownloadStarted), new HubBrowse.RequestProgressCallback(this.DownloadProgress), new HubBrowse.BinaryRequestSuccessCallback(this.DownloadComplete), new HubBrowse.RequestErrorCallback(this.DownloadError));
				}
			}
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x00242D60 File Offset: 0x00241160
		public void Update()
		{
			if (this.browser != null && this.latestUrl != null && this.latestUrl != string.Empty && !this.isDownloadQueuedJSON.val && this.updateAvailableJSON.val)
			{
				this.isDownloadQueuedJSON.val = true;
				this.browser.QueueDownload(this.latestUrl, this.promotionalUrl, new HubBrowse.BinaryRequestStartedCallback(this.DownloadStarted), new HubBrowse.RequestProgressCallback(this.DownloadProgress), new HubBrowse.BinaryRequestSuccessCallback(this.DownloadComplete), new HubBrowse.RequestErrorCallback(this.DownloadError));
			}
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x00242E14 File Offset: 0x00241214
		public void OpenInPackageManager()
		{
			VarPackage package = FileManager.GetPackage(this.nameJSON.val);
			if (package != null)
			{
				SuperController.singleton.OpenPackageInManager(this.nameJSON.val);
			}
		}

		// Token: 0x06005FD5 RID: 24533 RVA: 0x00242E4D File Offset: 0x0024124D
		protected void OpenScene()
		{
			if (this.alreadyHaveScenePath != null)
			{
				SuperController.singleton.Load(this.alreadyHaveScenePath);
			}
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x00242E6C File Offset: 0x0024126C
		public void Refresh()
		{
			this.isDownloadedJSON.val = false;
			VarPackage package;
			if (this.isDependencyJSON.val)
			{
				package = FileManager.GetPackage(this.nameJSON.val);
			}
			else
			{
				string str = FileManager.PackageIDToPackageGroupID(this.nameJSON.val);
				string packageUidOrPath = str + ".latest";
				package = FileManager.GetPackage(packageUidOrPath);
			}
			if (package != null)
			{
				this.alreadyHaveJSON.val = true;
				if ((this.Version == "latest" || !this.isDependencyJSON.val) && this.LatestVersion != -1)
				{
					if (package.Version < this.LatestVersion)
					{
						this.updateAvailableJSON.val = true;
						this.updateMsgJSON.val = string.Concat(new object[]
						{
							"Update ",
							package.Version,
							" -> ",
							this.LatestVersion
						});
					}
					else
					{
						this.updateAvailableJSON.val = false;
					}
				}
				else
				{
					this.updateAvailableJSON.val = false;
				}
				List<FileEntry> list = new List<FileEntry>();
				package.FindFiles("Saves/scene", "*.json", list);
				if (list.Count > 0)
				{
					FileEntry fileEntry = list[0];
					this.alreadyHaveScenePath = fileEntry.Uid;
					this.alreadyHaveSceneJSON.val = true;
				}
				else
				{
					this.alreadyHaveScenePath = null;
					this.alreadyHaveSceneJSON.val = false;
				}
			}
			else
			{
				this.alreadyHaveJSON.val = false;
				this.alreadyHaveScenePath = null;
				this.alreadyHaveSceneJSON.val = false;
			}
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x00243014 File Offset: 0x00241414
		public void RegisterUI(HubResourcePackageUI ui)
		{
			if (ui != null)
			{
				ui.connectedItem = this;
				this.goToResourceAction.button = ui.resourceButton;
				if (ui.resourceButton != null)
				{
					ui.resourceButton.interactable = (!this.notOnHubJSON.val && this.isDependencyJSON.val);
				}
				this.isDependencyJSON.indicator = ui.isDependencyIndicator;
				this.nameJSON.text = ui.nameText;
				this.licenseTypeJSON.text = ui.licenseTypeText;
				this.fileSizeJSON.text = ui.fileSizeText;
				this.notOnHubJSON.indicator = ui.notOnHubIndicator;
				this.alreadyHaveJSON.indicator = ui.alreadyHaveIndicator;
				this.alreadyHaveSceneJSON.indicator = ui.alreadyHaveSceneIndicator;
				this.updateAvailableJSON.indicator = ui.updateAvailableIndicator;
				this.updateMsgJSON.text = ui.updateMsgText;
				this.updateAction.button = ui.updateButton;
				this.downloadAction.button = ui.downloadButton;
				this.isDownloadQueuedJSON.indicator = ui.isDownloadQueuedIndicator;
				this.isDownloadingJSON.indicator = ui.isDownloadingIndicator;
				this.isDownloadedJSON.indicator = ui.isDownloadedIndicator;
				this.downloadProgressJSON.slider = ui.progressSlider;
				this.openInPackageManagerAction.button = ui.openInPackageManagerButton;
				this.openSceneAction.button = ui.openSceneButton;
			}
		}

		// Token: 0x0400500C RID: 20492
		protected HubBrowse browser;

		// Token: 0x0400500D RID: 20493
		private static readonly string[] SizeSuffixes = new string[]
		{
			"bytes",
			"KB",
			"MB",
			"GB",
			"TB",
			"PB",
			"EB",
			"ZB",
			"YB"
		};

		// Token: 0x0400500E RID: 20494
		protected string package_id;

		// Token: 0x0400500F RID: 20495
		protected string resource_id;

		// Token: 0x04005010 RID: 20496
		protected string resolvedVarName;

		// Token: 0x04005011 RID: 20497
		protected JSONStorableAction goToResourceAction;

		// Token: 0x04005012 RID: 20498
		protected JSONStorableBool isDependencyJSON;

		// Token: 0x04005017 RID: 20503
		protected JSONStorableString nameJSON;

		// Token: 0x04005018 RID: 20504
		protected JSONStorableString licenseTypeJSON;

		// Token: 0x04005019 RID: 20505
		protected JSONStorableString fileSizeJSON;

		// Token: 0x0400501B RID: 20507
		protected JSONStorableBool alreadyHaveJSON;

		// Token: 0x0400501C RID: 20508
		protected JSONStorableString updateMsgJSON;

		// Token: 0x0400501D RID: 20509
		protected JSONStorableBool updateAvailableJSON;

		// Token: 0x0400501E RID: 20510
		protected JSONStorableBool alreadyHaveSceneJSON;

		// Token: 0x0400501F RID: 20511
		protected string alreadyHaveScenePath;

		// Token: 0x04005020 RID: 20512
		protected JSONStorableBool notOnHubJSON;

		// Token: 0x04005021 RID: 20513
		public string promotionalUrl;

		// Token: 0x04005022 RID: 20514
		protected string downloadUrl;

		// Token: 0x04005023 RID: 20515
		protected string latestUrl;

		// Token: 0x04005024 RID: 20516
		protected JSONStorableFloat downloadProgressJSON;

		// Token: 0x04005025 RID: 20517
		protected JSONStorableBool isDownloadQueuedJSON;

		// Token: 0x04005026 RID: 20518
		protected JSONStorableBool isDownloadingJSON;

		// Token: 0x04005027 RID: 20519
		protected JSONStorableBool isDownloadedJSON;

		// Token: 0x04005028 RID: 20520
		protected JSONStorableAction downloadAction;

		// Token: 0x04005029 RID: 20521
		protected JSONStorableAction updateAction;

		// Token: 0x0400502A RID: 20522
		protected JSONStorableAction openInPackageManagerAction;

		// Token: 0x0400502B RID: 20523
		protected JSONStorableAction openSceneAction;
	}
}
