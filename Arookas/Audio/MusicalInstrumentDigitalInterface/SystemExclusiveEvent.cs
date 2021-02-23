using System;
using System.IO;
using Arookas.Collections;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200001A RID: 26
	public sealed class SystemExclusiveEvent : MIDIEvent
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003590 File Offset: 0x00001790
		public bool HasWideManufacturerID
		{
			get
			{
				return this.manufacturerID > 65535;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000359F File Offset: 0x0000179F
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000035B9 File Offset: 0x000017B9
		public AReadOnlyArray<byte> Data
		{
			get
			{
				return this.data_readOnly;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000035C1 File Offset: 0x000017C1
		public SystemExclusiveEventType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000035C9 File Offset: 0x000017C9
		private SystemExclusiveEvent(ulong absoluteTime, ulong deltaTime) : base(absoluteTime, deltaTime)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000035D3 File Offset: 0x000017D3
		public SystemExclusiveEvent(SystemExclusiveEventType type, int manufacturerID, bool wideManufacturerID, byte[] data) : this(0UL, 0UL, type, manufacturerID, wideManufacturerID, data)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000035E4 File Offset: 0x000017E4
		public SystemExclusiveEvent(ulong deltaTime, SystemExclusiveEventType type, int manufacturerID, bool wideManufacturerID, byte[] data) : this(0UL, deltaTime, type, manufacturerID, wideManufacturerID, data)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000035F8 File Offset: 0x000017F8
		public SystemExclusiveEvent(ulong absoluteTime, ulong deltaTime, SystemExclusiveEventType type, int manufacturerID, bool wideManufacturerID, byte[] data) : base(absoluteTime, deltaTime)
		{
			if (!type.IsDefined<SystemExclusiveEventType>())
			{
				throw new ArgumentOutOfRangeException("type");
			}
			if (manufacturerID < 0 || manufacturerID > (wideManufacturerID ? 65535 : 255))
			{
				throw new ArgumentOutOfRangeException("manufacturerID", manufacturerID, "The specified MIDI ID was negative or wider than the inferred MIDI ID width.");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.type = type;
			this.manufacturerID = (wideManufacturerID ? (manufacturerID << 16) : manufacturerID);
			this.data = data.Duplicate<byte>();
			this.data_readOnly = new AReadOnlyArray<byte>(data);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003694 File Offset: 0x00001894
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new SystemExclusiveEvent(absoluteTime, deltaTime)
			{
				data = this.data,
				data_readOnly = new AReadOnlyArray<byte>(this.data),
				manufacturerID = this.manufacturerID,
				type = this.type
			};
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000036E0 File Offset: 0x000018E0
		public override void ToStream(Stream stream)
		{
			bool flag = this.type == SystemExclusiveEventType.Normal || this.type == SystemExclusiveEventType.Terminating;
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (this.type == SystemExclusiveEventType.Normal)
            {
				abinaryWriter.Write8(240);
            }
			else
			{
				abinaryWriter.Write8(247);
			}
			abinaryWriter.WriteUIntVar((ulong)((long)((flag ? (this.data.Length + 1) : this.data.Length) + (this.HasWideManufacturerID ? 3 : 1))));
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
			if (flag)
			{
				abinaryWriter.Write8(247);
			}
		}

		// Token: 0x0400001C RID: 28
		private int manufacturerID;

		// Token: 0x0400001D RID: 29
		private AReadOnlyArray<byte> data_readOnly;

		// Token: 0x0400001E RID: 30
		private byte[] data;

		// Token: 0x0400001F RID: 31
		private SystemExclusiveEventType type;
	}
}
