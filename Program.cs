using System;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace Arookas.Demolisher
{
	static class Program
	{
		public static GLProgram EmbossProgram { get; private set; }
		public static GLShader EmbossFragmentShader { get; private set; }
		public static GLShader EmbossVertexShader { get; private set; }

		public static readonly Version Version = new Version(0, 3, 4);

		[STAThread]
		static void Main(string[] arguments)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			using (DemolisherForm demolisherForm = new DemolisherForm())
			{
#if !DEBUG
				try
				{
#endif
					CommandLine commandLine = new CommandLine(arguments, '+');

					// TODO: +export parameter
					foreach (var parameter in commandLine)
					{
						switch (parameter.Name.ToLowerInvariant())
						{
							case "+load": LoadModel(demolisherForm, parameter); break;
						}
					}

					Application.Run(demolisherForm);
#if !DEBUG
				}
				catch (Exception exception)
				{
					MessageBox.Show(String.Format("{0}\n\n{1}", exception.Message, exception.StackTrace), exception.GetType().Name, MessageBoxButtons.OK);
				}
#endif
			}
		}

		static void LoadModel(DemolisherForm form, CommandLineParameter parameter)
		{
			Vector3 t = Vector3.Zero, r = Vector3.Zero, s = Vector3.One;

			if (parameter.Count >= 4)
			{
				t = new Vector3(Single.Parse(parameter[1]), Single.Parse(parameter[2]), Single.Parse(parameter[3]));
			}
			if (parameter.Count >= 7)
			{
				r = new Vector3(Single.Parse(parameter[4]), Single.Parse(parameter[5]), Single.Parse(parameter[6]));
			}
			if (parameter.Count >= 10)
			{
				s = new Vector3(Single.Parse(parameter[7]), Single.Parse(parameter[8]), Single.Parse(parameter[9]));
			}

			form.LoadModel(parameter[0], t, r, s);

		}

		public static void LoadShaders()
		{
			try
			{
				EmbossVertexShader = GLShader.FromFile(ShaderType.VertexShader, "emboss.vp");
				EmbossFragmentShader = GLShader.FromFile(ShaderType.FragmentShader, "emboss.fp");
				EmbossProgram = GLProgram.Create();
				EmbossProgram.Attach(EmbossVertexShader);
				EmbossProgram.Attach(EmbossFragmentShader);
				EmbossProgram.Link();
			}
			catch (Exception exception)
			{
				MessageBox.Show(
					"A problem occured while loading the emboss shader program. Make sure the emboss.vp and emboss.fp files are in the directory of the executable and do not contain syntax errors.\n\n" + exception.ToString(),
					"Failed to load emboss shader",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}
		}
	}
}