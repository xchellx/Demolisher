using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000056 RID: 86
	internal struct sfPresetBag
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00007225 File Offset: 0x00005425
		public sfPresetBag(ABinaryReader binaryReader)
		{
			this.wGenNdx = binaryReader.Read16();
			this.wModNdx = binaryReader.Read16();
		}

		// Token: 0x04000142 RID: 322
		public ushort wGenNdx;

		// Token: 0x04000143 RID: 323
		public ushort wModNdx;
	}
}
