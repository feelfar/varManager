using System;
using MeshVR.Hands;
using UnityEngine;

namespace mmd2timeline
{
	// Token: 0x02000030 RID: 48
	internal static class FingerOutputExt
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000FA04 File Offset: 0x0000DC04
		public static void ConvertRotation(this FingerOutput fingerOutput, DAZBone bone, ConfigurableJoint joint)
		{
			Vector3 vector = Quaternion2Angles.GetAngles(joint.targetRotation, bone.jointDriveTargetRotationOrder) * 57.29578f;
			if (fingerOutput.twistEnabled)
			{
				float num = 0f;
				switch (fingerOutput.twistAxis)
				{
				case Finger.Axis.X:
					num = vector.x;
					break;
				case Finger.Axis.NegX:
					num = -vector.x;
					break;
				case Finger.Axis.Y:
					num = vector.y;
					break;
				case Finger.Axis.NegY:
					num = -vector.y;
					break;
				case Finger.Axis.Z:
					num = vector.z;
					break;
				case Finger.Axis.NegZ:
					num = -vector.z;
					break;
				}
				fingerOutput.currentTwist = num - fingerOutput.twistOffset;
			}
			if (fingerOutput.spreadEnabled)
			{
				float num2 = 0f;
				switch (fingerOutput.spreadAxis)
				{
				case Finger.Axis.X:
					num2 = vector.x;
					break;
				case Finger.Axis.NegX:
					num2 = -vector.x;
					break;
				case Finger.Axis.Y:
					num2 = vector.y;
					break;
				case Finger.Axis.NegY:
					num2 = -vector.y;
					break;
				case Finger.Axis.Z:
					num2 = vector.z;
					break;
				case Finger.Axis.NegZ:
					num2 = -vector.z;
					break;
				}
				fingerOutput.currentSpread = num2 - fingerOutput.spreadOffset;
			}
			if (fingerOutput.bendEnabled)
			{
				float num3 = fingerOutput.currentBend + fingerOutput.bendOffset;
				switch (fingerOutput.bendAxis)
				{
				case Finger.Axis.X:
					num3 = vector.x;
					break;
				case Finger.Axis.NegX:
					num3 = -vector.x;
					break;
				case Finger.Axis.Y:
					num3 = vector.y;
					break;
				case Finger.Axis.NegY:
					num3 = -vector.y;
					break;
				case Finger.Axis.Z:
					num3 = vector.z;
					break;
				case Finger.Axis.NegZ:
					num3 = -vector.z;
					break;
				}
				fingerOutput.currentBend = num3 - fingerOutput.bendOffset;
			}
		}
	}
}
