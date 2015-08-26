using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace arookas.Demolisher
{
	public class GLProgram : IEnumerable<GLShader>, IDisposable
	{
		List<GLShader> attachedShaders = new List<GLShader>(5);
		bool isDisposed;
		
		public int ID { get; private set; }
		public string InfoLog { get { return GL.GetProgramInfoLog(ID); } }

		public GLShader this[int index] { get { return attachedShaders[index]; } }
		public GLShader this[ShaderType shaderType]
		{
			get
			{
				if (!shaderType.IsDefined())
				{
					throw new ArgumentOutOfRangeException("shaderType", shaderType, "The specified shader type was not a defined OpenTK.Graphics.OpenGL.ShaderType value.");
				}

				return attachedShaders.FirstOrDefault(shader => shader.Type == shaderType);
			}
		}
		public int this[ProgramParameter parameter]
		{
			get
			{
				if (!parameter.IsDefined())
				{
					throw new ArgumentOutOfRangeException("parameter", parameter, "The specified program parameter was not a defined OpenTK.Graphics.OpenGL.ProgramParameter value.");
				}

				int value;
				GL.GetProgram(ID, parameter, out value);
				return value;
			}
		}

		GLProgram(int id)
		{
			ID = id;
		}

		public static GLProgram Create()
		{
			return new GLProgram(GL.CreateProgram());
		}
		public void Attach(GLShader shader)
		{
			if (shader == null)
			{
				throw new ArgumentNullException("shader");
			}

			GL.AttachShader(ID, shader);
			attachedShaders.Add(shader);
		}
		public void Link()
		{
			GL.LinkProgram(ID);

			if (this[ProgramParameter.LinkStatus] != 1)
			{
				throw new InvalidOperationException(String.Format("The GLProgram failed to be linked. The info log is:\n{0}", InfoLog));
			}
		}
		public void Use()
		{
			GL.UseProgram(ID);
		}
		public static void Unuse()
		{
			GL.UseProgram(0);
		}

		public void Dispose()
		{
			if (!isDisposed)
			{
				int deleteStatus;
				GL.DeleteProgram(ID);
				GL.GetProgram(ID, ProgramParameter.DeleteStatus, out deleteStatus);

				if (deleteStatus != 1)
				{
					throw new InvalidOperationException("The GLProgram failed to be deleted.");
				}

				isDisposed = true;
			}
		}

		public IEnumerator<GLShader> GetEnumerator()
		{
			return attachedShaders.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static implicit operator int(GLProgram program)
		{
			return program.ID;
		}
	}
}