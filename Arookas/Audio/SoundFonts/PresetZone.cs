using System;
using System.Collections.Generic;
using Arookas.Audio.SoundFonts.Generators;
using Arookas.Audio.SoundFonts.Modulators;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x0200005A RID: 90
	public sealed class PresetZone
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007421 File Offset: 0x00005621
		public GeneratorCollection Generators
		{
			get
			{
				return this.generators;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00007429 File Offset: 0x00005629
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00007431 File Offset: 0x00005631
		public Instrument Instrument
		{
			get
			{
				return this.instrument;
			}
			set
			{
				this.instrument = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000743A File Offset: 0x0000563A
		public bool IsGlobalZone
		{
			get
			{
				return this.instrument == null;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007445 File Offset: 0x00005645
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000744D File Offset: 0x0000564D
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007456 File Offset: 0x00005656
		public List<Modulator> Modulators
		{
			get
			{
				return this.modulators;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000745E File Offset: 0x0000565E
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00007466 File Offset: 0x00005666
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

		// Token: 0x0600025C RID: 604 RVA: 0x0000746F File Offset: 0x0000566F
		public PresetZone() : this(KeyRange.FullRange, VelocityRange.FullRange)
		{
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007481 File Offset: 0x00005681
		public PresetZone(KeyRange keyRange) : this(keyRange, VelocityRange.FullRange)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000748F File Offset: 0x0000568F
		public PresetZone(VelocityRange velocityRange) : this(KeyRange.FullRange, velocityRange)
		{
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000749D File Offset: 0x0000569D
		public PresetZone(KeyRange keyRange, VelocityRange velocityRange)
		{
			this.keyRange = keyRange;
			this.velocityRange = velocityRange;
			this.generators = new GeneratorCollection();
			this.modulators = new List<Modulator>(5);
		}

		// Token: 0x0400015B RID: 347
		private GeneratorCollection generators;

		// Token: 0x0400015C RID: 348
		private Instrument instrument;

		// Token: 0x0400015D RID: 349
		private KeyRange keyRange;

		// Token: 0x0400015E RID: 350
		private List<Modulator> modulators;

		// Token: 0x0400015F RID: 351
		private VelocityRange velocityRange;
	}
}
