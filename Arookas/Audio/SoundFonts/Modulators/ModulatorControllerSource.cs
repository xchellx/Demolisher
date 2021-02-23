using System;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x02000038 RID: 56
	public sealed class ModulatorControllerSource : ModulatorSource
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000066B4 File Offset: 0x000048B4
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000066BC File Offset: 0x000048BC
		public int Controller
		{
			get
			{
				return this.controller;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == 0 || value == 6 || value == 32 || value == 38 || (value >= 98 && value <= 101) || (value >= 120 && value <= 127))
				{
					throw new ArgumentException("The specified MIDI control-change number was not an allowed controller.", "value");
				}
				this.controller = value;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000671C File Offset: 0x0000491C
		public ModulatorControllerSource(int controller)
		{
			if (controller < 0 || controller > 127)
			{
				throw new ArgumentOutOfRangeException("controller");
			}
			if (controller == 0 || controller == 6 || controller == 32 || controller == 38 || (controller >= 98 && controller <= 101) || (controller >= 120 && controller <= 127))
			{
				throw new ArgumentException("The specified MIDI control-change number was not an allowed controller.", "value");
			}
			this.controller = controller;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000677F File Offset: 0x0000497F
		internal override ushort ToInternal()
		{
			return (ushort)((int)base.Type << 10 | (ModulatorType)base.Polarity | (ModulatorType)base.Direction | (ModulatorType)128 | (ModulatorType)this.controller);
		}

		// Token: 0x040000CF RID: 207
		private int controller;
	}
}
