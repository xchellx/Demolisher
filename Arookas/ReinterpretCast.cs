using System;
using System.Runtime.InteropServices;

namespace Arookas
{
	// Token: 0x0200006E RID: 110
	public static class ReinterpretCast
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000C002 File Offset: 0x0000A202
		public static TTo As<TFrom, TTo>(this TFrom value) where TFrom : struct where TTo : struct
		{
			if (Marshal.SizeOf(typeof(TFrom)) != Marshal.SizeOf(typeof(TTo)))
			{
				throw new ArgumentException("The specified source and destination types do not have the same unmanaged size.");
			}
			return new ReinterpretCast.Interpreter<TFrom, TTo>(value).asTTo;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C03A File Offset: 0x0000A23A
		public static sbyte AsSByte(this byte value)
		{
			return new ReinterpretCast.Interpreter8(value).asSByte;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C047 File Offset: 0x0000A247
		public static byte AsByte(this sbyte value)
		{
			return new ReinterpretCast.Interpreter8(value).asByte;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C054 File Offset: 0x0000A254
		public static short AsInt16(this ushort value)
		{
			return new ReinterpretCast.Interpreter16(value).asInt16;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C061 File Offset: 0x0000A261
		public static ushort AsUInt16(this short value)
		{
			return new ReinterpretCast.Interpreter16(value).asUInt16;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C06E File Offset: 0x0000A26E
		public static Int24 AsInt24(this UInt24 value)
		{
			return new ReinterpretCast.Interpreter24(value).asInt24;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C07B File Offset: 0x0000A27B
		public static UInt24 AsUInt24(this Int24 value)
		{
			return new ReinterpretCast.Interpreter24(value).asUInt24;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C088 File Offset: 0x0000A288
		public static int AsInt32(this uint value)
		{
			return new ReinterpretCast.Interpreter32(value).asInt32;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C095 File Offset: 0x0000A295
		public static int AsInt32(this float value)
		{
			return new ReinterpretCast.Interpreter32(value).asInt32;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C0A2 File Offset: 0x0000A2A2
		public static uint AsUInt32(this int value)
		{
			return new ReinterpretCast.Interpreter32(value).asUInt32;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public static uint AsUInt32(this float value)
		{
			return new ReinterpretCast.Interpreter32(value).asUInt32;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C0BC File Offset: 0x0000A2BC
		public static float AsSingle(this int value)
		{
			return new ReinterpretCast.Interpreter32(value).asSingle;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C0C9 File Offset: 0x0000A2C9
		public static float AsSingle(this uint value)
		{
			return new ReinterpretCast.Interpreter32(value).asSingle;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		public static long AsInt64(this ulong value)
		{
			return new ReinterpretCast.Interpreter64(value).asInt64;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C0E3 File Offset: 0x0000A2E3
		public static long AsInt64(this double value)
		{
			return new ReinterpretCast.Interpreter64(value).asInt64;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		public static ulong AsUInt64(this long value)
		{
			return new ReinterpretCast.Interpreter64(value).asUInt64;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		public static ulong AsUInt32(this double value)
		{
			return new ReinterpretCast.Interpreter64(value).asUInt64;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C10A File Offset: 0x0000A30A
		public static double AsDouble(this long value)
		{
			return new ReinterpretCast.Interpreter64(value).asDouble;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C117 File Offset: 0x0000A317
		public static double AsDouble(this ulong value)
		{
			return new ReinterpretCast.Interpreter64(value).asDouble;
		}

		// Token: 0x0200006F RID: 111
		[StructLayout(LayoutKind.Explicit)]
		private struct Interpreter8
		{
			// Token: 0x0600034A RID: 842 RVA: 0x0000C124 File Offset: 0x0000A324
			public Interpreter8(sbyte value)
			{
				this.asByte = 0;
				this.asSByte = value;
			}

			// Token: 0x0600034B RID: 843 RVA: 0x0000C134 File Offset: 0x0000A334
			public Interpreter8(byte value)
			{
				this.asSByte = 0;
				this.asByte = value;
			}

			// Token: 0x040001B4 RID: 436
			[FieldOffset(0)]
			public sbyte asSByte;

			// Token: 0x040001B5 RID: 437
			[FieldOffset(0)]
			public byte asByte;
		}

		// Token: 0x02000070 RID: 112
		[StructLayout(LayoutKind.Explicit)]
		private struct Interpreter16
		{
			// Token: 0x0600034C RID: 844 RVA: 0x0000C144 File Offset: 0x0000A344
			public Interpreter16(short value)
			{
				this.asUInt16 = 0;
				this.asInt16 = value;
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000C154 File Offset: 0x0000A354
			public Interpreter16(ushort value)
			{
				this.asInt16 = 0;
				this.asUInt16 = value;
			}

			// Token: 0x040001B6 RID: 438
			[FieldOffset(0)]
			public short asInt16;

			// Token: 0x040001B7 RID: 439
			[FieldOffset(0)]
			public ushort asUInt16;
		}

		// Token: 0x02000071 RID: 113
		[StructLayout(LayoutKind.Explicit)]
		private struct Interpreter24
		{
			// Token: 0x0600034E RID: 846 RVA: 0x0000C164 File Offset: 0x0000A364
			public Interpreter24(Int24 value)
			{
				this.asUInt24 = 0;
				this.asInt24 = value;
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000C179 File Offset: 0x0000A379
			public Interpreter24(UInt24 value)
			{
				this.asInt24 = (Int24)0;
				this.asUInt24 = value;
			}

			// Token: 0x040001B8 RID: 440
			[FieldOffset(0)]
			public Int24 asInt24;

			// Token: 0x040001B9 RID: 441
			[FieldOffset(0)]
			public UInt24 asUInt24;
		}

		// Token: 0x02000072 RID: 114
		[StructLayout(LayoutKind.Explicit)]
		private struct Interpreter32
		{
			// Token: 0x06000350 RID: 848 RVA: 0x0000C18E File Offset: 0x0000A38E
			public Interpreter32(int value)
			{
				this.asUInt32 = 0U;
				this.asSingle = 0f;
				this.asInt32 = value;
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
			public Interpreter32(uint value)
			{
				this.asInt32 = 0;
				this.asSingle = 0f;
				this.asUInt32 = value;
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
			public Interpreter32(float value)
			{
				this.asInt32 = 0;
				this.asUInt32 = 0U;
				this.asSingle = value;
			}

			// Token: 0x040001BA RID: 442
			[FieldOffset(0)]
			public int asInt32;

			// Token: 0x040001BB RID: 443
			[FieldOffset(0)]
			public uint asUInt32;

			// Token: 0x040001BC RID: 444
			[FieldOffset(0)]
			public float asSingle;
		}

		// Token: 0x02000073 RID: 115
		[StructLayout(LayoutKind.Explicit)]
		private struct Interpreter64
		{
			// Token: 0x06000353 RID: 851 RVA: 0x0000C1DB File Offset: 0x0000A3DB
			public Interpreter64(long value)
			{
				this.asUInt64 = 0UL;
				this.asDouble = 0.0;
				this.asInt64 = value;
			}

			// Token: 0x06000354 RID: 852 RVA: 0x0000C1FB File Offset: 0x0000A3FB
			public Interpreter64(ulong value)
			{
				this.asInt64 = 0L;
				this.asDouble = 0.0;
				this.asUInt64 = value;
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0000C21B File Offset: 0x0000A41B
			public Interpreter64(double value)
			{
				this.asInt64 = 0L;
				this.asUInt64 = 0UL;
				this.asDouble = value;
			}

			// Token: 0x040001BD RID: 445
			[FieldOffset(0)]
			public long asInt64;

			// Token: 0x040001BE RID: 446
			[FieldOffset(0)]
			public ulong asUInt64;

			// Token: 0x040001BF RID: 447
			[FieldOffset(0)]
			public double asDouble;
		}

		// Token: 0x02000074 RID: 116
		[StructLayout(LayoutKind.Explicit)]
		private struct Interpreter<TFrom, TTo> where TFrom : struct where TTo : struct
		{
			// Token: 0x06000356 RID: 854 RVA: 0x0000C234 File Offset: 0x0000A434
			public Interpreter(TFrom value)
			{
				this.asTTo = default(TTo);
				this.asTFrom = value;
			}

			// Token: 0x040001C0 RID: 448
			[FieldOffset(0)]
			public TFrom asTFrom;

			// Token: 0x040001C1 RID: 449
			[FieldOffset(0)]
			public TTo asTTo;
		}
	}
}
