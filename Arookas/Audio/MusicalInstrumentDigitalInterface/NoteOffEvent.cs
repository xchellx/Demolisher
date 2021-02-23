using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200000A RID: 10
	public sealed class NoteOffEvent : ChannelEvent
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002B5A File Offset: 0x00000D5A
		public int NoteNumber
		{
			get
			{
				return this.noteNumber;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B62 File Offset: 0x00000D62
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.NoteOff;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002B69 File Offset: 0x00000D69
		public int Velocity
		{
			get
			{
				return this.velocity;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B71 File Offset: 0x00000D71
		public NoteOffEvent(byte channelNumber, int noteNumber, int velocity) : this(0UL, 0UL, (int)channelNumber, noteNumber, velocity)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B80 File Offset: 0x00000D80
		public NoteOffEvent(ulong deltaTime, int channelNumber, byte noteNumber, int velocity) : this(0UL, deltaTime, channelNumber, (int)noteNumber, velocity)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B90 File Offset: 0x00000D90
		public NoteOffEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int noteNumber, int velocity) : base(absoluteTime, deltaTime, channelNumber)
		{
			if (noteNumber < 0 || noteNumber > 127)
			{
				throw new ArgumentOutOfRangeException("noteNumber", noteNumber, "The specified note number was negative or greater than 127.");
			}
			if (velocity < 0 || velocity > 127)
			{
				throw new ArgumentOutOfRangeException("velocity", velocity, "The specified velocity was negative or greater than 127.");
			}
			this.noteNumber = noteNumber;
			this.velocity = velocity;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BFA File Offset: 0x00000DFA
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new NoteOffEvent(absoluteTime, deltaTime, base.ChannelNumber, this.noteNumber, this.velocity);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C15 File Offset: 0x00000E15
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002C20 File Offset: 0x00000E20
		public override void ToStream(Stream stream, bool runningStatus)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (runningStatus)
			{
				abinaryWriter.Write8((byte)((int)this.Type | base.ChannelNumber));
			}
			abinaryWriter.Write8((byte)this.noteNumber);
			abinaryWriter.Write8((byte)this.velocity);
		}

		// Token: 0x0400000D RID: 13
		private int noteNumber;

		// Token: 0x0400000E RID: 14
		private int velocity;
	}
}
