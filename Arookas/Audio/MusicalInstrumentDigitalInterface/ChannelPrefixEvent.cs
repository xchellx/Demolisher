using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200000C RID: 12
	public sealed class ChannelPrefixEvent : MetaEvent
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002C93 File Offset: 0x00000E93
		public byte ChannelNumber
		{
			get
			{
				return this.channelNumber;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002C9B File Offset: 0x00000E9B
		public override MetaEventType Type
		{
			get
			{
				return MetaEventType.ChannelPrefix;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C9F File Offset: 0x00000E9F
		public ChannelPrefixEvent(byte channelNumber) : this(0UL, 0UL, channelNumber)
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002CAC File Offset: 0x00000EAC
		public ChannelPrefixEvent(ulong deltaTime, byte channelNumber) : this(0UL, deltaTime, channelNumber)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public ChannelPrefixEvent(ulong absoluteTime, ulong deltaTime, byte channelNumber) : base(absoluteTime, deltaTime)
		{
			if (channelNumber > 15)
			{
				throw new ArgumentOutOfRangeException("channelNumber", channelNumber, "The specified MIDI channel number was not a valid MIDI channel number.");
			}
			this.channelNumber = channelNumber;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new ChannelPrefixEvent(absoluteTime, deltaTime, this.channelNumber);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public override void ToStream(Stream stream)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			abinaryWriter.Write8(byte.MaxValue);
			abinaryWriter.Write8((byte)this.Type);
			abinaryWriter.Write8(1);
			abinaryWriter.Write8(this.channelNumber);
		}

		// Token: 0x0400000F RID: 15
		private byte channelNumber;
	}
}
