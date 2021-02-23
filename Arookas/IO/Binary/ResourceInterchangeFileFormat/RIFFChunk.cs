using System;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x02000088 RID: 136
	public sealed class RIFFChunk : RIFFItem
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000DDF5 File Offset: 0x0000BFF5
		internal long Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000DDFD File Offset: 0x0000BFFD
		internal RIFFChunk(RIFF parentingRIFF, string identification, uint length, long position) : base(parentingRIFF, identification, length)
		{
			this.position = position;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000DE10 File Offset: 0x0000C010
		public void Goto()
		{
			base.ParentingRIFF.GotoChunk(this);
		}

		// Token: 0x040001E4 RID: 484
		private long position;
	}
}
