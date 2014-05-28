/*
 * User: oIo
 * Date: 2/7/2011 – 11:53 PM
 */
namespace Generator.Controls
{
	partial class UcDataTreeForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcDataTreeForm));
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnUp = new System.Windows.Forms.ToolStripButton();
			this.btnDown = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.btnDelete = new System.Windows.Forms.ToolStripButton();
			this.tsMergeTables4View = new System.Windows.Forms.ToolStripButton();
			this.treeView1 = new System.Cor3.Forms.TreeViewMover();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btnUp,
									this.btnDown,
									this.toolStripButton1,
									this.toolStripButton2,
									this.toolStripSeparator1,
									this.toolStripButton3,
									this.btnDelete,
									this.tsMergeTables4View});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(293, 25);
			this.toolStrip1.TabIndex = 8;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnUp
			// 
			this.btnUp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnUp.Enabled = false;
			this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
			this.btnUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(23, 22);
			// 
			// btnDown
			// 
			this.btnDown.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDown.Enabled = false;
			this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
			this.btnDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(23, 22);
			this.btnDown.Text = "toolStripButton2";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.Click += new System.EventHandler(this.eSaveDataConfiguration);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "toolStripButton2";
			this.toolStripButton2.Click += new System.EventHandler(this.eLoadDataConfiguration);
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "toolStripButton3";
			this.toolStripButton3.Click += new System.EventHandler(this.eRunCombineTable);
			// 
			// btnDelete
			// 
			this.btnDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(23, 22);
			this.btnDelete.Text = "toolStripButton4";
			// 
			// tsMergeTables4View
			// 
			this.tsMergeTables4View.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsMergeTables4View.Image = ((System.Drawing.Image)(resources.GetObject("tsMergeTables4View.Image")));
			this.tsMergeTables4View.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsMergeTables4View.Name = "tsMergeTables4View";
			this.tsMergeTables4View.Size = new System.Drawing.Size(23, 22);
			this.tsMergeTables4View.Text = "Merge Tables for MODEL/VIEW";
			this.tsMergeTables4View.Click += new System.EventHandler(this.eRunCreateView);
			// 
			// treeView1
			// 
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.ButtonDelete = null;
			this.treeView1.ButtonDown = this.btnDown;
			this.treeView1.ButtonUp = this.btnUp;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.FullRowSelect = true;
			this.treeView1.HideSelection = false;
			this.treeView1.Location = new System.Drawing.Point(0, 25);
			this.treeView1.Name = "treeView1";
			this.treeView1.ShowLines = false;
			this.treeView1.ShowNodeToolTips = true;
			this.treeView1.Size = new System.Drawing.Size(293, 413);
			this.treeView1.TabIndex = 9;
			// 
			// UcDataTreeForm
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.toolStrip1);
			this.Name = "UcDataTreeForm";
			this.Size = new System.Drawing.Size(293, 438);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripButton tsMergeTables4View;
		private System.Windows.Forms.ToolStripButton btnDelete;
		private System.Cor3.Forms.TreeViewMover treeView1;
		private System.Windows.Forms.ToolStripButton toolStripButton3;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton btnDown;
		private System.Windows.Forms.ToolStripButton btnUp;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
