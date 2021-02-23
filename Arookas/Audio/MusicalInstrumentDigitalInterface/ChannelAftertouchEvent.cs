using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000006 RID: 6
	public sealed class ChannelAftertouchEvent : ChannelEvent
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000274D File Offset: 0x0000094D
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.ChannelAftertouch;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002754 File Offset: 0x00000954
		public int Pressure
		{
			get
			{
				return this.pressure;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000275C File Offset: 0x0000095C
		public ChannelAftertouchEvent(int channelNumber, int pressure) : this(0UL, 0UL, channelNumber, pressure)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000276A File Offset: 0x0000096A
		public ChannelAftertouchEvent(ulong deltaTime, int channelNumber, int pressure) : this(0UL, deltaTime, channelNumber, pressure)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002777 File Offset: 0x00000977
		public ChannelAftertouchEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int pressure) : base(absoluteTime, deltaTime, channelNumber)
		{
			if (pressure < 0 || pressure > 127)
			{
				throw new ArgumentOutOfRangeException("pressure", pressure, "The specified pressure was negative or greater than 127.");
			}
			this.pressure = pressure;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027AC File Offset: 0x000009AC
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new ChannelAftertouchEvent(absoluteTime, deltaTime, base.ChannelNumber, this.pressure);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027C1 File Offset: 0x000009C1
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027CC File Offset: 0x000009CC
		public override void ToStream(Stream stream, bool runningStatus)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (runningStatus)
			{
				abinaryWriter.Write8((byte)((int)this.Type | base.ChannelNumber));
			}
			abinaryWriter.Write8((byte)this.pressure);
		}

		// Token: 0x04000006 RID: 6
		private int pressure;
	}
}
