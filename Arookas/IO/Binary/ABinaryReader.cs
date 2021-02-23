using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Arookas.Math;

namespace Arookas.IO.Binary
{
	// Token: 0x02000091 RID: 145
	public sealed class ABinaryReader : ABinary, IDisposable
	{
		// Token: 0x17000146 RID: 326
		public byte this[long position]
		{
			get
			{
				base.Goto(position);
				byte result = this.Read8();
				base.Back();
				return result;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000F38E File Offset: 0x0000D58E
		public ABinaryReader(Stream stream) : this(stream, ABinary.SystemEndianness, Encoding.ASCII)
		{
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000F3A1 File Offset: 0x0000D5A1
		public ABinaryReader(Stream stream, Endianness endianness) : this(stream, endianness, Encoding.ASCII)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		public ABinaryReader(Stream stream, Encoding encoding) : this(stream, ABinary.SystemEndianness, encoding)
		{
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000F3BF File Offset: 0x0000D5BF
		public ABinaryReader(Stream stream, Endianness endianness, Encoding encoding) : base(stream, endianness, encoding)
		{
			if (!stream.CanRead)
			{
				throw new NotSupportedException("The specified System.IO.Stream does not support reading.");
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000F3DD File Offset: 0x0000D5DD
		public int Read(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.Read(buffer, 0, buffer.Length);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public int Read(byte[] buffer, int count)
		{
			return this.Read(buffer, 0, count);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000F404 File Offset: 0x0000D604
		public int Read(byte[] buffer, int startingIndex, int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (startingIndex < 0 || startingIndex >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("startingIndex", startingIndex, "The specified start index is negative or larger than the buffer size.");
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", count, "The specified count is negative or larger than the buffer size.");
			}
			if (startingIndex + count > buffer.Length)
			{
				throw new ArgumentException("The sum of the specified starting index and count is larger than the buffer size.");
			}
			return base.Stream.Read(buffer, startingIndex, count);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000F494 File Offset: 0x0000D694
		public byte Read8()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			byte result;
			if (!base.Stream.TryReadByte(out result))
			{
				throw new EndOfStreamException("Failed to read one byte before encountering the end of the stream.");
			}
			return result;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000F4D0 File Offset: 0x0000D6D0
		public byte[] Read8s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "The specified count was negative.");
			}
			byte[] array = new byte[count];
			if (base.Stream.Read(array, 0, count) < count)
			{
				throw new EndOfStreamException(string.Format("Failed to read {0} byte(s).", count));
			}
			return array;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000F539 File Offset: 0x0000D739
		public sbyte ReadS8()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			return this.Read8().AsSByte();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000F568 File Offset: 0x0000D768
		public sbyte[] ReadS8s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(1, count);
			return CollectionHelper.Initialize<sbyte>(count, (int index) => base.Buffer[index].AsSByte());
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000F597 File Offset: 0x0000D797
		public ushort Read16()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(2);
			return BitConverter.ToUInt16(base.Buffer, 0);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public ushort[] Read16s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			ushort[] array = new ushort[count];
			base.FillBuffer(2, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = BitConverter.ToUInt16(base.Buffer, i * 2);
			}
			return array;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000F60D File Offset: 0x0000D80D
		public short ReadS16()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(2);
			return BitConverter.ToInt16(base.Buffer, 0);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000F638 File Offset: 0x0000D838
		public short[] ReadS16s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			short[] array = new short[count];
			base.FillBuffer(2, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = BitConverter.ToInt16(base.Buffer, i * 2);
			}
			return array;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000F685 File Offset: 0x0000D885
		public UInt24 Read24()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(3);
			return (UInt24)((uint)((int)base.Buffer[2] << 16 | (int)base.Buffer[1] << 8 | (int)base.Buffer[0]));
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		public UInt24[] Read24s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			UInt24[] array = new UInt24[count];
			base.FillBuffer(3, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = (UInt24)((int)base.Buffer[3 * i + 2] << 16 | (int)base.Buffer[3 * i + 1] << 8 | (int)base.Buffer[3 * i]);
			}
			return array;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000F73E File Offset: 0x0000D93E
		public uint Read32()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(4);
			return BitConverter.ToUInt32(base.Buffer, 0);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000F768 File Offset: 0x0000D968
		public uint[] Read32s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			uint[] array = new uint[count];
			base.FillBuffer(4, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = BitConverter.ToUInt32(base.Buffer, i * 4);
			}
			return array;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000F7B5 File Offset: 0x0000D9B5
		public int ReadS32()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(4);
			return BitConverter.ToInt32(base.Buffer, 0);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public int[] ReadS32s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			int[] array = new int[count];
			base.FillBuffer(4, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = BitConverter.ToInt32(base.Buffer, i * 4);
			}
			return array;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000F82D File Offset: 0x0000DA2D
		public ulong Read64()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(8);
			return BitConverter.ToUInt64(base.Buffer, 0);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000F858 File Offset: 0x0000DA58
		public ulong[] Read64s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			ulong[] array = new ulong[count];
			base.FillBuffer(8, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = BitConverter.ToUInt64(base.Buffer, i * 8);
			}
			return array;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000F8A5 File Offset: 0x0000DAA5
		public long ReadS64()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(8);
			return BitConverter.ToInt64(base.Buffer, 0);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		public long[] ReadS64s(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			long[] array = new long[count];
			base.FillBuffer(8, count);
			for (int i = 0; i < count; i++)
			{
				array[i] = BitConverter.ToInt64(base.Buffer, i * 8);
			}
			return array;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000F920 File Offset: 0x0000DB20
		public ulong ReadUIntVar()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			List<byte> list = new List<byte>(ABinary.VariableLengthQuantitySize);
			while (list.Count <= ABinary.VariableLengthQuantitySize)
			{
				list.Add(this.Read8());
				if ((list[list.Count - 1] & 128) == 0)
				{
					if (base.Reverse)
					{
						list.Reverse();
					}
					ulong num = 0UL;
					int i = 0;
					int num2 = 0;
					while (i < list.Count)
					{
						num |= (ulong)((ulong)(list[i] & 127) << num2);
						i++;
						num2 += 7;
					}
					return num;
				}
			}
			throw new Exception(string.Format("UIntVar is larger than {0} bytes.", ABinary.VariableLengthQuantitySize));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		public ulong[] ReadUIntVars(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			ulong[] array = new ulong[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.ReadUIntVar();
			}
			return array;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000FA0D File Offset: 0x0000DC0D
		public float ReadSingle()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(4);
			return BitConverter.ToSingle(base.Buffer, 0);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public float[] ReadSingles(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "The specified count was negative.");
			}
			base.FillBuffer(4, count);
			return CollectionHelper.Initialize<float>(count, (int index) => BitConverter.ToSingle(base.Buffer, index * 4));
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000FA9C File Offset: 0x0000DC9C
		public double ReadDouble()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			base.FillBuffer(8);
			return BitConverter.ToDouble(base.Buffer, 0);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public double[] ReadDoubles(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "The specified count was negative.");
			}
			base.FillBuffer(8, count);
			return CollectionHelper.Initialize<double>(count, (int index) => BitConverter.ToDouble(base.Buffer, index * 8));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000FB28 File Offset: 0x0000DD28
		public Vector3D ReadVector3D()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			return new Vector3D(this.ReadSingle(), this.ReadSingle(), this.ReadSingle());
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public Vector3D[] ReadVector3Ds(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "The specified count was negative.");
			}
			return CollectionHelper.Initialize<Vector3D>(count, () => this.ReadVector3D());
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000FBA8 File Offset: 0x0000DDA8
		public char ReadCharacter()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			int encodingStride = base.EncodingStride;
			base.FillBuffer(encodingStride);
			return base.Encoding.GetChars(base.Buffer, 0, encodingStride)[0];
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000FBEC File Offset: 0x0000DDEC
		public char[] ReadCharacters(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			int encodingStride = base.EncodingStride;
			base.FillBuffer(encodingStride, count);
			return base.Encoding.GetChars(base.Buffer, 0, encodingStride * count);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000FC30 File Offset: 0x0000DE30
		public string ReadRawString(int length)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			return new string(this.ReadCharacters(length));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000FC54 File Offset: 0x0000DE54
		public string[] ReadRawStrings(int count, int length)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Count needs to be positive.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Length needs to be positive.");
			}
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = new string(this.ReadCharacters(length));
			}
			return array;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000FCCC File Offset: 0x0000DECC
		public string ReadNullTerminatedString()
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			long position = base.Position;
			while (this.Read8() > 0)
			{
			}
			int length = (int)(base.Position - position) - 1;
			base.Position = position;
			return this.ReadRawString(length);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000FD18 File Offset: 0x0000DF18
		public string[] ReadNullTerminatedStrings(int count)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Count needs to be positive.");
			}
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.ReadNullTerminatedString();
			}
			return array;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000FD70 File Offset: 0x0000DF70
		public string ReadClampedString(int length)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			string text = new string(this.ReadCharacters(length));
			if (text.Contains("\0"))
			{
				return text.Substring(0, text.IndexOf('\0'));
			}
			return text;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000FDBC File Offset: 0x0000DFBC
		public string[] ReadClampedStrings(int count, int length)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", count, "Count needs to be positive.");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", length, "Length needs to be positive.");
			}
			string[] array = new string[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.ReadClampedString(length);
			}
			return array;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000FE30 File Offset: 0x0000E030
		public long[] FindAll(byte[] searchFor)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			List<long> list = new List<long>(10);
			long position = base.Stream.Position;
			while (this.FindNext(searchFor))
			{
				List<long> list2 = list;
				long position2;
				base.Position = (position2 = base.Position) + 1L;
				list2.Add(position2);
			}
			base.Stream.Position = position;
			return list.ToArray();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000FE9C File Offset: 0x0000E09C
		public long[] FindAll(string searchFor, Encoding encoding, CultureInfo cultureInfo, CaseSensitivity caseSensitivity)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (searchFor == null)
			{
				throw new ArgumentNullException("searchFor");
			}
			if (searchFor.Length == 0)
			{
				throw new ArgumentException("String for which to search is empty.", "searchFor");
			}
			switch (caseSensitivity)
			{
			case CaseSensitivity.CaseSensitive:
			{
				byte[] bytes = encoding.GetBytes(searchFor);
				return this.FindAll(bytes);
			}
			case CaseSensitivity.CaseInsensitive:
			{
				byte[] bytes2 = encoding.GetBytes(searchFor.ToUpper(cultureInfo));
				byte[] bytes3 = encoding.GetBytes(searchFor.ToLower(cultureInfo));
				return this.FindAll(bytes2, bytes3);
			}
			default:
				throw new ArgumentOutOfRangeException("caseSensitivity", "Case-sensitivity value is out of range.");
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000FF3C File Offset: 0x0000E13C
		private long[] FindAll(byte[] searchForUpper, byte[] searchForLower)
		{
			List<long> list = new List<long>(10);
			long position = base.Position;
			while (this.FindNext(searchForUpper, searchForLower))
			{
				List<long> list2 = list;
				long position2;
				base.Position = (position2 = base.Position) + 1L;
				list2.Add(position2);
			}
			base.Position = position;
			return list.ToArray();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000FF8C File Offset: 0x0000E18C
		public bool FindNext(byte[] searchFor)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (searchFor == null)
			{
				throw new ArgumentNullException("searchFor");
			}
			if (searchFor.Length == 0)
			{
				throw new ArgumentException("searchFor is an empty array.");
			}
			long position = base.Position;
			while (!base.IsAtEndOfStream)
			{
				long position2 = base.Position;
				byte[] array = this.Read8s((int)System.Math.Min(4096L, base.BytesRemaining));
				for (int i = 0; i <= array.Length - searchFor.Length; i++)
				{
					bool flag = true;
					for (int j = 0; j < searchFor.Length; j++)
					{
						if (array[i + j] != searchFor[j])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						base.Position = position2 + (long)i;
						return true;
					}
				}
				base.Position -= (long)(searchFor.Length - 1);
			}
			base.Position = position;
			return false;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00010064 File Offset: 0x0000E264
		public bool FindNext(string searchFor, Encoding encoding, CultureInfo cultureInfo, CaseSensitivity caseSensitivity)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryReader");
			}
			if (searchFor == null)
			{
				throw new ArgumentNullException("searchFor");
			}
			if (searchFor.Length == 0)
			{
				throw new ArgumentException("String for which to search is empty.", "searchFor");
			}
			switch (caseSensitivity)
			{
			case CaseSensitivity.CaseSensitive:
			{
				byte[] bytes = encoding.GetBytes(searchFor);
				return this.FindNext(bytes);
			}
			case CaseSensitivity.CaseInsensitive:
			{
				byte[] bytes2 = encoding.GetBytes(searchFor.ToUpper(cultureInfo));
				byte[] bytes3 = encoding.GetBytes(searchFor.ToLower(cultureInfo));
				return this.FindNext(bytes2, bytes3);
			}
			default:
				throw new ArgumentOutOfRangeException("caseSensitivity", "Case-sensitivity value is out-of-range.");
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00010104 File Offset: 0x0000E304
		private bool FindNext(byte[] searchForUpper, byte[] searchForLower)
		{
			long position = base.Position;
			while (!base.IsAtEndOfStream)
			{
				long position2 = base.Position;
				byte[] array = this.Read8s((int)System.Math.Min(4096L, base.BytesRemaining));
				for (int i = 0; i <= array.Length - searchForUpper.Length; i++)
				{
					bool flag = true;
					for (int j = 0; j < searchForUpper.Length; j++)
					{
						if (array[i + j] != searchForUpper[j] && array[i + j] != searchForLower[j])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						base.Position = position2 + (long)i;
						return true;
					}
				}
				base.Position -= (long)(searchForUpper.Length - 1);
			}
			base.Position = position;
			return false;
		}
	}
}
