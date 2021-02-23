using System;
using Arookas.IO.Binary;

namespace Arookas.Audio.SoundFonts
{
	// Token: 0x02000057 RID: 87
	internal struct sfPresetHeader
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00007240 File Offset: 0x00005440
		public sfPresetHeader(ABinaryReader binaryReader)
		{
			this.achPresetName = binaryReader.ReadClampedString(20);
			this.wPreset = binaryReader.Read16();
			this.wBank = binaryReader.Read16();
			this.wPresetBagNdx = binaryReader.Read16();
			this.dwLibrary = binaryReader.Read32();
			this.dwGenre = binaryReader.Read32();
			this.dwMorphology = binaryReader.Read32();
		}

		// Token: 0x04000144 RID: 324
		public string achPresetName;

		// Token: 0x04000145 RID: 325
		public ushort wPreset;

		// Token: 0x04000146 RID: 326
		public ushort wBank;

		// Token: 0x04000147 RID: 327
		public ushort wPresetBagNdx;

		// Token: 0x04000148 RID: 328
		public uint dwLibrary;

		// Token: 0x04000149 RID: 329
		public uint dwGenre;

		// Token: 0x0400014A RID: 330
		public uint dwMorphology;
	}
}
