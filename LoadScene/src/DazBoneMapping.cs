using System;
using System.Collections.Generic;
using LibMMD.Model;
using LibMMD.Unity3D;
using UnityEngine;

// Token: 0x02000004 RID: 4
internal class DazBoneMapping
{
	// Token: 0x06000009 RID: 9 RVA: 0x0000221C File Offset: 0x0000041C
	public static Dictionary<string, Transform> CreateFakeBones(Transform root, Transform refTf)
	{
		List<string> list = new List<string>
		{
			"Shldr",
			"ForeArm",
			"Hand",
			"Carpal1",
			"Index1",
			"Index2",
			"Index3",
			"Mid1",
			"Mid2",
			"Mid3",
			"Carpal2",
			"Pinky1",
			"Pinky2",
			"Pinky3",
			"Ring1",
			"Ring2",
			"Ring3",
			"Thumb1",
			"Thumb2",
			"Thumb3"
		};
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("ForeArm", "Shldr");
		dictionary.Add("Hand", "ForeArm");
		dictionary.Add("Carpal1", "Hand");
		dictionary.Add("Index1", "Carpal1");
		dictionary.Add("Index2", "Index1");
		dictionary.Add("Index3", "Index2");
		dictionary.Add("Mid1", "Carpal1");
		dictionary.Add("Mid2", "Mid1");
		dictionary.Add("Mid3", "Mid2");
		dictionary.Add("Carpal2", "Hand");
		dictionary.Add("Pinky1", "Carpal2");
		dictionary.Add("Pinky2", "Pinky1");
		dictionary.Add("Pinky3", "Pinky2");
		dictionary.Add("Ring1", "Carpal2");
		dictionary.Add("Ring2", "Ring1");
		dictionary.Add("Ring3", "Ring2");
		dictionary.Add("Thumb1", "Hand");
		dictionary.Add("Thumb2", "Thumb1");
		dictionary.Add("Thumb3", "Thumb2");
		string[] array = new string[]
		{
			"l",
			"r"
		};
		Dictionary<string, Transform> dictionary2 = new Dictionary<string, Transform>();
		foreach (string str in array)
		{
			foreach (string text in list)
			{
				GameObject gameObject = new GameObject(str + text);
				dictionary2.Add(str + text, gameObject.transform);
				if (dictionary.ContainsKey(text))
				{
					gameObject.transform.parent = dictionary2[str + dictionary[text]];
				}
				else
				{
					gameObject.transform.parent = root;
				}
			}
			foreach (KeyValuePair<string, Transform> keyValuePair in dictionary2)
			{
				Transform transform = DazBoneMapping.SearchObjName(refTf, keyValuePair.Key);
				keyValuePair.Value.position = transform.position;
			}
		}
		dictionary2["lShldr"].eulerAngles = new Vector3(0f, 0f, 36f);
		dictionary2["rShldr"].eulerAngles = new Vector3(0f, 0f, -36f);
		return dictionary2;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000025BC File Offset: 0x000007BC
	public static Transform SearchObjName(Transform root, string name)
	{
		GameObject gameObject = TransformFindEx.FindLoop(root, name);
		if (gameObject != null)
		{
			return gameObject.transform;
		}
		if (name == "Genesis2Female")
		{
			gameObject = TransformFindEx.FindLoop(root, "Genesis2Male");
			if (gameObject != null)
			{
				return gameObject.transform;
			}
		}
		Debug.Log(string.Concat(new string[]
		{
			"Try to find ",
			name,
			" from ",
			root.name,
			" but not found."
		}));
		return null;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002640 File Offset: 0x00000840
	public static Vector3 TryGetPosition(GameObject target, string bone1, string bone2, float ratio)
	{
		Transform transform = DazBoneMapping.SearchObjName(target.transform, bone1);
		Transform transform2 = DazBoneMapping.SearchObjName(target.transform, bone2);
		return transform.position + (transform2.position - transform.position) * ratio - target.transform.position;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000269C File Offset: 0x0000089C
	public static Vector3 GetPosition(GameObject target, Bone bone, string name, Dictionary<string, Transform> check)
	{
		string text = name;
		if (DazBoneMapping.boneNames.ContainsKey(name))
		{
			text = DazBoneMapping.boneNames[name];
		}
		if (text.Contains("|"))
		{
			string[] array = text.Split(new char[]
			{
				'|'
			});
			string text2 = array[0];
			string text3 = array[1];
			float d = float.Parse(array[2]);
			Transform transform = DazBoneMapping.SearchObjName(target.transform, text2);
			if (check.ContainsKey(text2))
			{
				transform = check[text2];
			}
			Transform transform2 = DazBoneMapping.SearchObjName(target.transform, text3);
			if (check.ContainsKey(text3))
			{
				transform2 = check[text3];
			}
			return transform.position + (transform2.position - transform.position) * d - target.transform.position;
		}
		Transform transform3 = DazBoneMapping.SearchObjName(target.transform, text);
		if (DazBoneMapping.cachedBoneLookup == null)
		{
			DazBoneMapping.cachedBoneLookup = new Dictionary<string, Transform>();
		}
		if (DazBoneMapping.cachedBoneLookup.ContainsKey(name))
		{
			DazBoneMapping.cachedBoneLookup.Remove(name);
		}
		DazBoneMapping.cachedBoneLookup.Add(name, transform3);
		if (check.ContainsKey(text))
		{
			transform3 = check[text];
		}
		if (transform3 != null)
		{
			return transform3.position - target.transform.position;
		}
		Debug.Log("no set " + name);
		return bone.Position;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000027FC File Offset: 0x000009FC
	public static void UpdateBones(GameObject target, MmdGameObject mmd)
	{
		GameObject[] bones = mmd._bones;
		target.GetComponentInChildren<SkinnedMeshRenderer>();
		foreach (GameObject gameObject in bones)
		{
			Quaternion rotation = gameObject.transform.rotation;
			if (DazBoneMapping.cachedBoneLookup.ContainsKey(gameObject.name))
			{
				DazBoneMapping.cachedBoneLookup[gameObject.name].SetPositionAndRotation(gameObject.transform.position, rotation * DazBoneMapping.quat);
			}
		}
	}

	// Token: 0x04000001 RID: 1
	public static List<string> vamControls = new List<string>
	{
		"control",
		"hipControl",
		"pelvisControl",
		"chestControl",
		"headControl",
		"rHandControl",
		"lHandControl",
		"rFootControl",
		"lFootControl",
		"neckControl",
		"rElbowControl",
		"lElbowControl",
		"rKneeControl",
		"lKneeControl",
		"rToeControl",
		"lToeControl",
		"abdomenControl",
		"abdomen2Control",
		"rThighControl",
		"lThighControl",
		"rArmControl",
		"lArmControl",
		"rShoulderControl",
		"lShoulderControl"
	};

	// Token: 0x04000002 RID: 2
	public static List<string> AssetBindControlNames = new List<string>
	{
		"lHandControl",
		"rHandControl"
	};

	// Token: 0x04000003 RID: 3
	public static Dictionary<string, string> boneNames = new Dictionary<string, string>
	{
		{
			"全ての親",
			"Genesis2Female"
		},
		{
			"センター",
			"Genesis2Female|hip|0.8"
		},
		{
			"グルーブ",
			"Genesis2Female|hip|0.8"
		},
		{
			"舌親",
			"tongueBase"
		},
		{
			"舌1",
			"tongue01"
		},
		{
			"舌2",
			"tongue02"
		},
		{
			"舌3",
			"tongue03"
		},
		{
			"舌4",
			"tongue04"
		},
		{
			"舌5",
			"tongue05"
		},
		{
			"舌6",
			"tongueTip"
		},
		{
			"左目",
			"lEye"
		},
		{
			"右目",
			"rEye"
		},
		{
			"右胸上2",
			"rPectoral"
		},
		{
			"左胸上2",
			"lPectoral"
		},
		{
			"upperJaw",
			"upperJaw"
		},
		{
			"lowerJaw",
			"lowerJaw"
		},
		{
			"腰",
			"hip"
		},
		{
			"下半身",
			"pelvis"
		},
		{
			"上半身",
			"abdomen"
		},
		{
			"上半身2",
			"abdomen2"
		},
		{
			"上半身3",
			"chest"
		},
		{
			"首",
			"neck"
		},
		{
			"頭",
			"head"
		},
		{
			"左肩",
			"lCollar"
		},
		{
			"右肩",
			"rCollar"
		},
		{
			"左腕",
			"lShldr"
		},
		{
			"右腕",
			"rShldr"
		},
		{
			"左ひじ",
			"lForeArm"
		},
		{
			"右ひじ",
			"rForeArm"
		},
		{
			"左手首",
			"lHand"
		},
		{
			"右手首",
			"rHand"
		},
		{
			"左足D",
			"lThigh"
		},
		{
			"右足D",
			"rThigh"
		},
		{
			"左ひざD",
			"lShin"
		},
		{
			"右ひざD",
			"rShin"
		},
		{
			"左足首D",
			"lFoot"
		},
		{
			"右足首D",
			"rFoot"
		},
		{
			"左足先EX",
			"lToe"
		},
		{
			"右足先EX",
			"rToe"
		},
		{
			"左足",
			"lThigh"
		},
		{
			"左ひざ",
			"lShin"
		},
		{
			"左足首",
			"lFoot"
		},
		{
			"左つま先",
			"lToe"
		},
		{
			"lBigToe",
			"lBigToe"
		},
		{
			"lSmallToe1",
			"lSmallToe1"
		},
		{
			"lSmallToe2",
			"lSmallToe2"
		},
		{
			"lSmallToe3",
			"lSmallToe3"
		},
		{
			"lSmallToe4",
			"lSmallToe4"
		},
		{
			"右足",
			"rThigh"
		},
		{
			"右ひざ",
			"rShin"
		},
		{
			"右足首",
			"rFoot"
		},
		{
			"右つま先",
			"rToe"
		},
		{
			"rBigToe",
			"rBigToe"
		},
		{
			"rSmallToe1",
			"rSmallToe1"
		},
		{
			"rSmallToe2",
			"rSmallToe2"
		},
		{
			"rSmallToe3",
			"rSmallToe3"
		},
		{
			"rSmallToe4",
			"rSmallToe4"
		},
		{
			"左足IK親",
			"lFoot"
		},
		{
			"左足ＩＫ",
			"lFoot"
		},
		{
			"左つま先ＩＫ",
			"lToe"
		},
		{
			"右足IK親",
			"rFoot"
		},
		{
			"右足ＩＫ",
			"rFoot"
		},
		{
			"右つま先ＩＫ",
			"rToe"
		},
		{
			"右腕捩",
			"rShldr|rForeArm|0.5"
		},
		{
			"右腕捩1",
			"rShldr|rForeArm|0.25"
		},
		{
			"右腕捩2",
			"rShldr|rForeArm|0.5"
		},
		{
			"右腕捩3",
			"rShldr|rForeArm|0.75"
		},
		{
			"右肩P",
			"rCollar"
		},
		{
			"右肩C",
			"rCollar"
		},
		{
			"右手捩",
			"rForeArm|rHand|0.5"
		},
		{
			"右手捩1",
			"rForeArm|rHand|0.25"
		},
		{
			"右手捩2",
			"rForeArm|rHand|0.5"
		},
		{
			"右手捩3",
			"rForeArm|rHand|0.75"
		},
		{
			"左腕捩",
			"lShldr|lForeArm|0.5"
		},
		{
			"左腕捩1",
			"lShldr|lForeArm|0.25"
		},
		{
			"左腕捩2",
			"lShldr|lForeArm|0.5"
		},
		{
			"左腕捩3",
			"lShldr|lForeArm|0.75"
		},
		{
			"左肩P",
			"lCollar"
		},
		{
			"左肩C",
			"lCollar"
		},
		{
			"左手捩",
			"lForeArm|lHand|0.5"
		},
		{
			"左手捩1",
			"lForeArm|lHand|0.25"
		},
		{
			"左手捩2",
			"lForeArm|lHand|0.5"
		},
		{
			"左手捩3",
			"lForeArm|lHand|0.75"
		},
		{
			"rCarpal1",
			"rCarpal1"
		},
		{
			"rCarpal2",
			"rCarpal2"
		},
		{
			"lCarpal1",
			"lCarpal1"
		},
		{
			"lCarpal2",
			"lCarpal2"
		},
		{
			"右ダミー",
			"rCarpal1|rMid1|0.5"
		},
		{
			"左ダミー",
			"lCarpal1|lMid1|0.5"
		},
		{
			"左親指０",
			"lThumb1"
		},
		{
			"左親指１",
			"lThumb2"
		},
		{
			"左親指２",
			"lThumb3"
		},
		{
			"左人指１",
			"lIndex1"
		},
		{
			"左人指２",
			"lIndex2"
		},
		{
			"左人指３",
			"lIndex3"
		},
		{
			"左中指１",
			"lMid1"
		},
		{
			"左中指２",
			"lMid2"
		},
		{
			"左中指３",
			"lMid3"
		},
		{
			"左薬指１",
			"lRing1"
		},
		{
			"左薬指２",
			"lRing2"
		},
		{
			"左薬指３",
			"lRing3"
		},
		{
			"左小指１",
			"lPinky1"
		},
		{
			"左小指２",
			"lPinky2"
		},
		{
			"左小指３",
			"lPinky3"
		},
		{
			"右親指０",
			"rThumb1"
		},
		{
			"右親指１",
			"rThumb2"
		},
		{
			"右親指２",
			"rThumb3"
		},
		{
			"右人指１",
			"rIndex1"
		},
		{
			"右人指２",
			"rIndex2"
		},
		{
			"右人指３",
			"rIndex3"
		},
		{
			"右中指１",
			"rMid1"
		},
		{
			"右中指２",
			"rMid2"
		},
		{
			"右中指３",
			"rMid3"
		},
		{
			"右薬指１",
			"rRing1"
		},
		{
			"右薬指２",
			"rRing2"
		},
		{
			"右薬指３",
			"rRing3"
		},
		{
			"右小指１",
			"rPinky1"
		},
		{
			"右小指２",
			"rPinky2"
		},
		{
			"右小指３",
			"rPinky3"
		}
	};

	// Token: 0x04000004 RID: 4
	public static HashSet<string> ignoreUpdateBoneNames = new HashSet<string>
	{
		"舌親",
		"舌1",
		"舌2",
		"舌3",
		"舌4",
		"舌5",
		"舌6",
		"左目",
		"右目",
		"右胸上2",
		"左胸上2",
		"upperJaw",
		"lowerJaw",
		"rCarpal1",
		"rCarpal2",
		"lCarpal1",
		"lCarpal2"
	};

	// Token: 0x04000005 RID: 5
	public static HashSet<string> armBones = new HashSet<string>
	{
		"左肩",
		"左腕",
		"左ひじ",
		"左手首",
		"右肩",
		"右腕",
		"右ひじ",
		"右手首",
		"rCarpal1",
		"rCarpal2",
		"lCarpal1",
		"lCarpal2",
		"左親指０",
		"左親指１",
		"左親指２",
		"左人指１",
		"左人指２",
		"左人指３",
		"左中指１",
		"左中指２",
		"左中指３",
		"左薬指１",
		"左薬指２",
		"左薬指３",
		"左小指１",
		"左小指２",
		"左小指３",
		"右親指０",
		"右親指１",
		"右親指２",
		"右人指１",
		"右人指２",
		"右人指３",
		"右中指１",
		"右中指２",
		"右中指３",
		"右薬指１",
		"右薬指２",
		"右薬指３",
		"右小指１",
		"右小指２",
		"右小指３"
	};

	// Token: 0x04000006 RID: 6
	public static HashSet<string> fingerBoneNames = new HashSet<string>
	{
		"左親指０",
		"左親指１",
		"左親指２",
		"左人指１",
		"左人指２",
		"左人指３",
		"左中指１",
		"左中指２",
		"左中指３",
		"左薬指１",
		"左薬指２",
		"左薬指３",
		"左小指１",
		"左小指２",
		"左小指３",
		"右親指０",
		"右親指１",
		"右親指２",
		"右人指１",
		"右人指２",
		"右人指３",
		"右中指１",
		"右中指２",
		"右中指３",
		"右薬指１",
		"右薬指２",
		"右薬指３",
		"右小指１",
		"右小指２",
		"右小指３"
	};

	// Token: 0x04000007 RID: 7
	public static HashSet<string> thumbBoneNames = new HashSet<string>
	{
		"左親指０",
		"左親指１",
		"左親指２",
		"右親指０",
		"右親指１",
		"右親指２"
	};

	// Token: 0x04000008 RID: 8
	public static Dictionary<string, Transform> cachedBoneLookup;

	// Token: 0x04000009 RID: 9
	private static Quaternion quat = new Quaternion(0f, 1f, 0f, 0f);
}
