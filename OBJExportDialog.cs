using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Arookas.Demolisher
{
	public class OBJExportDialog : Form
	{
		public bool ExportTextures
		{
			get
			{
				return this.chk_textures.Checked;
			}
		}

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

		public bool ExportGeometry
		{
			get
			{
				return this.chk_geometry.Checked;
			}
		}

		public bool OnlyVisible
		{
			get
			{
				return this.chk_onlyvisible.Checked;
			}
		}

		public bool IgnoreTransforms
		{
			get
			{
				return this.chk_transforms.Checked;
			}
		}

		public bool SwapU
		{
			get
			{
				return this.chk_swapu.Checked;
			}
		}

		public bool SwapV
		{
			get
			{
				return this.chk_swapv.Checked;
			}
        }

        public OBJExportDialog()
		{
			this.InitializeComponent();
			this.cbx_textureformat.SelectedIndex = 0;
		}

		private void chk_geometry_CheckedChanged(object sender, EventArgs e)
		{
			this.chk_onlyvisible.Enabled = (this.chk_transforms.Enabled = (this.chk_swapu.Enabled = (this.chk_swapv.Enabled = this.chk_geometry.Checked)));
        }

		private void chk_textures_CheckedChanged(object sender, EventArgs e)
		{
			this.lbl_textureformat.Enabled = (this.cbx_textureformat.Enabled = this.chk_textures.Checked);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.chk_textures = new System.Windows.Forms.CheckBox();
            this.grp_geometry = new System.Windows.Forms.GroupBox();
            this.chk_swapv = new System.Windows.Forms.CheckBox();
            this.chk_onlyvisible = new System.Windows.Forms.CheckBox();
            this.chk_swapu = new System.Windows.Forms.CheckBox();
            this.chk_transforms = new System.Windows.Forms.CheckBox();
            this.chk_geometry = new System.Windows.Forms.CheckBox();
            this.grp_textures = new System.Windows.Forms.GroupBox();
            this.cbx_textureformat = new System.Windows.Forms.ComboBox();
            this.lbl_textureformat = new System.Windows.Forms.Label();
            this.grp_geometry.SuspendLayout();
            this.grp_textures.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(235, 282);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(141, 28);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(85, 282);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(141, 28);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            // 
            // chk_textures
            // 
            this.chk_textures.AutoSize = true;
            this.chk_textures.Checked = true;
            this.chk_textures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_textures.Location = new System.Drawing.Point(8, 23);
            this.chk_textures.Margin = new System.Windows.Forms.Padding(4);
            this.chk_textures.Name = "chk_textures";
            this.chk_textures.Size = new System.Drawing.Size(124, 21);
            this.chk_textures.TabIndex = 2;
            this.chk_textures.Text = "Export textures";
            this.chk_textures.UseVisualStyleBackColor = true;
            // 
            // grp_geometry
            // 
            this.grp_geometry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_geometry.Controls.Add(this.chk_swapv);
            this.grp_geometry.Controls.Add(this.chk_onlyvisible);
            this.grp_geometry.Controls.Add(this.chk_swapu);
            this.grp_geometry.Controls.Add(this.chk_transforms);
            this.grp_geometry.Controls.Add(this.chk_geometry);
            this.grp_geometry.Location = new System.Drawing.Point(16, 137);
            this.grp_geometry.Margin = new System.Windows.Forms.Padding(4);
            this.grp_geometry.Name = "grp_geometry";
            this.grp_geometry.Padding = new System.Windows.Forms.Padding(4);
            this.grp_geometry.Size = new System.Drawing.Size(360, 138);
            this.grp_geometry.TabIndex = 3;
            this.grp_geometry.TabStop = false;
            this.grp_geometry.Text = "Geometry";
            this.grp_geometry.Enter += new System.EventHandler(this.grp_geometry_Enter);
            // 
            // chk_swapv
            // 
            this.chk_swapv.AutoSize = true;
            this.chk_swapv.Checked = true;
            this.chk_swapv.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_swapv.Location = new System.Drawing.Point(111, 109);
            this.chk_swapv.Margin = new System.Windows.Forms.Padding(4);
            this.chk_swapv.Name = "chk_swapv";
            this.chk_swapv.Size = new System.Drawing.Size(77, 21);
            this.chk_swapv.TabIndex = 4;
            this.chk_swapv.Text = "Swap V";
            this.chk_swapv.UseVisualStyleBackColor = true;
            // 
            // chk_onlyvisible
            // 
            this.chk_onlyvisible.AutoSize = true;
            this.chk_onlyvisible.Checked = true;
            this.chk_onlyvisible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_onlyvisible.Location = new System.Drawing.Point(25, 52);
            this.chk_onlyvisible.Margin = new System.Windows.Forms.Padding(4);
            this.chk_onlyvisible.Name = "chk_onlyvisible";
            this.chk_onlyvisible.Size = new System.Drawing.Size(151, 21);
            this.chk_onlyvisible.TabIndex = 7;
            this.chk_onlyvisible.Text = "Only visible objects";
            this.chk_onlyvisible.UseVisualStyleBackColor = true;
            // 
            // chk_swapu
            // 
            this.chk_swapu.AutoSize = true;
            this.chk_swapu.Location = new System.Drawing.Point(25, 109);
            this.chk_swapu.Margin = new System.Windows.Forms.Padding(4);
            this.chk_swapu.Name = "chk_swapu";
            this.chk_swapu.Size = new System.Drawing.Size(78, 21);
            this.chk_swapu.TabIndex = 3;
            this.chk_swapu.Text = "Swap U";
            this.chk_swapu.UseVisualStyleBackColor = true;
            // 
            // chk_transforms
            // 
            this.chk_transforms.AutoSize = true;
            this.chk_transforms.Location = new System.Drawing.Point(25, 80);
            this.chk_transforms.Margin = new System.Windows.Forms.Padding(4);
            this.chk_transforms.Name = "chk_transforms";
            this.chk_transforms.Size = new System.Drawing.Size(183, 21);
            this.chk_transforms.TabIndex = 6;
            this.chk_transforms.Text = "Ignore object transforms";
            this.chk_transforms.UseVisualStyleBackColor = true;
            // 
            // chk_geometry
            // 
            this.chk_geometry.AutoSize = true;
            this.chk_geometry.Checked = true;
            this.chk_geometry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_geometry.Location = new System.Drawing.Point(8, 23);
            this.chk_geometry.Margin = new System.Windows.Forms.Padding(4);
            this.chk_geometry.Name = "chk_geometry";
            this.chk_geometry.Size = new System.Drawing.Size(133, 21);
            this.chk_geometry.TabIndex = 4;
            this.chk_geometry.Text = "Export geometry";
            this.chk_geometry.UseVisualStyleBackColor = true;
            // 
            // grp_textures
            // 
            this.grp_textures.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_textures.Controls.Add(this.cbx_textureformat);
            this.grp_textures.Controls.Add(this.lbl_textureformat);
            this.grp_textures.Controls.Add(this.chk_textures);
            this.grp_textures.Location = new System.Drawing.Point(16, 15);
            this.grp_textures.Margin = new System.Windows.Forms.Padding(4);
            this.grp_textures.Name = "grp_textures";
            this.grp_textures.Padding = new System.Windows.Forms.Padding(4);
            this.grp_textures.Size = new System.Drawing.Size(360, 114);
            this.grp_textures.TabIndex = 7;
            this.grp_textures.TabStop = false;
            this.grp_textures.Text = "Textures";
            // 
            // cbx_textureformat
            // 
            this.cbx_textureformat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbx_textureformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_textureformat.FormattingEnabled = true;
            this.cbx_textureformat.Items.AddRange(new object[] {
            "PNG",
            "TIFF",
            "BMP",
            "JPEG"});
            this.cbx_textureformat.Location = new System.Drawing.Point(25, 68);
            this.cbx_textureformat.Margin = new System.Windows.Forms.Padding(4);
            this.cbx_textureformat.Name = "cbx_textureformat";
            this.cbx_textureformat.Size = new System.Drawing.Size(325, 24);
            this.cbx_textureformat.TabIndex = 4;
            // 
            // lbl_textureformat
            // 
            this.lbl_textureformat.AutoSize = true;
            this.lbl_textureformat.Location = new System.Drawing.Point(21, 48);
            this.lbl_textureformat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_textureformat.Name = "lbl_textureformat";
            this.lbl_textureformat.Size = new System.Drawing.Size(56, 17);
            this.lbl_textureformat.TabIndex = 3;
            this.lbl_textureformat.Text = "Format:";
            // 
            // OBJExportDialog
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(392, 325);
            this.Controls.Add(this.grp_textures);
            this.Controls.Add(this.grp_geometry);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OBJExportDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Export OBJ";
            this.grp_geometry.ResumeLayout(false);
            this.grp_geometry.PerformLayout();
            this.grp_textures.ResumeLayout(false);
            this.grp_textures.PerformLayout();
            this.ResumeLayout(false);

		}

		private IContainer components;

		private Button btn_cancel;

		private Button btn_ok;

		private CheckBox chk_textures;

		private GroupBox grp_geometry;

		private CheckBox chk_transforms;

		private CheckBox chk_geometry;

		private CheckBox chk_onlyvisible;

		private GroupBox grp_textures;

		private CheckBox chk_swapv;

		private CheckBox chk_swapu;

		private ComboBox cbx_textureformat;

        private Label lbl_textureformat;

        private void grp_geometry_Enter(object sender, EventArgs e)
        {

        }
    }
}
