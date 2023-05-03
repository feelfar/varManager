using SimpleJSON;
using System;
using System.Collections.Generic;

namespace mmd2timeline
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class FloatParamsJson
	{
		// Token: 0x04000132 RID: 306
		public string Storable = "geometry";

		// Token: 0x04000133 RID: 307
		public string Name;

		// Token: 0x04000134 RID: 308
		public List<TimelineFrameJson> Value = new List<TimelineFrameJson>();

		// Token: 0x04000135 RID: 309
		public string Min = "0";

		// Token: 0x04000136 RID: 310
		public string Max = "1";

		// Token: 0x04000137 RID: 311
		[NonSerialized]
		public Dictionary<int, TimelineFrameJson> ValueLookup = new Dictionary<int, TimelineFrameJson>();
		
		public JSONClass ToJsonClass()
        {
			JSONClass jSONClass = new JSONClass();
			jSONClass["Storable"] = Storable;
			jSONClass["Name"] = Name;
			jSONClass["Min"] = Min;
			jSONClass["Max"] = Max;
			JSONArray jsonArray = new JSONArray();
            foreach (var item in Value)
            {
				jsonArray.Add(item.ToJsonClass());

			}
			jSONClass["Value"]=jsonArray;
			return jSONClass;

		}
	}
}
