using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Arookas.Collections
{
	// Token: 0x02000022 RID: 34
	[DebuggerDisplay("Count = {Count}")]
	public class ADictionary<TKey, TValue>
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000038D5 File Offset: 0x00001AD5
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000038E2 File Offset: 0x00001AE2
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000038EF File Offset: 0x00001AEF
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x1700003F RID: 63
		public TValue this[TKey key]
		{
			get
			{
				TValue result;
				if (!this.dictionary.TryGetValue(key, out result))
				{
					return default(TValue);
				}
				return result;
			}
			set
			{
				this.dictionary[key] = value;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003933 File Offset: 0x00001B33
		public ADictionary()
		{
			this.dictionary = new Dictionary<TKey, TValue>();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003946 File Offset: 0x00001B46
		public ADictionary(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = new Dictionary<TKey, TValue>(dictionary);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000395A File Offset: 0x00001B5A
		public ADictionary(IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, TValue>(comparer);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000396E File Offset: 0x00001B6E
		public ADictionary(int capacity)
		{
			this.dictionary = new Dictionary<TKey, TValue>(capacity);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003982 File Offset: 0x00001B82
		public ADictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003997 File Offset: 0x00001B97
		public ADictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			this.dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000039AC File Offset: 0x00001BAC
		public void Add(TKey key, TValue value)
		{
			this.dictionary.Add(key, value);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000039BB File Offset: 0x00001BBB
		public void Clear()
		{
			this.dictionary.Clear();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000039C8 File Offset: 0x00001BC8
		public bool ContainsKey(TKey key)
		{
			return this.dictionary.ContainsKey(key);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000039D6 File Offset: 0x00001BD6
		public bool ContainsValue(TValue value)
		{
			return this.dictionary.ContainsValue(value);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000039E4 File Offset: 0x00001BE4
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000039F1 File Offset: 0x00001BF1
		public bool Remove(TKey key)
		{
			return this.dictionary.Remove(key);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000039FF File Offset: 0x00001BFF
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.dictionary.TryGetValue(key, out value);
		}

		// Token: 0x04000034 RID: 52
		private Dictionary<TKey, TValue> dictionary;
	}
}
