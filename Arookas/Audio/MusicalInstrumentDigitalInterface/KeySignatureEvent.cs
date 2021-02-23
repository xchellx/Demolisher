using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000012 RID: 18
	public sealed class KeySignatureEvent : MetaEvent
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002EC2 File Offset: 0x000010C2
		public int Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002ECA File Offset: 0x000010CA
		public Scale Scale
		{
			get
			{
				return this.scale;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002ED2 File Offset: 0x000010D2
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.KeySignature;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002ED6 File Offset: 0x000010D6
		public KeySignatureEvent(int key, Scale scale) : this(0UL, 0UL, key, scale)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002EE4 File Offset: 0x000010E4
		public KeySignatureEvent(ulong deltaTime, int key, Scale scale) : this(0UL, deltaTime, key, scale)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EF4 File Offset: 0x000010F4
		public KeySignatureEvent(ulong absoluteTime, ulong deltaTime, int key, Scale scale) : base(absoluteTime, deltaTime)
		{
			if (key < -7 || key > 7)
			{
				throw new ArgumentOutOfRangeException("key", key, "The specified key was less than -7 or greater than 7.");
			}
			if (!scale.IsDefined<Scale>())
			{
				throw new ArgumentOutOfRangeException("scale", scale, "The specified scale was not a defined value.");
			}
			this.key = key;
			this.scale = scale;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F57 File Offset: 0x00001157
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new KeySignatureEvent(absoluteTime, deltaTime, this.key, this.scale);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F6C File Offset: 0x0000116C
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.Write8(2);
			abinaryWriter.Write8((byte)this.scale);
			abinaryWriter.Write8((byte)this.scale);
		}

		// Token: 0x04000011 RID: 17
		private int key;

		// Token: 0x04000012 RID: 18
		private Scale scale;
	}
}
