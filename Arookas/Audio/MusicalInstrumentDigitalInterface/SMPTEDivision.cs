using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x0200001F RID: 31
	public sealed class SMPTEDivision : TimeDivision
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000037AC File Offset: 0x000019AC
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000037B4 File Offset: 0x000019B4
		public SMPTEFrameRate FrameRate
		{
			get
			{
				return this.frameRate;
			}
			set
			{
				if (!value.IsDefined<SMPTEFrameRate>())
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified SMPTEFrameRate value was not defined.");
				}
				this.frameRate = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000037DB File Offset: 0x000019DB
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000037E3 File Offset: 0x000019E3
		public int TicksPerFrame
		{
			get
			{
				return this.ticksPerFrame;
			}
			set
			{
				if (value < 0 || value > 255)
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified ticks-per-frame value was negative or greater than 255.");
				}
				this.ticksPerFrame = value;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003810 File Offset: 0x00001A10
		public SMPTEDivision(SMPTEFrameRate frameRate, int ticksPerFrame)
		{
			if (!frameRate.IsDefined<SMPTEFrameRate>())
			{
				throw new ArgumentOutOfRangeException("frameRate", frameRate, "The specified SMPTEFrameRate value was not defined.");
			}
			if (ticksPerFrame < 0 || ticksPerFrame > 255)
			{
				throw new ArgumentOutOfRangeException("ticksPerFrame", ticksPerFrame, "The specified ticks-per-frame value was negative or greater than 255.");
			}
			this.frameRate = frameRate;
			this.ticksPerFrame = ticksPerFrame;
		}

		// Token: 0x0400002C RID: 44
		private SMPTEFrameRate frameRate;

		// Token: 0x0400002D RID: 45
		private int ticksPerFrame;
	}
}
