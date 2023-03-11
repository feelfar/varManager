using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000CCA RID: 3274
	public class JSONClass : JSONNode, IEnumerable
	{
		// Token: 0x17000E93 RID: 3731
		public override JSONNode this[string aKey]
		{
			get
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					return this.m_Dict[aKey];
				}
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					this.m_Dict[aKey] = value;
				}
				else
				{
					this.m_Dict.Add(aKey, value);
				}
			}
		}

		// Token: 0x17000E94 RID: 3732
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return null;
				}
				return this.m_Dict.ElementAt(aIndex).Value;
			}
			set
			{
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return;
				}
				string key = this.m_Dict.ElementAt(aIndex).Key;
				this.m_Dict[key] = value;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x060062C9 RID: 25289 RVA: 0x0024AA20 File Offset: 0x00248E20
		public override int Count
		{
			get
			{
				return this.m_Dict.Count;
			}
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x0024AA2D File Offset: 0x00248E2D
		public bool HasKey(string aKey)
		{
			return this.m_Dict.ContainsKey(aKey);
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x0024AA3C File Offset: 0x00248E3C
		public override void Add(string aKey, JSONNode aItem)
		{
			if (!string.IsNullOrEmpty(aKey))
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					this.m_Dict[aKey] = aItem;
				}
				else
				{
					this.m_Dict.Add(aKey, aItem);
				}
			}
			else
			{
				this.m_Dict.Add(Guid.NewGuid().ToString(), aItem);
			}
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x0024AAA8 File Offset: 0x00248EA8
		public override JSONNode Remove(string aKey)
		{
			if (!this.m_Dict.ContainsKey(aKey))
			{
				return null;
			}
			JSONNode result = this.m_Dict[aKey];
			this.m_Dict.Remove(aKey);
			return result;
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x0024AAE4 File Offset: 0x00248EE4
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_Dict.Count)
			{
				return null;
			}
			KeyValuePair<string, JSONNode> keyValuePair = this.m_Dict.ElementAt(aIndex);
			this.m_Dict.Remove(keyValuePair.Key);
			return keyValuePair.Value;
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x0024AB34 File Offset: 0x00248F34
		public override JSONNode Remove(JSONNode aNode)
		{
			JSONNode result;
			try
			{
				KeyValuePair<string, JSONNode> keyValuePair = (from k in this.m_Dict
				where k.Value == aNode
				select k).First<KeyValuePair<string, JSONNode>>();
				this.m_Dict.Remove(keyValuePair.Key);
				result = aNode;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x060062CF RID: 25295 RVA: 0x0024ABA4 File Offset: 0x00248FA4
		public IEnumerable<string> Keys
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> N in this.m_Dict)
				{
					yield return N.Key;
				}
				yield break;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x060062D0 RID: 25296 RVA: 0x0024ABC8 File Offset: 0x00248FC8
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> N in this.m_Dict)
				{
					yield return N.Value;
				}
				yield break;
			}
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x0024ABEC File Offset: 0x00248FEC
		public IEnumerator GetEnumerator()
		{
			foreach (KeyValuePair<string, JSONNode> N in this.m_Dict)
			{
				yield return N;
			}
			yield break;
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x0024AC08 File Offset: 0x00249008
		public override string ToString()
		{
			string text = "{";
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\"",
					JSONNode.Escape(keyValuePair.Key),
					"\":",
					keyValuePair.Value.ToString()
				});
			}
			text += "}";
			return text;
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x0024ACC8 File Offset: 0x002490C8
		public override string ToString(string aPrefix)
		{
			string text = "{ ";
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					"\"",
					JSONNode.Escape(keyValuePair.Key),
					"\" : ",
					keyValuePair.Value.ToString(aPrefix + "   ")
				});
			}
			text = text + "\n" + aPrefix + "}";
			return text;
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x0024ADAC File Offset: 0x002491AC
		public override void ToString(string aPrefix, StringBuilder sb)
		{
			bool flag = true;
			sb.Append("{ ");
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (!flag)
				{
					sb.Append(", ");
				}
				flag = false;
				sb.Append("\n" + aPrefix + "   ");
				sb.Append("\"" + JSONNode.Escape(keyValuePair.Key) + "\" : ");
				keyValuePair.Value.ToString(aPrefix + "   ", sb);
			}
			sb.Append("\n" + aPrefix + "}");
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x0024AE8C File Offset: 0x0024928C
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write((byte)2);
			aWriter.Write(this.m_Dict.Count);
			foreach (string text in this.m_Dict.Keys)
			{
				aWriter.Write(text);
				this.m_Dict[text].Serialize(aWriter);
			}
		}

		// Token: 0x040051CF RID: 20943
		private Dictionary<string, JSONNode> m_Dict = new Dictionary<string, JSONNode>();
	}
}
