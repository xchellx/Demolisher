using System;
using System.Collections.Generic;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000059 RID: 89
	public sealed class Preset
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000072A3 File Offset: 0x000054A3
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000072AB File Offset: 0x000054AB
		public int BankNumber
		{
			get
			{
				return this.bankNumber;
			}
			set
			{
				if (value < 0 || value > 128)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.bankNumber = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000072CB File Offset: 0x000054CB
		// (set) Token: 0x06000245 RID: 581 RVA: 0x000072D3 File Offset: 0x000054D3
		internal uint Genre
		{
			get
			{
				return this.genre;
			}
			set
			{
				this.genre = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000072DC File Offset: 0x000054DC
		// (set) Token: 0x06000247 RID: 583 RVA: 0x000072E4 File Offset: 0x000054E4
		internal uint Library
		{
			get
			{
				return this.library;
			}
			set
			{
				this.library = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000072ED File Offset: 0x000054ED
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000072F5 File Offset: 0x000054F5
		internal uint Morphology
		{
			get
			{
				return this.morphology;
			}
			set
			{
				this.morphology = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600024A RID: 586 RVA: 0x000072FE File Offset: 0x000054FE
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00007306 File Offset: 0x00005506
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length > 20)
				{
					throw new ArgumentException("The specified name is too long.", "value");
				}
				this.name = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00007337 File Offset: 0x00005537
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000733F File Offset: 0x0000553F
		public int ProgramNumber
		{
			get
			{
				return this.programNumber;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.programNumber = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000735C File Offset: 0x0000555C
		public List<PresetZone> Zones
		{
			get
			{
				return this.zones;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007364 File Offset: 0x00005564
		public Preset() : this(string.Empty, 0, 0)
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00007373 File Offset: 0x00005573
		public Preset(string name) : this(name, 0, 0)
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000737E File Offset: 0x0000557E
		public Preset(int bankNumber, int programNumber) : this(string.Empty, bankNumber, programNumber)
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00007390 File Offset: 0x00005590
		public Preset(string name, int bankNumber, int programNumber)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length > 20)
			{
				throw new ArgumentException("The specified name is too long.", "name");
			}
			if (bankNumber < 0 || bankNumber > 128)
			{
				throw new ArgumentOutOfRangeException("bankNumber", "The specified bank number was not a valid MIDI bank number.");
			}
			if (programNumber < 0 || programNumber > 127)
			{
				throw new ArgumentOutOfRangeException("programNumber", "The specified program number was not a valid MIDI program number.");
			}
			this.name = name;
			this.bankNumber = bankNumber;
			this.programNumber = programNumber;
			this.zones = new List<PresetZone>(5);
		}

		// Token: 0x04000154 RID: 340
		private int bankNumber;

		// Token: 0x04000155 RID: 341
		private uint genre;

		// Token: 0x04000156 RID: 342
		private uint library;

		// Token: 0x04000157 RID: 343
		private uint morphology;

		// Token: 0x04000158 RID: 344
		private string name;

		// Token: 0x04000159 RID: 345
		private int programNumber;

		// Token: 0x0400015A RID: 346
		private List<PresetZone> zones;
	}
}
