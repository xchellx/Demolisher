using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000048 RID: 72
	public sealed class SampleEndFineOffsetGenerator : Generator
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00006EA8 File Offset: 0x000050A8
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.SampleEndFineOffset;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00006EAB File Offset: 0x000050AB
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00006EB4 File Offset: 0x000050B4
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

		// Token: 0x0600021F RID: 543 RVA: 0x00006EBD File Offset: 0x000050BD
		public SampleEndFineOffsetGenerator(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006ECC File Offset: 0x000050CC
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.endAddrsOffset;
			result.genAmount.wAmount = this.Value;
			return result;
		}
	}
}
