using System;

namespace Arookas.Math
{
	// Token: 0x0200007D RID: 125
	public static class MathUtility
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000C364 File Offset: 0x0000A564
		public static double DegreesToRadians
		{
			get
			{
				return 0.017453292519943295;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000C36F File Offset: 0x0000A56F
		public static double RadiansToDegrees
		{
			get
			{
				return 57.295779513082323;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000C37A File Offset: 0x0000A57A
		public static bool Approximately(this float first, float second)
		{
			return first.Approximately(second, float.Epsilon);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000C388 File Offset: 0x0000A588
		public static bool Approximately(this float first, float second, float maximumDeviation)
		{
			return Math.Abs(first - second) <= maximumDeviation;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000C398 File Offset: 0x0000A598
		public static float Lerp(float start, float end, float percentage)
		{
			return start * (1f - percentage) + end * percentage;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000C3A7 File Offset: 0x0000A5A7
		public static double Lerp(double start, double end, float percentage)
		{
			return start * (double)(1f - percentage) + end * (double)percentage;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public static float ToDegrees(this float radians)
		{
			return radians * (float)MathUtility.RadiansToDegrees;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000C3C2 File Offset: 0x0000A5C2
		public static double ToDegrees(this double radians)
		{
			return radians * MathUtility.RadiansToDegrees;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		public static float ToRadians(this float degrees)
		{
			return degrees * (float)MathUtility.DegreesToRadians;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000C3D5 File Offset: 0x0000A5D5
		public static double ToRadians(this double degrees)
		{
			return degrees * MathUtility.DegreesToRadians;
		}
	}
}
