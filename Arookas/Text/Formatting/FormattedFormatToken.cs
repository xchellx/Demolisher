using System;

namespace Arookas.Text.Formatting
{
	// Token: 0x0200009E RID: 158
	internal sealed class FormattedFormatToken : FormatToken
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x00011777 File Offset: 0x0000F977
		public int ArgumentNumber
		{
			get
			{
				return this.argumentNumber;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001177F File Offset: 0x0000F97F
		public int Spacing
		{
			get
			{
				return this.spacing;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x00011787 File Offset: 0x0000F987
		public FormattedFormatToken.StringAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001178F File Offset: 0x0000F98F
		public string FormatString
		{
			get
			{
				return this.formatString;
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00011797 File Offset: 0x0000F997
		public FormattedFormatToken(int argumentNumber, int spacing, FormattedFormatToken.StringAlignment alignment, string formatString)
		{
			this.argumentNumber = argumentNumber;
			this.spacing = spacing;
			this.alignment = alignment;
			this.formatString = formatString;
		}

		// Token: 0x0400024F RID: 591
		private readonly int argumentNumber;

		// Token: 0x04000250 RID: 592
		private readonly int spacing;

		// Token: 0x04000251 RID: 593
		private readonly FormattedFormatToken.StringAlignment alignment;

		// Token: 0x04000252 RID: 594
		private readonly string formatString;

		// Token: 0x0200009F RID: 159
		internal enum StringAlignment
		{
			// Token: 0x04000254 RID: 596
			RightAligned,
			// Token: 0x04000255 RID: 597
			LeftAligned
		}
	}
}
