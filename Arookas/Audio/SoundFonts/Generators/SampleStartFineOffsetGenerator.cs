using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x0200004C RID: 76
	public sealed class SampleStartFineOffsetGenerator : Generator
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00007009 File Offset: 0x00005209
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleStartFineOffset;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000700C File Offset: 0x0000520C
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00007015 File Offset: 0x00005215
		public ushort Value
		{
			get
			{
				return (ushort)base.InternalValue;
			}
			set
			{
				base.InternalValue = (int)value;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000701E File Offset: 0x0000521E
		public SampleStartFineOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007030 File Offset: 0x00005230
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.startAddrsOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
