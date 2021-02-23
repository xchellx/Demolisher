using System;

namespace Arookas.IO.Binary
{
	// Token: 0x02000094 RID: 148
	public sealed class ABinaryStringFormat
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00010C36 File Offset: 0x0000EE36
		internal ABinaryStringFormatType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00010C3E File Offset: 0x0000EE3E
		public int Multiple
		{
			get
			{
				return this.multiple;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00010C46 File Offset: 0x0000EE46
		private ABinaryStringFormat(ABinaryStringFormatType type, int multiple)
		{
			this.type = type;
			this.multiple = multiple;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public static ABinaryStringFormat Clamped(int multiple)
		{
			return new ABinaryStringFormat(ABinaryStringFormatType.Clamped, multiple);
		}

		// Token: 0x040001FF RID: 511
		private readonly ABinaryStringFormatType type;

		// Token: 0x04000200 RID: 512
		private readonly int multiple;

		// Token: 0x04000201 RID: 513
		public static readonly ABinaryStringFormat Raw = new ABinaryStringFormat(ABinaryStringFormatType.Raw, 0);

		// Token: 0x04000202 RID: 514
		public static readonly ABinaryStringFormat RawWithLength = new ABinaryStringFormat(ABinaryStringFormatType.RawWithLength, 0);

		// Token: 0x04000203 RID: 515
		public static readonly ABinaryStringFormat NullTerminated = new ABinaryStringFormat(ABinaryStringFormatType.NullTerminated, 0);
	}
}
