using System;
using System.Collections.Generic;

namespace mmd2timeline
{
	// Token: 0x0200002E RID: 46
	internal class FaceMorph
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public static Dictionary<string, FaceMorph.MorphSetting[]> setting
		{
			get
			{
				if (FaceMorph.s_Setting == null)
				{
					FaceMorph.InitSetting();
				}
				return FaceMorph.s_Setting;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000E9D0 File Offset: 0x0000CBD0
		public static void InitSetting()
		{
			Dictionary<string, FaceMorph.MorphSetting[]> dictionary = new Dictionary<string, FaceMorph.MorphSetting[]>();
			FaceMorph.s_Setting = dictionary;
			dictionary.Add("真面目", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Down", 0f, 1f)
			});
			dictionary.Add("上", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Up", 0f, 1f)
			});
			dictionary.Add("下", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Down", 0f, 1f)
			});
			dictionary.Add("眉頭左", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Up", 0f, 0f),
				new FaceMorph.MorphSetting("Brow Down", 0f, 1f),
				new FaceMorph.MorphSetting("Brow Down Left", 0f, 1f),
				new FaceMorph.MorphSetting("Brow Up Right", 0f, 1f)
			});
			dictionary.Add("眉頭右", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Up", 0f, 0f),
				new FaceMorph.MorphSetting("Brow Down", 0f, 1f),
				new FaceMorph.MorphSetting("Brow Up Left", 0f, 1f),
				new FaceMorph.MorphSetting("Brow Down Right", 0f, 1f)
			});
			dictionary.Add("困る", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Inner Up", 0f, 1f),
				new FaceMorph.MorphSetting("Brow Outer Down", 0f, 1f)
			});
			dictionary.Add("にこり", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Up", 0f, 0.5f),
				new FaceMorph.MorphSetting("Brow Inner Down", 0f, 0.5f)
			});
			dictionary.Add("怒り", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Brow Outer Up", 0f, 1f),
				new FaceMorph.MorphSetting("Brow Inner Down", 0f, 1f)
			});
			dictionary.Add("まばたき", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 1f)
			});
			dictionary.Add("笑顔", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Squint", 0f, 1f)
			});
			dictionary.Add("笑い", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Squint", 0f, 1f)
			});
			dictionary.Add("ウィンク", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Right", 0f, 0.6f)
			});
			dictionary.Add("ウィンク２", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Right", 0f, 0.6f)
			});
			dictionary.Add("ウィンク左", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Right", 0f, 0.6f)
			});
			dictionary.Add("ウィンク右", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Left", 0f, 0.6f)
			});
			dictionary.Add("ウィンク１右", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Left", 0f, 0.6f)
			});
			dictionary.Add("ｳｨﾝｸ２右", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Left", 0f, 0.6f)
			});
			dictionary.Add("ウィンク２右", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0f),
				new FaceMorph.MorphSetting("Eyes Closed Left", 0f, 0.6f)
			});
			dictionary.Add("びっくり", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Squint", 0f, -1f)
			});
			dictionary.Add("じと目", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyes Closed", 0f, 0.1f),
				new FaceMorph.MorphSetting("Eyes Squint", 0f, -0.1f)
			});
			dictionary.Add("下瞼上", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyelids Bottom Up Right", 0f, 1f)
			});
			dictionary.Add("右下瞼上", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Eyelids Bottom Up Left", 0f, 1f)
			});
			dictionary.Add("あ", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("AA", 0f, 1f),
				new FaceMorph.MorphSetting("Mouth Open", 0f, 0.5f)
			});
			dictionary.Add("あ２", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("AA", 0f, 1f),
				new FaceMorph.MorphSetting("Mouth Open", 0f, 0.6f)
			});
			dictionary.Add("い", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("IY", 0f, 1f)
			});
			dictionary.Add("う", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("UW", 0f, 0.5f)
			});
			dictionary.Add("え", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("EH", 0f, 1f),
				new FaceMorph.MorphSetting("Mouth Open", 0f, 0.3f)
			});
			dictionary.Add("お", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("OW", 0f, 0.5f),
				new FaceMorph.MorphSetting("Mouth Open", 0f, 0.3f)
			});
			dictionary.Add("にやり", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Smile Simple", 0f, 1f)
			});
			dictionary.Add("にやり２", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Smile", 0f, 1f)
			});
			dictionary.Add("口横広げ", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, -1f)
			});
			dictionary.Add("ん", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, -1f),
				new FaceMorph.MorphSetting("Mouth Narrow", 0f, 0.5f)
			});
			dictionary.Add("w", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, -1f),
				new FaceMorph.MorphSetting("Mouth Corner Up-Down", 0f, 1f)
			});
			dictionary.Add("oms", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, 1f),
				new FaceMorph.MorphSetting("Mouth Corner Up-Down", 0f, 1f)
			});
			dictionary.Add("口角上げ", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, -1f),
				new FaceMorph.MorphSetting("Mouth Corner Up-Down", 0f, 1f)
			});
			dictionary.Add("口角下げ", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, 0f),
				new FaceMorph.MorphSetting("Mouth Corner Up-Down", 0f, 1f)
			});
			dictionary.Add("∧", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, -1f),
				new FaceMorph.MorphSetting("Mouth Corner Up-Down", 0f, 0f),
				new FaceMorph.MorphSetting("Mouth Narrow", 0f, 0.8f)
			});
			dictionary.Add("▲", new FaceMorph.MorphSetting[]
			{
				new FaceMorph.MorphSetting("Mouth Open", 0f, 0f),
				new FaceMorph.MorphSetting("Mouth Corner Up-Down", 0f, 0f),
				new FaceMorph.MorphSetting("Mouth Narrow", 0f, 0.8f)
			});
		}

		// Token: 0x0400011C RID: 284
		public static Dictionary<string, FaceMorph.MorphSetting[]> s_Setting;

		// Token: 0x0200006A RID: 106
		public class MorphSetting
		{
			// Token: 0x06000362 RID: 866 RVA: 0x00011DB3 File Offset: 0x0000FFB3
			public MorphSetting(string p0, float p1 = 0f, float p2 = 1f)
			{
				this.name = p0;
				this.min = p1;
				this.max = p2;
			}

			// Token: 0x04000208 RID: 520
			public string name;

			// Token: 0x04000209 RID: 521
			public float min;

			// Token: 0x0400020A RID: 522
			public float max;
		}
	}
}
