using Arookas.Math;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Arookas.Demolisher
{
	internal static class GLUtility
	{
		public static void EnableTexture2D()
		{
			for (int texUnit = 0; texUnit < 8; texUnit++)
			{
				GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
				GL.Enable(EnableCap.Texture2D);
			}
		}
		public static void DisableTexture2D()
		{
			for (int texUnit = 0; texUnit < 8; texUnit++)
			{
				GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
				GL.Disable(EnableCap.Texture2D);
			}
		}

		public static Vector3 ToVector3(this Vector3D vector3D)
		{
			return new Vector3(vector3D.X, vector3D.Y, vector3D.Z);
		}
		public static Vector3D ToVector3D(this Vector3 vector3)
		{
			return new Vector3D(vector3.X, vector3.Y, vector3.Z);
		}

		public static void WireCube(BoundingBox boundingBox)
		{
			WireCube(boundingBox.Min, boundingBox.Max);
		}
		public static void WireCube(Vector3 min, Vector3 max)
		{
			GL.Begin(BeginMode.Lines);

			// bottom
			GL.Vertex3(min.X, min.Y, min.Z);
			GL.Vertex3(max.X, min.Y, min.Z);

			GL.Vertex3(max.X, min.Y, min.Z);
			GL.Vertex3(max.X, min.Y, max.Z);

			GL.Vertex3(max.X, min.Y, max.Z);
			GL.Vertex3(min.X, min.Y, max.Z);

			GL.Vertex3(min.X, min.Y, max.Z);
			GL.Vertex3(min.X, min.Y, min.Z);

			// top
			GL.Vertex3(min.X, max.Y, min.Z);
			GL.Vertex3(max.X, max.Y, min.Z);

			GL.Vertex3(max.X, max.Y, min.Z);
			GL.Vertex3(max.X, max.Y, max.Z);

			GL.Vertex3(max.X, max.Y, max.Z);
			GL.Vertex3(min.X, max.Y, max.Z);

			GL.Vertex3(min.X, max.Y, max.Z);
			GL.Vertex3(min.X, max.Y, min.Z);
			
			// sides
			GL.Vertex3(min.X, min.Y, min.Z);
			GL.Vertex3(min.X, max.Y, min.Z);

			GL.Vertex3(max.X, min.Y, min.Z);
			GL.Vertex3(max.X, max.Y, min.Z);

			GL.Vertex3(max.X, min.Y, max.Z);
			GL.Vertex3(max.X, max.Y, max.Z);

			GL.Vertex3(min.X, min.Y, max.Z);
			GL.Vertex3(min.X, max.Y, max.Z);

			GL.End();
		}

		public static void SetTexture2DWrapModeS(WrapMode wrapMode)
		{
			switch (wrapMode)
			{
				case WrapMode.Clamp:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
					break;
				case WrapMode.Repeat:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
					break;
				case WrapMode.Mirror:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.MirroredRepeat);
					break;
			}
		}
		public static void SetTexture2DWrapModeT(WrapMode wrapMode)
		{
			switch (wrapMode)
			{
				case WrapMode.Clamp:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
					break;
				case WrapMode.Repeat:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
					break;
				case WrapMode.Mirror:
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.MirroredRepeat);
					break;
			}
		}

		public static void SetTexture2D(int texUnit, int texture2D)
		{
			GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, texture2D);
		}
		public static void SetTexture2D(int texUnit, int texture2D, WrapMode wrapS, WrapMode wrapT)
		{
			SetTexture2D(texUnit, texture2D);
			SetTexture2DWrapModeS(wrapS);
			SetTexture2DWrapModeT(wrapT);
		}

		public static Color4 ToColor4(this Color color)
		{
			return new Color4((byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
		}

		public static Vector3 TransformPoint(this Matrix4 matrix, Vector3 point)
		{
			Vector3 transformedPoint = new Vector3();

			transformedPoint.X =	point.X * matrix.M11 +
									point.Y * matrix.M12 +
									point.Z * matrix.M13 + matrix.M14;

			transformedPoint.Y =	point.X * matrix.M21 +
									point.Y * matrix.M22 +
									point.Z * matrix.M23 + matrix.M24;

			transformedPoint.Z =	point.X * matrix.M31 +
									point.Y * matrix.M32 +
									point.Z * matrix.M33 + matrix.M34;

			return transformedPoint;
		}
	}
}