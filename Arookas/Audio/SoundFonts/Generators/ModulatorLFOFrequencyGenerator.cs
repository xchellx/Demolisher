using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x02000042 RID: 66
	public sealed class ModulatorLFOFrequencyGenerator : Generator
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006C9D File Offset: 0x00004E9D
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.ModulatorLFOFrequency;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00006CA1 File Offset: 0x00004EA1
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00006CAA File Offset: 0x00004EAA
		public short Value
		{
			get
			{
				return (short)base.InternalValue;
			}
			set
			{
				base.InternalValue = (int)value;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006CB3 File Offset: 0x00004EB3
		public ModulatorLFOFrequencyGenerator(short value)
		{
			this.Value = value;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006CC4 File Offset: 0x00004EC4
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.freqModLFO;
			result.genAmount.shAmount = this.Value;
			return result;
		}
	}
}
