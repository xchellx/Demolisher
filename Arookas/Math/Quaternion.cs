using System;

namespace Arookas.Math
{
	// Token: 0x0200007F RID: 127
	public struct Quaternion
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C3E6 File Offset: 0x0000A5E6
		public float Angle
		{
			get
			{
				return Quaternion.CalculateAngle(this);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000C3F3 File Offset: 0x0000A5F3
		public Vector3D Axis
		{
			get
			{
				return Quaternion.CalculateAxis(this);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000C400 File Offset: 0x0000A600
		public Vector3D BackwardVector
		{
			get
			{
				return -this.ForwardVector;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000C40D File Offset: 0x0000A60D
		public Quaternion Conjugate
		{
			get
			{
				return Quaternion.CalculateConjugate(this);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000C41A File Offset: 0x0000A61A
		public Vector3D DownwardVector
		{
			get
			{
				return -this.UpwardVector;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000C427 File Offset: 0x0000A627
		public Vector3D EulerAngles
		{
			get
			{
				return Quaternion.CalculateEulerAngles(this);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000C434 File Offset: 0x0000A634
		public Vector3D ForwardVector
		{
			get
			{
				return Quaternion.CalculateForwardVector(this);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000C441 File Offset: 0x0000A641
		public static Quaternion Identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C45C File Offset: 0x0000A65C
		public Quaternion Inverse
		{
			get
			{
				return Quaternion.CalculateInverse(this);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000C469 File Offset: 0x0000A669
		public Vector3D LeftwardVector
		{
			get
			{
				return -this.RightwardVector;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000C476 File Offset: 0x0000A676
		public float Magnitude
		{
			get
			{
				return (float)System.Math.Sqrt((double)this.MagnitudeSquared);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000C485 File Offset: 0x0000A685
		public float MagnitudeSquared
		{
			get
			{
				return this.w * this.w + this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		public Quaternion Normalized
		{
			get
			{
				return Quaternion.Normalize(this);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C4CB File Offset: 0x0000A6CB
		public Vector3D RightwardVector
		{
			get
			{
				return Quaternion.CalculateRightwardVector(this);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public Vector3D UpwardVector
		{
			get
			{
				return Quaternion.CalculateUpwardVector(this);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C4E5 File Offset: 0x0000A6E5
		public float W
		{
			get
			{
				return this.w;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C4ED File Offset: 0x0000A6ED
		public float X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000C4F5 File Offset: 0x0000A6F5
		public float Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000C4FD File Offset: 0x0000A6FD
		public float Z
		{
			get
			{
				return this.z;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000C508 File Offset: 0x0000A708
		public static Quaternion Zero
		{
			get
			{
				return default(Quaternion);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000C520 File Offset: 0x0000A720
		public Quaternion(float scalar)
		{
			this.z = scalar;
			this.y = scalar;
			this.x = scalar;
			this.w = scalar;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C54F File Offset: 0x0000A74F
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C56E File Offset: 0x0000A76E
		public static bool Approximately(Quaternion first, Quaternion second)
		{
			return Quaternion.Approximately(first, second, float.Epsilon);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000C57C File Offset: 0x0000A77C
		public static bool Approximately(Quaternion first, Quaternion second, float maximumDeviation)
		{
			return first.x.Approximately(second.x, maximumDeviation) && first.y.Approximately(second.y, maximumDeviation) && first.z.Approximately(second.z, maximumDeviation) && first.w.Approximately(second.w, maximumDeviation);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000C5E1 File Offset: 0x0000A7E1
		public static float CalculateAngle(Quaternion quaternion)
		{
			return (float)(2.0 * System.Math.Acos((double)quaternion.w));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		public static Vector3D CalculateAxis(Quaternion quaternion)
		{
			float num = (float)System.Math.Sqrt((double)(1f - quaternion.w * quaternion.w));
			if (num < 1.401298E-45f)
			{
				return Vector3D.UnitX;
			}
			return new Vector3D(quaternion.x, quaternion.y, quaternion.z) / num;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000C654 File Offset: 0x0000A854
		public static Quaternion CalculateConjugate(Quaternion quaternion)
		{
			return new Quaternion(-quaternion.x, -quaternion.y, -quaternion.z, quaternion.w);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000C67C File Offset: 0x0000A87C
		public static float CalculateDotProduct(Quaternion first, Quaternion second)
		{
			return first.x * second.x + first.y * second.y + first.z * second.z * first.w * second.w;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000C6C8 File Offset: 0x0000A8C8
		public static Vector3D CalculateEulerAngles(Quaternion quaternion)
		{
			float num = quaternion.x * quaternion.y + quaternion.z * quaternion.w;
			if (num > 0.499f)
			{
				return new Vector3D((float)(System.Math.Atan2((double)quaternion.x, (double)quaternion.y) * 2.0), 1.57079637f, 0f);
			}
			if (num < -0.499f)
			{
				return new Vector3D((float)(System.Math.Atan2((double)quaternion.x, (double)quaternion.w) * -2.0), 1.57079637f, 0f);
			}
			double num2 = (double)(quaternion.x * quaternion.x);
			double num3 = (double)(quaternion.y * quaternion.y);
			double num4 = (double)(quaternion.z * quaternion.z);
			return new Vector3D((float)System.Math.Atan2((double)(2f * quaternion.y * quaternion.w - 2f * quaternion.x * quaternion.z), 1.0 - 2.0 * num3 - 2.0 * num4), (float)System.Math.Asin((double)(2f * num)), (float)System.Math.Atan2((double)(2f * quaternion.x * quaternion.w - 2f * quaternion.y * quaternion.z), 1.0 - 2.0 * num2 - 2.0 * num4));
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000C854 File Offset: 0x0000AA54
		public static Vector3D CalculateForwardVector(Quaternion quaternion)
		{
			return new Vector3D(2f * (quaternion.x * quaternion.z + quaternion.w * quaternion.y), 2f * (quaternion.y * quaternion.x - quaternion.w * quaternion.x), 1f - 2f * (quaternion.x * quaternion.x * quaternion.y * quaternion.y));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000C8DB File Offset: 0x0000AADB
		public static Quaternion CalculateInverse(Quaternion quaternion)
		{
			return quaternion.Conjugate / quaternion.MagnitudeSquared;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		public static Vector3D CalculateRightwardVector(Quaternion quaternion)
		{
			return new Vector3D(1f - 2f * (quaternion.y * quaternion.y + quaternion.z * quaternion.z), 2f * (quaternion.x * quaternion.y + quaternion.w * quaternion.z), 2f * (quaternion.x * quaternion.z - quaternion.w * quaternion.y));
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C978 File Offset: 0x0000AB78
		public static Vector3D CalculateUpwardVector(Quaternion quaternion)
		{
			return new Vector3D(2f * (quaternion.x * quaternion.y - quaternion.w * quaternion.z), 1f - 2f * (quaternion.x * quaternion.x + quaternion.z * quaternion.z), 2f * (quaternion.y * quaternion.z + quaternion.w * quaternion.x));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public static Quaternion FromAxisAngle(Vector3D axis, float angle)
		{
			Vector3D vector3D = axis * (float)System.Math.Sin((double)(angle * 0.5f));
			return new Quaternion(vector3D.X, vector3D.Y, vector3D.Z, (float)System.Math.Cos((double)(angle * 0.5f)));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public static Quaternion FromForwardUpward(Vector3D forward, Vector3D up)
		{
			Vector3D normalized = (forward + up).Normalized;
			return new Quaternion(up.Y * normalized.Z - up.Z * normalized.Y, up.Z * normalized.X - up.X * normalized.Z, up.X * normalized.Y - up.Y * normalized.X, Vector3D.CalculateDotProduct(up, normalized));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public static Quaternion FromLookAt(Vector3D source, Vector3D destination)
		{
			Vector3D normalized = (destination - source).Normalized;
			float num = Vector3D.CalculateDotProduct(Vector3D.UnitZ, normalized);
			if (System.Math.Abs(num + 1f) < 1.401298E-45f)
			{
				return new Quaternion(Vector3D.UnitY.X, Vector3D.UnitY.Y, Vector3D.UnitY.Z, 3.14159274f);
			}
			if (System.Math.Abs(num - 1f) < 1.401298E-45f)
			{
				return Quaternion.Identity;
			}
			Vector3D normalized2 = Vector3D.CalculateCrossProduct(Vector3D.UnitZ, normalized).Normalized;
			float angle = (float)System.Math.Acos((double)num);
			return Quaternion.FromAxisAngle(normalized2, angle);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public static Quaternion FromEulerAngles(Vector3D eulerAngles)
		{
			return Quaternion.FromEulerAngles(eulerAngles.X, eulerAngles.Y, eulerAngles.Z);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public static Quaternion FromEulerAngles(float pitch, float yaw, float roll)
		{
			float num = (float)System.Math.Sin((double)(yaw * 0.5f));
			float num2 = (float)System.Math.Sin((double)(roll * 0.5f));
			float num3 = (float)System.Math.Sin((double)(pitch * 0.5f));
			float num4 = (float)System.Math.Cos((double)(yaw * 0.5f));
			float num5 = (float)System.Math.Cos((double)(roll * 0.5f));
			float num6 = (float)System.Math.Cos((double)(pitch * 0.5f));
			return new Quaternion(num * num2 * num6 + num4 * num5 * num3, num * num5 * num6 + num4 * num2 * num3, num4 * num2 * num6 - num * num5 * num3, num4 * num5 * num6 - num * num2 * num3);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000CC42 File Offset: 0x0000AE42
		public override bool Equals(object obj)
		{
			return obj is Quaternion && (Quaternion)obj == this;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000CC5F File Offset: 0x0000AE5F
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode() ^ this.w.GetHashCode();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000CC90 File Offset: 0x0000AE90
		public static Quaternion Lerp(Quaternion start, Quaternion end, float percentage)
		{
			return new Quaternion(MathUtility.Lerp(start.x, end.x, percentage), MathUtility.Lerp(start.y, end.y, percentage), MathUtility.Lerp(start.z, end.z, percentage), MathUtility.Lerp(start.w, end.w, percentage));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000CCF2 File Offset: 0x0000AEF2
		public static Quaternion Normalize(Quaternion quaternion)
		{
			return quaternion / quaternion.Magnitude;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000CD04 File Offset: 0x0000AF04
		public static Quaternion Slerp(Quaternion start, Quaternion end, float percentage)
		{
			float num = Quaternion.CalculateDotProduct(start, end);
			if (num < 0f)
			{
				num = -num;
				end = -end;
			}
			if (1f - num > 1.401298E-45f)
			{
				float num2 = (float)System.Math.Acos((double)num);
				Vector3D vector3D = new Vector3D((float)System.Math.Sin((double)(num2 * (1f - percentage))), (float)System.Math.Sin((double)(num2 * percentage)), (float)System.Math.Sin((double)num2));
				return new Quaternion((start.x * vector3D.X + end.x * vector3D.Y) / vector3D.Z, (start.y * vector3D.X + end.y * vector3D.Y) / vector3D.Z, (start.z * vector3D.X + end.z * vector3D.Y) / vector3D.Z, (start.w * vector3D.X + end.w * vector3D.Y) / vector3D.Z);
			}
			return Quaternion.Lerp(start, end, percentage);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public override string ToString()
		{
			return string.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x,
				this.y,
				this.z,
				this.w
			});
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000CE74 File Offset: 0x0000B074
		public static Quaternion operator +(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		public static Quaternion operator -(Quaternion quaternion)
		{
			return new Quaternion(-quaternion.x, -quaternion.y, -quaternion.z, -quaternion.w);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public static Quaternion operator -(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000CF3C File Offset: 0x0000B13C
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.z * rhs.y - lhs.y * rhs.z, lhs.w * rhs.y + lhs.y * rhs.w + lhs.x * rhs.z - lhs.z * rhs.x, lhs.w * rhs.z + lhs.z * rhs.w + lhs.y * rhs.x - lhs.x * rhs.y, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000D04C File Offset: 0x0000B24C
		public static Vector3D operator *(Quaternion lhs, Vector3D rhs)
		{
			Vector3D first = new Vector3D(lhs.x, lhs.y, lhs.z);
			Vector3D vector3D = Vector3D.CalculateCrossProduct(first, rhs) * 2f;
			return rhs + vector3D * lhs.w + Vector3D.CalculateCrossProduct(first, vector3D);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000D0A6 File Offset: 0x0000B2A6
		public static Quaternion operator /(Quaternion lhs, float rhs)
		{
			return new Quaternion(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return lhs.x == rhs.y && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000D123 File Offset: 0x0000B323
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040001CC RID: 460
		private float w;

		// Token: 0x040001CD RID: 461
		private float x;

		// Token: 0x040001CE RID: 462
		private float y;

		// Token: 0x040001CF RID: 463
		private float z;
	}
}
