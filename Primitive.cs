using arookas.Collections;
using arookas.IO.Binary;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;

namespace arookas.Demolisher
{
	class Primitive : IEnumerable<Vertex>
	{
		Vertex[] vertices;

		public PrimitiveType Type { get; private set; }

		public int VertexCount { get { return vertices.Length; } }
		public int FaceCount { get { return (VertexCount - 2); } }
		
		public Vertex this[int index] { get { return vertices[index]; } }

		public BeginMode GLType
		{
			get
			{
				switch (Type)
				{
					case PrimitiveType.Lines: return BeginMode.Lines;
					case PrimitiveType.LineStrip: return BeginMode.LineStrip;
					case PrimitiveType.Points: return BeginMode.Points;
					case PrimitiveType.TriangleFan: return BeginMode.TriangleFan;
					case PrimitiveType.Triangles: return BeginMode.Triangles;
					case PrimitiveType.TriangleStrip: return BeginMode.TriangleStrip;
					default: throw new InvalidOperationException("Primitive type is invalid.");
				}
			}
		}

		public Primitive(ABinaryReader binaryReader, bool useNBT, int uvCount, BatchAttributes attributes)
		{
			Type = (PrimitiveType)binaryReader.Read8();
			vertices = CollectionUtility.Initialize<Vertex>(binaryReader.Read16(), () => new Vertex(binaryReader, useNBT, uvCount, attributes));
		}

		public IEnumerator<Vertex> GetEnumerator()
		{
			return vertices.GetArrayEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}