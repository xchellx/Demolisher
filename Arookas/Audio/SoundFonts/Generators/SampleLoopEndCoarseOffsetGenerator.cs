using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000044 RID: 68
	public sealed class SampleLoopEndCoarseOffsetGenerator : Generator
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006D4D File Offset: 0x00004F4D
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleLoopEndCoarseOffset;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006D51 File Offset: 0x00004F51
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00006D5A File Offset: 0x00004F5A
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

		// Token: 0x0600020B RID: 523 RVA: 0x00006D63 File Offset: 0x00004F63
		public SampleLoopEndCoarseOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006D74 File Offset: 0x00004F74
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.endloopAddrsCoarseOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
