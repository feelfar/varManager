using System;
using System.Collections;
using System.Text.RegularExpressions;
using MVR.FileManagement;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

namespace MVR.Hub
{
	// Token: 0x02000C8F RID: 3215
	public class HubResourceItem
	{
		// Token: 0x06005F8F RID: 24463 RVA: 0x00240D10 File Offset: 0x0023F110
		public HubResourceItem(JSONClass resource, HubBrowse hubBrowse, bool queueImagesImmediate = false)
		{
			this.useQueueImmediate = queueImagesImmediate;
			this.browser = hubBrowse;
			this.resource_id = resource["resource_id"];
			this.discussion_thread_id = resource["discussion_thread_id"];
			string startingValue = resource["title"];
			string startingValue2 = resource["tag_line"];
			string startingValue3 = resource["version_string"];
			string startingValue4 = resource["category"];
			string startingValue5 = resource["type"];
			string startingValue6 = resource["username"];
			string text = resource["icon_url"];
			string text2 = resource["image_url"];
			bool asBool = resource["hubDownloadable"].AsBool;
			bool asBool2 = resource["hubHosted"].AsBool;
			int asInt = resource["dependency_count"].AsInt;
			bool startingValue7 = asInt > 0;
			string startingValue8 = resource["download_count"];
			string startingValue9 = resource["rating_count"];
			float asFloat = resource["rating_avg"].AsFloat;
			int asInt2 = resource["last_update"].AsInt;
			this.LastUpdateTimestamp = this.UnixTimeStampToDateTime(asInt2);
			string startingValue10;
			if ((DateTime.Now - this.LastUpdateTimestamp).TotalDays > 7.0)
			{
				startingValue10 = this.LastUpdateTimestamp.ToString("MMM d, yyyy");
			}
			else
			{
				startingValue10 = this.LastUpdateTimestamp.ToString("dddd h:mm tt");
			}
			this.varFilesJSONArray = resource["hubFiles"].AsArray;
			this.titleJSON = new JSONStorableString("title", startingValue);
			this.tagLineJSON = new JSONStorableString("tagLine", startingValue2);
			this.versionNumberJSON = new JSONStorableString("versionNumber", startingValue3);
			this.payTypeJSON = new JSONStorableString("payType", startingValue4);
			this.categoryJSON = new JSONStorableString("category", startingValue5);
			this.payTypeAndCategorySelectAction = new JSONStorableAction("PayTypeAndCategorySelect", new JSONStorableAction.ActionCallback(this.PayTypeAndCategorySelect));
			this.creatorJSON = new JSONStorableString("creator", startingValue6);
			this.creatorSelectAction = new JSONStorableAction("CreatorSelect", new JSONStorableAction.ActionCallback(this.CreatorSelect));
			this.creatorIconUrlJSON = new JSONStorableUrl("creatorIconUrl", text, new JSONStorableString.SetStringCallback(this.SyncCreatorIconUrl));
			this.SyncCreatorIconUrl(text);
			this.thumbnailUrlJSON = new JSONStorableUrl("thumbnailUrl", text2, new JSONStorableString.SetStringCallback(this.SyncThumbnailUrl));
			this.SyncThumbnailUrl(text2);
			this.hubDownloadableJSON = new JSONStorableBool("hubDownloadable", asBool);
			this.hubHostedJSON = new JSONStorableBool("hubHosted", asBool2);
			this.hasDependenciesJSON = new JSONStorableBool("hasDependencies", startingValue7);
			this.dependencyCountJSON = new JSONStorableString("dependencyCount", asInt.ToString() + " Hub-Hosted Dependencies");
			this.downloadCountJSON = new JSONStorableString("downloadCount", startingValue8);
			this.ratingsCountJSON = new JSONStorableString("ratingsCount", startingValue9);
			this.ratingJSON = new JSONStorableFloat("rating", asFloat, 0f, 5f, true, false);
			this.lastUpdateJSON = new JSONStorableString("lastUpdate", startingValue10);
			this.openDetailAction = new JSONStorableAction("OpenDetail", new JSONStorableAction.ActionCallback(this.OpenDetail));
			this.inLibraryJSON = new JSONStorableBool("inLibrary", false);
			this.updateAvailableJSON = new JSONStorableBool("updateAvailable", false);
			this.updateMsgJSON = new JSONStorableString("updateMsg", "Update Available");
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005F90 RID: 24464 RVA: 0x002410DA File Offset: 0x0023F4DA
		public string ResourceId
		{
			get
			{
				return this.resource_id;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06005F91 RID: 24465 RVA: 0x002410E2 File Offset: 0x0023F4E2
		public string Title
		{
			get
			{
				return this.titleJSON.val;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06005F92 RID: 24466 RVA: 0x002410EF File Offset: 0x0023F4EF
		public string TagLine
		{
			get
			{
				return this.tagLineJSON.val;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06005F93 RID: 24467 RVA: 0x002410FC File Offset: 0x0023F4FC
		public string VersionNumber
		{
			get
			{
				return this.versionNumberJSON.val;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06005F94 RID: 24468 RVA: 0x00241109 File Offset: 0x0023F509
		public string PayType
		{
			get
			{
				return this.payTypeJSON.val;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06005F95 RID: 24469 RVA: 0x00241116 File Offset: 0x0023F516
		public string Category
		{
			get
			{
				return this.categoryJSON.val;
			}
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x00241123 File Offset: 0x0023F523
		protected void PayTypeAndCategorySelect()
		{
			this.browser.SetPayTypeAndCategoryFilter(this.payTypeJSON.val, this.categoryJSON.val, true);
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06005F97 RID: 24471 RVA: 0x00241147 File Offset: 0x0023F547
		public string Creator
		{
			get
			{
				return this.creatorJSON.val;
			}
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x00241154 File Offset: 0x0023F554
		protected void CreatorSelect()
		{
			this.browser.CreatorFilterOnly = this.creatorJSON.val;
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x0024116C File Offset: 0x0023F56C
		protected void SyncCreatorIconTexture(ImageLoaderThreaded.QueuedImage qi)
		{
			this.creatorIconTexture = qi.tex;
			if (this.creatorIconImage != null && this.creatorIconTexture != null)
			{
				this.creatorIconImage.texture = this.creatorIconTexture;
			}
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x002411B8 File Offset: 0x0023F5B8
		protected void SyncCreatorIconUrl(string url)
		{
			if (ImageLoaderThreaded.singleton != null && url != null && url != string.Empty)
			{
				ImageLoaderThreaded.QueuedImage queuedImage = new ImageLoaderThreaded.QueuedImage();
				queuedImage.imgPath = url;
				queuedImage.callback = new ImageLoaderThreaded.ImageLoaderCallback(this.SyncCreatorIconTexture);
				this.creatorIconQueuedImage = queuedImage;
				if (this.useQueueImmediate)
				{
					ImageLoaderThreaded.singleton.QueueThumbnailImmediate(queuedImage);
				}
				else
				{
					ImageLoaderThreaded.singleton.QueueThumbnail(queuedImage);
				}
			}
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x00241238 File Offset: 0x0023F638
		protected void SyncThumbnailTexture(ImageLoaderThreaded.QueuedImage qi)
		{
			this.thumbnailTexture = qi.tex;
			if (this.thumbnailImage != null && this.thumbnailTexture != null)
			{
				this.thumbnailImage.texture = this.thumbnailTexture;
			}
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x00241284 File Offset: 0x0023F684
		protected void SyncThumbnailUrl(string url)
		{
			if (ImageLoaderThreaded.singleton != null && url != null && url != string.Empty)
			{
				ImageLoaderThreaded.QueuedImage queuedImage = new ImageLoaderThreaded.QueuedImage();
				queuedImage.imgPath = url;
				queuedImage.callback = new ImageLoaderThreaded.ImageLoaderCallback(this.SyncThumbnailTexture);
				this.thumbnailQueuedImage = queuedImage;
				if (this.useQueueImmediate)
				{
					ImageLoaderThreaded.singleton.QueueThumbnailImmediate(queuedImage);
				}
				else
				{
					ImageLoaderThreaded.singleton.QueueThumbnail(queuedImage);
				}
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06005F9D RID: 24477 RVA: 0x00241304 File Offset: 0x0023F704
		public int DownloadCount
		{
			get
			{
				int result;
				if (int.TryParse(this.downloadCountJSON.val, out result))
				{
					return result;
				}
				return 0;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06005F9E RID: 24478 RVA: 0x0024132C File Offset: 0x0023F72C
		public int RatingsCount
		{
			get
			{
				int result;
				if (int.TryParse(this.ratingsCountJSON.val, out result))
				{
					return result;
				}
				return 0;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06005F9F RID: 24479 RVA: 0x00241353 File Offset: 0x0023F753
		public float Rating
		{
			get
			{
				return this.ratingJSON.val;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06005FA0 RID: 24480 RVA: 0x00241360 File Offset: 0x0023F760
		// (set) Token: 0x06005FA1 RID: 24481 RVA: 0x00241368 File Offset: 0x0023F768
		public DateTime LastUpdateTimestamp { get; protected set; }

		// Token: 0x06005FA2 RID: 24482 RVA: 0x00241374 File Offset: 0x0023F774
		protected DateTime UnixTimeStampToDateTime(int unixTimeStamp)
		{
			DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			result = result.AddSeconds((double)unixTimeStamp).ToLocalTime();
			return result;
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x002413A8 File Offset: 0x0023F7A8
		public virtual void Refresh()
		{
			if (this.hubDownloadableJSON.val && this.varFilesJSONArray != null && this.varFilesJSONArray.Count > 0)
			{
				bool val = true;
				bool val2 = false;
				IEnumerator enumerator = this.varFilesJSONArray.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						JSONNode jsonnode = (JSONNode)obj;
						string text = jsonnode["filename"];
						if (text != null)
						{
							text = Regex.Replace(text, ".var$", string.Empty);
							string packageGroupUid = Regex.Replace(text, "(.*)\\..*", "$1");
							string s = Regex.Replace(text, ".*\\.([0-9]+)$", "$1");
							int num;
							if (int.TryParse(s, out num) && FileManager.GetPackage(text) == null)
							{
								VarPackageGroup packageGroup = FileManager.GetPackageGroup(packageGroupUid);
								if (packageGroup == null || packageGroup.NewestPackage == null)
								{
									val = false;
									break;
								}
								if (packageGroup.NewestPackage.Version < num)
								{
									val2 = true;
									this.updateMsgJSON.val = string.Concat(new object[]
									{
										"Update Available ",
										packageGroup.NewestEnabledPackage.Version,
										" -> ",
										num
									});
								}
							}
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				this.inLibraryJSON.val = val;
				this.updateAvailableJSON.val = val2;
			}
			else
			{
				this.inLibraryJSON.val = false;
				this.updateAvailableJSON.val = false;
			}
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x00241570 File Offset: 0x0023F970
		public void OpenDetail()
		{
			this.browser.OpenDetail(this.resource_id, false);
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x00241584 File Offset: 0x0023F984
		public void Hide()
		{
			if (this.creatorIconQueuedImage != null)
			{
				this.creatorIconQueuedImage.cancel = true;
			}
			if (this.thumbnailQueuedImage != null)
			{
				this.thumbnailQueuedImage.cancel = true;
			}
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x002415B4 File Offset: 0x0023F9B4
		public void Show()
		{
			if (this.creatorIconQueuedImage != null && !this.creatorIconQueuedImage.preprocessed)
			{
				this.creatorIconQueuedImage.cancel = false;
				if (this.useQueueImmediate)
				{
					ImageLoaderThreaded.singleton.QueueThumbnailImmediate(this.creatorIconQueuedImage);
				}
				else
				{
					ImageLoaderThreaded.singleton.QueueThumbnail(this.creatorIconQueuedImage);
				}
			}
			if (this.thumbnailQueuedImage != null && !this.thumbnailQueuedImage.preprocessed)
			{
				this.thumbnailQueuedImage.cancel = false;
				if (this.useQueueImmediate)
				{
					ImageLoaderThreaded.singleton.QueueThumbnailImmediate(this.thumbnailQueuedImage);
				}
				else
				{
					ImageLoaderThreaded.singleton.QueueThumbnail(this.thumbnailQueuedImage);
				}
			}
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x0024166F File Offset: 0x0023FA6F
		public void Destroy()
		{
			if (this.creatorIconQueuedImage != null)
			{
				this.creatorIconQueuedImage.cancel = true;
			}
			if (this.thumbnailQueuedImage != null)
			{
				this.thumbnailQueuedImage.cancel = true;
			}
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x002416A0 File Offset: 0x0023FAA0
		public virtual void RegisterUI(HubResourceItemUI ui)
		{
			if (ui != null)
			{
				ui.connectedItem = this;
				this.titleJSON.text = ui.titleText;
				this.tagLineJSON.text = ui.tagLineText;
				this.versionNumberJSON.text = ui.versionText;
				this.payTypeJSON.text = ui.payTypeText;
				this.categoryJSON.text = ui.categoryText;
				this.payTypeAndCategorySelectAction.button = ui.payTypeAndCategorySelectButton;
				this.creatorSelectAction.button = ui.creatorSelectButton;
				this.creatorJSON.text = ui.creatorText;
				this.creatorIconImage = ui.creatorIconImage;
				if (this.creatorIconImage != null && this.creatorIconTexture != null)
				{
					this.creatorIconImage.texture = this.creatorIconTexture;
				}
				this.thumbnailImage = ui.thumbnailImage;
				if (this.thumbnailImage != null && this.thumbnailTexture != null)
				{
					this.thumbnailImage.texture = this.thumbnailTexture;
				}
				this.hubDownloadableJSON.indicator = ui.hubDownloadableIndicator;
				this.hubDownloadableJSON.negativeIndicator = ui.hubDownloadableNegativeIndicator;
				this.hubHostedJSON.indicator = ui.hubHostedIndicator;
				this.hubHostedJSON.negativeIndicator = ui.hubHostedNegativeIndicator;
				this.hasDependenciesJSON.indicator = ui.hasDependenciesIndicator;
				this.hasDependenciesJSON.negativeIndicator = ui.hasDependenciesNegativeIndicator;
				this.dependencyCountJSON.text = ui.dependencyCountText;
				this.downloadCountJSON.text = ui.downloadCountText;
				this.ratingsCountJSON.text = ui.ratingsCountText;
				this.ratingJSON.slider = ui.ratingSlider;
				this.lastUpdateJSON.text = ui.lastUpdateText;
				this.openDetailAction.button = ui.openDetailButton;
				this.inLibraryJSON.indicator = ui.inLibraryIndicator;
				this.updateAvailableJSON.indicator = ui.updateAvailableIndicator;
				this.updateMsgJSON.text = ui.updateMsgText;
			}
		}

		// Token: 0x04004F96 RID: 20374
		protected HubBrowse browser;

		// Token: 0x04004F97 RID: 20375
		protected string resource_id;

		// Token: 0x04004F98 RID: 20376
		protected string discussion_thread_id;

		// Token: 0x04004F99 RID: 20377
		protected JSONStorableString titleJSON;

		// Token: 0x04004F9A RID: 20378
		protected JSONStorableString tagLineJSON;

		// Token: 0x04004F9B RID: 20379
		protected JSONStorableString versionNumberJSON;

		// Token: 0x04004F9C RID: 20380
		protected JSONStorableString payTypeJSON;

		// Token: 0x04004F9D RID: 20381
		protected JSONStorableString categoryJSON;

		// Token: 0x04004F9E RID: 20382
		protected JSONStorableAction payTypeAndCategorySelectAction;

		// Token: 0x04004F9F RID: 20383
		protected JSONStorableString creatorJSON;

		// Token: 0x04004FA0 RID: 20384
		protected JSONStorableAction creatorSelectAction;

		// Token: 0x04004FA1 RID: 20385
		protected RawImage creatorIconImage;

		// Token: 0x04004FA2 RID: 20386
		protected Texture2D creatorIconTexture;

		// Token: 0x04004FA3 RID: 20387
		protected bool useQueueImmediate;

		// Token: 0x04004FA4 RID: 20388
		protected ImageLoaderThreaded.QueuedImage creatorIconQueuedImage;

		// Token: 0x04004FA5 RID: 20389
		protected JSONStorableUrl creatorIconUrlJSON;

		// Token: 0x04004FA6 RID: 20390
		protected RawImage thumbnailImage;

		// Token: 0x04004FA7 RID: 20391
		protected Texture2D thumbnailTexture;

		// Token: 0x04004FA8 RID: 20392
		protected ImageLoaderThreaded.QueuedImage thumbnailQueuedImage;

		// Token: 0x04004FA9 RID: 20393
		protected JSONStorableUrl thumbnailUrlJSON;

		// Token: 0x04004FAA RID: 20394
		protected JSONStorableBool hubDownloadableJSON;

		// Token: 0x04004FAB RID: 20395
		protected JSONStorableBool hubHostedJSON;

		// Token: 0x04004FAC RID: 20396
		protected JSONStorableString dependencyCountJSON;

		// Token: 0x04004FAD RID: 20397
		protected JSONStorableBool hasDependenciesJSON;

		// Token: 0x04004FAE RID: 20398
		protected JSONStorableString downloadCountJSON;

		// Token: 0x04004FAF RID: 20399
		protected JSONStorableString ratingsCountJSON;

		// Token: 0x04004FB0 RID: 20400
		protected JSONStorableFloat ratingJSON;

		// Token: 0x04004FB1 RID: 20401
		protected JSONStorableString lastUpdateJSON;

		// Token: 0x04004FB3 RID: 20403
		protected JSONArray varFilesJSONArray;

		// Token: 0x04004FB4 RID: 20404
		protected JSONStorableBool inLibraryJSON;

		// Token: 0x04004FB5 RID: 20405
		protected JSONStorableBool updateAvailableJSON;

		// Token: 0x04004FB6 RID: 20406
		protected JSONStorableString updateMsgJSON;

		// Token: 0x04004FB7 RID: 20407
		protected JSONStorableAction openDetailAction;
	}
}
