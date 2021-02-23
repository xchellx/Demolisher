using System;

namespace Arookas
{
	// Token: 0x02000083 RID: 131
	public struct UInt24 : IComparable, IFormattable, IConvertible, IComparable<UInt24>, IEquatable<UInt24>
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000D600 File Offset: 0x0000B800
		public UInt24(sbyte value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000D609 File Offset: 0x0000B809
		public UInt24(byte value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000D612 File Offset: 0x0000B812
		public UInt24(short value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000D61B File Offset: 0x0000B81B
		public UInt24(ushort value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D624 File Offset: 0x0000B824
		public UInt24(Int24 value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D632 File Offset: 0x0000B832
		public UInt24(UInt24 value)
		{
			this.value = value.value;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000D641 File Offset: 0x0000B841
		public UInt24(int value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D64A File Offset: 0x0000B84A
		public UInt24(uint value)
		{
			this.value = value;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D653 File Offset: 0x0000B853
		public UInt24(long value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000D65D File Offset: 0x0000B85D
		public UInt24(ulong value)
		{
			this.value = (uint)value;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D667 File Offset: 0x0000B867
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is UInt24)
			{
				return this.CompareTo((int)value);
			}
			throw new ArgumentException("UInt24 may be compared to only UInt24.", "value");
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000D697 File Offset: 0x0000B897
		public int CompareTo(UInt24 value)
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

		// Token: 0x060003DD RID: 989 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToBoolean(provider);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000D6CF File Offset: 0x0000B8CF
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToByte(provider);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000D6E2 File Offset: 0x0000B8E2
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToChar(provider);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000D6F5 File Offset: 0x0000B8F5
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToDateTime(provider);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000D708 File Offset: 0x0000B908
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToDecimal(provider);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000D71B File Offset: 0x0000B91B
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToDouble(provider);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000D72E File Offset: 0x0000B92E
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToInt16(provider);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D741 File Offset: 0x0000B941
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToInt32(provider);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000D754 File Offset: 0x0000B954
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToInt64(provider);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D767 File Offset: 0x0000B967
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToSByte(provider);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000D77A File Offset: 0x0000B97A
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToSingle(provider);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D78D File Offset: 0x0000B98D
		string IConvertible.ToString(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToString(provider);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToType(conversionType, provider);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToUInt16(provider);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000D7C7 File Offset: 0x0000B9C7
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToUInt32(provider);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000D7DA File Offset: 0x0000B9DA
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)this.value).ToUInt64(provider);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000D7ED File Offset: 0x0000B9ED
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt32;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D7F1 File Offset: 0x0000B9F1
		public override bool Equals(object obj)
		{
			return obj is UInt24 && this.value == ((UInt24)obj).value;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000D810 File Offset: 0x0000BA10
		public bool Equals(UInt24 obj)
		{
			return this.value == obj.value;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000D821 File Offset: 0x0000BA21
		public override int GetHashCode()
		{
			return (int)this.value;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000D829 File Offset: 0x0000BA29
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D836 File Offset: 0x0000BA36
		public string ToString(string format)
		{
			return this.value.ToString(format);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D844 File Offset: 0x0000BA44
		public string ToString(IFormatProvider provider)
		{
			return this.value.ToString(provider);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000D852 File Offset: 0x0000BA52
		public string ToString(string format, IFormatProvider provider)
		{
			return this.value.ToString(format, provider);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D861 File Offset: 0x0000BA61
		public static explicit operator UInt24(sbyte other)
		{
			return new UInt24(other);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000D869 File Offset: 0x0000BA69
		public static explicit operator sbyte(UInt24 other)
		{
			return (sbyte)other.value;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D873 File Offset: 0x0000BA73
		public static implicit operator UInt24(byte other)
		{
			return new UInt24(other);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000D87B File Offset: 0x0000BA7B
		public static explicit operator byte(UInt24 other)
		{
			return (byte)other.value;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000D885 File Offset: 0x0000BA85
		public static explicit operator UInt24(short other)
		{
			return new UInt24(other);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000D88D File Offset: 0x0000BA8D
		public static explicit operator short(UInt24 other)
		{
			return (short)other.value;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000D897 File Offset: 0x0000BA97
		public static implicit operator UInt24(ushort other)
		{
			return new UInt24(other);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000D89F File Offset: 0x0000BA9F
		public static explicit operator ushort(UInt24 other)
		{
			return (ushort)other.value;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000D8A9 File Offset: 0x0000BAA9
		public static explicit operator UInt24(Int24 other)
		{
			return new UInt24(other);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000D8B1 File Offset: 0x0000BAB1
		public static explicit operator Int24(UInt24 other)
		{
			return (Int24)other.value;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000D8BF File Offset: 0x0000BABF
		public static explicit operator UInt24(int other)
		{
			if ((long)other < 0L || (long)other > 16777215L)
			{
				throw new InvalidCastException();
			}
			return new UInt24(other);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000D8DD File Offset: 0x0000BADD
		public static implicit operator int(UInt24 other)
		{
			return (int)other.value;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000D8E6 File Offset: 0x0000BAE6
		public static explicit operator UInt24(uint other)
		{
			return new UInt24(other);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000D8EE File Offset: 0x0000BAEE
		public static implicit operator uint(UInt24 other)
		{
			return other.value;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D8F7 File Offset: 0x0000BAF7
		public static explicit operator UInt24(long other)
		{
			return new UInt24(other);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D8FF File Offset: 0x0000BAFF
		public static implicit operator long(UInt24 other)
		{
			return (long)((ulong)other.value);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D909 File Offset: 0x0000BB09
		public static explicit operator UInt24(ulong other)
		{
			return new UInt24(other);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000D911 File Offset: 0x0000BB11
		public static implicit operator ulong(UInt24 other)
		{
			return (ulong)other.value;
		}

		// Token: 0x040001D3 RID: 467
		public const uint MaxValue = 16777215U;

		// Token: 0x040001D4 RID: 468
		public const uint MinValue = 0U;

		// Token: 0x040001D5 RID: 469
		private uint value;
	}
}
