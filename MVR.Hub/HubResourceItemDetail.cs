using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace MVR.Hub
{
	// Token: 0x02000C90 RID: 3216
	public class HubResourceItemDetail : HubResourceItem
	{
		// Token: 0x06005FA9 RID: 24489 RVA: 0x002418C8 File Offset: 0x0023FCC8
		public HubResourceItemDetail(JSONClass resource, HubBrowse hubBrowse) : base(resource, hubBrowse, true)
		{
			if (this.resource_id != null)
			{
				this.resourceOverviewUrl = "https://hub.virtamate.com/resources/" + this.resource_id + "/overview-panel";
				this.resourceUpdatesUrl = "https://hub.virtamate.com/resources/" + this.resource_id + "/updates-panel";
				this.resourceReviewsUrl = "https://hub.virtamate.com/resources/" + this.resource_id + "/review-panel";
				this.resourceHistoryUrl = "https://hub.virtamate.com/resources/" + this.resource_id + "/history-panel";
			}
			if (this.discussion_thread_id != null)
			{
				this.resourceDiscussionUrl = "https://hub.virtamate.com/threads/" + this.discussion_thread_id + "/discussion-panel";
			}
			bool flag = false;
			string startingValue = string.Empty;
			string text = resource["status"];
			if (text != null && text == "error")
			{
				flag = true;
				startingValue = resource["error"];
			}
			string startingValue2 = resource["download_url"];
			this.promotionalUrl = resource["promotional_link"];
			this.dependencies = resource["dependencies"].AsObject;
			int asInt = resource["review_count"].AsInt;
			bool startingValue3 = asInt > 0;
			int asInt2 = resource["update_count"].AsInt;
			bool startingValue4 = asInt2 > 0;
			if (flag)
			{
				this.browser.NavigateWebPanel("about:blank");
			}
			else
			{
				this.NavigateToOverview();
			}
			this.hadErrorJSON = new JSONStorableBool("hadError", flag);
			this.errorJSON = new JSONStorableString("error", startingValue);
			this.closeDetailAction = new JSONStorableAction("CloseDetail", new JSONStorableAction.ActionCallback(this.CloseDetail));
			this.navigateToOverviewAction = new JSONStorableAction("NavigateToOverview", new JSONStorableAction.ActionCallback(this.NavigateToOverview));
			this.navigateToUpdatesAction = new JSONStorableAction("NavigateToUpdates", new JSONStorableAction.ActionCallback(this.NavigateToUpdates));
			this.hasUpdatesJSON = new JSONStorableBool("hasUpdates", startingValue4);
			this.updatesTextJSON = new JSONStorableString("updatesText", "Updates (" + asInt2 + ")");
			this.navigateToReviewsAction = new JSONStorableAction("NavigateToReviews", new JSONStorableAction.ActionCallback(this.NavigateToReviews));
			this.hasReviewsJSON = new JSONStorableBool("hasReviews", startingValue3);
			this.reviewsTextJSON = new JSONStorableString("reviewsText", "Reviews (" + asInt + ")");
			this.navigateToHistoryAction = new JSONStorableAction("NavigateToHistory", new JSONStorableAction.ActionCallback(this.NavigateToHistory));
			this.navigateToDiscussionAction = new JSONStorableAction("NavigateToDiscussion", new JSONStorableAction.ActionCallback(this.NavigateToDiscussion));
			this.hasPromotionalLinkJSON = new JSONStorableBool("hasPromotionalLink", this.promotionalUrl != null && this.promotionalUrl != string.Empty && this.promotionalUrl != "null");
			this.navigateToPromotionalLinkAction = new JSONStorableAction("NavigateToPromotionalLink", new JSONStorableAction.ActionCallback(this.NavigateToPromotionalLink));
			this.promotionalLinkTextJSON = new JSONStorableString("promotionalLinkText", base.Creator);
			this.hasOtherCreatorsJSON = new JSONStorableBool("hasOtherCreators", false);
			this.externalDownloadUrl = new JSONStorableString("externalDownloadUrl", startingValue2);
			this.goToExternalDownloadAction = new JSONStorableAction("GoToExternalDownload", new JSONStorableAction.ActionCallback(this.GoToExternalDownload));
			this.downloadAllAction = new JSONStorableAction("DownloadAll", new JSONStorableAction.ActionCallback(this.DownloadAll));
			this.downloadAvailableJSON = new JSONStorableBool("downloadAvailable", false);
			this.downloadPackages = new List<HubResourcePackage>();
		}

		// Token: 0x06005FAA RID: 24490 RVA: 0x00241C72 File Offset: 0x00240072
		public void CloseDetail()
		{
			this.browser.CloseDetail(this.resource_id);
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x00241C88 File Offset: 0x00240088
		public override void Refresh()
		{
			base.Refresh();
			if (this.downloadPackages != null)
			{
				foreach (HubResourcePackage hubResourcePackage in this.downloadPackages)
				{
					hubResourcePackage.Refresh();
				}
			}
			this.SyncDownloadAvailable();
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x00241CFC File Offset: 0x002400FC
		public void NavigateToOverview()
		{
			if (this.resourceOverviewUrl != null)
			{
				this.browser.NavigateWebPanel(this.resourceOverviewUrl);
			}
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x00241D1A File Offset: 0x0024011A
		public void NavigateToUpdates()
		{
			if (this.resourceUpdatesUrl != null)
			{
				this.browser.NavigateWebPanel(this.resourceUpdatesUrl);
			}
		}

		// Token: 0x06005FAE RID: 24494 RVA: 0x00241D38 File Offset: 0x00240138
		public void NavigateToReviews()
		{
			if (this.resourceReviewsUrl != null)
			{
				this.browser.NavigateWebPanel(this.resourceReviewsUrl);
			}
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x00241D56 File Offset: 0x00240156
		public void NavigateToHistory()
		{
			if (this.resourceHistoryUrl != null)
			{
				this.browser.NavigateWebPanel(this.resourceHistoryUrl);
			}
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x00241D74 File Offset: 0x00240174
		public void NavigateToDiscussion()
		{
			if (this.resourceDiscussionUrl != null)
			{
				this.browser.NavigateWebPanel(this.resourceDiscussionUrl);
			}
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x00241D92 File Offset: 0x00240192
		public void NavigateToPromotionalLink()
		{
			if (this.promotionalUrl != null)
			{
				this.browser.NavigateWebPanel(this.promotionalUrl);
			}
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x00241DB0 File Offset: 0x002401B0
		protected void GoToExternalDownload()
		{
			if (this.externalDownloadUrl != null && this.externalDownloadUrl.val != null)
			{
				this.browser.NavigateWebPanel(this.externalDownloadUrl.val);
			}
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x00241DE4 File Offset: 0x002401E4
		public void DownloadAll()
		{
			foreach (HubResourcePackage hubResourcePackage in this.downloadPackages)
			{
				hubResourcePackage.Download();
			}
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x00241E40 File Offset: 0x00240240
		protected void SyncDownloadAvailable()
		{
			bool val = false;
			if (this.downloadPackages != null)
			{
				foreach (HubResourcePackage hubResourcePackage in this.downloadPackages)
				{
					if (hubResourcePackage.NeedsDownload)
					{
						val = true;
					}
				}
			}
			this.downloadAvailableJSON.val = val;
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06005FB5 RID: 24501 RVA: 0x00241EBC File Offset: 0x002402BC
		public bool IsDownloading
		{
			get
			{
				if (this.downloadPackages != null)
				{
					foreach (HubResourcePackage hubResourcePackage in this.downloadPackages)
					{
						if (hubResourcePackage.IsDownloading || hubResourcePackage.IsDownloadQueued)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x00241F3C File Offset: 0x0024033C
		public void RegisterUI(HubResourceItemDetailUI ui)
		{
			base.RegisterUI(ui);
			if (ui != null)
			{
				ui.connectedItem = this;
				this.hadErrorJSON.indicator = ui.hadErrorIndicator;
				this.errorJSON.text = ui.errorText;
				this.closeDetailAction.button = ui.closeDetailButton;
				this.closeDetailAction.buttonAlt = ui.closeDetailButtonAlt;
				this.navigateToOverviewAction.button = ui.navigateToOverviewButton;
				this.navigateToUpdatesAction.button = ui.navigateToUpdatesButton;
				this.hasUpdatesJSON.indicator = ui.hasUpdatesIndicator;
				this.updatesTextJSON.text = ui.updatesText;
				this.navigateToReviewsAction.button = ui.navigateToReviewsButton;
				this.hasReviewsJSON.indicator = ui.hasReviewsIndicator;
				this.reviewsTextJSON.text = ui.reviewsText;
				this.navigateToHistoryAction.button = ui.navigateToHistoryButton;
				this.navigateToDiscussionAction.button = ui.navigateToDiscussionButton;
				this.hasPromotionalLinkJSON.indicator = ui.hasPromotionalLinkIndicator;
				this.navigateToPromotionalLinkAction.button = ui.navigateToPromotionalLinkButton;
				this.promotionalLinkTextJSON.text = ui.promotionalLinkText;
				this.hubDownloadableJSON.indicatorAlt = ui.hubDownloadableIndicatorAlt;
				this.hubDownloadableJSON.negativeIndicatorAlt = ui.hubDownloadableNegativeIndicatorAlt;
				this.externalDownloadUrl.text = ui.externalDownloadUrl;
				this.goToExternalDownloadAction.button = ui.goToExternalDownloadUrlButton;
				this.downloadAllAction.button = ui.downloadAllButton;
				this.downloadAvailableJSON.indicator = ui.downloadAvailableIndicator;
				this.hasOtherCreatorsJSON.indicator = ui.hasOtherCreatorsIndicator;
				if (this.hasPromotionalLinkJSON.val && ui.promtionalLinkButtonEnterExitAction != null)
				{
					ui.promtionalLinkButtonEnterExitAction.onEnterActions = delegate()
					{
						this.browser.ShowHoverUrl(this.promotionalUrl);
					};
					ui.promtionalLinkButtonEnterExitAction.onExitActions = delegate()
					{
						this.browser.ShowHoverUrl(string.Empty);
					};
				}
				this.packageContent = ui.packageContent;
				this.creatorSupportContent = ui.creatorSupportContent;
				if (this.packageContent != null)
				{
					IEnumerator enumerator = this.varFilesJSONArray.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							JSONNode jsonnode = (JSONNode)obj;
							JSONClass asObject = jsonnode.AsObject;
							if (asObject != null)
							{
								HubResourcePackage hubResourcePackage = new HubResourcePackage(asObject, this.browser, false);
								hubResourcePackage.promotionalUrl = this.promotionalUrl;
								this.downloadPackages.Add(hubResourcePackage);
								RectTransform rectTransform = this.browser.CreateDownloadPrefabInstance();
								rectTransform.SetParent(this.packageContent, false);
								HubResourcePackageUI component = rectTransform.GetComponent<HubResourcePackageUI>();
								if (component != null)
								{
									hubResourcePackage.RegisterUI(component);
								}
								if (this.dependencies != null)
								{
									HashSet<string> hashSet = new HashSet<string>();
									hashSet.Add(hubResourcePackage.Creator);
									JSONArray asArray = this.dependencies[hubResourcePackage.GroupName].AsArray;
									if (asArray != null)
									{
										IEnumerator enumerator2 = asArray.GetEnumerator();
										try
										{
											while (enumerator2.MoveNext())
											{
												object obj2 = enumerator2.Current;
												JSONNode jsonnode2 = (JSONNode)obj2;
												JSONClass asObject2 = jsonnode2.AsObject;
												if (asObject2 != null)
												{
													HubResourcePackage dhrp = new HubResourcePackage(asObject2, this.browser, true);
													this.downloadPackages.Add(dhrp);
													RectTransform rectTransform2 = this.browser.CreateDownloadPrefabInstance();
													if (rectTransform2 != null)
													{
														rectTransform2.SetParent(this.packageContent, false);
														HubResourcePackageUI component2 = rectTransform2.GetComponent<HubResourcePackageUI>();
														if (component2 != null)
														{
															dhrp.RegisterUI(component2);
														}
													}
													if (this.creatorSupportContent != null && dhrp.promotionalUrl != null && dhrp.promotionalUrl != string.Empty && dhrp.promotionalUrl != "null" && !hashSet.Contains(dhrp.Creator))
													{
														this.hasOtherCreatorsJSON.val = true;
														hashSet.Add(dhrp.Creator);
														RectTransform rectTransform3 = this.browser.CreateCreatorSupportButtonPrefabInstance();
														if (rectTransform3 != null)
														{
															rectTransform3.SetParent(this.creatorSupportContent, false);
															HubResourceCreatorSupportUI component3 = rectTransform3.GetComponent<HubResourceCreatorSupportUI>();
															if (component3 != null)
															{
																if (component3.linkButton != null)
																{
																	component3.linkButton.onClick.AddListener(delegate()
																	{
																		this.browser.NavigateWebPanel(dhrp.promotionalUrl);
																	});
																}
																if (component3.creatorNameText != null)
																{
																	component3.creatorNameText.text = dhrp.Creator;
																}
																if (component3.pointerEnterExitAction != null)
																{
																	component3.pointerEnterExitAction.onEnterActions = delegate()
																	{
																		this.browser.ShowHoverUrl(dhrp.promotionalUrl);
																	};
																	component3.pointerEnterExitAction.onExitActions = delegate()
																	{
																		this.browser.ShowHoverUrl(string.Empty);
																	};
																}
															}
														}
													}
												}
											}
										}
										finally
										{
											IDisposable disposable;
											if ((disposable = (enumerator2 as IDisposable)) != null)
											{
												disposable.Dispose();
											}
										}
									}
								}
							}
						}
					}
					finally
					{
						IDisposable disposable2;
						if ((disposable2 = (enumerator as IDisposable)) != null)
						{
							disposable2.Dispose();
						}
					}
					this.SyncDownloadAvailable();
				}
			}
		}

		// Token: 0x04004FB8 RID: 20408
		protected JSONStorableBool hadErrorJSON;

		// Token: 0x04004FB9 RID: 20409
		protected JSONStorableString errorJSON;

		// Token: 0x04004FBA RID: 20410
		protected JSONStorableAction closeDetailAction;

		// Token: 0x04004FBB RID: 20411
		protected List<HubResourcePackage> downloadPackages;

		// Token: 0x04004FBC RID: 20412
		protected string resourceOverviewUrl;

		// Token: 0x04004FBD RID: 20413
		protected JSONStorableAction navigateToOverviewAction;

		// Token: 0x04004FBE RID: 20414
		protected string resourceUpdatesUrl;

		// Token: 0x04004FBF RID: 20415
		protected JSONStorableAction navigateToUpdatesAction;

		// Token: 0x04004FC0 RID: 20416
		protected JSONStorableBool hasUpdatesJSON;

		// Token: 0x04004FC1 RID: 20417
		protected JSONStorableString updatesTextJSON;

		// Token: 0x04004FC2 RID: 20418
		protected string resourceReviewsUrl;

		// Token: 0x04004FC3 RID: 20419
		protected JSONStorableAction navigateToReviewsAction;

		// Token: 0x04004FC4 RID: 20420
		protected JSONStorableBool hasReviewsJSON;

		// Token: 0x04004FC5 RID: 20421
		protected JSONStorableString reviewsTextJSON;

		// Token: 0x04004FC6 RID: 20422
		protected string resourceHistoryUrl;

		// Token: 0x04004FC7 RID: 20423
		protected JSONStorableAction navigateToHistoryAction;

		// Token: 0x04004FC8 RID: 20424
		protected string resourceDiscussionUrl;

		// Token: 0x04004FC9 RID: 20425
		protected JSONStorableAction navigateToDiscussionAction;

		// Token: 0x04004FCA RID: 20426
		protected JSONStorableBool hasPromotionalLinkJSON;

		// Token: 0x04004FCB RID: 20427
		protected string promotionalUrl;

		// Token: 0x04004FCC RID: 20428
		protected JSONStorableAction navigateToPromotionalLinkAction;

		// Token: 0x04004FCD RID: 20429
		protected JSONStorableString promotionalLinkTextJSON;

		// Token: 0x04004FCE RID: 20430
		protected JSONStorableString externalDownloadUrl;

		// Token: 0x04004FCF RID: 20431
		protected JSONStorableAction goToExternalDownloadAction;

		// Token: 0x04004FD0 RID: 20432
		protected RectTransform packagePrefab;

		// Token: 0x04004FD1 RID: 20433
		protected RectTransform packageContent;

		// Token: 0x04004FD2 RID: 20434
		protected RectTransform creatorSupportContent;

		// Token: 0x04004FD3 RID: 20435
		protected JSONStorableBool hasOtherCreatorsJSON;

		// Token: 0x04004FD4 RID: 20436
		protected JSONClass dependencies;

		// Token: 0x04004FD5 RID: 20437
		protected JSONStorableAction downloadAllAction;

		// Token: 0x04004FD6 RID: 20438
		protected JSONStorableBool downloadAvailableJSON;
	}
}
