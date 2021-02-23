using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000046 RID: 70
	public sealed class SampleLoopEndFineOffsetGenerator : Generator
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006DFD File Offset: 0x00004FFD
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleLoopEndFineOffset;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00006E00 File Offset: 0x00005000
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00006E09 File Offset: 0x00005009
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

		// Token: 0x06000215 RID: 533 RVA: 0x00006E12 File Offset: 0x00005012
		public SampleLoopEndFineOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006E24 File Offset: 0x00005024
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.endloopAddrsOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
