using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000009 RID: 9
	public sealed class NoteOnEvent : ChannelEvent
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A42 File Offset: 0x00000C42
		public int NoteNumber
		{
			get
			{
				return this.noteNumber;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002A4A File Offset: 0x00000C4A
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.NoteOn;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A51 File Offset: 0x00000C51
		public int Velocity
		{
			get
			{
				return this.velocity;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A59 File Offset: 0x00000C59
		public NoteOnEvent(byte channelNumber, int noteNumber, int velocity) : this(0UL, 0UL, (int)channelNumber, noteNumber, velocity)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A68 File Offset: 0x00000C68
		public NoteOnEvent(ulong deltaTime, int channelNumber, int noteNumber, int velocity) : this(0UL, deltaTime, channelNumber, noteNumber, velocity)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A78 File Offset: 0x00000C78
		public NoteOnEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int noteNumber, int velocity) : base(absoluteTime, deltaTime, channelNumber)
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

		// Token: 0x06000041 RID: 65 RVA: 0x00002AE2 File Offset: 0x00000CE2
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new NoteOnEvent(absoluteTime, deltaTime, base.ChannelNumber, this.noteNumber, this.velocity);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AFD File Offset: 0x00000CFD
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B08 File Offset: 0x00000D08
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

		// Token: 0x0400000B RID: 11
		private int noteNumber;

		// Token: 0x0400000C RID: 12
		private int velocity;
	}
}
