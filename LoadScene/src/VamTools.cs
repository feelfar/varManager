using System;
using UnityEngine;

namespace mmd2timeline
{
	// Token: 0x0200003A RID: 58
	internal class VamTools
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00010228 File Offset: 0x0000E428
		private static bool IsFinger(string name)
		{
			foreach (string value in VamTools.FINGER_BONES)
			{
				if (name.Contains(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00010259 File Offset: 0x0000E459
		public static bool IsFinger(GameObject origTransform)
		{
			return origTransform != null && VamTools.IsFinger(origTransform.name);
		}

		// Token: 0x04000155 RID: 341
		private static string[] FINGER_BONES = new string[]
		{
			"Thumb",
			"Index",
			"Mid",
			"Ring",
			"Pinky",
			"Carpal"
		};
	}
}
