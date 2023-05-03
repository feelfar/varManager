using System;
using UnityEngine;

namespace LibMMD.Model
{
	// Token: 0x02000023 RID: 35
	public class Morph
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000A265 File Offset: 0x00008465
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000A26D File Offset: 0x0000846D
		public string Name { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000A276 File Offset: 0x00008476
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000A27E File Offset: 0x0000847E
		public string NameEn { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000A287 File Offset: 0x00008487
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000A28F File Offset: 0x0000848F
		public Morph.MorphCategory Category { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000A298 File Offset: 0x00008498
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public Morph.MorphType Type { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000A2A9 File Offset: 0x000084A9
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000A2B1 File Offset: 0x000084B1
		public Morph.MorphData[] MorphDatas { get; set; }

		// Token: 0x02000058 RID: 88
		public enum MorphCategory : byte
		{
			// Token: 0x040001BF RID: 447
			MorphCatSystem,
			// Token: 0x040001C0 RID: 448
			MorphCatEyebrow,
			// Token: 0x040001C1 RID: 449
			MorphCatEye,
			// Token: 0x040001C2 RID: 450
			MorphCatMouth,
			// Token: 0x040001C3 RID: 451
			MorphCatOther
		}

		// Token: 0x02000059 RID: 89
		public enum MorphType : byte
		{
			// Token: 0x040001C5 RID: 453
			MorphTypeGroup,
			// Token: 0x040001C6 RID: 454
			MorphTypeVertex,
			// Token: 0x040001C7 RID: 455
			MorphTypeBone,
			// Token: 0x040001C8 RID: 456
			MorphTypeUv,
			// Token: 0x040001C9 RID: 457
			MorphTypeExtUv1,
			// Token: 0x040001CA RID: 458
			MorphTypeExtUv2,
			// Token: 0x040001CB RID: 459
			MorphTypeExtUv3,
			// Token: 0x040001CC RID: 460
			MorphTypeExtUv4,
			// Token: 0x040001CD RID: 461
			MorphTypeMaterial
		}

		// Token: 0x0200005A RID: 90
		public abstract class MorphData
		{
		}

		// Token: 0x0200005B RID: 91
		public class GroupMorph : Morph.MorphData
		{
			// Token: 0x170000CE RID: 206
			// (get) Token: 0x0600030B RID: 779 RVA: 0x00010BE2 File Offset: 0x0000EDE2
			// (set) Token: 0x0600030C RID: 780 RVA: 0x00010BEA File Offset: 0x0000EDEA
			public int MorphIndex { get; set; }

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600030D RID: 781 RVA: 0x00010BF3 File Offset: 0x0000EDF3
			// (set) Token: 0x0600030E RID: 782 RVA: 0x00010BFB File Offset: 0x0000EDFB
			public float MorphRate { get; set; }
		}

		// Token: 0x0200005C RID: 92
		public class VertexMorph : Morph.MorphData
		{
			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000310 RID: 784 RVA: 0x00010C0C File Offset: 0x0000EE0C
			// (set) Token: 0x06000311 RID: 785 RVA: 0x00010C14 File Offset: 0x0000EE14
			public int VertexIndex { get; set; }

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000312 RID: 786 RVA: 0x00010C1D File Offset: 0x0000EE1D
			// (set) Token: 0x06000313 RID: 787 RVA: 0x00010C25 File Offset: 0x0000EE25
			public Vector3 Offset { get; set; }
		}

		// Token: 0x0200005D RID: 93
		public class BoneMorph : Morph.MorphData
		{
			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000315 RID: 789 RVA: 0x00010C36 File Offset: 0x0000EE36
			// (set) Token: 0x06000316 RID: 790 RVA: 0x00010C3E File Offset: 0x0000EE3E
			public int BoneIndex { get; set; }

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000317 RID: 791 RVA: 0x00010C47 File Offset: 0x0000EE47
			// (set) Token: 0x06000318 RID: 792 RVA: 0x00010C4F File Offset: 0x0000EE4F
			public Vector3 Translation { get; set; }

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000319 RID: 793 RVA: 0x00010C58 File Offset: 0x0000EE58
			// (set) Token: 0x0600031A RID: 794 RVA: 0x00010C60 File Offset: 0x0000EE60
			public Quaternion Rotation { get; set; }
		}

		// Token: 0x0200005E RID: 94
		public class UvMorph : Morph.MorphData
		{
			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600031C RID: 796 RVA: 0x00010C71 File Offset: 0x0000EE71
			// (set) Token: 0x0600031D RID: 797 RVA: 0x00010C79 File Offset: 0x0000EE79
			public int VertexIndex { get; set; }

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600031E RID: 798 RVA: 0x00010C82 File Offset: 0x0000EE82
			// (set) Token: 0x0600031F RID: 799 RVA: 0x00010C8A File Offset: 0x0000EE8A
			public Vector4 Offset { get; set; }
		}

		// Token: 0x0200005F RID: 95
		public class MaterialMorph : Morph.MorphData
		{
			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000321 RID: 801 RVA: 0x00010C9B File Offset: 0x0000EE9B
			// (set) Token: 0x06000322 RID: 802 RVA: 0x00010CA3 File Offset: 0x0000EEA3
			public int MaterialIndex { get; set; }

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000323 RID: 803 RVA: 0x00010CAC File Offset: 0x0000EEAC
			// (set) Token: 0x06000324 RID: 804 RVA: 0x00010CB4 File Offset: 0x0000EEB4
			public bool Global { get; set; }

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000325 RID: 805 RVA: 0x00010CBD File Offset: 0x0000EEBD
			// (set) Token: 0x06000326 RID: 806 RVA: 0x00010CC5 File Offset: 0x0000EEC5
			public Morph.MaterialMorph.MaterialMorphMethod Method { get; set; }

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000327 RID: 807 RVA: 0x00010CCE File Offset: 0x0000EECE
			// (set) Token: 0x06000328 RID: 808 RVA: 0x00010CD6 File Offset: 0x0000EED6
			public Color Diffuse { get; set; }

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000329 RID: 809 RVA: 0x00010CDF File Offset: 0x0000EEDF
			// (set) Token: 0x0600032A RID: 810 RVA: 0x00010CE7 File Offset: 0x0000EEE7
			public Color Specular { get; set; }

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600032B RID: 811 RVA: 0x00010CF0 File Offset: 0x0000EEF0
			// (set) Token: 0x0600032C RID: 812 RVA: 0x00010CF8 File Offset: 0x0000EEF8
			public Color Ambient { get; set; }

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600032D RID: 813 RVA: 0x00010D01 File Offset: 0x0000EF01
			// (set) Token: 0x0600032E RID: 814 RVA: 0x00010D09 File Offset: 0x0000EF09
			public float Shiness { get; set; }

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600032F RID: 815 RVA: 0x00010D12 File Offset: 0x0000EF12
			// (set) Token: 0x06000330 RID: 816 RVA: 0x00010D1A File Offset: 0x0000EF1A
			public Color EdgeColor { get; set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000331 RID: 817 RVA: 0x00010D23 File Offset: 0x0000EF23
			// (set) Token: 0x06000332 RID: 818 RVA: 0x00010D2B File Offset: 0x0000EF2B
			public float EdgeSize { get; set; }

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000333 RID: 819 RVA: 0x00010D34 File Offset: 0x0000EF34
			// (set) Token: 0x06000334 RID: 820 RVA: 0x00010D3C File Offset: 0x0000EF3C
			public Vector4 Texture { get; set; }

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000335 RID: 821 RVA: 0x00010D45 File Offset: 0x0000EF45
			// (set) Token: 0x06000336 RID: 822 RVA: 0x00010D4D File Offset: 0x0000EF4D
			public Vector4 SubTexture { get; set; }

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000337 RID: 823 RVA: 0x00010D56 File Offset: 0x0000EF56
			// (set) Token: 0x06000338 RID: 824 RVA: 0x00010D5E File Offset: 0x0000EF5E
			public Vector4 ToonTexture { get; set; }

			// Token: 0x0200006E RID: 110
			public enum MaterialMorphMethod : byte
			{
				// Token: 0x04000211 RID: 529
				MorphMatMul,
				// Token: 0x04000212 RID: 530
				MorphMatAdd
			}
		}
	}
}
