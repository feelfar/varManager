using System;
using System.Text;
using LibMMD.Material;
using LibMMD.Model;
using LibMMD.Util;
using mmd2timeline;
using UnityEngine;

namespace LibMMD.Reader
{
	// Token: 0x02000010 RID: 16
	public class PmxReader2 : ModelReader2
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00005958 File Offset: 0x00003B58
		public override MmdModel Read(BufferBinaryReader reader)
		{
			PmxReader2.PmxMeta pmxMeta = PmxReader2.ReadMeta(reader);
			if (!"PMX ".Equals(pmxMeta.Magic) || Math.Abs(pmxMeta.Version - 2f) > 0.0001f || pmxMeta.FileFlagSize != 8)
			{
				throw new MmdFileParseException("File is not a PMX 2.0 file");
			}
			MmdModel mmdModel = new MmdModel();
			PmxReader2.PmxConfig pmxConfig = PmxReader2.ReadPmxConfig(reader, mmdModel);
			PmxReader2.ReadModelNameAndDescription(reader, mmdModel, pmxConfig);
			PmxReader2.ReadVertices(reader, mmdModel, pmxConfig);
			PmxReader2.ReadTriangles(reader, mmdModel, pmxConfig);
			MmdTexture[] textureList = PmxReader2.ReadTextureList(reader, pmxConfig);
			PmxReader2.ReadParts(reader, mmdModel, pmxConfig, textureList);
			PmxReader2.ReadBones(reader, mmdModel, pmxConfig);
			PmxReader2.ReadMorphs(reader, mmdModel, pmxConfig);
			PmxReader2.ReadEntries(reader, pmxConfig);
			PmxReader2.ReadRigidBodies(reader, mmdModel, pmxConfig);
			PmxReader2.ReadConstraints(reader, mmdModel, pmxConfig);
			mmdModel.Normalize();
			return mmdModel;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005A10 File Offset: 0x00003C10
		private static void ReadConstraints(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			model.Constraints = new Constraint[num];
			for (int i = 0; i < num; i++)
			{
				Constraint constraint = new Constraint
				{
					Name = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					NameEn = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding)
				};
				if (reader.ReadByte() != 0)
				{
					throw new MmdFileParseException("Only 6DOF spring joints are supported.");
				}
				constraint.AssociatedRigidBodyIndex[0] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.RigidBodyIndexSize);
				constraint.AssociatedRigidBodyIndex[1] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.RigidBodyIndexSize);
				constraint.Position = MmdReaderUtil2.ReadVector3(reader);
				constraint.Rotation = MmdReaderUtil2.ReadVector3(reader);
				constraint.PositionLowLimit = MmdReaderUtil2.ReadVector3(reader);
				constraint.PositionHiLimit = MmdReaderUtil2.ReadVector3(reader);
				constraint.RotationLowLimit = MmdReaderUtil2.ReadVector3(reader);
				constraint.RotationHiLimit = MmdReaderUtil2.ReadVector3(reader);
				constraint.SpringTranslate = MmdReaderUtil2.ReadVector3(reader);
				constraint.SpringRotate = MmdReaderUtil2.ReadVector3(reader);
				model.Constraints[i] = constraint;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005B18 File Offset: 0x00003D18
		private static PmxReader2.PmxConfig ReadPmxConfig(BufferBinaryReader reader, MmdModel model)
		{
			PmxReader2.PmxConfig pmxConfig = new PmxReader2.PmxConfig();
			pmxConfig.Utf8Encoding = (reader.ReadByte() > 0);
			pmxConfig.ExtraUvNumber = (int)reader.ReadSByte();
			pmxConfig.VertexIndexSize = (int)reader.ReadSByte();
			pmxConfig.TextureIndexSize = (int)reader.ReadSByte();
			pmxConfig.MaterialIndexSize = (int)reader.ReadSByte();
			pmxConfig.BoneIndexSize = (int)reader.ReadSByte();
			pmxConfig.MorphIndexSize = (int)reader.ReadSByte();
			pmxConfig.RigidBodyIndexSize = (int)reader.ReadSByte();
			model.ExtraUvNumber = pmxConfig.ExtraUvNumber;
			pmxConfig.Encoding = (pmxConfig.Utf8Encoding ? Encoding.UTF8 : Encoding.Unicode);
			return pmxConfig;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00005BB8 File Offset: 0x00003DB8
		private static void ReadRigidBodies(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			model.Rigidbodies = new MmdRigidBody[num];
			for (int i = 0; i < num; i++)
			{
				MmdRigidBody mmdRigidBody = new MmdRigidBody
				{
					Name = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					NameEn = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					AssociatedBoneIndex = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize),
					CollisionGroup = (int)reader.ReadByte(),
					CollisionMask = reader.ReadUInt16(),
					Shape = (MmdRigidBody.RigidBodyShape)reader.ReadByte(),
					Dimemsions = MmdReaderUtil2.ReadVector3(reader),
					Position = MmdReaderUtil2.ReadVector3(reader),
					Rotation = MmdReaderUtil2.ReadVector3(reader),
					Mass = reader.ReadSingle(),
					TranslateDamp = reader.ReadSingle(),
					RotateDamp = reader.ReadSingle(),
					Restitution = reader.ReadSingle(),
					Friction = reader.ReadSingle(),
					Type = (MmdRigidBody.RigidBodyType)reader.ReadByte()
				};
				model.Rigidbodies[i] = mmdRigidBody;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005CC0 File Offset: 0x00003EC0
		private static void ReadEntries(BufferBinaryReader reader, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding);
				MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding);
				reader.ReadByte();
				int num2 = reader.ReadInt32();
				for (int j = 0; j < num2; j++)
				{
					if (reader.ReadByte() == 1)
					{
						MmdReaderUtil2.ReadIndex(reader, pmxConfig.MorphIndexSize);
					}
					else
					{
						MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
					}
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005D3C File Offset: 0x00003F3C
		private static void ReadMorphs(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			int? num2 = null;
			model.Morphs = new Morph[num];
			for (int i = 0; i < num; i++)
			{
				Morph morph = new Morph
				{
					Name = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					NameEn = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					Category = (Morph.MorphCategory)reader.ReadByte()
				};
				if (morph.Category == Morph.MorphCategory.MorphCatSystem)
				{
					num2 = new int?(i);
				}
				morph.Type = (Morph.MorphType)reader.ReadByte();
				int num3 = reader.ReadInt32();
				morph.MorphDatas = new Morph.MorphData[num3];
				switch (morph.Type)
				{
				case Morph.MorphType.MorphTypeGroup:
					for (int j = 0; j < num3; j++)
					{
						Morph.GroupMorph groupMorph = new Morph.GroupMorph
						{
							MorphIndex = MmdReaderUtil2.ReadIndex(reader, pmxConfig.MorphIndexSize),
							MorphRate = reader.ReadSingle()
						};
						morph.MorphDatas[j] = groupMorph;
					}
					break;
				case Morph.MorphType.MorphTypeVertex:
					for (int k = 0; k < num3; k++)
					{
						Morph.VertexMorph vertexMorph = new Morph.VertexMorph
						{
							VertexIndex = MmdReaderUtil2.ReadIndex(reader, pmxConfig.VertexIndexSize),
							Offset = MmdReaderUtil2.ReadVector3(reader)
						};
						morph.MorphDatas[k] = vertexMorph;
					}
					break;
				case Morph.MorphType.MorphTypeBone:
					for (int l = 0; l < num3; l++)
					{
						Morph.BoneMorph boneMorph = new Morph.BoneMorph
						{
							BoneIndex = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize),
							Translation = MmdReaderUtil2.ReadVector3(reader),
							Rotation = MmdReaderUtil2.ReadQuaternion(reader)
						};
						morph.MorphDatas[l] = boneMorph;
					}
					break;
				case Morph.MorphType.MorphTypeUv:
				case Morph.MorphType.MorphTypeExtUv1:
				case Morph.MorphType.MorphTypeExtUv2:
				case Morph.MorphType.MorphTypeExtUv3:
				case Morph.MorphType.MorphTypeExtUv4:
					for (int m = 0; m < num3; m++)
					{
						Morph.UvMorph uvMorph = new Morph.UvMorph
						{
							VertexIndex = MmdReaderUtil2.ReadIndex(reader, pmxConfig.VertexIndexSize),
							Offset = MmdReaderUtil2.ReadVector4(reader)
						};
						morph.MorphDatas[m] = uvMorph;
					}
					break;
				case Morph.MorphType.MorphTypeMaterial:
					for (int n = 0; n < num3; n++)
					{
						Morph.MaterialMorph materialMorph = new Morph.MaterialMorph();
						int num4 = MmdReaderUtil2.ReadIndex(reader, pmxConfig.MaterialIndexSize);
						if (num4 < model.Parts.Length && num4 > 0)
						{
							materialMorph.MaterialIndex = num4;
							materialMorph.Global = false;
						}
						else
						{
							materialMorph.MaterialIndex = 0;
							materialMorph.Global = true;
						}
						materialMorph.Method = (Morph.MaterialMorph.MaterialMorphMethod)reader.ReadByte();
						materialMorph.Diffuse = MmdReaderUtil2.ReadColor(reader, true);
						materialMorph.Specular = MmdReaderUtil2.ReadColor(reader, false);
						materialMorph.Shiness = reader.ReadSingle();
						materialMorph.Ambient = MmdReaderUtil2.ReadColor(reader, false);
						materialMorph.EdgeColor = MmdReaderUtil2.ReadColor(reader, true);
						materialMorph.EdgeSize = reader.ReadSingle();
						materialMorph.Texture = MmdReaderUtil2.ReadVector4(reader);
						materialMorph.SubTexture = MmdReaderUtil2.ReadVector4(reader);
						materialMorph.ToonTexture = MmdReaderUtil2.ReadVector4(reader);
						morph.MorphDatas[n] = materialMorph;
					}
					break;
				default:
					throw new MmdFileParseException("invalid morph type " + morph.Type.ToString());
				}
				bool flag = num2 != null;
				model.Morphs[i] = morph;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006064 File Offset: 0x00004264
		private static void ReadBones(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			model.Bones = new Bone[num];
			for (int i = 0; i < num; i++)
			{
				Bone bone = new Bone
				{
					Name = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					NameEn = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding),
					Position = MmdReaderUtil2.ReadVector3(reader)
				};
				bone.InitPosition = bone.Position;
				int num2 = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				if (num2 < num && num2 >= 0)
				{
					bone.ParentIndex = num2;
				}
				else
				{
					bone.ParentIndex = -1;
				}
				bone.TransformLevel = reader.ReadInt32();
				ushort num3 = reader.ReadUInt16();
				bone.ChildBoneVal.ChildUseId = ((num3 & 1) > 0);
				bone.Rotatable = ((num3 & 2) > 0);
				bone.Movable = ((num3 & 4) > 0);
				bone.Visible = ((num3 & 8) > 0);
				bone.Controllable = ((num3 & 16) > 0);
				bone.HasIk = ((num3 & 32) > 0);
				bone.AppendRotate = ((num3 & 256) > 0);
				bone.AppendTranslate = ((num3 & 512) > 0);
				bone.RotAxisFixed = ((num3 & 1024) > 0);
				bone.UseLocalAxis = ((num3 & 2048) > 0);
				bone.PostPhysics = ((num3 & 4096) > 0);
				bone.ReceiveTransform = ((num3 & 8192) > 0);
				if (bone.ChildBoneVal.ChildUseId)
				{
					bone.ChildBoneVal.Index = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				}
				else
				{
					bone.ChildBoneVal.Offset = MmdReaderUtil2.ReadVector3(reader);
				}
				if (bone.RotAxisFixed)
				{
					bone.RotAxis = MmdReaderUtil2.ReadVector3(reader);
				}
				if (bone.AppendRotate || bone.AppendTranslate)
				{
					bone.AppendBoneVal.Index = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
					bone.AppendBoneVal.Ratio = reader.ReadSingle();
				}
				if (bone.UseLocalAxis)
				{
					Vector3 vector = MmdReaderUtil2.ReadVector3(reader);
					Vector3 vector2 = MmdReaderUtil2.ReadVector3(reader);
					Vector3 vector3 = Vector3.Cross(vector, vector2);
					vector2 = Vector3.Cross(vector, vector3);
					vector.Normalize();
					vector3.Normalize();
					vector2.Normalize();
					bone.LocalAxisVal.AxisX = vector;
					bone.LocalAxisVal.AxisY = vector3;
					bone.LocalAxisVal.AxisZ = vector2;
				}
				if (bone.ReceiveTransform)
				{
					bone.ExportKey = reader.ReadInt32();
				}
				if (bone.HasIk)
				{
					PmxReader2.ReadBoneIk(reader, bone, pmxConfig.BoneIndexSize);
				}
				model.Bones[i] = bone;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000062EC File Offset: 0x000044EC
		private static void ReadBoneIk(BufferBinaryReader reader, Bone bone, int boneIndexSize)
		{
			bone.IkInfoVal = new Bone.IkInfo();
			bone.IkInfoVal.IkTargetIndex = MmdReaderUtil2.ReadIndex(reader, boneIndexSize);
			bone.IkInfoVal.CcdIterateLimit = reader.ReadInt32();
			bone.IkInfoVal.CcdAngleLimit = reader.ReadSingle();
			int num = reader.ReadInt32();
			bone.IkInfoVal.IkLinks = new Bone.IkLink[num];
			for (int i = 0; i < num; i++)
			{
				Bone.IkLink ikLink = new Bone.IkLink();
				ikLink.LinkIndex = MmdReaderUtil2.ReadIndex(reader, boneIndexSize);
				ikLink.HasLimit = (reader.ReadByte() > 0);
				if (ikLink.HasLimit)
				{
					ikLink.LoLimit = MmdReaderUtil2.ReadVector3(reader);
					ikLink.HiLimit = MmdReaderUtil2.ReadVector3(reader);
				}
				bone.IkInfoVal.IkLinks[i] = ikLink;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000063AC File Offset: 0x000045AC
		private static void ReadParts(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig, MmdTexture[] textureList)
		{
			int num = reader.ReadInt32();
			int num2 = 0;
			model.Parts = new Part[num];
			for (int i = 0; i < num; i++)
			{
				Part part = new Part();
				PmxReader2.ReadMaterial(reader, pmxConfig.Encoding, pmxConfig.TextureIndexSize, textureList);
				int num3 = reader.ReadInt32();
				if (num3 % 3 != 0)
				{
					throw new MmdFileParseException(string.Concat(new string[]
					{
						"part",
						i.ToString(),
						" triangle index count ",
						num3.ToString(),
						" is not multiple of 3"
					}));
				}
				part.BaseShift = num2;
				part.TriangleIndexNum = num3;
				num2 += num3;
				model.Parts[i] = part;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006464 File Offset: 0x00004664
		private static MmdTexture[] ReadTextureList(BufferBinaryReader reader, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			MmdTexture[] array = new MmdTexture[num];
			for (int i = 0; i < num; i++)
			{
				Encoding encoding = pmxConfig.Utf8Encoding ? Encoding.UTF8 : Encoding.Unicode;
				string texturePath = MmdReaderUtil2.ReadSizedString(reader, encoding);
				array[i] = new MmdTexture(texturePath);
			}
			return array;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000064B4 File Offset: 0x000046B4
		private static void ReadTriangles(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			model.TriangleIndexes = new int[num];
			if (num % 3 != 0)
			{
				throw new MmdFileParseException("triangle index count " + num.ToString() + " is not multiple of 3");
			}
			for (int i = 0; i < num; i++)
			{
				model.TriangleIndexes[i] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.VertexIndexSize);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006518 File Offset: 0x00004718
		private static void ReadVertices(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			int num = reader.ReadInt32();
			model.Vertices = new Vertex[num];
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				Vertex vertex = PmxReader2.ReadVertex(reader, pmxConfig);
				model.Vertices[(int)num2] = vertex;
				num2 += 1U;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006558 File Offset: 0x00004758
		private static void ReadModelNameAndDescription(BufferBinaryReader reader, MmdModel model, PmxReader2.PmxConfig pmxConfig)
		{
			model.Name = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding);
			model.NameEn = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding);
			model.Description = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding);
			model.DescriptionEn = MmdReaderUtil2.ReadSizedString(reader, pmxConfig.Encoding);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000065B0 File Offset: 0x000047B0
		private static Vertex ReadVertex(BufferBinaryReader reader, PmxReader2.PmxConfig pmxConfig)
		{
			PmxReader2.PmxVertexBasic pmxVertexBasic = PmxReader2.ReadVertexBasic(reader);
			Vertex vertex = new Vertex
			{
				Coordinate = pmxVertexBasic.Coordinate,
				Normal = pmxVertexBasic.Normal,
				UvCoordinate = pmxVertexBasic.UvCoordinate
			};
			if (pmxConfig.ExtraUvNumber > 0)
			{
				Vector4[] array = new Vector4[pmxConfig.ExtraUvNumber];
				for (int i = 0; i < pmxConfig.ExtraUvNumber; i++)
				{
					array[i] = MmdReaderUtil2.ReadVector4(reader);
				}
				vertex.ExtraUvCoordinate = array;
			}
			SkinningOperator skinningOperator = new SkinningOperator();
			SkinningOperator.SkinningType type = (SkinningOperator.SkinningType)reader.ReadByte();
			skinningOperator.Type = type;
			switch (type)
			{
			case SkinningOperator.SkinningType.SkinningBdef1:
				skinningOperator.Param = new SkinningOperator.Bdef1
				{
					BoneId = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize)
				};
				break;
			case SkinningOperator.SkinningType.SkinningBdef2:
			{
				SkinningOperator.Bdef2 bdef = new SkinningOperator.Bdef2();
				bdef.BoneId[0] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				bdef.BoneId[1] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				bdef.BoneWeight = reader.ReadSingle();
				skinningOperator.Param = bdef;
				break;
			}
			case SkinningOperator.SkinningType.SkinningBdef4:
			{
				SkinningOperator.Bdef4 bdef2 = new SkinningOperator.Bdef4();
				for (int j = 0; j < 4; j++)
				{
					bdef2.BoneId[j] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				}
				for (int k = 0; k < 4; k++)
				{
					bdef2.BoneWeight[k] = reader.ReadSingle();
				}
				skinningOperator.Param = bdef2;
				break;
			}
			case SkinningOperator.SkinningType.SkinningSdef:
			{
				SkinningOperator.Sdef sdef = new SkinningOperator.Sdef();
				sdef.BoneId[0] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				sdef.BoneId[1] = MmdReaderUtil2.ReadIndex(reader, pmxConfig.BoneIndexSize);
				sdef.BoneWeight = reader.ReadSingle();
				sdef.C = MmdReaderUtil2.ReadVector3(reader);
				sdef.R0 = MmdReaderUtil2.ReadVector3(reader);
				sdef.R1 = MmdReaderUtil2.ReadVector3(reader);
				skinningOperator.Param = sdef;
				break;
			}
			default:
				throw new MmdFileParseException("invalid skinning type: " + type.ToString());
			}
			vertex.SkinningOperator = skinningOperator;
			vertex.EdgeScale = reader.ReadSingle();
			return vertex;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000067CC File Offset: 0x000049CC
		private static MmdMaterial ReadMaterial(BufferBinaryReader reader, Encoding encoding, int textureIndexSize, MmdTexture[] textureList)
		{
			MmdMaterial mmdMaterial = new MmdMaterial();
			mmdMaterial.Name = MmdReaderUtil2.ReadSizedString(reader, encoding);
			mmdMaterial.NameEn = MmdReaderUtil2.ReadSizedString(reader, encoding);
			mmdMaterial.DiffuseColor = MmdReaderUtil2.ReadColor(reader, true);
			mmdMaterial.SpecularColor = MmdReaderUtil2.ReadColor(reader, false);
			mmdMaterial.Shiness = reader.ReadSingle();
			mmdMaterial.AmbientColor = MmdReaderUtil2.ReadColor(reader, false);
			byte b = reader.ReadByte();
			mmdMaterial.DrawDoubleFace = ((b & 1) > 0);
			mmdMaterial.DrawGroundShadow = ((b & 2) > 0);
			mmdMaterial.CastSelfShadow = ((b & 4) > 0);
			mmdMaterial.DrawSelfShadow = ((b & 8) > 0);
			mmdMaterial.DrawEdge = ((b & 16) > 0);
			mmdMaterial.EdgeColor = MmdReaderUtil2.ReadColor(reader, true);
			mmdMaterial.EdgeSize = reader.ReadSingle();
			MmdReaderUtil2.ReadIndex(reader, textureIndexSize);
			MmdReaderUtil2.ReadIndex(reader, textureIndexSize);
			mmdMaterial.SubTextureType = (MmdMaterial.SubTextureTypeEnum)reader.ReadByte();
			if (reader.ReadByte() > 0)
			{
				reader.ReadByte();
			}
			else
			{
				MmdReaderUtil2.ReadIndex(reader, textureIndexSize);
			}
			mmdMaterial.MetaInfo = MmdReaderUtil2.ReadSizedString(reader, encoding);
			return mmdMaterial;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000068D0 File Offset: 0x00004AD0
		private static PmxReader2.PmxMeta ReadMeta(BufferBinaryReader reader)
		{
			PmxReader2.PmxMeta result;
			result.Magic = MmdReaderUtil2.ReadStringFixedLength(reader, 4, Encoding.ASCII);
			result.Version = reader.ReadSingle();
			result.FileFlagSize = reader.ReadByte();
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000690C File Offset: 0x00004B0C
		private static PmxReader2.PmxVertexBasic ReadVertexBasic(BufferBinaryReader reader)
		{
			PmxReader2.PmxVertexBasic result;
			result.Coordinate = MmdReaderUtil2.ReadVector3(reader);
			result.Normal = MmdReaderUtil2.ReadVector3(reader);
			result.UvCoordinate = MmdReaderUtil2.ReadVector2(reader);
			return result;
		}

		// Token: 0x02000041 RID: 65
		private struct PmxMeta
		{
			// Token: 0x04000162 RID: 354
			public string Magic;

			// Token: 0x04000163 RID: 355
			public float Version;

			// Token: 0x04000164 RID: 356
			public byte FileFlagSize;
		}

		// Token: 0x02000042 RID: 66
		private struct PmxVertexBasic
		{
			// Token: 0x04000165 RID: 357
			public Vector3 Coordinate;

			// Token: 0x04000166 RID: 358
			public Vector3 Normal;

			// Token: 0x04000167 RID: 359
			public Vector2 UvCoordinate;
		}

		// Token: 0x02000043 RID: 67
		private class PmxConfig
		{
			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x06000294 RID: 660 RVA: 0x0001040B File Offset: 0x0000E60B
			// (set) Token: 0x06000295 RID: 661 RVA: 0x00010413 File Offset: 0x0000E613
			public bool Utf8Encoding { get; set; }

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x06000296 RID: 662 RVA: 0x0001041C File Offset: 0x0000E61C
			// (set) Token: 0x06000297 RID: 663 RVA: 0x00010424 File Offset: 0x0000E624
			public Encoding Encoding { get; set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000298 RID: 664 RVA: 0x0001042D File Offset: 0x0000E62D
			// (set) Token: 0x06000299 RID: 665 RVA: 0x00010435 File Offset: 0x0000E635
			public int ExtraUvNumber { get; set; }

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x0600029A RID: 666 RVA: 0x0001043E File Offset: 0x0000E63E
			// (set) Token: 0x0600029B RID: 667 RVA: 0x00010446 File Offset: 0x0000E646
			public int VertexIndexSize { get; set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x0600029C RID: 668 RVA: 0x0001044F File Offset: 0x0000E64F
			// (set) Token: 0x0600029D RID: 669 RVA: 0x00010457 File Offset: 0x0000E657
			public int TextureIndexSize { get; set; }

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x0600029E RID: 670 RVA: 0x00010460 File Offset: 0x0000E660
			// (set) Token: 0x0600029F RID: 671 RVA: 0x00010468 File Offset: 0x0000E668
			public int MaterialIndexSize { get; set; }

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060002A0 RID: 672 RVA: 0x00010471 File Offset: 0x0000E671
			// (set) Token: 0x060002A1 RID: 673 RVA: 0x00010479 File Offset: 0x0000E679
			public int BoneIndexSize { get; set; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060002A2 RID: 674 RVA: 0x00010482 File Offset: 0x0000E682
			// (set) Token: 0x060002A3 RID: 675 RVA: 0x0001048A File Offset: 0x0000E68A
			public int MorphIndexSize { get; set; }

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x00010493 File Offset: 0x0000E693
			// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001049B File Offset: 0x0000E69B
			public int RigidBodyIndexSize { get; set; }
		}

		// Token: 0x02000044 RID: 68
		private abstract class PmxMaterialDrawFlags
		{
			// Token: 0x04000171 RID: 369
			public const byte PmxMaterialDrawDoubleFace = 1;

			// Token: 0x04000172 RID: 370
			public const byte PmxMaterialDrawGroundShadow = 2;

			// Token: 0x04000173 RID: 371
			public const byte PmxMaterialCastSelfShadow = 4;

			// Token: 0x04000174 RID: 372
			public const byte PmxMaterialDrawSelfShadow = 8;

			// Token: 0x04000175 RID: 373
			public const byte PmxMaterialDrawEdge = 16;
		}

		// Token: 0x02000045 RID: 69
		private abstract class PmxBoneFlags
		{
			// Token: 0x04000176 RID: 374
			public const ushort PmxBoneChildUseId = 1;

			// Token: 0x04000177 RID: 375
			public const ushort PmxBoneRotatable = 2;

			// Token: 0x04000178 RID: 376
			public const ushort PmxBoneMovable = 4;

			// Token: 0x04000179 RID: 377
			public const ushort PmxBoneVisible = 8;

			// Token: 0x0400017A RID: 378
			public const ushort PmxBoneControllable = 16;

			// Token: 0x0400017B RID: 379
			public const ushort PmxBoneHasIk = 32;

			// Token: 0x0400017C RID: 380
			public const ushort PmxBoneAcquireRotate = 256;

			// Token: 0x0400017D RID: 381
			public const ushort PmxBoneAcquireTranslate = 512;

			// Token: 0x0400017E RID: 382
			public const ushort PmxBoneRotAxisFixed = 1024;

			// Token: 0x0400017F RID: 383
			public const ushort PmxBoneUseLocalAxis = 2048;

			// Token: 0x04000180 RID: 384
			public const ushort PmxBonePostPhysics = 4096;

			// Token: 0x04000181 RID: 385
			public const ushort PmxBoneReceiveTransform = 8192;
		}
	}
}
