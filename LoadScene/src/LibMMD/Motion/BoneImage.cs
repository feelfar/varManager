using System;
using System.Collections.Generic;
using LibMMD.Model;
using UnityEngine;

namespace LibMMD.Motion
{
	// Token: 0x02000012 RID: 18
	public class BoneImage
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00006E28 File Offset: 0x00005028
		public BoneImage()
		{
			this.Rotation = Quaternion.identity;
			this.Translation = Vector3.zero;
			this.MorphRotation = Quaternion.identity;
			this.MorphTranslation = Vector3.zero;
			this.GlobalOffsetMatrix = Matrix4x4.identity;
			this.GlobalOffsetMatrixInv = Matrix4x4.identity;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00006E7D File Offset: 0x0000507D
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00006E85 File Offset: 0x00005085
		public Quaternion Rotation { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00006E8E File Offset: 0x0000508E
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00006E96 File Offset: 0x00005096
		public Vector3 Translation { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00006E9F File Offset: 0x0000509F
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00006EA7 File Offset: 0x000050A7
		public Quaternion MorphRotation { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00006EB0 File Offset: 0x000050B0
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00006EB8 File Offset: 0x000050B8
		public Vector3 MorphTranslation { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00006EC1 File Offset: 0x000050C1
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00006EC9 File Offset: 0x000050C9
		public bool HasParent { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00006ED2 File Offset: 0x000050D2
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00006EDA File Offset: 0x000050DA
		public int Parent { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00006EE3 File Offset: 0x000050E3
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00006EEB File Offset: 0x000050EB
		public bool HasAppend { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00006EF4 File Offset: 0x000050F4
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00006EFC File Offset: 0x000050FC
		public bool AppendRotate { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00006F05 File Offset: 0x00005105
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00006F0D File Offset: 0x0000510D
		public bool AppendTranslate { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00006F16 File Offset: 0x00005116
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00006F1E File Offset: 0x0000511E
		public int AppendParent { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00006F27 File Offset: 0x00005127
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00006F2F File Offset: 0x0000512F
		public float AppendRatio { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00006F38 File Offset: 0x00005138
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00006F40 File Offset: 0x00005140
		public bool HasIk { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00006F49 File Offset: 0x00005149
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00006F51 File Offset: 0x00005151
		public bool IkLink { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00006F5A File Offset: 0x0000515A
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00006F62 File Offset: 0x00005162
		public float CcdAngleLimit { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00006F6B File Offset: 0x0000516B
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00006F73 File Offset: 0x00005173
		public int CcdIterateLimit { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00006F7C File Offset: 0x0000517C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00006F84 File Offset: 0x00005184
		public int[] IkLinks { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00006F8D File Offset: 0x0000518D
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00006F95 File Offset: 0x00005195
		public BoneImage.AxisFixType[] IkFixTypes { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00006F9E File Offset: 0x0000519E
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00006FA6 File Offset: 0x000051A6
		public BoneImage.AxisTransformOrder[] IkTransformOrders { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00006FAF File Offset: 0x000051AF
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00006FB7 File Offset: 0x000051B7
		public bool[] IkLinkLimited { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00006FC0 File Offset: 0x000051C0
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00006FC8 File Offset: 0x000051C8
		public Vector3[] IkLinkLimitsMin { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00006FD1 File Offset: 0x000051D1
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00006FD9 File Offset: 0x000051D9
		public Vector3[] IkLinkLimitsMax { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00006FE2 File Offset: 0x000051E2
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00006FEA File Offset: 0x000051EA
		public int IkTarget { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00006FF3 File Offset: 0x000051F3
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00006FFB File Offset: 0x000051FB
		public Quaternion PreIkRotation { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00007004 File Offset: 0x00005204
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000700C File Offset: 0x0000520C
		public Quaternion IkRotation { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00007015 File Offset: 0x00005215
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000701D File Offset: 0x0000521D
		public Quaternion TotalRotation { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00007026 File Offset: 0x00005226
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000702E File Offset: 0x0000522E
		public Vector3 TotalTranslation { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00007037 File Offset: 0x00005237
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000703F File Offset: 0x0000523F
		public Vector3 LocalOffset { get; set; }

		// Token: 0x04000027 RID: 39
		public string Name;

		// Token: 0x0400002A RID: 42
		public bool Only1Frame;

		// Token: 0x04000044 RID: 68
		public Matrix4x4 GlobalOffsetMatrix;

		// Token: 0x04000045 RID: 69
		public Matrix4x4 GlobalOffsetMatrixInv;

		// Token: 0x04000046 RID: 70
		public Matrix4x4 LocalMatrix;

		// Token: 0x04000047 RID: 71
		public Matrix4x4 SkinningMatrix;

		// Token: 0x0200004A RID: 74
		public enum AxisFixType
		{
			// Token: 0x04000195 RID: 405
			FixNone,
			// Token: 0x04000196 RID: 406
			FixX,
			// Token: 0x04000197 RID: 407
			FixY,
			// Token: 0x04000198 RID: 408
			FixZ,
			// Token: 0x04000199 RID: 409
			FixAll
		}

		// Token: 0x0200004B RID: 75
		public enum AxisTransformOrder
		{
			// Token: 0x0400019B RID: 411
			OrderZxy,
			// Token: 0x0400019C RID: 412
			OrderXyz,
			// Token: 0x0400019D RID: 413
			OrderYzx
		}

		// Token: 0x0200004C RID: 76
		public class TransformOrder : IComparer<int>
		{
			// Token: 0x060002D1 RID: 721 RVA: 0x0001081C File Offset: 0x0000EA1C
			public TransformOrder(MmdModel model)
			{
				this._model = model;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0001082C File Offset: 0x0000EA2C
			public int Compare(int a, int b)
			{
				if (this._model.Bones[a].TransformLevel == this._model.Bones[b].TransformLevel)
				{
					return a.CompareTo(b);
				}
				return this._model.Bones[a].TransformLevel.CompareTo(this._model.Bones[b].TransformLevel);
			}

			// Token: 0x0400019E RID: 414
			private readonly MmdModel _model;
		}
	}
}
