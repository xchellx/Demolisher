using arookas.Collections;
using arookas.IO.Binary;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace arookas {
	class demoBinModel : IRenderable, IDisposable {
		aBinaryReader mReader;
		byte mUnk1;
		string mName;
		uint[] mOffsets;
		bool mDisposed;

		demoObject[] mGraph;
		demoBatch[] mBatch;
		demoShader[] mShader;
		demoMaterial[] mMaterial;
		demoTexture[] mTexture;
		Vector3[] mPos;
		Vector3[] mNormal;
		Vector2[] mCoord0;
		Vector2[] mCoord1;
		int[] mId;

		const float cGlobalScale = 256.0f;
		const float cGlobalScaleReciprocal = (1.0f / cGlobalScale);

		uint TextureOffset {
			get { return mOffsets[0]; }
		}
		uint MaterialOffset {
			get { return mOffsets[1]; }
		}
		uint PositionOffset {
			get { return mOffsets[2]; }
		}
		uint NormalOffset {
			get { return mOffsets[3]; }
		}
		uint Coord0Offset {
			get { return mOffsets[6]; }
		}
		uint Coord1Offset {
			get { return mOffsets[7]; }
		}
		uint ShaderOffset {
			get { return mOffsets[10]; }
		}
		uint BatchOffset {
			get { return mOffsets[11]; }
		}
		uint GraphOffset {
			get { return mOffsets[12]; }
		}

		public bool Visible {
			get { return mGraph[0].Visible; }
			set { mGraph[0].Visible = value; }
		}

		public Vector3 Position {
			get { return mGraph[0].Position; }
			set { mGraph[0].Position = value; }
		}
		public Vector3 Rotation {
			get { return mGraph[0].Rotation; }
			set { mGraph[0].Rotation = value; }
		}
		public Vector3 Scale {
			get { return mGraph[0].Scale; }
			set { mGraph[0].Scale = value; }
		}

		public string Name {
			get { return mName; }
		}

		public demoObject this[int index] {
			get { return mGraph[index]; }
		}

		public demoBinModel(string file)
			: this(file, Vector3.Zero, Vector3.Zero, Vector3.One) { }
		public demoBinModel(string file, Vector3 pos, Vector3 rot, Vector3 scale) {
			mReader = new aBinaryReader(File.OpenRead(file), Endianness.Big, Encoding.GetEncoding(932));
			mUnk1 = mReader.Read8();
			mName = mReader.ReadString<aCSTR>(11);
			mOffsets = mReader.Read32s(21);

			mGraph = (GraphOffset > 0 ? fetchObjects(0) : new demoObject[0]);
			mBatch = loadSection(calcBatchCount(), fetchBatch);
			mShader = loadSection(calcShaderCount(), fetchShader);
			mMaterial = loadSection(calcMaterialCount(), fetchMaterial);
			mTexture = loadSection(calcTextureCount(), fetchTexture);
			mPos = loadSection(calcPositionCount(), fetchPosition);
			mNormal = loadSection(calcNormalCount(), fetchNormal);
			mCoord0 = loadSection(calcCoord0Count(), fetchCoord0);
			mCoord1 = loadSection(calcCoord1Count(), fetchCoord1);

			mId = mTexture.Select(texture => texture.toGL()).ToArray();

			Position = pos;
			Rotation = rot;
			Scale = scale;
		}

		T[] loadSection<T>(int count, Func<int, T> loadfunc) {
			return aCollection.Initialize(count, i => loadfunc(i));
		}

		int calcBatchCount() {
			var batch = -1;
			foreach (var obj in mGraph) {
				foreach (var part in obj) {
					if (part.Batch > batch) {
						batch = part.Batch;
					}
				}
			}
			return (batch + 1);
		}
		int calcMaterialCount() {
			var material = -1;
			foreach (var shader in mShader) {
				if (shader.Material.Max() > material) {
					material = shader.Material.Max();
				}
			}
			return (material + 1);
		}
		int calcNormalCount() {
			var normal = -1;
			foreach (var batch in mBatch) {
				foreach (var primitive in batch) {
					foreach (var vertex in primitive.Where(vertex => vertex.NormalIndex != null)) {
						if (vertex.NormalIndex > normal) {
							normal = vertex.NormalIndex.Value;
						}
						if (vertex.BinormalIndex != null) {
							if (vertex.BinormalIndex + 1 > normal) {
								normal = vertex.BinormalIndex.Value + 1;
							}
						}
						if (vertex.TangentIndex != null) {
							if (vertex.TangentIndex + 2 > normal) {
								normal = vertex.TangentIndex.Value + 2;
							}
						}
					}
				}
			}
			return (normal + 1);
		}
		int calcPositionCount() {
			var pos = -1;
			foreach (var batch in mBatch) {
				foreach (var primitive in batch) {
					foreach (var vertex in primitive.Where(vertex => vertex.PositionIndex != null)) {
						if (vertex.PositionIndex > pos) {
							pos = vertex.PositionIndex.Value;
						}
					}
				}
			}
			return (pos + 1);
		}
		int calcShaderCount() {
			var shader = -1;
			foreach (var obj in mGraph) {
				foreach (var part in obj) {
					if (part.Shader > shader) {
						shader = part.Shader;
					}
				}
			}
			return (shader + 1);
		}
		int calcTextureCount() {
			var tex = 0;
			foreach (var material in mMaterial) {
				if (material.mTex > tex) {
					tex = material.mTex;
				}
			}
			return (tex + 1);
		}
		int calcCoord0Count() {
			var coord0 = -1;
			foreach (var batch in mBatch) {
				foreach (var primitive in batch) {
					foreach (var vertex in primitive.Where(vertex => vertex.UVIndex[0] != null && vertex.UVIndex[0] > coord0)) {
						coord0 = vertex.UVIndex[0].Value;
					}
				}
			}
			return (coord0 + 1);
		}
		int calcCoord1Count() {
			var coord1 = -1;
			foreach (var batch in mBatch) {
				foreach (var primitive in batch) {
					foreach (var vertex in primitive.Where(vertex => vertex.UVIndex[1] != null && vertex.UVIndex[1] > coord1)) {
						coord1 = vertex.UVIndex[1].Value;
					}
				}
			}
			return (coord1 + 1);
		}

		demoBatch fetchBatch(int index) {
			const int cItemSize = 0x18;
			mReader.Keep();
			mReader.Goto(BatchOffset);
			mReader.PushAnchor();
			mReader.Goto(cItemSize * index);
			demoBatch batch = new demoBatch(mReader);
			mReader.PopAnchor();
			mReader.Back();
			return batch;
		}
		demoObject fetchObject(int index) {
			const int itemSize = 0x8C;
			mReader.Keep();
			mReader.Goto(GraphOffset);
			mReader.PushAnchor();
			mReader.Goto(itemSize * index);
			var obj = new demoObject(mReader);
			mReader.PopAnchor();
			mReader.Back();
			return obj;
		}
		demoObject[] fetchObjects(int index) {
			var objs = new List<demoObject>(10);
			var obj = fetchObject(index);
			objs.Add(obj);
			if (obj.Child >= 0) {
				objs.AddRange(fetchObjects(obj.Child));
			}
			if (obj.Next >= 0) {
				objs.AddRange(fetchObjects(obj.Next));
			}
			return objs.ToArray();
		}
		demoMaterial fetchMaterial(int index) {
			const int cItemSize = 0x14;
			mReader.Keep();
			mReader.Goto(MaterialOffset + cItemSize * index);
			var material = new demoMaterial(mReader);
			mReader.Back();
			return material;
		}
		Vector3 fetchNormal(int index) {
			const int cItemSize = 0xC;
			mReader.Keep();
			mReader.Goto(NormalOffset + cItemSize * index);
			Vector3 normal = new Vector3(mReader.ReadF32(), mReader.ReadF32(), mReader.ReadF32());
			mReader.Back();
			return normal;
		}
		Vector3 fetchPosition(int index) {
			const int itemSize = 0x6;
			mReader.Keep();
			mReader.Goto(PositionOffset + itemSize * index);
			Vector3 pos = new Vector3(mReader.ReadS16() / cGlobalScale, mReader.ReadS16() / cGlobalScale, mReader.ReadS16() / cGlobalScale);
			mReader.Back();
			return pos;
		}
		demoShader fetchShader(int index) {
			const int itemSize = 0x28;
			mReader.Keep();
			mReader.Goto(ShaderOffset + itemSize * index);
			demoShader shader = new demoShader(mReader);
			mReader.Back();
			return shader;
		}
		demoTexture fetchTexture(int index) {
			const int itemSize = 0xC;
			mReader.Keep();
			mReader.Goto(TextureOffset);
			mReader.PushAnchor();
			mReader.Goto(itemSize * index);
			var width = mReader.Read16();
			var height = mReader.Read16();
			var format = (demoTexFormat)mReader.Read8();
			var unk1 = mReader.Read8();
			if (mReader.Read16() != 0) {
				// throw new Exception("Texture.unk4 != 0");
			}
			mReader.Goto(mReader.Read32());
			var texture = demoTexture.loadTexture(width, height, format, mReader);
			mReader.PopAnchor();
			mReader.Back();
			return texture;
		}
		Vector2 fetchCoord0(int index) {
			const int cItemSize = 0x8;
			mReader.Keep();
			mReader.Goto(Coord0Offset + cItemSize * index);
			Vector2 texCoord = new Vector2(mReader.ReadF32(), mReader.ReadF32());
			mReader.Back();
			return texCoord;
		}
		Vector2 fetchCoord1(int index) {
			const int cItemSize = 0x8;
			mReader.Keep();
			mReader.Goto(Coord1Offset + cItemSize * index);
			Vector2 texCoord = new Vector2(mReader.ReadF32(), mReader.ReadF32());
			mReader.Back();
			return texCoord;
		}

		public void render(demoRenderFlags flags) {
			GL.PushMatrix();
			for (var index = 0; index >= 0; index = mGraph[index].Next) {
				renderObject(mGraph[index], flags);
			}
			GL.PopMatrix();
		}
		void renderObject(demoObject obj, demoRenderFlags flags) {
			if (!obj.Visible) {
				return;
			}
			if (!flags.HasFlag(demoRenderFlags.CEILINGS) && obj.hasFlag(demoObjectFlags.CEILING)) {
				return;
			}
			if (!flags.HasFlag(demoRenderFlags.FOURTHWALLS) && obj.hasFlag(demoObjectFlags.FOURTHWALL)) {
				return;
			}

			GL.PushMatrix();
			transformMatrix(obj.Position, obj.Rotation, obj.Scale);

			if (flags.HasFlag(demoRenderFlags.BOUNDINGBOX)) {
				GL.Disable(EnableCap.Lighting);
				GL.Disable(EnableCap.Texture2D);
				GL.Color3(aRGBA.Yellow);
				gl.drawWireCube(obj.Bounds * cGlobalScaleReciprocal);
				GL.Color3(aRGBA.White);
				GL.Enable(EnableCap.Texture2D);
				GL.Enable(EnableCap.Lighting);
			}

			if (obj.hasFlag(demoObjectFlags.FULLBRIGHT)) {
				GL.Disable(EnableCap.Lighting);
			}
			foreach (var part in obj) {
				if (mShader[part.Shader].Tint.a > 0 && ((mShader[part.Shader].Tint.a == aRGBA.cOpaque && flags.HasFlag(demoRenderFlags.OPAQUE)) || (mShader[part.Shader].Tint.a < aRGBA.cOpaque && flags.HasFlag(demoRenderFlags.TRANSLUCENT)))) {
					renderPart(part, flags);
				}
			}
			GL.Enable(EnableCap.Lighting);
			for (int childIndex = obj.Child; childIndex >= 0; childIndex = mGraph[childIndex].Next) {
				renderObject(mGraph[childIndex], flags);
			}
			GL.PopMatrix();
		}
		void renderPart(demoPart part, demoRenderFlags renderFlags) {
			GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, gl.convert(mShader[part.Shader].Tint));
			var batch = mBatch[part.Batch];

			if (batch.UseNBT) { // bump map
				int binormalAttribute = GL.GetAttribLocation(Program.sEmboss, "inBinormal");
				int tangentAttribute = GL.GetAttribLocation(Program.sEmboss, "inTangent");
				int diffuseMap = GL.GetUniformLocation(Program.sEmboss, "diffuseMap");
				int bumpMap = GL.GetUniformLocation(Program.sEmboss, "bumpMap");
				int embossFactor = GL.GetUniformLocation(Program.sEmboss, "embossScale");

				Program.sEmboss.use();

				GL.ActiveTexture(TextureUnit.Texture0 + 0);
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, mId[mMaterial[mShader[part.Shader].Material[0]].mTex]);
				gl.setWrapModeS(mMaterial[mShader[part.Shader].Material[0]].mWrapS);
				gl.setWrapModeT(mMaterial[mShader[part.Shader].Material[0]].mWrapT);
				GL.Uniform1(diffuseMap, 0);

				GL.ActiveTexture(TextureUnit.Texture0 + 1);
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, mId[mMaterial[mShader[part.Shader].Material[1]].mTex]);
				gl.setWrapModeS(mMaterial[mShader[part.Shader].Material[1]].mWrapS);
				gl.setWrapModeT(mMaterial[mShader[part.Shader].Material[1]].mWrapT);
				GL.Uniform1(bumpMap, 1);

				GL.Uniform1(embossFactor, 2.0f);

				foreach (var primitive in batch) {
					GL.Begin(primitive.GLType);

					foreach (var vertex in primitive) {
						GL.MultiTexCoord2(TextureUnit.Texture0 + 0, ref mCoord0[vertex.UVIndex[0].Value]);
						GL.MultiTexCoord2(TextureUnit.Texture0 + 1, ref mCoord1[vertex.UVIndex[1].Value]);
						GL.Normal3(mNormal[vertex.NormalIndex.Value]);
						GL.VertexAttrib3(tangentAttribute, ref mNormal[vertex.BinormalIndex.Value + 1]);
						GL.VertexAttrib3(binormalAttribute, ref mNormal[vertex.TangentIndex.Value + 2]);
						GL.Vertex3(mPos[vertex.PositionIndex.Value]);
					}

					GL.End();
				}

				GL.UseProgram(0);
			}
			else {
				for (int texUnit = 0; texUnit < 8; texUnit++) {
					if (mShader[part.Shader].Material[texUnit] < 0) {
						continue;
					}

					var material = mMaterial[mShader[part.Shader].Material[texUnit]];
					GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
					GL.Enable(EnableCap.Texture2D);
					GL.BindTexture(TextureTarget.Texture2D, mId[material.mTex]);
					gl.setWrapModeS(material.mWrapS);
					gl.setWrapModeT(material.mWrapT);
				}

				foreach (var primitive in batch) {
					GL.Begin(primitive.GLType);

					foreach (var vertex in primitive) {
						if (vertex.UVIndex[0] != null) {
							GL.MultiTexCoord2(TextureUnit.Texture0, ref mCoord0[vertex.UVIndex[0].Value]);
						}

						if (vertex.NormalIndex != null) {
							GL.Normal3(mNormal[vertex.NormalIndex.Value]);
						}

						GL.Vertex3(mPos[vertex.PositionIndex.Value]);
					}

					GL.End();
				}
			}

			gl.disableTexture2D();

			// normals
			if (renderFlags.HasFlag(demoRenderFlags.NBT)) {
				const float normalLength = 0.0625f;

				GL.Disable(EnableCap.Lighting);
				GL.Begin(BeginMode.Lines);

				foreach (var primitive in batch) {
					foreach (var vertex in primitive) {
						if (vertex.NormalIndex != null) {
							GL.Color4(Color4.Red);
							GL.Vertex3(mPos[vertex.PositionIndex.Value]);
							GL.Vertex3(mPos[vertex.PositionIndex.Value] + mNormal[vertex.NormalIndex.Value] * normalLength);
						}

						if (vertex.TangentIndex != null) {
							GL.Color4(Color4.Green);
							GL.Vertex3(mPos[vertex.PositionIndex.Value]);
							GL.Vertex3(mPos[vertex.PositionIndex.Value] + mNormal[vertex.TangentIndex.Value + 2] * normalLength);
						}

						if (vertex.BinormalIndex != null) {
							GL.Color4(Color4.Blue);
							GL.Vertex3(mPos[vertex.PositionIndex.Value]);
							GL.Vertex3(mPos[vertex.PositionIndex.Value] + mNormal[vertex.BinormalIndex.Value + 1] * normalLength);
						}
					}
				}

				GL.End();
				GL.Enable(EnableCap.Lighting);
			}
		}

		public static void transformMatrix(Vector3 pos, Vector3 rot, Vector3 scale) {
			// for some reason, rotation screws everything up if you do it before translation???
			GL.Scale(scale);
			GL.Translate(pos * cGlobalScaleReciprocal);
			GL.Rotate(rot.Z, Vector3.UnitZ);
			GL.Rotate(rot.X, Vector3.UnitX);
			GL.Rotate(rot.Y, Vector3.UnitY);
		}

		public void Dispose() {
			if (!mDisposed) {
				GL.DeleteTextures(mId.Length, mId);
				mDisposed = true;
			}
		}
	}

	class demoObject : IRenderable, IEnumerable<demoPart> {
		Vector3 mPos, mRot, mScale;
		demoBoundingBox mBounds;
		demoPart[] mParts;
		int mParent, mChild, mNext, mPrev;
		demoObjectFlags mFlags;
		float mUnk3;
		bool mVisible;

		public Vector3 Position {
			get { return mPos; }
			set { mPos = value; }
		}
		public Vector3 Rotation {
			get { return mRot; }
			set { mRot = value; }
		}
		public Vector3 Scale {
			get { return mScale; }
			set { mScale = value; }
		}

		public demoBoundingBox Bounds {
			get { return mBounds; }
		}

		public int Parent {
			get { return mParent; }
		}
		public int Child {
			get { return mChild; }
		}
		public int Next {
			get { return mNext; }
		}
		public int Prev {
			get { return mPrev; }
		}

		public demoObjectFlags Flags {
			get { return mFlags; }
		}

		public bool Visible {
			get { return mVisible; }
			set { mVisible = true; }
		}

		public int PartCount {
			get { return mParts.Length; }
		}
		public demoPart this[int index] {
			get { return mParts[index]; }
		}

		public demoObject(aBinaryReader reader) {
			mVisible = true;
			mParent = reader.ReadS16();
			mChild = reader.ReadS16();
			mNext = reader.ReadS16();
			mPrev = reader.ReadS16();

			if (reader.Read8() != 0) {
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", reader.Stream.Position));
#endif
			}

			mFlags = (demoObjectFlags)reader.Read8();

			if (reader.Read16() != 0) {
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", reader.Stream.Position));
#endif
			}

			mScale = new Vector3(reader.ReadF32(), reader.ReadF32(), reader.ReadF32());
			mRot = new Vector3(reader.ReadF32(), reader.ReadF32(), reader.ReadF32());
			mPos = new Vector3(reader.ReadF32(), reader.ReadF32(), reader.ReadF32());
			mBounds = new demoBoundingBox(reader.readVec3(), reader.readVec3());
			mUnk3 = reader.ReadF32();

			var partCount = reader.ReadS16();

			if (reader.Read16() != 0) {
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", reader.Stream.Position));
#endif
			}

			var partOffset = reader.ReadS32();

			if (reader.Read32s(7).Any(zero => zero != 0)) {
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("GraphObject padding != 0 at 0x{0:X8}.", reader.Stream.Position));
#endif
			}

			reader.Keep();
			reader.Goto(partOffset);
			mParts = aCollection.Initialize(partCount, () => new demoPart(reader));
			reader.Back();
		}

		public bool hasFlag(demoObjectFlags flags) {
			return (Flags & flags) != 0;
		}

		public IEnumerator<demoPart> GetEnumerator() {
			return mParts.GetArrayEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}


	[Flags]
	enum demoObjectFlags : byte {
		UNK01 = 0x01,
		UNK02 = 0x02,
		FOURTHWALL = 0x04, // invisible (except in GBH view)
		TRANSPARENT = 0x08, // transparent (when luigi is behind it)?
		UNK10 = 0x10,
		UNK20 = 0x20,
		FULLBRIGHT = 0x40, // fullbright (ignore lighting)
		CEILING = 0x80, // transparent/ceiling (will fade out when luigi vaccuums the floor)
	}

	class demoPart {
		short mShader, mBatch;

		public short Shader {
			get { return mBatch; }
		}
		public short Batch {
			get { return mBatch; }
		}

		public demoPart(aBinaryReader reader) {
			mShader = reader.ReadS16();
			mBatch = reader.ReadS16();
		}
	}

	class demoBatch : IEnumerable<demoPrimitive> {
		demoPrimitive[] mPrimitives;
		demoBatchAttributes mAttr;
		int mFaces;
		uint mOffset;
		bool mUseNBT;
		bool mUseNormals;
		byte mPositions;

		public demoBatchAttributes Attributes {
			get { return mAttr; }
		}
		public int FaceCount {
			get { return mFaces; }
		}
		public uint Offset {
			get { return mOffset; }
		}
		public bool UseNBT { get; private set; }
		public bool UseNormals { get; private set; }

		public demoPrimitive this[int index] {
			get { return mPrimitives[index]; }
		}

		public demoBatch(aBinaryReader reader) {
			mFaces = reader.Read16();
			var size = (reader.Read16() << 5);
			mAttr = (demoBatchAttributes)(uint)reader.Read32();
			mUseNormals = (reader.Read8() != 0);
			mPositions = reader.Read8();
			var uvs = reader.Read8();
			mUseNBT = (reader.Read8() != 0);
			mOffset = reader.Read32();

			if (reader.Read8s(8).Any(zero => zero != 0)) {
#if AROOKAS_DEMOLISHER_CHECKPADDING
				throw new Exception(String.Format("Batch padding != 0 at 0x{0:X8}.", reader.Stream.Position));
#endif
			}

			var faces = 0;
			var primitives = new List<demoPrimitive>();
			reader.Goto(Offset);
			while (faces < FaceCount && reader.Position < Offset + size) {
				var primitive = new demoPrimitive(reader, mUseNBT, uvs, mAttr);
				if (!primitive.Type.IsDefined()) {
					break;
				}
				primitives.Add(primitive);
				faces += primitive.FaceCount;
			}
			mPrimitives = primitives.ToArray();
		}

		public bool hasAttribute(demoBatchAttributes attr) {
			return (Attributes & attr) != 0;
		}

		public IEnumerator<demoPrimitive> GetEnumerator() {
			return mPrimitives.GetArrayEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	[Flags]
	enum demoBatchAttributes : uint { // same order as GX
		POSNORMMATRIX = 1 << 0,
		TEX0MATRIX = 1 << 1,
		TEX1MATRIX = 1 << 2,
		TEX2MATRIX = 1 << 3,
		TEX3MATRIX = 1 << 4,
		TEX4MATRIX = 1 << 5,
		TEX5MATRIX = 1 << 6,
		TEX6MATRIX = 1 << 7,
		TEX7MATRIX = 1 << 8,
		POSITION = 1 << 9,
		NORMAL = 1 << 10,
		COLOR0 = 1 << 11,
		COLOR1 = 1 << 12,
		TEXCOORD0 = 1 << 13,
		TEXCOORD1 = 1 << 14,
		TEXCOORD2 = 1 << 15,
		TEXCOORD3 = 1 << 16,
		TEXCOORD4 = 1 << 17,
		TEXCOORD5 = 1 << 18,
		TEXCOORD6 = 1 << 19,
		TEXCOORD7 = 1 << 20,
		POSITIONMATRIXARRAY = 1 << 21,
		NORMALMATRIXARRAY = 1 << 22,
		TEXMATRIXARRAY = 1 << 23,
		LIGHTARRAY = 1 << 24,
		NORMALBINORMALTANGENT = 1 << 25,
	}

	class demoShader {
		short[] mMaterial;
		aRGBA mTint;

		int mUnk1;
		byte mUnk2;
		short[] mUnk3;

		public short[] Material {
			get { return mMaterial; }
		}
		public aRGBA Tint {
			get { return mTint; }
		}

		public demoShader(aBinaryReader reader) {
			mUnk1 = reader.read24();
			mTint = aRGBA.FromRGBA8(reader.Read32());
			mUnk2 = reader.Read8();
			mMaterial = reader.ReadS16s(8);
			mUnk3 = reader.ReadS16s(8);
			if (mUnk3[0] != 0 || mUnk3[1] != -1) {
				// throw new Exception("shader unk3!!");
			}
		}
	}

	class demoMaterial {
		public short mTex;
		public short mUnk1; // always -1
		public demoWrapMode mWrapS;
		public demoWrapMode mWrapT;
		public ushort mUnk3; // unknown, flags??
		public uint mUnk4; // zeroes
		public uint mUnk5; // zeroes
		public uint mUnk6; // zeroes

		public demoMaterial(aBinaryReader reader) {
			mTex = reader.ReadS16();
			mUnk1 = reader.ReadS16();
			mWrapS = (demoWrapMode)reader.Read8();
			mWrapT = (demoWrapMode)reader.Read8();
			mUnk3 = reader.Read16();
			mUnk4 = reader.Read32();
			mUnk5 = reader.Read32();
			mUnk6 = reader.Read32();
		}
	}

	class demoPrimitive : IEnumerable<demoVertex> {
		demoPrimitiveType mType;
		demoVertex[] mVertices;

		public demoPrimitiveType Type {
			get { return mType; }
		}

		public int VertexCount {
			get { return mVertices.Length; }
		}
		public int FaceCount {
			get { return (VertexCount - 2); }
		}

		public BeginMode GLType {
			get {
				switch (Type) {
					case demoPrimitiveType.LINE: return BeginMode.Lines;
					case demoPrimitiveType.LINESTRIP: return BeginMode.LineStrip;
					case demoPrimitiveType.POINT: return BeginMode.Points;
					case demoPrimitiveType.TRIANGLEFAN: return BeginMode.TriangleFan;
					case demoPrimitiveType.TRIANGLE: return BeginMode.Triangles;
					case demoPrimitiveType.TRIANGLESTRIP: return BeginMode.TriangleStrip;
					default: {
						throw new InvalidOperationException("Primitive type is invalid.");
					}
				}
			}
		}

		public demoVertex this[int index] {
			get { return mVertices[index]; }
		}

		public demoPrimitive(aBinaryReader reader, bool useNBT, int uvCount, demoBatchAttributes attr) {
			mType = (demoPrimitiveType)reader.Read8();
			mVertices = aCollection.Initialize<demoVertex>(reader.Read16(), () => new demoVertex(reader, useNBT, uvCount, attr));
		}

		public IEnumerator<demoVertex> GetEnumerator() {
			return mVertices.GetArrayEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	enum demoPrimitiveType {
		POINT = 0x80,
		TRIANGLE = 0x90,
		TRIANGLESTRIP = 0x98,
		TRIANGLEFAN = 0xA0,
		LINE = 0xA8,
		LINESTRIP = 0xB8,
	}

	class demoVertex {
		short? mMatrix, mPos, mNormal, mBinormal, mTangent;
		short?[] mColor;
		short?[] mUV;

		public short? MatrixIndex {
			get { return mMatrix; }
		}
		public short? PositionIndex {
			get { return mPos; }
		}
		public short? NormalIndex {
			get { return mNormal; }
		}
		public short? BinormalIndex {
			get { return mBinormal; }
		}
		public short? TangentIndex {
			get { return mTangent; }
		}
		public short?[] ColorIndex {
			get { return mColor; }
		}
		public short?[] UVIndex {
			get { return mUV; }
		}

		public demoVertex(aBinaryReader reader, bool useNBT, int uvCount, demoBatchAttributes attr) {
			mMatrix = null;
			mPos = null;
			mNormal = null;
			mBinormal = null;
			mTangent = null;
			mColor = new short?[2];
			mUV = new short?[8];

			if (attr.HasFlag(demoBatchAttributes.POSITION)) {
				mPos = reader.ReadS16();
			}
			if (attr.HasFlag(demoBatchAttributes.NORMAL)) {
				mNormal = reader.ReadS16();
				if (useNBT) {
					mBinormal = reader.ReadS16();
					mTangent = reader.ReadS16();
				}
			}
			if (attr.HasFlag(demoBatchAttributes.COLOR0)) {
				mColor[0] = reader.ReadS16();
			}
			if (attr.HasFlag(demoBatchAttributes.COLOR1)) {
				mColor[1] = reader.ReadS16();
			}
			for (var tex = 0; tex < uvCount; tex++) {
				if (attr.HasFlag((demoBatchAttributes)(1 << (13 + tex)))) {
					mUV[tex] = reader.ReadS16();
				}
			}
		}
	}
}
