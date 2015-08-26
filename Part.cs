using arookas.IO.Binary;

namespace arookas.Demolisher
{
	struct Part
	{
		public short BatchIndex { get; private set; }
		public short ShaderIndex { get; private set; }

		public Part(ABinaryReader binaryReader)
			: this() // stupid fucking C#
		{
			ShaderIndex = binaryReader.ReadS16();
			BatchIndex = binaryReader.ReadS16();
		}
	}
}