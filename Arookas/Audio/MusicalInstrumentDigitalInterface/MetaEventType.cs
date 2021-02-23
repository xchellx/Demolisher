using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200002A RID: 42
	public enum MetaEventType : byte
	{
		// Token: 0x04000057 RID: 87
		SequenceNumber,
		// Token: 0x04000058 RID: 88
		Text,
		// Token: 0x04000059 RID: 89
		CopyrightNotice,
		// Token: 0x0400005A RID: 90
		Name,
		// Token: 0x0400005B RID: 91
		InstrumentName,
		// Token: 0x0400005C RID: 92
		Lyrics,
		// Token: 0x0400005D RID: 93
		Marker,
		// Token: 0x0400005E RID: 94
		CuePoint,
		// Token: 0x0400005F RID: 95
		ChannelPrefix = 32,
		// Token: 0x04000060 RID: 96
		EndOfTrack = 47,
		// Token: 0x04000061 RID: 97
		TempoChange = 81,
		// Token: 0x04000062 RID: 98
		SMPTEOffset = 84,
		// Token: 0x04000063 RID: 99
		TimeSignature = 88,
		// Token: 0x04000064 RID: 100
		KeySignature,
		// Token: 0x04000065 RID: 101
		SequencerSpecific = 127
	}
}
