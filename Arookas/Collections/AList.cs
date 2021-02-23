using System;
using System.Collections;
using System.Collections.Generic;

namespace Arookas.Collections
{
	// Token: 0x02000066 RID: 102
	public class AList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000B660 File Offset: 0x00009860
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000B66D File Offset: 0x0000986D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002FF RID: 767 RVA: 0x0000B670 File Offset: 0x00009870
		// (remove) Token: 0x06000300 RID: 768 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public event EventHandler<AListItemArgs<T>> ItemAdded;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000301 RID: 769 RVA: 0x0000B6E0 File Offset: 0x000098E0
		// (remove) Token: 0x06000302 RID: 770 RVA: 0x0000B718 File Offset: 0x00009918
		public event EventHandler<AListItemArgs<T>> ItemChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000303 RID: 771 RVA: 0x0000B750 File Offset: 0x00009950
		// (remove) Token: 0x06000304 RID: 772 RVA: 0x0000B788 File Offset: 0x00009988
		public event EventHandler<AListItemArgs<T>> ItemRemoved;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000305 RID: 773 RVA: 0x0000B7C0 File Offset: 0x000099C0
		// (remove) Token: 0x06000306 RID: 774 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public event EventHandler ListCleared;

		// Token: 0x170000F0 RID: 240
		public T this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				T t = this.list[index];
				if (!t.Equals(value))
				{
					this.list[index] = value;
					this.OnItemChanged(value, index);
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B880 File Offset: 0x00009A80
		public AList()
		{
			this.list = new List<T>();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B893 File Offset: 0x00009A93
		public AList(IEnumerable<T> collection)
		{
			this.list = new List<T>(collection);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		public AList(int capacity)
		{
			this.list = new List<T>(capacity);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B8BB File Offset: 0x00009ABB
		public void Add(T item)
		{
			this.list.Add(item);
			this.OnItemAdded(item, this.list.Count - 1);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public void AddRange(IEnumerable<T> collection)
		{
			foreach (T item in collection)
			{
				this.Add(item);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B928 File Offset: 0x00009B28
		public void Clear()
		{
			this.list.Clear();
			this.OnListCleared();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B93B File Offset: 0x00009B3B
		public bool Contains(T item)
		{
			return this.list.Contains(item);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B949 File Offset: 0x00009B49
		public void CopyTo(T[] array)
		{
			this.list.CopyTo(array);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B957 File Offset: 0x00009B57
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.list.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B966 File Offset: 0x00009B66
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this.list.CopyTo(index, array, arrayIndex, count);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B978 File Offset: 0x00009B78
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B98A File Offset: 0x00009B8A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B992 File Offset: 0x00009B92
		public int IndexOf(T item)
		{
			return this.list.IndexOf(item);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		public void Insert(int index, T item)
		{
			this.list.Insert(index, item);
			this.OnItemAdded(item, index);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public bool Remove(T item)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (item.Equals(this.list[i]))
				{
					T item2 = this.list[i];
					this.list.RemoveAt(i);
					this.OnItemRemoved(item2, i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000BA20 File Offset: 0x00009C20
		public void RemoveAt(int index)
		{
			T item = this.list[index];
			this.list.RemoveAt(index);
			this.OnItemRemoved(item, index);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000BA4E File Offset: 0x00009C4E
		private void OnItemAdded(T item, int index)
		{
			if (this.ItemAdded != null)
			{
				this.ItemAdded(this, new AListItemArgs<T>(item, index));
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000BA6B File Offset: 0x00009C6B
		private void OnItemChanged(T item, int index)
		{
			if (this.ItemChanged != null)
			{
				this.ItemChanged(this, new AListItemArgs<T>(item, index));
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000BA88 File Offset: 0x00009C88
		private void OnItemRemoved(T item, int index)
		{
			if (this.ItemRemoved != null)
			{
				this.ItemRemoved(this, new AListItemArgs<T>(item, index));
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		private void OnListCleared()
		{
			if (this.ListCleared != null)
			{
				this.ListCleared(this, EventArgs.Empty);
			}
		}

		// Token: 0x040001A9 RID: 425
		private List<T> list;
	}
}
