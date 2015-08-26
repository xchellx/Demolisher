namespace arookas.Demolisher
{
	partial class DemolisherForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gl_frame = new OpenTK.GLControl();
			this.men_main = new System.Windows.Forms.MenuStrip();
			this.men_file = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_open = new System.Windows.Forms.ToolStripMenuItem();
			this.men_view = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_alphatest = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_bbox = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_grid = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_invertLook = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_wireframe = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btn_ceilings = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_fourthwalls = new System.Windows.Forms.ToolStripMenuItem();
			this.btn_nbt = new System.Windows.Forms.ToolStripMenuItem();
			this.pan_controls = new System.Windows.Forms.Panel();
			this.tvw_models = new System.Windows.Forms.TreeView();
			this.str_main = new System.Windows.Forms.ToolStrip();
			this.btn_import = new System.Windows.Forms.ToolStripButton();
			this.btn_unload = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.men_main.SuspendLayout();
			this.pan_controls.SuspendLayout();
			this.str_main.SuspendLayout();
			this.SuspendLayout();
			// 
			// gl_frame
			// 
			this.gl_frame.AllowDrop = true;
			this.gl_frame.BackColor = System.Drawing.Color.Black;
			this.gl_frame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gl_frame.Location = new System.Drawing.Point(0, 24);
			this.gl_frame.Name = "gl_frame";
			this.gl_frame.Size = new System.Drawing.Size(640, 480);
			this.gl_frame.TabIndex = 0;
			this.gl_frame.VSync = true;
			this.gl_frame.Load += new System.EventHandler(this.gl_frame_Load);
			this.gl_frame.DragDrop += new System.Windows.Forms.DragEventHandler(this.gl_frame_DragDrop);
			this.gl_frame.DragEnter += new System.Windows.Forms.DragEventHandler(this.gl_frame_DragEnter);
			this.gl_frame.Paint += new System.Windows.Forms.PaintEventHandler(this.gl_frame_Paint);
			this.gl_frame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gl_frame_KeyDown);
			this.gl_frame.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gl_frame_KeyUp);
			this.gl_frame.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gl_frame_PreviewKeyDown);
			this.gl_frame.Resize += new System.EventHandler(this.gl_frame_Resize);
			// 
			// men_main
			// 
			this.men_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.men_file,
            this.men_view});
			this.men_main.Location = new System.Drawing.Point(0, 0);
			this.men_main.Name = "men_main";
			this.men_main.Size = new System.Drawing.Size(880, 24);
			this.men_main.TabIndex = 1;
			this.men_main.Text = "menuStrip1";
			// 
			// men_file
			// 
			this.men_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_open});
			this.men_file.Name = "men_file";
			this.men_file.Size = new System.Drawing.Size(37, 20);
			this.men_file.Text = "File";
			// 
			// btn_open
			// 
			this.btn_open.Name = "btn_open";
			this.btn_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.btn_open.Size = new System.Drawing.Size(155, 22);
			this.btn_open.Text = "Open...";
			this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
			// 
			// men_view
			// 
			this.men_view.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_invertLook,
            this.toolStripSeparator2,
            this.btn_alphatest,
            this.btn_bbox,
            this.btn_grid,
            this.btn_nbt,
            this.btn_wireframe,
            this.toolStripSeparator1,
            this.btn_ceilings,
            this.btn_fourthwalls});
			this.men_view.Name = "men_view";
			this.men_view.Size = new System.Drawing.Size(44, 20);
			this.men_view.Text = "View";
			// 
			// btn_alphatest
			// 
			this.btn_alphatest.Checked = true;
			this.btn_alphatest.CheckOnClick = true;
			this.btn_alphatest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btn_alphatest.Name = "btn_alphatest";
			this.btn_alphatest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.btn_alphatest.Size = new System.Drawing.Size(200, 22);
			this.btn_alphatest.Text = "Alpha Test";
			this.btn_alphatest.ToolTipText = "If checked, textures with alpha will be rendered using AlphaTest; otherwise, they" +
    " will be rendered with Blend.";
			this.btn_alphatest.CheckedChanged += new System.EventHandler(this.btn_alphatest_CheckedChanged);
			// 
			// btn_bbox
			// 
			this.btn_bbox.CheckOnClick = true;
			this.btn_bbox.Name = "btn_bbox";
			this.btn_bbox.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.btn_bbox.Size = new System.Drawing.Size(200, 22);
			this.btn_bbox.Text = "Bounding Boxes";
			this.btn_bbox.ToolTipText = "If checked, the graph objects will be rendered  with their bounding boxes around " +
    "them as yellow wireframes.";
			this.btn_bbox.CheckedChanged += new System.EventHandler(this.btn_bbox_CheckedChanged);
			// 
			// btn_grid
			// 
			this.btn_grid.Checked = true;
			this.btn_grid.CheckOnClick = true;
			this.btn_grid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btn_grid.Name = "btn_grid";
			this.btn_grid.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.btn_grid.Size = new System.Drawing.Size(200, 22);
			this.btn_grid.Text = "Grid";
			this.btn_grid.ToolTipText = "If checked, the XZ grid will be rendered.";
			this.btn_grid.CheckedChanged += new System.EventHandler(this.btn_grid_CheckedChanged);
			// 
			// btn_invertLook
			// 
			this.btn_invertLook.Checked = true;
			this.btn_invertLook.CheckOnClick = true;
			this.btn_invertLook.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btn_invertLook.Name = "btn_invertLook";
			this.btn_invertLook.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.btn_invertLook.Size = new System.Drawing.Size(200, 22);
			this.btn_invertLook.Text = "Invert Pitch";
			this.btn_invertLook.ToolTipText = "If checked, the up arrow key will look down and the down arrow key will look up.";
			this.btn_invertLook.Click += new System.EventHandler(this.btn_invertLook_Click);
			// 
			// btn_wireframe
			// 
			this.btn_wireframe.CheckOnClick = true;
			this.btn_wireframe.Name = "btn_wireframe";
			this.btn_wireframe.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.btn_wireframe.Size = new System.Drawing.Size(200, 22);
			this.btn_wireframe.Text = "Wireframe";
			this.btn_wireframe.ToolTipText = "If checked, only the outlines of the polygons will be rendered.";
			this.btn_wireframe.CheckedChanged += new System.EventHandler(this.btn_wireframe_CheckedChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
			// 
			// btn_ceilings
			// 
			this.btn_ceilings.Checked = true;
			this.btn_ceilings.CheckOnClick = true;
			this.btn_ceilings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btn_ceilings.Name = "btn_ceilings";
			this.btn_ceilings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
			this.btn_ceilings.Size = new System.Drawing.Size(200, 22);
			this.btn_ceilings.Text = "Ceilings";
			this.btn_ceilings.ToolTipText = "If checked, graph objects with the ceiling flag will be rendered.";
			this.btn_ceilings.CheckedChanged += new System.EventHandler(this.btn_ceilings_CheckedChanged);
			// 
			// btn_fourthwalls
			// 
			this.btn_fourthwalls.Checked = true;
			this.btn_fourthwalls.CheckOnClick = true;
			this.btn_fourthwalls.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btn_fourthwalls.Name = "btn_fourthwalls";
			this.btn_fourthwalls.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
			this.btn_fourthwalls.Size = new System.Drawing.Size(200, 22);
			this.btn_fourthwalls.Text = "Fourth Walls";
			this.btn_fourthwalls.ToolTipText = "If checked, graph objects with the fourth-wall flag will be rendered.";
			this.btn_fourthwalls.CheckedChanged += new System.EventHandler(this.btn_fourthwalls_CheckedChanged);
			// 
			// btn_nbt
			// 
			this.btn_nbt.CheckOnClick = true;
			this.btn_nbt.Name = "btn_nbt";
			this.btn_nbt.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.btn_nbt.Size = new System.Drawing.Size(200, 22);
			this.btn_nbt.Text = "NBT";
			this.btn_nbt.ToolTipText = "If checked, normals (red), binormals (blue), and tangents (green) of each vertex " +
    "will be rendered.";
			this.btn_nbt.CheckedChanged += new System.EventHandler(this.btn_nbt_CheckedChanged);
			// 
			// pan_controls
			// 
			this.pan_controls.Controls.Add(this.tvw_models);
			this.pan_controls.Controls.Add(this.str_main);
			this.pan_controls.Dock = System.Windows.Forms.DockStyle.Right;
			this.pan_controls.Location = new System.Drawing.Point(640, 24);
			this.pan_controls.Name = "pan_controls";
			this.pan_controls.Size = new System.Drawing.Size(240, 480);
			this.pan_controls.TabIndex = 2;
			// 
			// tvw_models
			// 
			this.tvw_models.CheckBoxes = true;
			this.tvw_models.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvw_models.Location = new System.Drawing.Point(0, 25);
			this.tvw_models.Name = "tvw_models";
			this.tvw_models.Size = new System.Drawing.Size(240, 455);
			this.tvw_models.TabIndex = 4;
			this.tvw_models.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvw_models_AfterCheck);
			this.tvw_models.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_models_AfterSelect);
			// 
			// str_main
			// 
			this.str_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.str_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_import,
            this.btn_unload});
			this.str_main.Location = new System.Drawing.Point(0, 0);
			this.str_main.Name = "str_main";
			this.str_main.Size = new System.Drawing.Size(240, 25);
			this.str_main.TabIndex = 3;
			this.str_main.Text = "toolStrip1";
			// 
			// btn_import
			// 
			this.btn_import.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btn_import.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btn_import.Name = "btn_import";
			this.btn_import.Size = new System.Drawing.Size(37, 22);
			this.btn_import.Text = "Load";
			this.btn_import.ToolTipText = "Load a BIN model";
			this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
			// 
			// btn_unload
			// 
			this.btn_unload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btn_unload.Enabled = false;
			this.btn_unload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btn_unload.Name = "btn_unload";
			this.btn_unload.Size = new System.Drawing.Size(49, 22);
			this.btn_unload.Text = "Unload";
			this.btn_unload.ToolTipText = "Unload the select BIN model";
			this.btn_unload.Click += new System.EventHandler(this.btn_unload_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(197, 6);
			// 
			// DemolisherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(880, 504);
			this.Controls.Add(this.gl_frame);
			this.Controls.Add(this.pan_controls);
			this.Controls.Add(this.men_main);
			this.KeyPreview = true;
			this.MainMenuStrip = this.men_main;
			this.Name = "DemolisherForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Demolisher";
			this.men_main.ResumeLayout(false);
			this.men_main.PerformLayout();
			this.pan_controls.ResumeLayout(false);
			this.pan_controls.PerformLayout();
			this.str_main.ResumeLayout(false);
			this.str_main.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenTK.GLControl gl_frame;
		private System.Windows.Forms.MenuStrip men_main;
		private System.Windows.Forms.ToolStripMenuItem men_file;
		private System.Windows.Forms.ToolStripMenuItem btn_open;
		private System.Windows.Forms.ToolStripMenuItem men_view;
		private System.Windows.Forms.ToolStripMenuItem btn_wireframe;
		private System.Windows.Forms.ToolStripMenuItem btn_invertLook;
		private System.Windows.Forms.ToolStripMenuItem btn_alphatest;
		private System.Windows.Forms.ToolStripMenuItem btn_grid;
		private System.Windows.Forms.Panel pan_controls;
		private System.Windows.Forms.ToolStrip str_main;
		private System.Windows.Forms.ToolStripButton btn_import;
		private System.Windows.Forms.TreeView tvw_models;
		private System.Windows.Forms.ToolStripButton btn_unload;
		private System.Windows.Forms.ToolStripMenuItem btn_bbox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem btn_fourthwalls;
		private System.Windows.Forms.ToolStripMenuItem btn_ceilings;
		private System.Windows.Forms.ToolStripMenuItem btn_nbt;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}