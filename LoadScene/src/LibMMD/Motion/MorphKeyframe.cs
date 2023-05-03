using System;

namespace LibMMD.Motion
{
	// Token: 0x0200001B RID: 27
	public class MorphKeyframe
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000867A File Offset: 0x0000687A
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00008682 File Offset: 0x00006882
		public float Weight { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000868B File Offset: 0x0000688B
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00008693 File Offset: 0x00006893
		public Interpolator WInterpolator { get; set; }
	}
}
