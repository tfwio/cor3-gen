using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Generator.Assets;
using Generator.Elements;

namespace Generator.Controls
{
	/// <summary>
	/// Description of UserControl1.
	/// </summary>
	public partial class UcDataTreeForm : System.Windows.Forms.UserControl
	{
		internal Window1 Appwin;
		
		System.Cor3.Forms.treeview_node_mover mover;
		ImageList DatabaseImageList = new ImageList();

		public bool HasSelectedNode { get { return SelectedNode!=null; } }
		
		public TreeNode SelectedNode { get { return this.treeView1.SelectedNode; } }
		public TreeNode LastSelectedNode { get; private set; }
		
		public System.Windows.Forms.TreeView TreeView { get { return this.treeView1; } }

		enum clipboardmode { @default, cut,copy,paste,@remove }

		public class ImageKeys : ImageKeyNames
		{
			static public void Apply(ImageList list)
			{
				list.Images.Add(ImageKeys.Assembly,fam3.efx_ico.ref_asm);
				list.Images.Add(ImageKeys.AssemblyDirectory,fam3.efx_ico.ref_asmDir);
				list.Images.Add(ImageKeys.Class,fam3.efx_ico.class_object_ymc);
				list.Images.Add(ImageKeys.Databases,fam3.fugue_icons.databases);
				list.Images.Add(ImageKeys.Database,fam3.fugue_icons.tables_stacks);
				list.Images.Add(ImageKeys.Delegate,fam3.efx_ico.methodC);
				list.Images.Add(ImageKeys.Enumeration,fam3.efx_ico.ref_enum);
				list.Images.Add(ImageKeys.Field,fam3.efx_ico.field);
				list.Images.Add(ImageKeys.Framework,fam3.efx_ico.curly_right);
				list.Images.Add(ImageKeys.Function,fam3.efx_ico.methodMx);
				list.Images.Add(ImageKeys.Method,fam3.efx_ico.methodM);
				list.Images.Add(ImageKeys.Memo,fam3.famfam_silky.note);
				list.Images.Add(ImageKeys.NS,fam3.efx_ico.ref_ns);
				list.Images.Add(ImageKeys.Obj,fam3.efx_ico.class_object_yrb);
				list.Images.Add(ImageKeys.Snippit,fam3.fugue_icons.document_snippet);
				list.Images.Add(ImageKeys.Table,fam3.fugue_icons.table);
				list.Images.Add(ImageKeys.SqlCollection,fam3.fugue_icons.document_hf);
				list.Images.Add(ImageKeys.SqlQuery,fam3.fugue_icons.sql);
				list.Images.Add(ImageKeys.SqlQueryTemplate,fam3.fugue_icons.sql_join);
				list.Images.Add(ImageKeys.View,fam3.fugue_icons.block);
				list.Images.Add(ImageKeys.ViewLink,fam3.fugue_icons.arrow_join_090);
			}
		}
		
		int cmdFind()
		{
			foreach (System.Windows.Input.CommandBinding b in Appwin.CommandBindings)
			{
				if (b.Command==Generator.Classes.UITemplateManager.CmdUpdateSelectedElement)
				{
					return Appwin.CommandBindings.IndexOf(b);
				}
			}
			return -1;
		}

		void TreeViewNodeClickHandler(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Node==null) return;
			else if (e.Node.Tag==null) return;
			
			if (LastSelectedNode==e.Node)
			{
				System.Diagnostics.Debug.Print("SKIPPING. Selected node is same as previous...");
				return;
			}
			
