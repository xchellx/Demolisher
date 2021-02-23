using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000055 RID: 85
	internal struct sfInst
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00007209 File Offset: 0x00005409
		public sfInst(ABinaryReader binaryReader)
		{
			this.achInstName = binaryReader.ReadClampedString(20);
			this.wInstBagNdx = binaryReader.Read16();
		}

		// Token: 0x04000140 RID: 320
		public string achInstName;

		// Token: 0x04000141 RID: 321
		public ushort wInstBagNdx;
	}
}
