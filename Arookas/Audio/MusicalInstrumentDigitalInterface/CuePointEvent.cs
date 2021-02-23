using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000010 RID: 16
	public sealed class CuePointEvent : TextEvent
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002E56 File Offset: 0x00001056
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.CuePoint;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E59 File Offset: 0x00001059
		public CuePointEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E66 File Offset: 0x00001066
		public CuePointEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E72 File Offset: 0x00001072
		public CuePointEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime, text)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E7D File Offset: 0x0000107D
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new CuePointEvent(absoluteTime, deltaTime, base.Text);
		}
	}
}
