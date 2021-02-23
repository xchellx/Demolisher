using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Arookas.IO.Binary
{
	// Token: 0x02000090 RID: 144
	public abstract class ABinary : IDisposable
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000ED36 File Offset: 0x0000CF36
		protected byte[] Buffer
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.buffer;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000ED51 File Offset: 0x0000CF51
		public long BytesRemaining
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.stream.Length - this.Position;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000ED78 File Offset: 0x0000CF78
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000ED93 File Offset: 0x0000CF93
		public Encoding Encoding
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.encoding;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				this.encoding = (value ?? Encoding.ASCII);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		protected int EncodingStride
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				if (this.Encoding == Encoding.Unicode || this.Encoding == Encoding.BigEndianUnicode)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000EDEA File Offset: 0x0000CFEA
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0000EE05 File Offset: 0x0000D005
		public Endianness Endianness
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.endianness;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				this.endianness = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000EE21 File Offset: 0x0000D021
		public bool IsAtBeginningOfStream
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.stream.Position == 0L;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000EE45 File Offset: 0x0000D045
		public bool IsAtEndOfStream
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.stream.Position >= this.stream.Length;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000EE75 File Offset: 0x0000D075
		protected bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000EE7D File Offset: 0x0000D07D
		public long Length
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.stream.Length;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000EE9D File Offset: 0x0000D09D
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public long Position
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.stream.Position - this.positionAnchor;
			}
			set
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				if (value <= 0L)
				{
					this.stream.Position = this.positionAnchor;
					return;
				}
				if (this.positionAnchor + value > this.stream.Length)
				{
					this.stream.Position = this.stream.Length;
					return;
				}
				this.stream.Position = this.positionAnchor + value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000EF3A File Offset: 0x0000D13A
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000EF42 File Offset: 0x0000D142
		public long PositionAnchor
		{
			get
			{
				return this.positionAnchor;
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", value, "The specified position anchor was a negative value.");
				}
				this.positionAnchor = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000EF66 File Offset: 0x0000D166
		protected bool Reverse
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return ABinary.SystemEndianness != this.Endianness;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000EF8B File Offset: 0x0000D18B
		public Stream Stream
		{
			get
			{
				if (this.isDisposed)
				{
					throw new ObjectDisposedException("ABinary");
				}
				return this.stream;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000EFA6 File Offset: 0x0000D1A6
		public static Endianness SystemEndianness
		{
			get
			{
				if (!BitConverter.IsLittleEndian)
				{
					return Endianness.Big;
				}
				return Endianness.Little;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		public static int VariableLengthQuantitySize
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000EFB6 File Offset: 0x0000D1B6
		protected ABinary(Stream stream) : this(stream, ABinary.SystemEndianness, Encoding.ASCII)
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		protected ABinary(Stream stream, Endianness endianness) : this(stream, endianness, Encoding.ASCII)
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000EFD8 File Offset: 0x0000D1D8
		protected ABinary(Stream stream, Encoding encoding) : this(stream, ABinary.SystemEndianness, encoding)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		protected ABinary(Stream stream, Endianness endianness, Encoding encoding)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!endianness.IsDefined<Endianness>())
			{
				throw new ArgumentOutOfRangeException("endianness", endianness, "The specified Arookas.IO.Binary.Endianness value was not a defined Arookas.IO.Binary.Endianness value.");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.stream = stream;
			this.endianness = endianness;
			this.encoding = encoding;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000F057 File Offset: 0x0000D257
		public void Back()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			this.stream.Position = ((this.jumpHistory.Count > 0) ? this.jumpHistory.Pop() : 0L);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000F094 File Offset: 0x0000D294
		public void ClearJumpHistory()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			this.jumpHistory.Clear();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				if (this.stream != null)
				{
					this.stream.Close();
				}
				this.buffer = null;
				this.isDisposed = true;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		protected void DumpBuffer(int stride)
		{
			this.DumpBuffer(stride, 1);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		protected void DumpBuffer(int stride, int count)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			int num = stride * count;
			if (this.Reverse)
			{
				for (int i = 0; i < num; i += stride)
				{
					Array.Reverse(this.buffer, i, stride);
				}
			}
			this.stream.Write(this.buffer, 0, num);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F14C File Offset: 0x0000D34C
		protected void FillBuffer(int stride)
		{
			this.FillBuffer(stride, 1);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F158 File Offset: 0x0000D358
		protected void FillBuffer(int stride, int count)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			int num = stride * count;
			this.ResizeBuffer(num);
			if (this.stream.Read(this.buffer, 0, num) < num)
			{
				throw new EndOfStreamException(string.Format("Failed to read {0} byte(s) before encountering the end of the stream.", num));
			}
			if (this.Reverse)
			{
				for (int i = 0; i < num; i += stride)
				{
					Array.Reverse(this.buffer, i, stride);
				}
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		public void Goto(long position)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			long position2 = this.stream.Position;
			this.Stream.Seek(this.positionAnchor + position, SeekOrigin.Begin);
			this.jumpHistory.Push(position2);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F21D File Offset: 0x0000D41D
		public void Keep()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			this.jumpHistory.Push(this.stream.Position);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000F248 File Offset: 0x0000D448
		public void SetAnchor()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			this.positionAnchor = this.stream.Position;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000F26E File Offset: 0x0000D46E
		public void Skip()
		{
			this.Skip(32);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F278 File Offset: 0x0000D478
		public void Skip(int multiple)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			long num = this.Position % (long)multiple;
			if (num != 0L)
			{
				this.Step((long)multiple - num);
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000F2B1 File Offset: 0x0000D4B1
		public void Step(long position)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			this.Stream.Seek(position, SeekOrigin.Current);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		public void Peek(long position)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			long position2 = this.stream.Position;
			this.Stream.Seek(position, SeekOrigin.End);
			this.jumpHistory.Push(position2);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000F31A File Offset: 0x0000D51A
		protected void ResizeBuffer(int newBufferSize)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			if (this.buffer == null || this.buffer.Length < newBufferSize)
			{
				this.buffer = new byte[newBufferSize];
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000F34E File Offset: 0x0000D54E
		public void ResetAnchor()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("ABinary");
			}
			this.positionAnchor = 0L;
		}

		// Token: 0x040001F5 RID: 501
		private byte[] buffer;

		// Token: 0x040001F6 RID: 502
		private Encoding encoding;

		// Token: 0x040001F7 RID: 503
		private Endianness endianness;

		// Token: 0x040001F8 RID: 504
		private bool isDisposed;

		// Token: 0x040001F9 RID: 505
		private Stack<long> jumpHistory = new Stack<long>(10);

		// Token: 0x040001FA RID: 506
		private long positionAnchor;

		// Token: 0x040001FB RID: 507
		private Stream stream;
	}
}
