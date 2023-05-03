using System;
using UnityEngine;

namespace LibMMD.Model
{
	// Token: 0x02000022 RID: 34
	public class MmdRigidBody
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000A15E File Offset: 0x0000835E
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000A166 File Offset: 0x00008366
		public string Name { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000A16F File Offset: 0x0000836F
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000A177 File Offset: 0x00008377
		public string NameEn { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000A180 File Offset: 0x00008380
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000A188 File Offset: 0x00008388
		public int AssociatedBoneIndex { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000A191 File Offset: 0x00008391
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000A199 File Offset: 0x00008399
		public int CollisionGroup { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000A1A2 File Offset: 0x000083A2
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000A1AA File Offset: 0x000083AA
		public ushort CollisionMask { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000A1B3 File Offset: 0x000083B3
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000A1BB File Offset: 0x000083BB
		public MmdRigidBody.RigidBodyShape Shape { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000A1C4 File Offset: 0x000083C4
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000A1CC File Offset: 0x000083CC
		public Vector3 Dimemsions { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000A1D5 File Offset: 0x000083D5
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000A1DD File Offset: 0x000083DD
		public Vector3 Position { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000A1E6 File Offset: 0x000083E6
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000A1EE File Offset: 0x000083EE
		public Vector3 Rotation { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000A1F7 File Offset: 0x000083F7
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000A1FF File Offset: 0x000083FF
		public float Mass { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000A208 File Offset: 0x00008408
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000A210 File Offset: 0x00008410
		public float TranslateDamp { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000A219 File Offset: 0x00008419
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000A221 File Offset: 0x00008421
		public float RotateDamp { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000A22A File Offset: 0x0000842A
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000A232 File Offset: 0x00008432
		public float Restitution { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000A23B File Offset: 0x0000843B
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000A243 File Offset: 0x00008443
		public float Friction { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000A24C File Offset: 0x0000844C
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000A254 File Offset: 0x00008454
		public MmdRigidBody.RigidBodyType Type { get; set; }

		// Token: 0x02000056 RID: 86
		public enum RigidBodyShape : byte
		{
			// Token: 0x040001B6 RID: 438
			RigidShapeSphere,
			// Token: 0x040001B7 RID: 439
			RigidShapeBox,
			// Token: 0x040001B8 RID: 440
			RigidShapeCapsule
		}

		// Token: 0x02000057 RID: 87
		public enum RigidBodyType : byte
		{
			// Token: 0x040001BA RID: 442
			RigidTypeKinematic,
			// Token: 0x040001BB RID: 443
			RigidTypePhysics,
			// Token: 0x040001BC RID: 444
			RigidTypePhysicsStrict,
			// Token: 0x040001BD RID: 445
			RigidTypePhysicsGhost
		}
	}
}
