using System;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x02000037 RID: 55
	public sealed class ModulatorGeneralControllerSource : ModulatorSource
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00006654 File Offset: 0x00004854
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000665C File Offset: 0x0000485C
		public ModulatorGeneralController GeneralController
		{
			get
			{
				return this.generalController;
			}
			set
			{
				if (!value.IsDefined<ModulatorGeneralController>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.generalController = value;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006678 File Offset: 0x00004878
		public ModulatorGeneralControllerSource(ModulatorGeneralController generalController)
		{
			if (!generalController.IsDefined<ModulatorGeneralController>())
			{
				throw new ArgumentOutOfRangeException("generalController");
			}
			this.generalController = generalController;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000669A File Offset: 0x0000489A
		internal override ushort ToInternal()
		{
			return (ushort)(base.Type << 10 | (ModulatorType)base.Polarity | (ModulatorType)base.Direction);
		}

		// Token: 0x040000CE RID: 206
		private ModulatorGeneralController generalController;
	}
}
