using Arookas.Collections;
using Arookas.Math;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AQuaternion = Arookas.Math.Quaternion;

namespace Arookas.Demolisher
{
	internal partial class DemolisherForm : Form
	{
		// state
		Matrix4 CameraMatrix
		{
			get
			{
				return Matrix4.LookAt(camera.pos.X, camera.pos.Y, camera.pos.Z, camera.pos.X, camera.pos.Y, camera.pos.Z + 100.0f, 0.0f, 1.0f, 0.0f);
			}
		}
		Viewpoint camera = new Viewpoint() { pos = Vector3D.UnitZ + new Vector3D(0.0f, 0.75f, 0.0f), rot = new Vector3D(30.0f, 180.0f, 0.0f) };
		ADictionary<Keys, bool> input = new ADictionary<Keys, bool>();
		List<Bin> loadedModels = new List<Bin>();
		bool glLoaded = false;
		Timer timer;

		// settings
		bool alphaTest = true;
		bool showGrid = true;
		bool invertLook = true;
		bool showBoundingBoxes = false;
		bool showCeilings = true;
		bool showFourthWalls = true;
		bool showNBT = false;
		PolygonMode polygonMode = PolygonMode.Fill;

		// util
		bool HasModelsLoaded
		{
			get
			{
				return (loadedModels.Count > 0);
			}
		}
		string TitleSuffix
		{
			get
			{
				return String.Format("Demolisher v{0} arookas", Program.Version);
			}
		}

		public DemolisherForm()
		{
			InitializeComponent();
			SetTitle();

			timer = new Timer()
			{
				Interval = 28,
			};

			timer.Tick += timer_Tick;
			timer.Start();
		}

		public void LoadModel(string fileName)
		{
			LoadModel(fileName, Vector3.Zero, Vector3.Zero, Vector3.One);
		}
		public void LoadModel(string fileName, Vector3 pos, Vector3 rot, Vector3 scale)
		{
			if (fileName.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase))
			{
				Bin bin = new Bin(fileName, pos, rot, scale);
				TreeNode binNode = new TreeNode()
				{
					Checked = true,
					Text = bin.Name,
					Tag = bin,
				};

				LoadBINNode(binNode.Nodes, bin, 0);

				tvw_models.BeginUpdate();
				tvw_models.Nodes.Add(binNode);
				tvw_models.EndUpdate();
				gl_frame.Invalidate();

				loadedModels.Add(bin);
			}

			SetTitle();
		}
		public void CloseModel(Bin bin)
		{
			bin.Dispose();
			loadedModels.Remove(bin);
			tvw_models.Nodes.Remove(tvw_models.Nodes.Cast<TreeNode>().First(node => node.Tag == bin));
			if (tvw_models.Nodes.Count == 0)
			{
				tvw_models_AfterSelect(null, new TreeViewEventArgs(null));
			}
			gl_frame.Invalidate();
			SetTitle();
		}
		public void CloseModels()
		{
			if (HasModelsLoaded)
			{
				loadedModels.ForEach(model => model.Dispose());
				loadedModels.Clear();
				SetTitle();
				Invalidate();
			}
		}

		void LoadBINNode(TreeNodeCollection nodes, Bin bin, int index)
		{
			for (int childIndex = bin[index].ChildIndex; childIndex >= 0; childIndex = bin[childIndex].NextIndex)
			{
				TreeNode node = new TreeNode()
				{
					Checked = true,
					Text = String.Format("Object {0}", childIndex),
					Tag = bin[childIndex],
				};
				if (bin[childIndex].ChildIndex >= 0)
				{
					LoadBINNode(node.Nodes, bin, bin[childIndex].ChildIndex);
				}
				nodes.Add(node);
			}
		}
		public void SetTitle()
		{
			Text = TitleSuffix;
		}

