using Arookas.Collections;
using Arookas.IO.Binary;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Linq;

namespace Arookas.Demolisher
{
	class Texture
	{
		Color[] data;

		public TextureFormat Format { get; private set; }
		public int Height { get; private set; }
		public byte Unk1 { get; private set; }
		public int Width { get; private set; }

		static int[] Lookup4Bit
		{
			get
			{
				if (lookup4Bit == null)
				{
					lookup4Bit = CollectionUtility.Initialize(16, index => (int)(255.0d / 15.0d * index));
				}

				return lookup4Bit;
			}
		}
		static int[] lookup4Bit;

		Texture() { } // see CreateInverse
		public Texture(ABinaryReader binaryReader)
		{
			Width = binaryReader.Read16();
			Height = binaryReader.Read16();
			Format = (TextureFormat)binaryReader.Read8();
			Unk1 = binaryReader.Read8();
			
			if (binaryReader.Read16() != 0)
			{
				throw new Exception("Texture.unk4 != 0");
			}

			uint offset = binaryReader.Read32();
			data = new Color[Width * Height];

			binaryReader.Goto(offset);

			switch (Format)
			{
				case TextureFormat.I4:
					ReadI4(binaryReader);
					break;
				case TextureFormat.I8:
					ReadI8(binaryReader);
					break;
				case TextureFormat.IA4:
					ReadIA4(binaryReader);
					break;
				case TextureFormat.IA8:
					ReadIA8(binaryReader);
					break;
				case TextureFormat.RGB565:
					ReadRGB565(binaryReader);
					break;
				case TextureFormat.RGB5A3:
					ReadRGB5A3(binaryReader);
					break;
				case TextureFormat.RGBA8:
					ReadRGB8(binaryReader);
					break;
				case TextureFormat.CMP:
					ReadCMP(binaryReader);
					break;
				default:
					throw new NotImplementedException(String.Format("Encountered an indexed texture Format {0}, which aren't implemented.", Format));
			}

			binaryReader.Back();
		}

		public Texture CreateInverse()
		{
			Texture texture = new Texture();
			texture.Width = Width;
			texture.Height = Height;
			texture.Format = Format;
			texture.Unk1 = Unk1;
			texture.data = data.Select(color => new Color(255 - color.R, 255 - color.G, 255 - color.B, color.A)).ToArray();
			return texture;
		}

		void ReadI4(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 8)
			{
				for (int x = 0; x < Width; x += 8)
				{
					for (int by = 0; by < 8 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 8 && x + bx < Width; bx += 2)
						{
							byte i4 = binaryReader.Read8();
							data[Width * (y + by) + (x + bx)] = new Color(Lookup4Bit[(i4 >> 4) & 0xF]);
							data[Width * (y + by) + (x + bx + 1)] = new Color(Lookup4Bit[i4 & 0xF]);
						}
					}
				}
			}
		}
		void ReadI8(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 4)
			{
				for (int x = 0; x < Width; x += 8)
				{
					for (int by = 0; by < 4 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 8 && x + bx < Width; bx++)
						{
							data[Width * (y + by) + (x + bx)] = new Color(binaryReader.Read8());
						}
					}
				}
			}
		}
		void ReadIA4(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 4)
			{
				for (int x = 0; x < Width; x += 4)
				{
					for (int by = 0; by < 4 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							byte ia4 = binaryReader.Read8();
							data[Width * (y + by) + (x + bx)] = new Color(Lookup4Bit[(ia4 >> 4) & 0xF], Lookup4Bit[ia4 & 0xF]);
						}
					}
				}
			}
		}
		void ReadIA8(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 4)
			{
				for (int x = 0; x < Width; x += 4)
				{
					for (int by = 0; by < 4 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							data[Width * (y + by) + (x + bx)] = new Color(binaryReader.Read8(), binaryReader.Read8());
						}
					}
				}
			}
		}
		void ReadRGB565(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 4)
			{
				for (int x = 0; x < Width; x += 4)
				{
					for (int by = 0; by < 4 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							data[Width * (y + by) + (x + bx)] = Color.FromRGB565(binaryReader.Read16());
						}
					}
				}
			}
		}
		void ReadRGB5A3(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 4)
			{
				for (int x = 0; x < Width; x += 4)
				{
					for (int by = 0; by < 4 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							ushort color_u16 = binaryReader.Read16();
							Color color;

							if ((color_u16 & 0x8000) == 0x8000)
							{
								color = Color.FromRGB5(color_u16);
							}
							else
							{
								color = Color.FromRGB4A3(color_u16);
							}

							data[Width * (y + by) + (x + bx)] = color;
						}
					}
				}
			}
		}
		void ReadRGB8(ABinaryReader binaryReader)
		{
			for (int y = 0; y < Height; y += 4)
			{
				for (int x = 0; x < Width; x += 4)
				{
					uint[] colors = new uint[16];

					for (int by = 0; by < 4 && y + by < Height; by++) // AR
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							colors[4 * by + bx] = (uint)(binaryReader.Read8() << 16);
						}
					}

					for (int by = 0; by < 4 && y + by < Height; by++) // GB
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							colors[4 * by + bx] |= binaryReader.Read8();
						}
					}

					for (int by = 0; by < 4 && y + by < Height; by++)
					{
						for (int bx = 0; bx < 4 && x + bx < Width; bx++)
						{
							data[Width * (y + by) + (x + bx)] = Color.FromARGB8(colors[4 * by + bx]);
						}
					}
				}
			}
		}
		void ReadCMP(ABinaryReader binaryReader)
		{
			// image
			for (int y = 0; y < Height; y += 8)
			{
				for (int x = 0; x < Width; x += 8)
				{
					// 2x2 block
					for (int ay = 0; ay < 8; ay += 4)
					{
						for (int ax = 0; ax < 8; ax += 4)
						{
							// 4x4 tile
							Color[] colors = Color.FromST3C1(binaryReader.Read64());

							for (int by = 0; by < 4 && y + ay + by < Height; by++)
							{
								for (int bx = 0; bx < 4 && x + ax + bx < Width; bx++)
								{
									data[Width * (y + ay + by) + (x + ax + bx)] = colors[by * 4 + bx];
								}
							}
						}
					}
				}
			}
		}

		public Bitmap ToBitmap()
		{
			Bitmap bitmap = new Bitmap(Width, Height);

			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					bitmap.SetPixel(x, y, data[Width * y + x]);
				}
			}

			return bitmap;
		}

		public int ToGLTexture()
		{
			int textureID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, textureID);
			GL.TexImage2D<Color>(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
			return textureID;
		}
		public int ToGLTexture(float intensity)
		{
			GL.PixelTransfer(PixelTransferParameter.RedScale, intensity);
			GL.PixelTransfer(PixelTransferParameter.GreenScale, intensity);
			GL.PixelTransfer(PixelTransferParameter.BlueScale, intensity);

			int glTexture = ToGLTexture();

			GL.PixelTransfer(PixelTransferParameter.RedScale, 1.0f);
			GL.PixelTransfer(PixelTransferParameter.GreenScale, 1.0f);
			GL.PixelTransfer(PixelTransferParameter.BlueScale, 1.0f);

			return glTexture;
		}
	}
}