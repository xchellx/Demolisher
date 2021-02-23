using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Arookas.IO.Binary
{
	// Token: 0x02000092 RID: 146
	public sealed class ABinaryWriter : ABinary, IDisposable
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x000101B7 File Offset: 0x0000E3B7
		public ABinaryWriter(Stream stream) : this(stream, ABinary.SystemEndianness, Encoding.ASCII)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000101CA File Offset: 0x0000E3CA
		public ABinaryWriter(Stream stream, Endianness endianness) : this(stream, endianness, Encoding.ASCII)
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000101D9 File Offset: 0x0000E3D9
		public ABinaryWriter(Stream stream, Encoding encoding) : this(stream, ABinary.SystemEndianness, encoding)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000101E8 File Offset: 0x0000E3E8
		public ABinaryWriter(Stream stream, Endianness endianness, Encoding encoding) : base(stream, endianness, encoding)
		{
			if (!stream.CanWrite)
			{
				throw new ArgumentException("The specified System.IO.Stream cannot write.", "stream");
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001020C File Offset: 0x0000E40C
		~ABinaryWriter()
		{
			if (!base.IsDisposed)
			{
				base.Dispose();
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00010240 File Offset: 0x0000E440
		public void Write8(byte value)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryWriter");
			}
			base.Stream.WriteByte(value);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00010261 File Offset: 0x0000E461
		public void Write8s(byte[] value)
		{
			this.Write8s(value, 0, value.Length);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001026E File Offset: 0x0000E46E
		public void Write8s(byte[] value, int count)
		{
			this.Write8s(value, 0, count);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00010279 File Offset: 0x0000E479
		public void Write8s(byte[] value, int offset, int count)
		{
			base.Stream.Write(value, offset, count);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00010289 File Offset: 0x0000E489
		public void WriteS8(sbyte value)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryWriter");
			}
			base.ResizeBuffer(1);
			base.Buffer[0] = (byte)value;
			base.DumpBuffer(1);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000102B6 File Offset: 0x0000E4B6
		public void WriteS8s(sbyte[] value)
		{
			this.WriteS8s(value, 0, value.Length);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000102C3 File Offset: 0x0000E4C3
		public void WriteS8s(sbyte[] value, int count)
		{
			this.WriteS8s(value, 0, count);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000102D0 File Offset: 0x0000E4D0
		public void WriteS8s(sbyte[] value, int offset, int count)
		{
			if (count <= 0)
			{
				count = value.Length;
			}
			base.ResizeBuffer(count);
			for (int i = 0; i < count; i++)
			{
				base.Buffer[i] = (byte)value[i + offset];
			}
			base.DumpBuffer(count, 1);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001030F File Offset: 0x0000E50F
		public void Write16(ushort value)
		{
			base.ResizeBuffer(2);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 2);
			base.DumpBuffer(2);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00010333 File Offset: 0x0000E533
		public void Write16s(ushort[] value)
		{
			this.Write16s(value, 0, value.Length);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00010340 File Offset: 0x0000E540
		public void Write16s(ushort[] value, int count)
		{
			this.Write16s(value, 0, count);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001034C File Offset: 0x0000E54C
		public void Write16s(ushort[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 2);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 2, 2);
			}
			base.DumpBuffer(2, count);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010390 File Offset: 0x0000E590
		public void WriteS16(short value)
		{
			base.ResizeBuffer(2);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 2);
			base.DumpBuffer(2);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public void WriteS16s(short[] value)
		{
			this.WriteS16s(value, 0, value.Length);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000103C1 File Offset: 0x0000E5C1
		public void WriteS16s(short[] value, int count)
		{
			this.WriteS16s(value, 0, count);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000103CC File Offset: 0x0000E5CC
		public void WriteS16s(short[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 2);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 2, 2);
			}
			base.DumpBuffer(2, count);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00010410 File Offset: 0x0000E610
		public void Write24(UInt24 value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.ResizeBuffer(3);
			Array.Copy(bytes, 0, base.Buffer, 0, 3);
			base.DumpBuffer(3);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00010446 File Offset: 0x0000E646
		public void Write24s(UInt24[] value)
		{
			this.Write24s(value, 0, value.Length);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00010453 File Offset: 0x0000E653
		public void Write24s(UInt24[] value, int count)
		{
			this.Write24s(value, 0, count);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00010460 File Offset: 0x0000E660
		public void Write24s(UInt24[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 3);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 3, 3);
			}
			base.DumpBuffer(3, count);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000104B4 File Offset: 0x0000E6B4
		public void WriteS24(Int24 value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			base.ResizeBuffer(3);
			Array.Copy(bytes, 0, base.Buffer, 0, 3);
			base.DumpBuffer(3);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000104EA File Offset: 0x0000E6EA
		public void WriteS24s(Int24[] value)
		{
			this.WriteS24s(value, 0, value.Length);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000104F7 File Offset: 0x0000E6F7
		public void WriteS24s(Int24[] value, int count)
		{
			this.WriteS24s(value, 0, count);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00010504 File Offset: 0x0000E704
		public void WriteS24s(Int24[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 3);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 3, 3);
			}
			base.DumpBuffer(3, count);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00010556 File Offset: 0x0000E756
		public void Write32(uint value)
		{
			base.ResizeBuffer(4);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 4);
			base.DumpBuffer(4);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001057A File Offset: 0x0000E77A
		public void Write32s(uint[] value)
		{
			this.Write32s(value, 0, value.Length);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00010587 File Offset: 0x0000E787
		public void Write32s(uint[] value, int count)
		{
			this.Write32s(value, 0, count);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00010594 File Offset: 0x0000E794
		public void Write32s(uint[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 4);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 4, 4);
			}
			base.DumpBuffer(4, count);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public void WriteS32(int value)
		{
			base.ResizeBuffer(4);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 4);
			base.DumpBuffer(4);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000105FC File Offset: 0x0000E7FC
		public void WriteS32s(int[] value)
		{
			this.WriteS32s(value, 0, value.Length);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00010609 File Offset: 0x0000E809
		public void WriteS32s(int[] value, int count)
		{
			this.WriteS32s(value, 0, count);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00010614 File Offset: 0x0000E814
		public void WriteS32s(int[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 4);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 4, 4);
			}
			base.DumpBuffer(4, count);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010658 File Offset: 0x0000E858
		public void Write64(ulong value)
		{
			base.ResizeBuffer(8);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 8);
			base.DumpBuffer(8);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001067C File Offset: 0x0000E87C
		public void Write64s(ulong[] value)
		{
			this.Write64s(value, 0, value.Length);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00010689 File Offset: 0x0000E889
		public void Write64s(ulong[] value, int count)
		{
			this.Write64s(value, 0, count);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010694 File Offset: 0x0000E894
		public void Write64s(ulong[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 8);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 8, 8);
			}
			base.DumpBuffer(8, count);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000106D8 File Offset: 0x0000E8D8
		public void WriteS64(long value)
		{
			base.ResizeBuffer(8);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 8);
			base.DumpBuffer(8);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000106FC File Offset: 0x0000E8FC
		public void WriteS64s(long[] value)
		{
			this.WriteS64s(value, 0, value.Length);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00010709 File Offset: 0x0000E909
		public void WriteS64s(long[] value, int count)
		{
			this.WriteS64s(value, 0, count);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00010714 File Offset: 0x0000E914
		public void WriteS64s(long[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 8);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 8, 8);
			}
			base.DumpBuffer(8, count);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00010758 File Offset: 0x0000E958
		public void WriteUIntVar(ulong value)
		{
			this.Write8s(this.GetUIntVarBytes(value));
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00010767 File Offset: 0x0000E967
		public void WriteUIntVars(ulong[] value)
		{
			this.WriteUIntVars(value, 0, value.Length);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00010774 File Offset: 0x0000E974
		public void WriteUIntVars(ulong[] value, int count)
		{
			this.WriteUIntVars(value, 0, count);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00010780 File Offset: 0x0000E980
		public void WriteUIntVars(ulong[] value, int offset, int count)
		{
			int num = 0;
			base.ResizeBuffer(ABinary.VariableLengthQuantitySize * count);
			for (int i = 0; i < count; i++)
			{
				byte[] uintVarBytes = this.GetUIntVarBytes(value[i + offset]);
				Array.Copy(uintVarBytes, 0, base.Buffer, num, uintVarBytes.Length);
			}
			base.DumpBuffer(1, num);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000107D0 File Offset: 0x0000E9D0
		private byte[] GetUIntVarBytes(ulong value)
		{
			List<byte> list = new List<byte>(ABinary.VariableLengthQuantitySize);
			do
			{
				byte item = (byte)((value & 127UL) | 128UL);
				list.Add(item);
				value >>= 7;
			}
			while (value > 0UL);
			if (base.Endianness != Endianness.Little)
			{
				list.Reverse();
			}
			List<byte> list2;
			int index;
			(list2 = list)[index = list.Count - 1] = ((byte)(list2[index] & 127));
			return list.ToArray();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00010839 File Offset: 0x0000EA39
		public void WriteSingle(float value)
		{
			base.ResizeBuffer(4);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 4);
			base.DumpBuffer(4);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001085D File Offset: 0x0000EA5D
		public void WriteSingles(float[] value)
		{
			this.WriteSingles(value, 0, value.Length);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001086A File Offset: 0x0000EA6A
		public void WriteSingles(float[] value, int count)
		{
			this.WriteSingles(value, 0, count);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00010878 File Offset: 0x0000EA78
		public void WriteSingles(float[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 4);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 4, 4);
			}
			base.DumpBuffer(4, count);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000108BC File Offset: 0x0000EABC
		public void WriteDouble(double value)
		{
			base.ResizeBuffer(8);
			Array.Copy(BitConverter.GetBytes(value), 0, base.Buffer, 0, 8);
			base.DumpBuffer(8);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000108E0 File Offset: 0x0000EAE0
		public void WriteDoubles(double[] value)
		{
			this.WriteDoubles(value, 0, value.Length);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000108ED File Offset: 0x0000EAED
		public void WriteDoubles(double[] value, int count)
		{
			this.WriteDoubles(value, 0, count);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000108F8 File Offset: 0x0000EAF8
		public void WriteDoubles(double[] value, int offset, int count)
		{
			base.ResizeBuffer(count * 8);
			for (int i = 0; i < count; i++)
			{
				Array.Copy(BitConverter.GetBytes(value[i + offset]), 0, base.Buffer, i * 8, 8);
			}
			base.DumpBuffer(8, count);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001093C File Offset: 0x0000EB3C
		public void WriteChar(char character)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryWriter");
			}
			int encodingStride = base.EncodingStride;
			base.ResizeBuffer(encodingStride);
			Array.Copy(base.Encoding.GetBytes(new string(character, 1)), 0, base.Buffer, 0, encodingStride);
			base.DumpBuffer(encodingStride);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00010991 File Offset: 0x0000EB91
		public void WriteChars(char[] value)
		{
			this.WriteChars(value, 0, value.Length);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001099E File Offset: 0x0000EB9E
		public void WriteChars(char[] value, int count)
		{
			this.WriteChars(value, 0, count);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x000109AC File Offset: 0x0000EBAC
		public void WriteChars(char[] value, int offset, int count)
		{
			int encodingStride = base.EncodingStride;
			base.ResizeBuffer(count * encodingStride);
			Array.Copy(base.Encoding.GetBytes(value, offset, count), 0, base.Buffer, 0, count * encodingStride);
			base.DumpBuffer(encodingStride, count);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000109EF File Offset: 0x0000EBEF
		public void WriteString(string value)
		{
			this.WriteString(value, ABinaryStringFormat.Raw);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000109FD File Offset: 0x0000EBFD
		public void WriteStrings(string[] value)
		{
			this.WriteStrings(value, 0, value.Length, ABinaryStringFormat.Raw);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010A0F File Offset: 0x0000EC0F
		public void WriteStrings(string[] value, int count)
		{
			this.WriteStrings(value, 0, count, ABinaryStringFormat.Raw);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00010A20 File Offset: 0x0000EC20
		public void WriteStrings(string[] value, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteString(value[i + offset], ABinaryStringFormat.Raw);
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00010A4C File Offset: 0x0000EC4C
		public void WriteString(string value, ABinaryStringFormat format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format", "String format may not be null.");
			}
			if (format.Type == ABinaryStringFormatType.RawWithLength)
			{
				this.WriteS32(value.Length);
			}
			if (format.Type == ABinaryStringFormatType.Clamped)
			{
				if (value.Length > format.Multiple)
				{
					value = value.Substring(0, format.Multiple);
				}
				this.WriteChars(value.ToCharArray());
				if (value.Length < format.Multiple)
				{
					int num = format.Multiple - value.Length;
					while (num-- > 0)
					{
						base.Stream.WriteByte(0);
					}
				}
			}
			else
			{
				this.WriteChars(value.ToCharArray());
			}
			if (format.Type == ABinaryStringFormatType.NullTerminated)
			{
				base.Stream.WriteByte(0);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00010B08 File Offset: 0x0000ED08
		public void WriteStrings(string[] value, ABinaryStringFormat format)
		{
			this.WriteStrings(value, 0, value.Length, format);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00010B16 File Offset: 0x0000ED16
		public void WriteStrings(string[] value, int count, ABinaryStringFormat format)
		{
			this.WriteStrings(value, 0, count, format);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00010B24 File Offset: 0x0000ED24
		public void WriteStrings(string[] value, int offset, int count, ABinaryStringFormat format)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteString(value[i + offset], format);
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00010B4C File Offset: 0x0000ED4C
		public void WritePadding(int multiple, byte padding)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryWriter");
			}
			int num = (int)((long)multiple - base.Stream.Position % (long)multiple);
			if (num > 0)
			{
				byte[] array = new byte[num];
				if (padding != 0)
				{
					for (int i = 0; i < num; i++)
					{
						array[i] = padding;
					}
				}
				base.Stream.Write(array, 0, num);
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00010BAC File Offset: 0x0000EDAC
		public void WritePadding(int multiple, byte[] padding)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ABinaryWriter");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			if (padding.Length == 0)
			{
				throw new ArgumentException("Padding byte array is empty.");
			}
			int num = (int)((long)multiple - base.Stream.Position % (long)multiple);
			if (num > 0)
			{
				byte[] array = new byte[num];
				int i = 0;
				int num2 = 0;
				while (i < num)
				{
					array[i] = padding[num2];
					if (++num2 >= padding.Length)
					{
						num2 = 0;
					}
					i++;
				}
				base.Stream.Write(array, 0, num);
			}
		}
	}
}
