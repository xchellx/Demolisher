using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000043 RID: 67
	public sealed class SampleEndCoarseOffsetGenerator : Generator
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006CF5 File Offset: 0x00004EF5
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleEndCoarseOffset;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006CF9 File Offset: 0x00004EF9
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00006D02 File Offset: 0x00004F02
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

		// Token: 0x06000206 RID: 518 RVA: 0x00006D0B File Offset: 0x00004F0B
		public SampleEndCoarseOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006D1C File Offset: 0x00004F1C
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.endAddrsCoarseOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
