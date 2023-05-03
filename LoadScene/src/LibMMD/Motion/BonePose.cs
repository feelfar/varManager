using System;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000014 RID: 20
	public class BonePose
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000070B6 File Offset: 0x000052B6
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000070BE File Offset: 0x000052BE
		public Vector3 Translation { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000070C7 File Offset: 0x000052C7
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000070CF File Offset: 0x000052CF
		public Quaternion Rotation { get; set; }

		// Token: 0x04000050 RID: 80
		public bool Only1Frame;
	}
}
