using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVR.Hub
{
	// Token: 0x02000C91 RID: 3217
	public class HubResourceItemDetailUI : HubResourceItemUI
	{
		// Token: 0x04004FD7 RID: 20439
		public new HubResourceItemDetail connectedItem;

		// Token: 0x04004FD8 RID: 20440
		public Button closeDetailButton;

		// Token: 0x04004FD9 RID: 20441
		public Button closeDetailButtonAlt;

		// Token: 0x04004FDA RID: 20442
		public GameObject hadErrorIndicator;

		// Token: 0x04004FDB RID: 20443
		public Text errorText;

		// Token: 0x04004FDC RID: 20444
		public Button navigateToOverviewButton;

		// Token: 0x04004FDD RID: 20445
		public GameObject hasUpdatesIndicator;

		// Token: 0x04004FDE RID: 20446
		public Text updatesText;

		// Token: 0x04004FDF RID: 20447
		public Button navigateToUpdatesButton;

		// Token: 0x04004FE0 RID: 20448
		public GameObject hasReviewsIndicator;

		// Token: 0x04004FE1 RID: 20449
		public Text reviewsText;

		// Token: 0x04004FE2 RID: 20450
		public Button navigateToReviewsButton;

		// Token: 0x04004FE3 RID: 20451
		public Button navigateToHistoryButton;

		// Token: 0x04004FE4 RID: 20452
		public Button navigateToDiscussionButton;

		// Token: 0x04004FE5 RID: 20453
		public GameObject hasPromotionalLinkIndicator;

		// Token: 0x04004FE6 RID: 20454
		public Text promotionalLinkText;

		// Token: 0x04004FE7 RID: 20455
		public Button navigateToPromotionalLinkButton;

		// Token: 0x04004FE8 RID: 20456
		public PointerEnterExitAction promtionalLinkButtonEnterExitAction;

		// Token: 0x04004FE9 RID: 20457
		public GameObject hasOtherCreatorsIndicator;

		// Token: 0x04004FEA RID: 20458
		public RectTransform creatorSupportContent;

		// Token: 0x04004FEB RID: 20459
		public GameObject hubDownloadableIndicatorAlt;

		// Token: 0x04004FEC RID: 20460
		public GameObject hubDownloadableNegativeIndicatorAlt;

		// Token: 0x04004FED RID: 20461
		public Text externalDownloadUrl;

		// Token: 0x04004FEE RID: 20462
		public Button goToExternalDownloadUrlButton;

		// Token: 0x04004FEF RID: 20463
		public RectTransform packageContent;

		// Token: 0x04004FF0 RID: 20464
		public Button downloadAllButton;

		// Token: 0x04004FF1 RID: 20465
		public GameObject downloadAvailableIndicator;
	}
}
