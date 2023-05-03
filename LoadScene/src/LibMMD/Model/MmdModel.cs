using System;

namespace LibMMD.Model
{
	// Token: 0x02000021 RID: 33
	public class MmdModel
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00009E90 File Offset: 0x00008090
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00009E98 File Offset: 0x00008098
		public string Name { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00009EA1 File Offset: 0x000080A1
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00009EA9 File Offset: 0x000080A9
		public string NameEn { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00009EB2 File Offset: 0x000080B2
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00009EBA File Offset: 0x000080BA
		public string Description { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00009EC3 File Offset: 0x000080C3
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00009ECB File Offset: 0x000080CB
		public string DescriptionEn { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00009ED4 File Offset: 0x000080D4
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00009EDC File Offset: 0x000080DC
		public Vertex[] Vertices { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00009EE5 File Offset: 0x000080E5
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00009EED File Offset: 0x000080ED
		public int ExtraUvNumber { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00009EF6 File Offset: 0x000080F6
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00009EFE File Offset: 0x000080FE
		public int[] TriangleIndexes { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009F07 File Offset: 0x00008107
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00009F0F File Offset: 0x0000810F
		public Part[] Parts { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00009F18 File Offset: 0x00008118
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00009F20 File Offset: 0x00008120
		public Bone[] Bones { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00009F29 File Offset: 0x00008129
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00009F31 File Offset: 0x00008131
		public Morph[] Morphs { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00009F3A File Offset: 0x0000813A
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00009F42 File Offset: 0x00008142
		public MmdRigidBody[] Rigidbodies { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009F4B File Offset: 0x0000814B
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00009F53 File Offset: 0x00008153
		public Constraint[] Constraints { get; set; }

		// Token: 0x06000181 RID: 385 RVA: 0x00009F5C File Offset: 0x0000815C
		public void Normalize()
		{
			foreach (Vertex vertex in this.Vertices)
			{
				SkinningOperator.SkinningType type = vertex.SkinningOperator.Type;
				if (type != SkinningOperator.SkinningType.SkinningBdef2)
				{
					if (type == SkinningOperator.SkinningType.SkinningSdef)
					{
						SkinningOperator.Sdef sdef = (SkinningOperator.Sdef)vertex.SkinningOperator.Param;
						int num = sdef.BoneId[0];
						int num2 = sdef.BoneId[1];
						float boneWeight = sdef.BoneWeight;
						if (this.Bones[num].ParentIndex != num2 && this.Bones[num2].ParentIndex != num)
						{
							if (Math.Abs(boneWeight) < 1E-06f)
							{
								SkinningOperator.Bdef1 param = new SkinningOperator.Bdef1
								{
									BoneId = num2
								};
								vertex.SkinningOperator.Param = param;
								vertex.SkinningOperator.Type = SkinningOperator.SkinningType.SkinningBdef1;
							}
							else if (Math.Abs(boneWeight - 1f) < 1E-05f)
							{
								SkinningOperator.Bdef1 param2 = new SkinningOperator.Bdef1
								{
									BoneId = num
								};
								vertex.SkinningOperator.Param = param2;
								vertex.SkinningOperator.Type = SkinningOperator.SkinningType.SkinningBdef1;
							}
							else
							{
								SkinningOperator.Bdef2 bdef = new SkinningOperator.Bdef2();
								bdef.BoneId[0] = num;
								bdef.BoneId[1] = num2;
								bdef.BoneWeight = boneWeight;
								vertex.SkinningOperator.Param = bdef;
								vertex.SkinningOperator.Type = SkinningOperator.SkinningType.SkinningBdef2;
							}
						}
					}
				}
				else
				{
					SkinningOperator.Bdef2 bdef2 = (SkinningOperator.Bdef2)vertex.SkinningOperator.Param;
					float boneWeight2 = bdef2.BoneWeight;
					if (Math.Abs(boneWeight2) < 1E-06f)
					{
						SkinningOperator.Bdef1 param3 = new SkinningOperator.Bdef1
						{
							BoneId = bdef2.BoneId[1]
						};
						vertex.SkinningOperator.Param = param3;
						vertex.SkinningOperator.Type = SkinningOperator.SkinningType.SkinningBdef1;
					}
					else if (Math.Abs(boneWeight2 - 1f) < 1E-05f)
					{
						SkinningOperator.Bdef1 param4 = new SkinningOperator.Bdef1
						{
							BoneId = bdef2.BoneId[0]
						};
						vertex.SkinningOperator.Param = param4;
						vertex.SkinningOperator.Type = SkinningOperator.SkinningType.SkinningBdef1;
					}
				}
			}
		}
	}
}
