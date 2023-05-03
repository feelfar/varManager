using System;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000017 RID: 23
	public class CameraPose
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000075A3 File Offset: 0x000057A3
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000075AB File Offset: 0x000057AB
		public float Fov { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000075B4 File Offset: 0x000057B4
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000075BC File Offset: 0x000057BC
		public float FocalLength { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000075C5 File Offset: 0x000057C5
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000075CD File Offset: 0x000057CD
		public Vector3 Position { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000075D6 File Offset: 0x000057D6
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000075DE File Offset: 0x000057DE
		public Vector3 Rotation { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000075E7 File Offset: 0x000057E7
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000075EF File Offset: 0x000057EF
		public bool Orthographic { get; set; }
	}
}
