using System;
using System.Collections;
using System.Collections.Generic;

namespace Arookas.Text.Formatting
{
	// Token: 0x020000A1 RID: 161
	internal sealed class FormatTokenCollection : IList<FormatToken>, ICollection<FormatToken>, IEnumerable<FormatToken>, IEnumerable
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001180A File Offset: 0x0000FA0A
		public bool IsGlobalScope
		{
			get
			{
				return this.Parent == null;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00011815 File Offset: 0x0000FA15
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00011818 File Offset: 0x0000FA18
		public int Count
		{
			get
			{
				return this.Tokens.Count;
			}
		}

		// Token: 0x17000163 RID: 355
		public FormatToken this[int index]
		{
			get
			{
				return this.Tokens[index];
			}
			set
			{
				this.Tokens[index] = value;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00011842 File Offset: 0x0000FA42
		public FormatTokenCollection(FormatTokenCollection parent)
		{
			this.Parent = parent;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001185C File Offset: 0x0000FA5C
		public void Add(FormatToken item)
		{
			this.Tokens.Add(item);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001186A File Offset: 0x0000FA6A
		public void AddRange(IEnumerable<FormatToken> collection)
		{
			this.Tokens.AddRange(collection);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00011878 File Offset: 0x0000FA78
		public void Clear()
		{
			this.Tokens.Clear();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00011885 File Offset: 0x0000FA85
		public bool Contains(FormatToken item)
		{
			return this.Tokens.Contains(item);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00011893 File Offset: 0x0000FA93
		public void CopyTo(FormatToken[] array)
		{
			this.Tokens.CopyTo(array);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000118A1 File Offset: 0x0000FAA1
		public void CopyTo(FormatToken[] array, int arrayIndex)
		{
			this.Tokens.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public void CopyTo(int index, FormatToken[] array, int arrayIndex, int count)
		{
			this.Tokens.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000118C2 File Offset: 0x0000FAC2
		public IEnumerator<FormatToken> GetEnumerator()
		{
			return this.Tokens.GetEnumerator();
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000118D4 File Offset: 0x0000FAD4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000118DC File Offset: 0x0000FADC
		public int IndexOf(FormatToken item)
		{
			return this.Tokens.IndexOf(item);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000118EA File Offset: 0x0000FAEA
		public void Insert(int index, FormatToken item)
		{
			this.Tokens.Insert(index, item);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000118F9 File Offset: 0x0000FAF9
		public bool Remove(FormatToken item)
		{
			return this.Tokens.Remove(item);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00011907 File Offset: 0x0000FB07
		public void RemoveAt(int index)
		{
			this.Tokens.RemoveAt(index);
		}

		// Token: 0x0400025A RID: 602
		public readonly FormatTokenCollection Parent;

		// Token: 0x0400025B RID: 603
		private readonly List<FormatToken> Tokens = new List<FormatToken>();
	}
}
