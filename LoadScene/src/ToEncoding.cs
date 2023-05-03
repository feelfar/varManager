using System;
using System.Collections.Generic;
using System.Text;

namespace mmd2timeline
{
	// Token: 0x02000038 RID: 56
	public class ToEncoding
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		public static string ToUnicode(byte[] sjis_bytes)
		{
			List<byte> list = new List<byte>();
			for (int i = 0; i < sjis_bytes.Length; i++)
			{
				ushort num;
				if ((sjis_bytes[i] >= 129 && sjis_bytes[i] <= 159) || (sjis_bytes[i] >= 224 && sjis_bytes[i] <= 234))
				{
					num = (ushort)(sjis_bytes[i] << 8);
					i++;
					if (sjis_bytes.Length > i)
					{
						num += (ushort)sjis_bytes[i];
					}
				}
				else
				{
					num = (ushort)sjis_bytes[i];
				}
				ushort code = SJISToUnicode.GetCode(num);
				byte item = (byte)(code >> 8);
				byte item2 = (byte)(code & 255);
				list.Add(item2);
				list.Add(item);
			}
			return Encoding.Unicode.GetString(list.ToArray());
		}
	}
}
