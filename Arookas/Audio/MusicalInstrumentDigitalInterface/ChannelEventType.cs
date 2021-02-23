using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200002B RID: 43
	public enum ChannelEventType : byte
	{
		// Token: 0x04000067 RID: 103
		NoteOff = 128,
		// Token: 0x04000068 RID: 104
		NoteOn = 144,
		// Token: 0x04000069 RID: 105
		NoteAftertouch = 160,
		// Token: 0x0400006A RID: 106
		Controller = 176,
		// Token: 0x0400006B RID: 107
		ProgramChange = 192,
		// Token: 0x0400006C RID: 108
		ChannelAftertouch = 208,
		// Token: 0x0400006D RID: 109
		PitchBend = 224
	}
}
