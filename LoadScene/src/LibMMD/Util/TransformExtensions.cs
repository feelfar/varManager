using System;
using UnityEngine;

namespace LibMMD.Util
{
	// Token: 0x0200000A RID: 10
	public static class TransformExtensions
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000047CC File Offset: 0x000029CC
		public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
		{
			transform.localScale = matrix.ExtractScale();
			transform.rotation = matrix.ExtractRotation();
			transform.position = matrix.ExtractPosition();
		}
	}
}
