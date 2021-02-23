using System;

namespace Arookas.Text.Formatting
{
	// Token: 0x0200009D RID: 157
	internal sealed class LiteralFormatToken : FormatToken
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00011760 File Offset: 0x0000F960
		public string Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00011768 File Offset: 0x0000F968
		public LiteralFormatToken(string literal)
		{
			this.literal = literal;
		}

		// Token: 0x0400024E RID: 590
		private readonly string literal;
	}
}
