using System;
using System.Drawing;

namespace Arookas
{
	// Token: 0x02000023 RID: 35
	public struct Color
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003A0E File Offset: 0x00001C0E
		public int A
		{
			get
			{
				return (int)this.alpha;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003A16 File Offset: 0x00001C16
		public uint ARGB
		{
			get
			{
				return (uint)((int)this.alpha << 24 | (int)this.red << 16 | (int)this.green << 8 | (int)this.blue);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003A3B File Offset: 0x00001C3B
		public static Color Black
		{
			get
			{
				return new Color(0);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003A43 File Offset: 0x00001C43
		public int B
		{
			get
			{
				return (int)this.blue;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003A4B File Offset: 0x00001C4B
		public static Color Blue
		{
			get
			{
				return new Color(0, 0, 255);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003A59 File Offset: 0x00001C59
		public static Color Cyan
		{
			get
			{
				return new Color(0, 255, 255);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003A6B File Offset: 0x00001C6B
		public int G
		{
			get
			{
				return (int)this.green;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003A73 File Offset: 0x00001C73
		public static Color Green
		{
			get
			{
				return new Color(0, 255, 0);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003A81 File Offset: 0x00001C81
		public Color Inverse
		{
			get
			{
				return new Color((int)(byte.MaxValue - this.red), (int)(byte.MaxValue - this.green), (int)(byte.MaxValue - this.blue), (int)this.alpha);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003AC1 File Offset: 0x00001CC1
		private static int[] Lookup3Bit
		{
			get
			{
				if (Color.lookup3Bit == null)
				{
					Color.lookup3Bit = CollectionHelper.Initialize<int>(8, (int index) => (int)(36.428571428571431 * (double)index));
				}
				return Color.lookup3Bit;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003B06 File Offset: 0x00001D06
		private static int[] Lookup4Bit
		{
			get
			{
				if (Color.lookup4Bit == null)
				{
					Color.lookup4Bit = CollectionHelper.Initialize<int>(16, (int index) => (int)(17.0 * (double)index));
				}
				return Color.lookup4Bit;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003B4C File Offset: 0x00001D4C
		private static int[] Lookup5Bit
		{
			get
			{
				if (Color.lookup5Bit == null)
				{
					Color.lookup5Bit = CollectionHelper.Initialize<int>(32, (int index) => (int)(8.2258064516129039 * (double)index));
				}
				return Color.lookup5Bit;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003B92 File Offset: 0x00001D92
		private static int[] Lookup6Bit
		{
			get
			{
				if (Color.lookup6Bit == null)
				{
					Color.lookup6Bit = CollectionHelper.Initialize<int>(64, (int index) => (int)(4.0476190476190474 * (double)index));
				}
				return Color.lookup6Bit;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003BC9 File Offset: 0x00001DC9
		public static Color Magenta
		{
			get
			{
				return new Color(255, 0, 255);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003BDB File Offset: 0x00001DDB
		public int R
		{
			get
			{
				return (int)this.red;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003BE3 File Offset: 0x00001DE3
		public static Color Red
		{
			get
			{
				return new Color(255, 0, 0);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public UInt24 RGB
		{
			get
			{
				return (UInt24)((int)this.alpha << 24 | (int)this.red << 16 | (int)this.green << 8);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003C14 File Offset: 0x00001E14
		public uint RGBA
		{
			get
			{
				return (uint)((int)this.red << 24 | (int)this.green << 16 | (int)this.blue << 8 | (int)this.alpha);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003C39 File Offset: 0x00001E39
		public static Color White
		{
			get
			{
				return new Color(255);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003C45 File Offset: 0x00001E45
		public static Color Yellow
		{
			get
			{
				return new Color(255, 255, 0);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003C57 File Offset: 0x00001E57
		public Color(int intensity)
		{
			this = new Color(intensity, intensity, intensity, 255);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003C67 File Offset: 0x00001E67
		public Color(int intensity, int alpha)
		{
			this = new Color(intensity, intensity, intensity, alpha);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003C73 File Offset: 0x00001E73
		public Color(int red, int green, int blue)
		{
			this = new Color(red, green, blue, 255);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003C84 File Offset: 0x00001E84
		public Color(int red, int green, int blue, int alpha)
		{
			if (red < 0 || red > 255)
			{
				throw new ArgumentOutOfRangeException("red", red, "The specified red component was negative or greater than 255.");
			}
			if (green < 0 || green > 255)
			{
				throw new ArgumentOutOfRangeException("green", green, "The specified green component was negative or greater than 255.");
			}
			if (blue < 0 || blue > 255)
			{
				throw new ArgumentOutOfRangeException("blue", blue, "The specified blue component was negative or greater than 255.");
			}
			if (alpha < 0 || alpha > 255)
			{
				throw new ArgumentOutOfRangeException("alpha", alpha, "The specified alpha component was negative or greater than 255.");
			}
			this.red = (byte)red;
			this.green = (byte)green;
			this.blue = (byte)blue;
			this.alpha = (byte)alpha;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003D3D File Offset: 0x00001F3D
		public Color(Color color)
		{
			this = new Color(color.R, color.G, color.B, color.A);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003D61 File Offset: 0x00001F61
		public Color(Color color, int alpha)
		{
			this = new Color(color.R, color.G, color.B, alpha);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003D7F File Offset: 0x00001F7F
		public Color(System.Drawing.Color color)
		{
			this = new Color((int)color.R, (int)color.G, (int)color.B, (int)color.A);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003DA3 File Offset: 0x00001FA3
		public Color(System.Drawing.Color color, int alpha)
		{
			this = new Color((int)color.R, (int)color.G, (int)color.B, alpha);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003DC1 File Offset: 0x00001FC1
		public override bool Equals(object obj)
		{
			return obj is Color && this == (Color)obj;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003DDE File Offset: 0x00001FDE
		public static Color FromARGB(uint argb)
		{
			return new Color((int)(argb >> 16 & 255U), (int)(argb >> 8 & 255U), (int)(argb & 255U), (int)(argb >> 24 & 255U));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003E09 File Offset: 0x00002009
		public static Color FromRGB4A3(ushort rgb4A3)
		{
			return new Color(Color.Lookup4Bit[rgb4A3 >> 8 & 15], Color.Lookup4Bit[rgb4A3 >> 4 & 15], Color.Lookup4Bit[(int)(rgb4A3 & 15)], Color.Lookup3Bit[rgb4A3 >> 12 & 7]);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003E3E File Offset: 0x0000203E
		public static Color FromRGB5(ushort rgb5)
		{
			return new Color(Color.Lookup5Bit[rgb5 >> 10 & 31], Color.Lookup5Bit[rgb5 >> 5 & 31], Color.Lookup5Bit[(int)(rgb5 & 31)]);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003E68 File Offset: 0x00002068
		public static Color FromRGB5A1(ushort rgb565A1)
		{
			return new Color(Color.Lookup5Bit[rgb565A1 >> 10 & 31], Color.Lookup5Bit[rgb565A1 >> 5 & 31], Color.Lookup5Bit[(int)(rgb565A1 & 31)], 255 * (rgb565A1 >> 15));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003E9C File Offset: 0x0000209C
		public static Color FromRGB565(ushort rgb565)
		{
			return new Color(Color.Lookup5Bit[rgb565 >> 11 & 31], Color.Lookup6Bit[rgb565 >> 5 & 63], Color.Lookup5Bit[(int)(rgb565 & 31)]);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003EC6 File Offset: 0x000020C6
		public static Color FromRGBA(uint rgba)
		{
			return new Color((int)(rgba >> 24 & 255U), (int)(rgba >> 16 & 255U), (int)(rgba >> 8 & 255U), (int)(rgba & 255U));
		}

		// Token: NONE RID: NONE RVA: NONE File Offset: NONE
		public static Color FromARGB8(uint argb8)
		{
			return new Color((int)(argb8 >> 16 & 255U), (int)(argb8 >> 8 & 255U), (int)(argb8 & 255U), (int)(argb8 >> 24 & 255U));
		}

		// Token: NONE RID: NONE RVA: NONE File Offset: NONE
		public static Color FromRGBA8(uint rgba8)
		{
			return new Color((int)(rgba8 >> 24 & 255U), (int)(rgba8 >> 16 & 255U), (int)(rgba8 >> 8 & 255U), (int)(rgba8 & 255U));
		}

		// Token: NONE RID: NONE RVA: NONE File Offset: NONE
		public static float ToScale(int clr)
		{
			return clr / 255.0f;
		}

		// Token: NONE RID: NONE RVA: NONE File Offset: NONE
		public static int ToValue(float clr)
		{
			return System.Math.Max(0, System.Math.Min(255, (int)System.Math.Floor(clr * 256.0)));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003EF4 File Offset: 0x000020F4
		public static Color[] FromST3C1(ulong st3c1)
		{
			Color[] array = new Color[4];
			array[0] = Color.FromRGB565((ushort)(st3c1 >> 48 & 65535UL));
			array[1] = Color.FromRGB565((ushort)(st3c1 >> 32 & 65535UL));
			if (array[0].ARGB > array[1].ARGB)
			{
				array[2] = Color.Lerp(array[0], array[1], 0.333333f);
				array[3] = Color.Lerp(array[0], array[1], 0.666666f);
			}
			else
			{
				array[2] = Color.Lerp(array[0], array[1], 0.5f);
				array[3] = new Color(0, 0);
			}
			Color[] array2 = new Color[16];
			int num = 30;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					array2[4 * i + j] = array[(int)(checked((IntPtr)(st3c1 >> num & 3UL)))];
					num -= 2;
				}
			}
			return array2;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000404D File Offset: 0x0000224D
		public override int GetHashCode()
		{
			return this.RGBA.AsInt32();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000405C File Offset: 0x0000225C
		public static Color Lerp(Color from, Color to, float percent)
		{
			return new Color((int)((float)from.red + (float)(to.red - from.red) * percent), (int)((float)from.green + (float)(to.green - from.green) * percent), (int)((float)from.blue + (float)(to.blue - from.blue) * percent), (int)((float)from.alpha + (float)(to.alpha - from.alpha) * percent));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000040E0 File Offset: 0x000022E0
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs.red == rhs.red && lhs.green == rhs.green && lhs.blue == rhs.blue && lhs.alpha == rhs.alpha;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000412F File Offset: 0x0000232F
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000413B File Offset: 0x0000233B
		public static Color operator /(Color color, int divisor)
		{
			return new Color((int)color.red / divisor, (int)color.green / divisor, (int)color.blue / divisor, (int)color.alpha / divisor);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004166 File Offset: 0x00002366
		public static Color operator /(Color color, float divisor)
		{
			return new Color((int)((float)color.red / divisor), (int)((float)color.green / divisor), (int)((float)color.blue / divisor), (int)((float)color.alpha / divisor));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004199 File Offset: 0x00002399
		public static Color operator *(Color color, int multiplier)
		{
			return new Color((int)color.red * multiplier, (int)color.green * multiplier, (int)color.blue * multiplier, (int)color.alpha * multiplier);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000041C4 File Offset: 0x000023C4
		public static Color operator *(Color color, float multiplier)
		{
			return new Color((int)((float)color.red * multiplier), (int)((float)color.green * multiplier), (int)((float)color.blue * multiplier), (int)((float)color.alpha * multiplier));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000041F7 File Offset: 0x000023F7
		public static Color operator +(Color color, int addend)
		{
			return new Color((int)color.red + addend, (int)color.green + addend, (int)color.blue + addend, (int)color.alpha + addend);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004222 File Offset: 0x00002422
		public static Color operator -(Color color, int subtrahend)
		{
			return new Color((int)color.red - subtrahend, (int)color.green - subtrahend, (int)color.blue - subtrahend, (int)color.alpha - subtrahend);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000424D File Offset: 0x0000244D
		public static Color operator -(Color color)
		{
			return color.Inverse;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004256 File Offset: 0x00002456
		public static implicit operator System.Drawing.Color(Color color)
		{
			return System.Drawing.Color.FromArgb((int)color.alpha, (int)color.red, (int)color.green, (int)color.blue);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004279 File Offset: 0x00002479
		public static implicit operator Color(System.Drawing.Color color)
		{
			return new Color((int)color.R, (int)color.G, (int)color.B, (int)color.A);
		}

		// Token: 0x04000035 RID: 53
		public const int Opaque = 255;

		// Token: 0x04000036 RID: 54
		private byte red;

		// Token: 0x04000037 RID: 55
		private byte green;

		// Token: 0x04000038 RID: 56
		private byte blue;

		// Token: 0x04000039 RID: 57
		private byte alpha;

		// Token: 0x0400003A RID: 58
		private static int[] lookup3Bit;

		// Token: 0x0400003B RID: 59
		private static int[] lookup4Bit;

		// Token: 0x0400003C RID: 60
		private static int[] lookup5Bit;

		// Token: 0x0400003D RID: 61
		private static int[] lookup6Bit;
	}
}
