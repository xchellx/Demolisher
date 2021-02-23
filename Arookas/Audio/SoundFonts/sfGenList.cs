using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000051 RID: 81
	internal struct sfGenList
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000713C File Offset: 0x0000533C
		public sfGenList(ABinaryReader binaryReader)
		{
			this.sfGenOper = (SFGenerator)binaryReader.Read16();
			this.genAmount = default(genAmountType);
			this.genAmount.wAmount = binaryReader.Read16();
		}

		// Token: 0x0400012F RID: 303
		public SFGenerator sfGenOper;

		// Token: 0x04000130 RID: 304
		public genAmountType genAmount;
	}
}
