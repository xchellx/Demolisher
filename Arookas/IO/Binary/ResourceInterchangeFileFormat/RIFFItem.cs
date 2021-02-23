using System;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x02000087 RID: 135
	public abstract class RIFFItem
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000DDB3 File Offset: 0x0000BFB3
		public ABinaryReader BinaryReader
		{
			get
			{
				return this.parentingRIFF.BinaryReader;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		public string Identification
		{
			get
			{
				return this.identification;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		public RIFF ParentingRIFF
		{
			get
			{
				return this.parentingRIFF;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		protected RIFFItem(RIFF parentingRIFF, string identification, uint length)
		{
			this.parentingRIFF = parentingRIFF;
			this.identification = identification;
			this.length = length;
		}

		// Token: 0x040001E1 RID: 481
		private string identification;

		// Token: 0x040001E2 RID: 482
		private uint length;

		// Token: 0x040001E3 RID: 483
		private RIFF parentingRIFF;
	}
}
