using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000017 RID: 23
	public sealed class CopyrightNoticeEvent : TextEvent
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000341D File Offset: 0x0000161D
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.CopyrightNotice;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003420 File Offset: 0x00001620
		public CopyrightNoticeEvent(string text) : this(0UL, 0UL, text)
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000342D File Offset: 0x0000162D
		public CopyrightNoticeEvent(ulong deltaTime, string text) : this(0UL, deltaTime, text)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003439 File Offset: 0x00001639
		public CopyrightNoticeEvent(ulong absoluteTime, ulong deltaTime, string text) : base(absoluteTime, deltaTime, text)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003444 File Offset: 0x00001644
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new CopyrightNoticeEvent(absoluteTime, deltaTime, base.Text);
		}
	}
}
