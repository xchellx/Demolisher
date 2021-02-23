using System;

namespace Arookas
{
	// Token: 0x02000084 RID: 132
	public struct Int24 : IComparable, IFormattable, IConvertible, IComparable<Int24>, IEquatable<Int24>
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000D91B File Offset: 0x0000BB1B
		public Int24(sbyte value)
		{
			this.value = (int)value;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D924 File Offset: 0x0000BB24
		public Int24(byte value)
		{
			this.value = (int)value;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D92D File Offset: 0x0000BB2D
		public Int24(short value)
		{
			this.value = (int)value;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D936 File Offset: 0x0000BB36
		public Int24(ushort value)
		{
			this.value = (int)value;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D93F File Offset: 0x0000BB3F
		public Int24(Int24 value)
		{
			this.value = value.value;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D94E File Offset: 0x0000BB4E
		public Int24(UInt24 value)
		{
			this.value = value;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000D95C File Offset: 0x0000BB5C
		public Int24(int value)
		{
			this.value = value;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000D965 File Offset: 0x0000BB65
		public Int24(uint value)
		{
			this.value = (int)value;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000D96E File Offset: 0x0000BB6E
		public Int24(long value)
		{
			this.value = (int)value;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000D978 File Offset: 0x0000BB78
		public Int24(ulong value)
		{
			this.value = (int)value;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000D982 File Offset: 0x0000BB82
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is Int24)
			{
				return this.CompareTo((int)value);
			}
			throw new ArgumentException("Int24 may be compared to only Int24.", "value");
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
		public int CompareTo(Int24 value)
		{
			if (value < value.value)
			{
				return -1;
			}
			if (value > value.value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToBoolean(provider);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToByte(provider);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000D9FD File Offset: 0x0000BBFD
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToChar(provider);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000DA10 File Offset: 0x0000BC10
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToDateTime(provider);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000DA23 File Offset: 0x0000BC23
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToDecimal(provider);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000DA36 File Offset: 0x0000BC36
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToDouble(provider);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000DA49 File Offset: 0x0000BC49
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToInt16(provider);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToInt32(provider);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000DA6F File Offset: 0x0000BC6F
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToInt64(provider);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000DA82 File Offset: 0x0000BC82
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToSByte(provider);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000DA95 File Offset: 0x0000BC95
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToSingle(provider);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		string IConvertible.ToString(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToString(provider);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000DABB File Offset: 0x0000BCBB
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToType(conversionType, provider);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000DACF File Offset: 0x0000BCCF
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToUInt16(provider);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000DAE2 File Offset: 0x0000BCE2
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToUInt32(provider);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000DAF5 File Offset: 0x0000BCF5
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToUInt64(provider);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000DB08 File Offset: 0x0000BD08
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000DB0C File Offset: 0x0000BD0C
		public override bool Equals(object obj)
		{
			return obj is Int24 && this.value == ((Int24)obj).value;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000DB2B File Offset: 0x0000BD2B
		public bool Equals(Int24 obj)
		{
			return this.value == obj.value;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		public override int GetHashCode()
		{
			return this.value;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000DB44 File Offset: 0x0000BD44
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000DB51 File Offset: 0x0000BD51
		public string ToString(string format)
		{
			return this.value.ToString(format);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000DB5F File Offset: 0x0000BD5F
		public string ToString(IFormatProvider provider)
		{
			return this.value.ToString(provider);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DB6D File Offset: 0x0000BD6D
		public string ToString(string format, IFormatProvider provider)
		{
			return this.value.ToString(format, provider);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		public static implicit operator Int24(sbyte other)
		{
			return new Int24(other);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public static explicit operator sbyte(Int24 other)
		{
			return (sbyte)other.value;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000DB8E File Offset: 0x0000BD8E
		public static implicit operator Int24(byte other)
		{
			return new Int24(other);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000DB96 File Offset: 0x0000BD96
		public static explicit operator byte(Int24 other)
		{
			return (byte)other.value;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		public static implicit operator Int24(short other)
		{
			return new Int24(other);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public static explicit operator short(Int24 other)
		{
			return (short)other.value;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000DBB2 File Offset: 0x0000BDB2
		public static implicit operator Int24(ushort other)
		{
			return new Int24(other);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000DBBA File Offset: 0x0000BDBA
		public static explicit operator ushort(Int24 other)
		{
			return (ushort)other.value;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public static explicit operator Int24(UInt24 other)
		{
			return new Int24(other);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public static explicit operator UInt24(Int24 other)
		{
			return (UInt24)other.value;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000DBDA File Offset: 0x0000BDDA
		public static explicit operator Int24(int other)
		{
			return new Int24(other);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000DBE2 File Offset: 0x0000BDE2
		public static implicit operator int(Int24 other)
		{
			return other.value;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000DBEB File Offset: 0x0000BDEB
		public static explicit operator Int24(uint other)
		{
			return new Int24(other);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public static explicit operator uint(Int24 other)
		{
			return (uint)other.value;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		public static explicit operator Int24(long other)
		{
			return new Int24(other);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public static implicit operator long(Int24 other)
		{
			return (long)other.value;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000DC0E File Offset: 0x0000BE0E
		public static explicit operator Int24(ulong other)
		{
			return new Int24(other);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000DC16 File Offset: 0x0000BE16
		public static explicit operator ulong(Int24 other)
		{
			return (ulong)((long)other.value);
		}

		// Token: 0x040001D6 RID: 470
		public const int MaxValue = 8388607;

		// Token: 0x040001D7 RID: 471
		public const int MinValue = 8388608;

		// Token: 0x040001D8 RID: 472
		private int value;
	}
}
