using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000CC9 RID: 3273
	public class JSONArray : JSONNode, IEnumerable
	{
		// Token: 0x17000E8F RID: 3727
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					return new JSONLazyCreator(this);
				}
				return this.m_List[aIndex];
			}
			set
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					this.m_List.Add(value);
				}
				else
				{
					this.m_List[aIndex] = value;
				}
			}
		}

		// Token: 0x17000E90 RID: 3728
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.m_List.Add(value);
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x060062BA RID: 25274 RVA: 0x0024A356 File Offset: 0x00248756
		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x0024A363 File Offset: 0x00248763
		public override void Add(string aKey, JSONNode aItem)
		{
			this.m_List.Add(aItem);
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x0024A374 File Offset: 0x00248774
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_List.Count)
			{
				return null;
			}
			JSONNode result = this.m_List[aIndex];
			this.m_List.RemoveAt(aIndex);
			return result;
		}

		// Token: 0x060062BD RID: 25277 RVA: 0x0024A3B5 File Offset: 0x002487B5
		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x060062BE RID: 25278 RVA: 0x0024A3C8 File Offset: 0x002487C8
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (JSONNode N in this.m_List)
				{
					yield return N;
				}
				yield break;
			}
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x0024A3EC File Offset: 0x002487EC
		public IEnumerator GetEnumerator()
		{
			foreach (JSONNode N in this.m_List)
			{
				yield return N;
			}
			yield break;
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x0024A408 File Offset: 0x00248808
		public override string ToString()
		{
			string text = "[ ";
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				text += jsonnode.ToString();
			}
			text += " ]";
			return text;
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0024A498 File Offset: 0x00248898
		public override string ToString(string aPrefix)
		{
			string text = "[ ";
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				text += jsonnode.ToString(aPrefix + "   ");
			}
			text = text + "\n" + aPrefix + "]";
			return text;
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0024A548 File Offset: 0x00248948
		public override void ToString(string aPrefix, StringBuilder sb)
		{
			sb.Append("[ ");
			bool flag = true;
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (!flag)
				{
					sb.Append(", ");
				}
				flag = false;
				sb.Append("\n" + aPrefix + "   ");
				jsonnode.ToString(aPrefix + "   ", sb);
			}
			sb.Append("\n" + aPrefix + "]");
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x0024A600 File Offset: 0x00248A00
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write((byte)1);
			aWriter.Write(this.m_List.Count);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				this.m_List[i].Serialize(aWriter);
			}
		}

		// Token: 0x040051CE RID: 20942
		private List<JSONNode> m_List = new List<JSONNode>();
	}
}
