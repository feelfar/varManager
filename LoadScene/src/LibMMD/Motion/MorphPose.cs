using System;

namespace LibMMD.Motion
{
	// Token: 0x0200001C RID: 28
	public class MorphPose
	{
		// Token: 0x06000105 RID: 261 RVA: 0x000086A4 File Offset: 0x000068A4
		public MorphPose()
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000086AC File Offset: 0x000068AC
		public MorphPose(float weight)
		{
			this.Weight = weight;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000086BB File Offset: 0x000068BB
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000086C3 File Offset: 0x000068C3
		public float Weight { get; set; }
	}
}
