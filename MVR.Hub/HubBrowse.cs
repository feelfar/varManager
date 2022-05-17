using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MVR.FileManagement;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

namespace MVR.Hub
{
	// Token: 0x02000C83 RID: 3203
	public class HubBrowse : JSONStorable
	{
		// Token: 0x06005F12 RID: 24338 RVA: 0x0023CFAC File Offset: 0x0023B3AC
		private IEnumerator GetRequest(string uri, HubBrowse.RequestSuccessCallback callback, HubBrowse.RequestErrorCallback errorCallback)
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
			{
				webRequest.SendWebRequest();
				while (!webRequest.isDone)
				{
					yield return null;
				}
				if (webRequest.isNetworkError)
				{
					Debug.LogError(uri + ": Error: " + webRequest.error);
					if (errorCallback != null)
					{
						errorCallback(webRequest.error);
					}
				}
				else
				{
					SimpleJSON.JSONNode jsonNode = JSON.Parse(webRequest.downloadHandler.text);
					if (callback != null)
					{
						callback(jsonNode);
					}
				}
			}
			yield break;
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x0023CFD8 File Offset: 0x0023B3D8
		private IEnumerator BinaryGetRequest(string uri, HubBrowse.BinaryRequestStartedCallback startedCallback, HubBrowse.BinaryRequestSuccessCallback successCallback, HubBrowse.RequestErrorCallback errorCallback, HubBrowse.RequestProgressCallback progressCallback, List<string> cookies = null)
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
			{
				string cookieHeader = "vamhubconsent=1";
				if (cookies != null)
				{
					foreach (string str in cookies)
					{
						cookieHeader = cookieHeader + ";" + str;
					}
				}
				webRequest.SetRequestHeader("Cookie", cookieHeader);
				webRequest.SendWebRequest();
				if (startedCallback != null)
				{
					startedCallback();
				}
				while (!webRequest.isDone)
				{
					if (progressCallback != null)
					{
						progressCallback(webRequest.downloadProgress);
					}
					yield return null;
				}
				if (webRequest.isNetworkError)
				{
					Debug.LogError(uri + ": Error: " + webRequest.error);
					if (errorCallback != null)
					{
						errorCallback(webRequest.error);
					}
				}
				else
				{
					Dictionary<string, string> responseHeaders = webRequest.GetResponseHeaders();
					if (successCallback != null)
					{
						successCallback(webRequest.downloadHandler.data, responseHeaders);
					}
				}
			}
			yield break;
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x0023D01C File Offset: 0x0023B41C
		private IEnumerator PostRequest(string uri, string postData, HubBrowse.RequestSuccessCallback callback, HubBrowse.RequestErrorCallback errorCallback)
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, postData))
			{
				webRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(postData));
				webRequest.SetRequestHeader("Content-Type", "application/json");
				webRequest.SetRequestHeader("Accept", "application/json");
				yield return webRequest.SendWebRequest();
				string[] pages = uri.Split(new char[]
				{
					'/'
				});
				int page = pages.Length - 1;
				if (webRequest.isNetworkError)
				{
					Debug.LogError(pages[page] + ": Error: " + webRequest.error);
					if (errorCallback != null)
					{
						errorCallback(webRequest.error);
					}
				}
				else
				{
					SimpleJSON.JSONNode jsonnode = JSON.Parse(webRequest.downloadHandler.text);
					if (jsonnode == null)
					{
						string text = "Error - Invalid JSON response: " + webRequest.downloadHandler.text;
						Debug.LogError(pages[page] + ": " + text);
						if (errorCallback != null)
						{
							errorCallback(text);
						}
					}
					else if (callback != null)
					{
						callback(jsonnode);
					}
				}
			}
			yield break;
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x0023D04D File Offset: 0x0023B44D
		protected void SyncHubEnabled(bool b)
		{
			this._hubEnabled = b;
			if (this._hubEnabled)
			{
				this.GetHubInfo();
				if (this._isShowing)
				{
					this.RefreshResources();
				}
			}
		}

		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06005F16 RID: 24342 RVA: 0x0023D078 File Offset: 0x0023B478
		// (set) Token: 0x06005F17 RID: 24343 RVA: 0x0023D080 File Offset: 0x0023B480
		public bool HubEnabled
		{
			get
			{
				return this._hubEnabled;
			}
			set
			{
				if (this.hubEnabledJSON != null)
				{
					this.hubEnabledJSON.val = value;
				}
				else
				{
					this._hubEnabled = value;
				}
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x0023D0A5 File Offset: 0x0023B4A5
		protected void EnableHub()
		{
			if (this.enableHubCallbacks != null)
			{
				this.enableHubCallbacks();
			}
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x0023D0C0 File Offset: 0x0023B4C0
		protected void SyncWebBrowserEnabled(bool b)
		{
			this._webBrowserEnabled = b;
			if (this._webBrowserEnabled && this.resourceDetailStack != null && this.resourceDetailStack.Count > 0)
			{
				HubResourceItemDetailUI hubResourceItemDetailUI = this.resourceDetailStack.Peek();
				if (hubResourceItemDetailUI.connectedItem != null)
				{
					hubResourceItemDetailUI.connectedItem.NavigateToOverview();
				}
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06005F1A RID: 24346 RVA: 0x0023D11D File Offset: 0x0023B51D
		// (set) Token: 0x06005F1B RID: 24347 RVA: 0x0023D125 File Offset: 0x0023B525
		public bool WebBrowserEnabled
		{
			get
			{
				return this._webBrowserEnabled;
			}
			set
			{
				if (this.webBrowserEnabledJSON != null)
				{
					this.webBrowserEnabledJSON.val = value;
				}
				else
				{
					this._webBrowserEnabled = value;
				}
			}
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x0023D14A File Offset: 0x0023B54A
		protected void EnableWebBrowser()
		{
			if (this.enableWebBrowserCallbacks != null)
			{
				this.enableWebBrowserCallbacks();
			}
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x0023D164 File Offset: 0x0023B564
		public void Show()
		{
			if (this.preShowCallbacks != null)
			{
				this.preShowCallbacks();
			}
			this._isShowing = true;
			if (this.hubBrowseUI != null)
			{
				this.hubBrowseUI.gameObject.SetActive(true);
			}
			else if (this.UITransform != null)
			{
				this.UITransform.gameObject.SetActive(true);
			}
			if (this._hubEnabled)
			{
				if (this._hasBeenRefreshed)
				{
					if (this.items != null)
					{
						foreach (HubResourceItemUI hubResourceItemUI in this.items)
						{
							if (hubResourceItemUI.connectedItem != null)
							{
								hubResourceItemUI.connectedItem.Show();
							}
						}
					}
				}
				else
				{
					this.RefreshResources();
				}
			}
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x0023D264 File Offset: 0x0023B664
		public void Hide()
		{
			this._isShowing = false;
			if (this.hubBrowseUI != null)
			{
				this.hubBrowseUI.gameObject.SetActive(false);
			}
			if (this.items != null)
			{
				foreach (HubResourceItemUI hubResourceItemUI in this.items)
				{
					if (hubResourceItemUI.connectedItem != null)
					{
						hubResourceItemUI.connectedItem.Hide();
					}
				}
			}
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x0023D304 File Offset: 0x0023B704
		protected void RefreshErrorCallback(string err)
		{
			if (this.refreshIndicator != null)
			{
				this.refreshIndicator.SetActive(false);
			}
			SuperController.LogError("Error during hub request " + err);
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x0023D334 File Offset: 0x0023B734
		protected void RefreshCallback(SimpleJSON.JSONNode jsonNode)
		{
			if (this.refreshIndicator != null)
			{
				this.refreshIndicator.SetActive(false);
			}
			if (jsonNode != null)
			{
				JSONClass asObject = jsonNode.AsObject;
				if (asObject != null)
				{
					string a = asObject["status"];
					if (a == "success")
					{
						JSONClass asObject2 = asObject["pagination"].AsObject;
						if (asObject2 != null)
						{
							this.numResourcesJSON.val = "Total: " + asObject2["total_found"];
							this.numPagesJSON.val = asObject2["total_pages"];
							if (this.items != null)
							{
								foreach (HubResourceItemUI hubResourceItemUI in this.items)
								{
									if (hubResourceItemUI.connectedItem != null)
									{
										hubResourceItemUI.connectedItem.Destroy();
									}
									UnityEngine.Object.Destroy(hubResourceItemUI.gameObject);
								}
								this.items.Clear();
							}
							else
							{
								this.items = new List<HubResourceItemUI>();
							}
							if (this.itemScrollRect != null)
							{
								this.itemScrollRect.verticalNormalizedPosition = 1f;
							}
							JSONArray asArray = asObject["resources"].AsArray;
							if (this.itemContainer != null && this.itemPrefab != null && asArray != null)
							{
								IEnumerator enumerator2 = asArray.GetEnumerator();
								try
								{
									while (enumerator2.MoveNext())
									{
										object obj = enumerator2.Current;
										JSONClass resource = (JSONClass)obj;
										HubResourceItem hubResourceItem = new HubResourceItem(resource, this, false);
										hubResourceItem.Refresh();
										RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.itemPrefab);
										rectTransform.SetParent(this.itemContainer, false);
										HubResourceItemUI component = rectTransform.GetComponent<HubResourceItemUI>();
										if (component != null)
										{
											hubResourceItem.RegisterUI(component);
											this.items.Add(component);
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
					else
					{
						string str = jsonNode["error"];
						SuperController.LogError("Refresh returned error " + str);
					}
				}
			}
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x0023D5C0 File Offset: 0x0023B9C0
		public void RefreshResources()
		{
			this._hasBeenRefreshed = true;
			if (this._hubEnabled)
			{
				if (this.refreshResourcesRoutine != null)
				{
					base.StopCoroutine(this.refreshResourcesRoutine);
				}
				JSONClass jsonclass = new JSONClass();
				jsonclass["source"] = "VaM";
				jsonclass["action"] = "getResources";
				jsonclass["latest_image"] = "Y";
				jsonclass["perpage"] = this._numPerPageInt.ToString();
				jsonclass["page"] = this._currentPageString;
				if (this._hostedOption != "All")
				{
					jsonclass["location"] = this._hostedOption;
				}
				if (this._searchFilter != string.Empty)
				{
					jsonclass["search"] = this._searchFilter;
					jsonclass["searchall"] = "true";
				}
				if (this._payTypeFilter != "All")
				{
					jsonclass["category"] = this._payTypeFilter;
				}
				if (this._categoryFilter != "All")
				{
					jsonclass["type"] = this._categoryFilter;
				}
				if (this._creatorFilter != "All")
				{
					jsonclass["username"] = this._creatorFilter;
				}
				if (this._tagsFilter != "All")
				{
					jsonclass["tags"] = this._tagsFilter;
				}
				string text = this._sortPrimary;
				if (this._sortSecondary != null && this._sortSecondary != string.Empty && this._sortSecondary != "None")
				{
					text = text + "," + this._sortSecondary;
				}
				jsonclass["sort"] = text;
				string postData = jsonclass.ToString();
				this.refreshResourcesRoutine = base.StartCoroutine(this.PostRequest(this.apiUrl, postData, new HubBrowse.RequestSuccessCallback(this.RefreshCallback), new HubBrowse.RequestErrorCallback(this.RefreshErrorCallback)));
				if (this.refreshIndicator != null)
				{
					this.refreshIndicator.SetActive(true);
				}
			}
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x0023D83D File Offset: 0x0023BC3D
		protected void SyncNumResources(string s)
		{
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x0023D83F File Offset: 0x0023BC3F
		protected void SetPageInfo()
		{
			this.pageInfoJSON.val = "Page " + this.currentPageJSON.val + " of " + this.numPagesJSON.val;
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x0023D874 File Offset: 0x0023BC74
		protected void SyncNumPages(string s)
		{
			int numPagesInt;
			if (int.TryParse(s, out numPagesInt))
			{
				this._numPagesInt = numPagesInt;
			}
			this.SetPageInfo();
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x0023D89B File Offset: 0x0023BC9B
		protected void SyncNumPerPage(float f)
		{
			this._numPerPageInt = (int)f;
			this.ResetRefresh();
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x0023D8AB File Offset: 0x0023BCAB
		protected void ResetRefresh()
		{
			this._currentPageString = "1";
			this._currentPageInt = 1;
			this.currentPageJSON.valNoCallback = this._currentPageString;
			this.SetPageInfo();
			this.RefreshResources();
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x0023D8DC File Offset: 0x0023BCDC
		protected void SyncCurrentPage(string s)
		{
			this._currentPageString = s;
			int currentPageInt;
			if (int.TryParse(s, out currentPageInt))
			{
				this._currentPageInt = currentPageInt;
			}
			this.SetPageInfo();
			this.RefreshResources();
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x0023D910 File Offset: 0x0023BD10
		protected void FirstPage()
		{
			this.currentPageJSON.val = "1";
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0023D924 File Offset: 0x0023BD24
		protected void PreviousPage()
		{
			if (this._currentPageInt > 1)
			{
				this.currentPageJSON.val = (this._currentPageInt - 1).ToString();
			}
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x0023D960 File Offset: 0x0023BD60
		protected void NextPage()
		{
			if (this._currentPageInt < this._numPagesInt)
			{
				this.currentPageJSON.val = (this._currentPageInt + 1).ToString();
			}
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x0023D9A0 File Offset: 0x0023BDA0
		protected void ResetFilters()
		{
			this._payTypeFilter = "All";
			this.payTypeFilterChooser.valNoCallback = "All";
			this._searchFilter = string.Empty;
			this.searchFilterJSON.valNoCallback = string.Empty;
			this._categoryFilter = "All";
			this.categoryFilterChooser.valNoCallback = "All";
			this._creatorFilter = "All";
			this.creatorFilterChooser.valNoCallback = "All";
			this._tagsFilter = "All";
			this.tagsFilterChooser.valNoCallback = "All";
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x0023DA34 File Offset: 0x0023BE34
		protected void ResetFiltersAndRefresh()
		{
			this.ResetFilters();
			this.ResetRefresh();
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x0023DA42 File Offset: 0x0023BE42
		protected void SyncHostedOption(string s)
		{
			this._hostedOption = s;
			this.ResetRefresh();
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06005F2E RID: 24366 RVA: 0x0023DA51 File Offset: 0x0023BE51
		// (set) Token: 0x06005F2F RID: 24367 RVA: 0x0023DA59 File Offset: 0x0023BE59
		public string HostedOption
		{
			get
			{
				return this._hostedOption;
			}
			set
			{
				this.hostedOptionChooser.val = value;
			}
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x0023DA68 File Offset: 0x0023BE68
		protected void SyncPayTypeFilter(string s)
		{
			this._payTypeFilter = s;
			if (this._payTypeFilter != "Free" && this._hostedOption != "All")
			{
				this.hostedOptionChooser.val = "All";
			}
			else
			{
				this.ResetRefresh();
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06005F31 RID: 24369 RVA: 0x0023DAC1 File Offset: 0x0023BEC1
		// (set) Token: 0x06005F32 RID: 24370 RVA: 0x0023DAC9 File Offset: 0x0023BEC9
		public string PayTypeFilter
		{
			get
			{
				return this._payTypeFilter;
			}
			set
			{
				this.payTypeFilterChooser.val = value;
			}
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x0023DAD8 File Offset: 0x0023BED8
		protected IEnumerator TriggerResetRefesh()
		{
			while (this.triggerCountdown > 0f)
			{
				this.triggerCountdown -= Time.unscaledDeltaTime;
				yield return null;
			}
			this.triggerResetRefreshRoutine = null;
			this.ResetRefresh();
			yield break;
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x0023DAF4 File Offset: 0x0023BEF4
		protected void SyncSearchFilter(string s)
		{
			this._searchFilter = s;
			bool flag = false;
			if (this._searchFilter.Length > 2)
			{
				if (this._minLengthSearchFilter != this._searchFilter)
				{
					this._minLengthSearchFilter = this._searchFilter;
					flag = true;
				}
			}
			else if (this._minLengthSearchFilter != string.Empty)
			{
				this._minLengthSearchFilter = string.Empty;
				flag = true;
			}
			if (flag)
			{
				this.triggerCountdown = 0.5f;
				if (this.triggerResetRefreshRoutine == null)
				{
					this.triggerResetRefreshRoutine = base.StartCoroutine(this.TriggerResetRefesh());
				}
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06005F35 RID: 24373 RVA: 0x0023DB94 File Offset: 0x0023BF94
		// (set) Token: 0x06005F36 RID: 24374 RVA: 0x0023DB9C File Offset: 0x0023BF9C
		public string SearchFilter
		{
			get
			{
				return this._searchFilter;
			}
			set
			{
				this.searchFilterJSON.val = value;
			}
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x0023DBAA File Offset: 0x0023BFAA
		protected void SyncCategoryFilter(string s)
		{
			this._categoryFilter = s;
			this.ResetRefresh();
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06005F38 RID: 24376 RVA: 0x0023DBB9 File Offset: 0x0023BFB9
		// (set) Token: 0x06005F39 RID: 24377 RVA: 0x0023DBC1 File Offset: 0x0023BFC1
		public string CategoryFilter
		{
			get
			{
				return this._categoryFilter;
			}
			set
			{
				this.categoryFilterChooser.val = value;
			}
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x0023DBCF File Offset: 0x0023BFCF
		public void SetPayTypeAndCategoryFilter(string payType, string category, bool onlyTheseFilters = true)
		{
			if (onlyTheseFilters)
			{
				this.CloseAllDetails();
				this.ResetFilters();
			}
			this._payTypeFilter = payType;
			this.payTypeFilterChooser.valNoCallback = payType;
			this._categoryFilter = category;
			this.categoryFilterChooser.valNoCallback = category;
			this.ResetRefresh();
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x0023DC0F File Offset: 0x0023C00F
		protected void SyncCreatorFilter(string s)
		{
			this._creatorFilter = s;
			this.ResetRefresh();
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06005F3C RID: 24380 RVA: 0x0023DC1E File Offset: 0x0023C01E
		// (set) Token: 0x06005F3D RID: 24381 RVA: 0x0023DC26 File Offset: 0x0023C026
		public string CreatorFilter
		{
			get
			{
				return this._creatorFilter;
			}
			set
			{
				this._hostedOption = "All";
				this.hostedOptionChooser.valNoCallback = "All";
				this.creatorFilterChooser.val = value;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06005F3E RID: 24382 RVA: 0x0023DC4F File Offset: 0x0023C04F
		// (set) Token: 0x06005F3F RID: 24383 RVA: 0x0023DC57 File Offset: 0x0023C057
		public string CreatorFilterOnly
		{
			get
			{
				return this._creatorFilter;
			}
			set
			{
				this.CloseAllDetails();
				this.ResetFilters();
				this._hostedOption = "All";
				this.hostedOptionChooser.valNoCallback = "All";
				this.creatorFilterChooser.val = value;
			}
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x0023DC8C File Offset: 0x0023C08C
		protected void SyncTagsFilter(string s)
		{
			this._tagsFilter = s;
			this.ResetRefresh();
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06005F41 RID: 24385 RVA: 0x0023DC9B File Offset: 0x0023C09B
		// (set) Token: 0x06005F42 RID: 24386 RVA: 0x0023DCA3 File Offset: 0x0023C0A3
		public string TagsFilter
		{
			get
			{
				return this._tagsFilter;
			}
			set
			{
				this.tagsFilterChooser.val = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005F43 RID: 24387 RVA: 0x0023DCB1 File Offset: 0x0023C0B1
		// (set) Token: 0x06005F44 RID: 24388 RVA: 0x0023DCB9 File Offset: 0x0023C0B9
		public string TagsFilterOnly
		{
			get
			{
				return this._tagsFilter;
			}
			set
			{
				this.ResetFilters();
				this.tagsFilterChooser.val = value;
			}
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x0023DCCD File Offset: 0x0023C0CD
		protected void SyncSortPrimary(string s)
		{
			this._sortPrimary = s;
			this.ResetRefresh();
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x0023DCDC File Offset: 0x0023C0DC
		protected void SyncSortSecondary(string s)
		{
			this._sortSecondary = s;
			this.ResetRefresh();
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x0023DCEC File Offset: 0x0023C0EC
		public void NavigateWebPanel(string url)
		{
			if (this.webBrowser != null && this.webBrowser.url != url && this._webBrowserEnabled)
			{
				if (this.isWebLoadingIndicator != null)
				{
					this.isWebLoadingIndicator.SetActive(true);
				}
				this.webBrowser.url = url;
			}
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x0023DD54 File Offset: 0x0023C154
		public void ShowHoverUrl(string url)
		{
			if (this.webBrowser != null)
			{
				this.webBrowser.HoveredURL = url;
			}
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x0023DD73 File Offset: 0x0023C173
		protected void GetResourceDetailErrorCallback(string err, HubResourceItemDetailUI hridui)
		{
			SuperController.LogError("Error during fetch of resource detail from Hub");
			this.CloseDetail(null);
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x0023DD88 File Offset: 0x0023C188
		protected void GetResourceDetailCallback(SimpleJSON.JSONNode jsonNode, HubResourceItemDetailUI hridui)
		{
			if (jsonNode != null && hridui != null)
			{
				JSONClass asObject = jsonNode.AsObject;
				if (asObject != null)
				{
					HubResourceItemDetail hubResourceItemDetail = new HubResourceItemDetail(asObject, this);
					hubResourceItemDetail.Refresh();
					hubResourceItemDetail.RegisterUI(hridui);
				}
			}
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x0023DDD8 File Offset: 0x0023C1D8
		public void OpenDetail(string resource_id, bool isPackageName = false)
		{
			if (this._hubEnabled)
			{
				if (this.resourceDetailPrefab != null && this.resourceDetailContainer != null)
				{
					this.Show();
					HubResourceItemDetailUI hridui;
					if (this.savedResourceDetailsPanels.TryGetValue(resource_id, out hridui))
					{
						this.savedResourceDetailsPanels.Remove(resource_id);
						hridui.gameObject.SetActive(true);
						this.resourceDetailStack.Push(hridui);
					}
					else
					{
						RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.resourceDetailPrefab);
						rectTransform.SetParent(this.resourceDetailContainer, false);
						hridui = rectTransform.GetComponent<HubResourceItemDetailUI>();
						this.resourceDetailStack.Push(hridui);
						JSONClass jsonclass = new JSONClass();
						jsonclass["source"] = "VaM";
						jsonclass["action"] = "getResourceDetail";
						jsonclass["latest_image"] = "Y";
						if (isPackageName)
						{
							jsonclass["package_name"] = resource_id;
						}
						else
						{
							jsonclass["resource_id"] = resource_id;
						}
						string postData = jsonclass.ToString();
						base.StartCoroutine(this.PostRequest(this.apiUrl, postData, delegate(SimpleJSON.JSONNode jsonNode)
						{
							this.GetResourceDetailCallback(jsonNode, hridui);
						}, delegate(string err)
						{
							this.GetResourceDetailErrorCallback(err, hridui);
						}));
					}
					if (this.detailPanel != null)
					{
						this.detailPanel.SetActive(true);
					}
				}
			}
			else
			{
				SuperController.LogError("Cannot perform action. Hub is disabled in User Preferences");
			}
		}

		// Token: 0x06005F4C RID: 24396 RVA: 0x0023DF78 File Offset: 0x0023C378
		public void CloseDetail(string resource_id)
		{
			if (this.resourceDetailStack.Count > 0)
			{
				HubResourceItemDetailUI hubResourceItemDetailUI = this.resourceDetailStack.Pop();
				if (hubResourceItemDetailUI.connectedItem != null && hubResourceItemDetailUI.connectedItem.IsDownloading)
				{
					hubResourceItemDetailUI.gameObject.SetActive(false);
					this.savedResourceDetailsPanels.Add(resource_id, hubResourceItemDetailUI);
				}
				else
				{
					if (resource_id != null)
					{
						this.savedResourceDetailsPanels.Remove(resource_id);
					}
					UnityEngine.Object.Destroy(hubResourceItemDetailUI.gameObject);
				}
			}
			if (this.resourceDetailStack.Count == 0)
			{
				if (this.detailPanel != null)
				{
					this.detailPanel.SetActive(false);
				}
			}
			else
			{
				HubResourceItemDetailUI hubResourceItemDetailUI2 = this.resourceDetailStack.Peek();
				if (hubResourceItemDetailUI2.connectedItem != null)
				{
					hubResourceItemDetailUI2.connectedItem.NavigateToOverview();
				}
			}
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x0023E050 File Offset: 0x0023C450
		protected void CloseAllDetails()
		{
			while (this.resourceDetailStack.Count > 0)
			{
				HubResourceItemDetailUI hubResourceItemDetailUI = this.resourceDetailStack.Pop();
				if (hubResourceItemDetailUI.connectedItem != null && hubResourceItemDetailUI.connectedItem.IsDownloading)
				{
					hubResourceItemDetailUI.gameObject.SetActive(false);
					this.savedResourceDetailsPanels.Add(hubResourceItemDetailUI.connectedItem.ResourceId, hubResourceItemDetailUI);
				}
				else
				{
					if (hubResourceItemDetailUI.connectedItem != null)
					{
						this.savedResourceDetailsPanels.Remove(hubResourceItemDetailUI.connectedItem.ResourceId);
					}
					UnityEngine.Object.Destroy(hubResourceItemDetailUI.gameObject);
				}
			}
			if (this.detailPanel != null)
			{
				this.detailPanel.SetActive(false);
			}
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x0023E10C File Offset: 0x0023C50C
		public RectTransform CreateDownloadPrefabInstance()
		{
			RectTransform result = null;
			if (this.packageDownloadPrefab != null)
			{
				result = UnityEngine.Object.Instantiate<RectTransform>(this.packageDownloadPrefab);
			}
			return result;
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x0023E13C File Offset: 0x0023C53C
		public RectTransform CreateCreatorSupportButtonPrefabInstance()
		{
			RectTransform result = null;
			if (this.creatorSupportButtonPrefab != null)
			{
				result = UnityEngine.Object.Instantiate<RectTransform>(this.creatorSupportButtonPrefab);
			}
			return result;
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x0023E169 File Offset: 0x0023C569
		protected void FindMissingPackagesErrorCallback(string err)
		{
			SuperController.LogError("Error during hub request " + err);
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x0023E17C File Offset: 0x0023C57C
		protected void FindMissingPackagesCallback(SimpleJSON.JSONNode jsonNode)
		{
			if (jsonNode != null)
			{
				JSONClass asObject = jsonNode.AsObject;
				if (asObject != null)
				{
					string text = asObject["status"];
					if (text != null && text == "error")
					{
						string str = jsonNode["error"];
						SuperController.LogError("findPackages returned error " + str);
					}
					else
					{
						JSONClass asObject2 = jsonNode["packages"].AsObject;
						if (asObject2 != null)
						{
							if (this.missingPackages != null)
							{
								foreach (HubResourcePackageUI hubResourcePackageUI in this.missingPackages)
								{
									UnityEngine.Object.Destroy(hubResourcePackageUI.gameObject);
								}
								this.missingPackages.Clear();
							}
							else
							{
								this.missingPackages = new List<HubResourcePackageUI>();
							}
							foreach (string text2 in this.checkMissingPackageNames)
							{
								JSONClass jsonclass = asObject2[text2].AsObject;
								if (jsonclass == null)
								{
									jsonclass = new JSONClass();
									jsonclass["filename"] = text2;
									jsonclass["downloadUrl"] = "null";
								}
								else
								{
									if (Regex.IsMatch(text2, "[0-9]+$"))
									{
										string text3 = jsonclass["filename"];
										if (text3 == null || text3 == "null" || text3 != text2 + ".var")
										{
											Debug.LogError("Missing file name " + text3 + " does not match missing package name " + text2);
											jsonclass["filename"] = text2;
											jsonclass["file_size"] = "null";
											jsonclass["licenseType"] = "null";
											jsonclass["downloadUrl"] = "null";
										}
									}
									else
									{
										string text4 = jsonclass["filename"];
										if (text4 == null || text4 == "null")
										{
											jsonclass["filename"] = text2;
										}
									}
									string text5 = jsonclass["resource_id"];
									if (text5 == null || text5 == "null")
									{
										jsonclass["downloadUrl"] = "null";
									}
								}
								HubResourcePackage hubResourcePackage = new HubResourcePackage(jsonclass, this, true);
								RectTransform rectTransform = this.CreateDownloadPrefabInstance();
								if (rectTransform != null)
								{
									rectTransform.SetParent(this.missingPackagesContainer, false);
									HubResourcePackageUI component = rectTransform.GetComponent<HubResourcePackageUI>();
									if (component != null)
									{
										this.missingPackages.Add(component);
										hubResourcePackage.RegisterUI(component);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x0023E4DC File Offset: 0x0023C8DC
		public void OpenMissingPackagesPanel()
		{
			if (this._hubEnabled)
			{
				if (this.packageManager != null && this.missingPackagesPanel != null && this.missingPackagesContainer != null)
				{
					this.Show();
					if (this.missingPackagesPanel != null)
					{
						this.missingPackagesPanel.SetActive(true);
					}
					if (this.downloadQueue.Count == 0)
					{
						List<string> missingPackageNames = this.packageManager.MissingPackageNames;
						if (missingPackageNames.Count > 0)
						{
							JSONClass jsonclass = new JSONClass();
							jsonclass["source"] = "VaM";
							jsonclass["action"] = "findPackages";
							this.checkMissingPackageNames = missingPackageNames;
							jsonclass["packages"] = string.Join(",", missingPackageNames.ToArray());
							string postData = jsonclass.ToString();
							base.StartCoroutine(this.PostRequest(this.apiUrl, postData, new HubBrowse.RequestSuccessCallback(this.FindMissingPackagesCallback), new HubBrowse.RequestErrorCallback(this.FindMissingPackagesErrorCallback)));
						}
						else if (this.missingPackages != null)
						{
							foreach (HubResourcePackageUI hubResourcePackageUI in this.missingPackages)
							{
								UnityEngine.Object.Destroy(hubResourcePackageUI.gameObject);
							}
							this.missingPackages.Clear();
						}
						else
						{
							this.missingPackages = new List<HubResourcePackageUI>();
						}
					}
				}
			}
			else
			{
				SuperController.LogError("Cannot perform action. Hub is disabled in User Preferences");
			}
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x0023E68C File Offset: 0x0023CA8C
		public void CloseMissingPackagesPanel()
		{
			if (this.missingPackagesPanel != null)
			{
				this.missingPackagesPanel.SetActive(false);
			}
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x0023E6AC File Offset: 0x0023CAAC
		public void DownloadAllMissingPackages()
		{
			if (this.missingPackages != null)
			{
				foreach (HubResourcePackageUI hubResourcePackageUI in this.missingPackages)
				{
					hubResourcePackageUI.connectedItem.Download();
				}
			}
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x0023E718 File Offset: 0x0023CB18
		public string GetPackageHubResourceId(string packageId)
		{
			string result = null;
			if (this.packageIdToResourceId != null)
			{
				this.packageIdToResourceId.TryGetValue(packageId, out result);
			}
			return result;
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x0023E742 File Offset: 0x0023CB42
		protected void GetPackagesJSONErrorCallback(string err)
		{
			SuperController.LogError("Error during hub request for packages.json " + err);
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x0023E754 File Offset: 0x0023CB54
		protected void GetPackagesJSONCallback(SimpleJSON.JSONNode jsonNode)
		{
			if (jsonNode != null)
			{
				JSONClass asObject = jsonNode.AsObject;
				if (asObject != null)
				{
					this.packageGroupToLatestVersion = new Dictionary<string, int>();
					this.packageIdToResourceId = new Dictionary<string, string>();
					foreach (string text in asObject.Keys)
					{
						string text2 = Regex.Replace(text, "\\.var$", string.Empty);
						string text3 = FileManager.PackageIDToPackageVersion(text2);
						int num;
						if (text3 != null && int.TryParse(text3, out num))
						{
							string value = asObject[text];
							this.packageIdToResourceId.Add(text2, value);
							string key = FileManager.PackageIDToPackageGroupID(text2);
							int num2;
							if (this.packageGroupToLatestVersion.TryGetValue(key, out num2))
							{
								if (num > num2)
								{
									this.packageGroupToLatestVersion.Remove(key);
									this.packageGroupToLatestVersion.Add(key, num);
								}
							}
							else
							{
								this.packageGroupToLatestVersion.Add(key, num);
							}
						}
					}
				}
			}
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x0023E87C File Offset: 0x0023CC7C
		protected void FindUpdatesErrorCallback(string err)
		{
			SuperController.LogError("Error during hub request " + err);
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x0023E890 File Offset: 0x0023CC90
		protected void FindUpdatesCallback(SimpleJSON.JSONNode jsonNode)
		{
			if (jsonNode != null)
			{
				JSONClass asObject = jsonNode.AsObject;
				if (asObject != null)
				{
					string text = asObject["status"];
					if (text != null && text == "error")
					{
						string str = jsonNode["error"];
						SuperController.LogError("findPackages returned error " + str);
					}
					else
					{
						JSONClass asObject2 = jsonNode["packages"].AsObject;
						if (asObject2 != null)
						{
							if (this.updates != null)
							{
								foreach (HubResourcePackageUI hubResourcePackageUI in this.updates)
								{
									UnityEngine.Object.Destroy(hubResourcePackageUI.gameObject);
								}
								this.updates.Clear();
							}
							else
							{
								this.updates = new List<HubResourcePackageUI>();
							}
							foreach (string text2 in this.checkUpdateNames)
							{
								JSONClass jsonclass = asObject2[text2].AsObject;
								if (jsonclass == null)
								{
									jsonclass = new JSONClass();
									jsonclass["filename"] = text2;
									jsonclass["downloadUrl"] = "null";
								}
								else
								{
									string text3 = jsonclass["filename"];
									if (text3 == null || text3 == "null")
									{
										jsonclass["filename"] = text2;
									}
								}
								HubResourcePackage hubResourcePackage = new HubResourcePackage(jsonclass, this, false);
								RectTransform rectTransform = this.CreateDownloadPrefabInstance();
								if (rectTransform != null)
								{
									rectTransform.SetParent(this.updatesContainer, false);
									HubResourcePackageUI component = rectTransform.GetComponent<HubResourcePackageUI>();
									if (component != null)
									{
										this.updates.Add(component);
										hubResourcePackage.RegisterUI(component);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x0023EAEC File Offset: 0x0023CEEC
		public void OpenUpdatesPanel()
		{
			if (this._hubEnabled)
			{
				if (this.packageManager != null && this.updatesPanel != null && this.updatesContainer != null)
				{
					this.Show();
					if (this.updatesPanel != null)
					{
						this.updatesPanel.SetActive(true);
					}
					if (this.downloadQueue.Count == 0)
					{
						this.checkUpdateNames = new List<string>();
						if (this.packageGroupToLatestVersion != null)
						{
							foreach (VarPackageGroup varPackageGroup in FileManager.GetPackageGroups())
							{
								int num;
								if (this.packageGroupToLatestVersion.TryGetValue(varPackageGroup.Name, out num) && varPackageGroup.NewestVersion < num)
								{
									this.checkUpdateNames.Add(varPackageGroup.Name + ".latest");
								}
							}
						}
						if (this.checkUpdateNames.Count > 0)
						{
							JSONClass jsonclass = new JSONClass();
							jsonclass["source"] = "VaM";
							jsonclass["action"] = "findPackages";
							jsonclass["packages"] = string.Join(",", this.checkUpdateNames.ToArray());
							string postData = jsonclass.ToString();
							base.StartCoroutine(this.PostRequest(this.apiUrl, postData, new HubBrowse.RequestSuccessCallback(this.FindUpdatesCallback), new HubBrowse.RequestErrorCallback(this.FindUpdatesErrorCallback)));
						}
						else if (this.updates != null)
						{
							foreach (HubResourcePackageUI hubResourcePackageUI in this.updates)
							{
								UnityEngine.Object.Destroy(hubResourcePackageUI.gameObject);
							}
							this.updates.Clear();
						}
						else
						{
							this.updates = new List<HubResourcePackageUI>();
						}
					}
				}
			}
			else
			{
				SuperController.LogError("Cannot perform action. Hub is disabled in User Preferences");
			}
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x0023ED30 File Offset: 0x0023D130
		public void CloseUpdatesPanel()
		{
			if (this.updatesPanel != null)
			{
				this.updatesPanel.SetActive(false);
			}
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x0023ED50 File Offset: 0x0023D150
		public void DownloadAllUpdates()
		{
			if (this.updates != null)
			{
				foreach (HubResourcePackageUI hubResourcePackageUI in this.updates)
				{
					hubResourcePackageUI.connectedItem.Download();
				}
			}
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x0023EDBC File Offset: 0x0023D1BC
		protected void RefreshCookies()
		{
			if (this.GetBrowserCookiesRoutine == null && this.browser != null)
			{
				base.StartCoroutine(this.GetBrowserCookies());
			}
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x0023EDE8 File Offset: 0x0023D1E8
		protected IEnumerator GetBrowserCookies()
		{
			if (this.hubCookies == null)
			{
				this.hubCookies = new List<string>();
			}
			while (!this.browser.IsReady)
			{
				yield return null;
			}
			IPromise<List<Cookie>> promise = this.browser.CookieManager.GetCookies();
			yield return promise.ToWaitFor(true);
			this.hubCookies.Clear();
			foreach (Cookie cookie in promise.Value)
			{
				if (cookie.domain == this.cookieHost)
				{
					this.hubCookies.Add(string.Format("{0}={1}", cookie.name, cookie.value));
				}
			}
			this.GetBrowserCookiesRoutine = null;
			yield break;
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x0023EE04 File Offset: 0x0023D204
		protected IEnumerator DownloadRoutine()
		{
			for (;;)
			{
				if (this.downloadQueue.Count > 0)
				{
					this.isDownloadingJSON.val = true;
					this.downloadQueuedCountJSON.val = "Queued: " + this.downloadQueue.Count;
					HubBrowse.DownloadRequest request = this.downloadQueue.Dequeue();
					yield return this.BinaryGetRequest(request.url, request.startedCallback, request.successCallback, request.errorCallback, request.progressCallback, this.hubCookies);
					if (this.downloadQueue.Count == 0)
					{
						FileManager.Refresh();
					}
				}
				else
				{
					this.isDownloadingJSON.val = false;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x0023EE20 File Offset: 0x0023D220
		protected void OnPackageRefresh()
		{
			if (this.items != null)
			{
				foreach (HubResourceItemUI hubResourceItemUI in this.items)
				{
					hubResourceItemUI.connectedItem.Refresh();
				}
			}
			if (this.missingPackages != null)
			{
				foreach (HubResourcePackageUI hubResourcePackageUI in this.missingPackages)
				{
					hubResourcePackageUI.connectedItem.Refresh();
				}
			}
			if (this.updates != null)
			{
				foreach (HubResourcePackageUI hubResourcePackageUI2 in this.updates)
				{
					hubResourcePackageUI2.connectedItem.Refresh();
				}
			}
			if (this.resourceDetailStack != null)
			{
				foreach (HubResourceItemDetailUI hubResourceItemDetailUI in this.resourceDetailStack)
				{
					hubResourceItemDetailUI.connectedItem.Refresh();
				}
			}
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x0023EFA0 File Offset: 0x0023D3A0
		public void QueueDownload(string url, string promotionalUrl, HubBrowse.BinaryRequestStartedCallback startedCallback, HubBrowse.RequestProgressCallback progressCallback, HubBrowse.BinaryRequestSuccessCallback successCallback, HubBrowse.RequestErrorCallback errorCallback)
		{
			HubBrowse.DownloadRequest downloadRequest = new HubBrowse.DownloadRequest();
			downloadRequest.url = url;
			if (this.downloadQueue.Count == 0)
			{
				downloadRequest.promotionalUrl = promotionalUrl;
			}
			downloadRequest.startedCallback = startedCallback;
			downloadRequest.progressCallback = progressCallback;
			downloadRequest.successCallback = successCallback;
			downloadRequest.errorCallback = errorCallback;
			this.downloadQueue.Enqueue(downloadRequest);
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x0023EFFC File Offset: 0x0023D3FC
		protected void OpenDownloading()
		{
			if (this.savedResourceDetailsPanels != null)
			{
				foreach (string resource_id in this.savedResourceDetailsPanels.Keys)
				{
					this.OpenDetail(resource_id, false);
				}
			}
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x0023F06C File Offset: 0x0023D46C
		protected void GetInfoCallback(SimpleJSON.JSONNode jsonNode)
		{
			if (this.refreshingGetInfoPanel != null)
			{
				this.refreshingGetInfoPanel.gameObject.SetActive(false);
			}
			if (this.failedGetInfoPanel != null)
			{
				this.failedGetInfoPanel.gameObject.SetActive(false);
			}
			this.hubInfoCoroutine = null;
			this.hubInfoRefreshing = false;
			this.hubInfoSuccess = true;
			JSONClass asObject = jsonNode.AsObject;
			if (asObject != null)
			{
				if (asObject["location"] != null)
				{
					JSONArray asArray = asObject["location"].AsArray;
					if (asArray != null)
					{
						List<string> list = new List<string>();
						list.Add("All");
						IEnumerator enumerator = asArray.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								SimpleJSON.JSONNode d = (SimpleJSON.JSONNode)obj;
								list.Add(d);
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
						this.hostedOptionChooser.choices = list;
					}
				}
				if (asObject["category"] != null)
				{
					JSONArray asArray2 = asObject["category"].AsArray;
					if (asArray2 != null)
					{
						List<string> list2 = new List<string>();
						list2.Add("All");
						IEnumerator enumerator2 = asArray2.GetEnumerator();
						try
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								SimpleJSON.JSONNode d2 = (SimpleJSON.JSONNode)obj2;
								list2.Add(d2);
							}
						}
						finally
						{
							IDisposable disposable2;
							if ((disposable2 = (enumerator2 as IDisposable)) != null)
							{
								disposable2.Dispose();
							}
						}
						this.payTypeFilterChooser.choices = list2;
					}
				}
				if (asObject["type"] != null)
				{
					JSONArray asArray3 = asObject["type"].AsArray;
					if (asArray3 != null)
					{
						List<string> list3 = new List<string>();
						list3.Add("All");
						IEnumerator enumerator3 = asArray3.GetEnumerator();
						try
						{
							while (enumerator3.MoveNext())
							{
								object obj3 = enumerator3.Current;
								SimpleJSON.JSONNode d3 = (SimpleJSON.JSONNode)obj3;
								list3.Add(d3);
							}
						}
						finally
						{
							IDisposable disposable3;
							if ((disposable3 = (enumerator3 as IDisposable)) != null)
							{
								disposable3.Dispose();
							}
						}
						this.categoryFilterChooser.choices = list3;
					}
				}
				if (asObject["users"] != null)
				{
					JSONClass asObject2 = asObject["users"].AsObject;
					if (asObject2 != null)
					{
						List<string> list4 = new List<string>();
						list4.Add("All");
						foreach (string item in asObject2.Keys)
						{
							list4.Add(item);
						}
						this.creatorFilterChooser.choices = list4;
					}
				}
				if (asObject["tags"] != null)
				{
					JSONClass asObject3 = asObject["tags"].AsObject;
					if (asObject3 != null)
					{
						List<string> list5 = new List<string>();
						list5.Add("All");
						foreach (string item2 in asObject3.Keys)
						{
							list5.Add(item2);
						}
						this.tagsFilterChooser.choices = list5;
					}
				}
				if (asObject["sort"] != null)
				{
					JSONArray asArray4 = asObject["sort"].AsArray;
					if (asArray4 != null)
					{
						List<string> list6 = new List<string>();
						List<string> list7 = new List<string>();
						list7.Add("None");
						IEnumerator enumerator6 = asArray4.GetEnumerator();
						try
						{
							while (enumerator6.MoveNext())
							{
								object obj4 = enumerator6.Current;
								SimpleJSON.JSONNode d4 = (SimpleJSON.JSONNode)obj4;
								list6.Add(d4);
								list7.Add(d4);
							}
						}
						finally
						{
							IDisposable disposable4;
							if ((disposable4 = (enumerator6 as IDisposable)) != null)
							{
								disposable4.Dispose();
							}
						}
						this.sortPrimaryChooser.choices = list6;
						this.sortSecondaryChooser.choices = list7;
					}
				}
				string text = asObject["last_update"];
				if (this.packagesJSONUrl != null && this.packagesJSONUrl != string.Empty && text != null)
				{
					string uri = this.packagesJSONUrl + "?" + text;
					base.StartCoroutine(this.GetRequest(uri, new HubBrowse.RequestSuccessCallback(this.GetPackagesJSONCallback), new HubBrowse.RequestErrorCallback(this.GetPackagesJSONErrorCallback)));
				}
			}
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x0023F590 File Offset: 0x0023D990
		protected void GetInfoErrorCallback(string err)
		{
			if (this.refreshingGetInfoPanel != null)
			{
				this.refreshingGetInfoPanel.gameObject.SetActive(false);
			}
			if (this.failedGetInfoPanel != null)
			{
				this.failedGetInfoPanel.gameObject.SetActive(true);
			}
			if (this.getInfoErrorText != null)
			{
				this.getInfoErrorText.text = err;
			}
			this.hubInfoCoroutine = null;
			this.hubInfoRefreshing = false;
			this.hubInfoSuccess = false;
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x0023F614 File Offset: 0x0023DA14
		protected void GetHubInfo()
		{
			if (!this.hubInfoRefreshing)
			{
				if (this.failedGetInfoPanel != null)
				{
					this.failedGetInfoPanel.gameObject.SetActive(false);
				}
				JSONClass jsonclass = new JSONClass();
				jsonclass["source"] = "VaM";
				jsonclass["action"] = "getInfo";
				string postData = jsonclass.ToString();
				this.hubInfoRefreshing = true;
				if (this.refreshingGetInfoPanel != null)
				{
					this.refreshingGetInfoPanel.gameObject.SetActive(true);
				}
				this.hubInfoCoroutine = base.StartCoroutine(this.PostRequest(this.apiUrl, postData, new HubBrowse.RequestSuccessCallback(this.GetInfoCallback), new HubBrowse.RequestErrorCallback(this.GetInfoErrorCallback)));
			}
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x0023F6DF File Offset: 0x0023DADF
		protected void CancelGetHubInfo()
		{
			if (this.hubInfoRefreshing && this.hubInfoCoroutine != null)
			{
				base.StopCoroutine(this.hubInfoCoroutine);
				this.GetInfoErrorCallback("Cancelled");
			}
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x0023F710 File Offset: 0x0023DB10
		protected void Init()
		{
			this.hubEnabledJSON = new JSONStorableBool("hubEnabled", this._hubEnabled, new JSONStorableBool.SetBoolCallback(this.SyncHubEnabled));
			this.enableHubAction = new JSONStorableAction("EnableHub", new JSONStorableAction.ActionCallback(this.EnableHub));
			this.webBrowserEnabledJSON = new JSONStorableBool("webBrowserEnabled", this._webBrowserEnabled, new JSONStorableBool.SetBoolCallback(this.SyncWebBrowserEnabled));
			this.enableWebBrowserAction = new JSONStorableAction("EnableWebBrowser", new JSONStorableAction.ActionCallback(this.EnableWebBrowser));
			this.cancelGetHubInfoAction = new JSONStorableAction("CancelGetHubInfo", new JSONStorableAction.ActionCallback(this.CancelGetHubInfo));
			this.retryGetHubInfoAction = new JSONStorableAction("RetryGetHubInfo", new JSONStorableAction.ActionCallback(this.GetHubInfo));
			this.numResourcesJSON = new JSONStorableString("numResources", "0", new JSONStorableString.SetStringCallback(this.SyncNumResources));
			this.pageInfoJSON = new JSONStorableString("pageInfo", "Page 0 of 0");
			this.numPagesJSON = new JSONStorableString("numPages", "0", new JSONStorableString.SetStringCallback(this.SyncNumPages));
			this.currentPageJSON = new JSONStorableString("currentPage", "1", new JSONStorableString.SetStringCallback(this.SyncCurrentPage));
			this.firstPageAction = new JSONStorableAction("FirstPage", new JSONStorableAction.ActionCallback(this.FirstPage));
			this.previousPageAction = new JSONStorableAction("PreviousPage", new JSONStorableAction.ActionCallback(this.PreviousPage));
			base.RegisterAction(this.previousPageAction);
			this.nextPageAction = new JSONStorableAction("NextPage", new JSONStorableAction.ActionCallback(this.NextPage));
			base.RegisterAction(this.nextPageAction);
			this.refreshResourcesAction = new JSONStorableAction("RefreshResources", new JSONStorableAction.ActionCallback(this.ResetRefresh));
			base.RegisterAction(this.refreshResourcesAction);
			this.clearFiltersAction = new JSONStorableAction("ResetFilters", new JSONStorableAction.ActionCallback(this.ResetFiltersAndRefresh));
			base.RegisterAction(this.clearFiltersAction);
			List<string> choicesList = new List<string>
			{
				"All"
			};
			this.hostedOptionChooser = new JSONStorableStringChooser("hostedOption", choicesList, this._hostedOption, "Hosted Option", new JSONStorableStringChooser.SetStringCallback(this.SyncHostedOption));
			this.hostedOptionChooser.isStorable = false;
			this.hostedOptionChooser.isRestorable = false;
			base.RegisterStringChooser(this.hostedOptionChooser);
			this.searchFilterJSON = new JSONStorableString("searchFilter", string.Empty, new JSONStorableString.SetStringCallback(this.SyncSearchFilter));
			this.searchFilterJSON.enableOnChange = true;
			this.searchFilterJSON.isStorable = false;
			this.searchFilterJSON.isRestorable = false;
			base.RegisterString(this.searchFilterJSON);
			List<string> choicesList2 = new List<string>
			{
				"All"
			};
			this.payTypeFilterChooser = new JSONStorableStringChooser("payType", choicesList2, this._payTypeFilter, "Pay Type", new JSONStorableStringChooser.SetStringCallback(this.SyncPayTypeFilter));
			this.payTypeFilterChooser.isStorable = false;
			this.payTypeFilterChooser.isRestorable = false;
			base.RegisterStringChooser(this.payTypeFilterChooser);
			List<string> choicesList3 = new List<string>
			{
				"All"
			};
			this.categoryFilterChooser = new JSONStorableStringChooser("category", choicesList3, this._categoryFilter, "Category", new JSONStorableStringChooser.SetStringCallback(this.SyncCategoryFilter));
			this.categoryFilterChooser.isStorable = false;
			this.categoryFilterChooser.isRestorable = false;
			base.RegisterStringChooser(this.categoryFilterChooser);
			List<string> choicesList4 = new List<string>
			{
				"All"
			};
			this.creatorFilterChooser = new JSONStorableStringChooser("creator", choicesList4, this._creatorFilter, "Creator", new JSONStorableStringChooser.SetStringCallback(this.SyncCreatorFilter));
			this.creatorFilterChooser.isStorable = false;
			this.creatorFilterChooser.isRestorable = false;
			base.RegisterStringChooser(this.creatorFilterChooser);
			List<string> choicesList5 = new List<string>
			{
				"All"
			};
			this.tagsFilterChooser = new JSONStorableStringChooser("tags", choicesList5, this._tagsFilter, "Tags", new JSONStorableStringChooser.SetStringCallback(this.SyncTagsFilter));
			this.tagsFilterChooser.isStorable = false;
			this.tagsFilterChooser.isRestorable = false;
			base.RegisterStringChooser(this.tagsFilterChooser);
			List<string> choicesList6 = new List<string>
			{
				"Latest Update"
			};
			this.sortPrimaryChooser = new JSONStorableStringChooser("sortPrimary", choicesList6, this._sortPrimary, "Primary Sort", new JSONStorableStringChooser.SetStringCallback(this.SyncSortPrimary));
			this.sortPrimaryChooser.isStorable = false;
			this.sortPrimaryChooser.isRestorable = false;
			base.RegisterStringChooser(this.sortPrimaryChooser);
			List<string> choicesList7 = new List<string>
			{
				"None"
			};
			this.sortSecondaryChooser = new JSONStorableStringChooser("sortSecondary", choicesList7, this._sortSecondary, "Secondary Sort", new JSONStorableStringChooser.SetStringCallback(this.SyncSortSecondary));
			this.sortSecondaryChooser.isStorable = false;
			this.sortSecondaryChooser.isRestorable = false;
			base.RegisterStringChooser(this.sortSecondaryChooser);
			this.openMissingPackagesPanelAction = new JSONStorableAction("OpenMissingPackagesPanel", new JSONStorableAction.ActionCallback(this.OpenMissingPackagesPanel));
			base.RegisterAction(this.openMissingPackagesPanelAction);
			this.closeMissingPackagesPanelAction = new JSONStorableAction("CloseMissingPackagesPanel", new JSONStorableAction.ActionCallback(this.CloseMissingPackagesPanel));
			base.RegisterAction(this.closeMissingPackagesPanelAction);
			this.downloadAllMissingPackagesAction = new JSONStorableAction("DownloadAllMissingPackages", new JSONStorableAction.ActionCallback(this.DownloadAllMissingPackages));
			base.RegisterAction(this.downloadAllMissingPackagesAction);
			this.openUpdatesPanelAction = new JSONStorableAction("OpenUpdatesPanel", new JSONStorableAction.ActionCallback(this.OpenUpdatesPanel));
			base.RegisterAction(this.openUpdatesPanelAction);
			this.closeUpdatesPanelAction = new JSONStorableAction("CloseUpdatesPanel", new JSONStorableAction.ActionCallback(this.CloseUpdatesPanel));
			base.RegisterAction(this.closeUpdatesPanelAction);
			this.downloadAllUpdatesAction = new JSONStorableAction("DownloadAllUpdates", new JSONStorableAction.ActionCallback(this.DownloadAllUpdates));
			base.RegisterAction(this.downloadAllUpdatesAction);
			this.isDownloadingJSON = new JSONStorableBool("isDownloading", false);
			this.downloadQueuedCountJSON = new JSONStorableString("downloadQueuedCount", "Queued: 0");
			this.openDownloadingAction = new JSONStorableAction("OpenDownloading", new JSONStorableAction.ActionCallback(this.OpenDownloading));
			base.RegisterAction(this.openDownloadingAction);
			this.resourceDetailStack = new Stack<HubResourceItemDetailUI>();
			this.savedResourceDetailsPanels = new Dictionary<string, HubResourceItemDetailUI>();
			this.downloadQueue = new Queue<HubBrowse.DownloadRequest>();
			base.StartCoroutine(this.DownloadRoutine());
			FileManager.RegisterRefreshHandler(new FileManager.OnRefresh(this.OnPackageRefresh));
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x0023FD74 File Offset: 0x0023E174
		protected override void InitUI(Transform t, bool isAlt)
		{
			if (t != null)
			{
				HubBrowseUI componentInChildren = t.GetComponentInChildren<HubBrowseUI>(true);
				if (componentInChildren != null)
				{
					if (!isAlt)
					{
						this.hubBrowseUI = componentInChildren;
						this.itemContainer = componentInChildren.itemContainer;
						this.itemScrollRect = componentInChildren.itemScrollRect;
						this.refreshingGetInfoPanel = componentInChildren.refreshingGetInfoPanel;
						if (this.refreshingGetInfoPanel != null && this.hubInfoRefreshing)
						{
							this.refreshingGetInfoPanel.gameObject.SetActive(true);
						}
						this.failedGetInfoPanel = componentInChildren.failedGetInfoPanel;
						if (this.failedGetInfoPanel != null && !this.hubInfoSuccess && !this.hubInfoRefreshing)
						{
							this.failedGetInfoPanel.gameObject.SetActive(true);
						}
						this.getInfoErrorText = componentInChildren.getInfoErrorText;
						this.detailPanel = componentInChildren.detailPanel;
						this.resourceDetailContainer = componentInChildren.resourceDetailContainer;
						this.browser = componentInChildren.browser;
						this.webBrowser = componentInChildren.webBrowser;
						this.isWebLoadingIndicator = componentInChildren.isWebLoadingIndicator;
						this.refreshIndicator = componentInChildren.refreshIndicator;
						this.missingPackagesPanel = componentInChildren.missingPackagesPanel;
						this.missingPackagesContainer = componentInChildren.missingPackagesContainer;
						this.updatesPanel = componentInChildren.updatesPanel;
						this.updatesContainer = componentInChildren.updatesContainer;
					}
					this.hubEnabledJSON.RegisterNegativeIndicator(componentInChildren.hubEnabledNegativeIndicator, isAlt);
					this.enableHubAction.RegisterButton(componentInChildren.enableHubButton, isAlt);
					this.webBrowserEnabledJSON.RegisterNegativeIndicator(componentInChildren.webBrowserEnabledNegativeIndicator, isAlt);
					this.enableWebBrowserAction.RegisterButton(componentInChildren.enabledWebBrowserButton, isAlt);
					this.cancelGetHubInfoAction.RegisterButton(componentInChildren.cancelGetHubInfoButton, isAlt);
					this.retryGetHubInfoAction.RegisterButton(componentInChildren.retryGetHubInfoButton, isAlt);
					this.pageInfoJSON.RegisterText(componentInChildren.pageInfoText, isAlt);
					this.numResourcesJSON.RegisterText(componentInChildren.numResourcesText, isAlt);
					this.firstPageAction.RegisterButton(componentInChildren.firstPageButton, isAlt);
					this.previousPageAction.RegisterButton(componentInChildren.previousPageButton, isAlt);
					this.nextPageAction.RegisterButton(componentInChildren.nextPageButton, isAlt);
					this.refreshResourcesAction.RegisterButton(componentInChildren.refreshButton, isAlt);
					this.clearFiltersAction.RegisterButton(componentInChildren.clearFiltersButton, isAlt);
					this.hostedOptionChooser.RegisterPopup(componentInChildren.hostedOptionPopup, isAlt);
					this.searchFilterJSON.RegisterInputField(componentInChildren.searchInputField, isAlt);
					this.payTypeFilterChooser.RegisterPopup(componentInChildren.payTypeFilterPopup, isAlt);
					this.categoryFilterChooser.RegisterPopup(componentInChildren.categoryFilterPopup, isAlt);
					this.creatorFilterChooser.RegisterPopup(componentInChildren.creatorFilterPopup, isAlt);
					this.tagsFilterChooser.RegisterPopup(componentInChildren.tagsFilterPopup, isAlt);
					this.sortPrimaryChooser.RegisterPopup(componentInChildren.sortPrimaryPopup, isAlt);
					this.sortSecondaryChooser.RegisterPopup(componentInChildren.sortSecondaryPopup, isAlt);
					this.openMissingPackagesPanelAction.RegisterButton(componentInChildren.openMissingPackagesPanelButton, isAlt);
					this.closeMissingPackagesPanelAction.RegisterButton(componentInChildren.closeMissingPackagesPanelButton, isAlt);
					this.downloadAllMissingPackagesAction.RegisterButton(componentInChildren.downloadAllMissingPackagesButton, isAlt);
					this.openUpdatesPanelAction.RegisterButton(componentInChildren.openUpdatesPanelButton, isAlt);
					this.closeUpdatesPanelAction.RegisterButton(componentInChildren.closeUpdatesPanelButton, isAlt);
					this.downloadAllUpdatesAction.RegisterButton(componentInChildren.downloadAllUpdatesButton, isAlt);
					this.isDownloadingJSON.RegisterIndicator(componentInChildren.isDownloadingIndicator, isAlt);
					this.downloadQueuedCountJSON.RegisterText(componentInChildren.downloadQueuedCountText, isAlt);
					this.openDownloadingAction.RegisterButton(componentInChildren.openDownloadingButton, isAlt);
				}
			}
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x002400E3 File Offset: 0x0023E4E3
		protected void OnLoad(ZenFulcrum.EmbeddedBrowser.JSONNode loadData)
		{
			this.browser.EvalJS("\r\n\t\t\t\twindow.scrollTo(0,0);\r\n\t\t\t", "scripted command");
			this.RefreshCookies();
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x00240104 File Offset: 0x0023E504
		protected override void Awake()
		{
			if (!this.awakecalled)
			{
				HubBrowse.singleton = this;
				base.Awake();
				this.Init();
				this.InitUI();
				this.InitUIAlt();
				if (this.browser != null)
				{
					this.browser.onLoad += this.OnLoad;
				}
			}
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x00240162 File Offset: 0x0023E562
		protected void Update()
		{
			if (this.browser != null && this.isWebLoadingIndicator != null)
			{
				this.isWebLoadingIndicator.SetActive(this.browser.IsLoadingRaw);
			}
		}

		// Token: 0x04004EFD RID: 20221
		public static HubBrowse singleton;

		// Token: 0x04004EFE RID: 20222
		public string cookieHost;

		// Token: 0x04004EFF RID: 20223
		public string apiUrl;

		// Token: 0x04004F00 RID: 20224
		public string packagesJSONUrl;

		// Token: 0x04004F01 RID: 20225
		protected bool _hubEnabled;

		// Token: 0x04004F02 RID: 20226
		protected JSONStorableBool hubEnabledJSON;

		// Token: 0x04004F03 RID: 20227
		public HubBrowse.EnableHubCallback enableHubCallbacks;

		// Token: 0x04004F04 RID: 20228
		protected JSONStorableAction enableHubAction;

		// Token: 0x04004F05 RID: 20229
		protected bool _webBrowserEnabled;

		// Token: 0x04004F06 RID: 20230
		protected JSONStorableBool webBrowserEnabledJSON;

		// Token: 0x04004F07 RID: 20231
		public HubBrowse.EnableWebBrowserCallback enableWebBrowserCallbacks;

		// Token: 0x04004F08 RID: 20232
		protected JSONStorableAction enableWebBrowserAction;

		// Token: 0x04004F09 RID: 20233
		protected HubBrowseUI hubBrowseUI;

		// Token: 0x04004F0A RID: 20234
		public RectTransform itemPrefab;

		// Token: 0x04004F0B RID: 20235
		protected RectTransform itemContainer;

		// Token: 0x04004F0C RID: 20236
		protected ScrollRect itemScrollRect;

		// Token: 0x04004F0D RID: 20237
		protected List<HubResourceItemUI> items;

		// Token: 0x04004F0E RID: 20238
		public RectTransform resourceDetailPrefab;

		// Token: 0x04004F0F RID: 20239
		protected GameObject detailPanel;

		// Token: 0x04004F10 RID: 20240
		public RectTransform packageDownloadPrefab;

		// Token: 0x04004F11 RID: 20241
		public RectTransform creatorSupportButtonPrefab;

		// Token: 0x04004F12 RID: 20242
		protected RectTransform resourceDetailContainer;

		// Token: 0x04004F13 RID: 20243
		protected Browser browser;

		// Token: 0x04004F14 RID: 20244
		protected VRWebBrowser webBrowser;

		// Token: 0x04004F15 RID: 20245
		protected GameObject isWebLoadingIndicator;

		// Token: 0x04004F16 RID: 20246
		protected GameObject refreshIndicator;

		// Token: 0x04004F17 RID: 20247
		protected bool _isShowing;

		// Token: 0x04004F18 RID: 20248
		public HubBrowse.PreShowCallback preShowCallbacks;

		// Token: 0x04004F19 RID: 20249
		protected bool _hasBeenRefreshed;

		// Token: 0x04004F1A RID: 20250
		protected Coroutine refreshResourcesRoutine;

		// Token: 0x04004F1B RID: 20251
		protected JSONStorableAction refreshResourcesAction;

		// Token: 0x04004F1C RID: 20252
		protected JSONStorableString numResourcesJSON;

		// Token: 0x04004F1D RID: 20253
		protected JSONStorableString pageInfoJSON;

		// Token: 0x04004F1E RID: 20254
		protected int _numPagesInt;

		// Token: 0x04004F1F RID: 20255
		protected JSONStorableString numPagesJSON;

		// Token: 0x04004F20 RID: 20256
		protected int _numPerPageInt = 48;

		// Token: 0x04004F21 RID: 20257
		protected JSONStorableFloat numPerPageJSON;

		// Token: 0x04004F22 RID: 20258
		protected string _currentPageString = "1";

		// Token: 0x04004F23 RID: 20259
		protected int _currentPageInt = 1;

		// Token: 0x04004F24 RID: 20260
		protected JSONStorableString currentPageJSON;

		// Token: 0x04004F25 RID: 20261
		protected JSONStorableAction firstPageAction;

		// Token: 0x04004F26 RID: 20262
		protected JSONStorableAction previousPageAction;

		// Token: 0x04004F27 RID: 20263
		protected JSONStorableAction nextPageAction;

		// Token: 0x04004F28 RID: 20264
		protected JSONStorableAction clearFiltersAction;

		// Token: 0x04004F29 RID: 20265
		protected string _hostedOption = "Hub And Dependencies";

		// Token: 0x04004F2A RID: 20266
		protected JSONStorableStringChooser hostedOptionChooser;

		// Token: 0x04004F2B RID: 20267
		protected string _payTypeFilter = "Free";

		// Token: 0x04004F2C RID: 20268
		protected JSONStorableStringChooser payTypeFilterChooser;

		// Token: 0x04004F2D RID: 20269
		protected const float _triggerDelay = 0.5f;

		// Token: 0x04004F2E RID: 20270
		protected float triggerCountdown;

		// Token: 0x04004F2F RID: 20271
		protected Coroutine triggerResetRefreshRoutine;

		// Token: 0x04004F30 RID: 20272
		protected string _minLengthSearchFilter = string.Empty;

		// Token: 0x04004F31 RID: 20273
		protected string _searchFilter = string.Empty;

		// Token: 0x04004F32 RID: 20274
		protected JSONStorableString searchFilterJSON;

		// Token: 0x04004F33 RID: 20275
		protected string _categoryFilter = "All";

		// Token: 0x04004F34 RID: 20276
		protected JSONStorableStringChooser categoryFilterChooser;

		// Token: 0x04004F35 RID: 20277
		protected string _creatorFilter = "All";

		// Token: 0x04004F36 RID: 20278
		protected JSONStorableStringChooser creatorFilterChooser;

		// Token: 0x04004F37 RID: 20279
		protected string _tagsFilter = "All";

		// Token: 0x04004F38 RID: 20280
		protected JSONStorableStringChooser tagsFilterChooser;

		// Token: 0x04004F39 RID: 20281
		protected string _sortPrimary = "Latest Update";

		// Token: 0x04004F3A RID: 20282
		protected JSONStorableStringChooser sortPrimaryChooser;

		// Token: 0x04004F3B RID: 20283
		protected string _sortSecondary = "None";

		// Token: 0x04004F3C RID: 20284
		protected JSONStorableStringChooser sortSecondaryChooser;

		// Token: 0x04004F3D RID: 20285
		protected Dictionary<string, HubResourceItemDetailUI> savedResourceDetailsPanels;

		// Token: 0x04004F3E RID: 20286
		protected Stack<HubResourceItemDetailUI> resourceDetailStack;

		// Token: 0x04004F3F RID: 20287
		public PackageBuilder packageManager;

		// Token: 0x04004F40 RID: 20288
		protected GameObject missingPackagesPanel;

		// Token: 0x04004F41 RID: 20289
		protected RectTransform missingPackagesContainer;

		// Token: 0x04004F42 RID: 20290
		protected List<string> checkMissingPackageNames;

		// Token: 0x04004F43 RID: 20291
		protected List<HubResourcePackageUI> missingPackages;

		// Token: 0x04004F44 RID: 20292
		protected JSONStorableAction openMissingPackagesPanelAction;

		// Token: 0x04004F45 RID: 20293
		protected JSONStorableAction closeMissingPackagesPanelAction;

		// Token: 0x04004F46 RID: 20294
		protected JSONStorableAction downloadAllMissingPackagesAction;

		// Token: 0x04004F47 RID: 20295
		protected GameObject updatesPanel;

		// Token: 0x04004F48 RID: 20296
		protected RectTransform updatesContainer;

		// Token: 0x04004F49 RID: 20297
		protected List<string> checkUpdateNames;

		// Token: 0x04004F4A RID: 20298
		protected List<HubResourcePackageUI> updates;

		// Token: 0x04004F4B RID: 20299
		protected Dictionary<string, int> packageGroupToLatestVersion;

		// Token: 0x04004F4C RID: 20300
		protected Dictionary<string, string> packageIdToResourceId;

		// Token: 0x04004F4D RID: 20301
		protected JSONStorableAction openUpdatesPanelAction;

		// Token: 0x04004F4E RID: 20302
		protected JSONStorableAction closeUpdatesPanelAction;

		// Token: 0x04004F4F RID: 20303
		protected JSONStorableAction downloadAllUpdatesAction;

		// Token: 0x04004F50 RID: 20304
		protected JSONStorableBool isDownloadingJSON;

		// Token: 0x04004F51 RID: 20305
		protected JSONStorableString downloadQueuedCountJSON;

		// Token: 0x04004F52 RID: 20306
		protected Queue<HubBrowse.DownloadRequest> downloadQueue;

		// Token: 0x04004F53 RID: 20307
		protected List<string> hubCookies;

		// Token: 0x04004F54 RID: 20308
		protected Coroutine GetBrowserCookiesRoutine;

		// Token: 0x04004F55 RID: 20309
		protected JSONStorableAction openDownloadingAction;

		// Token: 0x04004F56 RID: 20310
		protected RectTransform refreshingGetInfoPanel;

		// Token: 0x04004F57 RID: 20311
		protected RectTransform failedGetInfoPanel;

		// Token: 0x04004F58 RID: 20312
		protected Text getInfoErrorText;

		// Token: 0x04004F59 RID: 20313
		protected bool hubInfoSuccess;

		// Token: 0x04004F5A RID: 20314
		protected bool hubInfoCompleted;

		// Token: 0x04004F5B RID: 20315
		protected bool hubInfoRefreshing;

		// Token: 0x04004F5C RID: 20316
		protected Coroutine hubInfoCoroutine;

		// Token: 0x04004F5D RID: 20317
		protected JSONStorableAction cancelGetHubInfoAction;

		// Token: 0x04004F5E RID: 20318
		protected JSONStorableAction retryGetHubInfoAction;

		// Token: 0x02000C84 RID: 3204
		// (Invoke) Token: 0x06005F6D RID: 24429
		public delegate void BinaryRequestStartedCallback();

		// Token: 0x02000C85 RID: 3205
		// (Invoke) Token: 0x06005F71 RID: 24433
		public delegate void BinaryRequestSuccessCallback(byte[] data, Dictionary<string, string> responseHeaders);

		// Token: 0x02000C86 RID: 3206
		// (Invoke) Token: 0x06005F75 RID: 24437
		public delegate void RequestSuccessCallback(SimpleJSON.JSONNode jsonNode);

		// Token: 0x02000C87 RID: 3207
		// (Invoke) Token: 0x06005F79 RID: 24441
		public delegate void RequestErrorCallback(string err);

		// Token: 0x02000C88 RID: 3208
		// (Invoke) Token: 0x06005F7D RID: 24445
		public delegate void RequestProgressCallback(float progress);

		// Token: 0x02000C89 RID: 3209
		// (Invoke) Token: 0x06005F81 RID: 24449
		public delegate void EnableHubCallback();

		// Token: 0x02000C8A RID: 3210
		// (Invoke) Token: 0x06005F85 RID: 24453
		public delegate void EnableWebBrowserCallback();

		// Token: 0x02000C8B RID: 3211
		// (Invoke) Token: 0x06005F89 RID: 24457
		public delegate void PreShowCallback();

		// Token: 0x02000C8C RID: 3212
		protected class DownloadRequest
		{
			// Token: 0x04004F5F RID: 20319
			public string url;

			// Token: 0x04004F60 RID: 20320
			public string promotionalUrl;

			// Token: 0x04004F61 RID: 20321
			public HubBrowse.BinaryRequestStartedCallback startedCallback;

			// Token: 0x04004F62 RID: 20322
			public HubBrowse.RequestProgressCallback progressCallback;

			// Token: 0x04004F63 RID: 20323
			public HubBrowse.BinaryRequestSuccessCallback successCallback;

			// Token: 0x04004F64 RID: 20324
			public HubBrowse.RequestErrorCallback errorCallback;
		}
	}
}
