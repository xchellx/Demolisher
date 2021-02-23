using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000015 RID: 21
	public sealed class TimeSignatureEvent : MetaEvent
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000321C File Offset: 0x0000141C
		public int Denominator
		{
			get
			{
				return 1 << this.denominatorLogarithm;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003229 File Offset: 0x00001429
		public int DenominatorLogarithm
		{
			get
			{
				return this.denominatorLogarithm;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003231 File Offset: 0x00001431
		public int Numerator
		{
			get
			{
				return this.numerator;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003239 File Offset: 0x00001439
		public int TicksPerMetronomeClick
		{
			get
			{
				return this.ticksPerMetronomeClick;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003241 File Offset: 0x00001441
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003249 File Offset: 0x00001449
		public int ThirtySecondNotesPerQuarterNote
		{
			get
			{
				return this.thirtySecondNotesPerQuarterNote;
			}
			set
			{
				if (value < 1 || value > 255)
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified 32nd notes per quarter note value was negative or greater than 255.");
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000326D File Offset: 0x0000146D
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.TimeSignature;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003271 File Offset: 0x00001471
		public TimeSignatureEvent(int numerator, int denominatorLogarithm, int ticksPerMetronomeClick, int thirtySecondNotesPerQuarterNote) : this(0UL, 0, numerator, denominatorLogarithm, ticksPerMetronomeClick)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000327F File Offset: 0x0000147F
		public TimeSignatureEvent(ulong deltaTime, int numerator, int denominatorLogarithm, int ticksPerMetronomeClick, int thirtySecondNotesPerQuarterNote) : this(0UL, deltaTime, numerator, denominatorLogarithm, ticksPerMetronomeClick, thirtySecondNotesPerQuarterNote)
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003290 File Offset: 0x00001490
		public TimeSignatureEvent(ulong absoluteTime, ulong deltaTime, int numerator, int denominatorLogarithm, int ticksPerMetronomeClick, int thirtySecondNotesPerQuarterNote) : base(absoluteTime, deltaTime)
		{
			if (numerator < 0 || numerator > 255)
			{
				throw new ArgumentOutOfRangeException("numerator", numerator, "The specified numerator was negative or greater than 255.");
			}
			if (denominatorLogarithm < 0 || denominatorLogarithm > 255)
			{
				throw new ArgumentOutOfRangeException("numerator", numerator, "The specified denominator logarithm was negative or greater than 255.");
			}
			if (ticksPerMetronomeClick < 0 || ticksPerMetronomeClick > 255)
			{
				throw new ArgumentOutOfRangeException("numerator", numerator, "The specified ticks per metronome click was negative or greater than 255.");
			}
			if (thirtySecondNotesPerQuarterNote < 1 || thirtySecondNotesPerQuarterNote > 255)
			{
				throw new ArgumentOutOfRangeException("numerator", numerator, "The specified thirty-second notes per quarter note was less than one or greater than 255.");
			}
			this.numerator = numerator;
			this.denominatorLogarithm = denominatorLogarithm;
			this.ticksPerMetronomeClick = ticksPerMetronomeClick;
			this.thirtySecondNotesPerQuarterNote = thirtySecondNotesPerQuarterNote;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003352 File Offset: 0x00001552
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new TimeSignatureEvent(absoluteTime, deltaTime, this.numerator, this.denominatorLogarithm, this.ticksPerMetronomeClick, this.thirtySecondNotesPerQuarterNote);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003374 File Offset: 0x00001574
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.Write8(4);
			abinaryWriter.Write8((byte)this.numerator);
			abinaryWriter.Write8((byte)this.denominatorLogarithm);
			abinaryWriter.Write8((byte)this.ticksPerMetronomeClick);
			abinaryWriter.Write8((byte)this.thirtySecondNotesPerQuarterNote);
		}

		// Token: 0x04000017 RID: 23
		private int denominatorLogarithm;

		// Token: 0x04000018 RID: 24
		private int numerator;

		// Token: 0x04000019 RID: 25
		private int ticksPerMetronomeClick;

		// Token: 0x0400001A RID: 26
		private int thirtySecondNotesPerQuarterNote;
	}
}
