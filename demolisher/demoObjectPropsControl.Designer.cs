namespace arookas
{
	partial class demoObjectPropsControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TableLayoutPanel tblMain;
			System.Windows.Forms.GroupBox grpHierarchy;
			System.Windows.Forms.TableLayoutPanel tblHierarchy;
			System.Windows.Forms.GroupBox grpFlags;
			System.Windows.Forms.TableLayoutPanel tblFlags;
			this.lblNextNum = new System.Windows.Forms.Label();
			this.lblNext = new System.Windows.Forms.Label();
			this.lblChildNum = new System.Windows.Forms.Label();
			this.lblChild = new System.Windows.Forms.Label();
			this.lblIndexNum = new System.Windows.Forms.Label();
			this.lblIndex = new System.Windows.Forms.Label();
			this.chkFlag1 = new System.Windows.Forms.CheckBox();
			this.chkFlag5 = new System.Windows.Forms.CheckBox();
			this.chkFlag6 = new System.Windows.Forms.CheckBox();
			this.chkFlag7 = new System.Windows.Forms.CheckBox();
			this.chkFlag8 = new System.Windows.Forms.CheckBox();
			this.chkFlag2 = new System.Windows.Forms.CheckBox();
			this.chkFlag3 = new System.Windows.Forms.CheckBox();
			this.chkFlag4 = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			tblMain = new System.Windows.Forms.TableLayoutPanel();
			grpHierarchy = new System.Windows.Forms.GroupBox();
			tblHierarchy = new System.Windows.Forms.TableLayoutPanel();
			grpFlags = new System.Windows.Forms.GroupBox();
			tblFlags = new System.Windows.Forms.TableLayoutPanel();
			tblMain.SuspendLayout();
			grpHierarchy.SuspendLayout();
			tblHierarchy.SuspendLayout();
			grpFlags.SuspendLayout();
			tblFlags.SuspendLayout();
			this.SuspendLayout();
			// 
			// tblMain
			// 
			tblMain.ColumnCount = 1;
			tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tblMain.Controls.Add(grpHierarchy, 0, 0);
			tblMain.Controls.Add(grpFlags, 0, 1);
			tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
			tblMain.Location = new System.Drawing.Point(0, 0);
			tblMain.Name = "tblMain";
			tblMain.RowCount = 3;
			tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
			tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblMain.Size = new System.Drawing.Size(205, 303);
			tblMain.TabIndex = 0;
			// 
			// grpHierarchy
			// 
			grpHierarchy.Controls.Add(tblHierarchy);
			grpHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
			grpHierarchy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			grpHierarchy.Location = new System.Drawing.Point(3, 3);
			grpHierarchy.Name = "grpHierarchy";
			grpHierarchy.Size = new System.Drawing.Size(199, 64);
			grpHierarchy.TabIndex = 0;
			grpHierarchy.TabStop = false;
			grpHierarchy.Text = "Hierarchy";
			// 
			// tblHierarchy
			// 
			tblHierarchy.ColumnCount = 2;
			tblHierarchy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tblHierarchy.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tblHierarchy.Controls.Add(this.lblNextNum, 1, 2);
			tblHierarchy.Controls.Add(this.lblNext, 0, 2);
			tblHierarchy.Controls.Add(this.lblChildNum, 1, 1);
			tblHierarchy.Controls.Add(this.lblChild, 0, 1);
			tblHierarchy.Controls.Add(this.lblIndexNum, 1, 0);
			tblHierarchy.Controls.Add(this.lblIndex, 0, 0);
			tblHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
			tblHierarchy.Location = new System.Drawing.Point(3, 18);
			tblHierarchy.Name = "tblHierarchy";
			tblHierarchy.RowCount = 4;
			tblHierarchy.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblHierarchy.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblHierarchy.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblHierarchy.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblHierarchy.Size = new System.Drawing.Size(193, 43);
			tblHierarchy.TabIndex = 0;
			// 
			// lblNextNum
			// 
			this.lblNextNum.AutoSize = true;
			this.lblNextNum.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblNextNum.Location = new System.Drawing.Point(99, 26);
			this.lblNextNum.Name = "lblNextNum";
			this.lblNextNum.Size = new System.Drawing.Size(91, 13);
			this.lblNextNum.TabIndex = 5;
			this.lblNextNum.Text = "#999";
			// 
			// lblNext
			// 
			this.lblNext.AutoSize = true;
			this.lblNext.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblNext.Location = new System.Drawing.Point(3, 26);
			this.lblNext.Name = "lblNext";
			this.lblNext.Size = new System.Drawing.Size(90, 13);
			this.lblNext.TabIndex = 4;
			this.lblNext.Text = "Next";
			// 
			// lblChildNum
			// 
			this.lblChildNum.AutoSize = true;
			this.lblChildNum.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblChildNum.Location = new System.Drawing.Point(99, 13);
			this.lblChildNum.Name = "lblChildNum";
			this.lblChildNum.Size = new System.Drawing.Size(91, 13);
			this.lblChildNum.TabIndex = 3;
			this.lblChildNum.Text = "#999";
			// 
			// lblChild
			// 
			this.lblChild.AutoSize = true;
			this.lblChild.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblChild.Location = new System.Drawing.Point(3, 13);
			this.lblChild.Name = "lblChild";
			this.lblChild.Size = new System.Drawing.Size(90, 13);
			this.lblChild.TabIndex = 2;
			this.lblChild.Text = "Child";
			// 
			// lblIndexNum
			// 
			this.lblIndexNum.AutoSize = true;
			this.lblIndexNum.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblIndexNum.Location = new System.Drawing.Point(99, 0);
			this.lblIndexNum.Name = "lblIndexNum";
			this.lblIndexNum.Size = new System.Drawing.Size(91, 13);
			this.lblIndexNum.TabIndex = 1;
			this.lblIndexNum.Text = "#999";
			// 
			// lblIndex
			// 
			this.lblIndex.AutoSize = true;
			this.lblIndex.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblIndex.Location = new System.Drawing.Point(3, 0);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(90, 13);
			this.lblIndex.TabIndex = 0;
			this.lblIndex.Text = "Index";
			// 
			// grpFlags
			// 
			grpFlags.Controls.Add(tblFlags);
			grpFlags.Dock = System.Windows.Forms.DockStyle.Fill;
			grpFlags.Location = new System.Drawing.Point(3, 73);
			grpFlags.Name = "grpFlags";
			grpFlags.Size = new System.Drawing.Size(199, 134);
			grpFlags.TabIndex = 1;
			grpFlags.TabStop = false;
			grpFlags.Text = "Flags";
			// 
			// tblFlags
			// 
			tblFlags.ColumnCount = 2;
			tblFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tblFlags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tblFlags.Controls.Add(this.chkFlag1, 0, 0);
			tblFlags.Controls.Add(this.chkFlag5, 1, 0);
			tblFlags.Controls.Add(this.chkFlag6, 1, 1);
			tblFlags.Controls.Add(this.chkFlag7, 1, 2);
			tblFlags.Controls.Add(this.chkFlag8, 1, 3);
			tblFlags.Controls.Add(this.chkFlag2, 0, 1);
			tblFlags.Controls.Add(this.chkFlag3, 0, 2);
			tblFlags.Controls.Add(this.chkFlag4, 0, 3);
			tblFlags.Dock = System.Windows.Forms.DockStyle.Fill;
			tblFlags.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			tblFlags.Location = new System.Drawing.Point(3, 18);
			tblFlags.Name = "tblFlags";
			tblFlags.RowCount = 5;
			tblFlags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblFlags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblFlags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblFlags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblFlags.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblFlags.Size = new System.Drawing.Size(193, 113);
			tblFlags.TabIndex = 0;
			// 
			// chkFlag1
			// 
			this.chkFlag1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag1.Enabled = false;
			this.chkFlag1.Location = new System.Drawing.Point(3, 3);
			this.chkFlag1.Name = "chkFlag1";
			this.chkFlag1.Size = new System.Drawing.Size(90, 24);
			this.chkFlag1.TabIndex = 0;
			this.chkFlag1.Text = "???";
			this.chkFlag1.UseVisualStyleBackColor = true;
			// 
			// chkFlag5
			// 
			this.chkFlag5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag5.Enabled = false;
			this.chkFlag5.Location = new System.Drawing.Point(99, 3);
			this.chkFlag5.Name = "chkFlag5";
			this.chkFlag5.Size = new System.Drawing.Size(91, 24);
			this.chkFlag5.TabIndex = 1;
			this.chkFlag5.Text = "???";
			this.chkFlag5.UseVisualStyleBackColor = true;
			// 
			// chkFlag6
			// 
			this.chkFlag6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag6.Enabled = false;
			this.chkFlag6.Location = new System.Drawing.Point(99, 33);
			this.chkFlag6.Name = "chkFlag6";
			this.chkFlag6.Size = new System.Drawing.Size(91, 24);
			this.chkFlag6.TabIndex = 2;
			this.chkFlag6.Text = "???";
			this.chkFlag6.UseVisualStyleBackColor = true;
			// 
			// chkFlag7
			// 
			this.chkFlag7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag7.Enabled = false;
			this.chkFlag7.Location = new System.Drawing.Point(99, 63);
			this.chkFlag7.Name = "chkFlag7";
			this.chkFlag7.Size = new System.Drawing.Size(91, 17);
			this.chkFlag7.TabIndex = 3;
			this.chkFlag7.Text = "Fullbright";
			this.chkFlag7.UseVisualStyleBackColor = true;
			// 
			// chkFlag8
			// 
			this.chkFlag8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag8.Enabled = false;
			this.chkFlag8.Location = new System.Drawing.Point(99, 86);
			this.chkFlag8.Name = "chkFlag8";
			this.chkFlag8.Size = new System.Drawing.Size(91, 17);
			this.chkFlag8.TabIndex = 4;
			this.chkFlag8.Text = "Ceiling";
			this.chkFlag8.UseVisualStyleBackColor = true;
			// 
			// chkFlag2
			// 
			this.chkFlag2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag2.Enabled = false;
			this.chkFlag2.Location = new System.Drawing.Point(3, 33);
			this.chkFlag2.Name = "chkFlag2";
			this.chkFlag2.Size = new System.Drawing.Size(90, 24);
			this.chkFlag2.TabIndex = 5;
			this.chkFlag2.Text = "???";
			this.chkFlag2.UseVisualStyleBackColor = true;
			// 
			// chkFlag3
			// 
			this.chkFlag3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag3.Enabled = false;
			this.chkFlag3.Location = new System.Drawing.Point(3, 63);
			this.chkFlag3.Name = "chkFlag3";
			this.chkFlag3.Size = new System.Drawing.Size(90, 17);
			this.chkFlag3.TabIndex = 6;
			this.chkFlag3.Text = "Fourth Wall";
			this.chkFlag3.UseVisualStyleBackColor = true;
			// 
			// chkFlag4
			// 
			this.chkFlag4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkFlag4.Enabled = false;
			this.chkFlag4.Location = new System.Drawing.Point(3, 86);
			this.chkFlag4.Name = "chkFlag4";
			this.chkFlag4.Size = new System.Drawing.Size(90, 17);
			this.chkFlag4.TabIndex = 7;
			this.chkFlag4.Text = "Translucent";
			this.chkFlag4.UseVisualStyleBackColor = true;
			// 
			// demoObjectPropsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(tblMain);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "demoObjectPropsControl";
			this.Size = new System.Drawing.Size(205, 303);
			tblMain.ResumeLayout(false);
			grpHierarchy.ResumeLayout(false);
			tblHierarchy.ResumeLayout(false);
			tblHierarchy.PerformLayout();
			grpFlags.ResumeLayout(false);
			tblFlags.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.Label lblNextNum;
		private System.Windows.Forms.Label lblNext;
		private System.Windows.Forms.Label lblChildNum;
		private System.Windows.Forms.Label lblChild;
		private System.Windows.Forms.Label lblIndexNum;
		private System.Windows.Forms.CheckBox chkFlag1;
		private System.Windows.Forms.CheckBox chkFlag5;
		private System.Windows.Forms.CheckBox chkFlag6;
		private System.Windows.Forms.CheckBox chkFlag7;
		private System.Windows.Forms.CheckBox chkFlag8;
		private System.Windows.Forms.CheckBox chkFlag2;
		private System.Windows.Forms.CheckBox chkFlag3;
		private System.Windows.Forms.CheckBox chkFlag4;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
