using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000005 RID: 5
	public sealed class ProgramChangeEvent : ChannelEvent
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000268A File Offset: 0x0000088A
		public int ProgramNumber
		{
			get
			{
				return this.programNumber;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002692 File Offset: 0x00000892
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.ProgramChange;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002699 File Offset: 0x00000899
		public ProgramChangeEvent(int channelNumber, int programNumber) : this(0UL, 0UL, channelNumber, programNumber)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026A7 File Offset: 0x000008A7
		public ProgramChangeEvent(ulong deltaTime, int channelNumber, int programNumber) : this(0UL, deltaTime, channelNumber, programNumber)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026B4 File Offset: 0x000008B4
		public ProgramChangeEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int programNumber) : base(absoluteTime, deltaTime, channelNumber)
		{
			if (programNumber < 0 || programNumber > 127)
			{
				throw new ArgumentOutOfRangeException("programNumber", programNumber, "The specified program number was negative or greater than 127.");
			}
			this.programNumber = programNumber;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026E9 File Offset: 0x000008E9
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new ProgramChangeEvent(absoluteTime, deltaTime, base.ChannelNumber, this.programNumber);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026FE File Offset: 0x000008FE
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002708 File Offset: 0x00000908
		public override void ToStream(Stream stream, bool runningStatus)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (runningStatus)
			{
				abinaryWriter.Write8((byte)((int)this.Type | base.ChannelNumber));
			}
			abinaryWriter.Write8((byte)this.programNumber);
		}

		// Token: 0x04000005 RID: 5
		private int programNumber;
	}
}
