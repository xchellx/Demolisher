using System;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x0200003B RID: 59
	public sealed class ModulatorModulatorDestination : ModulatorDestination
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006806 File Offset: 0x00004A06
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000680E File Offset: 0x00004A0E
		public Modulator DestinationModulator
		{
			get
			{
				return this.destinationModulator;
			}
			set
			{
				this.destinationModulator = value;
			}
		}

		// Token: 0x040000D1 RID: 209
		private Modulator destinationModulator;
	}
}
