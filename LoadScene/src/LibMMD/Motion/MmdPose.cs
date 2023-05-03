using System;
using System.Collections.Generic;

namespace LibMMD.Motion
{
	// Token: 0x0200001A RID: 26
	public class MmdPose
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008645 File Offset: 0x00006845
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000864D File Offset: 0x0000684D
		public string ModelName { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00008656 File Offset: 0x00006856
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000865E File Offset: 0x0000685E
		public Dictionary<string, BonePose> BonePoses
		{
			get
			{
				return this._bonePoses;
			}
			set
			{
				this._bonePoses = value;
			}
		}

		// Token: 0x04000066 RID: 102
		private Dictionary<string, BonePose> _bonePoses = new Dictionary<string, BonePose>();
	}
}
