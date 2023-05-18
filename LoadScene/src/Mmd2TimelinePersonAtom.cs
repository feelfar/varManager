using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibMMD.Model;
using LibMMD.Motion;
using LibMMD.Reader;
using LibMMD.Unity3D;
using MeshVR.Hands;
using MVR.FileManagementSecure;
using SimpleJSON;
using uFileBrowser;
using UnityEngine;

namespace mmd2timeline
{
    // Token: 0x0200002C RID: 44
    internal class Mmd2TimelinePersonAtom
	{
		public Mmd2TimelinePersonAtom(Atom personAtom)
		{
			m_PersonAtom = personAtom;
			Init();
		}
		public void Init()
		{
			Debug.Log("Init");
			
			if (this.WithAsset())
			{
				this.m_AssetBoneProcess = new AssetBoneProcess();
				this.m_AssetBoneProcess.Init(this);
			}
			this.enableHeel = new JSONStorableBool("Enable High Heel", false, new JSONStorableBool.SetBoolCallback(this.EnableHighHeel));
			this.holdRotationMaxForceAdjust = new JSONStorableFloat("Foot Hold Rotation Max Force", 0f, 0f, 1000f, true, true);
			this.footJointDriveXTargetAdjust = new JSONStorableFloat("Foot Joint Drive X Angle", -45f, new JSONStorableFloat.SetFloatCallback(this.SetJointDriveXAngle), -65f, 40f, true, true);
			this.toeJointDriveXTargetAdjust = new JSONStorableFloat("Toe Joint Drive X Angle", 35f, new JSONStorableFloat.SetFloatCallback(this.SetJointDriveXAngle), -40f, 75f, true, true);
			this.pos = new JSONStorableVector3("PosY", new Vector3(0, 0, 0), new JSONStorableVector3.SetVector3Callback(this.SetPos), new Vector3(-1,-1,-1), new Vector3(1, 1, 1), true, true);
			this.startTime = new JSONStorableFloat("Start Time", 0f, new JSONStorableFloat.SetFloatCallback(this.SetStartTime), 0f, 10f, true, true);
			this.endTime = new JSONStorableFloat("End Time", 0f, new JSONStorableFloat.SetFloatCallback(this.SetEndTime), 0f, 10f, true, true);
			this.motionScale = new JSONStorableFloat("Motion Scale", 1f, new JSONStorableFloat.SetFloatCallback(this.SetMotionScale), 0.1f, 2f, true, true);
			this.smoothArmAdjust = new JSONStorableFloat("Smooth Arm Adjust", 0f, new JSONStorableFloat.SetFloatCallback(this.SmoothArm), 0f, 1f, true, true);
			this.sampleSpeed = new JSONStorableFloat("Sample Speed", 2f, new JSONStorableFloat.SetFloatCallback(this.SetSampleSpeed), 0.1f, 3f, true, true);
			this.sampleRateChooser = new JSONStorableStringChooser("Sample Mode", new List<string>
			{
				"EveryFrame",
				"KeyFrame"
			}, "KeyFrame", "Sample Mode");
			this.playProgress = new JSONStorableFloat("Preview", 0f, new JSONStorableFloat.SetFloatCallback(this.SetProgress), 0f, 10f, true, true);
			
		}
		// Token: 0x0600020D RID: 525 RVA: 0x0000BD6C File Offset: 0x00009F6C
		public virtual bool WithAsset()
		{
			return false;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000BD6F File Offset: 0x00009F6F
		private void Start()
		{
			this.EnableHighHeel(this.enableHeel.val);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000BD84 File Offset: 0x00009F84
		public string GetTimeText()
		{
			return string.Format("Time:{0:F2}/{1:F2} Frame:{2}", (float)this.CurFrame / 30f, this.endTime.val - this.startTime.val, this.CurFrame);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		private void SetMotionScale(float val)
		{
			Settings.s_MotionScale = val;
			if (this.m_MmdPersonGameObject != null)
			{
				this.m_MmdPersonGameObject.SetMotionPos(this.m_MmdPersonGameObject._playTime, true);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000BE01 File Offset: 0x0000A001
		private void SmoothArm(float val)
		{
			Settings.s_SmoothArm = val;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000BE09 File Offset: 0x0000A009
		public void SetStraightLegWorkAngle(float val)
		{
			Settings.s_StraightLegWorkAngle = val;
			if (this.m_MmdPersonGameObject == null)
			{
				return;
			}
			this.m_MmdPersonGameObject.SetMotionPos(this.m_MmdPersonGameObject._playTime, true);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000BE37 File Offset: 0x0000A037
		public void StraightLeg(float val)
		{
			Settings.s_StraightLeg = val;
			if (this.m_MmdPersonGameObject == null)
			{
				return;
			}
			this.m_MmdPersonGameObject.SetMotionPos(this.m_MmdPersonGameObject._playTime, true);
		}
		public void IgnoreFace(bool val)
		{
			Settings.s_IgnoreFace = val;
		}
		// Token: 0x06000214 RID: 532 RVA: 0x0000BE68 File Offset: 0x0000A068
		private void SetPos(Vector3 pos)
		{
			Vector3 localPosition = this.m_PersonAtom.mainController.transform.localPosition;
			this.m_PersonAtom.mainController.transform.localPosition = pos;
			this.SetTransform();
		}
		
		// Token: 0x06000216 RID: 534 RVA: 0x0000C62C File Offset: 0x0000A82C
		public HashSet<int> GetBodyKeyFrames(float beginTime, float endTime)
		{
			int num = (int)(beginTime * 30f);
			int num2 = (int)(endTime * 30f);
			Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>> boneMotions = this.m_MmdPersonGameObject._motion.BoneMotions;
			HashSet<int> hashSet = new HashSet<int>();
			foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in boneMotions)
			{
				bool flag = true;
				if (DazBoneMapping.fingerBoneNames.Contains(keyValuePair.Key))
				{
					flag = false;
				}
				if (flag)
				{
					foreach (KeyValuePair<int, BoneKeyframe> keyValuePair2 in keyValuePair.Value)
					{
						int key = keyValuePair2.Key;
						if (key >= num && key <= num2)
						{
							hashSet.Add(key);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000C714 File Offset: 0x0000A914
		private Dictionary<string, HashSet<int>> GetFingerKeyFrames(float beginTime, float endTime)
		{
			int num = (int)(beginTime * 30f);
			int num2 = (int)(endTime * 30f);
			Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>> boneMotions = this.m_MmdPersonGameObject._motion.BoneMotions;
			Dictionary<string, HashSet<int>> dictionary = new Dictionary<string, HashSet<int>>();
			foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in boneMotions)
			{
				if (DazBoneMapping.fingerBoneNames.Contains(keyValuePair.Key))
				{
					HashSet<int> hashSet = new HashSet<int>();
					foreach (KeyValuePair<int, BoneKeyframe> keyValuePair2 in keyValuePair.Value)
					{
						int key = keyValuePair2.Key;
						if (key >= num && key <= num2)
						{
							hashSet.Add(key);
						}
					}
					dictionary.Add(keyValuePair.Key, hashSet);
				}
			}
			return dictionary;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C810 File Offset: 0x0000AA10
		private void SetSampleSpeed(float val)
		{
			if (this.m_MmdPersonGameObject == null)
			{
				return;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C81F File Offset: 0x0000AA1F
		private void SetProgress(float val)
		{
			if (this.m_MmdPersonGameObject == null)
			{
				return;
			}
			this.m_MmdPersonGameObject.SetMotionPos(val, true);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C840 File Offset: 0x0000AA40
		private void SetStartTime(float val)
		{
			if (this.m_MmdPersonGameObject == null)
			{
				return;
			}
			this.m_MmdPersonGameObject.SetMotionPos(val, true);
			float max = this.endTime.max;
			this.endTime.min = val;
			this.endTime.max = max;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C88D File Offset: 0x0000AA8D
		private void SetEndTime(float val)
		{
			if (this.m_MmdPersonGameObject == null)
			{
				return;
			}
			this.endTime.val = val;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C8AA File Offset: 0x0000AAAA
		public void ImportVmd(string path)
		{
            try
            {
				m_MmdPersonGameObject.LoadMotion(path);
                m_MmdPersonGameObject.SetMotionPos(0f, true);
				//SuperController.LogMessage("this.m_MmdPersonGameObject.MotionLength" + this.m_MmdPersonGameObject.MotionLength.ToString());
				this.playProgress.max = this.m_MmdPersonGameObject.MotionLength;
				this.startTime.max = this.m_MmdPersonGameObject.MotionLength;
				this.endTime.max = this.m_MmdPersonGameObject.MotionLength;
				this.endTime.val = this.endTime.max;
			}
            catch (Exception ex)
            {
				SuperController.LogError("\nMessage ---\n" + ex.Message);
				SuperController.LogError(
				   "\nStackTrace ---\n" + ex.StackTrace);
				/*
				SuperController.LogError("\nSource ---\n{0}" + ex.Source);
                SuperController.LogError(
					"\nHelpLink ---\n" + ex.HelpLink);
                SuperController.LogError(
                    "\nTargetSite ---\n" + ex.TargetSite);
				*/
			}
		}
        public TimelineJson StartPlay(string audioSource="",bool istest=false)
        {
			if (this.m_MmdPersonGameObject == null)
            {
				LogUtil.LogError("StartPlay:m_MmdPersonGameObject is NULL");

				return null;
            }
            if (this.m_MmdPersonGameObject._model == null)
            {
				LogUtil.LogError("StartPlay:m_MmdPersonGameObject._model is NULL");
				return null;
            }
			this.m_MmdPersonGameObject._playTime = 0f;
			this.fingerKeyFrames = this.GetFingerKeyFrames(this.startTime.val, this.endTime.val);
			this.bodyKeyFrames = this.GetBodyKeyFrames(this.startTime.val, this.endTime.val);
			LogUtil.Log(String.Format("fingerKeyFrames:{0}bodyKeyFrames :{1}", fingerKeyFrames.Count, bodyKeyFrames.Count));

			this.StepPlay(this.startTime.val, this.endTime.val,audioSource,istest);
			return this.m_PersonAniJson;
        }
        // Token: 0x0600021D RID: 541 RVA: 0x0000C8CC File Offset: 0x0000AACC
        private void SetTransform()
		{
			Transform transform = this.m_PersonAtom.mainController.transform;
			this.rootHandler.transform.SetPositionAndRotation(transform.position, transform.rotation);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C908 File Offset: 0x0000AB08
		public void InitAtom()
		{
			try
			{
				SuperController.singleton.StopAllCoroutines();
				if (this.m_MmdPersonGameObject != null)
				{
					UnityEngine.Object.Destroy(this.m_MmdPersonGameObject);
					this.m_MmdPersonGameObject = null;
				}
				if (this.m_ChoosePerson != null)
				{
					SuperController.singleton.StopCoroutine(this.m_ChoosePerson);
					this.m_ChoosePerson = null;
				}
				if (this.m_SampleCo != null)
				{
					SuperController.singleton.StopCoroutine(this.m_SampleCo);
					this.m_SampleCo = null;
				}
				
				this.IsSampling = false;
				this.CurFrame = 0;
				this.m_BeginTime = 0f;
				this.m_EndTime = 0f;

				this.IsSampling = false;
				this.CurFrame = 0;
				this.m_BeginTime = 0f;
				this.m_EndTime = 0f;

				this.m_PersonAtom.tempFreezePhysics = true;
				this.m_PersonAtom.ResetPhysics(true, true);
				this.CoLoad();
				this.m_PersonAtom.tempFreezePhysics = false;
				this.m_ChoosePerson = null;
				this.pos.SetValToDefault();
				//this.m_ChoosePerson = SuperController.singleton.StartCoroutine(this.CoChoosePerson());
			}
			catch (Exception ex)
            {
				SuperController.LogError("\nMessage ---\n" + ex.Message);
				SuperController.LogError(
				   "\nStackTrace ---\n" + ex.StackTrace);
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C9B7 File Offset: 0x0000ABB7
		private IEnumerator CoChoosePerson()
		{
			this.m_PersonAtom.tempFreezePhysics = true;
			this.m_PersonAtom.ResetPhysics(true, true);
			int num;
			for (int i = 0; i < 20; i = num + 1)
			{
				yield return null;
				num = i;
			}
			for (int i = 0; i < 5; i = num + 1)
			{
				yield return null;
				num = i;
			}
			this.CoLoad();
			this.m_PersonAtom.tempFreezePhysics = false;
			this.m_ChoosePerson = null;
			yield break;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		private void Prepare()
		{
			JSONStorable storableByID = this.m_PersonAtom.GetStorableByID("LeftHandControl");
			storableByID.SetStringChooserParamValue("fingerControlMode", "JSONParams");
			JSONStorable storableByID2 = this.m_PersonAtom.GetStorableByID("RightHandControl");
			storableByID2.SetStringChooserParamValue("fingerControlMode", "JSONParams");
			Utility.ResetHandControl(storableByID as HandControl);
			Utility.ResetHandControl(storableByID2 as HandControl);
			GenerateDAZMorphsControlUI morphsControlUI = (this.m_PersonAtom.GetStorableByID("geometry") as DAZCharacterSelector).morphsControlUI;
			this.m_FaceMorphs.Clear();
			if (!Settings.s_IgnoreFace)
			{
				foreach (DAZMorph dazmorph in morphsControlUI.GetMorphs())
				{
					if (dazmorph.region == "Face" || dazmorph.region == "Head" || dazmorph.region == "Eyes")
					{
						this.m_FaceMorphs.Add(dazmorph.uid, dazmorph);
					}
				}
			}
			LogUtil.Log("FaceMorphs_Count:" + m_FaceMorphs.Count); 
			this.controllerLookup = new Dictionary<Transform, FreeControllerV3>();
			this.controllerNameLookup = new Dictionary<string, FreeControllerV3>();
			foreach (FreeControllerV3 freeControllerV in this.m_PersonAtom.freeControllers)
			{
				if (!(freeControllerV.name == "testesControl") && !(freeControllerV.name == "penisBaseControl") && !(freeControllerV.name == "penisMidControl") && !(freeControllerV.name == "penisTipControl"))
				{
					this.controllerNameLookup.Add(freeControllerV.name, freeControllerV);
					if (!(freeControllerV.name == "rNippleControl") && !(freeControllerV.name == "lNippleControl"))
					{
						if (this.enableHeel.val && (freeControllerV.name == "lToeControl" || freeControllerV.name == "rToeControl"))
						{
							freeControllerV.currentRotationState = FreeControllerV3.RotationState.Off;
							freeControllerV.currentPositionState = FreeControllerV3.PositionState.Off;
							freeControllerV.GetFloatJSONParam("jointDriveXTarget").val = this.toeJointDriveXTargetAdjust.val;
						}
						else
						{
							if (freeControllerV.name == "lToeControl" || freeControllerV.name == "rToeControl")
							{
								JSONStorableFloat floatJSONParam = freeControllerV.GetFloatJSONParam("jointDriveXTarget");
								floatJSONParam.val = floatJSONParam.defaultVal;
							}
							if (freeControllerV.name == "lFootControl" || freeControllerV.name == "rFootControl")
							{
								JSONStorableFloat floatJSONParam2 = freeControllerV.GetFloatJSONParam("holdRotationMaxForce");
								JSONStorableFloat floatJSONParam3 = freeControllerV.GetFloatJSONParam("jointDriveXTarget");
								if (this.enableHeel.val)
								{
									floatJSONParam2.val = this.holdRotationMaxForceAdjust.val;
									floatJSONParam3.val = this.footJointDriveXTargetAdjust.val;
								}
								else
								{
									floatJSONParam2.val = floatJSONParam2.defaultVal;
									floatJSONParam3.val = floatJSONParam3.defaultVal;
								}
							}
							freeControllerV.currentRotationState = FreeControllerV3.RotationState.On;
							freeControllerV.currentPositionState = FreeControllerV3.PositionState.On;
							if (freeControllerV.followWhenOff != null)
							{
								//LogUtil.Log("controllerLookup.Add " + freeControllerV.name);
								this.controllerLookup.Add(freeControllerV.followWhenOff, freeControllerV);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CD14 File Offset: 0x0000AF14
		private void CoLoad()
		{
			this.Prepare();
			GameObject gameObject = MmdGameObject.CreateGameObject("MmdGameObject");
			gameObject.transform.position = this.m_PersonAtom.transform.position;
			gameObject.transform.rotation = this.m_PersonAtom.transform.rotation;
			gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			this.m_MmdPersonGameObject = gameObject.GetComponent<MmdGameObject>();
			GameObject gameObject2 = new GameObject("MmdRoot");
			gameObject2.transform.position = this.m_PersonAtom.mainController.transform.position;
			gameObject2.transform.rotation = this.m_PersonAtom.mainController.transform.rotation;
			gameObject.transform.parent = gameObject2.transform;
			this.rootHandler = gameObject2.transform;
			GameObject temp = new GameObject();
			temp.transform.position = this.m_PersonAtom.transform.position;
			temp.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			Transform parent2 = this.m_PersonAtom.transform.Find("rescale2/PhysicsModel");
			if (parent2 == null)
			{
				parent2 = this.m_PersonAtom.transform.Find("rescale2/MoveWhenInactive/PhysicsModel");
			}
			GameObject gameObject3 = new GameObject("Root");
			Dictionary<string, Transform> check = DazBoneMapping.CreateFakeBones(gameObject3.transform, parent2);
			this.m_MmdPersonGameObject.m_ChangeInitTransform = delegate(MmdModel model)
			{
				for (int i = 0; i < model.Bones.Length; i++)
				{
					Bone bone = model.Bones[i];
					Vector3 position = DazBoneMapping.GetPosition(parent2.gameObject, bone, bone.Name, check);
					bone.Position = 10f * (temp.transform.TransformPoint(position) - temp.transform.position);
				}
			};
			this.m_MmdPersonGameObject.LoadModel(null);
			UnityEngine.Object.DestroyImmediate(gameObject3);
			this.m_MmdPersonGameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			this.m_MmdPersonGameObject.OnUpdate = delegate(MmdGameObject mmd)
			{
				List<GameObject> listFingerGameObject = new List<GameObject>();
				
					GameObject[] bones = mmd._bones;
					
					new List<GameObject>();
					foreach (GameObject mmdbone in bones)
					{
						Quaternion rotation = mmdbone.transform.rotation;
						if (DazBoneMapping.fingerBoneNames.Contains(mmdbone.name))
						{
							listFingerGameObject.Add(mmdbone);
						}
						else
						{
							//LogUtil.Log("mmdbone.name:" + mmdbone.name + " DazBoneMapping.ignoreUpdateBoneNames:" + DazBoneMapping.ignoreUpdateBoneNames.Count.ToString() + " DazBoneMapping.cachedBoneLookup:" + DazBoneMapping.cachedBoneLookup.Count.ToString());
							string bonename = mmdbone.name;
							if (!DazBoneMapping.ignoreUpdateBoneNames.Contains(bonename) && DazBoneMapping.cachedBoneLookup.ContainsKey(bonename))
							{
								Transform bonetransform = DazBoneMapping.cachedBoneLookup[mmdbone.name];
								if (bonetransform != null && (!this.enableHeel.val || (!(bonetransform.name == "lToe") && !(bonetransform.name == "rToe"))))
								{
									//LogUtil.Log("bonename:"+ bonename);
									Vector3 position = mmdbone.transform.position;
									if (this.controllerLookup.ContainsKey(bonetransform))
									{
										
										if (DazBoneMapping.armBones.Contains(bonename))
										{
											if (bonename.StartsWith("r") || bonename.StartsWith("右"))
											{
												this.controllerLookup[bonetransform].transform.SetPositionAndRotation(position, rotation * Quaternion.Euler(new Vector3(0f, 0f, 36f)) * Utility.quat);
											}
											else
											{
												this.controllerLookup[bonetransform].transform.SetPositionAndRotation(position, rotation * Quaternion.Euler(new Vector3(0f, 0f, -36f)) * Utility.quat);
											}
										}
										else
										{
											this.controllerLookup[bonetransform].transform.SetPositionAndRotation(position, rotation * Utility.quat);
										}
										//this.controllerLookup[bonetransform].followWhenOff = null;
									}
								}
							}
						}
					}
					foreach (GameObject item in listFingerGameObject)
					{
						this.UpdateFinger(item);
					}
					
					if (Settings.s_StraightLeg > 0f)
					{
						foreach (GameObject gameObject5 in bones)
						{
							Quaternion rotation2 = gameObject5.transform.rotation;
							string name2 = gameObject5.name;
							if (!DazBoneMapping.ignoreUpdateBoneNames.Contains(name2))
							{
								Vector3 position2 = gameObject5.transform.position;
								if (DazBoneMapping.cachedBoneLookup.ContainsKey(name2))
								{
									Transform transform2 = DazBoneMapping.cachedBoneLookup[name2];
									if (transform2 != null && (transform2.name == "lShin" || transform2.name == "rShin"))
									{
										string str = (transform2.name == "lShin") ? "l" : "r";
										Vector3 position3 = this.controllerNameLookup[str + "ThighControl"].transform.position;
										Vector3 position4 = this.controllerNameLookup[str + "FootControl"].transform.position;
										Vector3 vector = position2;
										Vector3 b = Vector3.Project(vector - position3, position4 - position3) + position3;
										Vector3.Distance(vector, b);
										if (Mathf.Abs(Vector3.Angle(position3 - vector, position4 - vector)) > Settings.s_StraightLegWorkAngle)
										{
											
											Vector3 position5 = Vector3.Lerp(position2, b, Settings.s_StraightLeg);
											this.controllerLookup[transform2].transform.SetPositionAndRotation(position5, rotation2 * Utility.quat);
										}
									}
								}
							}
						}
					}
				
				float relativeTime = this.GetRelativeTime();
				if (!Settings.s_IgnoreFace)
				{
					foreach (KeyValuePair<string, float> keyValuePair in mmd.GetUpdatedMorph(relativeTime))
					{
						if (this.m_FaceMorphs.ContainsKey(keyValuePair.Key))
						{
							this.m_FaceMorphs[keyValuePair.Key].morphValue = keyValuePair.Value;
						}
					}
				}
				if (Settings.s_SmoothArm > 0f)
				{
					foreach (KeyValuePair<Transform, FreeControllerV3> keyValuePair2 in this.controllerLookup)
					{
						FreeControllerV3 value = keyValuePair2.Value;
						if (value.name == "rArmControl" || value.name == "lArmControl" || value.name == "rShoulderControl" || value.name == "lShoulderControl" || value.name == "rHandControl" || value.name == "lHandControl")
						{
							Quaternion rotation3 = Quaternion.Slerp(value.transform.rotation, value.followWhenOff.rotation, Settings.s_SmoothArm);
							Vector3 position6 = Vector3.Lerp(value.transform.position, value.followWhenOff.position, Settings.s_SmoothArm);
							value.transform.SetPositionAndRotation(position6, rotation3);
						}
					}
				}
				if (this.IsSampling)
				{
					foreach (GameObject gameObject6 in listFingerGameObject)
					{
						Transform transform3 = DazBoneMapping.cachedBoneLookup[gameObject6.name];
						int curFrame = this.CurFrame;
						if (this.sampleRateChooser.val == "EveryFrame")
						{
							FingerOutput component = transform3.GetComponent<FingerOutput>();
							this.RecordFinger(relativeTime, transform3.name, component);
						}
						else if (this.fingerKeyFrames.ContainsKey(gameObject6.name) && this.fingerKeyFrames[gameObject6.name].Contains(curFrame))
						{
							FingerOutput component2 = transform3.GetComponent<FingerOutput>();
							this.RecordFinger(relativeTime, transform3.name, component2);
						}
					}
					foreach (KeyValuePair<string, TimelineControlJson> keyValuePair3 in this.timelineControlLookup)
					{
						if (!this.enableHeel.val || (!(keyValuePair3.Key == "lToeControl") && !(keyValuePair3.Key == "rToeControl")))
						{
							FreeControllerV3 freeController = this.controllerNameLookup[keyValuePair3.Key];
							TimelineControlJson value2 = keyValuePair3.Value;
							int curFrame2 = this.CurFrame;
							if (this.sampleRateChooser.val == "EveryFrame")
							{
								Utility.RecordController(relativeTime, freeController, value2);
							}
							else if (this.bodyKeyFrames.Contains(curFrame2))
							{
								Utility.RecordController(relativeTime, freeController, value2);
							}
						}
					}
				}
			};
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CF24 File Offset: 0x0000B124
		private void UpdateFinger(GameObject item)
		{
			string name = item.name;
			Quaternion rotation = item.transform.rotation;
			Transform transform = DazBoneMapping.cachedBoneLookup[item.name];
			Quaternion rhs = item.transform.rotation * Utility.quat;
			Quaternion rotation2 = item.transform.parent.rotation;
			Quaternion rotation3;
			if (name.StartsWith("r") || name.StartsWith("右"))
			{
				rhs = rotation * Quaternion.Euler(new Vector3(0f, 0f, 36f)) * Utility.quat;
				rotation3 = rotation2 * Quaternion.Euler(new Vector3(0f, 0f, 36f)) * Utility.quat;
			}
			else
			{
				rhs = rotation * Quaternion.Euler(new Vector3(0f, 0f, -36f)) * Utility.quat;
				rotation3 = rotation2 * Quaternion.Euler(new Vector3(0f, 0f, -36f)) * Utility.quat;
			}
			Quaternion targetLocalRotation = Quaternion.Inverse(rotation3) * rhs;
			DAZBone component = transform.GetComponent<DAZBone>();
			ConfigurableJoint component2 = transform.GetComponent<ConfigurableJoint>();
			component2.SetTargetRotationLocal(targetLocalRotation, Quaternion.identity);
			FingerOutput component3 = component.GetComponent<FingerOutput>();
			component3.ConvertRotation(component, component2);
			component3.UpdateOutput();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D088 File Offset: 0x0000B288
		private void RecordFinger(float time, string boneName, FingerOutput fingerOutput)
		{
			if (boneName == "lThumb1" || boneName == "rThumb1")
			{
				this.SetFingerKeyFrame(time, boneName, "thumbProximalBend", fingerOutput.currentBend);
				this.SetFingerKeyFrame(time, boneName, "thumbProximalSpread", fingerOutput.currentSpread);
				this.SetFingerKeyFrame(time, boneName, "thumbProximalTwist", fingerOutput.currentTwist);
				return;
			}
			if (boneName == "lThumb2" || boneName == "rThumb2")
			{
				this.SetFingerKeyFrame(time, boneName, "thumbMiddleBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lThumb3" || boneName == "rThumb3")
			{
				this.SetFingerKeyFrame(time, boneName, "thumbDistalBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lIndex1" || boneName == "rIndex1")
			{
				this.SetFingerKeyFrame(time, boneName, "indexProximalBend", fingerOutput.currentBend);
				this.SetFingerKeyFrame(time, boneName, "indexProximalSpread", fingerOutput.currentSpread);
				this.SetFingerKeyFrame(time, boneName, "indexProximalTwist", fingerOutput.currentTwist);
				return;
			}
			if (boneName == "lIndex2" || boneName == "rIndex2")
			{
				this.SetFingerKeyFrame(time, boneName, "indexMiddleBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lIndex3" || boneName == "rIndex3")
			{
				this.SetFingerKeyFrame(time, boneName, "indexDistalBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lMid1" || boneName == "rMid1")
			{
				this.SetFingerKeyFrame(time, boneName, "middleProximalBend", fingerOutput.currentBend);
				this.SetFingerKeyFrame(time, boneName, "middleProximalSpread", fingerOutput.currentSpread);
				this.SetFingerKeyFrame(time, boneName, "middleProximalTwist", fingerOutput.currentTwist);
				return;
			}
			if (boneName == "lMid2" || boneName == "rMid2")
			{
				this.SetFingerKeyFrame(time, boneName, "middleMiddleBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lMid3" || boneName == "rMid3")
			{
				this.SetFingerKeyFrame(time, boneName, "middleDistalBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lRing1" || boneName == "rRing1")
			{
				this.SetFingerKeyFrame(time, boneName, "ringProximalBend", fingerOutput.currentBend);
				this.SetFingerKeyFrame(time, boneName, "ringProximalSpread", fingerOutput.currentSpread);
				this.SetFingerKeyFrame(time, boneName, "ringProximalTwist", fingerOutput.currentTwist);
				return;
			}
			if (boneName == "lRing2" || boneName == "rRing2")
			{
				this.SetFingerKeyFrame(time, boneName, "ringMiddleBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lRing3" || boneName == "rRing3")
			{
				this.SetFingerKeyFrame(time, boneName, "ringDistalBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lPinky1" || boneName == "rPinky1")
			{
				this.SetFingerKeyFrame(time, boneName, "pinkyProximalBend", fingerOutput.currentBend);
				this.SetFingerKeyFrame(time, boneName, "pinkyProximalSpread", fingerOutput.currentSpread);
				this.SetFingerKeyFrame(time, boneName, "pinkyProximalTwist", fingerOutput.currentTwist);
				return;
			}
			if (boneName == "lPinky2" || boneName == "rPinky2")
			{
				this.SetFingerKeyFrame(time, boneName, "pinkyMiddleBend", fingerOutput.currentBend);
				return;
			}
			if (boneName == "lPinky3" || boneName == "rPinky3")
			{
				this.SetFingerKeyFrame(time, boneName, "pinkyDistalBend", fingerOutput.currentBend);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D404 File Offset: 0x0000B604
		public void ExportPersonAnimation()
		{
			FileBrowser fileBrowserUI = SuperController.singleton.fileBrowserUI;
			fileBrowserUI.SetTitle("Export animation");
			fileBrowserUI.fileRemovePrefix = null;
			fileBrowserUI.hideExtension = false;
			fileBrowserUI.keepOpen = false;
			fileBrowserUI.fileFormat = "json";
			fileBrowserUI.defaultPath = "Saves\\PluginData\\animations";
			fileBrowserUI.showDirs = true;
			fileBrowserUI.shortCuts = null;
			fileBrowserUI.browseVarFilesAsDirectories = false;
			fileBrowserUI.SetTextEntry(true);
			fileBrowserUI.Show(delegate(string path)
			{
				if (string.IsNullOrEmpty(path))
				{
					return;
				}
				if (!path.ToLower().EndsWith(".json"))
				{
					path += ".json";
				}
				string text = this.m_PersonAniJson.ToJsonClass().ToString();
				string path2 = "Saves/PluginData/animations/mmd2timeline/";
				if (!FileManagerSecure.DirectoryExists(path2, false))
				{
					FileManagerSecure.CreateDirectory(path2);
				}
				FileManagerSecure.WriteAllText(path, text);
			}, true);
			fileBrowserUI.ActivateFileNameField();
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000D488 File Offset: 0x0000B688
		public Vector3 AssetInitPosition
		{
			get
			{
				Vector3 result = Vector3.zero;
				if (this.m_MmdAssetGameObject != null)
				{
					foreach (Bone bone in this.m_MmdAssetGameObject._model.Bones)
					{
						if (bone.Name == this.m_AssetBoneProcess.m_BoneName)
						{
							result = bone.InitPosition * 0.1f;
							break;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		public List<string> GetCUAList()
		{
			List<string> list = new List<string>();
			foreach (Atom atom in SuperController.singleton.GetAtoms())
			{
				if (atom.type == "CustomUnityAsset")
				{
					list.Add(atom.name);
				}
			}
			return list;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D570 File Offset: 0x0000B770
		private void SetAssetBindHand(string hand)
		{
			LogUtil.Log("SetAssetBindHand " + hand);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000D584 File Offset: 0x0000B784
		private void SetAssetCUA(string cua)
		{
			LogUtil.LogWarning("SetAssetCUA " + cua);
			Atom cuaatom = null;
			foreach (Atom atom in SuperController.singleton.GetAtoms())
			{
				if (atom.type == "CustomUnityAsset" && atom.name == cua)
				{
					cuaatom = atom;
					break;
				}
			}
			this.m_AssetBoneProcess.m_CUAAtom = cuaatom;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000D618 File Offset: 0x0000B818
		private void SetAssetBone(string bone)
		{
			LogUtil.LogWarning("SetAssetBone " + bone);
			this.m_AssetBoneProcess.m_BoneName = bone;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000D638 File Offset: 0x0000B838
		public void ImportAssetVmd(string path)
		{
			LogUtil.Log("ImportAssetVmd " + path);
			this.m_AssetBoneProcess._motion = new VmdReader2().Read(path);
			if (this.m_MmdAssetGameObject != null)
			{
				this.m_MmdAssetGameObject.LoadMotion(path);
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, List<KeyValuePair<int, BoneKeyframe>>> keyValuePair in this.m_AssetBoneProcess._motion.BoneMotions)
			{
				list.Add(keyValuePair.Key);
			}
			this.m_AssetBoneChooser.choices = list;
			this.m_AssetBoneChooser.defaultVal = list[0];
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D700 File Offset: 0x0000B900
		public void ImportAssetBindRule(string path)
		{
			string[] array = FileManagerSecure.ReadAllText(path).Split(new char[]
			{
				'\r',
				'\n'
			});
			new List<KeyValuePair<int, string>>();
			this.m_AssetAttachKeyFrames = new Dictionary<int, string>();
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (!string.IsNullOrEmpty(text))
				{
					string[] array2 = text.Split(new char[]
					{
						':'
					});
					if (array2.Length == 2)
					{
						int key = int.Parse(array2[0].Trim());
						string text2 = array2[1].Trim();
						this.m_AssetAttachKeyFrames.Add(key, text2);
						LogUtil.LogWarning("AssetAttachKey " + key.ToString() + " " + text2);
					}
				}
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		public void ImportAssetPmx(string path)
		{
			if (this.m_MmdAssetGameObject != null)
			{
				UnityEngine.Object.Destroy(this.m_MmdAssetGameObject);
				this.m_MmdAssetGameObject = null;
			}
			GameObject gameObject = MmdGameObject.CreateGameObject("MmdAssetGameObject");
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			this.m_MmdAssetGameObject = gameObject.GetComponent<MmdGameObject>();
			new GameObject().transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			this.m_MmdAssetGameObject.LoadModel(path);
			List<string> list = new List<string>();
			foreach (GameObject gameObject2 in this.m_MmdAssetGameObject._bones)
			{
				list.Add(gameObject2.name);
			}
			this.m_MmdAssetGameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			this.m_MmdAssetGameObject.OnUpdate = delegate(MmdGameObject mmd)
			{
				foreach (GameObject gameObject3 in mmd._bones)
				{
					if (gameObject3.name == this.m_AssetBoneChooser.val)
					{
						this.m_AssetBoneProcess.m_CUAAtom.mainController.transform.SetPositionAndRotation(gameObject3.transform.position, gameObject3.transform.rotation * Utility.quat);
					}
				}
			};
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public void PreSampleAsset()
		{
			this.assetJson = new TimelineJson();
			this.assetJson.AtomType = "CustomUnityAsset";
			this.assetJson.Clips = new List<TimelineClipJson>();
			TimelineClipJson timelineClipJson = new TimelineClipJson();
			this.assetJson.Clips.Add(timelineClipJson);
			timelineClipJson.AnimationName = "asset motion";
			timelineClipJson.AnimationLength = (this.m_EndTime - this.m_BeginTime).ToString();
			timelineClipJson.Controllers = new List<TimelineControlJson>();
			this.assetControlJson = new TimelineControlJson();
			this.assetControlJson.Controller = "control";
			timelineClipJson.Controllers.Add(this.assetControlJson);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D984 File Offset: 0x0000BB84
		private void ExportAssetAnimation()
		{
			FileBrowser fileBrowserUI = SuperController.singleton.fileBrowserUI;
			fileBrowserUI.SetTitle("Export Asset Animation");
			fileBrowserUI.fileRemovePrefix = null;
			fileBrowserUI.hideExtension = false;
			fileBrowserUI.keepOpen = false;
			fileBrowserUI.fileFormat = "json";
			fileBrowserUI.defaultPath = "Saves\\PluginData\\animations";
			fileBrowserUI.showDirs = true;
			fileBrowserUI.shortCuts = null;
			fileBrowserUI.browseVarFilesAsDirectories = false;
			fileBrowserUI.SetTextEntry(true);
			fileBrowserUI.Show(delegate(string path)
			{
				if (string.IsNullOrEmpty(path))
				{
					return;
				}
				if (!path.ToLower().EndsWith(".json"))
				{
					path += ".json";
				}
				JSONClass jcTimeline=new JSONClass();
				string text = this.assetJson.ToJsonClass().ToString();
				string path2 = "Saves/PluginData/animations/mmd2timeline/";
				if (!FileManagerSecure.DirectoryExists(path2, false))
				{
					FileManagerSecure.CreateDirectory(path2);
				}
				FileManagerSecure.WriteAllText(path, text);
			}, true);
			fileBrowserUI.ActivateFileNameField();
		}


		// Token: 0x06000230 RID: 560 RVA: 0x0000DA78 File Offset: 0x0000BC78
		private void EnableHighHeel(bool v)
		{
			foreach (UIDynamic uidynamic in this.enableHeelUIElements)
			{
				uidynamic.gameObject.SetActive(v);
			}
			foreach (FreeControllerV3 freeControllerV in this.m_PersonAtom.freeControllers)
			{
				if (freeControllerV.name == "lToeControl" || freeControllerV.name == "rToeControl")
				{
					JSONStorableFloat floatJSONParam = freeControllerV.GetFloatJSONParam("jointDriveXTarget");
					if (this.enableHeel.val)
					{
						freeControllerV.currentRotationState = FreeControllerV3.RotationState.Off;
						freeControllerV.currentPositionState = FreeControllerV3.PositionState.Off;
						floatJSONParam.val = this.toeJointDriveXTargetAdjust.val;
					}
					else
					{
						freeControllerV.currentRotationState = FreeControllerV3.RotationState.On;
						freeControllerV.currentPositionState = FreeControllerV3.PositionState.On;
						freeControllerV.transform.localPosition = freeControllerV.startingLocalPosition;
						freeControllerV.transform.localRotation = freeControllerV.startingLocalRotation;
						floatJSONParam.val = floatJSONParam.defaultVal;
					}
				}
				else if (freeControllerV.name == "lFootControl" || freeControllerV.name == "rFootControl")
				{
					JSONStorableFloat floatJSONParam2 = freeControllerV.GetFloatJSONParam("holdRotationMaxForce");
					JSONStorableFloat floatJSONParam3 = freeControllerV.GetFloatJSONParam("jointDriveXTarget");
					if (this.enableHeel.val)
					{
						floatJSONParam2.val = this.holdRotationMaxForceAdjust.val;
						floatJSONParam3.val = this.footJointDriveXTargetAdjust.val;
					}
					else
					{
						floatJSONParam2.val = floatJSONParam2.defaultVal;
						floatJSONParam3.val = floatJSONParam3.defaultVal;
					}
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000DC28 File Offset: 0x0000BE28
		public void SetJointDriveXAngle(float val)
		{
			foreach (FreeControllerV3 freeControllerV in this.m_PersonAtom.freeControllers)
			{
				if (freeControllerV.name == "lFootControl" || freeControllerV.name == "rFootControl")
				{
					JSONStorableFloat floatJSONParam = freeControllerV.GetFloatJSONParam("holdRotationMaxForce");
					JSONStorableFloat floatJSONParam2 = freeControllerV.GetFloatJSONParam("jointDriveXTarget");
					if (this.enableHeel.val)
					{
						floatJSONParam.val = this.holdRotationMaxForceAdjust.val;
						floatJSONParam2.val = this.footJointDriveXTargetAdjust.val;
					}
				}
				else if (freeControllerV.name == "lToeControl" || freeControllerV.name == "rToeControl")
				{
					JSONStorableFloat floatJSONParam3 = freeControllerV.GetFloatJSONParam("jointDriveXTarget");
					if (this.enableHeel.val)
					{
						floatJSONParam3.val = this.toeJointDriveXTargetAdjust.val;
					}
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000DD18 File Offset: 0x0000BF18
		public float GetRelativeTime()
		{
			float value = (float)this.CurFrame / 30f - this.m_BeginTime;
			float min = 0f;
			float max = this.m_EndTime - this.m_BeginTime;
			return Mathf.Clamp(value, min, max);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000DD54 File Offset: 0x0000BF54
		public void SetFingerKeyFrame(float time, string boneName, string key, float value)
		{
			Dictionary<string, FloatParamsJson> dictionary = boneName.StartsWith("l") ? this.m_LeftFingerMotions : this.m_RightFingerMotions;
			TimelineFrameJson timelineFrameJson = new TimelineFrameJson();
			timelineFrameJson.t = time.ToString();
			timelineFrameJson.v = ((int)value).ToString();
			timelineFrameJson.c = "3";
			timelineFrameJson.i = timelineFrameJson.v;
			timelineFrameJson.o = timelineFrameJson.v;
			dictionary[key].Value.Add(timelineFrameJson);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		private void ImportVmd()
		{
			if (this.m_MmdPersonGameObject == null)
			{
				SuperController.LogError("[mmd2timeline]: need InitAtom");
				return;
			}
			try
			{
				SuperController.singleton.GetMediaPathDialog(delegate(string path)
				{
					if (string.IsNullOrEmpty(path))
					{
						return;
					}
					Settings.s_SmoothArm = 0f;
					this.smoothArmAdjust.val = 0f;
					this.ImportVmd(path);
					
				}, "vmd", "Saves", false, true, false, null, false, null, true, false);
			}
			catch (Exception arg)
			{
				SuperController.LogError(string.Format("[mmd2timeline]: Failed to open file dialog: {0}", arg));
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000DE48 File Offset: 0x0000C048
		private void Pause()
		{
			this.IsPausing = true;
			this.IsSteping = false;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000DE58 File Offset: 0x0000C058
		private void Continue()
		{
			this.IsPausing = false;
			this.IsSteping = false;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000DE68 File Offset: 0x0000C068
		private void NextKeyFrame()
		{
			this.IsSteping = true;
			this.IsPausing = false;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DE78 File Offset: 0x0000C078
		public void StepPlay(float beginTime, float endTime, string audioSource,bool istest)
		{
			this.m_BeginTime = beginTime;
			this.m_EndTime = endTime;
			LogUtil.Log("this.m_EndTime:" + this.m_EndTime.ToString());
			this.m_PersonAniJson = new TimelineJson();
			this.m_PersonAniJson.Clips = new List<TimelineClipJson>();
			TimelineClipJson timelineClipJson = new TimelineClipJson();
			if (!string.IsNullOrEmpty(audioSource))
				timelineClipJson.AudioSourceControl = audioSource;
			this.m_PersonAniJson.Clips.Add(timelineClipJson);
			if (!Settings.s_IgnoreFace)
			{
				List<FloatParamsJson> morphKeyFrames = this.m_MmdPersonGameObject.GetMorphKeyFrames(beginTime, endTime);
				LogUtil.Log("List<FloatParamsJson> morphKeyFrames:" + morphKeyFrames.Count.ToString());
				timelineClipJson.FloatParams = morphKeyFrames;
			}
			else
				timelineClipJson.FloatParams = new List<FloatParamsJson>();
			this.m_RightFingerMotions = new Dictionary<string, FloatParamsJson>();
			this.m_LeftFingerMotions = new Dictionary<string, FloatParamsJson>();
			for (int i = 0; i < 2; i++)
			{
				Dictionary<string, FloatParamsJson> dictionary = (i == 0) ? this.m_LeftFingerMotions : this.m_RightFingerMotions;
				foreach (string text in FingerMorph.setting)
				{
					FloatParamsJson floatParamsJson = new FloatParamsJson();
					floatParamsJson.Storable = FingerMorph.StorableNames[i];
					floatParamsJson.Name = text;
					floatParamsJson.Min = "-100";
					floatParamsJson.Max = "100";
					dictionary.Add(text, floatParamsJson);
					timelineClipJson.FloatParams.Add(floatParamsJson);
				}
			}
			
			timelineClipJson.AnimationName = this.m_MmdPersonGameObject._motion.Name;
			timelineClipJson.AnimationLength = (endTime - beginTime).ToString();
			timelineClipJson.Controllers = new List<TimelineControlJson>();
			LogUtil.Log(string.Format("timelineClipJson.AnimationName:{0};timelineClipJson.AnimationLength:{1} ", timelineClipJson.AnimationName, timelineClipJson.AnimationLength));
			this.timelineControlLookup = new Dictionary<string, TimelineControlJson>();
			foreach (string text2 in DazBoneMapping.vamControls)
			{
				if (!this.enableHeel.val || (!(text2 == "rToeControl") && !(text2 == "lToeControl")))
				{
					TimelineControlJson timelineControlJson = new TimelineControlJson();
					timelineControlJson.Controller = text2;
					this.timelineControlLookup.Add(text2, timelineControlJson);
					timelineClipJson.Controllers.Add(timelineControlJson);
				}
			}

			if (this.WithAsset())
			{
				this.PreSampleAsset();
			}
			//this.CoStepPlay(beginTime, endTime);
			
			if (this.m_SampleCo != null)
			{
				SuperController.singleton.StopCoroutine(this.m_SampleCo);
                this.m_SampleCo = null;
            }
            //if(istest)
            this.m_SampleCo = SuperController.singleton.StartCoroutine(this.CoStepPlay(beginTime, endTime, istest));
			
			this.m_MmdPersonGameObject.ResetMotion();
			if (this.m_MmdAssetGameObject != null)
			{
				this.m_MmdAssetGameObject.ResetMotion();
			}
			
			this.IsSampling = false;
			this.IsPausing = false;
			this.CurFrame = 0;
			//this.m_SampleCo = null;
			//else
			//	this.CoStepPlay1(beginTime, endTime);


		}

        // Token: 0x170000A0 RID: 160
        // (get) Token: 0x06000239 RID: 569 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
        // (set) Token: 0x0600023A RID: 570 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
        private bool IsPausing { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000E0C9 File Offset: 0x0000C2C9
		private bool IsSteping { get; set; }
        public Atom PersonAtom
        {
            get { return m_PersonAtom; }
            set { m_PersonAtom = value; }
        }

        private IEnumerator CoStepPlay(float beginTime, float endTime,bool isTest)
		{
			this.m_BeginTime = beginTime;
			this.m_EndTime = endTime;
			LogUtil.Log(String.Format("beginTime:{0};endTime:{1}", m_BeginTime, m_EndTime));
			this.IsPausing = false;
			this.IsSampling = true;
			List<int> personFrameSteps = this.m_MmdPersonGameObject.GetMotionKeyFrames(beginTime, endTime);
			LogUtil.Log(String.Format("personFrameSteps:{0}", personFrameSteps.Count));
			HashSet<int> assetFrameSet = null;
			if (this.WithAsset())
			{
				HashSet<int> hashSet = new HashSet<int>();
				foreach (int item in personFrameSteps)
				{
					hashSet.Add(item);
				}
				this.m_AssetBoneProcess.GetMotionKeyFrames(beginTime, endTime);
				foreach (int item2 in personFrameSteps)
				{
					hashSet.Add(item2);
				}
				personFrameSteps.Clear();
				foreach (int item3 in hashSet)
				{
					personFrameSteps.Add(item3);
				}
				personFrameSteps.Sort();
				assetFrameSet = this.m_AssetBoneProcess.GetMotionKeyFrameSet();
			}
			if (this.sampleRateChooser.val == "EveryFrame")
			{
				List<int> list = new List<int>();
				if (personFrameSteps.Count > 1)
				{
					int num = personFrameSteps[0];
					int num2 = personFrameSteps[personFrameSteps.Count - 1];
					for (int k = num; k <= num2; k++)
					{
						list.Add(k);
					}
				}
				personFrameSteps = list;
			}
			int num3;
			for (int i = 0; i < personFrameSteps.Count; i = num3 + 1)
			{
				this.CurFrame = personFrameSteps[i];
				if (this.m_AssetAttachKeyFrames != null && this.m_AssetAttachKeyFrames.ContainsKey(this.CurFrame))
				{
					this.m_AssetBoneProcess.Attach(this.CurFrame, this.m_MmdPersonGameObject, this.m_AssetAttachKeyFrames[this.CurFrame]);
				}
				this.m_MmdPersonGameObject._motionPlayer.SeekFrame(this.CurFrame);
				this.m_MmdPersonGameObject._poser.PrePhysicsPosing(false);
				this.m_MmdPersonGameObject._poser.PostPhysicsPosing();
				this.m_MmdPersonGameObject.UpdateBones(false);
				if (this.m_MmdPersonGameObject.OnUpdate != null)
				{
					//LogUtil.Log("MmdPersonGameObject.OnUpdate curFrame:" + CurFrame.ToString());
					this.m_MmdPersonGameObject.OnUpdate(this.m_MmdPersonGameObject);
				}
				if (this.m_MmdAssetGameObject != null)
				{
					this.m_MmdAssetGameObject._motionPlayer.SeekFrame(this.CurFrame);
					this.m_MmdAssetGameObject._poser.PrePhysicsPosing(false);
					this.m_MmdAssetGameObject._poser.PostPhysicsPosing();
					this.m_MmdAssetGameObject.UpdateBones(false);
				}
				if (this.WithAsset())
				{
					Physics.autoSimulation = false;
					for (int j = 0; j < 20; j = num3 + 1)
					{
						Physics.Simulate(Time.fixedDeltaTime);
						yield return new WaitForEndOfFrame();
						num3 = j;
					}
					Physics.autoSimulation = true;
				}
				int num4 = 1;
				if (i < personFrameSteps.Count - 1)
                {
                    num4 = personFrameSteps[i + 1] - this.CurFrame;
                }
                this.sampleSpeed.val = Math.Max(0.05f, this.sampleSpeed.val);
                if (isTest)
                    yield return new WaitForSeconds(0.0333333351f / this.sampleSpeed.val * (float)num4);
                if (this.WithAsset())
                {
                    if (this.m_AssetBoneProcess.m_FakeBone != null)
					{
						this.m_AssetBoneProcess.Process(this.CurFrame);
					}
					else if (this.m_MmdAssetGameObject != null && this.m_MmdAssetGameObject.OnUpdate != null)
					{
						this.m_MmdAssetGameObject.OnUpdate(this.m_MmdAssetGameObject);
					}
					else
					{
						this.m_AssetBoneProcess.Process(this.CurFrame);
					}
					if (assetFrameSet.Contains(this.CurFrame) || this.bodyKeyFrames.Contains(this.CurFrame))
					{
						Utility.RecordController(this.GetRelativeTime(), this.m_AssetBoneProcess.m_CUAAtom.mainController, this.assetControlJson);
					}
				}
				if (i >= personFrameSteps.Count - 1)
				{
					break;
				}
				if (this.IsSteping)
				{
					this.IsPausing = true;
				}
				while (this.IsPausing)
				{
					yield return new WaitForSeconds(0.5f);
				}
				num3 = i;
			}
			
			
			yield break;
		}        // Token: 0x0600023D RID: 573 RVA: 0x0000E0D2 File Offset: 0x0000C2D2
    	public void SavePluginPreset(string presetFileName)
		{
			try
			{
				string strPersonMmd2Timeline = "{\"setUnlistedParamsToDefault\":\"true\",\"storables\":[{\"id\":\"PluginManager\",\"plugins\":{\"plugin#0\":\"AcidBubbles.Timeline.latest:/Custom/Scripts/AcidBubbles/Timeline/VamTimeline.AtomAnimation.cslist\"}},{\"id\":\"plugin#0_VamTimeline.AtomPlugin\",\"enabled\":\"true\",\"pluginLabel\":\"\",\"Animation\":{\"Speed\":\"1\",\"Weight\":\"1\",\"Master\":\"0\",\"SyncWithPeers\":\"1\",\"SyncSubsceneOnly\":\"0\",\"TimeMode\":\"0\",\"LiveParenting\":\"0\",\"ForceBlendTime\":\"0\",\"Clips\":$$$clips$$$},\"Options\":{\"AutoKeyframeAllControllers\":\"0\",\"Snap\":\"0.1\",\"Locked\":\"1\",\"ShowPaths\":\"1\"}}]}";
				JSONClass jc = this.m_PersonAniJson.ToJsonClass();
				JSONArray ja = jc["Clips"] as JSONArray;
				StringBuilder stringBuilder = new StringBuilder(100000);
				ja.ToString(string.Empty, stringBuilder);
				strPersonMmd2Timeline = strPersonMmd2Timeline.Replace("$$$clips$$$", stringBuilder.ToString());
				string dirPluginPreset = "Custom/Atom/Person/Plugins/";
				string pluginPresetFullName = dirPluginPreset + presetFileName;
				if (!FileManagerSecure.DirectoryExists(dirPluginPreset, false))
				{
					FileManagerSecure.CreateDirectory(dirPluginPreset);
				}
				FileManagerSecure.WriteAllText(pluginPresetFullName, strPersonMmd2Timeline);
			}
			catch (Exception ex)
			{
				SuperController.LogError("\nMessage ---\n" + ex.Message);
				SuperController.LogError(
				   "\nStackTrace ---\n" + ex.StackTrace);
			}
		}
		public void SavePluginPreset1()
        {
            try
            {
                string strPersonMmd2Timeline = "{\"setUnlistedParamsToDefault\":\"true\",\"storables\":[{\"id\":\"PluginManager\",\"plugins\":{\"plugin#0\":\"AcidBubbles.Timeline.latest:/Custom/Scripts/AcidBubbles/Timeline/VamTimeline.AtomAnimation.cslist\"}},{\"id\":\"plugin#0_VamTimeline.AtomPlugin\",\"enabled\":\"true\",\"pluginLabel\":\"\",\"Animation\":{\"Speed\":\"1\",\"Weight\":\"1\",\"Master\":\"0\",\"SyncWithPeers\":\"1\",\"SyncSubsceneOnly\":\"0\",\"TimeMode\":\"2\",\"LiveParenting\":\"1\",\"ForceBlendTime\":\"0\",\"Clips\":[{\"AnimationName\":\"Anim1\",\"AnimationLength\":\"2\",\"BlendDuration\":\"1\",\"Loop\":\"1\",\"NextAnimationRandomizeWeight\":\"1\",\"AutoTransitionPrevious\":\"0\",\"AutoTransitionNext\":\"0\",\"SyncTransitionTime\":\"1\",\"SyncTransitionTimeNL\":\"0\",\"EnsureQuaternionContinuity\":\"1\",\"AnimationLayer\":\"Main\",\"Speed\":\"1\",\"Weight\":\"1\",\"Uninterruptible\":\"0\",\"AnimationSegment\":\"Segment1\"}]},\"Options\":{\"AutoKeyframeAllControllers\":\"0\",\"Snap\":\"0.1\",\"Locked\":\"0\",\"ShowPaths\":\"1\"}}]}";
                
                string dirPluginPreset = "Custom/Atom/Person/Plugins/";
                string pluginPresetFullName = dirPluginPreset + "Preset_mmdloader.vap";
                if (!FileManagerSecure.DirectoryExists(dirPluginPreset, false))
                {
                    FileManagerSecure.CreateDirectory(dirPluginPreset);
                }
                FileManagerSecure.WriteAllText(pluginPresetFullName, strPersonMmd2Timeline);
            }
            catch (Exception ex)
            {
                SuperController.LogError("\nMessage ---\n" + ex.Message);
                SuperController.LogError(
                   "\nStackTrace ---\n" + ex.StackTrace);
                /*
				SuperController.LogError("\nSource ---\n{0}" + ex.Source);
                SuperController.LogError(
					"\nHelpLink ---\n" + ex.HelpLink);
                SuperController.LogError(
                    "\nTargetSite ---\n" + ex.TargetSite);
				*/
            }
        }
        private Atom m_PersonAtom;
        // Token: 0x040000E9 RID: 233
        private AssetBoneProcess m_AssetBoneProcess;

        // Token: 0x040000ED RID: 237
        public JSONStorableFloat playProgress;

        // Token: 0x040000EE RID: 238
        public JSONStorableFloat smoothArmAdjust;

        // Token: 0x040000EF RID: 239
        public JSONStorableFloat straightLegWorkAngle;

        // Token: 0x040000F0 RID: 240
        public JSONStorableFloat straightLegAdjust;

        // Token: 0x040000F1 RID: 241
        public JSONStorableFloat motionScale;

        // Token: 0x040000F2 RID: 242
        public JSONStorableVector3 pos;

        // Token: 0x040000F3 RID: 243
        public JSONStorableFloat startTime;

        // Token: 0x040000F4 RID: 244
        public JSONStorableFloat endTime;

        // Token: 0x040000F5 RID: 245
        public JSONStorableFloat sampleSpeed;

        // Token: 0x040000F6 RID: 246
        public JSONStorableStringChooser sampleRateChooser;

        // Token: 0x040000F7 RID: 247
        public Dictionary<string, HashSet<int>> fingerKeyFrames;

		// Token: 0x040000F8 RID: 248
		public HashSet<int> bodyKeyFrames;

		// Token: 0x040000F9 RID: 249
		public MmdGameObject m_MmdPersonGameObject;

		// Token: 0x040000FA RID: 250
		private Coroutine m_ChoosePerson;

		// Token: 0x040000FB RID: 251
		public Dictionary<string, DAZMorph> m_FaceMorphs = new Dictionary<string, DAZMorph>();

		// Token: 0x040000FC RID: 252
		private Dictionary<Transform, FreeControllerV3> controllerLookup;

		// Token: 0x040000FD RID: 253
		private Dictionary<string, FreeControllerV3> controllerNameLookup;

		// Token: 0x040000FE RID: 254
		public Transform rootHandler;

		// Token: 0x040000FF RID: 255
		private const string _saveExt = "json";

		// Token: 0x04000100 RID: 256
		private const string _saveFolder = "Saves\\PluginData\\animations";

		// Token: 0x04000101 RID: 257
		private List<string> m_AssetBindHandType = new List<string>
		{
			"None",
			"RightHand",
			"LeftHand"
		};

		// Token: 0x04000102 RID: 258
		private JSONStorableStringChooser m_AssetBoneChooser;

		// Token: 0x04000103 RID: 259
		private JSONStorableStringChooser m_AssetCUAChooser;

		// Token: 0x04000104 RID: 260
		private JSONStorableStringChooser m_AssetBindHandChooser;

		// Token: 0x04000105 RID: 261
		private MmdGameObject m_MmdAssetGameObject;

		// Token: 0x04000106 RID: 262
		private Dictionary<int, string> m_AssetAttachKeyFrames;

		// Token: 0x04000107 RID: 263
		private TimelineControlJson assetControlJson;

		// Token: 0x04000108 RID: 264
		private TimelineJson assetJson;

		// Token: 0x04000109 RID: 265
		public List<UIDynamic> enableHeelUIElements = new List<UIDynamic>();

		// Token: 0x0400010A RID: 266
		public JSONStorableBool enableHeel;

		// Token: 0x0400010B RID: 267
		public JSONStorableFloat footJointDriveXTargetAdjust;

		// Token: 0x0400010C RID: 268
		public JSONStorableFloat toeJointDriveXTargetAdjust;

		// Token: 0x0400010D RID: 269
		public JSONStorableFloat holdRotationMaxForceAdjust;

		// Token: 0x0400010E RID: 270
		public int CurFrame;

		// Token: 0x0400010F RID: 271
		private float m_BeginTime;

		// Token: 0x04000110 RID: 272
		private float m_EndTime;

		// Token: 0x04000111 RID: 273
		public bool IsSampling;

		// Token: 0x04000112 RID: 274
		public TimelineJson m_PersonAniJson;

		// Token: 0x04000113 RID: 275
		public Dictionary<string, TimelineControlJson> timelineControlLookup;

		// Token: 0x04000114 RID: 276
		public Dictionary<string, FloatParamsJson> m_RightFingerMotions;

		// Token: 0x04000115 RID: 277
		public Dictionary<string, FloatParamsJson> m_LeftFingerMotions;

		// Token: 0x04000116 RID: 278
		private Coroutine m_SampleCo;
	}
}
