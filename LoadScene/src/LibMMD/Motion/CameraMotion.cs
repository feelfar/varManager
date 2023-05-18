using System;
using System.Collections.Generic;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000016 RID: 22
	public class CameraMotion
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000714E File Offset: 0x0000534E
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00007156 File Offset: 0x00005356
		public List<KeyValuePair<int, CameraKeyframe>> KeyFrames { get; set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x00007160 File Offset: 0x00005360
		public CameraPose GetCameraPoseByFrame(int frame)
		{
			if (this.KeyFrames.Count == 0)
			{
				return null;
			}
			if ((float)this.KeyFrames[0].Key >= frame)
			{
				return CameraMotion.CameraKeyFrameToCameraPose(this.KeyFrames[0].Value);
			}
			if ((float)this.KeyFrames[this.KeyFrames.Count - 1].Key <= frame)
			{
				return CameraMotion.CameraKeyFrameToCameraPose(this.KeyFrames[this.KeyFrames.Count - 1].Value);
			}
			KeyValuePair<int, CameraKeyframe> item = new KeyValuePair<int, CameraKeyframe>((int)frame, null);
			int num = this.KeyFrames.BinarySearch(item, CameraMotion.CameraKeyframeSearchComparator.Instance);
			if (num < 0)
			{
				num = ~num;
			}
			int index;
			if (num == 0)
			{
				index = 0;
			}
			else if (num >= this.KeyFrames.Count)
			{
				index = (num = this.KeyFrames.Count - 1);
			}
			else
			{
				index = num - 1;
			}
			KeyValuePair<int, CameraKeyframe> keyValuePair = this.KeyFrames[num];
			int key = keyValuePair.Key;
			CameraKeyframe value = keyValuePair.Value;
			KeyValuePair<int, CameraKeyframe> keyValuePair2 = this.KeyFrames[index];
			int key2 = keyValuePair2.Key;
			CameraKeyframe value2 = keyValuePair2.Value;
			if (key2 == key || key2 == key - 1)
			{
				return CameraMotion.CameraKeyFrameToCameraPose(value2);
			}
			float t = (frame - (float)key2) / (float)(key - key2);
			float[] array = new float[6];
			for (int i = 0; i < 6; i++)
			{
				Vector3 p = new Vector3((float)value2.Interpolation[i * 4], (float)value2.Interpolation[i * 4 + 2]);
				Vector3 p2 = new Vector3((float)value2.Interpolation[i * 4 + 1], (float)value2.Interpolation[i * 4 + 3]);
				array[i] = CameraMotion.CalculBezierPointByTwo(t, p, p2);
			}
			float x = value2.Position.x + array[0] * (value.Position.x - value2.Position.x);
			float y = value2.Position.y + array[1] * (value.Position.y - value2.Position.y);
			float z = value2.Position.z + array[2] * (value.Position.z - value2.Position.z);
			float x2 = value2.Rotation.x + array[3] * (value.Rotation.x - value2.Rotation.x);
			float y2 = value2.Rotation.y + array[3] * (value.Rotation.y - value2.Rotation.y);
			float z2 = value2.Rotation.z + array[3] * (value.Rotation.z - value2.Rotation.z);
			float focalLength = value2.FocalLength + array[4] * (value.FocalLength - value2.FocalLength);
			float fov = value2.Fov + (value.Fov - value2.Fov) * array[5];
			return new CameraPose
			{
				FocalLength = focalLength,
				Fov = fov,
				Orthographic = value2.Orthographic,
				Position = new Vector3(x, y, z),
				Rotation = new Vector3(x2, y2, z2)
			};
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000074AC File Offset: 0x000056AC
		private static CameraPose CameraKeyFrameToCameraPose(CameraKeyframe value)
		{
			return new CameraPose
			{
				FocalLength = value.FocalLength,
				Fov = value.Fov,
				Orthographic = value.Orthographic,
				Position = value.Position,
				Rotation = value.Rotation
			};
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000074FA File Offset: 0x000056FA
		/*
		public CameraPose GetCameraPose(double time)
		{
			return this.GetCameraPoseByFrame((float)(time * 30.0));
		}
		*/
		// Token: 0x060000D5 RID: 213 RVA: 0x0000750E File Offset: 0x0000570E
		private static float CalculBezierPointByTwo(float t, Vector3 p1, Vector3 p2)
		{
			return CameraMotion.CalculateBezierPoint(t, Vector3.zero, p1, p2, new Vector3(127f, 127f, 0f)).y / 127f;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000753C File Offset: 0x0000573C
		private static Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			float num = 1f - t;
			float num2 = t * t;
			float num3 = num * num;
			float d = num3 * num;
			float d2 = num2 * t;
			return d * p0 + 3f * num3 * t * p1 + 3f * num * num2 * p2 + d2 * p3;
		}

		// Token: 0x0200004D RID: 77
		private class CameraKeyframeSearchComparator : IComparer<KeyValuePair<int, CameraKeyframe>>
		{
			// Token: 0x060002D3 RID: 723 RVA: 0x00010894 File Offset: 0x0000EA94
			private CameraKeyframeSearchComparator()
			{
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0001089C File Offset: 0x0000EA9C
			public int Compare(KeyValuePair<int, CameraKeyframe> x, KeyValuePair<int, CameraKeyframe> y)
			{
				return x.Key.CompareTo(y.Key);
			}

			// Token: 0x0400019F RID: 415
			public static readonly CameraMotion.CameraKeyframeSearchComparator Instance = new CameraMotion.CameraKeyframeSearchComparator();
		}
	}
}
