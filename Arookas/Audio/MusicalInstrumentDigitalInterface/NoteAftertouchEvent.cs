using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000008 RID: 8
	public sealed class NoteAftertouchEvent : ChannelEvent
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000292A File Offset: 0x00000B2A
		public int NoteNumber
		{
			get
			{
				return this.noteNumber;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002932 File Offset: 0x00000B32
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.NoteAftertouch;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002939 File Offset: 0x00000B39
		public int Pressure
		{
			get
			{
				return this.pressure;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002941 File Offset: 0x00000B41
		public NoteAftertouchEvent(int channelNumber, int noteNumber, int pressure) : this(0UL, 0UL, channelNumber, noteNumber, pressure)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002950 File Offset: 0x00000B50
		public NoteAftertouchEvent(ulong deltaTime, int channelNumber, int noteNumber, int pressure) : this(0UL, deltaTime, channelNumber, noteNumber, pressure)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002960 File Offset: 0x00000B60
		public NoteAftertouchEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int noteNumber, int pressure) : base(absoluteTime, deltaTime, channelNumber)
		{
			if (noteNumber < 0 || noteNumber > 127)
			{
				throw new ArgumentOutOfRangeException("noteNumber", noteNumber, "The specified note number was negative or greater than 127.");
			}
			if (pressure < 0 || pressure > 127)
			{
				throw new ArgumentOutOfRangeException("pressure", pressure, "The specified pressure was negative or greater than 127.");
			}
			this.noteNumber = noteNumber;
			this.pressure = pressure;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029CA File Offset: 0x00000BCA
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new NoteAftertouchEvent(absoluteTime, deltaTime, base.ChannelNumber, this.noteNumber, this.pressure);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029E5 File Offset: 0x00000BE5
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029F0 File Offset: 0x00000BF0
		public override void ToStream(Stream stream, bool runningStatus)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (runningStatus)
			{
				abinaryWriter.Write8((byte)((int)this.Type | base.ChannelNumber));
			}
			abinaryWriter.Write8((byte)this.noteNumber);
			abinaryWriter.Write8((byte)this.pressure);
		}

		// Token: 0x04000009 RID: 9
		private int noteNumber;

		// Token: 0x0400000A RID: 10
		private int pressure;
	}
}
