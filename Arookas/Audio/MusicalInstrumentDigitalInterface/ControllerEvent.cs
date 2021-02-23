using System;
using System.IO;
using Arookas.IO.Binary;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000007 RID: 7
	public sealed class ControllerEvent : ChannelEvent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002811 File Offset: 0x00000A11
		public int ControllerNumber
		{
			get
			{
				return this.controllerNumber;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002819 File Offset: 0x00000A19
		public int ControllerValue
		{
			get
			{
				return this.controllerValue;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002821 File Offset: 0x00000A21
		public override ChannelEventType Type
		{
			get
			{
				return ChannelEventType.Controller;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002828 File Offset: 0x00000A28
		public ControllerEvent(int channelNumber, int controllerNumber, int controllerValue) : this(0UL, 0UL, channelNumber, controllerNumber, controllerValue)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002837 File Offset: 0x00000A37
		public ControllerEvent(ulong deltaTime, int channelNumber, int controllerNumber, int controllerValue) : this(0UL, deltaTime, channelNumber, controllerNumber, controllerValue)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002848 File Offset: 0x00000A48
		public ControllerEvent(ulong absoluteTime, ulong deltaTime, int channelNumber, int controllerNumber, int controllerValue) : base(absoluteTime, deltaTime, channelNumber)
		{
			if (controllerNumber < 0 || controllerNumber > 127)
			{
				throw new ArgumentOutOfRangeException("controllerValue", controllerValue, "The specified controller number was negative or greater than 127.");
			}
			if (controllerValue < 0 || controllerValue > 127)
			{
				throw new ArgumentOutOfRangeException("controllerValue", controllerValue, "The specified controller value was negative or greater than 127.");
			}
			this.controllerNumber = controllerNumber;
			this.controllerValue = controllerValue;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028B2 File Offset: 0x00000AB2
		public override MIDIEvent SetTime(ulong absoluteTime, ulong deltaTime)
		{
			return new ControllerEvent(absoluteTime, deltaTime, base.ChannelNumber, this.controllerNumber, this.controllerValue);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028CD File Offset: 0x00000ACD
		public override void ToStream(Stream stream)
		{
			this.ToStream(stream, false);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028D8 File Offset: 0x00000AD8
		public override void ToStream(Stream stream, bool runningStatus)
		{
			ABinaryWriter abinaryWriter = new ABinaryWriter(stream, Endianness.Big);
			abinaryWriter.WriteUIntVar(base.DeltaTime);
			if (runningStatus)
			{
				abinaryWriter.Write8((byte)((int)this.Type | base.ChannelNumber));
			}
			abinaryWriter.Write8((byte)this.controllerNumber);
			abinaryWriter.Write8((byte)this.controllerValue);
		}

		// Token: 0x04000007 RID: 7
		private int controllerNumber;

		// Token: 0x04000008 RID: 8
		private int controllerValue;
	}
}
