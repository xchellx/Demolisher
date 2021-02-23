using System;
using System.Collections.Generic;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x0200008A RID: 138
	public sealed class RIFFList : RIFFItem
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000DF54 File Offset: 0x0000C154
		public RIFFItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000DF5C File Offset: 0x0000C15C
		internal RIFFList(RIFF parentingRIFF, string identification, uint length, IEnumerable<RIFFItem> items) : base(parentingRIFF, identification, length)
		{
			this.items = new RIFFItemCollection(items);
		}

		// Token: 0x040001E6 RID: 486
		private RIFFItemCollection items;
	}
}
