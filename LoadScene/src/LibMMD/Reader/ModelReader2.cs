using System;
using LibMMD.Model;
using mmd2timeline;
using MVR.FileManagementSecure;

namespace LibMMD.Reader
{
	// Token: 0x0200000F RID: 15
	public abstract class ModelReader2
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000058F4 File Offset: 0x00003AF4
		public MmdModel Read(string path)
		{
			byte[] bytes;
            if (path == null)
            {
                bytes = FileManagerSecure.ReadAllBytes(Settings.pmxPath);
            }
            else
            {
                bytes = FileManagerSecure.ReadAllBytes(path);
            }
            BufferBinaryReader reader = new BufferBinaryReader(bytes);
			return this.Read(reader);
		}

		// Token: 0x0600005E RID: 94
		public abstract MmdModel Read(BufferBinaryReader reader);

		// Token: 0x0600005F RID: 95 RVA: 0x00005942 File Offset: 0x00003B42
		public static MmdModel LoadMmdModel(string path)
		{
			return new PmxReader2().Read(path);
		}
	}
}
