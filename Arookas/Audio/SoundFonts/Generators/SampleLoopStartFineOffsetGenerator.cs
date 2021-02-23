using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000049 RID: 73
	public sealed class SampleLoopStartFineOffsetGenerator : Generator
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00006EFC File Offset: 0x000050FC
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleLoopStartFineOffset;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006EFF File Offset: 0x000050FF
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00006F08 File Offset: 0x00005108
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

		// Token: 0x06000224 RID: 548 RVA: 0x00006F11 File Offset: 0x00005111
		public SampleLoopStartFineOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00006F20 File Offset: 0x00005120
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.startloopAddrsOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
