using System;
using UnityEngine;

namespace LibMMD.Model
{
	// Token: 0x02000025 RID: 37
	public class SkinningOperator
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000A2FD File Offset: 0x000084FD
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000A305 File Offset: 0x00008505
		public SkinningOperator.SkinningType Type { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000A30E File Offset: 0x0000850E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000A316 File Offset: 0x00008516
		public SkinningOperator.SkinningParam Param { get; set; }

		// Token: 0x02000060 RID: 96
		public enum SkinningType : byte
		{
			// Token: 0x040001E4 RID: 484
			SkinningBdef1,
			// Token: 0x040001E5 RID: 485
			SkinningBdef2,
			// Token: 0x040001E6 RID: 486
			SkinningBdef4,
			// Token: 0x040001E7 RID: 487
			SkinningSdef
		}

		// Token: 0x02000061 RID: 97
		public abstract class SkinningParam
		{
		}

		// Token: 0x02000062 RID: 98
		public class Bdef1 : SkinningOperator.SkinningParam
		{
			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x0600033B RID: 827 RVA: 0x00010D77 File Offset: 0x0000EF77
			// (set) Token: 0x0600033C RID: 828 RVA: 0x00010D7F File Offset: 0x0000EF7F
			public int BoneId { get; set; }
		}

		// Token: 0x02000063 RID: 99
		public class Bdef2 : SkinningOperator.SkinningParam
		{
			// Token: 0x0600033E RID: 830 RVA: 0x00010D90 File Offset: 0x0000EF90
			public Bdef2()
			{
				this.BoneId = new int[2];
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600033F RID: 831 RVA: 0x00010DA4 File Offset: 0x0000EFA4
			// (set) Token: 0x06000340 RID: 832 RVA: 0x00010DAC File Offset: 0x0000EFAC
			public int[] BoneId { get; set; }

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000341 RID: 833 RVA: 0x00010DB5 File Offset: 0x0000EFB5
			// (set) Token: 0x06000342 RID: 834 RVA: 0x00010DBD File Offset: 0x0000EFBD
			public float BoneWeight { get; set; }
		}

		// Token: 0x02000064 RID: 100
		public class Bdef4 : SkinningOperator.SkinningParam
		{
			// Token: 0x06000343 RID: 835 RVA: 0x00010DC6 File Offset: 0x0000EFC6
			public Bdef4()
			{
				this.BoneId = new int[4];
				this.BoneWeight = new float[4];
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x06000344 RID: 836 RVA: 0x00010DE6 File Offset: 0x0000EFE6
			// (set) Token: 0x06000345 RID: 837 RVA: 0x00010DEE File Offset: 0x0000EFEE
			public int[] BoneId { get; set; }

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000346 RID: 838 RVA: 0x00010DF7 File Offset: 0x0000EFF7
			// (set) Token: 0x06000347 RID: 839 RVA: 0x00010DFF File Offset: 0x0000EFFF
			public float[] BoneWeight { get; set; }
		}

		// Token: 0x02000065 RID: 101
		public class Sdef : SkinningOperator.SkinningParam
		{
			// Token: 0x06000348 RID: 840 RVA: 0x00010E08 File Offset: 0x0000F008
			public Sdef()
			{
				this.BoneId = new int[2];
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000349 RID: 841 RVA: 0x00010E1C File Offset: 0x0000F01C
			// (set) Token: 0x0600034A RID: 842 RVA: 0x00010E24 File Offset: 0x0000F024
			public int[] BoneId { get; set; }

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x0600034B RID: 843 RVA: 0x00010E2D File Offset: 0x0000F02D
			// (set) Token: 0x0600034C RID: 844 RVA: 0x00010E35 File Offset: 0x0000F035
			public float BoneWeight { get; set; }

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x0600034D RID: 845 RVA: 0x00010E3E File Offset: 0x0000F03E
			// (set) Token: 0x0600034E RID: 846 RVA: 0x00010E46 File Offset: 0x0000F046
			public Vector3 C { get; set; }

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600034F RID: 847 RVA: 0x00010E4F File Offset: 0x0000F04F
			// (set) Token: 0x06000350 RID: 848 RVA: 0x00010E57 File Offset: 0x0000F057
			public Vector3 R0 { get; set; }

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000351 RID: 849 RVA: 0x00010E60 File Offset: 0x0000F060
			// (set) Token: 0x06000352 RID: 850 RVA: 0x00010E68 File Offset: 0x0000F068
			public Vector3 R1 { get; set; }
		}
	}
}
