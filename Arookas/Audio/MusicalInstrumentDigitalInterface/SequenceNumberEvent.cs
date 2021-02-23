using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000014 RID: 20
	public sealed class SequenceNumberEvent : MetaEvent
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000316A File Offset: 0x0000136A
		public int SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003172 File Offset: 0x00001372
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.SequenceNumber;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003175 File Offset: 0x00001375
		public SequenceNumberEvent(int sequenceNumber) : this(0UL, 0UL, sequenceNumber)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003182 File Offset: 0x00001382
		public SequenceNumberEvent(ulong deltaTime, int sequenceNumber) : this(0UL, deltaTime, sequenceNumber)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000318E File Offset: 0x0000138E
		public SequenceNumberEvent(ulong absoluteTime, ulong deltaTime, int sequenceNumber) : base(absoluteTime, deltaTime)
		{
			if (sequenceNumber < 0 || sequenceNumber > 65535)
			{
				throw new ArgumentOutOfRangeException("sequenceNumber", sequenceNumber, "The specified sequence number was negative or greater than 65,535.");
			}
			this.sequenceNumber = sequenceNumber;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000031C1 File Offset: 0x000013C1
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new SequenceNumberEvent(absoluteTime, deltaTime, this.sequenceNumber);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000031D0 File Offset: 0x000013D0
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.Write8(2);
			abinaryWriter.Write16((ushort)this.sequenceNumber);
		}

		// Token: 0x04000016 RID: 22
		private int sequenceNumber;
	}
}
