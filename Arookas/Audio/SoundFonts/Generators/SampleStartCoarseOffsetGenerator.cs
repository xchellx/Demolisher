using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000047 RID: 71
	public sealed class SampleStartCoarseOffsetGenerator : Generator
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006E54 File Offset: 0x00005054
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleStartCoarseOffset;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006E57 File Offset: 0x00005057
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00006E60 File Offset: 0x00005060
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

		// Token: 0x0600021A RID: 538 RVA: 0x00006E69 File Offset: 0x00005069
		public SampleStartCoarseOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006E78 File Offset: 0x00005078
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.startAddrsCoarseOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
