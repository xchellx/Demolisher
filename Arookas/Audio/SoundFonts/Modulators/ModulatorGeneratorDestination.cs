using System;
using Arookas.Audio.SoundFonts.Generators;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x0200003A RID: 58
	public sealed class ModulatorGeneratorDestination : ModulatorDestination
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000067DA File Offset: 0x000049DA
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000067E2 File Offset: 0x000049E2
		public GeneratorType DestinationGenerator
		{
			get
			{
				return this.destinationGenerator;
			}
			set
			{
				if (!value.IsDefined<GeneratorType>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.destinationGenerator = value;
			}
		}

		// Token: 0x040000D0 RID: 208
		private GeneratorType destinationGenerator;
	}
}
