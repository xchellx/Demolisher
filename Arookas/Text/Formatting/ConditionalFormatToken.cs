using System;

namespace Arookas.Text.Formatting
{
	// Token: 0x020000A0 RID: 160
	internal sealed class ConditionalFormatToken : FormatToken
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000117BC File Offset: 0x0000F9BC
		public int ArgumentNumber
		{
			get
			{
				return this.argumentNumber;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public bool Inverted
		{
			get
			{
				return this.inverted;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000117CC File Offset: 0x0000F9CC
		public FormatTokenCollection TrueTokens
		{
			get
			{
				return this.trueTokens;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public FormatTokenCollection FalseTokens
		{
			get
			{
				return this.falseTokens;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000117DC File Offset: 0x0000F9DC
		public ConditionalFormatToken(int argumentNumber, bool inverted, FormatTokenCollection parent)
		{
			this.argumentNumber = argumentNumber;
			this.inverted = inverted;
			this.trueTokens = new FormatTokenCollection(parent);
			this.falseTokens = new FormatTokenCollection(parent);
		}

		// Token: 0x04000256 RID: 598
		private readonly int argumentNumber;

		// Token: 0x04000257 RID: 599
		private readonly bool inverted;

		// Token: 0x04000258 RID: 600
		private readonly FormatTokenCollection trueTokens;

		// Token: 0x04000259 RID: 601
		private readonly FormatTokenCollection falseTokens;
	}
}
