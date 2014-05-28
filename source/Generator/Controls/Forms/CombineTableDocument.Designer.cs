/*
 * oIo — 12/24/2010 ? 9:42 PM
 */
namespace Generator.Controls
{
	partial class CombineTablesForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombineTablesForm));
			this.cbDataSource = new System.Windows.Forms.ComboBox();
			this.cbDataDest = new System.Windows.Forms.ComboBox();
			this.cbTableSrc = new System.Windows.Forms.ComboBox();
			this.cbTableDest = new System.Windows.Forms.ComboBox();
			this.listFieldSource = new System.Windows.Forms.ListBox();
			this.listFieldDest = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbDataSource
			// 
			this.cbDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataSource.FormattingEnabled = true;
			this.cbDataSource.Location = new System.Drawing.Point(3, 3);
			this.cbDataSource.Name = "cbDataSource";
			this.cbDataSource.Size = new System.Drawing.Size(210, 21);
			this.cbDataSource.TabIndex = 0;
			this.cbDataSource.SelectedValueChanged += new System.EventHandler(this.dbSrcSelected);
			// 
			// cbDataDest
			// 
			this.cbDataDest.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbDataDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataDest.FormattingEnabled = true;
			this.cbDataDest.Location = new System.Drawing.Point(219, 3);
			this.cbDataDest.Name = "cbDataDest";
			this.cbDataDest.Size = new System.Drawing.Size(211, 21);
			this.cbDataDest.TabIndex = 0;
			this.cbDataDest.SelectedValueChanged += new System.EventHandler(this.dbDstSelected);
			// 
			// cbTableSrc
			// 
			this.cbTableSrc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbTableSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTableSrc.FormattingEnabled = true;
			this.cbTableSrc.Location = new System.Drawing.Point(3, 30);
			this.cbTableSrc.Name = "cbTableSrc";
			this.cbTableSrc.Size = new System.Drawing.Size(210, 21);
			this.cbTableSrc.TabIndex = 0;
			this.cbTableSrc.SelectedValueChanged += new System.EventHandler(this.tSrcSelected);
			// 
			// cbTableDest
			// 
			this.cbTableDest.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbTableDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTableDest.FormattingEnabled = true;
			this.cbTableDest.Location = new System.Drawing.Point(219, 30);
			this.cbTableDest.Name = "cbTableDest";
			this.cbTableDest.Size = new System.Drawing.Size(211, 21);
			this.cbTableDest.TabIndex = 0;
			this.cbTableDest.SelectedValueChanged += new System.EventHandler(this.tDstSelected);
			// 
			// listFieldSource
			// 
			this.listFieldSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listFieldSource.FormattingEnabled = true;
			this.listFieldSource.Location = new System.Drawing.Point(3, 57);
			this.listFieldSource.Name = "listFieldSource";
			this.listFieldSource.Size = new System.Drawing.Size(210, 171);
			this.listFieldSource.TabIndex = 1;
			this.listFieldSource.SelectedValueChanged += new System.EventHandler(this.eFieldSrcSelected);
			// 
			// listFieldDest
			// 
			this.listFieldDest.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listFieldDest.FormattingEnabled = true;
			this.listFieldDest.Location = new System.Drawing.Point(219, 57);
			this.listFieldDest.Name = "listFieldDest";
			this.listFieldDest.Size = new System.Drawing.Size(211, 171);
			this.listFieldDest.TabIndex = 1;
			this.listFieldDest.SelectedValueChanged += new System.EventHandler(this.eFieldDstSelected);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(219, 314);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(211, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Copy ‘TableElement’ to Clipboard";
			this.toolTip1.SetToolTip(this.button1, "NOTE:\r\n\tany elements that are not assigned to a destination\r\n\twill NOT be added t" +
						"o the generated field.");
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(219, 284);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(97, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "autoincr";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox1.Location = new System.Drawing.Point(3, 284);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(210, 24);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "Inherit Src";
			this.toolTip1.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.cbDataSource, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.button1, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.checkBox1, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.cbDataDest, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbTableSrc, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.cbTableDest, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.listFieldDest, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.listFieldSource, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.comboBox1, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.comboBox2, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label2, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.button2, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 6);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(433, 340);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// comboBox1
			// 
			this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(3, 257);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(210, 21);
			this.comboBox1.TabIndex = 4;
			// 
			// comboBox2
			// 
			this.comboBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(219, 257);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(211, 21);
			this.comboBox2.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
			this.label1.Location = new System.Drawing.Point(3, 231);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(210, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Source Property";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(219, 231);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(211, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Destination Property";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(12, 12);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 311);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(216, 25);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(74, 22);
			this.toolStripButton1.Text = "Select None";
			this.toolStripButton1.ToolTipText = "De-selects any associated field index from the source (left) table\r\nto the destin" +
			"ation (right).\r\n\r\nTo dis-associate all of the fields, (re-)select the source tab" +
			"le.";
			this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1Click);
			// 
			// CombineTableDocument
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(433, 340);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "CombineTableDocument";
			this.Text = "DocTableConverterTool";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listFieldDest;
		private System.Windows.Forms.ListBox listFieldSource;
		private System.Windows.Forms.ComboBox cbTableDest;
		private System.Windows.Forms.ComboBox cbTableSrc;
		private System.Windows.Forms.ComboBox cbDataDest;
		private System.Windows.Forms.ComboBox cbDataSource;
	}
}
