using System;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000018 RID: 24
	public class Interpolator
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00007600 File Offset: 0x00005800
		public Interpolator()
		{
			this._c0 = Vector2.zero;
			this._c1 = new Vector2(3f, 3f);
			this._presamples = new float[32];
			this._isLinear = true;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000763C File Offset: 0x0000583C
		public Vector2 GetC(int i)
		{
			if (i == 0)
			{
				return this._c0 / 3f;
			}
			return this._c1 / 3f;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007662 File Offset: 0x00005862
		public void SetC(Vector2 c0, Vector2 c1)
		{
			this._c0 = c0 * 3f;
			this._c1 = c1 * 3f;
			this.PreSample();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000768C File Offset: 0x0000588C
		public float Calculate(float x)
		{
			if (this._isLinear)
			{
				return x;
			}
			x *= 31f;
			int num = (int)x;
			float num2 = x - (float)num;
			if (num < 31)
			{
				return (1f - num2) * this._presamples[num] + num2 * this._presamples[num + 1];
			}
			return this._presamples[31];
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000076E0 File Offset: 0x000058E0
		private void PreSample()
		{
			if (Math.Abs(this._c0.x - this._c0.y) < 1E-07f && Math.Abs(this._c1.x - this._c1.y) < 1E-07f)
			{
				this._isLinear = true;
				return;
			}
			this._isLinear = false;
			for (int i = 0; i < 32; i++)
			{
				float x = (float)i / 31f;
				this._presamples[i] = this.Interpolate(x);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007768 File Offset: 0x00005968
		private float Interpolate(float x)
		{
			float num = 0f;
			float num2 = 1f;
			float num3 = 0f;
			float num4;
			for (int i = 0; i < 32; i++)
			{
				num3 = (num + num2) * 0.5f;
				num4 = 1f - num3;
				float num5 = num3 * (num4 * (num4 * this._c0.x + num3 * this._c1.x) + num3 * num3);
				if (Math.Abs(num5 - x) < 1E-07f)
				{
					break;
				}
				if (num5 > x)
				{
					num2 = num3;
				}
				else
				{
					num = num3;
				}
			}
			num4 = 1f - num3;
			return num3 * (num4 * (num4 * this._c0.y + num3 * this._c1.y) + num3 * num3);
		}

		// Token: 0x0400005D RID: 93
		private const int PresampleResolution = 32;

		// Token: 0x0400005E RID: 94
		private Vector2 _c0;

		// Token: 0x0400005F RID: 95
		private Vector2 _c1;

		// Token: 0x04000060 RID: 96
		private bool _isLinear;

		// Token: 0x04000061 RID: 97
		private float[] _presamples;
	}
}
