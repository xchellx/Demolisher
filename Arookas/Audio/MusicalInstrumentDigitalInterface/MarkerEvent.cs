using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000011 RID: 17
	public sealed class MarkerEvent : TextEvent
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E8C File Offset: 0x0000108C
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.Marker;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002E8F File Offset: 0x0000108F
		public MarkerEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002E9C File Offset: 0x0000109C
		public MarkerEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002EA8 File Offset: 0x000010A8
		public MarkerEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime, text)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002EB3 File Offset: 0x000010B3
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new MarkerEvent(absoluteTime, deltaTime, base.Text);
		}
	}
}
