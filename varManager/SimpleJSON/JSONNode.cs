using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000CC8 RID: 3272
	public class JSONNode
	{
		// Token: 0x06006284 RID: 25220 RVA: 0x002495C2 File Offset: 0x002479C2
		public virtual void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x17000E83 RID: 3715
		public virtual JSONNode this[int aIndex]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000E84 RID: 3716
		public virtual JSONNode this[string aKey]
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x002495CE File Offset: 0x002479CE
		// (set) Token: 0x0600628A RID: 25226 RVA: 0x002495D5 File Offset: 0x002479D5
		public virtual string Value
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600628B RID: 25227 RVA: 0x002495D7 File Offset: 0x002479D7
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x002495DA File Offset: 0x002479DA
		public virtual void Add(JSONNode aItem)
		{
			this.Add(string.Empty, aItem);
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x002495E8 File Offset: 0x002479E8
		public virtual JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x002495EB File Offset: 0x002479EB
		public virtual JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x002495EE File Offset: 0x002479EE
		public virtual JSONNode Remove(JSONNode aNode)
		{
			return aNode;
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06006290 RID: 25232 RVA: 0x002495F4 File Offset: 0x002479F4
		public virtual IEnumerable<JSONNode> Childs
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06006291 RID: 25233 RVA: 0x00249610 File Offset: 0x00247A10
		public IEnumerable<JSONNode> DeepChilds
		{
			get
			{
				foreach (JSONNode C in this.Childs)
				{
					foreach (JSONNode D in C.DeepChilds)
					{
						yield return D;
					}
				}
				yield break;
			}
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x00249633 File Offset: 0x00247A33
		public override string ToString()
		{
			return "JSONNode";
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x0024963A File Offset: 0x00247A3A
		public virtual string ToString(string aPrefix)
		{
			return "JSONNode";
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x00249641 File Offset: 0x00247A41
		public virtual void ToString(string aPrefix, StringBuilder sb)
		{
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x00249644 File Offset: 0x00247A44
		// (set) Token: 0x06006296 RID: 25238 RVA: 0x00249668 File Offset: 0x00247A68
		public virtual int AsInt
		{
			get
			{
				int result = 0;
				if (int.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06006297 RID: 25239 RVA: 0x00249680 File Offset: 0x00247A80
		// (set) Token: 0x06006298 RID: 25240 RVA: 0x002496AC File Offset: 0x00247AAC
		public virtual float AsFloat
		{
			get
			{
				float result = 0f;
				if (float.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0f;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06006299 RID: 25241 RVA: 0x002496C4 File Offset: 0x00247AC4
		// (set) Token: 0x0600629A RID: 25242 RVA: 0x002496F8 File Offset: 0x00247AF8
		public virtual double AsDouble
		{
			get
			{
				double result = 0.0;
				if (double.TryParse(this.Value, out result))
				{
					return result;
				}
				return 0.0;
			}
			set
			{
				this.Value = value.ToString();
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600629B RID: 25243 RVA: 0x00249710 File Offset: 0x00247B10
		// (set) Token: 0x0600629C RID: 25244 RVA: 0x00249741 File Offset: 0x00247B41
		public virtual bool AsBool
		{
			get
			{
				bool result = false;
				if (bool.TryParse(this.Value, out result))
				{
					return result;
				}
				return !string.IsNullOrEmpty(this.Value);
			}
			set
			{
				this.Value = ((!value) ? "false" : "true");
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x0600629D RID: 25245 RVA: 0x0024975E File Offset: 0x00247B5E
		public virtual JSONArray AsArray
		{
			get
			{
				return this as JSONArray;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x0600629E RID: 25246 RVA: 0x00249766 File Offset: 0x00247B66
		public virtual JSONClass AsObject
		{
			get
			{
				return this as JSONClass;
			}
		}

		// Token: 0x0600629F RID: 25247 RVA: 0x0024976E File Offset: 0x00247B6E
		public static implicit operator JSONNode(string s)
		{
			return new JSONData(s);
		}

		// Token: 0x060062A0 RID: 25248 RVA: 0x00249776 File Offset: 0x00247B76
		public static implicit operator string(JSONNode d)
		{
			return (!(d == null)) ? d.Value : null;
		}

		// Token: 0x060062A1 RID: 25249 RVA: 0x00249790 File Offset: 0x00247B90
		public static bool operator ==(JSONNode a, object b)
		{
			return (b == null && a is JSONLazyCreator) || object.ReferenceEquals(a, b);
		}

		// Token: 0x060062A2 RID: 25250 RVA: 0x002497AC File Offset: 0x00247BAC
		public static bool operator !=(JSONNode a, object b)
		{
			return !(a == b);
		}

		// Token: 0x060062A3 RID: 25251 RVA: 0x002497B8 File Offset: 0x00247BB8
		public override bool Equals(object obj)
		{
			return object.ReferenceEquals(this, obj);
		}

		// Token: 0x060062A4 RID: 25252 RVA: 0x002497C1 File Offset: 0x00247BC1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060062A5 RID: 25253 RVA: 0x002497CC File Offset: 0x00247BCC
		internal static string Escape(string aText)
		{
			string text = string.Empty;
			if (aText != null)
			{
				foreach (char c in aText)
				{
					switch (c)
					{
					case '\b':
						text += "\\b";
						break;
					case '\t':
						text += "\\t";
						break;
					case '\n':
						text += "\\n";
						break;
					default:
						if (c != '"')
						{
							if (c != '\\')
							{
								text += c;
							}
							else
							{
								text += "\\\\";
							}
						}
						else
						{
							text += "\\\"";
						}
						break;
					case '\f':
						text += "\\f";
						break;
					case '\r':
						text += "\\r";
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x060062A6 RID: 25254 RVA: 0x002498C8 File Offset: 0x00247CC8
		public static JSONNode Parse(string aJSON)
		{
			Stack<JSONNode> stack = new Stack<JSONNode>();
			JSONNode jsonnode = null;
			int i = 0;
			bool flag = false;
			string text = string.Empty;
			string text2 = string.Empty;
			bool flag2 = false;
			while (i < aJSON.Length)
			{
				char c = aJSON[i];
				switch (c)
				{
				case '\t':
					goto IL_342;
				case '\n':
				case '\r':
					break;
				default:
					switch (c)
					{
					case '[':
						if (flag2)
						{
							text += aJSON[i];
							goto IL_47F;
						}
						stack.Push(new JSONArray());
						if (jsonnode != null)
						{
							text2 = text2.Trim();
							if (jsonnode is JSONArray)
							{
								jsonnode.Add(stack.Peek());
							}
							else if (text2 != string.Empty)
							{
								jsonnode.Add(text2, stack.Peek());
							}
						}
						text2 = string.Empty;
						text = string.Empty;
						flag = false;
						jsonnode = stack.Peek();
						goto IL_47F;
					case '\\':
						i++;
						if (flag2)
						{
							char c2 = aJSON[i];
							switch (c2)
							{
							case 'r':
								text += '\r';
								break;
							default:
								if (c2 != 'b')
								{
									if (c2 != 'f')
									{
										if (c2 != 'n')
										{
											text += c2;
										}
										else
										{
											text += '\n';
										}
									}
									else
									{
										text += '\f';
									}
								}
								else
								{
									text += '\b';
								}
								break;
							case 't':
								text += '\t';
								break;
							case 'u':
							{
								string s = aJSON.Substring(i + 1, 4);
								text += (char)int.Parse(s, NumberStyles.AllowHexSpecifier);
								i += 4;
								break;
							}
							}
						}
						goto IL_47F;
					case ']':
						break;
					default:
						switch (c)
						{
						case ' ':
							goto IL_342;
						default:
							switch (c)
							{
							case '{':
								if (flag2)
								{
									text += aJSON[i];
									goto IL_47F;
								}
								stack.Push(new JSONClass());
								if (jsonnode != null)
								{
									text2 = text2.Trim();
									if (jsonnode is JSONArray)
									{
										jsonnode.Add(stack.Peek());
									}
									else if (text2 != string.Empty)
									{
										jsonnode.Add(text2, stack.Peek());
									}
								}
								text2 = string.Empty;
								text = string.Empty;
								flag = false;
								jsonnode = stack.Peek();
								goto IL_47F;
							default:
								if (c != ',')
								{
									if (c != ':')
									{
										text += aJSON[i];
										flag = true;
										goto IL_47F;
									}
									if (flag2)
									{
										text += aJSON[i];
										goto IL_47F;
									}
									text2 = text;
									text = string.Empty;
									flag = false;
									goto IL_47F;
								}
								else
								{
									if (flag2)
									{
										text += aJSON[i];
										goto IL_47F;
									}
									if (flag)
									{
										if (jsonnode is JSONArray)
										{
											jsonnode.Add(text);
										}
										else if (text2 != string.Empty)
										{
											jsonnode.Add(text2, text);
										}
									}
									text2 = string.Empty;
									text = string.Empty;
									flag = false;
									goto IL_47F;
								}
								break;
							case '}':
								break;
							}
							break;
						case '"':
							flag2 ^= true;
							flag = true;
							goto IL_47F;
						}
						break;
					}
					if (flag2)
					{
						text += aJSON[i];
					}
					else
					{
						if (stack.Count == 0)
						{
							throw new Exception("JSON Parse: Too many closing brackets");
						}
						stack.Pop();
						if (flag)
						{
							text2 = text2.Trim();
							if (jsonnode is JSONArray)
							{
								jsonnode.Add(text);
							}
							else if (text2 != string.Empty)
							{
								jsonnode.Add(text2, text);
							}
						}
						text2 = string.Empty;
						text = string.Empty;
						flag = false;
						if (stack.Count > 0)
						{
							jsonnode = stack.Peek();
						}
					}
					break;
				}
				IL_47F:
				i++;
				continue;
				IL_342:
				if (flag2)
				{
					text += aJSON[i];
				}
				goto IL_47F;
			}
			if (flag2)
			{
				throw new Exception("JSON Parse: Quotation marks seems to be messed up.");
			}
			return jsonnode;
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x00249D77 File Offset: 0x00248177
		public virtual void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x060062A8 RID: 25256 RVA: 0x00249D7C File Offset: 0x0024817C
		public void SaveToStream(Stream aData)
		{
			BinaryWriter aWriter = new BinaryWriter(aData);
			this.Serialize(aWriter);
		}

		// Token: 0x060062A9 RID: 25257 RVA: 0x00249D97 File Offset: 0x00248197
		public void SaveToCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060062AA RID: 25258 RVA: 0x00249DA3 File Offset: 0x002481A3
		public void SaveToCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060062AB RID: 25259 RVA: 0x00249DAF File Offset: 0x002481AF
		public string SaveToCompressedBase64()
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x00249DBC File Offset: 0x002481BC
		public void SaveToFile(string aFileName)
		{
			Directory.CreateDirectory(new FileInfo(aFileName).Directory.FullName);
			using (FileStream fileStream = File.OpenWrite(aFileName))
			{
				this.SaveToStream(fileStream);
			}
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x00249E10 File Offset: 0x00248210
		public string SaveToBase64()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.SaveToStream(memoryStream);
				memoryStream.Position = 0L;
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x00249E64 File Offset: 0x00248264
		public static JSONNode Deserialize(BinaryReader aReader)
		{
			JSONBinaryTag jsonbinaryTag = (JSONBinaryTag)aReader.ReadByte();
			switch (jsonbinaryTag)
			{
			case JSONBinaryTag.Array:
			{
				int num = aReader.ReadInt32();
				JSONArray jsonarray = new JSONArray();
				for (int i = 0; i < num; i++)
				{
					jsonarray.Add(JSONNode.Deserialize(aReader));
				}
				return jsonarray;
			}
			case JSONBinaryTag.Class:
			{
				int num2 = aReader.ReadInt32();
				JSONClass jsonclass = new JSONClass();
				for (int j = 0; j < num2; j++)
				{
					string aKey = aReader.ReadString();
					JSONNode aItem = JSONNode.Deserialize(aReader);
					jsonclass.Add(aKey, aItem);
				}
				return jsonclass;
			}
			case JSONBinaryTag.Value:
				return new JSONData(aReader.ReadString());
			case JSONBinaryTag.IntValue:
				return new JSONData(aReader.ReadInt32());
			case JSONBinaryTag.DoubleValue:
				return new JSONData(aReader.ReadDouble());
			case JSONBinaryTag.BoolValue:
				return new JSONData(aReader.ReadBoolean());
			case JSONBinaryTag.FloatValue:
				return new JSONData(aReader.ReadSingle());
			default:
				throw new Exception("Error deserializing JSON. Unknown tag: " + jsonbinaryTag);
			}
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x00249F63 File Offset: 0x00248363
		public static JSONNode LoadFromCompressedFile(string aFileName)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x00249F6F File Offset: 0x0024836F
		public static JSONNode LoadFromCompressedStream(Stream aData)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x00249F7B File Offset: 0x0024837B
		public static JSONNode LoadFromCompressedBase64(string aBase64)
		{
			throw new Exception("Can't use compressed functions. You need include the SharpZipLib and uncomment the define at the top of SimpleJSON");
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x00249F88 File Offset: 0x00248388
		public static JSONNode LoadFromStream(Stream aData)
		{
			JSONNode result;
			using (BinaryReader binaryReader = new BinaryReader(aData))
			{
				result = JSONNode.Deserialize(binaryReader);
			}
			return result;
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x00249FC8 File Offset: 0x002483C8
		public static JSONNode LoadFromFile(string aFileName)
		{
			JSONNode result;
			using (FileStream fileStream = File.OpenRead(aFileName))
			{
				result = JSONNode.LoadFromStream(fileStream);
			}
			return result;
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x0024A008 File Offset: 0x00248408
		public static JSONNode LoadFromBase64(string aBase64)
		{
			byte[] buffer = Convert.FromBase64String(aBase64);
			return JSONNode.LoadFromStream(new MemoryStream(buffer)
			{
				Position = 0L
			});
		}
	}
}
