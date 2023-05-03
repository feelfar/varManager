using System;
using LibMMD.Material;

namespace LibMMD.Model
{
	// Token: 0x02000024 RID: 36
	public class Part
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000A2C2 File Offset: 0x000084C2
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000A2CA File Offset: 0x000084CA
		public MmdMaterial Material { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000A2D3 File Offset: 0x000084D3
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000A2DB File Offset: 0x000084DB
		public int BaseShift { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A2E4 File Offset: 0x000084E4
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000A2EC File Offset: 0x000084EC
		public int TriangleIndexNum { get; set; }
	}
}