			LastSelectedNode = e.Node;
			NodeSelected(e.Node);
			
		}
		//FIXME
		internal void NodeSelected(TreeNode node)
		{
			object o = node.Tag;
			
			if (o is DatabaseElement)
			{
				Appwin.TemplateFactory.SelectedDatabase = o as DatabaseElement;
				Appwin.TemplateFactory.SelectedView = null;
				Appwin.TemplateFactory.SelectedLink = null;
				Appwin.TemplateFactory.SelectedTable = null;
				Appwin.TemplateFactory.SelectedField = null;
				System.Diagnostics.Debug.Print("DatabaseElement Selected “{0}”", Appwin.TemplateFactory.SelectedDatabase.Name);
			}
			else if (o is TableElement)
			{
				Appwin.TemplateFactory.SelectedView = null;
				Appwin.TemplateFactory.SelectedLink = null;
				Appwin.TemplateFactory.SelectedField = null;
				Appwin.TemplateFactory.SelectedTable = o as TableElement;
				Appwin.TemplateFactory.SelectedDatabase = node.Parent.Tag as DatabaseElement;
				System.Diagnostics.Debug.Print("TableElement Selected “{0}”", Appwin.TemplateFactory.SelectedTable.Name);
			}
			else if (o is FieldElement)
			{
				Appwin.TemplateFactory.SelectedView = null;
				Appwin.TemplateFactory.SelectedLink = null;
				Appwin.TemplateFactory.SelectedTable = null;
				Appwin.TemplateFactory.SelectedDatabase = node.Parent.Parent.Tag as DatabaseElement;
				Appwin.TemplateFactory.SelectedTable = node.Parent.Tag as TableElement;
				Appwin.TemplateFactory.SelectedField = o as FieldElement;
				System.Diagnostics.Debug.Print("FieldElement Selected “{0}”", Appwin.TemplateFactory.SelectedField.DataName);
			}
			else if (o is QueryElement) // note how we don't reset the selection
			{
				Appwin.TemplateFactory.SelectedQuery = (o as QueryElement);
				Window1.This.ucSql.avalonTextEditor.Text = Appwin.TemplateFactory.SelectedQuery.sql;
				Appwin.TemplateFactory.SelectedView = null;
			}
			else if (o is DataViewElement) // note how we don't reset the selection
			{
				Appwin.TemplateFactory.SelectedView = null;
				Appwin.TemplateFactory.SelectedLink = null;
				Appwin.TemplateFactory.SelectedField = null;
				Appwin.TemplateFactory.SelectedTable = null;
				Appwin.TemplateFactory.SelectedDatabase = node.Parent.Tag as DatabaseElement;
				Appwin.TemplateFactory.SelectedView = (o as DataViewElement);
				System.Diagnostics.Debug.Print("DataViewElement Selected “{0}”", Appwin.TemplateFactory.SelectedView.Name);
			}
//			else if (o is DataViewElement) // note how we don't reset the selection
//			{
//				Appwin.TemplateFactory.SelectedView = (o as DataViewLink);
//			}
			else
			{
				Global.statC("{0}: Was selected and is not supported during this initialization process.", o.GetType().Name);
			}
			Logger.LogY(
				"got selection","{0}, {1}, {2}",
				HasDatabase?Appwin.TemplateFactory.SelectedDatabase.Name:"none",
				HasTable?Appwin.TemplateFactory.SelectedTable.Name:"none",
				HasField?Appwin.TemplateFactory.SelectedField.DataName:"none"
			);
			Appwin.TemplateFactory.OnUpdateDatabaseContextRequest();
			Appwin.TemplateFactory.UpdateSelectedElementHandler(node,null);
		}
		
		// HasQueryElement is defined in TemplateManager
		bool HasDatabase { get { return !(Appwin.TemplateFactory.SelectedDatabase==null); } }
		bool HasTable { get { return !(Appwin.TemplateFactory.SelectedTable==null); } }
		bool HasField { get { return !(Appwin.TemplateFactory.SelectedField==null); } }
		
		public void InitializeTree(Window1 win)
		{
			Appwin = win;
			mover = new System.Cor3.Forms.treeview_node_mover(treeView1, btnUp, btnDown, btnDelete);
			treeView1.NodeMouseClick += TreeViewNodeClickHandler;
			DatabaseImageList.ImageSize = new Size(16,16);
			DatabaseImageList.ColorDepth = ColorDepth.Depth32Bit;
			ImageKeys.Apply(DatabaseImageList);
			treeView1.ImageList = DatabaseImageList;
			// Richt-Click Context-Menu
			treeView1.MouseDown += eMouseDown;
		}
		
		public UcDataTreeForm()
		{
			InitializeComponent();
			WindowsInterop.WindowsTheme.HandleTheme(treeView1,true);
		}

		#region ClipBoard Item

		bool HasClipboard { get { return NodeClipboard!=null; } }
		TreeNode NodeClipboard { get;set; }
		TreeNode NodeClicked { get;set; }
		TreeNode NodeClone { get;set; }
		
		public void SetClipboardItem(TreeNode item)
		{
			NodeClipboard = NodeClone = item.Clone() as TreeNode;
		}
		
