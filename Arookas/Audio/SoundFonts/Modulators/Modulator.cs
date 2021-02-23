using System;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x02000031 RID: 49
	public sealed class Modulator
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000064C4 File Offset: 0x000046C4
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000064CC File Offset: 0x000046CC
		public int Amount
		{
			get
			{
				return this.amount;
			}
			set
			{
				if (value < -32768 || value > 32767)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.amount = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000064F0 File Offset: 0x000046F0
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000064F8 File Offset: 0x000046F8
		public ModulatorSource AmountSource
		{
			get
			{
				return this.amountSource;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.amountSource = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000650F File Offset: 0x0000470F
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006517 File Offset: 0x00004717
		public ModulatorDestination Destination
		{
			get
			{
				return this.destination;
			}
			set
			{
				this.destination = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006520 File Offset: 0x00004720
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006528 File Offset: 0x00004728
		public ModulatorSource Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.source = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000653F File Offset: 0x0000473F
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006547 File Offset: 0x00004747
		public ModulatorTransform Transform
		{
			get
			{
				return this.transform;
			}
			set
			{
				if (!value.IsDefined<ModulatorTransform>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.transform = value;
			}
		}

		// Token: 0x040000B4 RID: 180
		private int amount;

		// Token: 0x040000B5 RID: 181
		private ModulatorSource amountSource;

		// Token: 0x040000B6 RID: 182
		private ModulatorDestination destination;

		// Token: 0x040000B7 RID: 183
		private ModulatorSource source;

		// Token: 0x040000B8 RID: 184
		private ModulatorTransform transform;
	}
}
