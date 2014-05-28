/*
 * oIo ? 12/24/2010 ? 9:42 PM
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Cor3;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Generator.Classes;
using Generator.Elements;
using Generator.Elements.Types;

namespace Generator.Controls
{
	public partial class CombineTablesViewForm : Form
	{
		
		internal readonly UITemplateManager Host;
		
		DataSet FormContext = new DataSet("formcontext");
		
		DataTable dbSrc { get { return FormContext.Tables["dbSrc"]; } }
		DataTable dbDst { get { return FormContext.Tables["dbDst"]; } }
		DataTable tblSrc { get { return FormContext.Tables["tblSrc"]; } }
		DataTable tblDst { get { return FormContext.Tables["tblDst"]; } }
		DataTable fSrc { get { return FormContext.Tables["fSrc"]; } }
		DataTable fDst { get { return FormContext.Tables["fDst"]; } }

		DatabaseCollection Collection { get { return Host.SelectedCollection; } }
		
		enum selContext { source, destination }
		enum selType { database, table }
		string sDbSrc,sDbDst,sTblSrc,sTblDest;

		string this[selContext ctx, selType typ]
		{
			get
			{
				if (ctx== selContext.destination)
				{
					if (typ== selType.database) return sDbDst;
					return sTblDest;
				}
				if (typ== selType.database) return sDbSrc;
				return sTblSrc;
			}
			set
			{
				if (ctx== selContext.destination)
				{
					if (typ== selType.database) sDbDst = value;
					else sTblDest = value;
					return;
				}
				if (typ== selType.database) sDbSrc = value;
				else sTblSrc = value;
			}
		}
		
		public CombinedViewInfo ViewInfo
		{
			get
			{
				return new CombinedViewInfo{
					Host = this.Host,
					Infos = GetInfos()
				};
			}
		}
		public List<ProvidedViewInfo> GetInfos()
		{
			List<ProvidedViewInfo> infos = new List<ProvidedViewInfo>();
			foreach (Control c in this.tableLayoutPanel1.Controls)
			{
				infos.Add((c as TableFieldSelector).ProvidedView);
			}
			return infos;
		}
		
		public CombineTablesViewForm(UITemplateManager host)
		{
			Host = host;
			InitializeComponent();
		}
		
		#region Designer
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombineTablesViewForm));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tViewName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.tableLayoutPanel1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tViewName
			// 
			this.tViewName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tViewName.Location = new System.Drawing.Point(175, 3);
			this.tViewName.Name = "tViewName";
			this.tViewName.Size = new System.Drawing.Size(565, 23);
			this.tViewName.TabIndex = 3;
			this.toolTip1.SetToolTip(this.tViewName, "Required: A name for this provided View");
			// 
			// groupBox1
			// 
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 390);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
			this.groupBox1.Size = new System.Drawing.Size(743, 92);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 29);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(743, 361);
			this.flowLayoutPanel1.TabIndex = 3;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.tViewName, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(743, 29);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripButton1,
									this.toolStripButton2,
									this.toolStripSplitButton1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
			this.menuStrip1.Size = new System.Drawing.Size(172, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 21);
			this.toolStripButton1.Text = "toolStripDropDownButton1";
			this.toolStripButton1.Click += new System.EventHandler(this.AddLinkTableToolStripMenuItemClick);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 21);
			this.toolStripButton2.Text = "toolStripDropDownButton1";
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
			this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 21);
			this.toolStripSplitButton1.Text = "Add to Database/Table-Configuration File";
			this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.Event_Harvest);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripDropDownButton1});
			this.toolStrip1.Location = new System.Drawing.Point(411, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(332, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.Visible = false;
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(32, 22);
			this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
			// 
			// CombineTablesViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(743, 482);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "CombineTablesViewForm";
			this.Text = "DocTableConverterTool";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox tViewName;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripSplitButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolTip toolTip1;
		#endregion
		
		int newId = 0;
		
		public int NewId { get { return ++newId; } }
		
		void AddLinkTableToolStripMenuItemClick(object sender, EventArgs e)
		{
			TableFieldSelector selector = new TableFieldSelector();
			flowLayoutPanel1.Controls.Add(selector);
			selector.RebaseControl(Host);
			selector.Name = string.Format("Table{0:00#}",newId);
			selector.Show();
		}
		
		void Event_Harvest(object sender, EventArgs e)
		{
			DataViewElement element = GetDataView();
			TreeNode tableNode = Host.BoundTree.Nodes[0]
				.Nodes.Find(element.Database,false)[0]
				.Nodes.Find(element.Table,false)[0];
			
			tableNode.Parent.Nodes.Insert(tableNode.Index,element.ToNode());
		}
		public DataViewElement GetDataView()
		{
			if (this.flowLayoutPanel1.Controls.Count <= 1)
			{
				MessageBox.Show("Not enough tables selected to provide a view.","User error");
				return null;
			}
			
			List<DataViewLink> list = new List<DataViewLink>();
			foreach (Control c in flowLayoutPanel1.Controls)
			{
				var fs = c as TableFieldSelector;
				list.Add(fs.ProvidedLink);
			}
			
			DataViewElement view = new DataViewElement();
			view.Name = tViewName.Text;
			view.Database = list[0].Database;
			view.Table = list[0].Table;
			view.Alias = list[0].Alias;
			view.Fields = list[0].Fields;
			view.LinkItems = new List<DataViewLink>();
			for (int i=1; i<list.Count; i++)
			{
				view.LinkItems.Add(list[i]);
			}
			return view;
		}
		
	}
	
}
