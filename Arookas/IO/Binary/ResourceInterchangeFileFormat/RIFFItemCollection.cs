using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x02000089 RID: 137
	public sealed class RIFFItemCollection : IEnumerable<RIFFItem>, IEnumerable
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000DE20 File Offset: 0x0000C020
		public IEnumerable<RIFFChunk> Chunks
		{
			get
			{
				List<RIFFChunk> list = new List<RIFFChunk>(this.items.Length);
				foreach (RIFFItem riffitem in this)
				{
					RIFFChunk riffchunk = riffitem as RIFFChunk;
					if (riffchunk != null)
					{
						list.Add(riffchunk);
					}
				}
				return list;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000DE84 File Offset: 0x0000C084
		public IEnumerable<RIFFList> Lists
		{
			get
			{
				List<RIFFList> list = new List<RIFFList>(this.items.Length);
				foreach (RIFFItem riffitem in this)
				{
					RIFFList rifflist = riffitem as RIFFList;
					if (rifflist != null)
					{
						list.Add(rifflist);
					}
				}
				return list;
			}
		}

		// Token: 0x1700012D RID: 301
		public RIFFItem this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		internal RIFFItemCollection(IEnumerable<RIFFItem> items)
		{
			int num = items.Count<RIFFItem>();
			this.items = new RIFFItem[num];
			for (int i = 0; i < items.Count<RIFFItem>(); i++)
			{
				this.items[i] = items.ElementAt(i);
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000DF3A File Offset: 0x0000C13A
		public IEnumerator<RIFFItem> GetEnumerator()
		{
			return ((IEnumerable<RIFFItem>)this.items).GetEnumerator();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000DF4C File Offset: 0x0000C14C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001E5 RID: 485
		private RIFFItem[] items;
	}
}
