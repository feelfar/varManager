using System;
using System.Collections.Generic;
using System.Linq;
using LibMMD.Motion;
using LibMMD.Util;
using mmd2timeline;
using MVR.FileManagementSecure;
using UnityEngine;

namespace LibMMD.Reader
{
	// Token: 0x02000011 RID: 17
	public class VmdReader2
	{
		// Token: 0x06000073 RID: 115 RVA: 0x0000694C File Offset: 0x00004B4C
		public MmdMotion Read(string path)
		{
			BufferBinaryReader reader = new BufferBinaryReader(FileManagerSecure.ReadAllBytes(path));
			return this.Read(reader);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000696C File Offset: 0x00004B6C
		public MmdMotion Read(BufferBinaryReader reader)
		{
			VmdReader2.TempMmdMotion tempMmdMotion = new VmdReader2.TempMmdMotion();
			string value = MmdReaderUtil2.ReadStringFixedLength(reader, 30, null);
			if (!"Vocaloid Motion Data 0002".Equals(value))
			{
				LogUtil.LogError("File is not a VMD file.");
				throw new MmdFileParseException("File is not a VMD file.");
			}
			tempMmdMotion.Name = MmdReaderUtil2.ReadStringFixedLength(reader, 20, null);
			LogUtil.Log(string.Format("tempMmdMotion.Name :{0}", tempMmdMotion.Name));
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				VmdReader2.VmdBone vmdBone = this.ReadVmdBone(reader);
				BoneKeyframe orCreateBoneKeyFrame = tempMmdMotion.GetOrCreateBoneKeyFrame(vmdBone.BoneName, vmdBone.NFrame);
				orCreateBoneKeyFrame.Translation = vmdBone.Translation;
				orCreateBoneKeyFrame.Rotation = vmdBone.Rotation;
				Vector2 c;
				c.x = (float)vmdBone.XInterpolator[0] * 0.007874016f;
				c.y = (float)vmdBone.XInterpolator[4] * 0.007874016f;
				Vector2 c2;
				c2.x = (float)vmdBone.XInterpolator[8] * 0.007874016f;
				c2.y = (float)vmdBone.XInterpolator[12] * 0.007874016f;
				orCreateBoneKeyFrame.XInterpolator = new Interpolator();
				orCreateBoneKeyFrame.XInterpolator.SetC(c, c2);
				c.x = (float)vmdBone.YInterpolator[0] * 0.007874016f;
				c.y = (float)vmdBone.YInterpolator[4] * 0.007874016f;
				c2.x = (float)vmdBone.YInterpolator[8] * 0.007874016f;
				c2.y = (float)vmdBone.YInterpolator[12] * 0.007874016f;
				orCreateBoneKeyFrame.YInterpolator = new Interpolator();
				orCreateBoneKeyFrame.YInterpolator.SetC(c, c2);
				c.x = (float)vmdBone.ZInterpolator[0] * 0.007874016f;
				c.y = (float)vmdBone.ZInterpolator[4] * 0.007874016f;
				c2.x = (float)vmdBone.ZInterpolator[8] * 0.007874016f;
				c2.y = (float)vmdBone.ZInterpolator[12] * 0.007874016f;
				orCreateBoneKeyFrame.ZInterpolator = new Interpolator();
				orCreateBoneKeyFrame.ZInterpolator.SetC(c, c2);
				c.x = (float)vmdBone.RInterpolator[0] * 0.007874016f;
				c.y = (float)vmdBone.RInterpolator[4] * 0.007874016f;
				c2.x = (float)vmdBone.RInterpolator[8] * 0.007874016f;
				c2.y = (float)vmdBone.RInterpolator[12] * 0.007874016f;
				orCreateBoneKeyFrame.RInterpolator = new Interpolator();
				orCreateBoneKeyFrame.RInterpolator.SetC(c, c2);
			}
			int num2 = reader.ReadInt32();
			for (int j = 0; j < num2; j++)
			{
				VmdReader2.VmdMorph vmdMorph = this.ReadVmdMorph(reader);
				MorphKeyframe orCreateMorphKeyFrame = tempMmdMotion.GetOrCreateMorphKeyFrame(vmdMorph.MorphName, vmdMorph.NFrame);
				orCreateMorphKeyFrame.Weight = vmdMorph.Weight;
				orCreateMorphKeyFrame.WInterpolator = new Interpolator();
			}
			return tempMmdMotion.BuildMmdMotion();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006C30 File Offset: 0x00004E30
		public CameraMotion ReadCameraMotion(string path)
		{
			BufferBinaryReader reader = new BufferBinaryReader(FileManagerSecure.ReadAllBytes(path));
			/*var sr = File.OpenRead(path);
			byte[] buffer = new byte[sr.Length];
			sr.Read(buffer, 0, buffer.Length);
			BufferBinaryReader reader = new BufferBinaryReader(buffer);*/
			return this.ReadCameraMotion(reader, false);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006C54 File Offset: 0x00004E54
		public CameraMotion ReadCameraMotion(BufferBinaryReader reader, bool motionReadAlready = false)
		{
			if (!motionReadAlready)
			{
				this.Read(reader);
			}
			CameraMotion cameraMotion = new CameraMotion();
			int num = reader.ReadInt32();
			Dictionary<int, CameraKeyframe> dictionary = new Dictionary<int, CameraKeyframe>();
			for (int i = 0; i < num; i++)
			{
				int key = reader.ReadInt32();
				float focalLength = reader.ReadSingle();
				Vector3 position = MmdReaderUtil2.ReadVector3(reader);
				Vector3 rotation = MmdReaderUtil2.ReadVector3(reader);
				byte[] interpolation = reader.ReadBytes(24);
				uint num2 = reader.ReadUInt32();
				byte b = reader.ReadByte();
				CameraKeyframe value = new CameraKeyframe
				{
					Fov = num2,
					FocalLength = focalLength,
					Orthographic = (b > 0),
					Position = position,
					Rotation = rotation,
					Interpolation = interpolation
				};
				dictionary[key] = value;
			}
			List<KeyValuePair<int, CameraKeyframe>> keyFrames = (from kv in (from entry in dictionary
			select entry).ToList<KeyValuePair<int, CameraKeyframe>>()
			orderby kv.Key
			select kv).ToList<KeyValuePair<int, CameraKeyframe>>();
			cameraMotion.KeyFrames = keyFrames;
			return cameraMotion;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006D74 File Offset: 0x00004F74
		private VmdReader2.VmdBone ReadVmdBone(BufferBinaryReader reader)
		{
			return new VmdReader2.VmdBone
			{
				BoneName = MmdReaderUtil2.ReadStringFixedLength(reader, 15, null),
				NFrame = reader.ReadInt32(),
				Translation = MmdReaderUtil2.ReadVector3(reader),
				Rotation = MmdReaderUtil2.ReadQuaternion(reader),
				XInterpolator = reader.ReadBytes(16),
				YInterpolator = reader.ReadBytes(16),
				ZInterpolator = reader.ReadBytes(16),
				RInterpolator = reader.ReadBytes(16)
			};
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006DF1 File Offset: 0x00004FF1
		private VmdReader2.VmdMorph ReadVmdMorph(BufferBinaryReader reader)
		{
			return new VmdReader2.VmdMorph
			{
				MorphName = MmdReaderUtil2.ReadStringFixedLength(reader, 15, null),
				NFrame = reader.ReadInt32(),
				Weight = reader.ReadSingle()
			};
		}

		// Token: 0x02000046 RID: 70
		private class VmdBone
		{
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060002A9 RID: 681 RVA: 0x000104BC File Offset: 0x0000E6BC
			// (set) Token: 0x060002AA RID: 682 RVA: 0x000104C4 File Offset: 0x0000E6C4
			public string BoneName { get; set; }

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060002AB RID: 683 RVA: 0x000104CD File Offset: 0x0000E6CD
			// (set) Token: 0x060002AC RID: 684 RVA: 0x000104D5 File Offset: 0x0000E6D5
			public int NFrame { get; set; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060002AD RID: 685 RVA: 0x000104DE File Offset: 0x0000E6DE
			// (set) Token: 0x060002AE RID: 686 RVA: 0x000104E6 File Offset: 0x0000E6E6
			public Vector3 Translation { get; set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060002AF RID: 687 RVA: 0x000104EF File Offset: 0x0000E6EF
			// (set) Token: 0x060002B0 RID: 688 RVA: 0x000104F7 File Offset: 0x0000E6F7
			public Quaternion Rotation { get; set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x00010500 File Offset: 0x0000E700
			// (set) Token: 0x060002B2 RID: 690 RVA: 0x00010508 File Offset: 0x0000E708
			public byte[] XInterpolator { get; set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x00010511 File Offset: 0x0000E711
			// (set) Token: 0x060002B4 RID: 692 RVA: 0x00010519 File Offset: 0x0000E719
			public byte[] YInterpolator { get; set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x00010522 File Offset: 0x0000E722
			// (set) Token: 0x060002B6 RID: 694 RVA: 0x0001052A File Offset: 0x0000E72A
			public byte[] ZInterpolator { get; set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x00010533 File Offset: 0x0000E733
			// (set) Token: 0x060002B8 RID: 696 RVA: 0x0001053B File Offset: 0x0000E73B
			public byte[] RInterpolator { get; set; }
		}

		// Token: 0x02000047 RID: 71
		private class VmdMorph
		{
			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060002BA RID: 698 RVA: 0x0001054C File Offset: 0x0000E74C
			// (set) Token: 0x060002BB RID: 699 RVA: 0x00010554 File Offset: 0x0000E754
			public string MorphName { get; set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0001055D File Offset: 0x0000E75D
			// (set) Token: 0x060002BD RID: 701 RVA: 0x00010565 File Offset: 0x0000E765
			public int NFrame { get; set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060002BE RID: 702 RVA: 0x0001056E File Offset: 0x0000E76E
			// (set) Token: 0x060002BF RID: 703 RVA: 0x00010576 File Offset: 0x0000E776
			public float Weight { get; set; }
		}

		// Token: 0x02000048 RID: 72
		private class TempMmdMotion
		{
			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x00010587 File Offset: 0x0000E787
			// (set) Token: 0x060002C2 RID: 706 RVA: 0x0001058F File Offset: 0x0000E78F
			public string Name { get; set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060002C3 RID: 707 RVA: 0x00010598 File Offset: 0x0000E798
			// (set) Token: 0x060002C4 RID: 708 RVA: 0x000105A0 File Offset: 0x0000E7A0
			public int Length { get; private set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x000105A9 File Offset: 0x0000E7A9
			// (set) Token: 0x060002C6 RID: 710 RVA: 0x000105B1 File Offset: 0x0000E7B1
			public Dictionary<string, Dictionary<int, BoneKeyframe>> BoneMotions { get; set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x000105BA File Offset: 0x0000E7BA
			// (set) Token: 0x060002C8 RID: 712 RVA: 0x000105C2 File Offset: 0x0000E7C2
			public Dictionary<string, Dictionary<int, MorphKeyframe>> MorphMotions { get; set; }

			// Token: 0x060002C9 RID: 713 RVA: 0x000105CB File Offset: 0x0000E7CB
			public TempMmdMotion()
			{
				this.BoneMotions = new Dictionary<string, Dictionary<int, BoneKeyframe>>();
				this.MorphMotions = new Dictionary<string, Dictionary<int, MorphKeyframe>>();
			}

			// Token: 0x060002CA RID: 714 RVA: 0x000105EC File Offset: 0x0000E7EC
			public BoneKeyframe GetOrCreateBoneKeyFrame(string boneName, int frame)
			{
				if (frame > this.Length)
				{
					this.Length = frame;
				}
				Dictionary<int, BoneKeyframe> dictionary;
				if (!this.BoneMotions.TryGetValue(boneName, out dictionary))
				{
					dictionary = new Dictionary<int, BoneKeyframe>();
					this.BoneMotions.Add(boneName, dictionary);
				}
				BoneKeyframe boneKeyframe;
				if (!dictionary.TryGetValue(frame, out boneKeyframe))
				{
					boneKeyframe = new BoneKeyframe();
					dictionary.Add(frame, boneKeyframe);
				}
				return boneKeyframe;
			}

			// Token: 0x060002CB RID: 715 RVA: 0x00010648 File Offset: 0x0000E848
			public MorphKeyframe GetOrCreateMorphKeyFrame(string boneName, int frame)
			{
				if (frame > this.Length)
				{
					this.Length = frame;
				}
				Dictionary<int, MorphKeyframe> dictionary;
				if (!this.MorphMotions.TryGetValue(boneName, out dictionary))
				{
					dictionary = new Dictionary<int, MorphKeyframe>();
					this.MorphMotions.Add(boneName, dictionary);
				}
				MorphKeyframe morphKeyframe;
				if (!dictionary.TryGetValue(frame, out morphKeyframe))
				{
					morphKeyframe = new MorphKeyframe();
					dictionary.Add(frame, morphKeyframe);
				}
				return morphKeyframe;
			}

			// Token: 0x060002CC RID: 716 RVA: 0x000106A4 File Offset: 0x0000E8A4
			public MmdMotion BuildMmdMotion()
			{
				MmdMotion mmdMotion = new MmdMotion();
				mmdMotion.BoneMotions = new Dictionary<string, List<KeyValuePair<int, BoneKeyframe>>>();
				foreach (KeyValuePair<string, Dictionary<int, BoneKeyframe>> keyValuePair in this.BoneMotions)
				{
					List<KeyValuePair<int, BoneKeyframe>> list = keyValuePair.Value.ToList<KeyValuePair<int, BoneKeyframe>>();
					list = (from kv in list
					orderby kv.Key
					select kv).ToList<KeyValuePair<int, BoneKeyframe>>();
					mmdMotion.BoneMotions.Add(keyValuePair.Key, list);
				}
				mmdMotion.MorphMotions = new Dictionary<string, List<KeyValuePair<int, MorphKeyframe>>>();
				foreach (KeyValuePair<string, Dictionary<int, MorphKeyframe>> keyValuePair2 in this.MorphMotions)
				{
					List<KeyValuePair<int, MorphKeyframe>> list2 = keyValuePair2.Value.ToList<KeyValuePair<int, MorphKeyframe>>();
					list2 = (from kv in list2
					orderby kv.Key
					select kv).ToList<KeyValuePair<int, MorphKeyframe>>();
					mmdMotion.MorphMotions.Add(keyValuePair2.Key, list2);
				}
				mmdMotion.Length = this.Length;
				mmdMotion.Name = this.Name;
				return mmdMotion;
			}
		}
	}
}
