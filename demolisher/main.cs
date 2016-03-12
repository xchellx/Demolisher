using System;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace arookas {
	static class Program {
		public static glProgram sEmboss;
		public static glShader sEmbossFragment, sEmbossVertex;

		public static readonly Version sVersion = new Version(0, 3, 1);

		[STAThread]
		static void Main(string[] arguments) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			using (var form = new demoForm()) {
#if !DEBUG
				try {
#endif
				aCommandLine commandLine = new aCommandLine(arguments, '+');
				foreach (var param in commandLine) {
					switch (param.Name.ToLowerInvariant()) {
						case "+load": LoadModel(form, param); break;
					}
				}
				Application.Run(form);
#if !DEBUG
				}
				catch (Exception exception) {
					MessageBox.Show(String.Format("{0}\n\n{1}", exception.Message, exception.StackTrace), exception.GetType().Name, MessageBoxButtons.OK);
				}
#endif
			}
		}

		static void LoadModel(demoForm form, aCommandLineParameter param) {
			var trans = Vector3.Zero;
			var rot = Vector3.Zero;
			var scale = Vector3.One;
			if (param.Count >= 4) {
				trans = new Vector3(Single.Parse(param[1]), Single.Parse(param[2]), Single.Parse(param[3]));
			}
			if (param.Count >= 7) {
				rot = new Vector3(Single.Parse(param[4]), Single.Parse(param[5]), Single.Parse(param[6]));
			}
			if (param.Count >= 10) {
				scale = new Vector3(Single.Parse(param[7]), Single.Parse(param[8]), Single.Parse(param[9]));
			}
			form.loadModel(param[0], trans, rot, scale);

		}

		public static void LoadShaders() {
#if !DEBUG
			try {
#endif
			sEmbossVertex = glShader.fromFile(ShaderType.VertexShader, "emboss.vp");
			sEmbossFragment = glShader.fromFile(ShaderType.FragmentShader, "emboss.fp");
			sEmboss = glProgram.create();
			sEmboss.attach(sEmbossVertex);
			sEmboss.attach(sEmbossFragment);
			sEmboss.link();
#if !DEBUG
			}
			catch {
				MessageBox.Show(
					"A problem occured while loading the emboss shader program. Make sure the emboss.vp and emboss.fp files are in the directory of the executable and do not contain syntax errors.",
					"Failed to load emboss shader",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				return;
			}
#endif
		}
	}

	interface IRenderable {
		bool Visible { get; set; }
	}

	struct demoBoundingBox {
		Vector3 mMin, mMax;

		public Vector3 Min {
			get { return mMin; }
		}
		public Vector3 Max {
			get { return mMax; }
		}

		public demoBoundingBox(Vector3 min, Vector3 max) {
			mMin = min;
			mMax = max;
		}

		public static demoBoundingBox operator *(demoBoundingBox bbox, float scalar) {
			return new demoBoundingBox(bbox.Min * scalar, bbox.Max * scalar);
		}
	}
}
