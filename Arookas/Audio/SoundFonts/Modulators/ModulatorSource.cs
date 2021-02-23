using System;

namespace Arookas.Audio.SoundFonts.Modulators
{
	// Token: 0x02000036 RID: 54
	public abstract class ModulatorSource
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000656B File Offset: 0x0000476B
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00006573 File Offset: 0x00004773
		public ModulatorDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				if (!value.IsDefined<ModulatorDirection>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.direction = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000658F File Offset: 0x0000478F
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006597 File Offset: 0x00004797
		public ModulatorPolarity Polarity
		{
			get
			{
				return this.polarity;
			}
			set
			{
				if (!value.IsDefined<ModulatorPolarity>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.polarity = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000065B3 File Offset: 0x000047B3
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000065BB File Offset: 0x000047BB
		public ModulatorType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				if (!value.IsDefined<ModulatorType>())
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000065D8 File Offset: 0x000047D8
		internal static ModulatorSource FromInternal(ushort interalSource)
		{
			ModulatorType modulatorType = (ModulatorType)(interalSource >> 10 & 63);
			ModulatorPolarity modulatorPolarity = (ModulatorPolarity)(interalSource & 512);
			ModulatorDirection modulatorDirection = (ModulatorDirection)(interalSource & 256);
			if ((interalSource & 128) != 0)
			{
				return new ModulatorControllerSource((int)(interalSource & 127))
				{
					Direction = modulatorDirection,
					Polarity = modulatorPolarity,
					Type = modulatorType
				};
			}
			return new ModulatorGeneralControllerSource((ModulatorGeneralController)(interalSource & 127))
			{
				Direction = modulatorDirection,
				Polarity = modulatorPolarity,
				Type = modulatorType
			};
		}

		// Token: 0x060001BB RID: 443
		internal abstract ushort ToInternal();

		// Token: 0x040000CB RID: 203
		private ModulatorDirection direction;

		// Token: 0x040000CC RID: 204
		private ModulatorPolarity polarity;

		// Token: 0x040000CD RID: 205
		private ModulatorType type;
	}
}
