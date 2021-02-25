using Arookas.Collections;
using Arookas.IO.Binary;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Arookas.Demolisher
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
			batches = (HasBatches ? CollectionHelper.Initialize(GetBatchCount(), index => GetBatch(index)) : new Batch[0]);
			shaders = (HasShaders ? CollectionHelper.Initialize(GetShaderCount(), index => GetShader(index)) : new Shader[0]);
			materials = (HasMaterials ? CollectionHelper.Initialize(GetMaterialCount(), index => GetMaterial(index)) : new Material[0]);
			textures = (HasTextures ? CollectionHelper.Initialize(GetTextureCount(), index => GetTexture(index)) : new Texture[0]);
			positions = (HasPositions ? CollectionHelper.Initialize(GetPositionCount(), index => GetPosition(index)) : new Vector3[0]);
			normals = (HasNormals ? CollectionHelper.Initialize(GetNormalCount(), index => GetNormal(index)) : new Vector3[0]);
			texCoord0s = (HasTexCoord0s ? CollectionHelper.Initialize(GetTexCoord0Count(), index => GetTexCoord0(index)) : new Vector2[0]);
			texCoord1s = (HasTexCoord1s ? CollectionHelper.Initialize(GetTexCoord1Count(), index => GetTexCoord1(index)) : new Vector2[0]);

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

		public void Export(string outputFolder, bool exportTextures, ImageFormat textureFormat, bool exportGeometry, bool onlyVisible, bool ignoreTransforms, bool swapU, bool swapV)
		{
			outputFolder = Path.Combine(outputFolder, name);
			if (!Directory.Exists(outputFolder))
			{
				Directory.CreateDirectory(outputFolder);
			}
			if (exportGeometry)
			{
				ExportGraphObject(outputFolder, 0, onlyVisible, ignoreTransforms, swapU, swapV, exportTextures, textureFormat);
			}
		}

		private void ExportGraphObject(string outputFolder, int index, bool onlyVisible, bool ignoreTransforms, bool swapU, bool swapV, bool exportTextures, ImageFormat textureFormat)
		{
			GraphObject graphObject = graphObjects[index];
			if (graphObject.Visible && graphObject.PartCount > 0)
			{
				using (StreamWriter streamWriter = File.CreateText(Path.Combine(outputFolder, string.Format("object{0}.obj", index))))
				using (StreamWriter streamWriter2 = (exportTextures) ? File.CreateText(Path.Combine(outputFolder, string.Format("object{0}.mtl", index))) : null)
				{
					if (exportTextures) streamWriter2.WriteLine("# object{0}.mtl converted at {1} using Demolisher v{2} by arookas", index, DateTime.Now, Program.Version);

					streamWriter.WriteLine("# object{0}.obj converted at {1} using Demolisher v{2} by arookas", index, DateTime.Now, Program.Version);
					if (exportTextures) streamWriter.WriteLine("mtllib object{0}.mtl", index);
					short[] posIndices = GetUsedVertexAttributes(graphObject, 0);
					Vector3[] array = CollectionHelper.Initialize<Vector3>(posIndices.Length, (int i) => positions[(int)posIndices[i]]);
					if (!ignoreTransforms)
					{
						array.Transform(delegate (Vector3 oP)
						{
							Vector3 vector4 = Vector3.Zero;
							Vector3 left = Vector3.Zero;
							Vector3 vector5 = Vector3.One;
							for (int num2 = index; num2 >= 0; num2 = (int)graphObjects[num2].ParentIndex)
							{
								vector4 += graphObjects[num2].Position;
								left += graphObjects[num2].Rotation;
								vector5 = Vector3.Multiply(vector5, graphObjects[num2].Scale);
							}
							Vector3 left2 = Vector3.Multiply(oP, vector5);
							return left2 + vector4 * 0.0039f;
						});
					}
					streamWriter.WriteLine();
					streamWriter.WriteLine("# vertices");
					foreach (Vector3 vector in array)
					{
						TextWriter textWriter = streamWriter;
						string format = "v {0} {1} {2}";
						float x = vector.X;
						object arg = x.ToString(CultureInfo.InvariantCulture);
						float y = vector.Y;
						object arg2 = y.ToString(CultureInfo.InvariantCulture);
						float z = vector.Z;
						textWriter.WriteLine(format, arg, arg2, z.ToString(CultureInfo.InvariantCulture));
					}
					short[] normalIndices = GetUsedVertexAttributes(graphObject, 1);
					Vector3[] array3 = CollectionHelper.Initialize<Vector3>(normalIndices.Length, (int i) => normals[(int)normalIndices[i]]);
					streamWriter.WriteLine();
					streamWriter.WriteLine("# normals");
					foreach (Vector3 vector2 in array3)
					{
						TextWriter textWriter2 = streamWriter;
						string format2 = "vn {0} {1} {2}";
						float x2 = vector2.X;
						object arg3 = x2.ToString(CultureInfo.InvariantCulture);
						float y2 = vector2.Y;
						object arg4 = y2.ToString(CultureInfo.InvariantCulture);
						float z2 = vector2.Z;
						textWriter2.WriteLine(format2, arg3, arg4, z2.ToString(CultureInfo.InvariantCulture));
					}
					short[] uvIndices = GetUsedVertexAttributes(graphObject, 2);
					Vector2[] array5 = CollectionHelper.Initialize<Vector2>(uvIndices.Length, (int i) => texCoord0s[(int)uvIndices[i]]); // TODO: Because the legacy code only worked with TEXCOORDS0, we ignore TEXCOORDS1. TEXCOORDS1 could be implemented in the future with a format other than OBJ.
					streamWriter.WriteLine();
					streamWriter.WriteLine("# uvs");
					foreach (Vector2 vector3 in array5)
					{
						streamWriter.WriteLine("vt {0} {1}", (swapU ? (-vector3.X) : vector3.X).ToString(CultureInfo.InvariantCulture), (swapV ? (-vector3.Y) : vector3.Y).ToString(CultureInfo.InvariantCulture));
					}
					streamWriter.WriteLine();
					streamWriter.WriteLine("# faces");
					int num = 0;
					foreach (Part part in graphObject.parts)
					{
						streamWriter.WriteLine();
						for (int texUnit = 0; texUnit < 8; texUnit++)
						{
							if (shaders[(int)part.ShaderIndex].MaterialIndex[texUnit] < 0)
							{
								streamWriter.WriteLine("g object{0}_part{1}", index, num++);
							}
							else
							{
								int objIndex = num++;
								Material objMat = materials[(int)shaders[(int)part.ShaderIndex].MaterialIndex[texUnit]];

								// Start faces group with texture material (smoothing off)
								streamWriter.WriteLine("g object{0}_part{1}_texture{2}", index, objIndex, objMat.textureIndex);
								// Material definition is still written, even if exportTextures/mtl isnt specified
								streamWriter.WriteLine("usemtl object{0}_part{1}_texture{2}", index, objIndex, objMat.textureIndex);
								streamWriter.WriteLine("s 1");

								// Save texture to file
								if (exportTextures)
								{
									Bitmap tex = textures[objMat.textureIndex].ToBitmap();
									tex.Save(Path.Combine(outputFolder, string.Format("object{0}_part{1}_texture{2}.{3}", index, objIndex, objMat.textureIndex, textureFormat.GetExtension())), textureFormat);

									// Write texture material information
									streamWriter2.WriteLine();
									streamWriter2.WriteLine("# object{0} - part{1} - texture{2}", index, objIndex, objMat.textureIndex);
									streamWriter2.WriteLine("# WrapS - {0}", objMat.wrapS.ToString());
									streamWriter2.WriteLine("# WrapT - {0}", objMat.wrapT.ToString());
									streamWriter2.WriteLine("newmtl object{0}_part{1}_texture{2}", index, objIndex, objMat.textureIndex);
									// TODO: Other data such as roughness, specularity, etc. would be used here but I dont have a frame of reference to get these things from this point
									streamWriter2.WriteLine("Ns 500");
									streamWriter2.WriteLine("Ka 0.8 0.8 0.8");
									streamWriter2.WriteLine("Kd 0.8 0.8 0.8");
									streamWriter2.WriteLine("Ks 0.8 0.8 0.8");
									streamWriter2.WriteLine("d 1");
									streamWriter2.WriteLine("illum 2");
									streamWriter2.WriteLine("map_Kd object{0}_part{1}_texture{2}.{3}", index, objIndex, objMat.textureIndex, textureFormat.GetExtension());
								}
							}
						}
						Primitive[] primitives = batches[(int)part.BatchIndex].primitives;
						for (int m = 0; m < primitives.Length; m++)
						{
							Primitive primitive = primitives[m];
							PrimitiveType type = primitive.Type;
							if (type != PrimitiveType.TriangleStrip)
							{
								if (type == PrimitiveType.TriangleFan)
								{
									streamWriter.WriteLine();
									streamWriter.WriteLine("# fan 0xA0");
									int vertex;
									for (vertex = 2; vertex < primitive.vertices.Length; vertex++)
									{
										TextWriter textWriter3 = streamWriter;
										string format3 = "f {0}/{3}/{6} {1}/{4}/{7} {2}/{5}/{8}";
										object[] array7 = new object[9];
										array7[0] = posIndices.IndexOfFirst((short pos) => pos == primitive.vertices[0].PositionIndex) + 1;
										array7[1] = posIndices.IndexOfFirst((short pos) => pos == primitive.vertices[vertex - 1].PositionIndex) + 1;
										array7[2] = posIndices.IndexOfFirst((short pos) => pos == primitive.vertices[vertex].PositionIndex) + 1;
										array7[3] = uvIndices.IndexOfFirst((short uv) => uv == primitive.vertices[0].UVIndex[0].GetValueOrDefault()) + 1; // TEXCOORD0 (UVIndex[0]) only
										array7[4] = uvIndices.IndexOfFirst((short uv) => uv == primitive.vertices[vertex - 1].UVIndex[0].GetValueOrDefault()) + 1; // TEXCOORD0 (UVIndex[0]) only
										array7[5] = uvIndices.IndexOfFirst((short uv) => uv == primitive.vertices[vertex].UVIndex[0].GetValueOrDefault()) + 1; // TEXCOORD0 (UVIndex[0]) only
										array7[6] = normalIndices.IndexOfFirst((short normal) => normal == primitive.vertices[0].NormalIndex) + 1;
										array7[7] = normalIndices.IndexOfFirst((short normal) => normal == primitive.vertices[vertex - 1].NormalIndex) + 1;
										array7[8] = normalIndices.IndexOfFirst((short normal) => normal == primitive.vertices[vertex].NormalIndex) + 1;
										textWriter3.WriteLine(format3, array7);
									}
								}
							}
							else
							{
								streamWriter.WriteLine();
								streamWriter.WriteLine("# strip 0x98");
								bool flag = true;
								int vertex = 2;
								while (vertex < primitive.vertices.Length)
								{
									TextWriter textWriter4 = streamWriter;
									string format4 = flag ? "f {2}/{5}/{8} {1}/{4}/{7} {0}/{3}/{6}" : "f {0}/{3}/{6} {1}/{4}/{7} {2}/{5}/{8}";
									object[] array8 = new object[9];
									array8[0] = posIndices.IndexOfFirst((short pos) => pos == primitive.vertices[vertex - 2].PositionIndex) + 1;
									array8[1] = posIndices.IndexOfFirst((short pos) => pos == primitive.vertices[vertex - 1].PositionIndex) + 1;
									array8[2] = posIndices.IndexOfFirst((short pos) => pos == primitive.vertices[vertex].PositionIndex) + 1;
									array8[3] = uvIndices.IndexOfFirst((short uv) => uv == primitive.vertices[vertex - 2].UVIndex[0].GetValueOrDefault()) + 1; // TEXCOORD0 (UVIndex[0]) only
									array8[4] = uvIndices.IndexOfFirst((short uv) => uv == primitive.vertices[vertex - 1].UVIndex[0].GetValueOrDefault()) + 1; // TEXCOORD0 (UVIndex[0]) only
									array8[5] = uvIndices.IndexOfFirst((short uv) => uv == primitive.vertices[vertex].UVIndex[0].GetValueOrDefault()) + 1; // TEXCOORD0 (UVIndex[0]) only
									array8[6] = normalIndices.IndexOfFirst((short normal) => normal == primitive.vertices[vertex - 2].NormalIndex) + 1;
									array8[7] = normalIndices.IndexOfFirst((short normal) => normal == primitive.vertices[vertex - 1].NormalIndex) + 1;
									array8[8] = normalIndices.IndexOfFirst((short normal) => normal == primitive.vertices[vertex].NormalIndex) + 1;
									textWriter4.WriteLine(format4, array8);
									vertex++;
									flag = !flag;
								}
							}
						}
					}
				}
			}
			if (graphObject.Visible)
			{
				for (int n = (int)graphObject.ChildIndex; n >= 0; n = (int)graphObjects[n].ChildIndex)
				{
					ExportGraphObject(outputFolder, n, onlyVisible, ignoreTransforms, swapU, swapV, exportTextures, textureFormat);
				}
			}
			for (int nextIndex = (int)graphObject.NextIndex; nextIndex >= 0; nextIndex = (int)graphObjects[nextIndex].NextIndex)
			{
				ExportGraphObject(outputFolder, nextIndex, onlyVisible, ignoreTransforms, swapU, swapV, exportTextures, textureFormat);
			}
		}

		private short[] GetUsedVertexAttributes(GraphObject graphObject, int attribute)
		{
			HashSet<short> hashSet = new HashSet<short>();
			foreach (Part part in graphObject.parts)
			{
				foreach (Primitive primitive in batches[(int)part.BatchIndex].primitives)
				{
					foreach (Vertex vertex in primitive.vertices)
					{
						switch (attribute)
						{
							case 0:
								hashSet.Add(vertex.PositionIndex.GetValueOrDefault());
								break;
							case 1:
								hashSet.Add(vertex.NormalIndex.GetValueOrDefault());
								break;
							case 2:
								hashSet.Add(vertex.UVIndex[0].GetValueOrDefault()); // TEXCOORD0 (UVIndex[0]) only
								break;
						}
					}
				}
			}
			return hashSet.ToArray<short>();
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