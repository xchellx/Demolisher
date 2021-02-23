using System;
using System.IO;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000003 RID: 3
	public abstract class ChannelEvent : MIDIEvent
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000255B File Offset: 0x0000075B
		public int ChannelNumber
		{
			get
			{
				return this.channelNumber;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12
		public abstract ChannelEventType Type { get; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002563 File Offset: 0x00000763
		public ChannelEvent(int channelNumber) : this(0UL, 0UL, channelNumber)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002570 File Offset: 0x00000770
		public ChannelEvent(ulong deltaTime, int channelNumber) : this(0UL, deltaTime, channelNumber)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000257C File Offset: 0x0000077C
		public ChannelEvent(ulong absoluteTime, ulong deltaTime, int channelNumber) : base(absoluteTime, deltaTime)
		{
			if (channelNumber < 0 || channelNumber > 15)
			{
				throw new ArgumentOutOfRangeException("channelNumber", channelNumber, "The specified channel number was negative or greater than 15.");
			}
			this.channelNumber = channelNumber;
		}

		// Token: 0x06000010 RID: 16
		public abstract void ToStream(Stream stream, bool runningStatus);

		// Token: 0x04000003 RID: 3
		private int channelNumber;
	}
}
