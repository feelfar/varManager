using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public static class ConfigurableJointExtensions
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static void SetTargetRotationLocal(this ConfigurableJoint joint, Quaternion targetLocalRotation, Quaternion startLocalRotation)
	{
		if (joint.configuredInWorldSpace)
		{
			Debug.LogError("SetTargetRotationLocal should not be used with joints that are configured in world space. For world space joints, use SetTargetRotation.", joint);
		}
		SetTargetRotationInternal(joint, targetLocalRotation, startLocalRotation, Space.Self);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000206E File Offset: 0x0000026E
	public static void SetTargetRotation(this ConfigurableJoint joint, Quaternion targetWorldRotation, Quaternion startWorldRotation)
	{
		if (!joint.configuredInWorldSpace)
		{
			Debug.LogError("SetTargetRotation must be used with joints that are configured in world space. For local space joints, use SetTargetRotationLocal.", joint);
		}
		SetTargetRotationInternal(joint, targetWorldRotation, startWorldRotation, Space.World);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
	private static void SetTargetRotationInternal(ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation, Space space)
	{
		Vector3 axis = joint.axis;
		Vector3 normalized = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
		Vector3 normalized2 = Vector3.Cross(normalized, axis).normalized;
		Quaternion quaternion = Quaternion.LookRotation(normalized, normalized2);
		Quaternion quaternion2 = Quaternion.Inverse(quaternion);
		if (space == Space.World)
		{
			quaternion2 *= startRotation * Quaternion.Inverse(targetRotation);
		}
		else
		{
			quaternion2 *= Quaternion.Inverse(targetRotation) * startRotation;
		}
		quaternion2 *= quaternion;
		joint.targetRotation = quaternion2;
	}
}
