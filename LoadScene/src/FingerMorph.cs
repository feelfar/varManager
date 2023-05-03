using System;
using System.Collections.Generic;
using UnityEngine;

namespace mmd2timeline
{
	// Token: 0x0200002F RID: 47
	internal class FingerMorph
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000F293 File Offset: 0x0000D493
		public DAZMorphFormula CreateDAZMorphFormula(DAZMorphFormulaTargetType targetType, string target, float multiplier)
		{
			return new DAZMorphFormula
			{
				targetType = targetType,
				target = target,
				multiplier = multiplier
			};
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public void InitRight()
		{
			FingerMorph.MorphSetting morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Right Thumb Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationY, "rThumb1", -45f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationY, "rThumb2", -40f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationY, "rThumb3", -68f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Right Ring Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rRing1", 61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rRing2", 90f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rRing3", 65f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Right Pinky Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rPinky1", 61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rPinky2", 90f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rPinky3", 75f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Right Mid Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rMid1", 61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rMid2", 90f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rMid3", 57f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Right Index Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rIndex1", 61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rIndex2", 100f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "rIndex3", 62f)
			};
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000F48C File Offset: 0x0000D68C
		public void InitLeft()
		{
			FingerMorph.MorphSetting morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Left Thumb Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationY, "lThumb1", 45f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationY, "lThumb2", 40f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationY, "lThumb3", 68f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Left Ring Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lRing1", -61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lRing2", -90f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lRing3", -65f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Left Pinky Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lPinky1", -61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lPinky2", -90f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lPinky3", -75f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Left Mid Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lMid1", -61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lMid2", -90f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lMid3", -57f)
			};
			morphSetting = new FingerMorph.MorphSetting();
			morphSetting.Name = "Left Index Finger Bend";
			morphSetting.Formulas = new DAZMorphFormula[]
			{
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lIndex1", -61f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lIndex2", -100f),
				this.CreateDAZMorphFormula(DAZMorphFormulaTargetType.RotationZ, "lIndex3", -62f)
			};
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000F668 File Offset: 0x0000D868
		public static Vector3 ConvertToMorphRotation(DAZBone bone, ConfigurableJoint configurableJoint)
		{
			Vector3 vector = Quaternion2Angles.GetAngles(configurableJoint.targetRotation, bone.jointDriveTargetRotationOrder) * 57.29578f;
			Vector3 result = default(Vector3);
			if (configurableJoint.axis.x == 1f)
			{
				result.x = -vector.x;
				if (configurableJoint.secondaryAxis.y == 1f)
				{
					result.y = vector.y;
					result.z = vector.z;
				}
				else
				{
					result.z = vector.y;
					result.y = vector.z;
				}
			}
			else if (configurableJoint.axis.y == 1f)
			{
				result.y = vector.x;
				if (configurableJoint.secondaryAxis.x == 1f)
				{
					result.x = -vector.y;
					result.z = vector.z;
				}
				else
				{
					result.z = vector.y;
					result.x = -vector.z;
				}
			}
			else
			{
				result.z = vector.x;
				if (configurableJoint.secondaryAxis.x == 1f)
				{
					result.x = -vector.y;
					result.y = vector.z;
				}
				else
				{
					result.y = vector.y;
					result.x = -vector.z;
				}
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		public static Quaternion EulerToQuaternion(Vector3 r, Quaternion2Angles.RotationOrder ro)
		{
			Quaternion quaternion = Quaternion.Euler(r.x, 0f, 0f);
			Quaternion quaternion2 = Quaternion.Euler(0f, r.y, 0f);
			Quaternion quaternion3 = Quaternion.Euler(0f, 0f, r.z);
			Quaternion result = quaternion;
			switch (ro)
			{
			case Quaternion2Angles.RotationOrder.XYZ:
				result = quaternion * quaternion2 * quaternion3;
				break;
			case Quaternion2Angles.RotationOrder.XZY:
				result = quaternion * quaternion3 * quaternion2;
				break;
			case Quaternion2Angles.RotationOrder.YXZ:
				result = quaternion2 * quaternion * quaternion3;
				break;
			case Quaternion2Angles.RotationOrder.YZX:
				result = quaternion2 * quaternion3 * quaternion;
				break;
			case Quaternion2Angles.RotationOrder.ZXY:
				result = quaternion3 * quaternion * quaternion2;
				break;
			case Quaternion2Angles.RotationOrder.ZYX:
				result = quaternion3 * quaternion2 * quaternion;
				break;
			}
			return result;
		}

		// Token: 0x0400011D RID: 285
		public static string[] StorableNames = new string[]
		{
			"LeftHandFingerControl",
			"RightHandFingerControl"
		};

		// Token: 0x0400011E RID: 286
		public static HashSet<string> setting = new HashSet<string>
		{
			"indexProximalBend",
			"indexProximalSpread",
			"indexProximalTwist",
			"indexMiddleBend",
			"indexDistalBend",
			"middleProximalBend",
			"middleProximalSpread",
			"middleProximalTwist",
			"middleMiddleBend",
			"middleDistalBend",
			"ringProximalBend",
			"ringProximalSpread",
			"ringProximalTwist",
			"ringMiddleBend",
			"ringDistalBend",
			"pinkyProximalBend",
			"pinkyProximalSpread",
			"pinkyProximalTwist",
			"pinkyMiddleBend",
			"pinkyDistalBend",
			"thumbProximalBend",
			"thumbProximalSpread",
			"thumbProximalTwist",
			"thumbMiddleBend",
			"thumbDistalBend"
		};

		// Token: 0x0200006B RID: 107
		private class MorphSetting
		{
			// Token: 0x0400020B RID: 523
			public string Name;

			// Token: 0x0400020C RID: 524
			public DAZMorphFormula[] Formulas;
		}
	}
}
