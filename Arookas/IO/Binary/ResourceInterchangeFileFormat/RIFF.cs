using System;
using System.Collections.Generic;
using System.IO;

namespace Arookas.IO.Binary.ResourceInterchangeFileFormat
{
	// Token: 0x02000086 RID: 134
	public sealed class RIFF : IDisposable
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public ABinaryReader BinaryReader
		{
			get
			{
				return this.binaryReader;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000DC28 File Offset: 0x0000BE28
		public string Identification
		{
			get
			{
				return this.identification;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000DC30 File Offset: 0x0000BE30
		public RIFFItemCollection Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000DC38 File Offset: 0x0000BE38
		public uint Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public RIFF(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.binaryReader = new ABinaryReader(stream);
			this.startPosition = stream.Position;
			if (this.binaryReader.ReadRawString(4) != "RIFF")
			{
				throw new Exception("Couldn't find RIFF header.");
			}
			this.length = this.binaryReader.Read32();
			this.identification = this.binaryReader.ReadRawString(4);
			this.items = new RIFFItemCollection(this.GetList((long)((ulong)(this.length + 8U))));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000DCD9 File Offset: 0x0000BED9
		public void Dispose()
		{
			this.binaryReader.Dispose();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
		private List<RIFFItem> GetList(long endPosition)
		{
			List<RIFFItem> list = new List<RIFFItem>();
			while (this.binaryReader.Position < endPosition)
			{
				string a = this.binaryReader.ReadRawString(4);
				uint num = this.binaryReader.Read32();
				if (a == "LIST")
				{
					string text = this.binaryReader.ReadRawString(4);
					list.Add(new RIFFList(this, text, num, this.GetList((long)((ulong)((uint)(this.binaryReader.Position - 4L + (long)((ulong)num)))))));
				}
				else
				{
					list.Add(new RIFFChunk(this, a, num, this.binaryReader.Position));
					this.binaryReader.Step((long)((ulong)num));
					this.binaryReader.Skip(2);
				}
			}
			return list;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		public void GotoChunk(RIFFChunk chunk)
		{
			this.binaryReader.Position = chunk.Position;
		}

		// Token: 0x040001DC RID: 476
		private ABinaryReader binaryReader;

		// Token: 0x040001DD RID: 477
		private string identification;

		// Token: 0x040001DE RID: 478
		private RIFFItemCollection items;

		// Token: 0x040001DF RID: 479
		private uint length;

		// Token: 0x040001E0 RID: 480
		private long startPosition;
	}
}
