using System;
using System.Text;

namespace SimpleJSON
{
	// Token: 0x02000CCC RID: 3276
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x060062E1 RID: 25313 RVA: 0x0024B563 File Offset: 0x00249963
		public JSONLazyCreator(JSONNode aNode)
		{
			this.m_Node = aNode;
			this.m_Key = null;
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x0024B579 File Offset: 0x00249979
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
			this.m_Node = aNode;
			this.m_Key = aKey;
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x0024B58F File Offset: 0x0024998F
		private void Set(JSONNode aVal)
		{
			if (this.m_Key == null)
			{
				this.m_Node.Add(aVal);
			}
			else
			{
				this.m_Node.Add(this.m_Key, aVal);
			}
			this.m_Node = null;
		}

		// Token: 0x17000E99 RID: 3737
		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.Set(new JSONArray
				{
					value
				});
			}
		}

		// Token: 0x17000E9A RID: 3738
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				this.Set(new JSONClass
				{
					{
						aKey,
						value
					}
				});
			}
		}

		// Token: 0x060062E8 RID: 25320 RVA: 0x0024B620 File Offset: 0x00249A20
		public override void Add(JSONNode aItem)
		{
			this.Set(new JSONArray
			{
				aItem
			});
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x0024B644 File Offset: 0x00249A44
		public override void Add(string aKey, JSONNode aItem)
		{
			this.Set(new JSONClass
			{
				{
					aKey,
					aItem
				}
			});
		}

		// Token: 0x060062EA RID: 25322 RVA: 0x0024B666 File Offset: 0x00249A66
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return b == null || object.ReferenceEquals(a, b);
		}

		// Token: 0x060062EB RID: 25323 RVA: 0x0024B677 File Offset: 0x00249A77
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return !(a == b);
		}

		// Token: 0x060062EC RID: 25324 RVA: 0x0024B683 File Offset: 0x00249A83
		public override bool Equals(object obj)
		{
			return obj == null || object.ReferenceEquals(this, obj);
		}

		// Token: 0x060062ED RID: 25325 RVA: 0x0024B694 File Offset: 0x00249A94
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060062EE RID: 25326 RVA: 0x0024B69C File Offset: 0x00249A9C
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x060062EF RID: 25327 RVA: 0x0024B6A3 File Offset: 0x00249AA3
		public override string ToString(string aPrefix)
		{
			return string.Empty;
		}

		// Token: 0x060062F0 RID: 25328 RVA: 0x0024B6AA File Offset: 0x00249AAA
		public override void ToString(string aPrefix, StringBuilder sb)
		{
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060062F1 RID: 25329 RVA: 0x0024B6AC File Offset: 0x00249AAC
		// (set) Token: 0x060062F2 RID: 25330 RVA: 0x0024B6C8 File Offset: 0x00249AC8
		public override int AsInt
		{
			get
			{
				JSONData aVal = new JSONData(0);
				this.Set(aVal);
				return 0;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060062F3 RID: 25331 RVA: 0x0024B6E4 File Offset: 0x00249AE4
		// (set) Token: 0x060062F4 RID: 25332 RVA: 0x0024B708 File Offset: 0x00249B08
		public override float AsFloat
		{
			get
			{
				JSONData aVal = new JSONData(0f);
				this.Set(aVal);
				return 0f;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x0024B724 File Offset: 0x00249B24
		// (set) Token: 0x060062F6 RID: 25334 RVA: 0x0024B750 File Offset: 0x00249B50
		public override double AsDouble
		{
			get
			{
				JSONData aVal = new JSONData(0.0);
				this.Set(aVal);
				return 0.0;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x060062F7 RID: 25335 RVA: 0x0024B76C File Offset: 0x00249B6C
		// (set) Token: 0x060062F8 RID: 25336 RVA: 0x0024B788 File Offset: 0x00249B88
		public override bool AsBool
		{
			get
			{
				JSONData aVal = new JSONData(false);
				this.Set(aVal);
				return false;
			}
			set
			{
				JSONData aVal = new JSONData(value);
				this.Set(aVal);
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x060062F9 RID: 25337 RVA: 0x0024B7A4 File Offset: 0x00249BA4
		public override JSONArray AsArray
		{
			get
			{
				JSONArray jsonarray = new JSONArray();
				this.Set(jsonarray);
				return jsonarray;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060062FA RID: 25338 RVA: 0x0024B7C0 File Offset: 0x00249BC0
		public override JSONClass AsObject
		{
			get
			{
				JSONClass jsonclass = new JSONClass();
				this.Set(jsonclass);
				return jsonclass;
			}
		}

		// Token: 0x040051D1 RID: 20945
		private JSONNode m_Node;

		// Token: 0x040051D2 RID: 20946
		private string m_Key;
	}
}
