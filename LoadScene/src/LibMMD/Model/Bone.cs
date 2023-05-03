using System;
using UnityEngine;

namespace LibMMD.Model
{
	// Token: 0x0200001F RID: 31
	public class Bone
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00009C11 File Offset: 0x00007E11
		public Bone()
		{
			this.ChildBoneVal = new Bone.ChildBone();
			this.AppendBoneVal = new Bone.AppendBone();
			this.LocalAxisVal = new Bone.LocalAxis();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00009C3A File Offset: 0x00007E3A
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00009C42 File Offset: 0x00007E42
		public string Name { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00009C4B File Offset: 0x00007E4B
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00009C53 File Offset: 0x00007E53
		public string NameEn { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00009C5C File Offset: 0x00007E5C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00009C64 File Offset: 0x00007E64
		public Vector3 InitPosition { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00009C6D File Offset: 0x00007E6D
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00009C75 File Offset: 0x00007E75
		public Vector3 Position { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009C7E File Offset: 0x00007E7E
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00009C86 File Offset: 0x00007E86
		public int ParentIndex { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00009C8F File Offset: 0x00007E8F
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00009C97 File Offset: 0x00007E97
		public int TransformLevel { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00009CA0 File Offset: 0x00007EA0
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public bool Rotatable { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00009CB1 File Offset: 0x00007EB1
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00009CB9 File Offset: 0x00007EB9
		public bool Movable { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00009CC2 File Offset: 0x00007EC2
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00009CCA File Offset: 0x00007ECA
		public bool Visible { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009CD3 File Offset: 0x00007ED3
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00009CDB File Offset: 0x00007EDB
		public bool Controllable { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00009CE4 File Offset: 0x00007EE4
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00009CEC File Offset: 0x00007EEC
		public bool HasIk { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00009CF5 File Offset: 0x00007EF5
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00009CFD File Offset: 0x00007EFD
		public bool AppendRotate { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00009D06 File Offset: 0x00007F06
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00009D0E File Offset: 0x00007F0E
		public bool AppendTranslate { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00009D17 File Offset: 0x00007F17
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00009D1F File Offset: 0x00007F1F
		public bool RotAxisFixed { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00009D28 File Offset: 0x00007F28
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00009D30 File Offset: 0x00007F30
		public bool UseLocalAxis { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00009D39 File Offset: 0x00007F39
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00009D41 File Offset: 0x00007F41
		public bool PostPhysics { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00009D4A File Offset: 0x00007F4A
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00009D52 File Offset: 0x00007F52
		public bool ReceiveTransform { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00009D5B File Offset: 0x00007F5B
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00009D63 File Offset: 0x00007F63
		public Bone.ChildBone ChildBoneVal { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00009D6C File Offset: 0x00007F6C
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00009D74 File Offset: 0x00007F74
		public Bone.AppendBone AppendBoneVal { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00009D7D File Offset: 0x00007F7D
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00009D85 File Offset: 0x00007F85
		public Vector3 RotAxis { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00009D8E File Offset: 0x00007F8E
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00009D96 File Offset: 0x00007F96
		public Bone.LocalAxis LocalAxisVal { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00009D9F File Offset: 0x00007F9F
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00009DA7 File Offset: 0x00007FA7
		public int ExportKey { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00009DB0 File Offset: 0x00007FB0
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public Bone.IkInfo IkInfoVal { get; set; }

		// Token: 0x02000051 RID: 81
		public class IkLink
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060002E0 RID: 736 RVA: 0x0001099D File Offset: 0x0000EB9D
			// (set) Token: 0x060002E1 RID: 737 RVA: 0x000109A5 File Offset: 0x0000EBA5
			public int LinkIndex { get; set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060002E2 RID: 738 RVA: 0x000109AE File Offset: 0x0000EBAE
			// (set) Token: 0x060002E3 RID: 739 RVA: 0x000109B6 File Offset: 0x0000EBB6
			public bool HasLimit { get; set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060002E4 RID: 740 RVA: 0x000109BF File Offset: 0x0000EBBF
			// (set) Token: 0x060002E5 RID: 741 RVA: 0x000109C7 File Offset: 0x0000EBC7
			public Vector3 LoLimit { get; set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x000109D0 File Offset: 0x0000EBD0
			// (set) Token: 0x060002E7 RID: 743 RVA: 0x000109D8 File Offset: 0x0000EBD8
			public Vector3 HiLimit { get; set; }

			// Token: 0x060002E8 RID: 744 RVA: 0x000109E1 File Offset: 0x0000EBE1
			public static Bone.IkLink CopyOf(Bone.IkLink ikLink)
			{
				return new Bone.IkLink
				{
					LinkIndex = ikLink.LinkIndex,
					HasLimit = ikLink.HasLimit,
					LoLimit = ikLink.LoLimit,
					HiLimit = ikLink.HiLimit
				};
			}
		}

		// Token: 0x02000052 RID: 82
		public class ChildBone
		{
			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060002EA RID: 746 RVA: 0x00010A20 File Offset: 0x0000EC20
			// (set) Token: 0x060002EB RID: 747 RVA: 0x00010A28 File Offset: 0x0000EC28
			public bool ChildUseId { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060002EC RID: 748 RVA: 0x00010A31 File Offset: 0x0000EC31
			// (set) Token: 0x060002ED RID: 749 RVA: 0x00010A39 File Offset: 0x0000EC39
			public Vector3 Offset { get; set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002EE RID: 750 RVA: 0x00010A42 File Offset: 0x0000EC42
			// (set) Token: 0x060002EF RID: 751 RVA: 0x00010A4A File Offset: 0x0000EC4A
			public int Index { get; set; }

			// Token: 0x060002F0 RID: 752 RVA: 0x00010A53 File Offset: 0x0000EC53
			public static Bone.ChildBone CopyOf(Bone.ChildBone childBone)
			{
				return new Bone.ChildBone
				{
					ChildUseId = childBone.ChildUseId,
					Offset = childBone.Offset,
					Index = childBone.Index
				};
			}
		}

		// Token: 0x02000053 RID: 83
		public class AppendBone
		{
			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002F2 RID: 754 RVA: 0x00010A86 File Offset: 0x0000EC86
			// (set) Token: 0x060002F3 RID: 755 RVA: 0x00010A8E File Offset: 0x0000EC8E
			public int Index { get; set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060002F4 RID: 756 RVA: 0x00010A97 File Offset: 0x0000EC97
			// (set) Token: 0x060002F5 RID: 757 RVA: 0x00010A9F File Offset: 0x0000EC9F
			public float Ratio { get; set; }

			// Token: 0x060002F6 RID: 758 RVA: 0x00010AA8 File Offset: 0x0000ECA8
			public static Bone.AppendBone CopyOf(Bone.AppendBone appendBone)
			{
				return new Bone.AppendBone
				{
					Index = appendBone.Index,
					Ratio = appendBone.Ratio
				};
			}
		}

		// Token: 0x02000054 RID: 84
		public class IkInfo
		{
			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x00010ACF File Offset: 0x0000ECCF
			// (set) Token: 0x060002F9 RID: 761 RVA: 0x00010AD7 File Offset: 0x0000ECD7
			public int IkTargetIndex { get; set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060002FA RID: 762 RVA: 0x00010AE0 File Offset: 0x0000ECE0
			// (set) Token: 0x060002FB RID: 763 RVA: 0x00010AE8 File Offset: 0x0000ECE8
			public int CcdIterateLimit { get; set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060002FC RID: 764 RVA: 0x00010AF1 File Offset: 0x0000ECF1
			// (set) Token: 0x060002FD RID: 765 RVA: 0x00010AF9 File Offset: 0x0000ECF9
			public float CcdAngleLimit { get; set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060002FE RID: 766 RVA: 0x00010B02 File Offset: 0x0000ED02
			// (set) Token: 0x060002FF RID: 767 RVA: 0x00010B0A File Offset: 0x0000ED0A
			public Bone.IkLink[] IkLinks { get; set; }

			// Token: 0x06000300 RID: 768 RVA: 0x00010B14 File Offset: 0x0000ED14
			public static Bone.IkInfo CopyOf(Bone.IkInfo ikInfo)
			{
				Bone.IkLink[] array = new Bone.IkLink[ikInfo.IkLinks.Length];
				ikInfo.IkLinks.CopyTo(array, 0);
				return new Bone.IkInfo
				{
					IkTargetIndex = ikInfo.IkTargetIndex,
					CcdIterateLimit = ikInfo.CcdIterateLimit,
					CcdAngleLimit = ikInfo.CcdAngleLimit,
					IkLinks = array
				};
			}
		}

		// Token: 0x02000055 RID: 85
		public class LocalAxis
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000302 RID: 770 RVA: 0x00010B74 File Offset: 0x0000ED74
			// (set) Token: 0x06000303 RID: 771 RVA: 0x00010B7C File Offset: 0x0000ED7C
			public Vector3 AxisX { get; set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000304 RID: 772 RVA: 0x00010B85 File Offset: 0x0000ED85
			// (set) Token: 0x06000305 RID: 773 RVA: 0x00010B8D File Offset: 0x0000ED8D
			public Vector3 AxisY { get; set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000306 RID: 774 RVA: 0x00010B96 File Offset: 0x0000ED96
			// (set) Token: 0x06000307 RID: 775 RVA: 0x00010B9E File Offset: 0x0000ED9E
			public Vector3 AxisZ { get; set; }

			// Token: 0x06000308 RID: 776 RVA: 0x00010BA7 File Offset: 0x0000EDA7
			public static Bone.LocalAxis CopyOf(Bone.LocalAxis localAxis)
			{
				return new Bone.LocalAxis
				{
					AxisX = localAxis.AxisX,
					AxisY = localAxis.AxisY,
					AxisZ = localAxis.AxisZ
				};
			}
		}
	}
}
