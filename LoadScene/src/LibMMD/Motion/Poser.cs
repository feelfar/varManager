using System;
using System.Collections.Generic;
using System.Linq;
using LibMMD.Model;
using LibMMD.Util;
using mmd2timeline;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x0200001E RID: 30
	public class Poser
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00008AA0 File Offset: 0x00006CA0
		public Poser(MmdModel model)
		{
			this.Model = model;
			int num = model.Bones.Length;
			this.BoneImages = new BoneImage[num];
			for (int i = 0; i < num; i++)
			{
				this.BoneImages[i] = new BoneImage();
			}
			for (int j = 0; j < num; j++)
			{
				Bone bone = model.Bones[j];
				this._boneNameMap[bone.Name] = j;
				BoneImage boneImage = this.BoneImages[j];
				boneImage.Name = bone.Name;
				boneImage.GlobalOffsetMatrix.m03 = -bone.Position[0];
				boneImage.GlobalOffsetMatrix.m13 = -bone.Position[1];
				boneImage.GlobalOffsetMatrix.m23 = -bone.Position[2];
				boneImage.GlobalOffsetMatrixInv.m03 = bone.Position[0];
				boneImage.GlobalOffsetMatrixInv.m13 = bone.Position[1];
				boneImage.GlobalOffsetMatrixInv.m23 = bone.Position[2];
				boneImage.Parent = bone.ParentIndex;
				if (boneImage.Parent < num && boneImage.Parent >= 0)
				{
					boneImage.HasParent = true;
					boneImage.LocalOffset = bone.Position - model.Bones[boneImage.Parent].Position;
				}
				else
				{
					boneImage.HasParent = false;
					boneImage.LocalOffset = bone.Position;
				}
				boneImage.AppendRotate = bone.AppendRotate;
				boneImage.AppendTranslate = bone.AppendTranslate;
				boneImage.HasAppend = false;
				if (boneImage.AppendRotate || boneImage.AppendTranslate)
				{
					boneImage.AppendParent = bone.AppendBoneVal.Index;
					if (boneImage.AppendParent < num)
					{
						boneImage.HasAppend = true;
						boneImage.AppendRatio = bone.AppendBoneVal.Ratio;
					}
				}
				boneImage.HasIk = bone.HasIk;
				if (boneImage.HasIk)
				{
					int num2 = bone.IkInfoVal.IkLinks.Length;
					boneImage.IkLinks = new int[num2];
					boneImage.IkFixTypes = new BoneImage.AxisFixType[num2];
					boneImage.IkTransformOrders = Enumerable.Repeat<BoneImage.AxisTransformOrder>(BoneImage.AxisTransformOrder.OrderYzx, num2).ToArray<BoneImage.AxisTransformOrder>();
					boneImage.IkLinkLimited = new bool[num2];
					boneImage.IkLinkLimitsMin = new Vector3[num2];
					boneImage.IkLinkLimitsMax = new Vector3[num2];
					for (int k = 0; k < num2; k++)
					{
						Bone.IkLink ikLink = bone.IkInfoVal.IkLinks[k];
						boneImage.IkLinks[k] = ikLink.LinkIndex;
						boneImage.IkLinkLimited[k] = ikLink.HasLimit;
						if (boneImage.IkLinkLimited[k])
						{
							for (int l = 0; l < 3; l++)
							{
								float val = ikLink.LoLimit[l];
								float val2 = ikLink.HiLimit[l];
								boneImage.IkLinkLimitsMin[k][l] = Math.Min(val, val2);
								boneImage.IkLinkLimitsMax[k][l] = Math.Max(val, val2);
							}
							if ((double)boneImage.IkLinkLimitsMin[k].x > -1.5707963267948966 && (double)boneImage.IkLinkLimitsMax[k].x < 1.5707963267948966)
							{
								boneImage.IkTransformOrders[k] = BoneImage.AxisTransformOrder.OrderZxy;
							}
							else if ((double)boneImage.IkLinkLimitsMin[k].y > -1.5707963267948966 && (double)boneImage.IkLinkLimitsMax[k].y < 1.5707963267948966)
							{
								boneImage.IkTransformOrders[k] = BoneImage.AxisTransformOrder.OrderXyz;
							}
							if (Math.Abs(boneImage.IkLinkLimitsMin[k].x) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].x) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMin[k].y) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].y) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMin[k].z) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].z) < 1E-07f)
							{
								boneImage.IkFixTypes[k] = BoneImage.AxisFixType.FixAll;
							}
							else if (Math.Abs(boneImage.IkLinkLimitsMin[k].y) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].y) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMin[k].z) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].z) < 1E-07f)
							{
								boneImage.IkFixTypes[k] = BoneImage.AxisFixType.FixX;
							}
							else if (Math.Abs(boneImage.IkLinkLimitsMin[k].x) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].x) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMin[k].z) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].z) < 1E-07f)
							{
								boneImage.IkFixTypes[k] = BoneImage.AxisFixType.FixY;
							}
							else if (Math.Abs(boneImage.IkLinkLimitsMin[k].x) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].x) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMin[k].y) < 1E-07f && Math.Abs(boneImage.IkLinkLimitsMax[k].y) < 1E-07f)
							{
								boneImage.IkFixTypes[k] = BoneImage.AxisFixType.FixZ;
							}
						}
						this.BoneImages[boneImage.IkLinks[k]].IkLink = true;
					}
					boneImage.CcdAngleLimit = bone.IkInfoVal.CcdAngleLimit;
					boneImage.CcdIterateLimit = Math.Min(bone.IkInfoVal.CcdIterateLimit, 256);
					boneImage.IkTarget = bone.IkInfoVal.IkTargetIndex;
				}
				if (bone.PostPhysics)
				{
					this._postPhysicsBones.Add(j);
				}
				else
				{
					this._prePhysicsBones.Add(j);
				}
			}
			BoneImage.TransformOrder comparer = new BoneImage.TransformOrder(model);
			this._prePhysicsBones.Sort(comparer);
			this._postPhysicsBones.Sort(comparer);
			int num3 = model.Morphs.Length;
			this.MorphRates = new float[num3];
			for (int m = 0; m < num3; m++)
			{
				Morph morph = model.Morphs[m];
				this._morphNameMap[morph.Name] = m;
			}
			this.ResetPosing();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009228 File Offset: 0x00007428
		public void SetBonePose(int index, BonePose bonePose)
		{
			this.BoneImages[index].Translation = bonePose.Translation * Settings.s_MotionScale;
			this.BoneImages[index].Rotation = bonePose.Rotation;
			this.BoneImages[index].Only1Frame = bonePose.Only1Frame;
			string name = this.BoneImages[index].Name;
			if (!(this.BoneImages[index].Name == "左足首"))
			{
				this.BoneImages[index].Name = "右足首";
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000929F File Offset: 0x0000749F
		public void SetMorphPose(int index, MorphPose morphPose)
		{
			this.MorphRates[index] = morphPose.Weight;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000092B0 File Offset: 0x000074B0
		private void UpdateBoneAppendTransform(int index)
		{
			BoneImage boneImage = this.BoneImages[index];
			if (!boneImage.HasAppend)
			{
				return;
			}
			if (boneImage.AppendRotate)
			{
				boneImage.TotalRotation *= Quaternion.SlerpUnclamped(Quaternion.identity, this.BoneImages[boneImage.AppendParent].TotalRotation, boneImage.AppendRatio);
			}
			if (boneImage.AppendTranslate)
			{
				boneImage.TotalTranslation += boneImage.AppendRatio * this.BoneImages[boneImage.AppendParent].TotalTranslation;
			}
			this.UpdateLocalMatrix(boneImage);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009348 File Offset: 0x00007548
		private void UpdateLocalMatrix(BoneImage image)
		{
			image.LocalMatrix = MathUtil.QuaternionToMatrix4X4(image.TotalRotation);
			MathUtil.SetTransToMatrix4X4(image.TotalTranslation + image.LocalOffset, ref image.LocalMatrix);
			if (image.HasParent)
			{
				image.LocalMatrix = this.BoneImages[image.Parent].LocalMatrix * image.LocalMatrix;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000093B0 File Offset: 0x000075B0
		private void UpdateBoneSelfTransform(int index)
		{
			BoneImage boneImage = this.BoneImages[index];
			boneImage.TotalRotation = boneImage.MorphRotation * boneImage.Rotation;
			boneImage.TotalTranslation = boneImage.MorphTranslation + boneImage.Translation;
			if (boneImage.IkLink)
			{
				boneImage.PreIkRotation = boneImage.TotalRotation;
				boneImage.TotalRotation = boneImage.IkRotation * boneImage.TotalRotation;
			}
			this.UpdateLocalMatrix(boneImage);
			if (!boneImage.HasIk)
			{
				return;
			}
			if (boneImage.Only1Frame)
			{
				return;
			}
			int num = boneImage.IkLinks.Length;
			for (int i = 0; i < num; i++)
			{
				this.BoneImages[boneImage.IkLinks[i]].IkRotation = Quaternion.identity;
			}
			Vector3 transFromMatrix4X = MathUtil.GetTransFromMatrix4X4(boneImage.LocalMatrix);
			for (int j = 0; j < num; j++)
			{
				this.UpdateBoneSelfTransform(boneImage.IkTarget);
			}
			Vector3 b = MathUtil.GetTransFromMatrix4X4(this.BoneImages[boneImage.IkTarget].LocalMatrix);
			Vector3 vector = transFromMatrix4X - b;
			if (Vector3.Dot(vector, vector) < 1E-07f)
			{
				return;
			}
			int num2 = boneImage.CcdIterateLimit / 2;
			for (int k = 0; k < boneImage.CcdIterateLimit; k++)
			{
				for (int l = 0; l < num; l++)
				{
					if (boneImage.IkFixTypes[l] != BoneImage.AxisFixType.FixAll)
					{
						BoneImage boneImage2 = this.BoneImages[boneImage.IkLinks[l]];
						Vector3 transFromMatrix4X2 = MathUtil.GetTransFromMatrix4X4(boneImage2.LocalMatrix);
						Vector3 lhs = transFromMatrix4X2 - b;
						Vector3 rhs = transFromMatrix4X2 - transFromMatrix4X;
						lhs.Normalize();
						rhs.Normalize();
						Vector3 vector2 = Vector3.Cross(lhs, rhs);
						for (int m = 0; m < 3; m++)
						{
							if (Math.Abs(vector2[m]) < 1E-07f)
							{
								vector2[m] = 1E-07f;
							}
						}
						Matrix4x4 matrix4x = boneImage2.HasParent ? this.BoneImages[boneImage2.Parent].LocalMatrix : Matrix4x4.identity;
						if (boneImage.IkLinkLimited[l] && boneImage.IkFixTypes[l] != BoneImage.AxisFixType.FixNone && k < num2)
						{
							switch (boneImage.IkFixTypes[l])
							{
							case BoneImage.AxisFixType.FixX:
								vector2.x = Poser.Nabs(Vector3.Dot(vector2, MathUtil.Matrix4x4ColDowngrade(matrix4x, 0)));
								vector2.y = (vector2.z = 0f);
								break;
							case BoneImage.AxisFixType.FixY:
								vector2.y = Poser.Nabs(Vector3.Dot(vector2, MathUtil.Matrix4x4ColDowngrade(matrix4x, 1)));
								vector2.x = (vector2.z = 0f);
								break;
							case BoneImage.AxisFixType.FixZ:
								vector2.z = Poser.Nabs(Vector3.Dot(vector2, MathUtil.Matrix4x4ColDowngrade(matrix4x, 2)));
								vector2.x = (vector2.y = 0f);
								break;
							}
						}
						else
						{
							vector2 = Matrix4x4.Transpose(matrix4x).MultiplyVector(vector2);
							vector2.Normalize();
						}
						float num3 = Mathf.Min(Mathf.Acos(Mathf.Clamp(Vector3.Dot(lhs, rhs), -1f, 1f)), boneImage.CcdAngleLimit * (float)(l + 1));
						boneImage2.IkRotation = Quaternion.AngleAxis((float)((double)num3 / 3.1415926535897931 * 180.0), vector2) * boneImage2.IkRotation;
						if (boneImage.IkLinkLimited[l])
						{
							Quaternion quaternion = boneImage2.IkRotation * boneImage2.PreIkRotation;
							switch (boneImage.IkTransformOrders[l])
							{
							case BoneImage.AxisTransformOrder.OrderZxy:
								quaternion = MathUtil.ZxyToQuaternion(Poser.LimitEularAngle(MathUtil.QuaternionToZxy(quaternion), boneImage.IkLinkLimitsMin[l], boneImage.IkLinkLimitsMax[l], k < num2));
								break;
							case BoneImage.AxisTransformOrder.OrderXyz:
								quaternion = MathUtil.XyzToQuaternion(Poser.LimitEularAngle(MathUtil.QuaternionToXyz(quaternion), boneImage.IkLinkLimitsMin[l], boneImage.IkLinkLimitsMax[l], k < num2));
								break;
							case BoneImage.AxisTransformOrder.OrderYzx:
								quaternion = MathUtil.YzxToQuaternion(Poser.LimitEularAngle(MathUtil.QuaternionToYzx(quaternion), boneImage.IkLinkLimitsMin[l], boneImage.IkLinkLimitsMax[l], k < num2));
								break;
							default:
								throw new ArgumentOutOfRangeException();
							}
							boneImage2.IkRotation = quaternion * Quaternion.Inverse(boneImage2.PreIkRotation);
						}
						for (int n = 0; n <= l; n++)
						{
							BoneImage boneImage3 = this.BoneImages[boneImage.IkLinks[l - n]];
							boneImage3.TotalRotation = boneImage3.IkRotation * boneImage3.PreIkRotation;
							this.UpdateLocalMatrix(boneImage3);
						}
						this.UpdateBoneSelfTransform(boneImage.IkTarget);
						b = MathUtil.Matrix4x4ColDowngrade(this.BoneImages[boneImage.IkTarget].LocalMatrix, 3);
					}
				}
				Vector3 vector3 = transFromMatrix4X - b;
				if (Vector3.Dot(vector3, vector3) < 1E-07f)
				{
					return;
				}
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000989A File Offset: 0x00007A9A
		private static float Nabs(float x)
		{
			if (x > 0f)
			{
				return 1f;
			}
			return -1f;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000098B0 File Offset: 0x00007AB0
		private static Vector3 LimitEularAngle(Vector3 eular, Vector3 eularMin, Vector3 eularMax, bool ikt)
		{
			Vector3 result = eular;
			for (int i = 0; i < 3; i++)
			{
				if (result[i] < eularMin[i])
				{
					float num = 2f * eularMin[i] - result[i];
					if (num <= eularMax[i] && ikt)
					{
						result[i] = num;
					}
					else
					{
						result[i] = eularMin[i];
					}
				}
				if (result[i] > eularMax[i])
				{
					float num2 = 2f * eularMax[i] - result[i];
					if (num2 >= eularMin[i] && ikt)
					{
						result[i] = num2;
					}
					else
					{
						result[i] = eularMax[i];
					}
				}
			}
			return result;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009980 File Offset: 0x00007B80
		public void ResetPosing()
		{
			for (int i = 0; i < this.MorphRates.Length; i++)
			{
				this.MorphRates[i] = 0f;
			}
			foreach (BoneImage boneImage in this.BoneImages)
			{
				boneImage.Rotation = new Quaternion(0f, 0f, 0f, 1f);
				boneImage.Translation = Vector4.zero;
			}
			this.PrePhysicsPosing(true);
			this.PostPhysicsPosing();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009A00 File Offset: 0x00007C00
		public void PrePhysicsPosing(bool calculateMorph = true)
		{
			foreach (BoneImage boneImage in this.BoneImages)
			{
				boneImage.MorphTranslation = Vector3.zero;
				boneImage.MorphRotation = Quaternion.identity;
				boneImage.LocalMatrix = Matrix4x4.identity;
				boneImage.PreIkRotation = Quaternion.identity;
				boneImage.IkRotation = Quaternion.identity;
				boneImage.TotalRotation = Quaternion.identity;
				boneImage.TotalTranslation = Vector3.zero;
			}
			this.UpdateBoneTransform(this._prePhysicsBones);
			this.UpdateBoneSkinningMatrix(this._prePhysicsBones);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009A8C File Offset: 0x00007C8C
		private void UpdateBoneSkinningMatrix(List<int> indexList)
		{
			foreach (int num in indexList)
			{
				BoneImage boneImage = this.BoneImages[num];
				boneImage.SkinningMatrix = boneImage.LocalMatrix * boneImage.GlobalOffsetMatrix;
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009AF4 File Offset: 0x00007CF4
		private void UpdateBoneTransform(List<int> indexList)
		{
			foreach (int index in indexList)
			{
				this.UpdateBoneSelfTransform(index);
			}
			foreach (int index2 in indexList)
			{
				this.UpdateBoneAppendTransform(index2);
			}
			foreach (int num in indexList)
			{
				this.UpdateLocalMatrix(this.BoneImages[num]);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009BC4 File Offset: 0x00007DC4
		public void PostPhysicsPosing()
		{
			this.UpdateBoneTransform(this._postPhysicsBones);
			this.UpdateBoneSkinningMatrix(this._postPhysicsBones);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00009BDE File Offset: 0x00007DDE
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00009BE6 File Offset: 0x00007DE6
		public BoneImage[] BoneImages { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00009BEF File Offset: 0x00007DEF
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00009BF7 File Offset: 0x00007DF7
		public float[] MorphRates { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00009C00 File Offset: 0x00007E00
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00009C08 File Offset: 0x00007E08
		public MmdModel Model { get; set; }

		// Token: 0x04000072 RID: 114
		private readonly Dictionary<string, int> _boneNameMap = new Dictionary<string, int>();

		// Token: 0x04000073 RID: 115
		private readonly Dictionary<string, int> _morphNameMap = new Dictionary<string, int>();

		// Token: 0x04000074 RID: 116
		private readonly List<int> _prePhysicsBones = new List<int>();

		// Token: 0x04000075 RID: 117
		private readonly List<int> _postPhysicsBones = new List<int>();
	}
}
