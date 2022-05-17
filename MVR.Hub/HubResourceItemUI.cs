using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVR.Hub
{
	// Token: 0x02000C92 RID: 3218
	public class HubResourceItemUI : MonoBehaviour
	{
		// Token: 0x04004FF2 RID: 20466
		public HubResourceItem connectedItem;

		// Token: 0x04004FF3 RID: 20467
		public Text titleText;

		// Token: 0x04004FF4 RID: 20468
		public Text tagLineText;

		// Token: 0x04004FF5 RID: 20469
		public Text versionText;

		// Token: 0x04004FF6 RID: 20470
		public Text payTypeText;

		// Token: 0x04004FF7 RID: 20471
		public Text categoryText;

		// Token: 0x04004FF8 RID: 20472
		public Button payTypeAndCategorySelectButton;

		// Token: 0x04004FF9 RID: 20473
		public Button creatorSelectButton;

		// Token: 0x04004FFA RID: 20474
		public Text creatorText;

		// Token: 0x04004FFB RID: 20475
		public RawImage creatorIconImage;

		// Token: 0x04004FFC RID: 20476
		public RawImage thumbnailImage;

		// Token: 0x04004FFD RID: 20477
		public GameObject hubDownloadableIndicator;

		// Token: 0x04004FFE RID: 20478
		public GameObject hubDownloadableNegativeIndicator;

		// Token: 0x04004FFF RID: 20479
		public GameObject hubHostedIndicator;

		// Token: 0x04005000 RID: 20480
		public GameObject hubHostedNegativeIndicator;

		// Token: 0x04005001 RID: 20481
		public GameObject hasDependenciesIndicator;

		// Token: 0x04005002 RID: 20482
		public GameObject hasDependenciesNegativeIndicator;

		// Token: 0x04005003 RID: 20483
		public GameObject inLibraryIndicator;

		// Token: 0x04005004 RID: 20484
		public GameObject updateAvailableIndicator;

		// Token: 0x04005005 RID: 20485
		public Text updateMsgText;

		// Token: 0x04005006 RID: 20486
		public Text dependencyCountText;

		// Token: 0x04005007 RID: 20487
		public Text downloadCountText;

		// Token: 0x04005008 RID: 20488
		public Text ratingsCountText;

		// Token: 0x04005009 RID: 20489
		public Slider ratingSlider;

		// Token: 0x0400500A RID: 20490
		public Text lastUpdateText;

		// Token: 0x0400500B RID: 20491
		public Button openDetailButton;
	}
}
