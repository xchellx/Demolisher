using System;

namespace Arookas.Collections
{
	// Token: 0x02000068 RID: 104
	internal sealed class AListCapacityArgs : EventArgs
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000BAE6 File Offset: 0x00009CE6
		public int OldCapacity
		{
			get
			{
				return this.oldCapacity;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000BAEE File Offset: 0x00009CEE
		public int NewCapacity
		{
			get
			{
				return this.newCapacity;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000BAF6 File Offset: 0x00009CF6
		public AListCapacityArgs(int oldCapacity, int newCapacity)
		{
			this.oldCapacity = oldCapacity;
			this.newCapacity = newCapacity;
		}

		// Token: 0x040001B0 RID: 432
		private readonly int oldCapacity;

		// Token: 0x040001B1 RID: 433
		private readonly int newCapacity;
	}
}
