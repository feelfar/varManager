using System;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000013 RID: 19
	public class BoneKeyframe
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00007048 File Offset: 0x00005248
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00007050 File Offset: 0x00005250
		public Vector3 Translation { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00007059 File Offset: 0x00005259
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00007061 File Offset: 0x00005261
		public Quaternion Rotation { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000706A File Offset: 0x0000526A
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00007072 File Offset: 0x00005272
		public Interpolator XInterpolator { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000707B File Offset: 0x0000527B
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00007083 File Offset: 0x00005283
		public Interpolator YInterpolator { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000708C File Offset: 0x0000528C
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00007094 File Offset: 0x00005294
		public Interpolator ZInterpolator { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000709D File Offset: 0x0000529D
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000070A5 File Offset: 0x000052A5
		public Interpolator RInterpolator { get; set; }
	}
}