		/// <summary>
		/// Bare in mind that this is here to catch clicks for the elaboration of
		/// our context-menu.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void eMouseDown(object sender, MouseEventArgs e)
		{
			TreeNode tn = treeView1.HitTest(e.X,e.Y).Node;
			
			if (tn==null) return;
			if (tn.Tag==null) return;
			
			// Skipped (Reference Assembly)
			
			#region Right-Mouse Check #1
			if (e.Button != MouseButtons.Right)
			{
				// commented 2011.02.08
				if (tn.Tag!=null && tn.Tag is ReferenceAssemblyElement)
				{
//					Host.DocTableEditor.refAsm1
//						.FromReferenceAssemblyElement(
//							tn.Tag as ReferenceAssemblyElement);
				}
				return;
			}
			#endregion
			
			if (
				(tn.Tag is TableElement) ||
				(tn.Tag is FieldElement) ||
				(tn.Tag is DatabaseCollection) ||
				(tn.Tag is DatabaseElement)
			)
			{
				#region ContextMenu
				MakeContextMenu(e,tn);
				#endregion
			}
			else if ((tn.Tag as string) == DatabaseCollection.ref_asm_node)
			{
				// this is skipped due to the commenting above
				#region ContextMenu
				MakeContextMenu(e,tn);
				#endregion
			}
			else if (tn.Tag is string && (tn.Tag as string)==DatabaseCollection.ref_asm_node)
			{
				#region ContextMenu
//				ReferenceAssemblyElement fx = new ReferenceAssemblyElement(NodeClicked);
//				NodeClone = fx.ToNode();
				MakeContextMenu(e,tn);
				#endregion
			}
			else if (tn.Tag is ReferenceAssemblyElement)
			{
				#region SKIP
				ReferenceAssemblyElement rae = tn.Tag as ReferenceAssemblyElement;
//				if (rae.AssemblyIsLoaded)
//				if (rae.HasAsmName) Host.DocTableEditor.refAsm1.LoadAssemblyFileReflectionOnly(rae.AssemblyFileLocation);
				MakeContextMenu(e,tn);
				#endregion
			}
			else
			{
				Global.statY("{0}",tn.Tag);
			}
		}

		#region TreeView.Nodes.Node.ContextMenuStrip
		
		static int separatorNum = 0;
		static string SeparatorNames { get { return string.Format("toolStripSeparator{0:00#}",++separatorNum); } }
		ContextMenuStrip NodeContext;
		ToolStripSeparator sep { get { return new ToolStripSeparator(); } }
		static ToolStripItem NewItem(string name, Image icon=null, EventHandler e=null)
		{
			if (name=="-") return new ToolStripSeparator(){Name=SeparatorNames};
			return new ToolStripMenuItem(name,icon,e);
		}
		
		ToolStripMenuItem GetNewT(TreeNode node)
		{
			NodeClicked = node;
			ToolStripMenuItem itemNew = new ToolStripMenuItem("New — ‘{0}’",null,eItemCut);
			if (NodeClicked.Tag is DatabaseElement)
			{
				DatabaseElement tx = new DatabaseElement(NodeClicked);
				itemNew.Text = string.Format(itemNew.Text,"Table");
				NodeClone = tx.ToNode();
			}
			else if (NodeClicked.Tag is TableElement)
			{
				itemNew.Text = string.Format(itemNew.Text,"Field");
				TableElement tx = new TableElement(NodeClicked);
				NodeClone = tx.ToNode();
			}
			else if (NodeClicked.Tag is FieldElement)
			{
				FieldElement fx = new FieldElement(NodeClicked);
				NodeClone = fx.ToNode();
			}
			else if (NodeClicked.Tag is ReferenceAssemblyElement)
			{
				ReferenceAssemblyElement fx = new ReferenceAssemblyElement(NodeClicked);
				NodeClone = fx.ToNode();
			}
			else if (NodeClicked.Tag is DataViewElement)
			{
				DataViewElement fx = new DataViewElement(NodeClicked);
				NodeClone = fx.ToNode();
			}
			else {
				itemNew.Dispose();
				itemNew = null;
			}
			return itemNew;
		}
		ContextMenuStrip GetContextMenu(TreeNode node)
		{
			if (NodeContext != null) { NodeContext.Dispose(); NodeContext = null; }
			NodeContext = new ContextMenuStrip(){
				Items =
				{
					new ToolStripLabel(node.Tag.GetType().Name){Font=new Font(Font.FontFamily,11f,FontStyle.Bold,GraphicsUnit.Point)},
					GetNewT(node),
					NewItem("-"),
					new ToolStripMenuItem("Refresh",fam3.famfam_silky.arrow_refresh,eItemRefresh){Name="Refresh"},
					NewItem("-"),
					new ToolStripMenuItem("Cut",fam3.famfam_silky.cut,eItemCut){Name="Cut"},
					new ToolStripMenuItem("Copy",fam3.famfam_silky.page_copy,eItemCopy){Name="Copy"},
					new ToolStripMenuItem("Paste"){Name="Paste"},
					NewItem("-"),
					new ToolStripMenuItem("Paste Above",fam3.famfam_silky.paste_plain,eItemPasteAbove){Name="PasteAbove",Visible=HasClipboard},
					new ToolStripMenuItem("Paste Below",fam3.famfam_silky.paste_plain,eItemPasteBelow){Name="PasteBelow",Visible=HasClipboard},
					new ToolStripSeparator{Name="CCSep",Visible=HasClipboard},
					new ToolStripMenuItem("Remove",fam3.famfam_silky.delete,eItemRemove){ ShortcutKeyDisplayString = "‘d’",Name="Remove"},
				}
			};
			return NodeContext;
		}

