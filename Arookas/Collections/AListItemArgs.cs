using System;

namespace Arookas.Collections
{
	// Token: 0x02000067 RID: 103
	public sealed class AListItemArgs<T> : EventArgs
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		public T Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public AListItemArgs(T item, int index)
		{
			this.item = item;
			this.index = index;
		}

		// Token: 0x040001AE RID: 430
		private readonly T item;

		// Token: 0x040001AF RID: 431
		private readonly int index;
	}
}
