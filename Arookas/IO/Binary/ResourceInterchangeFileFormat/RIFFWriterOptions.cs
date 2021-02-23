using System;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x02000078 RID: 120
	[Flags]
	public enum RIFFWriterOptions
	{
		// Token: 0x040001C5 RID: 453
		Default = 0,
		// Token: 0x040001C6 RID: 454
		SkipHeader = 1,
		// Token: 0x040001C7 RID: 455
		SkipAligment = 2
	}
}
