using System;
using UnityEngine;

namespace LibMMD.Model
{
	// Token: 0x02000020 RID: 32
	public class Constraint
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00009DC1 File Offset: 0x00007FC1
		public Constraint()
		{
			this.AssociatedRigidBodyIndex = new int[2];
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00009DD5 File Offset: 0x00007FD5
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00009DDD File Offset: 0x00007FDD
		public string Name { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00009DE6 File Offset: 0x00007FE6
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00009DEE File Offset: 0x00007FEE
		public string NameEn { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00009DF7 File Offset: 0x00007FF7
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00009DFF File Offset: 0x00007FFF
		public int[] AssociatedRigidBodyIndex { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00009E08 File Offset: 0x00008008
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00009E10 File Offset: 0x00008010
		public Vector3 Position { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009E19 File Offset: 0x00008019
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00009E21 File Offset: 0x00008021
		public Vector3 Rotation { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00009E2A File Offset: 0x0000802A
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00009E32 File Offset: 0x00008032
		public Vector3 PositionLowLimit { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00009E3B File Offset: 0x0000803B
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00009E43 File Offset: 0x00008043
		public Vector3 PositionHiLimit { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00009E4C File Offset: 0x0000804C
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00009E54 File Offset: 0x00008054
		public Vector3 RotationLowLimit { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00009E5D File Offset: 0x0000805D
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00009E65 File Offset: 0x00008065
		public Vector3 RotationHiLimit { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009E6E File Offset: 0x0000806E
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00009E76 File Offset: 0x00008076
		public Vector3 SpringTranslate { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00009E7F File Offset: 0x0000807F
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00009E87 File Offset: 0x00008087
		public Vector3 SpringRotate { get; set; }
	}
}
