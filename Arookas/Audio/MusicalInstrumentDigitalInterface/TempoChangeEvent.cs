using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000019 RID: 25
	public sealed class TempoChangeEvent : MetaEvent
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000034C3 File Offset: 0x000016C3
		public int BeatsPerMinute
		{
			get
			{
				return (int)(60000000.0 / (double)this.tempo);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000034D7 File Offset: 0x000016D7
		public int Tempo
		{
			get
			{
				return this.tempo;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000034DF File Offset: 0x000016DF
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.TempoChange;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000034E3 File Offset: 0x000016E3
		public TempoChangeEvent(int tempo) : this(0UL, 0UL, tempo)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000034F0 File Offset: 0x000016F0
		public TempoChangeEvent(ulong deltaTime, int tempo) : this(0UL, deltaTime, tempo)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000034FC File Offset: 0x000016FC
		public TempoChangeEvent(ulong absoluteTime, ulong deltaTime, int tempo) : base(absoluteTime, deltaTime)
		{
			if (tempo < 0 || tempo > 8355711)
			{
				throw new ArgumentOutOfRangeException("tempo", tempo, "The specified tempo was negative or too high.");
			}
			this.tempo = tempo;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000352F File Offset: 0x0000172F
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new TempoChangeEvent(absoluteTime, deltaTime, this.tempo);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003540 File Offset: 0x00001740
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.Write8(3);
			abinaryWriter.Write24((UInt24)this.tempo);
		}

		// Token: 0x0400001B RID: 27
		private int tempo;
	}
}
