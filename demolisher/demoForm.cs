using arookas.Collections;
using arookas.Math;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace arookas {
	internal partial class demoForm : Form {
		Timer mTimer;
		demoViewpoint mCam;
		Dictionary<Keys, bool> mInput;
		List<demoBinModel> mModels;
		bool mGLLoaded;

		bool mAlphaTest = true;
		bool mShowGrid = true;
		bool mInvertLook = true;
		bool mShowBounds = false;
		bool mShowCeilings = true;
		bool mShowFourthWalls = true;
		bool mShowNBT = false;
		PolygonMode mPolyMode = PolygonMode.Fill;

		const float cNearClip = 0.01f;
		const float cFarClip = 1000.0f;

		public demoForm() {
			InitializeComponent();
			init();
			updateTitle();
		}

		void init() {
			mGLLoaded = false;
			mModels = new List<demoBinModel>();
			mInput = new Dictionary<Keys, bool>();
			updateTitle();
			initCamera();
			initTimer();
		}
		void initCamera() {
			mCam = new demoViewpoint() {
				pos = aVec3.UnitZ + new aVec3(0.0f, 0.75f, 0.0f),
				rot = new aVec3(30.0f, 180.0f, 0.0f)
			};
		}
		void initViewport() {

			GL.Viewport(glFrame.ClientRectangle.Size);
			GL.MatrixMode(MatrixMode.Projection);
			Matrix4 perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(60.0f * (float)aMath.cDegToRad, (float)glFrame.ClientRectangle.Width / glFrame.ClientRectangle.Height, cNearClip, cFarClip);
			GL.LoadMatrix(ref perspectiveMatrix);
		}
		void initTimer() {
			mTimer = new Timer() {
				Interval = (int)(1000 / 35.0f),
			};
			mTimer.Tick += evTick;
			mTimer.Start();
		}

		Matrix4 calcCamMatrix() {
			var x = mCam.pos.x;
			var y = mCam.pos.y;
			var z = mCam.pos.z;
			return Matrix4.LookAt(x, y, z, x, y, z + 100.0f, 0.0f, 1.0f, 0.0f);
		}

		void loadBinNode(TreeNodeCollection nodes, demoBinModel bin, int i) {
			for (var child = bin[i].Child; child >= 0; child = bin[child].Next) {
				var node = new TreeNode() {
					Checked = true,
					Text = String.Format("Object {0}", child),
					Tag = bin[child],
				};
				if (bin[child].Child >= 0) {
					loadBinNode(node.Nodes, bin, bin[child].Child);
				}
				nodes.Add(node);
			}
		}
		void updateTitle() {
			Text = String.Format("Demolisher v{0} arookas", Program.sVersion);
		}

		bool getKeyHeld(Keys key) {
			bool value;
			if (!mInput.TryGetValue(key, out value)) {
				return false;
			}
			return value;
		}

		public void loadModel(string file) {
			loadModel(file, Vector3.Zero, Vector3.Zero, Vector3.One);
		}
		public void loadModel(string file, Vector3 pos, Vector3 rot, Vector3 scale) {
			if (file.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase)) {
				var bin = new demoBinModel(file, pos, rot, scale);
				var binNode = new TreeNode() {
					Checked = true,
					Text = bin.Name,
					Tag = bin,
				};
				loadBinNode(binNode.Nodes, bin, 0);
				tvwModels.BeginUpdate();
				tvwModels.Nodes.Add(binNode);
				tvwModels.EndUpdate();
				glFrame.Invalidate();
				mModels.Add(bin);
			}
			updateTitle();
		}
		public void closeModel(demoBinModel model) {
			model.Dispose();
			mModels.Remove(model);
			tvwModels.Nodes.Remove(tvwModels.Nodes.Cast<TreeNode>().First(node => node.Tag == model));
			glFrame.Invalidate();
			updateTitle();
		}
		public void closeModels() {
			if (mModels.Count > 0) {
				mModels.ForEach(model => model.Dispose());
				mModels.Clear();
				updateTitle();
				Invalidate();
			}
		}

		void render() {
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			// modes
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.LineSmooth);
			gl.enableTexture2D();

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.FrontFace(FrontFaceDirection.Cw);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			GL.LineWidth(1.0f);
			GL.PolygonMode(MaterialFace.Front, mPolyMode);
			GL.ShadeModel(ShadingModel.Smooth);

			// viewpoint
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			Matrix4 cameraMatrix = calcCamMatrix();
			GL.Rotate(mCam.rot.z, Vector3.UnitZ);
			GL.Rotate(mCam.rot.x, Vector3.UnitX);
			GL.Rotate(mCam.rot.y, Vector3.UnitY);
			GL.Scale(1.0f, 1.0f, 1.0f);
			GL.MultMatrix(ref cameraMatrix);

			// lighting
			GL.Enable(EnableCap.Lighting);
			GL.Light(LightName.Light0, LightParameter.Ambient, gl.convert(new aRGBA(127)));
			GL.Light(LightName.Light0, LightParameter.Position, new Vector4(gl.convert(mCam.pos), 1.0f));
			GL.Enable(EnableCap.Light0);

			var flags = demoRenderFlags.NONE;
			if (mShowBounds) {
				flags |= demoRenderFlags.BOUNDINGBOX;
			}
			if (mShowCeilings) {
				flags |= demoRenderFlags.CEILINGS;
			}
			if (mShowFourthWalls) {
				flags |= demoRenderFlags.FOURTHWALLS;
			}
			if (mShowNBT) {
				flags |= demoRenderFlags.NBT;
			}
			mModels.ForEach(model => model.render(demoRenderFlags.OPAQUE | flags));
			mModels.ForEach(model => model.render(demoRenderFlags.TRANSLUCENT | flags));

			if (mShowGrid) {
				renderGrid(20);
			}

			glFrame.SwapBuffers();
		}
		void renderGrid(int size) {
			GL.Disable(EnableCap.Lighting);
			gl.disableTexture2D();
			GL.Begin(BeginMode.Lines);
			for (var z = -size; z <= size; z++) {
				GL.Color3(aRGBA.White);
				GL.Vertex3(-size, 0.0f, z);
				GL.Vertex3(size, 0.0f, z);
			}
			for (var x = -size; x <= size; x++) {
				GL.Color3(aRGBA.White);
				GL.Vertex3(x, 0.0f, -size);
				GL.Vertex3(x, 0.0f, size);
			}
			GL.End();
			gl.enableTexture2D();
			GL.Enable(EnableCap.Lighting);
		}

		// events
		void evTick(object sender, EventArgs e) {
			if (mGLLoaded) {
				const float cMoveSpeed = 0.05f;
				const float cRunSpeed = 0.5f;
				const float cRotSpeed = 5.0f;

				var invalidate = false;
				var pos = new aVec3();
				if (getKeyHeld(Keys.E)) {
					pos += aVec3.UnitZ;
					invalidate = true;
				}
				if (getKeyHeld(Keys.D)) {
					pos -= aVec3.UnitZ;
					invalidate = true;
				}
				if (getKeyHeld(Keys.S)) {
					pos += aVec3.UnitX;
					invalidate = true;
				}
				if (getKeyHeld(Keys.F)) {
					pos -= aVec3.UnitX;
					invalidate = true;
				}

				mCam.pos += aQuat.FromEulerAngles(mCam.rot * aVec3.NegateY * (float)aMath.cDegToRad) * pos * (getKeyHeld(Keys.ShiftKey) ? cRunSpeed : cMoveSpeed);

				if (getKeyHeld(mInvertLook ? Keys.Down : Keys.Up)) {
					mCam.rot -= aVec3.UnitX * cRotSpeed;
					invalidate = true;
				}
				if (getKeyHeld(mInvertLook ? Keys.Up : Keys.Down)) {
					mCam.rot += aVec3.UnitX * cRotSpeed;
					invalidate = true;
				}
				if (getKeyHeld(Keys.Left)) {
					mCam.rot -= aVec3.UnitY * cRotSpeed;
					invalidate = true;
				}

				if (getKeyHeld(Keys.Right)) {
					mCam.rot += aVec3.UnitY * cRotSpeed;
					invalidate = true;
				}

				if (invalidate) {
					glFrame.Invalidate();
				}
			}
		}

		void evClickOpen(object sender, EventArgs e) {
			var dialog = new OpenFileDialog() {
				Title = "Open BIN model",
				Filter = "BIN models (*.bin)|*.bin",
				Multiselect = true,
			};
			using (dialog) {
				if (dialog.ShowDialog() == DialogResult.OK) {
					foreach (string model in dialog.FileNames) {
						loadModel(model);
					}
				}
			}
		}

		void evClickAlphaTest(object sender, EventArgs e) {
			mAlphaTest = btnAlphaTest.Checked;
			glFrame.Invalidate();
		}
		void evClickBounds(object sender, EventArgs e) {
			mShowBounds = btnBounds.Checked;
			glFrame.Invalidate();
		}
		void evClickGrid(object sender, EventArgs e) {
			mShowGrid = btnGrid.Checked;
			glFrame.Invalidate();
		}
		void evClickInvertLook(object sender, EventArgs e) {
			mInvertLook = btnInvertLook.Checked;
			glFrame.Invalidate();
		}
		void evClickWireframe(object sender, EventArgs e) {
			mPolyMode = (btnWireFrame.Checked ? PolygonMode.Line : PolygonMode.Fill);
			glFrame.Invalidate();
		}
		void evClickCeilings(object sender, EventArgs e) {
			mShowCeilings = btnCeilings.Checked;
			glFrame.Invalidate();
		}
		void evClickFourthWalls(object sender, EventArgs e) {
			mShowFourthWalls = btnFourthWalls.Checked;
			glFrame.Invalidate();
		}
		void evClickNBT(object sender, EventArgs e) {
			mShowNBT = btnNBT.Checked;
			glFrame.Invalidate();
		}

		void evClickImport(object sender, EventArgs e) {
			evClickOpen(sender, e);
		}
		void evClickUnload(object sender, EventArgs e) {
			closeModel(tvwModels.SelectedNode.Tag as demoBinModel);
		}

		void evToggleVisible(object sender, TreeViewEventArgs e) {
			if (e.Node.Tag is IRenderable) {
				var renderable = (e.Node.Tag as IRenderable);
				renderable.Visible = !renderable.Visible;
			}
			glFrame.Invalidate();
		}

		void evGLDragDrop(object sender, DragEventArgs e) {
			foreach (var model in (e.Data.GetData(DataFormats.FileDrop) as string[])) {
				loadModel(model);
			}
		}
		void evGLDragEnter(object sender, DragEventArgs e) {
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
			}
			else {
				e.Effect = DragDropEffects.None;
			}
		}
		void evGLKeyDown(object sender, KeyEventArgs e) {
			mInput[e.KeyCode] = true;
		}
		void evGLKeyUp(object sender, KeyEventArgs e) {
			mInput[e.KeyCode] = false;
		}
		void evGLLoad(object sender, EventArgs e) {
			mGLLoaded = true;
			Program.LoadShaders();
		}
		void evGLRender(object sender, PaintEventArgs e) {
			render();
		}
		void evGLPreviewKey(object sender, PreviewKeyDownEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
				case Keys.Shift | Keys.Up:
				case Keys.Shift | Keys.Down:
				case Keys.Shift | Keys.Left:
				case Keys.Shift | Keys.Right:
				e.IsInputKey = true;
				break;
			}
		}
		void evGLResize(object sender, EventArgs e) {
			initViewport();
			glFrame.Invalidate();
		}
	}

	struct demoViewpoint {
		public aVec3 pos;
		public aVec3 rot;
	}

	[Flags]
	enum demoRenderFlags {
		NONE = 0,
		OPAQUE = 1 << 0,
		TRANSLUCENT = 1 << 1,
		BOUNDINGBOX = 1 << 2,
		FOURTHWALLS = 1 << 3,
		CEILINGS = 1 << 4,
		NBT = 1 << 5,
	}
}
