using System;
using System.Text;
using LibMMD.Reader;
using mmd2timeline;
using UnityEngine;

namespace LibMMD.Util
{
	// Token: 0x02000008 RID: 8
	public static class MmdReaderUtil2
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00004558 File Offset: 0x00002758
		public static string ReadStringFixedLength(BufferBinaryReader reader, int length, Encoding encoding = null)
		{
			if (length < 0)
			{
				throw new MmdFileParseException("pmx string length is negative");
			}
			if (length == 0)
			{
				return "";
			}
			byte[] array = reader.ReadBytes(length);
			string text;
			if (encoding != null)
			{
				text = encoding.GetString(array);
			}
			else
			{
				text = ToEncoding.ToUnicode(array);
			}
			int num = text.IndexOf("\0", StringComparison.Ordinal);
			if (num >= 0)
			{
				text = text.Substring(0, num);
			}
			return text;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000045B8 File Offset: 0x000027B8
		public static string ReadSizedString(BufferBinaryReader reader, Encoding encoding)
		{
			int length = reader.ReadInt32();
			return MmdReaderUtil2.ReadStringFixedLength(reader, length, encoding);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000045D4 File Offset: 0x000027D4
		public static Vector4 ReadVector4(BufferBinaryReader reader)
		{
			Vector4 result = default(Vector4);
			result[0] = MathUtil.NanToZero(reader.ReadSingle());
			result[1] = MathUtil.NanToZero(reader.ReadSingle());
			result[2] = MathUtil.NanToZero(reader.ReadSingle());
			result[3] = MathUtil.NanToZero(reader.ReadSingle());
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004638 File Offset: 0x00002838
		public static Quaternion ReadQuaternion(BufferBinaryReader reader)
		{
			return new Quaternion
			{
				x = MathUtil.NanToZero(reader.ReadSingle()),
				y = MathUtil.NanToZero(reader.ReadSingle()),
				z = MathUtil.NanToZero(reader.ReadSingle()),
				w = MathUtil.NanToZero(reader.ReadSingle())
			};
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004698 File Offset: 0x00002898
		public static Vector3 ReadVector3(BufferBinaryReader reader)
		{
			Vector3 result = default(Vector3);
			result[0] = MathUtil.NanToZero(reader.ReadSingle());
			result[1] = MathUtil.NanToZero(reader.ReadSingle());
			result[2] = MathUtil.NanToZero(reader.ReadSingle());
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000046E8 File Offset: 0x000028E8
		public static Vector2 ReadVector2(BufferBinaryReader reader)
		{
			Vector2 result = default(Vector2);
			result[0] = MathUtil.NanToZero(reader.ReadSingle());
			result[1] = MathUtil.NanToZero(reader.ReadSingle());
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004724 File Offset: 0x00002924
		public static int ReadIndex(BufferBinaryReader reader, int size)
		{
			switch (size)
			{
			case 1:
				return (int)reader.ReadSByte();
			case 2:
				return (int)reader.ReadUInt16();
			case 4:
				return reader.ReadInt32();
			}
			throw new MmdFileParseException("invalid index size: " + size.ToString());
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004778 File Offset: 0x00002978
		public static Color ReadColor(BufferBinaryReader reader, bool readA)
		{
			return new Color
			{
				r = reader.ReadSingle(),
				g = reader.ReadSingle(),
				b = reader.ReadSingle(),
				a = (readA ? reader.ReadSingle() : 1f)
			};
		}
	}
}
