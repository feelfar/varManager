using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class TransformFindEx
{
	// Token: 0x06000010 RID: 16 RVA: 0x000035F0 File Offset: 0x000017F0
	public static GameObject FindLoop(Transform root, string name)
	{
		if (root.name == name)
		{
			return root.gameObject;
		}
		for (int i = 0; i < root.childCount; i++)
		{
			GameObject gameObject = TransformFindEx.FindLoop(root.GetChild(i), name);
			if (gameObject != null)
			{
				return gameObject;
			}
		}
		return null;
	}
}
