using System;
using System.IO;
using System.Linq;
using Arookas.IO.Binary;
using Arookas.IO.Binary.ResourceInterchangeFileFormat;

namespace Arookas.Audio
{
	// Token: 0x02000063 RID: 99
	public class Wave
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B270 File Offset: 0x00009470
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000B278 File Offset: 0x00009478
		public ushort BitsPerSample
		{
			get
			{
				return this.bitsPerSample;
			}
			set
			{
				this.bitsPerSample = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B281 File Offset: 0x00009481
		public ushort BlockAlign
		{
			get
			{
				return this.channelCount * this.bitsPerSample / 8;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B293 File Offset: 0x00009493
		public uint ByteRate
		{
			get
			{
				return this.sampleRate * (uint)this.channelCount * (uint)this.bitsPerSample / 8U;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000B2AB File Offset: 0x000094AB
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000B2B3 File Offset: 0x000094B3
		public ushort ChannelCount
		{
			get
			{
				return this.channelCount;
			}
			set
			{
				this.channelCount = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B2BC File Offset: 0x000094BC
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public byte[] Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B2CD File Offset: 0x000094CD
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000B2D5 File Offset: 0x000094D5
		public WaveFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (!Enum.IsDefined(typeof(WaveFormat), value))
				{
					throw new ArgumentOutOfRangeException();
				}
				this.format = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000B2FB File Offset: 0x000094FB
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000B303 File Offset: 0x00009503
		public uint SampleRate
		{
			get
			{
				return this.sampleRate;
			}
			set
			{
				this.sampleRate = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000B30C File Offset: 0x0000950C
		public uint WrittenSize
		{
			get
			{
				uint num = 12U;
				num += 8U;
				if (this.format == WaveFormat.PCM)
				{
					num += 16U;
				}
				else
				{
					num += 20U;
				}
				num += 8U;
				if (this.data != null)
				{
					num += (uint)this.data.Length;
				}
				return num;
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B358 File Offset: 0x00009558
		public static Wave FromFile(string fileName)
		{
			Wave result;
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				result = Wave.FromStream(fileStream);
			}
			return result;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public static Wave FromStream(Stream stream)
		{
			Wave result;
			using (RIFF riff = new RIFF(stream))
			{
				Wave wave = new Wave();
				if (riff.Identification != "WAVE")
				{
					throw new Exception("Given Stream does not contain a WAVE file.");
				}
				riff.GotoChunk(riff.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "fmt "));
				wave.Format = (WaveFormat)riff.BinaryReader.Read16();
				wave.ChannelCount = riff.BinaryReader.Read16();
				wave.SampleRate = riff.BinaryReader.Read32();
				riff.BinaryReader.Step(8L);
				wave.BitsPerSample = riff.BinaryReader.Read16();
				RIFFChunk riffchunk = riff.Items.Chunks.Single((RIFFChunk chunk) => chunk.Identification == "data");
				riff.GotoChunk(riffchunk);
				wave.Data = riff.BinaryReader.Read8s((int)riffchunk.Length);
				result = wave;
			}
			return result;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B4DC File Offset: 0x000096DC
		public void ToFile(string fileName, WaveWriteOptions options)
		{
			using (FileStream fileStream = File.Create(fileName))
			{
				this.ToStream(fileStream, options);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B514 File Offset: 0x00009714
		public void ToStream(Stream stream, WaveWriteOptions options)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentNullException("Given Stream cannot write.");
			}
			using (ABinaryWriter abinaryWriter = new ABinaryWriter(stream))
			{
				if (!options.HasFlag(WaveWriteOptions.NoHeader))
				{
					abinaryWriter.WriteString("RIFF");
					abinaryWriter.Write32(this.WrittenSize);
					abinaryWriter.WriteString("WAVE");
					abinaryWriter.WritePadding(4, 0);
				}
				abinaryWriter.WriteString("fmt ");
				abinaryWriter.Write32((this.format == WaveFormat.PCM) ? 16U : 20U);
				abinaryWriter.Write16((ushort)this.format);
				abinaryWriter.Write16(this.channelCount);
				abinaryWriter.Write32(this.sampleRate);
				abinaryWriter.Write32(this.ByteRate);
				abinaryWriter.Write32((uint)this.BlockAlign);
				abinaryWriter.Write32((uint)this.bitsPerSample);
				if (this.format != WaveFormat.PCM)
				{
					abinaryWriter.Write32(0U);
				}
				abinaryWriter.WritePadding(4, 0);
				abinaryWriter.WriteString("data");
				if (this.data == null)
				{
					abinaryWriter.Write32(0U);
				}
				else
				{
					abinaryWriter.WriteS32(this.data.Length);
					abinaryWriter.Write8s(this.data);
				}
				abinaryWriter.WritePadding(4, 0);
			}
		}

		// Token: 0x04000199 RID: 409
		private ushort bitsPerSample;

		// Token: 0x0400019A RID: 410
		private ushort channelCount;

		// Token: 0x0400019B RID: 411
		private byte[] data;

		// Token: 0x0400019C RID: 412
		private WaveFormat format;

		// Token: 0x0400019D RID: 413
		private uint sampleRate;
	}
}
