using arookas.IO.Binary;

namespace arookas.Demolisher
{
	struct Shader
	{
		uint unk1;
		byte unk2;
		short[] unk3;

		public short[] MaterialIndex { get; private set; }
		public Color Tint { get; private set; }

		public Shader(ABinaryReader binaryReader)
			: this() // stupid fucking C#
		{
			unk1 = binaryReader.Read24();
			Tint = Color.FromRGBA8(binaryReader.Read32());
			unk2 = binaryReader.Read8();
			MaterialIndex = binaryReader.ReadS16s(8);
			unk3 = binaryReader.ReadS16s(8);

			if (unk3[0] != 0 || unk3[1] != -1)
			{
				throw new System.Exception("shader unk3!!");
			}
		}
	}
}