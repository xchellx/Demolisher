using arookas.IO;
using arookas.IO.Binary;
using System;
using System.IO;
using System.Text;

namespace arookas.Demolisher
{
	internal static class Yay0
	{
		public const uint Identifier = 0x59617930;

		public static bool IsCompressed(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("bytes");
			}

			using (ABinaryReader binaryReader = new ABinaryReader(File.OpenRead(fileName), Endianness.Big, Encoding.GetEncoding(932)))
			{
				if (binaryReader.Length < 0x10)
				{
					return false;
				}

				return binaryReader.Read32() == Identifier;
			}
		}
		public static Stream Decompress(string fileName)
		{
			if (!IsCompressed(fileName))
			{
				return File.OpenRead(fileName);
			}

			byte[] bytesToDecompress = File.ReadAllBytes(fileName);

			using (ABinaryReader codeReader = new ABinaryReader(bytesToDecompress.ToMemoryStream(false, true), Endianness.Big, Encoding.GetEncoding(932)),
					countReader = new ABinaryReader(bytesToDecompress.ToMemoryStream(false, true), Endianness.Big, Encoding.GetEncoding(932)),
					dataReader = new ABinaryReader(bytesToDecompress.ToMemoryStream(false, true), Endianness.Big, Encoding.GetEncoding(932)))
			{
				// Parse the header.
				string headerIdentifier = codeReader.ReadRawString(4);
				int uncompressedSize = codeReader.ReadS32();
				countReader.Position = codeReader.ReadS32();
				dataReader.Position = codeReader.ReadS32();

				// This is the buffer where we will put our decompressed data.
				// I would use an ABinaryWriter for this, but we need to read/write from the same buffer for the RLE parts.
				byte[] outputArray = new byte[uncompressedSize];

				int outPosition = 0;		// Our position in the destination buffer.
				uint validBitsCount = 0;	// The number of valid bits left in the code byte.
				byte currentCodeByte = 0;	// Our current code byte.

				// Begin decompression.
				while (outPosition < uncompressedSize)
				{
					// If the current code byte is used, get a new one.
					if (validBitsCount <= 0)
					{
						currentCodeByte = codeReader.Read8();
						validBitsCount = 8;
					}

					// If the next bit in the code byte is a 1, do a direct, 1:1 copy of the next data byte; otherwise, uncompress a chunk of data.
					if ((currentCodeByte & 0x80) == 0x80)
					{
						outputArray[outPosition++] = dataReader.Read8();
					}
					else
					{
						// Read the count data.
						ushort count = countReader.Read16();

						// The last three nybbles represent the distance in the buffer to go back.
						int distance = (count & 0xFFF);

						// Calculate the position from which we start.
						int startOffset = (outPosition - (distance + 1));

						// The upper nybble of count; if zero, read a third byte and add 0x10.
						int byteCount = ((count >> 12) & 0xF);

						// If the upper nybble of the count is equal to zero, that means the number of bytes is too large for a nybble and is actually in the next whole byte.
						// Add 0x10 to this byte's value, possibly to account for the original nybble. (How does Thakis figure this crap out?)
						if (byteCount == 0)
						{
							byteCount = (dataReader.Read8() + 0x10);
						}

						// Take into consideration the two bytes for the count by adding two to the byte count.
						byteCount += 2;

						// Copy the run data.
						Repeater.Repeat(byteCount, () => outputArray[outPosition++] = outputArray[startOffset++]);
					}

					// Get the next bit in the code byte.
					currentCodeByte <<= 1;
					validBitsCount--;
				}

				// Return the uncompressed data.
				return outputArray.ToMemoryStream(false, true);
			}
		}
	}
}