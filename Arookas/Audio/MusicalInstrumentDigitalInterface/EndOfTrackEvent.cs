using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000018 RID: 24
	public sealed class EndOfTrackEvent : MetaEvent
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003453 File Offset: 0x00001653
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.EndOfTrack;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003457 File Offset: 0x00001657
		public EndOfTrackEvent() : this(0UL, 0UL)
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003463 File Offset: 0x00001663
		public EndOfTrackEvent(ulong deltaTime) : this(0UL, deltaTime)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000346E File Offset: 0x0000166E
		public EndOfTrackEvent(ulong absoluteTime, ulong deltaTime) : base(absoluteTime, deltaTime)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003478 File Offset: 0x00001678
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new EndOfTrackEvent(absoluteTime, deltaTime);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003484 File Offset: 0x00001684
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.Write8(0);
		}
	}
}
