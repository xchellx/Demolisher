﻿namespace arookas {
	partial class demoForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Windows.Forms.MenuStrip menMain;
			System.Windows.Forms.ToolStripMenuItem menFile;
			System.Windows.Forms.ToolStripMenuItem menView;
			System.Windows.Forms.ToolStrip strMain;
			this.btnOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.btnInvertLook = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnAlphaTest = new System.Windows.Forms.ToolStripMenuItem();
			this.btnBounds = new System.Windows.Forms.ToolStripMenuItem();
			this.btnGrid = new System.Windows.Forms.ToolStripMenuItem();
			this.btnNBT = new System.Windows.Forms.ToolStripMenuItem();
			this.btnWireFrame = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.btnCeilings = new System.Windows.Forms.ToolStripMenuItem();
			this.btnFourthWalls = new System.Windows.Forms.ToolStripMenuItem();
			this.panScene = new System.Windows.Forms.Panel();
			this.tvwModels = new System.Windows.Forms.TreeView();
			this.glFrame = new OpenTK.GLControl();
			this.panObjectProps = new arookas.demoObjectPropsControl();
			this.btnCloseAll = new System.Windows.Forms.ToolStripMenuItem();
			menMain = new System.Windows.Forms.MenuStrip();
			menFile = new System.Windows.Forms.ToolStripMenuItem();
			menView = new System.Windows.Forms.ToolStripMenuItem();
			strMain = new System.Windows.Forms.ToolStrip();
			menMain.SuspendLayout();
			this.panScene.SuspendLayout();
			this.SuspendLayout();
			// 
			// menMain
			// 
			menMain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			menMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            menFile,
            menView});
			menMain.Location = new System.Drawing.Point(0, 0);
			menMain.Name = "menMain";
			menMain.Size = new System.Drawing.Size(880, 24);
			menMain.TabIndex = 1;
			// 
			// menFile
			// 
			menFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnCloseAll});
			menFile.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			menFile.Name = "menFile";
			menFile.Size = new System.Drawing.Size(37, 20);
			menFile.Text = "File";
			// 
			// btnOpen
			// 
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.btnOpen.Size = new System.Drawing.Size(154, 22);
			this.btnOpen.Text = "Open...";
			this.btnOpen.Click += new System.EventHandler(this.evClickOpen);
			// 
			// menView
			// 
			menView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInvertLook,
            this.toolStripSeparator2,
            this.btnAlphaTest,
            this.btnBounds,
            this.btnGrid,
            this.btnNBT,
            this.btnWireFrame,
            this.toolStripSeparator1,
            this.btnCeilings,
            this.btnFourthWalls});
			menView.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			menView.Name = "menView";
			menView.Size = new System.Drawing.Size(44, 20);
			menView.Text = "View";
			// 
			// btnInvertLook
			// 
			this.btnInvertLook.Checked = true;
			this.btnInvertLook.CheckOnClick = true;
			this.btnInvertLook.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnInvertLook.Name = "btnInvertLook";
			this.btnInvertLook.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.btnInvertLook.Size = new System.Drawing.Size(199, 22);
			this.btnInvertLook.Text = "Invert Pitch";
			this.btnInvertLook.ToolTipText = "If checked, the up arrow key will look down and the down arrow key will look up.";
			this.btnInvertLook.Click += new System.EventHandler(this.evClickInvertLook);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
			// 
			// btnAlphaTest
			// 
			this.btnAlphaTest.Checked = true;
			this.btnAlphaTest.CheckOnClick = true;
			this.btnAlphaTest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnAlphaTest.Name = "btnAlphaTest";
			this.btnAlphaTest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.btnAlphaTest.Size = new System.Drawing.Size(199, 22);
			this.btnAlphaTest.Text = "Alpha Test";
			this.btnAlphaTest.ToolTipText = "If checked, textures with alpha will be rendered using AlphaTest; otherwise, they" +
    " will be rendered with Blend.";
			this.btnAlphaTest.CheckedChanged += new System.EventHandler(this.evClickAlphaTest);
			// 
			// btnBounds
			// 
			this.btnBounds.CheckOnClick = true;
			this.btnBounds.Name = "btnBounds";
			this.btnBounds.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.btnBounds.Size = new System.Drawing.Size(199, 22);
			this.btnBounds.Text = "Bounding Boxes";
			this.btnBounds.ToolTipText = "If checked, the graph objects will be rendered  with their bounding boxes around " +
    "them as yellow wireframes.";
			this.btnBounds.CheckedChanged += new System.EventHandler(this.evClickBounds);
			// 
			// btnGrid
			// 
			this.btnGrid.Checked = true;
			this.btnGrid.CheckOnClick = true;
			this.btnGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnGrid.Name = "btnGrid";
			this.btnGrid.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.btnGrid.Size = new System.Drawing.Size(199, 22);
			this.btnGrid.Text = "Grid";
			this.btnGrid.ToolTipText = "If checked, the XZ grid will be rendered.";
			this.btnGrid.CheckedChanged += new System.EventHandler(this.evClickGrid);
			// 
			// btnNBT
			// 
			this.btnNBT.CheckOnClick = true;
			this.btnNBT.Name = "btnNBT";
			this.btnNBT.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.btnNBT.Size = new System.Drawing.Size(199, 22);
			this.btnNBT.Text = "NBT";
			this.btnNBT.ToolTipText = "If checked, normals (red), binormals (blue), and tangents (green) of each vertex " +
    "will be rendered.";
			this.btnNBT.CheckedChanged += new System.EventHandler(this.evClickNBT);
			// 
			// btnWireFrame
			// 
			this.btnWireFrame.CheckOnClick = true;
			this.btnWireFrame.Name = "btnWireFrame";
			this.btnWireFrame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.btnWireFrame.Size = new System.Drawing.Size(199, 22);
			this.btnWireFrame.Text = "Wireframe";
			this.btnWireFrame.ToolTipText = "If checked, only the outlines of the polygons will be rendered.";
			this.btnWireFrame.CheckedChanged += new System.EventHandler(this.evClickWireframe);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
			// 
			// btnCeilings
			// 
			this.btnCeilings.Checked = true;
			this.btnCeilings.CheckOnClick = true;
			this.btnCeilings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnCeilings.Name = "btnCeilings";
			this.btnCeilings.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
			this.btnCeilings.Size = new System.Drawing.Size(199, 22);
			this.btnCeilings.Text = "Ceilings";
			this.btnCeilings.ToolTipText = "If checked, objects with the ceiling flag will be rendered.";
			this.btnCeilings.CheckedChanged += new System.EventHandler(this.evClickCeilings);
			// 
			// btnFourthWalls
			// 
			this.btnFourthWalls.Checked = true;
			this.btnFourthWalls.CheckOnClick = true;
			this.btnFourthWalls.CheckState = System.Windows.Forms.CheckState.Checked;
			this.btnFourthWalls.Name = "btnFourthWalls";
			this.btnFourthWalls.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
			this.btnFourthWalls.Size = new System.Drawing.Size(199, 22);
			this.btnFourthWalls.Text = "Fourth Walls";
			this.btnFourthWalls.ToolTipText = "If checked, objects with the fourth-wall flag will be rendered.";
			this.btnFourthWalls.CheckedChanged += new System.EventHandler(this.evClickFourthWalls);
			// 
			// strMain
			// 
			strMain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			strMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			strMain.Location = new System.Drawing.Point(0, 24);
			strMain.Name = "strMain";
			strMain.Size = new System.Drawing.Size(880, 25);
			strMain.TabIndex = 3;
			// 
			// panScene
			// 
			this.panScene.Controls.Add(this.tvwModels);
			this.panScene.Dock = System.Windows.Forms.DockStyle.Right;
			this.panScene.Location = new System.Drawing.Point(678, 49);
			this.panScene.Name = "panScene";
			this.panScene.Size = new System.Drawing.Size(202, 366);
			this.panScene.TabIndex = 2;
			// 
			// tvwModels
			// 
			this.tvwModels.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvwModels.CheckBoxes = true;
			this.tvwModels.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvwModels.Indent = 8;
			this.tvwModels.ItemHeight = 18;
			this.tvwModels.Location = new System.Drawing.Point(0, 0);
			this.tvwModels.Name = "tvwModels";
			this.tvwModels.ShowLines = false;
			this.tvwModels.ShowPlusMinus = false;
			this.tvwModels.Size = new System.Drawing.Size(202, 366);
			this.tvwModels.TabIndex = 4;
			this.tvwModels.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.evToggleVisible);
			this.tvwModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.evSelectObject);
			this.tvwModels.Leave += new System.EventHandler(this.evUnfocusObjects);
			// 
			// glFrame
			// 
			this.glFrame.AllowDrop = true;
			this.glFrame.BackColor = System.Drawing.Color.Black;
			this.glFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.glFrame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.glFrame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glFrame.Location = new System.Drawing.Point(0, 49);
			this.glFrame.Name = "glFrame";
			this.glFrame.Size = new System.Drawing.Size(678, 366);
			this.glFrame.TabIndex = 0;
			this.glFrame.VSync = true;
			this.glFrame.Load += new System.EventHandler(this.evGLLoad);
			this.glFrame.DragDrop += new System.Windows.Forms.DragEventHandler(this.evGLDragDrop);
			this.glFrame.DragEnter += new System.Windows.Forms.DragEventHandler(this.evGLDragEnter);
			this.glFrame.Paint += new System.Windows.Forms.PaintEventHandler(this.evGLRender);
			this.glFrame.KeyDown += new System.Windows.Forms.KeyEventHandler(this.evGLKeyDown);
			this.glFrame.KeyUp += new System.Windows.Forms.KeyEventHandler(this.evGLKeyUp);
			this.glFrame.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.evGLPreviewKey);
			this.glFrame.Resize += new System.EventHandler(this.evGLResize);
			// 
			// panObjectProps
			// 
			this.panObjectProps.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panObjectProps.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panObjectProps.Location = new System.Drawing.Point(0, 415);
			this.panObjectProps.Name = "panObjectProps";
			this.panObjectProps.Size = new System.Drawing.Size(880, 143);
			this.panObjectProps.TabIndex = 5;
			// 
			// btnCloseAll
			// 
			this.btnCloseAll.Name = "btnCloseAll";
			this.btnCloseAll.Size = new System.Drawing.Size(154, 22);
			this.btnCloseAll.Text = "Close All";
			this.btnCloseAll.ToolTipText = "Close all currently-loaded models";
			this.btnCloseAll.Click += new System.EventHandler(this.evClickCloseAll);
			// 
			// demoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(880, 558);
			this.Controls.Add(this.glFrame);
			this.Controls.Add(this.panScene);
			this.Controls.Add(strMain);
			this.Controls.Add(menMain);
			this.Controls.Add(this.panObjectProps);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KeyPreview = true;
			this.MainMenuStrip = menMain;
			this.Name = "demoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Demolisher";
			menMain.ResumeLayout(false);
			menMain.PerformLayout();
			this.panScene.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private arookas.demoObjectPropsControl panObjectProps;
		private OpenTK.GLControl glFrame;
		private System.Windows.Forms.ToolStripMenuItem btnOpen;
		private System.Windows.Forms.ToolStripMenuItem btnWireFrame;
		private System.Windows.Forms.ToolStripMenuItem btnInvertLook;
		private System.Windows.Forms.ToolStripMenuItem btnAlphaTest;
		private System.Windows.Forms.ToolStripMenuItem btnGrid;
		private System.Windows.Forms.TreeView tvwModels;
		private System.Windows.Forms.ToolStripMenuItem btnBounds;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem btnFourthWalls;
		private System.Windows.Forms.ToolStripMenuItem btnCeilings;
		private System.Windows.Forms.ToolStripMenuItem btnNBT;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Panel panScene;
		private System.Windows.Forms.ToolStripMenuItem btnCloseAll;
	}
}