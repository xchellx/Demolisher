using arookas.IO;
using arookas.IO.Binary;
using System;
using System.IO;
using System.Text;

namespace arookas.Demolisher {
	internal static class Yay0 {
		const uint cIdentifier = 0x59617930u;

		public static bool IsCompressed(string fileName) {
			if (fileName == null) {
				throw new ArgumentNullException("bytes");
			}
			using (var stream = File.OpenRead(fileName)) {
				var reader = new aBinaryReader(stream, Endianness.Big, Encoding.GetEncoding(932));
				if (reader.Length < 0x10) {
					return false;
				}
				return reader.Read32() == cIdentifier;
			}
		}
		public static Stream Decompress(string file) {
			if (!IsCompressed(file)) {
				return File.OpenRead(file);
			}
			var compressed = File.ReadAllBytes(file);
			using (var stream = new MemoryStream(compressed)) {
				var code = new aBinaryReader(compressed, );
			}
			using (var codeReader = new aBinaryReader(, Endianness.Big, Encoding.GetEncoding(932)),
					countReader = new aBinaryReader(compressed.ToMemoryStream(false, true), Endianness.Big, Encoding.GetEncoding(932)),
					dataReader = new aBinaryReader(compressed.ToMemoryStream(false, true), Endianness.Big, Encoding.GetEncoding(932)))
			{
				// Parse the header.
				var headerIdentifier = codeReader.ReadString(4);
				var uncompressedSize = codeReader.ReadS32();
				countReader.Position = codeReader.ReadS32();
				dataReader.Position = codeReader.ReadS32();
				var decompressed = new byte[uncompressedSize];
				int outPosition = 0;
				uint validBitsCount = 0;
				byte currentCodeByte = 0;

				while (outPosition < uncompressedSize)
				{
					if (validBitsCount <= 0)
					{
						currentCodeByte = codeReader.Read8();
						validBitsCount = 8;
					}

					// If the next bit in the code byte is a 1, do a direct, 1:1 copy of the next data byte; otherwise, uncompress a chunk of data.
					if ((currentCodeByte & 0x80) == 0x80)
					{
						decompressed[outPosition++] = dataReader.Read8();
					}
					else
					{
						var count = countReader.Read16();
						var distance = (count & 0xFFF);
						var startOffset = (outPosition - (distance + 1));
						var byteCount = ((count >> 12) & 0xF);
						if (byteCount == 0)
						{
							byteCount = (dataReader.Read8() + 0x10);
						}
						byteCount += 2;
						Repeater.Repeat(byteCount, () => decompressed[outPosition++] = decompressed[startOffset++]);
					}
					currentCodeByte <<= 1;
					validBitsCount--;
				}
				return new MemoryStream(decompressed);
			}
		}
	}
}