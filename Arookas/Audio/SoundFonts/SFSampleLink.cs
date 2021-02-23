using System;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000058 RID: 88
	internal enum SFSampleLink : ushort
	{
		// Token: 0x0400014C RID: 332
		Mono = 1,
		// Token: 0x0400014D RID: 333
		Right,
		// Token: 0x0400014E RID: 334
		Left = 4,
		// Token: 0x0400014F RID: 335
		Linked = 8,
		// Token: 0x04000150 RID: 336
		MonoROM = 32769,
		// Token: 0x04000151 RID: 337
		RightROM,
		// Token: 0x04000152 RID: 338
		LeftROM = 32772,
		// Token: 0x04000153 RID: 339
		LinkedROM = 32776
	}
}
