using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200000B RID: 11
	public abstract class MetaEvent : MIDIEvent
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004D RID: 77
		public abstract MetaEventType Type { get; }

		// Token: 0x0600004E RID: 78 RVA: 0x00002C72 File Offset: 0x00000E72
		protected MetaEvent() : this(0UL, 0UL)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C7E File Offset: 0x00000E7E
		protected MetaEvent(ulong deltaTime) : this(0UL, deltaTime)
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C89 File Offset: 0x00000E89
		protected MetaEvent(ulong absoluteTime, ulong deltaTime) : base(absoluteTime, deltaTime)
		{
		}
	}
}
