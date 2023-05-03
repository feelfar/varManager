using System;
using System.Text;

namespace mmd2timeline
{
	// Token: 0x0200002D RID: 45
	public class BufferBinaryReader
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000E5D3 File Offset: 0x0000C7D3
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000E5DB File Offset: 0x0000C7DB
		public int Offset { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public int Length { get; private set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000E5FD File Offset: 0x0000C7FD
		public BufferBinaryReader(byte[] bytes)
		{
			this.Length = bytes.Length;
			this.buffer = bytes;
			this.Offset = 0;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000E61C File Offset: 0x0000C81C
		public byte ReadByte()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			return array[offset];
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000E644 File Offset: 0x0000C844
		public sbyte ReadSByte()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			return (sbyte)array[offset];
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000E66A File Offset: 0x0000C86A
		public virtual bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000E678 File Offset: 0x0000C878
		public string ReadChars(int count)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				byte[] array = this.buffer;
				int offset = this.Offset;
				this.Offset = offset + 1;
				stringBuilder2.Append(Convert.ToChar(array[offset]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		public byte[] ReadBytes(int count)
		{
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				byte[] array2 = array;
				int num = i;
				byte[] array3 = this.buffer;
				int offset = this.Offset;
				this.Offset = offset + 1;
				array2[num] = array3[offset];
			}
			return array;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000E700 File Offset: 0x0000C900
		public short ReadInt16()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			ushort num = array[offset];
			byte[] array2 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			return (short)(num | (ushort)array2[offset] << 8);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000E744 File Offset: 0x0000C944
		public ushort ReadUInt16()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			ushort num = array[offset];
			byte[] array2 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			return (ushort)(num | array2[offset] << 8);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000E788 File Offset: 0x0000C988
		public int ReadInt24()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			int num = array[offset];
			byte[] array2 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			int num2 = num | array2[offset] << 8;
			byte[] array3 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			return num2 | (int)array3[offset] << 16;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		public uint ReadUInt24()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			uint num = array[offset];
			byte[] array2 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			uint num2 = num | (uint)(array2[offset]) << 8;
			byte[] array3 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			return num2 | (uint)(array3[offset]) << 16;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000E844 File Offset: 0x0000CA44
		public int ReadInt32()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			int num = array[offset];
			byte[] array2 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			int num2 = num | array2[offset] << 8;
			byte[] array3 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			int num3 = num2 | array3[offset] << 16;
			byte[] array4 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			return num3 | (int)array4[offset] << 24;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		public uint ReadUInt32()
		{
			byte[] array = this.buffer;
			int offset = this.Offset;
			this.Offset = offset + 1;
			uint num = array[offset];
			byte[] array2 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			uint num2 = num | (uint)array2[offset] << 8;
			byte[] array3 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			uint num3 = num2 | (uint)array3[offset] << 16;
			byte[] array4 = this.buffer;
			offset = this.Offset;
			this.Offset = offset + 1;
			return num3 | (uint)array4[offset] << 24;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000E938 File Offset: 0x0000CB38
		public float ReadSingle()
		{
			float result = BitConverter.ToSingle(this.buffer, this.Offset);
			this.Offset += 4;
			return result;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000E959 File Offset: 0x0000CB59
		public double ReadDouble()
		{
			double result = BitConverter.ToDouble(this.buffer, this.Offset);
			this.Offset += 8;
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000E97A File Offset: 0x0000CB7A
		public long ReadInt64()
		{
			long result = BitConverter.ToInt64(this.buffer, this.Offset);
			this.Offset += 8;
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E99B File Offset: 0x0000CB9B
		public ulong ReadUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.buffer, this.Offset);
			this.Offset += 8;
			return result;
		}

		// Token: 0x04000119 RID: 281
		private readonly byte[] buffer;
	}
}
