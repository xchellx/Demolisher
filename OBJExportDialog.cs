using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Arookas.Demolisher
{
	// Token: 0x02000014 RID: 20
	public class OBJExportDialog : Form
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000064B8 File Offset: 0x000046B8
		public bool ExportTextures
		{
			get
			{
				return this.chk_textures.Checked;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000064C8 File Offset: 0x000046C8
		public ImageFormat TextureFormat
		{
			get
			{
				switch (this.cbx_textureformat.SelectedIndex)
				{
					case 1:
						return ImageFormat.Tiff;
					case 2:
						return ImageFormat.Bmp;
					case 3:
						return ImageFormat.Jpeg;
				}
				return ImageFormat.Png;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00006510 File Offset: 0x00004710
		public bool ExportGeometry
		{
			get
			{
				return this.chk_geometry.Checked;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000651D File Offset: 0x0000471D
		public bool OnlyVisible
		{
			get
			{
				return this.chk_onlyvisible.Checked;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000652A File Offset: 0x0000472A
		public bool IgnoreTransforms
		{
			get
			{
				return this.chk_transforms.Checked;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00006537 File Offset: 0x00004737
		public bool SwapU
		{
			get
			{
				return this.chk_swapu.Checked;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00006544 File Offset: 0x00004744
		public bool SwapV
		{
			get
			{
				return this.chk_swapv.Checked;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006551 File Offset: 0x00004751
		public OBJExportDialog()
		{
			this.InitializeComponent();
			this.cbx_textureformat.SelectedIndex = 0;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000656C File Offset: 0x0000476C
		private void chk_geometry_CheckedChanged(object sender, EventArgs e)
		{
			this.chk_onlyvisible.Enabled = (this.chk_transforms.Enabled = (this.chk_swapu.Enabled = (this.chk_swapv.Enabled = this.chk_geometry.Checked)));
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000065BC File Offset: 0x000047BC
		private void chk_textures_CheckedChanged(object sender, EventArgs e)
		{
			this.lbl_textureformat.Enabled = (this.cbx_textureformat.Enabled = this.chk_textures.Checked);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000065ED File Offset: 0x000047ED
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000660C File Offset: 0x0000480C
		private void InitializeComponent()
		{
			this.btn_cancel = new Button();
			this.btn_ok = new Button();
			this.chk_textures = new CheckBox();
			this.grp_geometry = new GroupBox();
			this.chk_swapv = new CheckBox();
			this.chk_onlyvisible = new CheckBox();
			this.chk_swapu = new CheckBox();
			this.chk_transforms = new CheckBox();
			this.chk_geometry = new CheckBox();
			this.grp_textures = new GroupBox();
			this.cbx_textureformat = new ComboBox();
			this.lbl_textureformat = new Label();
			this.grp_geometry.SuspendLayout();
			this.grp_textures.SuspendLayout();
			base.SuspendLayout();
			this.btn_cancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_cancel.DialogResult = DialogResult.Cancel;
			this.btn_cancel.Location = new Point(176, 232);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new Size(106, 23);
			this.btn_cancel.TabIndex = 0;
			this.btn_cancel.Text = "Cancel";
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_ok.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_ok.DialogResult = DialogResult.OK;
			this.btn_ok.Location = new Point(64, 232);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new Size(106, 23);
			this.btn_ok.TabIndex = 1;
			this.btn_ok.Text = "OK";
			this.btn_ok.UseVisualStyleBackColor = true;
			this.chk_textures.AutoSize = true;
			this.chk_textures.Checked = true;
			this.chk_textures.CheckState = CheckState.Checked;
			this.chk_textures.Location = new Point(6, 19);
			this.chk_textures.Name = "chk_textures";
			this.chk_textures.Size = new Size(96, 17);
			this.chk_textures.TabIndex = 2;
			this.chk_textures.Text = "Export textures";
			this.chk_textures.UseVisualStyleBackColor = true;
			this.chk_textures.CheckedChanged += this.chk_textures_CheckedChanged;
			this.grp_geometry.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.grp_geometry.Controls.Add(this.chk_swapv);
			this.grp_geometry.Controls.Add(this.chk_onlyvisible);
			this.grp_geometry.Controls.Add(this.chk_swapu);
			this.grp_geometry.Controls.Add(this.chk_transforms);
			this.grp_geometry.Controls.Add(this.chk_geometry);
			this.grp_geometry.Location = new Point(12, 111);
			this.grp_geometry.Name = "grp_geometry";
			this.grp_geometry.Size = new Size(270, 115);
			this.grp_geometry.TabIndex = 3;
			this.grp_geometry.TabStop = false;
			this.grp_geometry.Text = "Geometry";
			this.chk_swapv.AutoSize = true;
			this.chk_swapv.Checked = true;
			this.chk_swapv.CheckState = CheckState.Checked;
			this.chk_swapv.Location = new Point(89, 88);
			this.chk_swapv.Name = "chk_swapv";
			this.chk_swapv.Size = new Size(63, 17);
			this.chk_swapv.TabIndex = 4;
			this.chk_swapv.Text = "Swap V";
			this.chk_swapv.UseVisualStyleBackColor = true;
			this.chk_onlyvisible.AutoSize = true;
			this.chk_onlyvisible.Checked = true;
			this.chk_onlyvisible.CheckState = CheckState.Checked;
			this.chk_onlyvisible.Location = new Point(19, 42);
			this.chk_onlyvisible.Name = "chk_onlyvisible";
			this.chk_onlyvisible.Size = new Size(116, 17);
			this.chk_onlyvisible.TabIndex = 7;
			this.chk_onlyvisible.Text = "Only visible objects";
			this.chk_onlyvisible.UseVisualStyleBackColor = true;
			this.chk_swapu.AutoSize = true;
			this.chk_swapu.Location = new Point(19, 88);
			this.chk_swapu.Name = "chk_swapu";
			this.chk_swapu.Size = new Size(64, 17);
			this.chk_swapu.TabIndex = 3;
			this.chk_swapu.Text = "Swap U";
			this.chk_swapu.UseVisualStyleBackColor = true;
			this.chk_transforms.AutoSize = true;
			this.chk_transforms.Location = new Point(19, 65);
			this.chk_transforms.Name = "chk_transforms";
			this.chk_transforms.Size = new Size(139, 17);
			this.chk_transforms.TabIndex = 6;
			this.chk_transforms.Text = "Ignore object transforms";
			this.chk_transforms.UseVisualStyleBackColor = true;
			this.chk_geometry.AutoSize = true;
			this.chk_geometry.Checked = true;
			this.chk_geometry.CheckState = CheckState.Checked;
			this.chk_geometry.Location = new Point(6, 19);
			this.chk_geometry.Name = "chk_geometry";
			this.chk_geometry.Size = new Size(102, 17);
			this.chk_geometry.TabIndex = 4;
			this.chk_geometry.Text = "Export geometry";
			this.chk_geometry.UseVisualStyleBackColor = true;
			this.chk_geometry.CheckedChanged += this.chk_geometry_CheckedChanged;
			this.grp_textures.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.grp_textures.Controls.Add(this.cbx_textureformat);
			this.grp_textures.Controls.Add(this.lbl_textureformat);
			this.grp_textures.Controls.Add(this.chk_textures);
			this.grp_textures.Location = new Point(12, 12);
			this.grp_textures.Name = "grp_textures";
			this.grp_textures.Size = new Size(270, 93);
			this.grp_textures.TabIndex = 7;
			this.grp_textures.TabStop = false;
			this.grp_textures.Text = "Textures";
			this.cbx_textureformat.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.cbx_textureformat.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbx_textureformat.FormattingEnabled = true;
			this.cbx_textureformat.Items.AddRange(new object[]
			{
				"PNG",
				"TIFF",
				"BMP",
				"JPEG"
			});
			this.cbx_textureformat.Location = new Point(19, 55);
			this.cbx_textureformat.Name = "cbx_textureformat";
			this.cbx_textureformat.Size = new Size(245, 21);
			this.cbx_textureformat.TabIndex = 4;
			this.lbl_textureformat.AutoSize = true;
			this.lbl_textureformat.Location = new Point(16, 39);
			this.lbl_textureformat.Name = "lbl_textureformat";
			this.lbl_textureformat.Size = new Size(42, 13);
			this.lbl_textureformat.TabIndex = 3;
			this.lbl_textureformat.Text = "Format:";
			base.AcceptButton = this.btn_ok;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.btn_cancel;
			base.ClientSize = new Size(294, 267);
			base.Controls.Add(this.grp_textures);
			base.Controls.Add(this.grp_geometry);
			base.Controls.Add(this.btn_ok);
			base.Controls.Add(this.btn_cancel);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "OBJExportDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Export OBJ";
			this.grp_geometry.ResumeLayout(false);
			this.grp_geometry.PerformLayout();
			this.grp_textures.ResumeLayout(false);
			this.grp_textures.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x04000081 RID: 129
		private IContainer components;

		// Token: 0x04000082 RID: 130
		private Button btn_cancel;

		// Token: 0x04000083 RID: 131
		private Button btn_ok;

		// Token: 0x04000084 RID: 132
		private CheckBox chk_textures;

		// Token: 0x04000085 RID: 133
		private GroupBox grp_geometry;

		// Token: 0x04000086 RID: 134
		private CheckBox chk_transforms;

		// Token: 0x04000087 RID: 135
		private CheckBox chk_geometry;

		// Token: 0x04000088 RID: 136
		private CheckBox chk_onlyvisible;

		// Token: 0x04000089 RID: 137
		private GroupBox grp_textures;

		// Token: 0x0400008A RID: 138
		private CheckBox chk_swapv;

		// Token: 0x0400008B RID: 139
		private CheckBox chk_swapu;

		// Token: 0x0400008C RID: 140
		private ComboBox cbx_textureformat;

		// Token: 0x0400008D RID: 141
		private Label lbl_textureformat;
	}
}
