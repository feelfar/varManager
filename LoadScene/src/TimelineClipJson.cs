using SimpleJSON;
using System;
using System.Collections.Generic;

namespace mmd2timeline
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	public class TimelineClipJson
	{
		// Token: 0x04000121 RID: 289
		public string AnimationName;

		// Token: 0x04000122 RID: 290
		public string AnimationLength;

		// Token: 0x04000123 RID: 291
		public string BlendDuration = "1";

		// Token: 0x04000124 RID: 292
		public string Loop = "1";

		// Token: 0x04000125 RID: 293
		public string NextAnimationRandomizeWeight = "1";

		// Token: 0x04000126 RID: 294
		public string AutoTransitionPrevious = "0";

		// Token: 0x04000127 RID: 295
		public string AutoTransitionNext = "0";

		// Token: 0x04000128 RID: 296
		public string SyncTransitionTime = "1";

		// Token: 0x04000129 RID: 297
		public string SyncTransitionTimeNL = "0";

		// Token: 0x0400012A RID: 298
		public string EnsureQuaternionContinuity = "1";

		// Token: 0x0400012B RID: 299
		public string AnimationLayer = "Main";

		// Token: 0x0400012C RID: 300
		public string Speed = "1";

		// Token: 0x0400012D RID: 301
		public string Weight = "1";

		// Token: 0x0400012E RID: 302
		public string Uninterruptible = "0";

		// Token: 0x0400012F RID: 303
		public string AnimationSegment = "Segment 1";

		public string AudioSourceControl = "";

		// Token: 0x04000130 RID: 304
		public List<TimelineControlJson> Controllers;

		// Token: 0x04000131 RID: 305
		public List<FloatParamsJson> FloatParams;
		public JSONClass ToJsonClass()
		{
			JSONClass jSONClass = new JSONClass();
			jSONClass["AnimationName"] = AnimationName;
			jSONClass["AnimationLength"] = AnimationLength;
			jSONClass["BlendDuration"] = BlendDuration;
			jSONClass["Loop"] = Loop;
			jSONClass["NextAnimationRandomizeWeight"] = NextAnimationRandomizeWeight;
			jSONClass["AutoTransitionPrevious"] = AutoTransitionPrevious;
			jSONClass["AutoTransitionNext"] = AutoTransitionNext;
			jSONClass["SyncTransitionTime"] = SyncTransitionTime;
			jSONClass["SyncTransitionTimeNL"] = SyncTransitionTimeNL;
			jSONClass["EnsureQuaternionContinuity"] = EnsureQuaternionContinuity;
			jSONClass["AnimationLayer"] = AnimationLayer;
			jSONClass["Speed"] = Speed;
			jSONClass["Weight"] = Weight;
			jSONClass["Uninterruptible"] = Uninterruptible;
			jSONClass["AnimationSegment"] = AnimationSegment;
            if (!string.IsNullOrEmpty(AudioSourceControl))
            {
				jSONClass["AudioSourceControl"] = AudioSourceControl;
            }
			JSONArray jsonArray = new JSONArray();
			foreach (var item in Controllers)
			{
				jsonArray.Add(item.ToJsonClass());
			}
			jSONClass["Controllers"] = jsonArray;

			JSONArray jsonArray2 = new JSONArray();
			foreach (var item in FloatParams)
			{
				jsonArray2.Add(item.ToJsonClass());
			}
			jSONClass["FloatParams"] = jsonArray2;
			return jSONClass;

		}
	}
}
