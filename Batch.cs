using arookas.Collections;
using arookas.IO.Binary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace arookas.Demolisher
{
	class Batch : IEnumerable<Primitive>
	{
		Primitive[] primitives;
		byte positions;

		public BatchAttributes Attributes { get; private set; }
		public int FaceCount { get; private set; }
		public uint Offset { get; private set; }
		public bool UseNBT { get; private set; }
		public bool UseNormals { get; private set; }
		
		public Primitive this[int index] { get { return primitives[index]; } }

		public Batch(ABinaryReader binaryReader)
		{
			FaceCount = binaryReader.Read16();
			int size = (binaryReader.Read16() << 5);

			Attributes = (BatchAttributes)(uint)binaryReader.Read32();
			UseNormals = (binaryReader.Read8() != 0);
			positions = binaryReader.Read8();
			int uvCount = binaryReader.Read8();
			UseNBT = (binaryReader.Read8() != 0);
			Offset = binaryReader.Read32();

			if (binaryReader.Read8s(8).Any(zero => zero != 0))
			{
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("Batch padding != 0 at 0x{0:X8}.", binaryReader.Stream.Position));
#endif
			}
			
			int faces = 0;
			List<Primitive> primitives = new List<Primitive>();
			binaryReader.Goto(Offset);

			while (faces < FaceCount && binaryReader.Position < Offset + size)
			{
				Primitive primitive = new Primitive(binaryReader, UseNBT, uvCount, Attributes);

				if (!primitive.Type.IsDefined())
				{
					break;
				}

				primitives.Add(primitive);
				faces += primitive.FaceCount;
			}

			binaryReader.Back();

			this.primitives = primitives.ToArray();
		}

		public bool HasAttribute(BatchAttributes attribute)
		{
			return Attributes.HasFlag(attribute);
		}
		
		public IEnumerator<Primitive> GetEnumerator()
		{
			return primitives.GetArrayEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}