using SimpleJSON;
using System;
using System.Collections.Generic;

namespace mmd2timeline
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class TimelineJson
	{
		// Token: 0x0400011F RID: 287
		public List<TimelineClipJson> Clips;

		// Token: 0x04000120 RID: 288
		public string AtomType = "Person";
		public JSONClass ToJsonClass()
		{
			JSONClass jSONClass = new JSONClass();
			jSONClass["AtomType"] = AtomType;
			JSONArray jsonArray = new JSONArray();
			foreach (var item in Clips)
			{
				jsonArray.Add(item.ToJsonClass());
			}
			jSONClass["Clips"] = jsonArray;
			return jSONClass;

		}
	}
}
