using System;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000015 RID: 21
	public class CameraKeyframe
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000070E0 File Offset: 0x000052E0
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000070E8 File Offset: 0x000052E8
		public float Fov { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000070F1 File Offset: 0x000052F1
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000070F9 File Offset: 0x000052F9
		public float FocalLength { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007102 File Offset: 0x00005302
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x0000710A File Offset: 0x0000530A
		public Vector3 Position { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00007113 File Offset: 0x00005313
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000711B File Offset: 0x0000531B
		public Vector3 Rotation { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007124 File Offset: 0x00005324
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000712C File Offset: 0x0000532C
		public byte[] Interpolation { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00007135 File Offset: 0x00005335
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000713D File Offset: 0x0000533D
		public bool Orthographic { get; set; }
	}
}
