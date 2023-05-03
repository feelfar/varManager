using System;

namespace LibMMD.Material
{
	// Token: 0x02000028 RID: 40
	public class MmdTexture
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A4CF File Offset: 0x000086CF
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000A4D7 File Offset: 0x000086D7
		public string TexturePath { get; set; }

		// Token: 0x060001ED RID: 493 RVA: 0x0000A4E0 File Offset: 0x000086E0
		public MmdTexture(string texturePath)
		{
			this.TexturePath = texturePath;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A4EF File Offset: 0x000086EF
		protected bool Equals(MmdTexture other)
		{
			return string.Equals(this.TexturePath, other.TexturePath);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A502 File Offset: 0x00008702
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj.GetType() == base.GetType() && this.Equals((MmdTexture)obj)));
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A52B File Offset: 0x0000872B
		public override int GetHashCode()
		{
			if (this.TexturePath == null)
			{
				return 0;
			}
			return this.TexturePath.GetHashCode();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A542 File Offset: 0x00008742
		private MmdTexture()
		{
		}
	}
}
