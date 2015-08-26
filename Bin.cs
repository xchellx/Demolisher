using arookas.Collections;
using arookas.IO.Binary;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arookas.Demolisher
{
	class Bin : IRenderable, IDisposable
	{
		ABinaryReader binaryReader;
		byte unk1;
		string name;
		uint[] offsets;

		GraphObject[] graphObjects;
		Batch[] batches;
		Shader[] shaders;
		Material[] materials;
		Texture[] textures;
		Vector3[] positions;
		Vector3[] normals;
		Vector2[] texCoord0s;
		Vector2[] texCoord1s;
		int[] glTextures;

		bool isDisposed;

		public const float GlobalScale = 256.0f;
		public const float GlobalScaleReciprocal = (1.0f / GlobalScale);

		bool HasTextures { get { return (TextureOffset > 0); } }
		bool HasMaterials { get { return (MaterialOffset > 0); } }
		bool HasPositions { get { return (PositionOffset > 0); } }
		bool HasNormals { get { return (NormalOffset > 0); } }
		bool HasTexCoord0s { get { return (TexCoord0Offset > 0); } }
		bool HasTexCoord1s { get { return (TexCoord1Offset > 0); } }
		bool HasShaders { get { return (ShaderOffset > 0); } }
		bool HasBatches { get { return (BatchOffset > 0); } }
		bool HasGraph { get { return (GraphOffset > 0); } }

		uint TextureOffset { get { return offsets[0]; } }
		uint MaterialOffset { get { return offsets[1]; } }
		uint PositionOffset { get { return offsets[2]; } }
		uint NormalOffset { get { return offsets[3]; } }
		uint TexCoord0Offset { get { return offsets[6]; } }
		uint TexCoord1Offset { get { return offsets[7]; } }
		uint ShaderOffset { get { return offsets[10]; } }
		uint BatchOffset { get { return offsets[11]; } }
		uint GraphOffset { get { return offsets[12]; } }

		public bool Visible
		{
			get { return graphObjects[0].Visible; }
			set { graphObjects[0].Visible = value; }
		}

		public Vector3 Position
		{
			get { return graphObjects[0].Position; }
			set { graphObjects[0].Position = value; }
		}
		public Vector3 Rotation
		{
			get { return graphObjects[0].Rotation; }
			set { graphObjects[0].Rotation = value; }
		}
		public Vector3 Scale
		{
			get { return graphObjects[0].Scale; }
			set { graphObjects[0].Scale = value; }
		}

		public string Name { get { return name; } }

		public GraphObject this[int index] { get { return graphObjects[index]; } }

		public Bin(string fileName)
			: this(fileName, Vector3.Zero, Vector3.Zero, Vector3.One)
		{
			
		}
		public Bin(string fileName, Vector3 position, Vector3 rotation, Vector3 scale)
		{
			// header
			binaryReader = new ABinaryReader(Yay0.Decompress(fileName), Endianness.Big, Encoding.GetEncoding(932));
			unk1 = binaryReader.Read8();
			name = binaryReader.ReadClampedString(11);
			offsets = binaryReader.Read32s(21);

			// data
			graphObjects = (HasGraph ? GetGraphObjects(0) : new GraphObject[0]);
			batches = (HasBatches ? CollectionUtility.Initialize(GetBatchCount(), index => GetBatch(index)) : new Batch[0]);
			shaders = (HasShaders ? CollectionUtility.Initialize(GetShaderCount(), index => GetShader(index)) : new Shader[0]);
			materials = (HasMaterials ? CollectionUtility.Initialize(GetMaterialCount(), index => GetMaterial(index)) : new Material[0]);
			textures = (HasTextures ? CollectionUtility.Initialize(GetTextureCount(), index => GetTexture(index)) : new Texture[0]);
			positions = (HasPositions ? CollectionUtility.Initialize(GetPositionCount(), index => GetPosition(index)) : new Vector3[0]);
			normals = (HasNormals ? CollectionUtility.Initialize(GetNormalCount(), index => GetNormal(index)) : new Vector3[0]);
			texCoord0s = (HasTexCoord0s ? CollectionUtility.Initialize(GetTexCoord0Count(), index => GetTexCoord0(index)) : new Vector2[0]);
			texCoord1s = (HasTexCoord1s ? CollectionUtility.Initialize(GetTexCoord1Count(), index => GetTexCoord1(index)) : new Vector2[0]);

			// Load textures.
			glTextures = textures.Select(texture => texture.ToGLTexture()).ToArray();
			
			// Orient.
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}

		int GetBatchCount()
		{
			short batchCount = -1;

			foreach (var graphObject in graphObjects)
			{
				foreach (var part in graphObject)
				{
					if (part.BatchIndex > batchCount)
					{
						batchCount = part.BatchIndex;
					}
				}
			}

			return (batchCount + 1);
		}
		int GetMaterialCount()
		{
			short materialCount = -1;

			foreach (var shader in shaders)
			{
				if (shader.MaterialIndex.Max() > materialCount)
				{
					materialCount = shader.MaterialIndex.Max();
				}
			}

			return (materialCount + 1);
		}
		int GetNormalCount()
		{
			int normalCount = -1;

			foreach (var batch in batches)
			{
				foreach (var primitive in batch)
				{
					foreach (var vertex in primitive.Where(vertex => vertex.NormalIndex != null))
					{
						if (vertex.NormalIndex > normalCount)
						{
							normalCount = vertex.NormalIndex.Value;
						}

						if (vertex.BinormalIndex != null)
						{
							if (vertex.BinormalIndex + 1 > normalCount)
							{
								normalCount = vertex.BinormalIndex.Value + 1;
							}
						}

						if (vertex.TangentIndex != null)
						{
							if (vertex.TangentIndex + 2 > normalCount)
							{
								normalCount = vertex.TangentIndex.Value + 2;
							}
						}
					}
				}
			}

			return (normalCount + 1);
		}
		int GetPositionCount()
		{
			short positionCount = -1;

			foreach (var batch in batches)
			{
				foreach (var primitive in batch)
				{
					foreach (var vertex in primitive.Where(vertex => vertex.PositionIndex != null))
					{
						if (vertex.PositionIndex > positionCount)
						{
							positionCount = vertex.PositionIndex.Value;
						}
					}
				}
			}

			return (positionCount + 1);
		}
		int GetShaderCount()
		{
			short shaderCount = -1;

			foreach (var graphObject in graphObjects)
			{
				foreach (var part in graphObject)
				{
					if (part.ShaderIndex > shaderCount)
					{
						shaderCount = part.ShaderIndex;
					}
				}
			}

			return (shaderCount + 1);
		}
		int GetTextureCount()
		{
			short textureCount = 0;

			foreach (var material in materials)
			{
				if (material.textureIndex > textureCount)
				{
					textureCount = material.textureIndex;
				}
			}

			return (textureCount + 1);
		}
		int GetTexCoord0Count()
		{
			short texCoord0Count = -1;

			foreach (var batch in batches)
			{
				foreach (var primitive in batch)
				{
					foreach (var vertex in primitive.Where(vertex => vertex.UVIndex[0] != null && vertex.UVIndex[0] > texCoord0Count))
					{
						texCoord0Count = vertex.UVIndex[0].Value;
					}
				}
			}

			return (texCoord0Count + 1);
		}
		int GetTexCoord1Count()
		{
			short texCoord1Count = -1;

			foreach (var batch in batches)
			{
				foreach (var primitive in batch)
				{
					foreach (var vertex in primitive.Where(vertex => vertex.UVIndex[1] != null && vertex.UVIndex[1] > texCoord1Count))
					{
						texCoord1Count = vertex.UVIndex[1].Value;
					}
				}
			}

			return (texCoord1Count + 1);
		}

		Batch GetBatch(int index)
		{
			const int itemSize = 0x18;

			binaryReader.Goto(BatchOffset + itemSize * index);
			binaryReader.PositionAnchor = BatchOffset;
			Batch batch = new Batch(binaryReader);
			binaryReader.ResetAnchor();
			binaryReader.Back();

			return batch;
		}
		GraphObject GetGraphObject(int index)
		{
			const int itemSize = 0x8C;

			binaryReader.Goto(GraphOffset + itemSize * index);
			binaryReader.PositionAnchor = GraphOffset;
			GraphObject graphObject = new GraphObject(binaryReader);
			binaryReader.ResetAnchor();
			binaryReader.Back();

			return graphObject;
		}
		GraphObject[] GetGraphObjects(int index)
		{
			List<GraphObject> graphObjects = new List<GraphObject>(10);
			GraphObject graphObject = GetGraphObject(index);
			graphObjects.Add(graphObject);

			if (graphObject.ChildIndex >= 0)
			{
				graphObjects.AddRange(GetGraphObjects(graphObject.ChildIndex));
			}

			if (graphObject.NextIndex >= 0)
			{
				graphObjects.AddRange(GetGraphObjects(graphObject.NextIndex));
			}

			return graphObjects.ToArray();
		}
		Material GetMaterial(int index)
		{
			const int itemSize = 0x14;

			binaryReader.Goto(MaterialOffset + itemSize * index);
			Material material = new Material(binaryReader);
			binaryReader.Back();
			return material;
		}
		Vector3 GetNormal(int index)
		{
			const int itemSize = 0xC;

			binaryReader.Goto(NormalOffset + itemSize * index);
			Vector3 normal = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
			binaryReader.Back();
			return normal;
		}
		Vector3 GetPosition(int index)
		{
			const int itemSize = 0x6;

			binaryReader.Goto(PositionOffset + itemSize * index);
			Vector3 pos = new Vector3(binaryReader.ReadS16() / GlobalScale, binaryReader.ReadS16() / GlobalScale, binaryReader.ReadS16() / GlobalScale);
			binaryReader.Back();
			return pos;
		}
		Shader GetShader(int index)
		{
			const int itemSize = 0x28;

			binaryReader.Goto(ShaderOffset + itemSize * index);
			Shader shader = new Shader(binaryReader);
			binaryReader.Back();
			return shader;
		}
		Texture GetTexture(int index)
		{
			const int itemSize = 0xC;

			binaryReader.Goto(TextureOffset + itemSize * index);
			binaryReader.PositionAnchor = TextureOffset;
			Texture texture = new Texture(binaryReader);
			binaryReader.ResetAnchor();
			binaryReader.Back();
			return texture;
		}
		Vector2 GetTexCoord0(int index)
		{
			const int itemSize = 0x8;

			binaryReader.Goto(TexCoord0Offset + itemSize * index);
			Vector2 texCoord = new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
			binaryReader.Back();
			return texCoord;
		}
		Vector2 GetTexCoord1(int index)
		{
			const int itemSize = 0x8;

			binaryReader.Goto(TexCoord1Offset + itemSize * index);
			Vector2 texCoord = new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
			binaryReader.Back();
			return texCoord;
		}

		public void Render(RenderFlags renderFlags)
		{
			GL.PushMatrix();

			// Render all root graph objects.
			for (int index = 0; index >= 0; index = graphObjects[index].NextIndex)
			{
				RenderGraphObject(graphObjects[index], renderFlags);
			}

			GL.PopMatrix();
		}
		void RenderGraphObject(GraphObject graphObject, RenderFlags renderFlags)
		{
			// visibility check
			if (!graphObject.Visible)
			{
				return;
			}
			
			// render-flag check
			if (!renderFlags.HasFlag(RenderFlags.Ceilings) && graphObject.HasRenderFlag(GraphObjectRenderFlags.Ceiling))
			{
				return;
			}
			if (!renderFlags.HasFlag(RenderFlags.FourthWalls) && graphObject.HasRenderFlag(GraphObjectRenderFlags.FourthWall))
			{
				return;
			}

			// matrix transformations
			GL.PushMatrix();
			TransformMatrix(graphObject.Position, graphObject.Rotation, graphObject.Scale);

			// bounding box
			if (renderFlags.HasFlag(RenderFlags.BoundingBox))
			{
				GL.Disable(EnableCap.Lighting);
				GL.Disable(EnableCap.Texture2D);
				GL.Color3(Color.Yellow);
				GLUtility.WireCube(graphObject.BoundingBox * GlobalScaleReciprocal);
				GL.Color3(Color.White);
				GL.Enable(EnableCap.Texture2D);
				GL.Enable(EnableCap.Lighting);
			}

			if (graphObject.HasRenderFlag(GraphObjectRenderFlags.FullBright))
			{
				GL.Disable(EnableCap.Lighting);
			}

			// Render parts.
			foreach (var part in graphObject)
			{
				if (shaders[part.ShaderIndex].Tint.A > 0 && ((shaders[part.ShaderIndex].Tint.A == Color.Opaque && renderFlags.HasFlag(RenderFlags.Opaque)) || (shaders[part.ShaderIndex].Tint.A < Color.Opaque && renderFlags.HasFlag(RenderFlags.Translucent))))
				{
					RenderPart(part, renderFlags);
				}
			}

			GL.Enable(EnableCap.Lighting);

			// Render children.
			for (int childIndex = graphObject.ChildIndex; childIndex >= 0; childIndex = graphObjects[childIndex].NextIndex)
			{
				RenderGraphObject(graphObjects[childIndex], renderFlags);
			}

			GL.PopMatrix();
		}
		void RenderPart(Part part, RenderFlags renderFlags)
		{
			GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, shaders[part.ShaderIndex].Tint.ToColor4());
			var batch = batches[part.BatchIndex];

			if (batch.UseNBT) // Render with bump map.
			{
				int binormalAttribute = GL.GetAttribLocation(Program.EmbossProgram, "inBinormal");
				int tangentAttribute = GL.GetAttribLocation(Program.EmbossProgram, "inTangent");
				int diffuseMap = GL.GetUniformLocation(Program.EmbossProgram, "diffuseMap");
				int bumpMap = GL.GetUniformLocation(Program.EmbossProgram, "bumpMap");
				int embossFactor = GL.GetUniformLocation(Program.EmbossProgram, "embossScale");

				Program.EmbossProgram.Use();

				GL.ActiveTexture(TextureUnit.Texture0 + 0);
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, glTextures[materials[shaders[part.ShaderIndex].MaterialIndex[0]].textureIndex]);
				GLUtility.SetTexture2DWrapModeS(materials[shaders[part.ShaderIndex].MaterialIndex[0]].wrapS);
				GLUtility.SetTexture2DWrapModeT(materials[shaders[part.ShaderIndex].MaterialIndex[0]].wrapT);
				GL.Uniform1(diffuseMap, 0);

				GL.ActiveTexture(TextureUnit.Texture0 + 1);
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, glTextures[materials[shaders[part.ShaderIndex].MaterialIndex[1]].textureIndex]);
				GLUtility.SetTexture2DWrapModeS(materials[shaders[part.ShaderIndex].MaterialIndex[1]].wrapS);
				GLUtility.SetTexture2DWrapModeT(materials[shaders[part.ShaderIndex].MaterialIndex[1]].wrapT);
				GL.Uniform1(bumpMap, 1);

				GL.Uniform1(embossFactor, 2.0f);

				foreach (var primitive in batch)
				{
					GL.Begin(primitive.GLType);

					foreach (var vertex in primitive)
					{
						GL.MultiTexCoord2(TextureUnit.Texture0 + 0, ref texCoord0s[vertex.UVIndex[0].Value]);
						GL.MultiTexCoord2(TextureUnit.Texture0 + 1, ref texCoord1s[vertex.UVIndex[1].Value]);
						GL.Normal3(normals[vertex.NormalIndex.Value]);
						GL.VertexAttrib3(tangentAttribute, ref normals[vertex.BinormalIndex.Value + 1]);
						GL.VertexAttrib3(binormalAttribute, ref normals[vertex.TangentIndex.Value + 2]);
						GL.Vertex3(positions[vertex.PositionIndex.Value]);
					}

					GL.End();
				}

				GL.UseProgram(0);
			}
			else // Render without bump map.
			{
				for (int texUnit = 0; texUnit < 8; texUnit++)
				{
					if (shaders[part.ShaderIndex].MaterialIndex[texUnit] < 0)
					{
						continue;
					}

					var material = materials[shaders[part.ShaderIndex].MaterialIndex[texUnit]];
					GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
					GL.Enable(EnableCap.Texture2D);
					GL.BindTexture(TextureTarget.Texture2D, glTextures[material.textureIndex]);
					GLUtility.SetTexture2DWrapModeS(material.wrapS);
					GLUtility.SetTexture2DWrapModeT(material.wrapT);
				}

				foreach (var primitive in batch)
				{
					GL.Begin(primitive.GLType);

					foreach (var vertex in primitive)
					{
						if (vertex.UVIndex[0] != null)
						{
							GL.MultiTexCoord2(TextureUnit.Texture0, ref texCoord0s[vertex.UVIndex[0].Value]);
						}

						if (vertex.NormalIndex != null)
						{
							GL.Normal3(normals[vertex.NormalIndex.Value]);
						}

						GL.Vertex3(positions[vertex.PositionIndex.Value]);
					}

					GL.End();
				}
			}

			GLUtility.DisableTexture2D();

			// normals
			if (renderFlags.HasFlag(RenderFlags.NBT))
			{
				const float normalLength = 0.0625f;

				GL.Disable(EnableCap.Lighting);
				GL.Begin(BeginMode.Lines);

				foreach (var primitive in batch)
				{
					foreach (var vertex in primitive)
					{
						if (vertex.NormalIndex != null)
						{
							GL.Color4(Color.Red.ToColor4());
							GL.Vertex3(positions[vertex.PositionIndex.Value]);
							GL.Vertex3(positions[vertex.PositionIndex.Value] + normals[vertex.NormalIndex.Value] * normalLength);
						}

						if (vertex.TangentIndex != null)
						{
							GL.Color4(Color.Green.ToColor4());
							GL.Vertex3(positions[vertex.PositionIndex.Value]);
							GL.Vertex3(positions[vertex.PositionIndex.Value] + normals[vertex.TangentIndex.Value + 2] * normalLength);
						}

						if (vertex.BinormalIndex != null)
						{
							GL.Color4(Color.Blue.ToColor4());
							GL.Vertex3(positions[vertex.PositionIndex.Value]);
							GL.Vertex3(positions[vertex.PositionIndex.Value] + normals[vertex.BinormalIndex.Value + 1] * normalLength);
						}
					}
				}

				GL.End();
				GL.Enable(EnableCap.Lighting);
			}
		}

		public static void TransformMatrix(Vector3 pos, Vector3 rot, Vector3 scale)
		{
			// for some reason, rotation screws everything up if you do it before translation???
			GL.Scale(scale);
			GL.Translate(pos * GlobalScaleReciprocal);
			GL.Rotate(rot.Z, Vector3.UnitZ);
			GL.Rotate(rot.X, Vector3.UnitX);
			GL.Rotate(rot.Y, Vector3.UnitY);
		}

		public void Dispose()
		{
			if (!isDisposed)
			{
				binaryReader.Dispose();
				GL.DeleteTextures(glTextures.Length, glTextures);
				isDisposed = true;
			}
		}
	}

	[Flags]
	enum RenderFlags
	{
		None = 0,
		Opaque = 1 << 0,
		Translucent = 1 << 1,
		BoundingBox = 1 << 2,
		FourthWalls = 1 << 3,
		Ceilings = 1 << 4,
		NBT = 1 << 5,
	}
}