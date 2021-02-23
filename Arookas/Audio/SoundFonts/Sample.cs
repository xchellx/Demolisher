using System;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x0200003E RID: 62
	public sealed class Sample
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000681F File Offset: 0x00004A1F
		public bool IsMono
		{
			get
			{
				return this.link == null;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000682A File Offset: 0x00004A2A
		public bool IsStereo
		{
			get
			{
				return this.link != null && this.link.link == this && this.linkType + (int)this.link.linkType == (SampleLinkType)6;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00006859 File Offset: 0x00004A59
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00006861 File Offset: 0x00004A61
		public bool IsPitched
		{
			get
			{
				return this.isPitched;
			}
			set
			{
				this.isPitched = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000686A File Offset: 0x00004A6A
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00006872 File Offset: 0x00004A72
		public bool IsROM
		{
			get
			{
				return this.isROM;
			}
			set
			{
				this.isROM = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000687B File Offset: 0x00004A7B
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00006883 File Offset: 0x00004A83
		public Sample Link
		{
			get
			{
				return this.link;
			}
			set
			{
				if (this.link == this)
				{
					throw new ArgumentException("A Sample may not link to itself.", "value");
				}
				if (value == null)
				{
					this.linkType = SampleLinkType.None;
				}
				else if (this.linkType == SampleLinkType.None)
				{
					this.linkType = SampleLinkType.Linked;
				}
				this.link = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000068C0 File Offset: 0x00004AC0
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000068C8 File Offset: 0x00004AC8
		public SampleLinkType LinkType
		{
			get
			{
				return this.linkType;
			}
			set
			{
				if (!value.IsDefined<SampleLinkType>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == SampleLinkType.None)
				{
					this.link = null;
				}
				else if (this.link == null)
				{
					throw new InvalidOperationException("Cannot assign Sample.LinkType a value other than SampleLinkType.None when Sample.Link is null. Try assigning Sample.Link a non-null value before setting the link type.");
				}
				this.linkType = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00006903 File Offset: 0x00004B03
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000690B File Offset: 0x00004B0B
		public int LoopEnd
		{
			get
			{
				return this.loopEnd;
			}
			set
			{
				this.loopEnd = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00006914 File Offset: 0x00004B14
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000691C File Offset: 0x00004B1C
		public int LoopStart
		{
			get
			{
				return this.loopStart;
			}
			set
			{
				this.loopStart = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00006925 File Offset: 0x00004B25
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000692D File Offset: 0x00004B2D
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000695E File Offset: 0x00004B5E
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00006966 File Offset: 0x00004B66
		public int PitchCorrection
		{
			get
			{
				return this.pitchCorrection;
			}
			set
			{
				if (value < -99 | value > 99)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.pitchCorrection = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00006987 File Offset: 0x00004B87
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000698F File Offset: 0x00004B8F
		public int RootNote
		{
			get
			{
				return this.rootNote;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.rootNote = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000069AC File Offset: 0x00004BAC
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000069B4 File Offset: 0x00004BB4
		public Int24[] Samples
		{
			get
			{
				return this.samples;
			}
			set
			{
				this.samples = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000069BD File Offset: 0x00004BBD
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000069C5 File Offset: 0x00004BC5
		public int SampleRate
		{
			get
			{
				return this.sampleRate;
			}
			set
			{
				if (this.sampleRate < 400 || this.sampleRate > 50000)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.sampleRate = value;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000069F3 File Offset: 0x00004BF3
		public Sample() : this(string.Empty)
		{
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006A00 File Offset: 0x00004C00
		public Sample(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length > 20)
			{
				throw new ArgumentException("The specified name is too long.", "value");
			}
			this.name = name;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006A37 File Offset: 0x00004C37
		public static void CreateStereoPair(Sample leftSample, Sample rightSample)
		{
			if (leftSample == null)
			{
				throw new ArgumentNullException("leftSample");
			}
			if (rightSample == null)
			{
				throw new ArgumentNullException("rightSample");
			}
			leftSample.link = rightSample;
			leftSample.linkType = SampleLinkType.Right;
			rightSample.link = leftSample;
			rightSample.linkType = SampleLinkType.Left;
		}

		// Token: 0x040000DA RID: 218
		private bool isPitched;

		// Token: 0x040000DB RID: 219
		private bool isROM;

		// Token: 0x040000DC RID: 220
		private Sample link;

		// Token: 0x040000DD RID: 221
		private SampleLinkType linkType;

		// Token: 0x040000DE RID: 222
		private int loopEnd;

		// Token: 0x040000DF RID: 223
		private int loopStart;

		// Token: 0x040000E0 RID: 224
		private string name;

		// Token: 0x040000E1 RID: 225
		private int pitchCorrection;

		// Token: 0x040000E2 RID: 226
		private int rootNote;

		// Token: 0x040000E3 RID: 227
		private Int24[] samples;

		// Token: 0x040000E4 RID: 228
		private int sampleRate;
	}
}
