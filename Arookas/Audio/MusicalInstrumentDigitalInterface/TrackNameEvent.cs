using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000016 RID: 22
	public sealed class TrackNameEvent : TextEvent
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000033E7 File Offset: 0x000015E7
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.Name;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000033EA File Offset: 0x000015EA
		public TrackNameEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000033F7 File Offset: 0x000015F7
		public TrackNameEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003403 File Offset: 0x00001603
		public TrackNameEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime, text)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000340E File Offset: 0x0000160E
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new TrackNameEvent(absoluteTime, deltaTime, base.Text);
		}
	}
}
