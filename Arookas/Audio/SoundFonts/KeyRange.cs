using System;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000041 RID: 65
	public struct KeyRange
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00006B87 File Offset: 0x00004D87
		public static KeyRange FullRange
		{
			get
			{
				return new KeyRange(0, 127);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006B91 File Offset: 0x00004D91
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006B99 File Offset: 0x00004D99
		public int HighKey
		{
			get
			{
				return this.highKey;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.highKey = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006BB6 File Offset: 0x00004DB6
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00006BBE File Offset: 0x00004DBE
		public int LowKey
		{
			get
			{
				return this.lowKey;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.lowKey = value;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006BDB File Offset: 0x00004DDB
		public KeyRange(int lowKey, int highKey)
		{
			if (lowKey < 0 || lowKey > 127)
			{
				throw new ArgumentOutOfRangeException("lowKey");
			}
			if (highKey < 0 || highKey > 127)
			{
				throw new ArgumentOutOfRangeException("highKey");
			}
			this.lowKey = (int)((byte)lowKey);
			this.highKey = (int)((byte)highKey);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006C15 File Offset: 0x00004E15
		public override bool Equals(object obj)
		{
			return obj is KeyRange && (KeyRange)obj == this;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006C32 File Offset: 0x00004E32
		public override int GetHashCode()
		{
			return this.lowKey.GetHashCode() ^ this.highKey.GetHashCode();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006C4B File Offset: 0x00004E4B
		public override string ToString()
		{
			return string.Format("(keys #{0} through #{1})", this.lowKey, this.highKey);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006C6D File Offset: 0x00004E6D
		public static bool operator ==(KeyRange lhs, KeyRange rhs)
		{
			return lhs.lowKey == rhs.lowKey && lhs.highKey == rhs.highKey;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006C91 File Offset: 0x00004E91
		public static bool operator !=(KeyRange lhs, KeyRange rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000EC RID: 236
		private int highKey;

		// Token: 0x040000ED RID: 237
		private int lowKey;
	}
}
