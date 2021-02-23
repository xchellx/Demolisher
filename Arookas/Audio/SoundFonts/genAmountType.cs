using System;
using System.Runtime.InteropServices;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x0200004E RID: 78
	[StructLayout(LayoutKind.Explicit)]
	internal struct genAmountType
	{
		// Token: 0x040000F0 RID: 240
		[FieldOffset(0)]
		public byte byLo;

		// Token: 0x040000F1 RID: 241
		[FieldOffset(1)]
		public byte byHi;

		// Token: 0x040000F2 RID: 242
		[FieldOffset(0)]
		public short shAmount;

		// Token: 0x040000F3 RID: 243
		[FieldOffset(0)]
		public ushort wAmount;
	}
}
