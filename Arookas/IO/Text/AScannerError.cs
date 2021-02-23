using System;

namespace Arookas.IO.Text
{
	// Token: 0x02000098 RID: 152
	public enum AScannerError : byte
	{
		// Token: 0x0400023E RID: 574
		None,
		// Token: 0x0400023F RID: 575
		NothingWasThere,
		// Token: 0x04000240 RID: 576
		UnexpectedEOF,
		// Token: 0x04000241 RID: 577
		IncorrectNumberFormat,
		// Token: 0x04000242 RID: 578
		MissingQuote,
		// Token: 0x04000243 RID: 579
		MissingBrace,
		// Token: 0x04000244 RID: 580
		MissingSemicolon
	}
}
