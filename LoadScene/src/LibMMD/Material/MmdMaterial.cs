using System;
using UnityEngine;

namespace LibMMD.Material
{
	// Token: 0x02000027 RID: 39
	public class MmdMaterial
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A395 File Offset: 0x00008595
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000A39D File Offset: 0x0000859D
		public string Name { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000A3A6 File Offset: 0x000085A6
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000A3AE File Offset: 0x000085AE
		public string NameEn { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000A3B7 File Offset: 0x000085B7
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000A3BF File Offset: 0x000085BF
		public Color DiffuseColor { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000A3C8 File Offset: 0x000085C8
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000A3D0 File Offset: 0x000085D0
		public Color SpecularColor { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000A3D9 File Offset: 0x000085D9
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000A3E1 File Offset: 0x000085E1
		public Color AmbientColor { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000A3EA File Offset: 0x000085EA
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000A3F2 File Offset: 0x000085F2
		public float Shiness { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A3FB File Offset: 0x000085FB
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000A403 File Offset: 0x00008603
		public bool DrawDoubleFace { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000A40C File Offset: 0x0000860C
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000A414 File Offset: 0x00008614
		public bool DrawGroundShadow { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000A41D File Offset: 0x0000861D
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000A425 File Offset: 0x00008625
		public bool CastSelfShadow { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000A42E File Offset: 0x0000862E
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000A436 File Offset: 0x00008636
		public bool DrawSelfShadow { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A43F File Offset: 0x0000863F
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000A447 File Offset: 0x00008647
		public bool DrawEdge { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A450 File Offset: 0x00008650
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000A458 File Offset: 0x00008658
		public Color EdgeColor { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000A461 File Offset: 0x00008661
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000A469 File Offset: 0x00008669
		public float EdgeSize { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000A472 File Offset: 0x00008672
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000A47A File Offset: 0x0000867A
		public MmdTexture Toon { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000A483 File Offset: 0x00008683
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000A48B File Offset: 0x0000868B
		public MmdTexture Texture { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A494 File Offset: 0x00008694
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000A49C File Offset: 0x0000869C
		public MmdTexture SubTexture { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A4A5 File Offset: 0x000086A5
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A4AD File Offset: 0x000086AD
		public MmdMaterial.SubTextureTypeEnum SubTextureType { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A4B6 File Offset: 0x000086B6
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A4BE File Offset: 0x000086BE
		public string MetaInfo { get; set; }

		// Token: 0x02000066 RID: 102
		public enum SubTextureTypeEnum : byte
		{
			// Token: 0x040001F3 RID: 499
			MatSubTexOff,
			// Token: 0x040001F4 RID: 500
			MatSubTexSph,
			// Token: 0x040001F5 RID: 501
			MatSubTexSpa,
			// Token: 0x040001F6 RID: 502
			MatSubTexSub
		}
	}
}
