using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000053 RID: 83
	internal struct sfSample
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00007168 File Offset: 0x00005368
		public sfSample(ABinaryReader binaryReader)
		{
			this.achSampleName = binaryReader.ReadClampedString(20);
			this.dwStart = binaryReader.Read32();
			this.dwEnd = binaryReader.Read32();
			this.dwStartLoop = binaryReader.Read32();
			this.dwEndLoop = binaryReader.Read32();
			this.dwSampleRate = binaryReader.Read32();
			this.byOriginalKey = binaryReader.Read8();
			this.chCorrect = binaryReader.ReadS8();
			this.wSampleLink = binaryReader.Read16();
			this.sfSampleType = (SFSampleLink)binaryReader.Read16();
		}

		// Token: 0x04000134 RID: 308
		public string achSampleName;

		// Token: 0x04000135 RID: 309
		public uint dwStart;

		// Token: 0x04000136 RID: 310
		public uint dwEnd;

		// Token: 0x04000137 RID: 311
		public uint dwStartLoop;

		// Token: 0x04000138 RID: 312
		public uint dwEndLoop;

		// Token: 0x04000139 RID: 313
		public uint dwSampleRate;

		// Token: 0x0400013A RID: 314
		public byte byOriginalKey;

		// Token: 0x0400013B RID: 315
		public sbyte chCorrect;

		// Token: 0x0400013C RID: 316
		public ushort wSampleLink;

		// Token: 0x0400013D RID: 317
		public SFSampleLink sfSampleType;
	}
}
