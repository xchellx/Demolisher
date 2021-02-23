using System;

namespace Arookas.IO.Text
{
	// Token: 0x02000097 RID: 151
	[Flags]
	public enum AScannerElementTypes : byte
	{
		// Token: 0x04000232 RID: 562
		None = 0,
		// Token: 0x04000233 RID: 563
		Integer = 1,
		// Token: 0x04000234 RID: 564
		Float = 2,
		// Token: 0x04000235 RID: 565
		String = 4,
		// Token: 0x04000236 RID: 566
		OperatorToken = 8,
		// Token: 0x04000237 RID: 567
		SymbolToken = 16,
		// Token: 0x04000238 RID: 568
		TextToken = 32,
		// Token: 0x04000239 RID: 569
		Number = 3,
		// Token: 0x0400023A RID: 570
		NumberOrString = 7,
		// Token: 0x0400023B RID: 571
		Token = 56,
		// Token: 0x0400023C RID: 572
		All = 63
	}
}
