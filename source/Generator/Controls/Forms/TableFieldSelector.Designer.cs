/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 2/8/2013
 * Time: 5:49 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Generator.Controls
{
	partial class TableFieldSelector
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
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.tLinkId = new System.Windows.Forms.TextBox();
			this.tLinkOn = new System.Windows.Forms.TextBox();
			this.labelLinkOn = new System.Windows.Forms.Label();
			this.cbDataSource = new System.Windows.Forms.ComboBox();
			this.cbTableSrc = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listFieldSource = new System.Windows.Forms.CheckedListBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.invertCheckedItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unCheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tAlias = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tableLayoutPanel1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.tLinkId, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.tLinkOn, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.labelLinkOn, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.cbDataSource, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbTableSrc, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.listFieldSource, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tAlias, 0, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 9;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(158, 310);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// label3
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.label3, 2);
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
			this.label3.Location = new System.Drawing.Point(3, 274);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 12);
			this.label3.TabIndex = 11;
			this.label3.Text = "ID to Link";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tLinkId
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tLinkId, 2);
			this.tLinkId.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tLinkId.Location = new System.Drawing.Point(2, 288);
			this.tLinkId.Margin = new System.Windows.Forms.Padding(2);
			this.tLinkId.Name = "tLinkId";
			this.tLinkId.Size = new System.Drawing.Size(154, 20);
			this.tLinkId.TabIndex = 10;
			this.toolTip1.SetToolTip(this.tLinkId, "The local field being referenced in our Link.\r\n…as provided by the list above.");
			this.tLinkId.Enter += new System.EventHandler(this.InnerTriggerFocus);
			// 
			// tLinkOn
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tLinkOn, 2);
			this.tLinkOn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tLinkOn.Location = new System.Drawing.Point(2, 252);
			this.tLinkOn.Margin = new System.Windows.Forms.Padding(2);
			this.tLinkOn.Name = "tLinkOn";
			this.tLinkOn.Size = new System.Drawing.Size(154, 20);
			this.tLinkOn.TabIndex = 9;
			this.toolTip1.SetToolTip(this.tLinkOn, "This is the name of the table and field such\r\nas the primary table in the link-se" +
						"t.\r\n\tEG: ‘o.ID’");
			this.tLinkOn.Enter += new System.EventHandler(this.InnerTriggerFocus);
			// 
			// labelLinkOn
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.labelLinkOn, 2);
			this.labelLinkOn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelLinkOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
			this.labelLinkOn.Location = new System.Drawing.Point(3, 238);
			this.labelLinkOn.Name = "labelLinkOn";
			this.labelLinkOn.Size = new System.Drawing.Size(152, 12);
			this.labelLinkOn.TabIndex = 8;
			this.labelLinkOn.Text = "Link On";
			this.labelLinkOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cbDataSource
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.cbDataSource, 2);
			this.cbDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbDataSource.FormattingEnabled = true;
			this.cbDataSource.Location = new System.Drawing.Point(3, 3);
			this.cbDataSource.Name = "cbDataSource";
			this.cbDataSource.Size = new System.Drawing.Size(152, 21);
			this.cbDataSource.TabIndex = 0;
			// 
			// cbTableSrc
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.cbTableSrc, 2);
			this.cbTableSrc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbTableSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTableSrc.FormattingEnabled = true;
			this.cbTableSrc.Location = new System.Drawing.Point(3, 30);
			this.cbTableSrc.Name = "cbTableSrc";
			this.cbTableSrc.Size = new System.Drawing.Size(152, 21);
			this.cbTableSrc.TabIndex = 0;
			// 
			// label1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.label1, 2);
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F);
			this.label1.Location = new System.Drawing.Point(3, 202);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "Alias";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listFieldSource
			// 
			this.listFieldSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tableLayoutPanel1.SetColumnSpan(this.listFieldSource, 2);
			this.listFieldSource.ContextMenuStrip = this.contextMenuStrip1;
			this.listFieldSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listFieldSource.FormattingEnabled = true;
			this.listFieldSource.Location = new System.Drawing.Point(3, 57);
			this.listFieldSource.Name = "listFieldSource";
			this.listFieldSource.Size = new System.Drawing.Size(152, 142);
			this.listFieldSource.TabIndex = 6;
			this.listFieldSource.Enter += new System.EventHandler(this.InnerTriggerFocus);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.invertCheckedItemsToolStripMenuItem,
									this.checkAllToolStripMenuItem,
									this.unCheckAllToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(186, 92);
			// 
			// invertCheckedItemsToolStripMenuItem
			// 
			this.invertCheckedItemsToolStripMenuItem.Name = "invertCheckedItemsToolStripMenuItem";
			this.invertCheckedItemsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.invertCheckedItemsToolStripMenuItem.Text = "Invert Checked Items";
			this.invertCheckedItemsToolStripMenuItem.Click += new System.EventHandler(this.Event_CheckInvert);
			// 
			// checkAllToolStripMenuItem
			// 
			this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
			this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.checkAllToolStripMenuItem.Text = "Check All";
			this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.Event_CheckAll);
			// 
			// unCheckAllToolStripMenuItem
			// 
			this.unCheckAllToolStripMenuItem.Name = "unCheckAllToolStripMenuItem";
			this.unCheckAllToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.unCheckAllToolStripMenuItem.Text = "Un-Check All";
			this.unCheckAllToolStripMenuItem.Click += new System.EventHandler(this.Event_CheckNone);
			// 
			// tAlias
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.tAlias, 2);
			this.tAlias.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tAlias.Location = new System.Drawing.Point(2, 216);
			this.tAlias.Margin = new System.Windows.Forms.Padding(2);
			this.tAlias.Name = "tAlias";
			this.tAlias.Size = new System.Drawing.Size(154, 20);
			this.tAlias.TabIndex = 7;
			this.toolTip1.SetToolTip(this.tAlias, "Required.  This is the alias as referenced and linked.");
			this.tAlias.Enter += new System.EventHandler(this.InnerTriggerFocus);
			// 
			// TableFieldSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TableFieldSelector";
			this.Size = new System.Drawing.Size(158, 310);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem unCheckAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem invertCheckedItemsToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolTip toolTip1;
		internal System.Windows.Forms.TextBox tLinkId;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelLinkOn;
		internal System.Windows.Forms.TextBox tLinkOn;
		internal System.Windows.Forms.TextBox tAlias;
		private System.Windows.Forms.CheckedListBox listFieldSource;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.ComboBox cbTableSrc;
		internal System.Windows.Forms.ComboBox cbDataSource;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}