		void MakeContextMenu(MouseEventArgs args, TreeNode node)
		{
			NodeContext = GetContextMenu(node);
			if (NodeContext!=null) {}
			Point CP = treeView1.PointToScreen(new Point(args.X,args.Y));
			NodeContext.Show(CP);
		}
		string GetElementName(object element)
		{
			if (element is FieldElement) return (element as FieldElement).DataName;
			else if (element is TableElement) return (element as TableElement).Name;
			else if (element is DatabaseElement) return (element as DatabaseElement).Name;
			else return "unknown type";
		}
		void CycleElement()
		{
			if (NodeClicked.Tag is TableElement)
				foreach (TreeNode n in NodeClicked.Nodes)
					n.Text = (n.Tag as FieldElement).DataName;
		}
		void eItemRefresh(object sender, EventArgs e)
		{
			NodeClicked.Text = GetElementName(NodeClicked.Tag);
			CycleElement();
			NodeClipboard = NodeClone;
		}
		void eItemCut(object sender, EventArgs e)
		{
			NodeClicked.TreeView.Nodes.Remove(NodeClicked);
			NodeClipboard = NodeClone;
		}
		void eItemCopy(object sender, EventArgs e)
		{
			NodeClipboard = NodeClone;
		}
		void eItemPasteAbove(object sender, EventArgs e)
		{
			NodeClicked.Parent.Nodes.Insert( NodeClicked.Index, NodeClipboard);
			NodeClipboard = null;
		}
		void eItemPasteBelow(object sender, EventArgs e)
		{
			NodeClicked.Parent.Nodes.Insert( NodeClicked.Index+1, NodeClipboard);
			NodeClipboard = null;
		}
		void eItemRemove(object sender, EventArgs e)
		{
			NodeClicked.Nodes.Remove(NodeClicked);
		}

		#endregion
		
		#endregion
		
		#region Add Event

		TreeNode CreateField(string name, System.Data.SqlDbType dbType, System.TypeCode systemType)
		{
			FieldElement newField = new FieldElement(name,dbType.ToString());
			newField.DataTypeNative = systemType.ToString();
			return newField.ToNode();
		}

		/// <summary>
		/// only supported types thus far are Int, BigInt, and String (nvarchar)
		/// </summary>
		/// <param name="type"></param>
		internal void AddSqlType(SqlDbType type) { AddSqlType(type,-1); }

		/// “if the type is not supported it creates dbnull”
		internal void AddSqlType(SqlDbType type, int len)
		{
			// george harrison: here comes the sun
			if (HasSelectedNode)
			{
				TreeNode root = SelectedNode;
				TreeNode NewNode = null;
				if (SelectedNode.Tag is FieldElement) { root = SelectedNode.Parent; }
				else if (SelectedNode.Tag is TableElement) { root = SelectedNode; }
				else { MessageBox.Show("Please select something we can add to!"); return; }
				root.Nodes.Add(NewNode = CreateField(string.Format("New {0}",type),type,GetTypeCode(type)));
				if (NewNode!=null)
				{
					(NewNode.Tag as FieldElement).MaxLength = len;
					(NewNode.Tag as FieldElement).IsNullable = true;
					root.TreeView.SelectedNode = NewNode;
				}
			}
		}
		/// if the type is not supported it returns dbnull
		TypeCode GetTypeCode(SqlDbType type)
		{
			switch (type)
			{
					case SqlDbType.Bit:			return TypeCode.Boolean;

					case SqlDbType.TinyInt:		return TypeCode.Byte;
					case SqlDbType.SmallInt:	return TypeCode.Int16;
					case SqlDbType.Int:			return TypeCode.Int32;
					case SqlDbType.BigInt:		return TypeCode.Int64;

					case SqlDbType.NVarChar:	return TypeCode.String;

					case SqlDbType.Float:		return TypeCode.Single;
					case SqlDbType.Real:		return TypeCode.Double;
					case SqlDbType.Decimal:		return TypeCode.Decimal;

					case SqlDbType.DateTime:	return TypeCode.DateTime;
					
					default: return TypeCode.DBNull;
			}
		}
		#endregion

		void eLoadDataConfiguration(object sender, EventArgs e) { Appwin.TemplateFactory.sConfig.DatabaseCollectionLoadAction(); }
		void eSaveDataConfiguration(object sender, EventArgs e) { Appwin.TemplateFactory.sConfig.DatabaseCollectionSaveAction(); }
		void eRunCombineTable(object sender, EventArgs e) { Appwin.TemplateFactory.RunTableCombineToolAction(); }
		void eRunCreateView(object sender, EventArgs e) { Appwin.TemplateFactory.RunCreateViewToolAction(); }
		
	}
}
