using System;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000040 RID: 64
	public struct VelocityRange
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00006A71 File Offset: 0x00004C71
		public static VelocityRange FullRange
		{
			get
			{
				return new VelocityRange(0, 127);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00006A7B File Offset: 0x00004C7B
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00006A83 File Offset: 0x00004C83
		public int HighVelocity
		{
			get
			{
				return this.highVelocity;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.highVelocity = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00006AA0 File Offset: 0x00004CA0
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00006AA8 File Offset: 0x00004CA8
		public int LowVelocity
		{
			get
			{
				return this.lowVelocity;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.lowVelocity = value;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006AC5 File Offset: 0x00004CC5
		public VelocityRange(int lowVelocity, int highVelocity)
		{
			if (lowVelocity < 0 || lowVelocity > 127)
			{
				throw new ArgumentOutOfRangeException("lowVelocity");
			}
			if (highVelocity < 0 || highVelocity > 127)
			{
				throw new ArgumentOutOfRangeException("highVelocity");
			}
			this.lowVelocity = (int)((byte)lowVelocity);
			this.highVelocity = (int)((byte)highVelocity);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006AFF File Offset: 0x00004CFF
		public override bool Equals(object obj)
		{
			return obj is VelocityRange && (VelocityRange)obj == this;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006B1C File Offset: 0x00004D1C
		public override int GetHashCode()
		{
			return this.lowVelocity.GetHashCode() ^ this.highVelocity.GetHashCode();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006B35 File Offset: 0x00004D35
		public override string ToString()
		{
			return string.Format("(velocities {0} through {1})", this.lowVelocity, this.highVelocity);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006B57 File Offset: 0x00004D57
		public static bool operator ==(VelocityRange lhs, VelocityRange rhs)
		{
			return lhs.lowVelocity == rhs.lowVelocity && lhs.highVelocity == rhs.highVelocity;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006B7B File Offset: 0x00004D7B
		public static bool operator !=(VelocityRange lhs, VelocityRange rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040000EA RID: 234
		private int highVelocity;

		// Token: 0x040000EB RID: 235
		private int lowVelocity;
	}
}