		void InitViewport()
		{
			const float nearClip = 0.01f;
			const float farClip = 1000.0f;

			GL.Viewport(gl_frame.ClientRectangle.Size);
			GL.MatrixMode(MatrixMode.Projection);
			Matrix4 perspectiveMatrix = Matrix4.CreatePerspectiveFieldOfView(60.0f * (float)MathUtility.DegreesToRadians, (float)gl_frame.ClientRectangle.Width / gl_frame.ClientRectangle.Height, nearClip, farClip);
			GL.LoadMatrix(ref perspectiveMatrix);
		}
		void Render()
		{
			// Refresh.
			GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			// Enable crap.
			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.LineSmooth);
			GLUtility.EnableTexture2D();

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.FrontFace(FrontFaceDirection.Cw);
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			GL.LineWidth(1.0f);
			GL.PolygonMode(MaterialFace.Front, polygonMode);
			GL.ShadeModel(ShadingModel.Smooth);

			// Set the viewpoint.
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			Matrix4 cameraMatrix = CameraMatrix;
			GL.Rotate(camera.rot.Z, Vector3.UnitZ);
			GL.Rotate(camera.rot.X, Vector3.UnitX);
			GL.Rotate(camera.rot.Y, Vector3.UnitY);
			GL.Scale(1.0f, 1.0f, 1.0f);
			GL.MultMatrix(ref cameraMatrix);

			// Set up lighting.
			GL.Enable(EnableCap.Lighting);
			GL.Light(LightName.Light0, LightParameter.Ambient, new Color(127).ToColor4());
			GL.Light(LightName.Light0, LightParameter.Position, new Vector4(camera.pos.ToVector3(), 1.0f));
			GL.Enable(EnableCap.Light0);

			// Render model (opaque first, then translucent).
			if (HasModelsLoaded)
			{
				loadedModels.ForEach(model => model.Render(RenderFlags.Opaque | (showBoundingBoxes ? RenderFlags.BoundingBox : RenderFlags.None) | (showCeilings ? RenderFlags.Ceilings : RenderFlags.None) | (showFourthWalls ? RenderFlags.FourthWalls : RenderFlags.None) | (showNBT ? RenderFlags.NBT : RenderFlags.None)));
				loadedModels.ForEach(model => model.Render(RenderFlags.Translucent | (showBoundingBoxes ? RenderFlags.BoundingBox : RenderFlags.None) | (showCeilings ? RenderFlags.Ceilings : RenderFlags.None) | (showFourthWalls ? RenderFlags.FourthWalls : RenderFlags.None) | (showNBT ? RenderFlags.NBT : RenderFlags.None)));
			}

			// Render grid.
			if (showGrid)
			{
				RenderGrid(20);
			}

