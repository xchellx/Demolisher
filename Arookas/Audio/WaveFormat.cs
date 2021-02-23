using System;

namespace Arookas.Audio
{
	// Token: 0x02000064 RID: 100
	public enum WaveFormat : ushort
	{
		// Token: 0x040001A1 RID: 417
		PCM = 1,
		// Token: 0x040001A2 RID: 418
		IEEEFloatingPoint = 3,
		// Token: 0x040001A3 RID: 419
		Alaw = 6,
		// Token: 0x040001A4 RID: 420
		Mulaw,
		// Token: 0x040001A5 RID: 421
		Extensible = 65534
	}
}
