using arookas.Math;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace arookas {
	public class glProgram : IEnumerable<glShader>, IDisposable {
		int mId;
		List<glShader> mShaders;
		bool mDisposed;

		public glShader this[int index] {
			get { return mShaders[index]; }
		}
		public glShader this[ShaderType type] {
			get {
				return mShaders.FirstOrDefault(shader => shader.Type == type);
			}
		}
		public int this[ProgramParameter parameter] {
			get {
				int value;
				GL.GetProgram(mId, parameter, out value);
				return value;
			}
		}

		glProgram(int id) {
			mShaders = new List<glShader>(5);
			mId = id;
		}

		public void attach(glShader shader) {
			if (shader == null) {
				throw new ArgumentNullException("shader");
			}
			GL.AttachShader(mId, shader);
			mShaders.Add(shader);
		}
		public void link() {
			GL.LinkProgram(mId);
			if (this[ProgramParameter.LinkStatus] != 1) {
				throw new InvalidOperationException(String.Format("The GLProgram failed to be linked. The info log is:\n{0}", getInfoLog()));
			}
		}
		public void use() {
			GL.UseProgram(mId);
		}

		public string getInfoLog() {
			return GL.GetProgramInfoLog(mId); 
		}

		public void Dispose() {
			if (!mDisposed) {
				int status;
				GL.DeleteProgram(mId);
				GL.GetProgram(mId, ProgramParameter.DeleteStatus, out status);
				if (status != 1) {
					throw new InvalidOperationException("The GL program failed to be deleted.");
				}
				mDisposed = true;
			}
		}

		public IEnumerator<glShader> GetEnumerator() {
			return mShaders.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		public static glProgram create() {
			return new glProgram(GL.CreateProgram());
		}
		public static void unuse() {
			GL.UseProgram(0);
		}
		
		public static implicit operator int(glProgram prog) {
			return prog.mId;
		}
	}

	public class glShader : IDisposable {
		int mId;
		ShaderType mType;
		string mSource;
		bool mDisposed;

		public ShaderType Type {
			get { return mType; }
		}

		public int this[ShaderParameter param] {
			get {
				int value;
				GL.GetShader(mId, param, out value);
				return value;
			}
		}

		glShader(int id, ShaderType type, string source) {
			mId = id;
			mType = type;
			mSource = source;
		}

		public string getInfoLog() {
			return GL.GetShaderInfoLog(mId);
		}

		public void Dispose() {
			if (!mDisposed) {
				int deleteStatus;
				GL.DeleteShader(mId);
				GL.GetShader(mId, ShaderParameter.DeleteStatus, out deleteStatus);

				if (deleteStatus != 1) {
					throw new InvalidOperationException("The GL shader failed to be deleted.");
				}

				mDisposed = true;
			}
		}

		public static glShader fromFile(ShaderType type, string path) {
			if (path == null) {
				throw new ArgumentNullException("path");
			}
			return fromSource(type, File.ReadAllText(path));
		}
		public static glShader fromSource(ShaderType type, string source) {
			if (!type.IsDefined()) {
				throw new ArgumentOutOfRangeException("type", type, "The specified shader type was not a defined OpenTK.Graphics.OpenGL.ShaderType value.");
			}

			if (source == null) {
				throw new ArgumentNullException("source");
			}

			var id = GL.CreateShader(type);
			GL.ShaderSource(id, source);
			GL.CompileShader(id);

			int result;
			GL.GetShader(id, ShaderParameter.CompileStatus, out result);

			if (result != 1) {
				var log = GL.GetShaderInfoLog(id);
				GL.DeleteShader(id);
				throw new ArgumentException(String.Format("The GLSL shader of type {0} failed to compile. The info log is:\n{1}", type, log), "source");
			}

			return new glShader(id, type, source);
		}
		public static glShader fromSource(ShaderType type, string[] source) {
			if (source == null) {
				throw new ArgumentNullException("source");
			}
			return fromSource(type, String.Concat(source));
		}

		public static implicit operator int(glShader shader) {
			return shader.mId;
		}
	}

	static class gl {
		public static void enableTexture2D() {
			for (int texUnit = 0; texUnit < 8; texUnit++) {
				GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
				GL.Enable(EnableCap.Texture2D);
			}
		}
		public static void disableTexture2D() {
			for (int texUnit = 0; texUnit < 8; texUnit++) {
				GL.ActiveTexture(TextureUnit.Texture0 + texUnit);
				GL.Disable(EnableCap.Texture2D);
			}
		}

		public static void drawWireCube(demoBoundingBox boundingBox) {
			drawWireCube(boundingBox.Min, boundingBox.Max);
		}
		public static void drawWireCube(Vector3 min, Vector3 max) {
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

		public static void setWrapModeS(demoWrapMode mode) {
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)convert(mode));
		}
		public static void setWrapModeT(demoWrapMode mode) {
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)convert(mode));
		}

		public static void setTexture2D(int unit, int tex) {
			GL.ActiveTexture(TextureUnit.Texture0 + unit);
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, tex);
		}
		public static void setTexture2D(int unit, int tex, demoWrapMode wrapS, demoWrapMode wrapT) {
			setTexture2D(unit, tex);
			setWrapModeS(wrapS);
			setWrapModeT(wrapT);
		}

		public static Vector3 TransformPoint(this Matrix4 matrix, Vector3 point) {
			Vector3 transformedPoint = new Vector3();

			transformedPoint.X = point.X * matrix.M11 +
									point.Y * matrix.M12 +
									point.Z * matrix.M13 + matrix.M14;

			transformedPoint.Y = point.X * matrix.M21 +
									point.Y * matrix.M22 +
									point.Z * matrix.M23 + matrix.M24;

			transformedPoint.Z = point.X * matrix.M31 +
									point.Y * matrix.M32 +
									point.Z * matrix.M33 + matrix.M34;

			return transformedPoint;
		}

		// gl <-> alib interop
		public static Vector3 convert(this aVec3 vec) {
			return new Vector3(vec.x, vec.y, vec.z);
		}
		public static aVec3 convert(this Vector3 vec) {
			return new aVec3(vec.X, vec.Y, vec.Z);
		}
		public static Color4 convert(this aRGBA color) {
			return new Color4(color.r, color.g, color.b, color.a);
		}

		public static demoWrapMode convert(TextureWrapMode mode) {
			switch (mode) {
				case TextureWrapMode.ClampToEdge: return demoWrapMode.Clamp;
				case TextureWrapMode.Repeat: return demoWrapMode.Repeat;
				case TextureWrapMode.MirroredRepeat: return demoWrapMode.Mirror;
			}
			throw new Exception("Unknown wrap mode.");
		}
		public static TextureWrapMode convert(demoWrapMode mode) {
			switch (mode) {
				case demoWrapMode.Clamp: return TextureWrapMode.ClampToEdge;
				case demoWrapMode.Repeat: return TextureWrapMode.Repeat;
				case demoWrapMode.Mirror: return TextureWrapMode.MirroredRepeat;
			}
			throw new Exception("Unknown wrap mode.");
		}
	}
}