			// Swap the front and back buffers.
			gl_frame.SwapBuffers();
		}
		void RenderGrid(int gridSize)
		{
			GL.Disable(EnableCap.Lighting);
			GLUtility.DisableTexture2D();
			GL.Begin(BeginMode.Lines);

			for (int z = -gridSize; z <= gridSize; z++)
			{
				GL.Color3(Color.White);
				GL.Vertex3(-gridSize, 0.0f, z);
				GL.Vertex3(gridSize, 0.0f, z);
			}

			for (int x = -gridSize; x <= gridSize; x++)
			{
				GL.Color3(Color.White);
				GL.Vertex3(x, 0.0f, -gridSize);
				GL.Vertex3(x, 0.0f, gridSize);
			}

			GL.End();
			GLUtility.EnableTexture2D();
			GL.Enable(EnableCap.Lighting);
		}

		// timer
		void timer_Tick(object sender, EventArgs e)
		{
			if (glLoaded)
			{
				const float moveSpeed = 0.05f;
				const float runSpeed = 0.5f;
				const float rotSpeed = 5.0f;

				bool invalidate = false;

				// Movement.
				Vector3D pos = new Vector3D();

				if (input[Keys.E])
				{
					pos += Vector3D.UnitZ; // forward
					invalidate = true;
				}

				if (input[Keys.D])
				{
					pos -= Vector3D.UnitZ; // backward
					invalidate = true;
				}

				if (input[Keys.S])
				{
					pos += Vector3D.UnitX; // left
					invalidate = true;
				}

				if (input[Keys.F])
				{
					pos -= Vector3D.UnitX; // right
					invalidate = true;
				}

				camera.pos += AQuaternion.FromEulerAngles(camera.rot * new Vector3D(1.0f, -1.0f, 1.0f) * (float)MathUtility.DegreesToRadians) * pos * (input[Keys.ShiftKey] ? runSpeed : moveSpeed);

				// Look.
				if (input[invertLook ? Keys.Down : Keys.Up]) // up
				{
					camera.rot -= Vector3D.UnitX * rotSpeed;
					invalidate = true;
				}

				if (input[invertLook ? Keys.Up : Keys.Down]) // down
				{
					camera.rot += Vector3D.UnitX * rotSpeed;
					invalidate = true;
				}

				if (input[Keys.Left]) // left
				{
					camera.rot -= Vector3D.UnitY * rotSpeed;
					invalidate = true;
				}

				if (input[Keys.Right]) // right
				{
					camera.rot += Vector3D.UnitY * rotSpeed;
					invalidate = true;
				}

				if (invalidate)
				{
					gl_frame.Invalidate();
				}
			}
		}

		// men_file
		void btn_open_Click(object sender, EventArgs e)

		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog()
			{
				Title = "Open BIN model",
				Filter = "BIN models (*.bin)|*.bin",
				Multiselect = true,
			})
			{
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					foreach (string model in openFileDialog.FileNames)
					{
						LoadModel(model);
					}
				}
			}
		}

		// men_view
		void btn_alphatest_CheckedChanged(object sender, EventArgs e)
		{
			alphaTest = btn_alphatest.Checked;
			gl_frame.Invalidate();
		}
		void btn_bbox_CheckedChanged(object sender, EventArgs e)
		{
			showBoundingBoxes = btn_bbox.Checked;
			gl_frame.Invalidate();
		}
		void btn_grid_CheckedChanged(object sender, EventArgs e)
		{
			showGrid = btn_grid.Checked;
			gl_frame.Invalidate();
		}
		void btn_invertLook_Click(object sender, EventArgs e)
		{
			invertLook = btn_invertLook.Checked;
			gl_frame.Invalidate();
		}
		void btn_wireframe_CheckedChanged(object sender, EventArgs e)
		{
			polygonMode = (btn_wireframe.Checked ? PolygonMode.Line : PolygonMode.Fill);
			gl_frame.Invalidate();
		}
		void btn_ceilings_CheckedChanged(object sender, EventArgs e)
		{
			showCeilings = btn_ceilings.Checked;
			gl_frame.Invalidate();
		}
		void btn_fourthwalls_CheckedChanged(object sender, EventArgs e)
		{
			showFourthWalls = btn_fourthwalls.Checked;
			gl_frame.Invalidate();
		}
		void btn_nbt_CheckedChanged(object sender, EventArgs e)
		{
			showNBT = btn_nbt.Checked;
			gl_frame.Invalidate();
		}
		
		// men_main
		void btn_import_Click(object sender, EventArgs e)
		{
			btn_open_Click(sender, e);
		}
		void btn_unload_Click(object sender, EventArgs e)
		{
			CloseModel(tvw_models.SelectedNode.Tag as Bin);
		}

		// tvw_models
		void tvw_models_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is IRenderable)
			{
				IRenderable renderable = (e.Node.Tag as IRenderable);
				renderable.Visible = !renderable.Visible;
			}

			gl_frame.Invalidate();
		}

		// gl_frame
		void gl_frame_DragDrop(object sender, DragEventArgs e)
		{
			foreach (string model in (e.Data.GetData(DataFormats.FileDrop) as string[]))
			{
				LoadModel(model);
			}
		}
		void gl_frame_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}
		void gl_frame_KeyDown(object sender, KeyEventArgs e)
		{
			input[e.KeyCode] = true;
		}
		void gl_frame_KeyUp(object sender, KeyEventArgs e)
		{
			input[e.KeyCode] = false;
		}
		void gl_frame_Load(object sender, EventArgs e)
		{
			glLoaded = true;
			Program.LoadShaders();
		}
		void gl_frame_Paint(object sender, PaintEventArgs e)
		{
			Render();
		}
		void gl_frame_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			switch (e.KeyCode)
			{
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
		void gl_frame_Resize(object sender, EventArgs e)
		{
			InitViewport();
			gl_frame.Invalidate();
		}

		private void tvw_models_AfterSelect(object sender, TreeViewEventArgs e)
		{
			btn_unload.Enabled = !(e.Node == null || !(e.Node.Tag is Bin));
		}
	}
}