using arookas.Collections;
using arookas.IO.Binary;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Linq;

namespace arookas {
	class demoTexture {
		aRGBA[] mData;
		demoTexFormat mFormat;
		int mWidth, mHeight;

		public int Width {
			get { return mWidth; }
		}
		public int Height {
			get { return mHeight; }
		}
		public demoTexFormat Format {
			get { return mFormat; }
		}

		static int[] Lookup4Bit {
			get {
				if (s4BitLUT == null) {
					s4BitLUT = aCollection.Initialize(16, index => (int)(255.0d / 15.0d * index));
				}
				return s4BitLUT;
			}
		}
		static int[] s4BitLUT;

		demoTexture() { }

		public demoTexture createInverse() {
			var tex = new demoTexture();
			tex.mWidth = mWidth;
			tex.mHeight = mHeight;
			tex.mFormat = mFormat;
			tex.mData = mData.Select(i => new aRGBA(255 - i.r, 255 - i.g, 255 - i.b, i.a)).ToArray();
			return tex;
		}

		public Bitmap toBitmap() {
			var bitmap = new Bitmap(Width, Height);
			for (var y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					bitmap.SetPixel(x, y, mData[Width * y + x]);
				}
			}
			return bitmap;
		}

		public int toGL() {
			var id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, id);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, mWidth, mHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, mData);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
			return id;
		}
		public int toGL(float intensity) {
			GL.PixelTransfer(PixelTransferParameter.RedScale, intensity);
			GL.PixelTransfer(PixelTransferParameter.GreenScale, intensity);
			GL.PixelTransfer(PixelTransferParameter.BlueScale, intensity);
			var tex = toGL();
			GL.PixelTransfer(PixelTransferParameter.RedScale, 1.0f);
			GL.PixelTransfer(PixelTransferParameter.GreenScale, 1.0f);
			GL.PixelTransfer(PixelTransferParameter.BlueScale, 1.0f);
			return tex;
		}
		
		public static demoTexture loadTexture(int width, int height, demoTexFormat format, aBinaryReader reader) {
			var tex = new demoTexture();
			tex.mWidth = width;
			tex.mHeight = height;
			switch (format) {
				case demoTexFormat.I4: loadTextureI4(tex, reader); break;
				case demoTexFormat.I8: loadTextureI8(tex, reader); break;
				case demoTexFormat.IA4: loadTextureIA4(tex, reader); break;
				case demoTexFormat.IA8: loadTextureIA8(tex, reader); break;
				case demoTexFormat.RGB565: loadTextureRGB565(tex, reader); break;
				case demoTexFormat.RGB5A3: loadTextureRGB5A3(tex, reader); break;
				case demoTexFormat.RGBA8: loadTextureRGB8(tex, reader); break;
				case demoTexFormat.CMP: loadTextureCMP(tex, reader); break;
				default: {
					throw new NotImplementedException(String.Format("Encountered an unsupported texture format {0}.", format));
				}
			}
			return tex;
		}

		public static void loadTextureI4(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 8) {
				for (var x = 0; x < tex.mWidth; x += 8) {
					for (var by = 0; by < 8 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 8 && x + bx < tex.mWidth; bx += 2) {
							var i4 = reader.Read8();
							tex.mData[tex.mWidth * (y + by) + (x + bx)] = new aRGBA(Lookup4Bit[(i4 >> 4) & 0xF]);
							tex.mData[tex.mWidth * (y + by) + (x + bx + 1)] = new aRGBA(Lookup4Bit[i4 & 0xF]);
						}
					}
				}
			}
		}
		public static void loadTextureI8(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 4) {
				for (var x = 0; x < tex.mWidth; x += 8) {
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 8 && x + bx < tex.mWidth; bx++) {
							tex.mData[tex.mWidth * (y + by) + (x + bx)] = new aRGBA(reader.Read8());
						}
					}
				}
			}
		}
		public static void loadTextureIA4(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 4) {
				for (var x = 0; x < tex.mWidth; x += 4) {
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							var ia4 = reader.Read8();
							tex.mData[tex.mWidth * (y + by) + (x + bx)] = new aRGBA(Lookup4Bit[(ia4 >> 4) & 0xF], Lookup4Bit[ia4 & 0xF]);
						}
					}
				}
			}
		}
		public static void loadTextureIA8(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 4) {
				for (var x = 0; x < tex.mWidth; x += 4) {
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							tex.mData[tex.mWidth * (y + by) + (x + bx)] = new aRGBA(reader.Read8(), reader.Read8());
						}
					}
				}
			}
		}
		public static void loadTextureRGB565(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 4) {
				for (var x = 0; x < tex.mWidth; x += 4) {
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							tex.mData[tex.mWidth * (y + by) + (x + bx)] = aRGBA.FromRGB565(reader.Read16());
						}
					}
				}
			}
		}
		public static void loadTextureRGB5A3(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 4) {
				for (var x = 0; x < tex.mWidth; x += 4) {
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							var u16 = reader.Read16();
							tex.mData[tex.mWidth * (y + by) + (x + bx)] =
								(u16 & 0x8000) != 0 ?
								aRGBA.FromRGB5(u16) :
								aRGBA.FromRGB4A3(u16);
						}
					}
				}
			}
		}
		public static void loadTextureRGB8(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 4) {
				for (var x = 0; x < tex.mWidth; x += 4) {
					var colors = new uint[16];
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) { // AR
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							colors[4 * by + bx] = (uint)(reader.Read8() << 16);
						}
					}
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) { // GB
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							colors[4 * by + bx] |= reader.Read8();
						}
					}
					for (var by = 0; by < 4 && y + by < tex.mHeight; by++) {
						for (var bx = 0; bx < 4 && x + bx < tex.mWidth; bx++) {
							tex.mData[tex.mWidth * (y + by) + (x + bx)] = aRGBA.FromARGB8(colors[4 * by + bx]);
						}
					}
				}
			}
		}
		public static void loadTextureCMP(demoTexture tex, aBinaryReader reader) {
			for (var y = 0; y < tex.mHeight; y += 8) { // image
				for (var x = 0; x < tex.mWidth; x += 8) {
					for (var by = 0; by < 8; by += 4) { // block
						for (var bx = 0; bx < 8; bx += 4) {
							var colors = aRGBA.FromST3C1(reader.Read64());
							for (var ty = 0; ty < 4 && y + by + ty < tex.mHeight; ty++) { // tile
								for (var tx = 0; tx < 4 && x + bx + tx < tex.mWidth; tx++) {
									tex.mData[tex.mWidth * (y + by + ty) + (x + bx + tx)] = colors[ty * 4 + tx];
								}
							}
						}
					}
				}
			}
		}
	}

	enum demoTexFormat {
		I4 = 0,
		I8 = 1,
		IA4 = 2,
		IA8 = 3,
		RGB565 = 4,
		RGB5A3 = 5,
		RGBA8 = 6,
		CI4 = 8,
		CI8 = 9,
		CI14X2 = 10,
		CMP = 14,
	}

	enum demoWrapMode {
		Clamp,
		Repeat,
		Mirror,
	}
}
