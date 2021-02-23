using System;
using System.IO;
using System.Text;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x0200008B RID: 139
	public sealed class RIFFWriter : IDisposable
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0000DF74 File Offset: 0x0000C174
		public RIFFWriter(Stream stream, string characterCode) : this(stream, characterCode, ABinary.SystemEndianness, RIFFWriterOptions.Default)
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000DF84 File Offset: 0x0000C184
		public RIFFWriter(Stream stream, string characterCode, Endianness endianness) : this(stream, characterCode, endianness, RIFFWriterOptions.Default)
		{
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000DF90 File Offset: 0x0000C190
		public RIFFWriter(Stream stream, string characterCode, RIFFWriterOptions options) : this(stream, characterCode, ABinary.SystemEndianness, options)
		{
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
		public RIFFWriter(Stream stream, string characterCode, Endianness endianness, RIFFWriterOptions options)
		{
			this.binaryWriter = new ABinaryWriter(stream, endianness);
			this.options = options;
			if (!options.HasFlag(RIFFWriterOptions.SkipHeader))
			{
				this.binaryWriter.WriteString("RIFF");
				this.binaryWriter.Write32(0U);
				this.startPosition = this.binaryWriter.Position;
				this.binaryWriter.WriteString(RIFFWriter.ValidateCharacterCode(characterCode));
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E01C File Offset: 0x0000C21C
		public void Close()
		{
			if (!this.isClosed)
			{
				if (!this.options.HasFlag(RIFFWriterOptions.SkipHeader))
				{
					long num = this.binaryWriter.Position - this.startPosition;
					this.binaryWriter.Goto(this.startPosition - 4L);
					this.binaryWriter.Write32((uint)num);
					this.binaryWriter.Back();
				}
				this.isClosed = true;
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E08F File Offset: 0x0000C28F
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				if (!this.isClosed)
				{
					this.Close();
				}
				this.binaryWriter.Dispose();
				this.isDisposed = true;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		public void WriteChunk(string characterCode, Action<ABinaryWriter> writeMethod)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("RIFFWriter");
			}
			if (writeMethod == null)
			{
				throw new ArgumentNullException("writeMethod");
			}
			this.binaryWriter.WriteString(RIFFWriter.ValidateCharacterCode(characterCode));
			this.binaryWriter.Write32(0U);
			long position = this.binaryWriter.Position;
			Endianness endianness = this.binaryWriter.Endianness;
			Encoding encoding = this.binaryWriter.Encoding;
			writeMethod(this.binaryWriter);
			this.binaryWriter.Endianness = endianness;
			this.binaryWriter.Encoding = encoding;
			long num = this.binaryWriter.Position - position;
			this.binaryWriter.Goto(position - 4L);
			this.binaryWriter.Write32((uint)num);
			this.binaryWriter.Back();
			if (!this.options.HasFlag(RIFFWriterOptions.SkipAligment))
			{
				this.binaryWriter.Skip(2);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		public void WriteLIST(string characterCode, Action writeMethod)
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("RIFFWriter");
			}
			if (writeMethod == null)
			{
				throw new ArgumentNullException("writeMethod");
			}
			this.binaryWriter.WriteString("LIST");
			this.binaryWriter.Write32(0U);
			long position = this.binaryWriter.Position;
			this.binaryWriter.WriteString(RIFFWriter.ValidateCharacterCode(characterCode));
			Endianness endianness = this.binaryWriter.Endianness;
			Encoding encoding = this.binaryWriter.Encoding;
			writeMethod();
			this.binaryWriter.Endianness = endianness;
			this.binaryWriter.Encoding = encoding;
			long num = this.binaryWriter.Position - position;
			this.binaryWriter.Goto(position - 4L);
			this.binaryWriter.Write32((uint)num);
			this.binaryWriter.Back();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000E27C File Offset: 0x0000C47C
		private static string ValidateCharacterCode(string characterCode)
		{
			if (characterCode == null)
			{
				throw new ArgumentNullException("characterCode");
			}
			if (characterCode.Length < 4)
			{
				characterCode += new string(' ', 4 - characterCode.Length);
			}
			else if (characterCode.Length > 4)
			{
				characterCode = characterCode.Substring(0, 4);
			}
			return characterCode;
		}

		// Token: 0x040001E7 RID: 487
		private ABinaryWriter binaryWriter;

		// Token: 0x040001E8 RID: 488
		private bool isClosed;

		// Token: 0x040001E9 RID: 489
		private bool isDisposed;

		// Token: 0x040001EA RID: 490
		private RIFFWriterOptions options;

		// Token: 0x040001EB RID: 491
		private long startPosition;
	}
}
