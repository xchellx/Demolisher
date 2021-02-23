using System;

namespace Arookas.Math
{
	// Token: 0x02000080 RID: 128
	public struct Vector3D
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000D12F File Offset: 0x0000B32F
		public float Magnitude
		{
			get
			{
				return (float)Math.Sqrt((double)this.MagnitudeSquared);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000D13E File Offset: 0x0000B33E
		public float MagnitudeSquared
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000D169 File Offset: 0x0000B369
		public static Vector3D NegateX
		{
			get
			{
				return new Vector3D(-1f, 1f, 1f);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000D17F File Offset: 0x0000B37F
		public static Vector3D NegateY
		{
			get
			{
				return new Vector3D(1f, -1f, 1f);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D195 File Offset: 0x0000B395
		public static Vector3D NegateZ
		{
			get
			{
				return new Vector3D(1f, -1f, 1f);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000D1AB File Offset: 0x0000B3AB
		public Vector3D Normalized
		{
			get
			{
				return Vector3D.Normalize(this);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public static Vector3D One
		{
			get
			{
				return new Vector3D(1f);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
		public static Vector3D UnitX
		{
			get
			{
				return new Vector3D(1f, 0f, 0f);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D1DA File Offset: 0x0000B3DA
		public static Vector3D UnitY
		{
			get
			{
				return new Vector3D(0f, 1f, 0f);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		public static Vector3D UnitZ
		{
			get
			{
				return new Vector3D(0f, 0f, 1f);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000D206 File Offset: 0x0000B406
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000D20E File Offset: 0x0000B40E
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D217 File Offset: 0x0000B417
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000D21F File Offset: 0x0000B41F
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D228 File Offset: 0x0000B428
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000D230 File Offset: 0x0000B430
		public float Z
		{
			get
			{
				return this.z;
			}
			set
			{
				this.z = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000D23C File Offset: 0x0000B43C
		public static Vector3D Zero
		{
			get
			{
				return default(Vector3D);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000D254 File Offset: 0x0000B454
		public Vector3D(float scalar)
		{
			this.z = scalar;
			this.y = scalar;
			this.x = scalar;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D27A File Offset: 0x0000B47A
		public Vector3D(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000D294 File Offset: 0x0000B494
		public static Vector3D CalculateCrossProduct(Vector3D first, Vector3D second)
		{
			return new Vector3D(first.y * second.z - first.z * second.y, first.z * second.x - first.x * second.z, first.x * second.y - first.y * second.x);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000D304 File Offset: 0x0000B504
		public static float CalculateDistance(Vector3D from, Vector3D to)
		{
			return (from - to).Magnitude;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000D320 File Offset: 0x0000B520
		public static float CalculateDotProduct(Vector3D first, Vector3D second)
		{
			return first.x * second.x + first.y * second.y + first.z * second.z;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000D351 File Offset: 0x0000B551
		public override bool Equals(object obj)
		{
			return obj is Vector3D && (Vector3D)obj == this;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000D36E File Offset: 0x0000B56E
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000D393 File Offset: 0x0000B593
		public void InvertHandedness()
		{
			this.z = -this.z;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000D3A2 File Offset: 0x0000B5A2
		public static Vector3D Lerp(Vector3D start, Vector3D end, float percentage)
		{
			return start * (1f - percentage) + end * percentage;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000D3BD File Offset: 0x0000B5BD
		public static Vector3D Normalize(Vector3D vector3D)
		{
			if (vector3D == Vector3D.Zero)
			{
				return Vector3D.Zero;
			}
			return vector3D / vector3D.Magnitude;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000D3DF File Offset: 0x0000B5DF
		public override string ToString()
		{
			return string.Format("({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000D40C File Offset: 0x0000B60C
		public static Vector3D operator +(Vector3D lhs, Vector3D rhs)
		{
			return new Vector3D(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000D440 File Offset: 0x0000B640
		public static Vector3D operator -(Vector3D vector3D)
		{
			return new Vector3D(-vector3D.x, -vector3D.y, -vector3D.z);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000D45F File Offset: 0x0000B65F
		public static Vector3D operator -(Vector3D lhs, Vector3D rhs)
		{
			return new Vector3D(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000D493 File Offset: 0x0000B693
		public static Vector3D operator *(Vector3D lhs, Vector3D rhs)
		{
			return new Vector3D(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000D4C7 File Offset: 0x0000B6C7
		public static Vector3D operator *(Vector3D lhs, float rhs)
		{
			return lhs * new Vector3D(rhs);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000D4D5 File Offset: 0x0000B6D5
		public static Vector3D operator *(float lhs, Vector3D rhs)
		{
			return new Vector3D(lhs) * rhs;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000D4E3 File Offset: 0x0000B6E3
		public static Vector3D operator /(Vector3D lhs, Vector3D rhs)
		{
			return new Vector3D(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000D517 File Offset: 0x0000B717
		public static Vector3D operator /(Vector3D lhs, float rhs)
		{
			return lhs / new Vector3D(rhs);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000D525 File Offset: 0x0000B725
		public static bool operator ==(Vector3D lhs, Vector3D rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000D559 File Offset: 0x0000B759
		public static bool operator !=(Vector3D lhs, Vector3D rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040001D0 RID: 464
		private float x;

		// Token: 0x040001D1 RID: 465
		private float y;

		// Token: 0x040001D2 RID: 466
		private float z;
	}
}
