using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x0200004B RID: 75
	public sealed class ModulatorLFODelayGenerator : Generator
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006FB1 File Offset: 0x000051B1
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.ModulatorLFODelay;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00006FB5 File Offset: 0x000051B5
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00006FBE File Offset: 0x000051BE
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

		// Token: 0x0600022F RID: 559 RVA: 0x00006FC7 File Offset: 0x000051C7
		public ModulatorLFODelayGenerator(short value)
		{
			this.Value = value;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00006FD8 File Offset: 0x000051D8
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.delayModLFO;
			result.genAmount.shAmount = this.Value;
			return result;
		}
	}
}
