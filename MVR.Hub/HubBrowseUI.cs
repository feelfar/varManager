using System;
using UnityEngine;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;

namespace MVR.Hub
{
	// Token: 0x02000C8D RID: 3213
	public class HubBrowseUI : UIProvider
	{
		// Token: 0x04004F65 RID: 20325
		public GameObject hubEnabledNegativeIndicator;

		// Token: 0x04004F66 RID: 20326
		public Button enableHubButton;

		// Token: 0x04004F67 RID: 20327
		public GameObject webBrowserEnabledNegativeIndicator;

		// Token: 0x04004F68 RID: 20328
		public Button enabledWebBrowserButton;

		// Token: 0x04004F69 RID: 20329
		public RectTransform refreshingGetInfoPanel;

		// Token: 0x04004F6A RID: 20330
		public RectTransform failedGetInfoPanel;

		// Token: 0x04004F6B RID: 20331
		public Text getInfoErrorText;

		// Token: 0x04004F6C RID: 20332
		public Button cancelGetHubInfoButton;

		// Token: 0x04004F6D RID: 20333
		public Button retryGetHubInfoButton;

		// Token: 0x04004F6E RID: 20334
		public RectTransform itemContainer;

		// Token: 0x04004F6F RID: 20335
		public ScrollRect itemScrollRect;

		// Token: 0x04004F70 RID: 20336
		public GameObject refreshIndicator;

		// Token: 0x04004F71 RID: 20337
		public Button refreshButton;

		// Token: 0x04004F72 RID: 20338
		public Text numResourcesText;

		// Token: 0x04004F73 RID: 20339
		public Text pageInfoText;

		// Token: 0x04004F74 RID: 20340
		public Button firstPageButton;

		// Token: 0x04004F75 RID: 20341
		public Button previousPageButton;

		// Token: 0x04004F76 RID: 20342
		public Button nextPageButton;

		// Token: 0x04004F77 RID: 20343
		public Button clearFiltersButton;

		// Token: 0x04004F78 RID: 20344
		public UIPopup hostedOptionPopup;

		// Token: 0x04004F79 RID: 20345
		public UIPopup payTypeFilterPopup;

		// Token: 0x04004F7A RID: 20346
		public UIPopup categoryFilterPopup;

		// Token: 0x04004F7B RID: 20347
		public UIPopup creatorFilterPopup;

		// Token: 0x04004F7C RID: 20348
		public UIPopup tagsFilterPopup;

		// Token: 0x04004F7D RID: 20349
		public InputField searchInputField;

		// Token: 0x04004F7E RID: 20350
		public Toggle searchAllToggle;

		// Token: 0x04004F7F RID: 20351
		public UIPopup sortPrimaryPopup;

		// Token: 0x04004F80 RID: 20352
		public UIPopup sortSecondaryPopup;

		// Token: 0x04004F81 RID: 20353
		public GameObject detailPanel;

		// Token: 0x04004F82 RID: 20354
		public RectTransform resourceDetailContainer;

		// Token: 0x04004F83 RID: 20355
		public Browser browser;

		// Token: 0x04004F84 RID: 20356
		public VRWebBrowser webBrowser;

		// Token: 0x04004F85 RID: 20357
		public GameObject isWebLoadingIndicator;

		// Token: 0x04004F86 RID: 20358
		public GameObject missingPackagesPanel;

		// Token: 0x04004F87 RID: 20359
		public RectTransform missingPackagesContainer;

		// Token: 0x04004F88 RID: 20360
		public Button openMissingPackagesPanelButton;

		// Token: 0x04004F89 RID: 20361
		public Button closeMissingPackagesPanelButton;

		// Token: 0x04004F8A RID: 20362
		public Button downloadAllMissingPackagesButton;

		// Token: 0x04004F8B RID: 20363
		public GameObject updatesPanel;

		// Token: 0x04004F8C RID: 20364
		public RectTransform updatesContainer;

		// Token: 0x04004F8D RID: 20365
		public Button openUpdatesPanelButton;

		// Token: 0x04004F8E RID: 20366
		public Button closeUpdatesPanelButton;

		// Token: 0x04004F8F RID: 20367
		public Button downloadAllUpdatesButton;

		// Token: 0x04004F90 RID: 20368
		public GameObject isDownloadingIndicator;

		// Token: 0x04004F91 RID: 20369
		public Text downloadQueuedCountText;

		// Token: 0x04004F92 RID: 20370
		public Button openDownloadingButton;
	}
}
