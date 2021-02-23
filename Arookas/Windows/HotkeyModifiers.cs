using System;

namespace Arookas.Windows
{
	// Token: 0x020000A3 RID: 163
	[Flags]
	public enum HotkeyModifiers
	{
		// Token: 0x04000261 RID: 609
		None = 0,
		// Token: 0x04000262 RID: 610
		Alt = 1,
		// Token: 0x04000263 RID: 611
		Control = 2,
		// Token: 0x04000264 RID: 612
		Shift = 4,
		// Token: 0x04000265 RID: 613
		Windows = 8,
		// Token: 0x04000266 RID: 614
		All = 15
	}
}
