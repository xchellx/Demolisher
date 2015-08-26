using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace Arookas.Demolisher
{
	public class GLShader : IDisposable
	{
		bool isDisposed;

		public int ID { get; private set; }
		public string Source { get; private set; }
		public ShaderType Type { get; private set; }
		public string InfoLog { get { return GL.GetShaderInfoLog(ID); } }

		public int this[ShaderParameter parameter]
		{
			get
			{
				if (!parameter.IsDefined())
				{
					throw new ArgumentOutOfRangeException("parameter", parameter, "The specified shader parameter was not a defined OpenTK.Graphics.OpenGL.ShaderParameter value.");
				}
				
				int value;
				GL.GetShader(ID, parameter, out value);
				return value;
			}
		}

		GLShader(int id, ShaderType type, string source)
		{
			ID = id;
			Type = type;
			Source = source;
		}

		public void Dispose()
		{
			if (!isDisposed)
			{
				int deleteStatus;
				GL.DeleteShader(ID);
				GL.GetShader(ID, ShaderParameter.DeleteStatus, out deleteStatus);

				if (deleteStatus != 1)
				{
					throw new InvalidOperationException("The GLShader failed to be deleted.");
				}

				isDisposed = true;
			}
		}

		public static GLShader FromFile(ShaderType shaderType, string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}

			return FromSource(shaderType, File.ReadAllText(path));
		}
		public static GLShader FromSource(ShaderType shaderType, string source)
		{
			if (!shaderType.IsDefined())
			{
				throw new ArgumentOutOfRangeException("shaderType", shaderType, "The specified shader type was not a defined OpenTK.Graphics.OpenGL.ShaderType value.");
			}

			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			int id = GL.CreateShader(shaderType);
			GL.ShaderSource(id, source);
			GL.CompileShader(id);

			int compileResult;
			GL.GetShader(id, ShaderParameter.CompileStatus, out compileResult);

			if (compileResult != 1)
			{
				string infoLog = GL.GetShaderInfoLog(id);
				GL.DeleteShader(id);
				throw new ArgumentException(String.Format("The GLSL shader of type {0} failed to compile. The info log is:\n{1}", shaderType, infoLog), "source");
			}

			return new GLShader(id, shaderType, source);
		}
		public static GLShader FromSource(ShaderType shaderType, string[] source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			return FromSource(shaderType, String.Concat(source));
		}

		public static implicit operator int(GLShader shader)
		{
			return shader.ID;
		}
	}
}