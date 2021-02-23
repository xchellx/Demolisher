using System;
using System.Collections;
using System.Collections.Generic;

namespace Arookas.Collections
{
	// Token: 0x02000069 RID: 105
	public sealed class AReadOnlyArray<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BB0C File Offset: 0x00009D0C
		public int Length
		{
			get
			{
				return this.collection.Length;
			}
		}

		// Token: 0x170000F6 RID: 246
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.collection.Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.collection[index];
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BB3E File Offset: 0x00009D3E
		public AReadOnlyArray(T[] collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.collection = collection;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BB5B File Offset: 0x00009D5B
		public IEnumerator<T> GetEnumerator()
		{
			return this.collection.GetArrayEnumerator<T>();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BB68 File Offset: 0x00009D68
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001B2 RID: 434
		private readonly T[] collection;
	}
}
