using System;

namespace Arookas.IO.Binary
{
	// Token: 0x02000076 RID: 118
	public class PositionAnchor
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000C251 File Offset: 0x0000A451
		// (set) Token: 0x06000359 RID: 857 RVA: 0x0000C260 File Offset: 0x0000A460
		public long End
		{
			get
			{
				return this.start + this.size;
			}
			set
			{
				this.size = value - this.start;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000C270 File Offset: 0x0000A470
		// (set) Token: 0x0600035B RID: 859 RVA: 0x0000C278 File Offset: 0x0000A478
		public long Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C281 File Offset: 0x0000A481
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0000C289 File Offset: 0x0000A489
		public long Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x040001C2 RID: 450
		private long start;

		// Token: 0x040001C3 RID: 451
		private long size;
	}
}
