using System;
using System.Collections.Generic;
using UnityEngine;

namespace LibMMD.Unity3D
{
	// Token: 0x0200000D RID: 13
	public class Utils
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00005728 File Offset: 0x00003928
		public static void UpdateFloatsToVector3(float[] f, Vector3[] dst)
		{
			int num = f.Length / 3;
			for (int i = 0; i < num; i++)
			{
				int num2 = i * 3;
				dst[i].x = f[num2];
				dst[i].y = f[num2 + 1];
				dst[i].z = f[num2 + 2];
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000577C File Offset: 0x0000397C
		public static void MmdUvToUnityUv(float[] f, Vector2[] dst)
		{
			int num = f.Length / 2;
			for (int i = 0; i < num; i++)
			{
				int num2 = i * 2;
				dst[i].x = f[num2];
				dst[i].y = 1f - f[num2 + 1];
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000057C8 File Offset: 0x000039C8
		public static List<T> ArrayToList<T>(T[] src, int srcIndex, int length)
		{
			List<T> list = new List<T>(length);
			int num = srcIndex + length;
			for (int i = srcIndex; i < num; i++)
			{
				list.Add(src[i]);
			}
			return list;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000057FA File Offset: 0x000039FA
		public static Color FloatRgbaToColor(float[] rgba)
		{
			return new Color(rgba[0], rgba[1], rgba[2], rgba[3]);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00005810 File Offset: 0x00003A10
		public static Color[] Bgra32ToColors(byte[] bytes)
		{
			int num = bytes.Length / 4;
			Color[] array = new Color[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = i * 4;
				array[i] = new Color((float)bytes[num2 + 2] / 255f, (float)bytes[num2 + 1] / 255f, (float)bytes[num2] / 255f, (float)bytes[num2 + 3] / 255f);
			}
			return array;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00005878 File Offset: 0x00003A78
		public static void ClearAllTransformChild(Transform t)
		{
			int childCount = t.childCount;
			for (int i = 0; i < childCount; i++)
			{
				UnityEngine.Object.Destroy(t.GetChild(i).gameObject);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000058AC File Offset: 0x00003AAC
		public static void DisposeIgnoreException(IDisposable disposable)
		{
			if (disposable == null)
			{
				return;
			}
			try
			{
				disposable.Dispose();
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
}
