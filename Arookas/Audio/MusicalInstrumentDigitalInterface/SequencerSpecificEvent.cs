using System;
using System.IO;
using Arookas.Collections;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000013 RID: 19
	public sealed class SequencerSpecificEvent : MetaEvent
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002FC3 File Offset: 0x000011C3
		public bool HasWideManufacturerID
		{
			get
			{
				return this.manufacturerID > 65535;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FD2 File Offset: 0x000011D2
		public int ManufacturerID
		{
			get
			{
				if (!this.HasWideManufacturerID)
				{
					return this.manufacturerID;
				}
				return this.manufacturerID >> 16;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002FEC File Offset: 0x000011EC
		public AReadOnlyArray<byte> Data
		{
			get
			{
				return this.data_readOnly;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002FF4 File Offset: 0x000011F4
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.SequencerSpecific;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002FF8 File Offset: 0x000011F8
		private SequencerSpecificEvent(ulong absoluteTime, ulong deltaTime) : base(absoluteTime, deltaTime)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003002 File Offset: 0x00001202
		public SequencerSpecificEvent(int manufacturerID, bool wideManufacturerID, byte[] data) : this(0UL, 0UL, manufacturerID, wideManufacturerID, data)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003011 File Offset: 0x00001211
		public SequencerSpecificEvent(ulong deltaTime, int manufacturerID, bool wideManufacturerID, byte[] data) : this(0UL, deltaTime, manufacturerID, wideManufacturerID, data)
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003020 File Offset: 0x00001220
		public SequencerSpecificEvent(ulong absoluteTime, ulong deltaTime, int manufacturerID, bool wideManufacturerID, byte[] data) : base(absoluteTime, deltaTime)
		{
			if (manufacturerID < 0 || manufacturerID > (wideManufacturerID ? 65535 : 255))
			{
				throw new ArgumentOutOfRangeException("manufacturerID", manufacturerID, "The specified MIDI ID was negative or wider than the inferred MIDI ID width.");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.manufacturerID = (wideManufacturerID ? (manufacturerID << 16) : manufacturerID);
			this.data = data.Duplicate<byte>();
			this.data_readOnly = new AReadOnlyArray<byte>(data);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000309C File Offset: 0x0000129C
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new SequencerSpecificEvent(absoluteTime, deltaTime)
			{
				data = this.data,
				data_readOnly = new AReadOnlyArray<byte>(this.data),
				manufacturerID = this.manufacturerID
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000030DC File Offset: 0x000012DC
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.WriteUIntVar((ulong)((long)(this.data.Length + (this.HasWideManufacturerID ? 3 : 1))));
			if (this.HasWideManufacturerID)
			{
				abinaryWriter.Write8(0);
				abinaryWriter.Write16((ushort)this.manufacturerID);
			}
			else
			{
				abinaryWriter.Write8((byte)(this.manufacturerID >> 16));
			}
			abinaryWriter.Write8s(this.data);
		}

		// Token: 0x04000013 RID: 19
		private int manufacturerID;

		// Token: 0x04000014 RID: 20
		private AReadOnlyArray<byte> data_readOnly;

		// Token: 0x04000015 RID: 21
		private byte[] data;
	}
}
