using SimpleJSON;
using System;

namespace mmd2timeline
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class TimelineFrameJson
	{
		// Token: 0x04000142 RID: 322
		public string t;

		// Token: 0x04000143 RID: 323
		public string v;

		// Token: 0x04000144 RID: 324
		public string c;

		// Token: 0x04000145 RID: 325
		public string i;

		// Token: 0x04000146 RID: 326
		public string o;

		// Token: 0x04000147 RID: 327
		[NonSerialized]
		public int frame;

		// Token: 0x04000148 RID: 328
		[NonSerialized]
		public float value;

		public JSONClass ToJsonClass()
		{
			JSONClass jSONClass = new JSONClass();
			jSONClass["t"] = t;
			jSONClass["v"] = v;
			jSONClass["c"] = c;
			jSONClass["i"] = i;
			jSONClass["o"] = o;
			return jSONClass;

		}
	}
}
