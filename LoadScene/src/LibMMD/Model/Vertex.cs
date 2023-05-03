using System;
using UnityEngine;

namespace LibMMD.Model
{
	// Token: 0x02000026 RID: 38
	public class Vertex
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000A327 File Offset: 0x00008527
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000A32F File Offset: 0x0000852F
		public Vector3 Coordinate { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000A338 File Offset: 0x00008538
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000A340 File Offset: 0x00008540
		public Vector3 Normal { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000A349 File Offset: 0x00008549
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000A351 File Offset: 0x00008551
		public Vector2 UvCoordinate { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000A35A File Offset: 0x0000855A
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000A362 File Offset: 0x00008562
		public Vector4[] ExtraUvCoordinate { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000A36B File Offset: 0x0000856B
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000A373 File Offset: 0x00008573
		public SkinningOperator SkinningOperator { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000A37C File Offset: 0x0000857C
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000A384 File Offset: 0x00008584
		public float EdgeScale { get; set; }
	}
}
