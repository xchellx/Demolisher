using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200000E RID: 14
	public sealed class LyricsEvent : TextEvent
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002DEA File Offset: 0x00000FEA
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.Lyrics;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002DED File Offset: 0x00000FED
		public LyricsEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002DFA File Offset: 0x00000FFA
		public LyricsEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002E06 File Offset: 0x00001006
		public LyricsEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime, text)
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E11 File Offset: 0x00001011
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new LyricsEvent(absoluteTime, deltaTime, base.Text);
		}
	}
}
