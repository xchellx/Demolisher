using Arookas.Collections;
using Arookas.IO.Binary;
using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Arookas.Demolisher
{
	class GraphObject : IRenderable, IEnumerable<Part>
	{
		public Part[] parts;
		float unk3;

		public BoundingBox BoundingBox { get; private set; }
		public int ChildIndex { get; private set; }
		public int NextIndex { get; private set; }
		public int PrevIndex { get; private set; }
		public int ParentIndex { get; private set; }
		public GraphObjectRenderFlags RenderFlags { get; private set; }

		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; }
		
		public bool Visible { get; set; }

		public int PartCount { get { return parts.Length; } }
		public Part this[int index] { get { return parts[index]; } }

		public GraphObject(ABinaryReader binaryReader)
		{
			Visible = true;
			ParentIndex = binaryReader.ReadS16();
			ChildIndex = binaryReader.ReadS16();
			NextIndex = binaryReader.ReadS16();
			PrevIndex = binaryReader.ReadS16();

			if (binaryReader.Read8() != 0)
			{
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", binaryReader.Stream.Position));
#endif
			}

			RenderFlags = (GraphObjectRenderFlags)binaryReader.Read8();

			if (binaryReader.Read16() != 0)
			{
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", binaryReader.Stream.Position));
#endif
			}

			Scale = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
			Rotation = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
			Position = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
			BoundingBox = new BoundingBox(binaryReader.ReadVector3D().ToVector3(), binaryReader.ReadVector3D().ToVector3());
			unk3 = binaryReader.ReadSingle();

			int partCount = binaryReader.ReadS16();

			if (binaryReader.Read16() != 0)
			{
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", binaryReader.Stream.Position));
#endif
			}

			int partOffset = binaryReader.ReadS32();

			if (binaryReader.Read32s(7).Any(zero => zero != 0))
			{
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", binaryReader.Stream.Position));
#endif
			}

			binaryReader.Goto(partOffset);
			parts = CollectionHelper.Initialize<Part>(partCount, () => new Part(binaryReader));
			binaryReader.Back();
		}

		public bool HasRenderFlag(GraphObjectRenderFlags renderFlag)
		{
			return RenderFlags.HasFlag(renderFlag);
		}
		
		public IEnumerator<Part> GetEnumerator()
		{
			return parts.GetArrayEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	[Flags]
	enum GraphObjectRenderFlags : byte
	{
		FourthWall = 0x04,				// invisible (except in GBH view)
		Transparent = 0x08,				// transparent (when luigi is behind it)?
		FullBright = 0x40,				// fullbright (ignore lighting)
		Ceiling = 0x80,					// transparent/ceiling (will fade out when luigi vaccuums the floor)
	}
}