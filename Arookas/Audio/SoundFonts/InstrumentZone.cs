using System;
using System.Collections.Generic;
using Arookas.Audio.SoundFonts.Generators;
using Arookas.Audio.SoundFonts.Modulators;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000030 RID: 48
	public sealed class InstrumentZone
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000641B File Offset: 0x0000461B
		public GeneratorCollection Generators
		{
			get
			{
				return this.generators;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006423 File Offset: 0x00004623
		public bool IsGlobalZone
		{
			get
			{
				return this.sample == null;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000642E File Offset: 0x0000462E
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00006436 File Offset: 0x00004636
		public KeyRange KeyRange
		{
			get
			{
				return this.keyRange;
			}
			set
			{
				this.keyRange = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x0000643F File Offset: 0x0000463F
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00006447 File Offset: 0x00004647
		public Sample Sample
		{
			get
			{
				return this.sample;
			}
			set
			{
				this.sample = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006450 File Offset: 0x00004650
		public List<Modulator> Modulators
		{
			get
			{
				return this.modulators;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006458 File Offset: 0x00004658
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00006460 File Offset: 0x00004660
		public VelocityRange VelocityRange
		{
			get
			{
				return this.velocityRange;
			}
			set
			{
				this.velocityRange = value;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006469 File Offset: 0x00004669
		public InstrumentZone() : this(KeyRange.FullRange, VelocityRange.FullRange)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000647B File Offset: 0x0000467B
		public InstrumentZone(KeyRange keyRange) : this(keyRange, VelocityRange.FullRange)
		{
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006489 File Offset: 0x00004689
		public InstrumentZone(VelocityRange velocityRange) : this(KeyRange.FullRange, velocityRange)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006497 File Offset: 0x00004697
		public InstrumentZone(KeyRange keyRange, VelocityRange velocityRange)
		{
			this.keyRange = keyRange;
			this.velocityRange = velocityRange;
			this.generators = new GeneratorCollection();
			this.modulators = new List<Modulator>(5);
		}

		// Token: 0x040000AF RID: 175
		private GeneratorCollection generators;

		// Token: 0x040000B0 RID: 176
		private KeyRange keyRange;

		// Token: 0x040000B1 RID: 177
		private Sample sample;

		// Token: 0x040000B2 RID: 178
		private List<Modulator> modulators;

		// Token: 0x040000B3 RID: 179
		private VelocityRange velocityRange;
	}
}
