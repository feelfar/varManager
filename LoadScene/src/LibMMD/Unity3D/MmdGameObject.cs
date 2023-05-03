using System;
using System.Collections.Generic;
using System.Linq;
using LibMMD.Model;
using LibMMD.Motion;
using LibMMD.Reader;
using LibMMD.Util;
using mmd2timeline;
using MVR.FileManagementSecure;
using UnityEngine;

namespace LibMMD.Unity3D
{
	// Token: 0x0200000C RID: 12
	public class MmdGameObject : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000049BC File Offset: 0x00002BBC
		public string ModelName
		{
			get
			{
				return this._model.Name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000049C9 File Offset: 0x00002BC9
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000049D1 File Offset: 0x00002BD1
		public string BonePoseFilePath { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00004A0A File Offset: 0x00002C0A
		public float MotionLength
		{
			get
			{
				if (this._motion != null)
				{
					return (float)this._motion.Length / 30f;
				}
				return 0f;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004A2C File Offset: 0x00002C2C
		public static GameObject CreateGameObject(string name = "MMDGameObject")
		{
			GameObject gameObject = new GameObject(name);
			gameObject.AddComponent<MeshFilter>();
			gameObject.AddComponent<MmdGameObject>();
			gameObject.AddComponent<SkinnedMeshRenderer>().quality = SkinQuality.Bone4;
			return gameObject;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004A50 File Offset: 0x00002C50
		public bool LoadModel(string path = null)
		{
			try
			{
				this.DoLoadModel(path);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				return false;
			}
			Utils.ClearAllTransformChild(base.transform);
			this._bones = this.CreateBones(base.gameObject);
			if (this._motion != null)
			{
				this.ResetMotionPlayer();
			}
			this._playTime = 0f;
			this.UpdateBones(false);
			return true;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public void LoadMotion(string path)
		{
			if (this._model == null)
			{
				throw new InvalidOperationException("model not loaded yet");
			}
			if (this.importedVmdPath.FindIndex((string v) => path == v) >= 0)
			{
				return;
			}
			this.LoadMotionKernal(path);
			this._playTime = 0f;
			this.UpdateBones(false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004B28 File Offset: 0x00002D28
		public Dictionary<string, float> GetUpdatedMorph(float time)
		{
			Dictionary<string, float> dictionary = new Dictionary<string, float>();
			foreach (KeyValuePair<string, FaceMorph.MorphSetting[]> keyValuePair in FaceMorph.setting)
			{
				float weight = this._motion.GetMorphPose(keyValuePair.Key, (double)time).Weight;
				foreach (FaceMorph.MorphSetting morphSetting in keyValuePair.Value)
				{
					float num = Mathf.Lerp(morphSetting.min, morphSetting.max, weight);
					if (dictionary.ContainsKey(morphSetting.name))
					{
						if (dictionary[morphSetting.name] < num)
						{
							dictionary[morphSetting.name] = num;
						}
					}
					else
					{
						dictionary.Add(morphSetting.name, num);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004C14 File Offset: 0x00002E14
		public List<FloatParamsJson> GetMorphKeyFrames(float beginTime, float endTime)
		{
			List<FloatParamsJson> list = new List<FloatParamsJson>();
			Dictionary<string, FloatParamsJson> dictionary = new Dictionary<string, FloatParamsJson>();
			LogUtil.Log("this._motion.MorphMotions count:"   + this._motion.MorphMotions.Count().ToString());
			foreach (KeyValuePair<string, List<KeyValuePair<int, MorphKeyframe>>> keyValuePair in this._motion.MorphMotions)
			{
				if (FaceMorph.setting.ContainsKey(keyValuePair.Key))
				{
					foreach (FaceMorph.MorphSetting morphSetting in FaceMorph.setting[keyValuePair.Key])
					{
						if (!dictionary.ContainsKey(morphSetting.name))
						{
							FloatParamsJson floatParamsJson = new FloatParamsJson();
							floatParamsJson.Name = morphSetting.name;
							dictionary.Add(morphSetting.name, floatParamsJson);
						}
						foreach (KeyValuePair<int, MorphKeyframe> keyValuePair2 in keyValuePair.Value)
						{
							float num = (float)keyValuePair2.Key / 30f;
							if (num >= beginTime && num <= endTime)
							{
								TimelineFrameJson timelineFrameJson = new TimelineFrameJson();
								timelineFrameJson.t = (num - beginTime).ToString();
								timelineFrameJson.frame = keyValuePair2.Key;
								timelineFrameJson.value = Mathf.Lerp(morphSetting.min, morphSetting.max, keyValuePair2.Value.Weight);
								timelineFrameJson.v = timelineFrameJson.value.ToString();
								timelineFrameJson.c = "3";
								timelineFrameJson.i = timelineFrameJson.v;
								timelineFrameJson.o = timelineFrameJson.v;
								if (dictionary[morphSetting.name].ValueLookup.ContainsKey(timelineFrameJson.frame))
								{
									TimelineFrameJson timelineFrameJson2 = dictionary[morphSetting.name].ValueLookup[timelineFrameJson.frame];
									if (timelineFrameJson2.value < timelineFrameJson.value)
									{
										dictionary[morphSetting.name].Value.Remove(timelineFrameJson2);
										dictionary[morphSetting.name].ValueLookup.Remove(timelineFrameJson.frame);
										dictionary[morphSetting.name].Value.Add(timelineFrameJson);
										dictionary[morphSetting.name].ValueLookup.Add(timelineFrameJson.frame, timelineFrameJson);
									}
								}
								else
								{
									dictionary[morphSetting.name].Value.Add(timelineFrameJson);
									dictionary[morphSetting.name].ValueLookup.Add(timelineFrameJson.frame, timelineFrameJson);
								}
							}
						}
					}
				}
				else
				{
					Debug.LogWarning("not process " + keyValuePair.Key);
				}
			}
			foreach (KeyValuePair<string, FloatParamsJson> keyValuePair3 in dictionary)
			{
				keyValuePair3.Value.Value.Sort(delegate(TimelineFrameJson a, TimelineFrameJson b)
				{
					if (a.frame > b.frame)
					{
						return 1;
					}
					if (a.frame < b.frame)
					{
						return -1;
					}
					return 0;
				});
				list.Add(keyValuePair3.Value);
			}
			return list;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004FAC File Offset: 0x000031AC
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			this.Release();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004FBA File Offset: 0x000031BA
		public void ChangeInitTransform()
		{
			if (this.m_ChangeInitTransform != null)
			{
				this.m_ChangeInitTransform(this._model);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004FD5 File Offset: 0x000031D5
		private void DoLoadModel(string path)
		{
			this._model = ModelReader2.LoadMmdModel(path);
			this.ChangeInitTransform();
			this.Release();
			this._poser = new Poser(this._model);
			Debug.LogFormat("load model finished", new object[0]);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00005010 File Offset: 0x00003210
		public string GetImportedVmds()
		{
			List<string> list = new List<string>();
			foreach (string path in this.importedVmdPath)
			{
				string text = FileManagerSecure.GetFileName(path);
				if (text.EndsWith(".vmd"))
				{
					text = text.Substring(0, text.Length - 4);
				}
				list.Add(text);
			}
			return string.Join(",", list.ToArray());
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000509C File Offset: 0x0000329C
		private bool LoadMotionKernal(string filePath)
		{
			if (this._motion == null)
			{
				SuperController.LogMessage("this._motion = new:" + filePath);
				this.importedVmdPath.Add(filePath);
				this._motion = new VmdReader2().Read(filePath);
				SuperController.LogMessage(String.Format("_motion.BoneMotions.Count:{0};_motion.MorphMotions.Count:{1}", _motion.BoneMotions.Count,_motion.MorphMotions.Count));
			}
			else
			{
				this.importedVmdPath.Add(filePath);
				MmdMotion mmdMotion = new VmdReader2().Read(filePath);
				SuperController.LogMessage("this._motion combin:" + filePath);
                this._motion.Length = Mathf.Max(this._motion.Length, mmdMotion.Length);
                this._motion.CombineBoneMotions(mmdMotion.BoneMotions);
                this._motion.CombineMorphMotions(mmdMotion.MorphMotions);

                SuperController.LogMessage(String.Format("mmdMotion.BoneMotions.Count:{0};mmdMotion.MorphMotions.Count:{1}", mmdMotion.BoneMotions.Count, mmdMotion.MorphMotions.Count));

                SuperController.LogMessage(String.Format("_motion.BoneMotions.Count:{0};_motion.MorphMotions.Count:{1}", _motion.BoneMotions.Count, _motion.MorphMotions.Count));

			}
			SuperController.LogMessage("MmdGameObject._motion.Length:" + this._motion.Length.ToString());
			if (this._motion.Length == 0)
			{
				this._poser.ResetPosing();
				this.ResetMotionPlayer();
				return false;
			}
			this.ResetMotionPlayer();
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00005152 File Offset: 0x00003352
		private void ResetMotionPlayer()
		{
			this._motionPlayer = new MotionPlayer(this._motion, this._poser);
			this._motionPlayer.SeekFrame(0);
			this._poser.PrePhysicsPosing(true);
			this._poser.PostPhysicsPosing();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00005190 File Offset: 0x00003390
		public List<int> GetMotionKeyFrames(float beginTime, float endTime)
		{
			Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>> boneMotions = this._motion.BoneMotions;
			HashSet<int> hashSet = new HashSet<int>();
			foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in boneMotions)
			{
				foreach (KeyValuePair<int, BoneKeyframe> keyValuePair2 in keyValuePair.Value)
				{
					hashSet.Add(keyValuePair2.Key);
				}
			}
			List<int> list = new List<int>();
			int num = (int)(beginTime * 30f);
			int num2 = (int)(endTime * 30f);
			foreach (int num3 in hashSet)
			{
				if (num3 >= num && num3 <= num2)
				{
					list.Add(num3);
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000052A0 File Offset: 0x000034A0
		public void UpdateBones(bool skipPhysicsControlBones = false)
		{
			if (this.CanNotUpdateBone())
			{
				return;
			}
			for (int i = 0; i < this._bones.Length; i++)
			{
				this.UpdateBone(i);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000052D0 File Offset: 0x000034D0
		private bool CanNotUpdateBone()
		{
			return this._bones == null || this._poser == null || this._model == null || this._poser.BoneImages.Length != this._bones.Length || this._model.Bones.Length != this._bones.Length;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005328 File Offset: 0x00003528
		private void UpdateBone(int i)
		{
			Transform transform = this._boneRootGameObject.transform;
			Matrix4x4 skinningMatrix = this._poser.BoneImages[i].SkinningMatrix;
			this._bones[i].transform.position = transform.TransformPoint(skinningMatrix.MultiplyPoint3x4(this._model.Bones[i].Position));
			this._bones[i].transform.rotation = transform.rotation * skinningMatrix.ExtractRotation();
			this._bones[i].transform.localScale = Vector3.one;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000053BF File Offset: 0x000035BF
		private void Release()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000053C1 File Offset: 0x000035C1
		public void ResetMotion()
		{
			if (this._motionPlayer == null)
			{
				return;
			}
			this._playTime = 0f;
			this._motionPlayer.SeekFrame(0);
			this._poser.PrePhysicsPosing(true);
			this._poser.PostPhysicsPosing();
			this.UpdateBones(false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00005401 File Offset: 0x00003601
		public double GetMotionPos()
		{
			return (double)this._playTime;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000540C File Offset: 0x0000360C
		public void SetMotionPos(float pos, bool update = true)
		{
			if (this._motion == null)
			{
				return;
			}
			this._playTime = pos;
			this._motionPlayer.SeekTime((double)this._playTime);
			this._poser.PrePhysicsPosing(true);
			this._poser.PostPhysicsPosing();
			this.UpdateBones(false);
			if (update && this.OnUpdate != null)
			{
				this.OnUpdate(this);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005470 File Offset: 0x00003670
		private GameObject[] CreateBones(GameObject rootGameObject)
		{
			if (this._model == null)
			{
				return new GameObject[0];
			}
			GameObject[] array = this.EntryAttributeForBones();
			this.AttachParentsForBone(rootGameObject, array);
			return array;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000549C File Offset: 0x0000369C
		private GameObject[] EntryAttributeForBones()
		{
			return this._model.Bones.Select(delegate(Bone x)
			{
				GameObject gameObject = new GameObject(x.Name);
				if (Settings.s_Debug)
				{
					GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					gameObject2.transform.parent = gameObject.transform;
					gameObject2.transform.localPosition = Vector3.zero;
					gameObject2.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
					gameObject2.GetComponent<MeshRenderer>().material.color = Color.red;
					UnityEngine.Object.DestroyImmediate(gameObject2.GetComponent<Collider>());
				}
				gameObject.transform.position = x.InitPosition;
				return gameObject;
			}).ToArray<GameObject>();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000054D4 File Offset: 0x000036D4
		private void AttachParentsForBone(GameObject rootGameObject, GameObject[] bones)
		{
			GameObject gameObject = new GameObject("Model");
			this._boneRootGameObject = gameObject;
			Transform transform = gameObject.transform;
			base.GetComponent<SkinnedMeshRenderer>().rootBone = transform;
			transform.parent = rootGameObject.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			int i = 0;
			int num = this._model.Bones.Length;
			while (i < num)
			{
				int parentIndex = this._model.Bones[i].ParentIndex;
				bones[i].transform.parent = ((parentIndex < bones.Length && parentIndex >= 0) ? bones[parentIndex].transform : transform);
				i++;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005598 File Offset: 0x00003798
		private int GetRelBoneIndexFromNearbyRigidbody(int rigidbodyIndex)
		{
			int boneCount = this._model.Bones.Length;
			int num = this._model.Rigidbodies[rigidbodyIndex].AssociatedBoneIndex;
			if (num < boneCount)
			{
				return num;
			}
			foreach (int rigidbodyIndex2 in from x in this._model.Constraints
			where x.AssociatedRigidBodyIndex[1] == rigidbodyIndex
			where x.AssociatedRigidBodyIndex[0] < boneCount
			select x.AssociatedRigidBodyIndex[0])
			{
				num = this.GetRelBoneIndexFromNearbyRigidbody(rigidbodyIndex2);
				if (num < boneCount)
				{
					return num;
				}
			}
			foreach (int rigidbodyIndex3 in from x in this._model.Constraints
			where x.AssociatedRigidBodyIndex[0] == rigidbodyIndex
			where x.AssociatedRigidBodyIndex[1] < boneCount
			select x.AssociatedRigidBodyIndex[1])
			{
				num = this.GetRelBoneIndexFromNearbyRigidbody(rigidbodyIndex3);
				if (num < boneCount)
				{
					return num;
				}
			}
			return int.MaxValue;
		}

		// Token: 0x04000014 RID: 20
		public Action<MmdGameObject> OnUpdate;

		// Token: 0x04000015 RID: 21
		public bool AutoPhysicsStepLength = true;

		// Token: 0x04000016 RID: 22
		public bool Playing;

		// Token: 0x04000017 RID: 23
		public int PhysicsCacheFrameSize = 300;

		// Token: 0x04000018 RID: 24
		public MmdGameObject.PhysicsModeEnum PhysicsMode;

		// Token: 0x04000019 RID: 25
		public float PhysicsFps = 120f;

		// Token: 0x0400001B RID: 27
		public GameObject[] _bones;

		// Token: 0x0400001C RID: 28
		public MmdModel _model;

		// Token: 0x0400001D RID: 29
		public Poser _poser;

		// Token: 0x0400001E RID: 30
		public MmdMotion _motion;

		// Token: 0x0400001F RID: 31
		public MotionPlayer _motionPlayer;

		// Token: 0x04000020 RID: 32
		public float _playTime;

		// Token: 0x04000021 RID: 33
		private List<List<int>> _partIndexes;

		// Token: 0x04000022 RID: 34
		private List<Vector3[]> _partMorphVertexCache;


		// Token: 0x04000024 RID: 36
		private GameObject _boneRootGameObject;

		// Token: 0x04000025 RID: 37
		public Action<MmdModel> m_ChangeInitTransform;

		// Token: 0x04000026 RID: 38
		private List<string> importedVmdPath = new List<string>();

		// Token: 0x0200003D RID: 61
		public enum PhysicsModeEnum
		{
			// Token: 0x04000158 RID: 344
			None,
			// Token: 0x04000159 RID: 345
			Unity
		}
	}
}
