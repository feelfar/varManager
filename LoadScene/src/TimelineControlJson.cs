using SimpleJSON;
using System;
using System.Collections.Generic;

namespace mmd2timeline
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class TimelineControlJson
	{
		// Token: 0x04000138 RID: 312
		public string Controller;

		// Token: 0x04000139 RID: 313
		public string ControlPosition = "1";

		// Token: 0x0400013A RID: 314
		public string ControlRotation = "1";

		// Token: 0x0400013B RID: 315
		public List<TimelineFrameJson> X = new List<TimelineFrameJson>();

		// Token: 0x0400013C RID: 316
		public List<TimelineFrameJson> Y = new List<TimelineFrameJson>();

		// Token: 0x0400013D RID: 317
		public List<TimelineFrameJson> Z = new List<TimelineFrameJson>();

		// Token: 0x0400013E RID: 318
		public List<TimelineFrameJson> RotX = new List<TimelineFrameJson>();

		// Token: 0x0400013F RID: 319
		public List<TimelineFrameJson> RotY = new List<TimelineFrameJson>();

		// Token: 0x04000140 RID: 320
		public List<TimelineFrameJson> RotZ = new List<TimelineFrameJson>();

		// Token: 0x04000141 RID: 321
		public List<TimelineFrameJson> RotW = new List<TimelineFrameJson>();
		public JSONClass ToJsonClass()
		{
			JSONClass jSONClass = new JSONClass();
			jSONClass["Controller"] = Controller;
			jSONClass["ControlPosition"] = ControlPosition;
			jSONClass["ControlRotation"] = ControlRotation;

			JSONArray jsonArray = new JSONArray();
			foreach (var item in X)
			{
				jsonArray.Add(item.ToJsonClass());
			}
			jSONClass["X"] = jsonArray;

			JSONArray jsonArray2 = new JSONArray();
			foreach (var item in Y)
			{
				jsonArray2.Add(item.ToJsonClass());
			}
			jSONClass["Y"] = jsonArray2;

			JSONArray jsonArray3 = new JSONArray();
			foreach (var item in Z)
			{
				jsonArray3.Add(item.ToJsonClass());
			}
			jSONClass["Z"] = jsonArray3;

			JSONArray jsonArray4 = new JSONArray();
			foreach (var item in RotX)
			{
				jsonArray4.Add(item.ToJsonClass());
			}
			jSONClass["RotX"] = jsonArray4;

			JSONArray jsonArray5 = new JSONArray();
			foreach (var item in RotY)
			{
				jsonArray5.Add(item.ToJsonClass());
			}
			jSONClass["RotY"] = jsonArray5;

			JSONArray jsonArray6 = new JSONArray();
			foreach (var item in RotZ)
			{
				jsonArray6.Add(item.ToJsonClass());
			}
			jSONClass["RotZ"] = jsonArray6;

			JSONArray jsonArray7 = new JSONArray();
			foreach (var item in RotW)
			{
				jsonArray7.Add(item.ToJsonClass());
			}
			jSONClass["RotW"] = jsonArray7;

			return jSONClass;

		}
	}
}
