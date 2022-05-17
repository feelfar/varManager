using System;
using UnityEngine;
using UnityEngine.UI;

namespace MVR.Hub
{
	// Token: 0x02000C94 RID: 3220
	public class HubResourcePackageUI : MonoBehaviour
	{
		// Token: 0x0400502C RID: 20524
		public HubResourcePackage connectedItem;

		// Token: 0x0400502D RID: 20525
		public Button resourceButton;

		// Token: 0x0400502E RID: 20526
		public Text nameText;

		// Token: 0x0400502F RID: 20527
		public Text licenseTypeText;

		// Token: 0x04005030 RID: 20528
		public Text fileSizeText;

		// Token: 0x04005031 RID: 20529
		public GameObject isDependencyIndicator;

		// Token: 0x04005032 RID: 20530
		public GameObject notOnHubIndicator;

		// Token: 0x04005033 RID: 20531
		public GameObject alreadyHaveIndicator;

		// Token: 0x04005034 RID: 20532
		public Button openInPackageManagerButton;

		// Token: 0x04005035 RID: 20533
		public GameObject alreadyHaveSceneIndicator;

		// Token: 0x04005036 RID: 20534
		public Button openSceneButton;

		// Token: 0x04005037 RID: 20535
		public Button downloadButton;

		// Token: 0x04005038 RID: 20536
		public GameObject updateAvailableIndicator;

		// Token: 0x04005039 RID: 20537
		public Button updateButton;

		// Token: 0x0400503A RID: 20538
		public Text updateMsgText;

		// Token: 0x0400503B RID: 20539
		public GameObject isDownloadQueuedIndicator;

		// Token: 0x0400503C RID: 20540
		public GameObject isDownloadingIndicator;

		// Token: 0x0400503D RID: 20541
		public GameObject isDownloadedIndicator;

		// Token: 0x0400503E RID: 20542
		public Slider progressSlider;
	}
}
