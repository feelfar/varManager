using System;
using MeshVR.Hands;
using UnityEngine;

namespace mmd2timeline
{
	// Token: 0x02000039 RID: 57
	internal class Utility
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000FE64 File Offset: 0x0000E064
		public static void ResetHandControl(HandControl c)
		{
			Utility.ResetFinger(c.thumbDistalBone);
			Utility.ResetFinger(c.thumbMiddlaBone);
			Utility.ResetFinger(c.thumbProximalBone);
			Utility.ResetFinger(c.indexDistalBone);
			Utility.ResetFinger(c.indexMiddlaBone);
			Utility.ResetFinger(c.indexProximalBone);
			Utility.ResetFinger(c.middleDistalBone);
			Utility.ResetFinger(c.middleMiddlaBone);
			Utility.ResetFinger(c.middleProximalBone);
			Utility.ResetFinger(c.ringDistalBone);
			Utility.ResetFinger(c.ringMiddlaBone);
			Utility.ResetFinger(c.ringProximalBone);
			Utility.ResetFinger(c.pinkyDistalBone);
			Utility.ResetFinger(c.pinkyMiddlaBone);
			Utility.ResetFinger(c.pinkyProximalBone);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000FF16 File Offset: 0x0000E116
		public static void ResetFinger(DAZBone dazBone)
		{
			FingerOutput component = dazBone.GetComponent<FingerOutput>();
			component.currentBend = 0f;
			component.currentSpread = 0f;
			component.currentTwist = 0f;
			component.UpdateOutput();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000FF44 File Offset: 0x0000E144
		public static void RecordController(float time, FreeControllerV3 freeController, TimelineControlJson json)
		{
			Transform transform = freeController.transform;
			TimelineFrameJson timelineFrameJson = new TimelineFrameJson();
			timelineFrameJson.t = time.ToString();
			TimelineFrameJson timelineFrameJson2 = timelineFrameJson;
			Vector3 localPosition = transform.localPosition;
			timelineFrameJson2.v = localPosition.x.ToString();
			timelineFrameJson.c = "3";
			timelineFrameJson.i = timelineFrameJson.v;
			timelineFrameJson.o = timelineFrameJson.v;
			json.X.Add(timelineFrameJson);
			TimelineFrameJson timelineFrameJson3 = new TimelineFrameJson();
			timelineFrameJson3.t = time.ToString();
			TimelineFrameJson timelineFrameJson4 = timelineFrameJson3;
			localPosition = transform.localPosition;
			timelineFrameJson4.v = localPosition.y.ToString();
			timelineFrameJson3.c = "3";
			timelineFrameJson3.i = timelineFrameJson3.v;
			timelineFrameJson3.o = timelineFrameJson3.v;
			json.Y.Add(timelineFrameJson3);
			TimelineFrameJson timelineFrameJson5 = new TimelineFrameJson();
			timelineFrameJson5.t = time.ToString();
			TimelineFrameJson timelineFrameJson6 = timelineFrameJson5;
			localPosition = transform.localPosition;
			timelineFrameJson6.v = localPosition.z.ToString();
			timelineFrameJson5.c = "3";
			timelineFrameJson5.i = timelineFrameJson5.v;
			timelineFrameJson5.o = timelineFrameJson5.v;
			json.Z.Add(timelineFrameJson5);
			TimelineFrameJson timelineFrameJson7 = new TimelineFrameJson();
			timelineFrameJson7.t = time.ToString();
			TimelineFrameJson timelineFrameJson8 = timelineFrameJson7;
			Quaternion localRotation = transform.localRotation;
			timelineFrameJson8.v = localRotation.x.ToString();
			timelineFrameJson7.c = "3";
			timelineFrameJson7.i = timelineFrameJson7.v;
			timelineFrameJson7.o = timelineFrameJson7.v;
			json.RotX.Add(timelineFrameJson7);
			TimelineFrameJson timelineFrameJson9 = new TimelineFrameJson();
			timelineFrameJson9.t = time.ToString();
			TimelineFrameJson timelineFrameJson10 = timelineFrameJson9;
			localRotation = transform.localRotation;
			timelineFrameJson10.v = localRotation.y.ToString();
			timelineFrameJson9.c = "3";
			timelineFrameJson9.i = timelineFrameJson9.v;
			timelineFrameJson9.o = timelineFrameJson9.v;
			json.RotY.Add(timelineFrameJson9);
			TimelineFrameJson timelineFrameJson11 = new TimelineFrameJson();
			timelineFrameJson11.t = time.ToString();
			TimelineFrameJson timelineFrameJson12 = timelineFrameJson11;
			localRotation = transform.localRotation;
			timelineFrameJson12.v = localRotation.z.ToString();
			timelineFrameJson11.c = "3";
			timelineFrameJson11.i = timelineFrameJson11.v;
			timelineFrameJson11.o = timelineFrameJson11.v;
			json.RotZ.Add(timelineFrameJson11);
			TimelineFrameJson timelineFrameJson13 = new TimelineFrameJson();
			timelineFrameJson13.t = time.ToString();
			TimelineFrameJson timelineFrameJson14 = timelineFrameJson13;
			localRotation = transform.localRotation;
			timelineFrameJson14.v = localRotation.w.ToString();
			timelineFrameJson13.c = "3";
			timelineFrameJson13.i = timelineFrameJson13.v;
			timelineFrameJson13.o = timelineFrameJson13.v;
			json.RotW.Add(timelineFrameJson13);
		}

		// Token: 0x04000154 RID: 340
		public static Quaternion quat = new Quaternion(0f, 1f, 0f, 0f);
	}
}
