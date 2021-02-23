using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000045 RID: 69
	public sealed class SampleLoopStartCoarseOffsetGenerator : Generator
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleLoopStartCoarseOffset;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006DA9 File Offset: 0x00004FA9
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00006DB2 File Offset: 0x00004FB2
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

		// Token: 0x06000210 RID: 528 RVA: 0x00006DBB File Offset: 0x00004FBB
		public SampleLoopStartCoarseOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006DCC File Offset: 0x00004FCC
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.startloopAddrsCoarseOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
