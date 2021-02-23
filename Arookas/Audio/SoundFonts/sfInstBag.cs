using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000054 RID: 84
	internal struct sfInstBag
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000071EF File Offset: 0x000053EF
		public sfInstBag(ABinaryReader binaryReader)
		{
			this.wInstGenNdx = binaryReader.Read16();
			this.wInstModNdx = binaryReader.Read16();
		}

		// Token: 0x0400013E RID: 318
		public ushort wInstGenNdx;

		// Token: 0x0400013F RID: 319
		public ushort wInstModNdx;
	}
}
