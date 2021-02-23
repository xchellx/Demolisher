using System;
using System.Collections;
using System.Collections.Generic;

namespace Arookas.Collections
{
	// Token: 0x0200006A RID: 106
	public sealed class AReadOnlyList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000BB70 File Offset: 0x00009D70
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x170000F8 RID: 248
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.collection.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.collection[index];
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		public AReadOnlyList(IList<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = collection;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		public bool Contains(T item)
		{
			return this.collection.Contains(item);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000BBD3 File Offset: 0x00009DD3
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.collection.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000BBE2 File Offset: 0x00009DE2
		public IEnumerator<T> GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000BBEF File Offset: 0x00009DEF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000BBF7 File Offset: 0x00009DF7
		public int IndexOf(T item)
		{
			return this.collection.IndexOf(item);
		}

		// Token: 0x040001B3 RID: 435
		private readonly IList<T> collection;
	}
}
