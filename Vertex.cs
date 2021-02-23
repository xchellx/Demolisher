using Arookas.Math;
using Arookas.IO.Binary;

namespace Arookas.Demolisher
{
	struct Vertex
	{
		public short? MatrixIndex { get; private set; }
		public short? PositionIndex { get; private set; }
		public short? NormalIndex { get; private set; }
		public short? BinormalIndex { get; private set; }
		public short? TangentIndex { get; private set; }
		public short?[] ColorIndex { get; private set; }
		public short?[] UVIndex { get; private set; }

		public Vertex(ABinaryReader binaryReader, bool useNBT, int uvCount, BatchAttributes attributes)
			: this() // stupid fucking C#
		{
			MatrixIndex = null;
			PositionIndex = null;
			NormalIndex = null;
			BinormalIndex = null;
			TangentIndex = null;
			ColorIndex = new short?[2];
			UVIndex = new short?[8];

			if (attributes.HasFlag(BatchAttributes.Position))
			{
				PositionIndex = binaryReader.ReadS16();
			}

			if (attributes.HasFlag(BatchAttributes.Normal))
			{
				NormalIndex = binaryReader.ReadS16();

				if (useNBT)
				{
					BinormalIndex = binaryReader.ReadS16();
					TangentIndex = binaryReader.ReadS16();
				}
			}

			if (attributes.HasFlag(BatchAttributes.Color0))
			{
				ColorIndex[0] = binaryReader.ReadS16();
			}

			if (attributes.HasFlag(BatchAttributes.Color1))
			{
				ColorIndex[1] = binaryReader.ReadS16();
			}

			for (int texCoord = 0; texCoord < uvCount; texCoord++)
			{
				if (attributes.HasFlag((BatchAttributes)(1 << (13 + texCoord))))
				{
					UVIndex[texCoord] = binaryReader.ReadS16();
				}
			}
		}
	}
}