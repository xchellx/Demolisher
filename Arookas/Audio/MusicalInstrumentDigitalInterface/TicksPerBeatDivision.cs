using System;

namespace Arookas.Audio.MusicalInstrumentDigitalInterface
{
	// Token: 0x02000021 RID: 33
	public sealed class TicksPerBeatDivision : TimeDivision
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003871 File Offset: 0x00001A71
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003879 File Offset: 0x00001A79
		public int TicksPerBeat
		{
			get
			{
				return this.ticksPerBeat;
			}
			set
			{
				if (value < 0 || value > 32767)
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified ticks-per-beat value was negative or greater than 32,767.");
				}
				this.ticksPerBeat = value;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000038A4 File Offset: 0x00001AA4
		public TicksPerBeatDivision(int ticksPerBeat)
		{
			if (ticksPerBeat < 0 || ticksPerBeat > 32767)
			{
				throw new ArgumentOutOfRangeException("ticksPerBeat", ticksPerBeat, "The specified ticks-per-beat value was negative or greater than Int16.MaxValue.");
			}
			this.ticksPerBeat = ticksPerBeat;
		}

		// Token: 0x04000033 RID: 51
		private int ticksPerBeat;
	}
}
