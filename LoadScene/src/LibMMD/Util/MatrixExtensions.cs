using System;
using UnityEngine;

namespace LibMMD.Util
{
	// Token: 0x02000007 RID: 7
	public static class MatrixExtensions
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00004428 File Offset: 0x00002628
		public static Quaternion ExtractRotation(this Matrix4x4 matrix)
		{
			Vector3 forward;
			forward.x = matrix.m02;
			forward.y = matrix.m12;
			forward.z = matrix.m22;
			Vector3 upwards;
			upwards.x = matrix.m01;
			upwards.y = matrix.m11;
			upwards.z = matrix.m21;
			return Quaternion.LookRotation(forward, upwards);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000448C File Offset: 0x0000268C
		public static Vector3 ExtractPosition(this Matrix4x4 matrix)
		{
			Vector3 result;
			result.x = matrix.m03;
			result.y = matrix.m13;
			result.z = matrix.m23;
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000044C4 File Offset: 0x000026C4
		public static Vector3 ExtractScale(this Matrix4x4 matrix)
		{
			Vector3 result;
			result.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
			result.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
			result.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
			return result;
		}
	}
}
