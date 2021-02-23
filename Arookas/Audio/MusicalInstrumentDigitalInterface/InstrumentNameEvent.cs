using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200000F RID: 15
	public sealed class InstrumentNameEvent : TextEvent
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E20 File Offset: 0x00001020
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.InstrumentName;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E23 File Offset: 0x00001023
		public InstrumentNameEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E30 File Offset: 0x00001030
		public InstrumentNameEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E3C File Offset: 0x0000103C
		public InstrumentNameEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime, text)
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E47 File Offset: 0x00001047
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new InstrumentNameEvent(absoluteTime, deltaTime, base.Text);
		}
	}
}
