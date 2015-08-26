using arookas.IO.Binary;

namespace arookas.Demolisher
{
	struct Material
	{
		// TODO: encapsulate these
		public short textureIndex;
		public short unk1; // always -1
		public WrapMode wrapS;
		public WrapMode wrapT;
		public ushort unk3; // unknown, flags??
		public uint unk4; // zeroes
		public uint unk5; // zeroes
		public uint unk6; // zeroes

		public Material(ABinaryReader binaryReader)
		{
			textureIndex = binaryReader.ReadS16();
			unk1 = binaryReader.ReadS16();
			wrapS = (WrapMode)binaryReader.Read8();
			wrapT = (WrapMode)binaryReader.Read8();
			unk3 = binaryReader.Read16();
			unk4 = binaryReader.Read32();
			unk5 = binaryReader.Read32();
			unk6 = binaryReader.Read32();
		}
	}
}