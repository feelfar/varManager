using System;
using UnityEngine;

namespace LibMMD.Util
{
	// Token: 0x02000006 RID: 6
	public static class MathUtil
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00003648 File Offset: 0x00001848
		public static Matrix4x4 Matrix4X4MuliplyFloat(Matrix4x4 mat, float val)
		{
			return new Matrix4x4
			{
				m00 = mat.m00 * val,
				m01 = mat.m01 * val,
				m02 = mat.m02 * val,
				m03 = mat.m03 * val,
				m10 = mat.m10 * val,
				m11 = mat.m11 * val,
				m12 = mat.m12 * val,
				m13 = mat.m13 * val,
				m20 = mat.m20 * val,
				m21 = mat.m21 * val,
				m22 = mat.m22 * val,
				m23 = mat.m23 * val,
				m30 = mat.m30 * val,
				m31 = mat.m31 * val,
				m32 = mat.m32 * val,
				m33 = mat.m33 * val
			};
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003750 File Offset: 0x00001950
		public static Matrix4x4 Matrix4X4Add(Matrix4x4 mat1, Matrix4x4 mat2)
		{
			return new Matrix4x4
			{
				m00 = mat1.m00 + mat2.m00,
				m01 = mat1.m01 + mat2.m01,
				m02 = mat1.m02 + mat2.m02,
				m03 = mat1.m03 + mat2.m03,
				m10 = mat1.m10 + mat2.m10,
				m11 = mat1.m11 + mat2.m11,
				m12 = mat1.m12 + mat2.m12,
				m13 = mat1.m13 + mat2.m13,
				m20 = mat1.m20 + mat2.m20,
				m21 = mat1.m21 + mat2.m21,
				m22 = mat1.m22 + mat2.m22,
				m23 = mat1.m23 + mat2.m23,
				m30 = mat1.m30 + mat2.m30,
				m31 = mat1.m31 + mat2.m31,
				m32 = mat1.m32 + mat2.m32,
				m33 = mat1.m33 + mat2.m33
			};
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000038A6 File Offset: 0x00001AA6
		public static Matrix4x4 QuaternionToMatrix4X4(Quaternion quaternion)
		{
			return Matrix4x4.TRS(Vector3.zero, quaternion, Vector3.one);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000038B8 File Offset: 0x00001AB8
		public static Vector3 GetTransFromMatrix4X4(Matrix4x4 mat)
		{
			return new Vector3(mat.m03, mat.m13, mat.m23);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000038D1 File Offset: 0x00001AD1
		public static void SetTransToMatrix4X4(Vector3 trans, ref Matrix4x4 mat)
		{
			mat.m03 = trans[0];
			mat.m13 = trans[1];
			mat.m23 = trans[2];
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000038FD File Offset: 0x00001AFD
		public static Vector3 Matrix4x4ColDowngrade(Matrix4x4 mat, int col)
		{
			return new Vector3(mat[0, col], mat[1, col], mat[2, col]);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003920 File Offset: 0x00001B20
		public static Vector3 QuaternionToXyz(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.x;
			float num2 = quaternion.y * quaternion.y;
			float num3 = quaternion.z * quaternion.z;
			float num4 = quaternion.w * quaternion.x;
			float num5 = quaternion.w * quaternion.y;
			float num6 = quaternion.w * quaternion.z;
			float num7 = quaternion.x * quaternion.y;
			float num8 = quaternion.x * quaternion.z;
			float num9 = quaternion.y * quaternion.z;
			Vector3 result;
			result.x = (float)Math.Atan2((double)(2f * (num4 - num9)), (double)(1f - 2f * (num + num2)));
			result.y = (float)Math.Asin((double)(2f * (num5 + num8)));
			result.z = (float)Math.Atan2((double)(2f * (num6 - num7)), (double)(1f - 2f * (num2 + num3)));
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003A1C File Offset: 0x00001C1C
		public static Vector3 QuaternionToXzy(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.x;
			float num2 = quaternion.y * quaternion.y;
			float num3 = quaternion.z * quaternion.z;
			float num4 = quaternion.w * quaternion.x;
			float num5 = quaternion.w * quaternion.y;
			float num6 = quaternion.w * quaternion.z;
			float num7 = quaternion.x * quaternion.y;
			float num8 = quaternion.x * quaternion.z;
			float num9 = quaternion.y * quaternion.z;
			Vector3 result;
			result.x = (float)Math.Atan2((double)(2f * (num4 + num9)), (double)(1f - 2f * (num + num3)));
			result.y = (float)Math.Atan2((double)(2f * (num5 + num8)), (double)(1f - 2f * (num2 + num3)));
			result.z = (float)Math.Asin((double)(2f * (num6 - num7)));
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003B18 File Offset: 0x00001D18
		public static Vector3 QuaternionToYxz(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.x;
			float num2 = quaternion.y * quaternion.y;
			float num3 = quaternion.z * quaternion.z;
			float num4 = quaternion.w * quaternion.x;
			float num5 = quaternion.w * quaternion.y;
			float num6 = quaternion.w * quaternion.z;
			float num7 = quaternion.x * quaternion.y;
			float num8 = quaternion.x * quaternion.z;
			float num9 = quaternion.y * quaternion.z;
			Vector3 result;
			result.x = (float)Math.Asin((double)(2f * (num4 - num9)));
			result.y = (float)Math.Atan2((double)(2f * (num5 + num8)), (double)(1f - 2f * (num + num2)));
			result.z = (float)Math.Atan2((double)(2f * (num6 + num7)), (double)(1f - 2f * (num + num3)));
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003C14 File Offset: 0x00001E14
		public static Vector3 QuaternionToYzx(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.x;
			float num2 = quaternion.y * quaternion.y;
			float num3 = quaternion.z * quaternion.z;
			float num4 = quaternion.w * quaternion.x;
			float num5 = quaternion.w * quaternion.y;
			float num6 = quaternion.w * quaternion.z;
			float num7 = quaternion.x * quaternion.y;
			float num8 = quaternion.x * quaternion.z;
			float num9 = quaternion.y * quaternion.z;
			Vector3 result;
			result.x = (float)Math.Atan2((double)(2f * (num4 - num9)), (double)(1f - 2f * (num + num3)));
			result.y = (float)Math.Atan2((double)(2f * (num5 - num8)), (double)(1f - 2f * (num2 + num3)));
			result.z = (float)Math.Asin((double)(2f * (num6 + num7)));
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003D10 File Offset: 0x00001F10
		public static Vector3 QuaternionToZxy(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.x;
			float num2 = quaternion.y * quaternion.y;
			float num3 = quaternion.z * quaternion.z;
			float num4 = quaternion.w * quaternion.x;
			float num5 = quaternion.w * quaternion.y;
			float num6 = quaternion.w * quaternion.z;
			float num7 = quaternion.x * quaternion.y;
			float num8 = quaternion.x * quaternion.z;
			float num9 = quaternion.y * quaternion.z;
			Vector3 result;
			result.x = (float)Math.Asin((double)(2f * (num4 + num9)));
			result.y = (float)Math.Atan2((double)(2f * (num5 - num8)), (double)(1f - 2f * (num + num2)));
			result.z = (float)Math.Atan2((double)(2f * (num6 - num7)), (double)(1f - 2f * (num + num3)));
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003E0C File Offset: 0x0000200C
		public static Vector3 QuaternionToZyx(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.x;
			float num2 = quaternion.y * quaternion.y;
			float num3 = quaternion.z * quaternion.z;
			float num4 = quaternion.w * quaternion.x;
			float num5 = quaternion.w * quaternion.y;
			float num6 = quaternion.w * quaternion.z;
			float num7 = quaternion.x * quaternion.y;
			float num8 = quaternion.x * quaternion.z;
			float num9 = quaternion.y * quaternion.z;
			Vector3 result;
			result.x = (float)Math.Atan2((double)(2f * (num4 + num9)), (double)(1f - 2f * (num + num2)));
			result.y = (float)Math.Asin((double)(2f * (num5 - num8)));
			result.z = (float)Math.Atan2((double)(2f * (num6 + num7)), (double)(1f - 2f * (num2 + num3)));
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003F08 File Offset: 0x00002108
		public static Quaternion XyzToQuaternion(Vector3 euler)
		{
			double num = Math.Cos((double)(euler.x * 0.5f));
			double num2 = Math.Sin((double)(euler.x * 0.5f));
			double num3 = Math.Cos((double)(euler.y * 0.5f));
			double num4 = Math.Sin((double)(euler.y * 0.5f));
			double num5 = Math.Cos((double)(euler.z * 0.5f));
			double num6 = Math.Sin((double)(euler.z * 0.5f));
			Quaternion result;
			result.w = (float)(num * num3 * num5 - num2 * num4 * num6);
			result.x = (float)(num2 * num3 * num5 + num * num4 * num6);
			result.y = (float)(num * num4 * num5 - num2 * num3 * num6);
			result.z = (float)(num2 * num4 * num5 + num * num3 * num6);
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003FE0 File Offset: 0x000021E0
		public static Quaternion XzyToQuaternion(Vector3 euler)
		{
			double num = Math.Cos((double)(euler.x * 0.5f));
			double num2 = Math.Sin((double)(euler.x * 0.5f));
			double num3 = Math.Cos((double)(euler.y * 0.5f));
			double num4 = Math.Sin((double)(euler.y * 0.5f));
			double num5 = Math.Cos((double)(euler.z * 0.5f));
			double num6 = Math.Sin((double)(euler.z * 0.5f));
			Quaternion result;
			result.w = (float)(num * num3 * num5 + num2 * num4 * num6);
			result.x = (float)(num2 * num3 * num5 - num * num4 * num6);
			result.y = (float)(num * num4 * num5 - num2 * num3 * num6);
			result.z = (float)(num * num3 * num6 + num2 * num4 * num5);
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000040B8 File Offset: 0x000022B8
		public static Quaternion YxzToQuaternion(Vector3 euler)
		{
			double num = Math.Cos((double)(euler.x * 0.5f));
			double num2 = Math.Sin((double)(euler.x * 0.5f));
			double num3 = Math.Cos((double)(euler.y * 0.5f));
			double num4 = Math.Sin((double)(euler.y * 0.5f));
			double num5 = Math.Cos((double)(euler.z * 0.5f));
			double num6 = Math.Sin((double)(euler.z * 0.5f));
			Quaternion result;
			result.w = (float)(num * num3 * num5 + num2 * num4 * num6);
			result.x = (float)(num2 * num3 * num5 + num * num4 * num6);
			result.y = (float)(num * num4 * num5 - num2 * num3 * num6);
			result.z = (float)(num * num3 * num6 - num2 * num4 * num5);
			return result;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00004190 File Offset: 0x00002390
		public static Quaternion YzxToQuaternion(Vector3 euler)
		{
			double num = Math.Cos((double)(euler.x * 0.5f));
			double num2 = Math.Sin((double)(euler.x * 0.5f));
			double num3 = Math.Cos((double)(euler.y * 0.5f));
			double num4 = Math.Sin((double)(euler.y * 0.5f));
			double num5 = Math.Cos((double)(euler.z * 0.5f));
			double num6 = Math.Sin((double)(euler.z * 0.5f));
			Quaternion result;
			result.w = (float)(num * num3 * num5 - num2 * num4 * num6);
			result.x = (float)(num2 * num3 * num5 + num * num4 * num6);
			result.y = (float)(num * num4 * num5 + num2 * num3 * num6);
			result.z = (float)(num * num3 * num6 - num2 * num4 * num5);
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00004268 File Offset: 0x00002468
		public static Quaternion ZxyToQuaternion(Vector3 euler)
		{
			double num = Math.Cos((double)(euler.x * 0.5f));
			double num2 = Math.Sin((double)(euler.x * 0.5f));
			double num3 = Math.Cos((double)(euler.y * 0.5f));
			double num4 = Math.Sin((double)(euler.y * 0.5f));
			double num5 = Math.Cos((double)(euler.z * 0.5f));
			double num6 = Math.Sin((double)(euler.z * 0.5f));
			Quaternion result;
			result.w = (float)(num * num3 * num5 - num2 * num4 * num6);
			result.x = (float)(num2 * num3 * num5 - num * num4 * num6);
			result.y = (float)(num * num4 * num5 + num2 * num3 * num6);
			result.z = (float)(num * num3 * num6 + num2 * num4 * num5);
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004340 File Offset: 0x00002540
		public static Quaternion ZYXToQuaternion(Vector3 euler)
		{
			double num = Math.Cos((double)(euler.x * 0.5f));
			double num2 = Math.Sin((double)(euler.x * 0.5f));
			double num3 = Math.Cos((double)(euler.y * 0.5f));
			double num4 = Math.Sin((double)(euler.y * 0.5f));
			double num5 = Math.Cos((double)(euler.z * 0.5f));
			double num6 = Math.Sin((double)(euler.z * 0.5f));
			Quaternion result;
			result.w = (float)(num * num3 * num5 + num2 * num4 * num6);
			result.x = (float)(num2 * num3 * num5 - num * num4 * num6);
			result.y = (float)(num * num4 * num5 + num2 * num3 * num6);
			result.z = (float)(num * num3 * num6 - num2 * num4 * num5);
			return result;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00004417 File Offset: 0x00002617
		public static float NanToZero(float f)
		{
			if (!float.IsNaN(f))
			{
				return f;
			}
			return 0f;
		}
	}
}
