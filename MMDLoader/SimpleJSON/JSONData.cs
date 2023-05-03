using System;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000CCB RID: 3275
	public class JSONData : JSONNode
	{
		// Token: 0x060062D6 RID: 25302 RVA: 0x0024B3A7 File Offset: 0x002497A7
		public JSONData(string aData)
		{
			this.m_Data = aData;
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x0024B3B6 File Offset: 0x002497B6
		public JSONData(float aData)
		{
			this.AsFloat = aData;
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x0024B3C5 File Offset: 0x002497C5
		public JSONData(double aData)
		{
			this.AsDouble = aData;
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x0024B3D4 File Offset: 0x002497D4
		public JSONData(bool aData)
		{
			this.AsBool = aData;
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x0024B3E3 File Offset: 0x002497E3
		public JSONData(int aData)
		{
			this.AsInt = aData;
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x060062DB RID: 25307 RVA: 0x0024B3F2 File Offset: 0x002497F2
		// (set) Token: 0x060062DC RID: 25308 RVA: 0x0024B3FA File Offset: 0x002497FA
		public override string Value
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x0024B403 File Offset: 0x00249803
		public override string ToString()
		{
			return "\"" + JSONNode.Escape(this.m_Data) + "\"";
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x0024B41F File Offset: 0x0024981F
		public override string ToString(string aPrefix)
		{
			return "\"" + JSONNode.Escape(this.m_Data) + "\"";
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x0024B43B File Offset: 0x0024983B
		public override void ToString(string aPrefix, StringBuilder sb)
		{
			sb.Append("\"" + JSONNode.Escape(this.m_Data) + "\"");
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x0024B460 File Offset: 0x00249860
		public override void Serialize(BinaryWriter aWriter)
		{
			JSONData jsondata = new JSONData(string.Empty);
			jsondata.AsInt = this.AsInt;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write((byte)4);
				aWriter.Write(this.AsInt);
				return;
			}
			jsondata.AsFloat = this.AsFloat;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write((byte)7);
				aWriter.Write(this.AsFloat);
				return;
			}
			jsondata.AsDouble = this.AsDouble;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write((byte)5);
				aWriter.Write(this.AsDouble);
				return;
			}
			jsondata.AsBool = this.AsBool;
			if (jsondata.m_Data == this.m_Data)
			{
				aWriter.Write((byte)6);
				aWriter.Write(this.AsBool);
				return;
			}
			aWriter.Write((byte)3);
			aWriter.Write(this.m_Data);
		}

		// Token: 0x040051D0 RID: 20944
		private string m_Data;
	}
}
