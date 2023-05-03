using System;
using System.Collections.Generic;
using mmd2timeline;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000019 RID: 25
	public class MmdMotion
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00007818 File Offset: 0x00005A18
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00007820 File Offset: 0x00005A20
		public string Name { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007829 File Offset: 0x00005A29
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00007831 File Offset: 0x00005A31
		public int Length { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000783A File Offset: 0x00005A3A
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00007842 File Offset: 0x00005A42
		public Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>> BoneMotions { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000784B File Offset: 0x00005A4B
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00007853 File Offset: 0x00005A53
		public Dictionary<string, List<KeyValuePair<int, MorphKeyframe>>> MorphMotions { get; set; }

		// Token: 0x060000F1 RID: 241 RVA: 0x0000785C File Offset: 0x00005A5C
		public void GetSingleKeyframes()
		{
			Dictionary<int, HashSet<string>> dictionary = new Dictionary<int, HashSet<string>>();
			foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in this.BoneMotions)
			{
				foreach (KeyValuePair<int, BoneKeyframe> keyValuePair2 in keyValuePair.Value)
				{
					if (!dictionary.ContainsKey(keyValuePair2.Key))
					{
						dictionary.Add(keyValuePair2.Key, new HashSet<string>());
					}
					dictionary[keyValuePair2.Key].Add(keyValuePair.Key);
				}
			}
			foreach (KeyValuePair<int, HashSet<string>> keyValuePair3 in dictionary)
			{
				if (keyValuePair3.Value.Count == 1)
				{
					LogUtil.Log("1 bone:" + keyValuePair3.Key.ToString());
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000798C File Offset: 0x00005B8C
		public void CombineBoneMotions(Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>> motions)
		{
			foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in motions)
			{
				if (!this.BoneMotions.ContainsKey(keyValuePair.Key))
				{
					this.BoneMotions.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					List<KeyValuePair<int, BoneKeyframe>> list = this.BoneMotions[keyValuePair.Key];
					Dictionary<int, KeyValuePair<int, BoneKeyframe>> dictionary = new Dictionary<int, KeyValuePair<int, BoneKeyframe>>();
					foreach (KeyValuePair<int, BoneKeyframe> value in list)
					{
						dictionary.Add(value.Key, value);
					}
					foreach (KeyValuePair<int, BoneKeyframe> value2 in keyValuePair.Value)
					{
						if (dictionary.ContainsKey(value2.Key))
						{
							dictionary.Remove(value2.Key);
						}
						dictionary.Add(value2.Key, value2);
					}
					List<KeyValuePair<int, BoneKeyframe>> list2 = new List<KeyValuePair<int, BoneKeyframe>>();
					foreach (KeyValuePair<int, KeyValuePair<int, BoneKeyframe>> keyValuePair2 in dictionary)
					{
						list2.Add(keyValuePair2.Value);
					}
					list2.Sort(delegate(KeyValuePair<int, BoneKeyframe> a, KeyValuePair<int, BoneKeyframe> b)
					{
						if (a.Key < b.Key)
						{
							return -1;
						}
						if (a.Key > b.Key)
						{
							return 1;
						}
						return 0;
					});
					this.BoneMotions[keyValuePair.Key] = list2;
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007B88 File Offset: 0x00005D88
		public void CombineMorphMotions(Dictionary<string, List<KeyValuePair<int, MorphKeyframe>>> motions)
		{
			foreach (KeyValuePair<string, List<KeyValuePair<int, MorphKeyframe>>> keyValuePair in motions)
			{
				if (!this.MorphMotions.ContainsKey(keyValuePair.Key))
				{
					this.MorphMotions.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					List<KeyValuePair<int, MorphKeyframe>> list = this.MorphMotions[keyValuePair.Key];
					Dictionary<int, KeyValuePair<int, MorphKeyframe>> dictionary = new Dictionary<int, KeyValuePair<int, MorphKeyframe>>();
					foreach (KeyValuePair<int, MorphKeyframe> value in list)
					{
						dictionary.Add(value.Key, value);
					}
					foreach (KeyValuePair<int, MorphKeyframe> value2 in keyValuePair.Value)
					{
						if (dictionary.ContainsKey(value2.Key))
						{
							dictionary.Remove(value2.Key);
						}
						dictionary.Add(value2.Key, value2);
					}
					List<KeyValuePair<int, MorphKeyframe>> list2 = new List<KeyValuePair<int, MorphKeyframe>>();
					foreach (KeyValuePair<int, KeyValuePair<int, MorphKeyframe>> keyValuePair2 in dictionary)
					{
						list2.Add(keyValuePair2.Value);
					}
					list2.Sort(delegate(KeyValuePair<int, MorphKeyframe> a, KeyValuePair<int, MorphKeyframe> b)
					{
						if (a.Key < b.Key)
						{
							return -1;
						}
						if (a.Key > b.Key)
						{
							return 1;
						}
						return 0;
					});
					this.MorphMotions[keyValuePair.Key] = list2;
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007D84 File Offset: 0x00005F84
		public MmdMotion()
		{
			this.BoneMotions = new Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>>();
			this.MorphMotions = new Dictionary<string, List<KeyValuePair<int, MorphKeyframe>>>();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public BonePose GetBonePose(string boneName, int frame)
		{
			List<KeyValuePair<int, BoneKeyframe>> list;
			this.BoneMotions.TryGetValue(boneName, out list);
			if (list == null || list.Count == 0)
			{
				return new BonePose
				{
					Translation = Vector3.zero,
					Rotation = Quaternion.identity,
					Only1Frame = true
				};
			}
			if (list[0].Key >= frame)
			{
				BoneKeyframe value = list[0].Value;
				return new BonePose
				{
					Translation = value.Translation,
					Rotation = value.Rotation,
					Only1Frame = true
				};
			}
			if (list.Count == 1)
			{
				BoneKeyframe value2 = list[0].Value;
				return new BonePose
				{
					Translation = value2.Translation,
					Rotation = value2.Rotation,
					Only1Frame = true
				};
			}
			if (list[list.Count - 1].Key <= frame)
			{
				BoneKeyframe value3 = list[list.Count - 1].Value;
				return new BonePose
				{
					Translation = value3.Translation,
					Rotation = value3.Rotation
				};
			}
			KeyValuePair<int, BoneKeyframe> item = new KeyValuePair<int, BoneKeyframe>(frame, null);
			int num = list.BinarySearch(item, MmdMotion.BoneKeyframeSearchComparator.Instance);
			if (num < 0)
			{
				num = ~num;
			}
			int index;
			if (num == 0)
			{
				index = 0;
			}
			else if (num >= list.Count)
			{
				index = (num = list.Count - 1);
			}
			else
			{
				index = num - 1;
			}
			KeyValuePair<int, BoneKeyframe> keyValuePair = list[num];
			int key = keyValuePair.Key;
			BoneKeyframe value4 = keyValuePair.Value;
			KeyValuePair<int, BoneKeyframe> keyValuePair2 = list[index];
			int key2 = keyValuePair2.Key;
			BoneKeyframe value5 = keyValuePair2.Value;
			if (key2 == key)
			{
				return new BonePose
				{
					Translation = value5.Translation,
					Rotation = value5.Rotation
				};
			}
			float x = (float)(frame - key2) / (float)(key - key2);
			Vector3 translation = default(Vector3);
			float num2 = value5.XInterpolator.Calculate(x);
			translation.x = value5.Translation.x * (1f - num2) + value4.Translation.x * num2;
			num2 = value5.YInterpolator.Calculate(x);
			translation.y = value5.Translation.y * (1f - num2) + value4.Translation.y * num2;
			num2 = value5.ZInterpolator.Calculate(x);
			translation.z = value5.Translation.z * (1f - num2) + value4.Translation.z * num2;
			num2 = value5.RInterpolator.Calculate(x);
			Quaternion rotation = Quaternion.Lerp(value5.Rotation, value4.Rotation, num2);
			return new BonePose
			{
				Translation = translation,
				Rotation = rotation
			};
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00008070 File Offset: 0x00006270
		public BonePose GetBonePose(string boneName, double time)
		{
			List<KeyValuePair<int, BoneKeyframe>> list;
			this.BoneMotions.TryGetValue(boneName, out list);
			if (list == null || list.Count == 0)
			{
				return new BonePose
				{
					Translation = Vector3.zero,
					Rotation = Quaternion.identity,
					Only1Frame = true
				};
			}
			if (list.Count == 1)
			{
				BoneKeyframe value = list[0].Value;
				return new BonePose
				{
					Translation = value.Translation,
					Rotation = value.Rotation,
					Only1Frame = true
				};
			}
			double num = time * 30.0;
			if ((double)list[0].Key >= num)
			{
				BoneKeyframe value2 = list[0].Value;
				return new BonePose
				{
					Translation = value2.Translation,
					Rotation = value2.Rotation
				};
			}
			if ((double)list[list.Count - 1].Key <= num)
			{
				BoneKeyframe value3 = list[list.Count - 1].Value;
				return new BonePose
				{
					Translation = value3.Translation,
					Rotation = value3.Rotation
				};
			}
			KeyValuePair<int, BoneKeyframe> item = new KeyValuePair<int, BoneKeyframe>((int)num, null);
			int num2 = list.BinarySearch(item, MmdMotion.BoneKeyframeSearchComparator.Instance);
			if (num2 < 0)
			{
				num2 = ~num2;
			}
			int index;
			if (num2 == 0)
			{
				index = 0;
			}
			else if (num2 >= list.Count)
			{
				index = (num2 = list.Count - 1);
			}
			else
			{
				index = num2 - 1;
			}
			KeyValuePair<int, BoneKeyframe> keyValuePair = list[num2];
			int key = keyValuePair.Key;
			BoneKeyframe value4 = keyValuePair.Value;
			KeyValuePair<int, BoneKeyframe> keyValuePair2 = list[index];
			int key2 = keyValuePair2.Key;
			BoneKeyframe value5 = keyValuePair2.Value;
			if (key2 == key)
			{
				return new BonePose
				{
					Translation = value5.Translation,
					Rotation = value5.Rotation
				};
			}
			float x = (float)(num - (double)key2) / (float)(key - key2);
			Vector3 translation = default(Vector3);
			float num3 = value5.XInterpolator.Calculate(x);
			translation.x = value5.Translation.x * (1f - num3) + value4.Translation.x * num3;
			num3 = value5.YInterpolator.Calculate(x);
			translation.y = value5.Translation.y * (1f - num3) + value4.Translation.y * num3;
			num3 = value5.ZInterpolator.Calculate(x);
			translation.z = value5.Translation.z * (1f - num3) + value4.Translation.z * num3;
			num3 = value5.RInterpolator.Calculate(x);
			Quaternion rotation = Quaternion.Lerp(value5.Rotation, value4.Rotation, num3);
			return new BonePose
			{
				Translation = translation,
				Rotation = rotation
			};
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008348 File Offset: 0x00006548
		public MorphPose GetMorphPose(string morphName, int frame)
		{
			List<KeyValuePair<int, MorphKeyframe>> list;
			this.MorphMotions.TryGetValue(morphName, out list);
			if (list == null || list.Count == 0)
			{
				return new MorphPose(0f);
			}
			if (list[0].Key >= frame)
			{
				return new MorphPose(list[0].Value.Weight);
			}
			if (list[list.Count - 1].Key <= frame)
			{
				return new MorphPose(list[list.Count - 1].Value.Weight);
			}
			KeyValuePair<int, MorphKeyframe> item = new KeyValuePair<int, MorphKeyframe>(frame, null);
			int num = list.BinarySearch(item, MmdMotion.MorphKeyframeSearchComparator.Instance);
			if (num < 0)
			{
				num = ~num;
			}
			int index;
			if (num == 0)
			{
				index = 0;
			}
			else if (num >= list.Count)
			{
				index = (num = list.Count - 1);
			}
			else
			{
				index = num - 1;
			}
			KeyValuePair<int, MorphKeyframe> keyValuePair = list[num];
			int key = keyValuePair.Key;
			MorphKeyframe value = keyValuePair.Value;
			KeyValuePair<int, MorphKeyframe> keyValuePair2 = list[index];
			int key2 = keyValuePair2.Key;
			MorphKeyframe value2 = keyValuePair2.Value;
			if (key2 == key)
			{
				return new MorphPose(value2.Weight);
			}
			float x = (float)(frame - key2) / (float)(key - key2);
			float weight = value2.Weight;
			float weight2 = value.Weight;
			float num2 = value2.WInterpolator.Calculate(x);
			return new MorphPose(weight * (1f - num2) + weight2 * num2);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000084B0 File Offset: 0x000066B0
		public MorphPose GetMorphPose(string morphName, double time)
		{
			List<KeyValuePair<int, MorphKeyframe>> list;
			this.MorphMotions.TryGetValue(morphName, out list);
			if (list == null || list.Count == 0)
			{
				return new MorphPose(0f);
			}
			double num = time * 30.0;
			if ((double)list[0].Key >= num)
			{
				return new MorphPose(list[0].Value.Weight);
			}
			if ((double)list[list.Count - 1].Key <= num)
			{
				return new MorphPose(list[list.Count - 1].Value.Weight);
			}
			KeyValuePair<int, MorphKeyframe> item = new KeyValuePair<int, MorphKeyframe>((int)num, null);
			int num2 = list.BinarySearch(item, MmdMotion.MorphKeyframeSearchComparator.Instance);
			if (num2 < 0)
			{
				num2 = ~num2;
			}
			int index;
			if (num2 == 0)
			{
				index = 0;
			}
			else if (num2 >= list.Count)
			{
				index = (num2 = list.Count - 1);
			}
			else
			{
				index = num2 - 1;
			}
			KeyValuePair<int, MorphKeyframe> keyValuePair = list[num2];
			int key = keyValuePair.Key;
			MorphKeyframe value = keyValuePair.Value;
			KeyValuePair<int, MorphKeyframe> keyValuePair2 = list[index];
			int key2 = keyValuePair2.Key;
			MorphKeyframe value2 = keyValuePair2.Value;
			if (key2 == key)
			{
				return new MorphPose(value2.Weight);
			}
			float x = (float)(num - (double)key2) / (float)(key - key2);
			float weight = value2.Weight;
			float weight2 = value.Weight;
			float num3 = value2.WInterpolator.Calculate(x);
			return new MorphPose(weight * (1f - num3) + weight2 * num3);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008629 File Offset: 0x00006829
		public bool IsBoneRegistered(string boneName)
		{
			return this.BoneMotions.ContainsKey(boneName);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008637 File Offset: 0x00006837
		public bool IsMorphRegistered(string morphName)
		{
			return this.MorphMotions.ContainsKey(morphName);
		}

		// Token: 0x0200004E RID: 78
		private class BoneKeyframeSearchComparator : IComparer<KeyValuePair<int, BoneKeyframe>>
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x000108CB File Offset: 0x0000EACB
			private BoneKeyframeSearchComparator()
			{
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x000108D4 File Offset: 0x0000EAD4
			public int Compare(KeyValuePair<int, BoneKeyframe> x, KeyValuePair<int, BoneKeyframe> y)
			{
				return x.Key.CompareTo(y.Key);
			}

			// Token: 0x040001A0 RID: 416
			public static readonly MmdMotion.BoneKeyframeSearchComparator Instance = new MmdMotion.BoneKeyframeSearchComparator();
		}

		// Token: 0x0200004F RID: 79
		private class MorphKeyframeSearchComparator : IComparer<KeyValuePair<int, MorphKeyframe>>
		{
			// Token: 0x060002D9 RID: 729 RVA: 0x00010903 File Offset: 0x0000EB03
			private MorphKeyframeSearchComparator()
			{
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0001090C File Offset: 0x0000EB0C
			public int Compare(KeyValuePair<int, MorphKeyframe> x, KeyValuePair<int, MorphKeyframe> y)
			{
				return x.Key.CompareTo(y.Key);
			}

			// Token: 0x040001A1 RID: 417
			public static readonly MmdMotion.MorphKeyframeSearchComparator Instance = new MmdMotion.MorphKeyframeSearchComparator();
		}
	}
}
