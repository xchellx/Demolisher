using System;

namespace Arookas.Audio.SoundFonts.Generators
{
	// Token: 0x0200004A RID: 74
	public sealed class PanGenerator : Generator
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006F50 File Offset: 0x00005150
		public override GeneratorType Type
		{
			get
			{
				return GeneratorType.Pan;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006F54 File Offset: 0x00005154
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00006F5D File Offset: 0x0000515D
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

		// Token: 0x06000229 RID: 553 RVA: 0x00006F66 File Offset: 0x00005166
		public PanGenerator() : this(0)
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00006F6F File Offset: 0x0000516F
		public PanGenerator(short value)
		{
			this.Value = value;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006F80 File Offset: 0x00005180
		internal override sfGenList ToInternal()
		{
			sfGenList result = default(sfGenList);
			result.sfGenOper = SFGenerator.pan;
			result.genAmount.shAmount = this.Value;
			return result;
		}
	}
}
