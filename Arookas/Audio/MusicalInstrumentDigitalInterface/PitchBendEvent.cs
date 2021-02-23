using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000004 RID: 4
	public sealed class PitchBendEvent : ChannelEvent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000025AC File Offset: 0x000007AC
		public int PitchBend
		{
			get
			{
				return this.pitchBend;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000025B4 File Offset: 0x000007B4
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.PitchBend;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000025BB File Offset: 0x000007BB
		public PitchBendEvent(int channelNumber, int pitchBend) : this(0UL, 0UL, channelNumber, pitchBend)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025C9 File Offset: 0x000007C9
		public PitchBendEvent(ulong deltaTime, int channelNumber, int pitchBend) : this(0UL, deltaTime, channelNumber, pitchBend)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025D6 File Offset: 0x000007D6
		public PitchBendEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int pitchBend) : base(absoluteTime, deltaTime, channelNumber)
		{
			if (pitchBend < 0 || pitchBend > 16383)
			{
				throw new ArgumentOutOfRangeException("pitchBend", pitchBend, "The specifed pitch-bend value was negative or greater than 16,383.");
			}
			this.pitchBend = pitchBend;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000260E File Offset: 0x0000080E
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new PitchBendEvent(absoluteTime, deltaTime, base.ChannelNumber, this.pitchBend);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002623 File Offset: 0x00000823
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002630 File Offset: 0x00000830
		public override void ToStream(Stream stream, bool runningStatus)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (runningStatus)
			{
				abinaryWriter.Write8((byte)((int)this.Type | base.ChannelNumber));
			}
			abinaryWriter.Write8((byte)(this.pitchBend & 127));
			abinaryWriter.Write8((byte)(this.pitchBend >> 7 & 127));
		}

		// Token: 0x04000004 RID: 4
		private int pitchBend;
	}
}
