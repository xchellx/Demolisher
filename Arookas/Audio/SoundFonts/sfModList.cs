using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000050 RID: 80
	internal struct sfModList
	{
		// Token: 0x0600023B RID: 571 RVA: 0x000070FE File Offset: 0x000052FE
		public sfModList(ABinaryReader binaryReader)
		{
			this.sfModSrcOper = binaryReader.Read16();
			this.sfModDestOper = binaryReader.Read16();
			this.modAmount = binaryReader.ReadS16();
			this.sfModAmtSrcOper = binaryReader.Read16();
			this.sfModTransOper = (SFTransform)binaryReader.Read16();
		}

		// Token: 0x0400012A RID: 298
		public ushort sfModSrcOper;

		// Token: 0x0400012B RID: 299
		public ushort sfModDestOper;

		// Token: 0x0400012C RID: 300
		public short modAmount;

		// Token: 0x0400012D RID: 301
		public ushort sfModAmtSrcOper;

		// Token: 0x0400012E RID: 302
		public SFTransform sfModTransOper;
	}
}
