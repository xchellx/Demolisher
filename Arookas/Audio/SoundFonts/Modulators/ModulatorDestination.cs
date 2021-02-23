using System;
using Arookas.Audio.SoundFonts.Generators;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x02000039 RID: 57
	public abstract class ModulatorDestination
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x000067A8 File Offset: 0x000049A8
		internal static ModulatorDestination FromInternal(ushort internalDestination)
		{
			if ((internalDestination & 32768) != 0)
			{
				return new ModulatorModulatorDestination();
			}
			return new ModulatorGeneratorDestination
			{
				DestinationGenerator = (GeneratorType)internalDestination
			};
		}
	}
}
