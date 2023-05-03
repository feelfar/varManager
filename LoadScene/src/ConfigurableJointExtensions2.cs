using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class ConfigurableJointExtensions2
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002113 File Offset: 0x00000313
	public static Quaternion GetJointLocalRotationInConnectedBodySpace(ConfigurableJoint joint)
	{
		return Quaternion.Inverse(joint.connectedBody.transform.rotation) * joint.transform.rotation;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000213C File Offset: 0x0000033C
	public static Quaternion GetJointTargetRotationInWorldSpace(ConfigurableJoint joint, Quaternion initLocalRot)
	{
		Quaternion coordRotation = Quaternion.LookRotation(Vector3.Cross(joint.axis, joint.secondaryAxis), joint.secondaryAxis);
		return joint.connectedBody.transform.rotation * initLocalRot * Quaternion.Inverse(SwitchQuaternionCoordinateSystem(joint.targetRotation, coordRotation));
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002194 File Offset: 0x00000394
	public static Quaternion GetJointRotationInJointAisSpace(ConfigurableJoint joint, Quaternion initLocalRot)
	{
		Quaternion rotation = Quaternion.LookRotation(Vector3.Cross(joint.axis, joint.secondaryAxis), joint.secondaryAxis);
		return SwitchQuaternionCoordinateSystem(Quaternion.Inverse(Quaternion.Inverse(initLocalRot) * Quaternion.Inverse(joint.connectedBody.transform.rotation) * joint.transform.rotation), Quaternion.Inverse(rotation));
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000021FE File Offset: 0x000003FE
	public static Quaternion SwitchQuaternionCoordinateSystem(Quaternion q, Quaternion coordRotation)
	{
		return coordRotation * q * Quaternion.Inverse(coordRotation);
	}
}
