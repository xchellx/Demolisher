using System;

namespace Arookas.IO.Text
{
	// Token: 0x0200009A RID: 154
	[Flags]
	public enum AScannerTokenScanModes : byte
	{
		// Token: 0x04000246 RID: 582
		Normal = 0,
		// Token: 0x04000247 RID: 583
		OperatorsAsText = 1,
		// Token: 0x04000248 RID: 584
		SymbolsAsText = 2,
		// Token: 0x04000249 RID: 585
		Text = 3
	}
}
