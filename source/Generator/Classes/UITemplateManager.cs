#region Using
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Generator.Parser;

#if WPF4
using System.Collections.ObjectModel;
#else
using System.Collections.Generic;
#endif

using Generator.Controls;
using Generator.Elements;
using Generator.Service;
using ICSharpCode.AvalonEdit;
#endregion
namespace Generator.Classes
{
	public class UITemplateManager : TemplateManager, IUIFactory
	{
		#region Commands
		static public readonly ICommand CmdViewTemplate = new RoutedCommand("View Templates Tab",typeof(UITemplateManager),new InputGestureCollection(new InputGesture[]{ new KeyGesture(Key.T,ModifierKeys.Control) }));
		static public readonly ICommand CmdViewSql = new RoutedCommand("View SQL Tab",typeof(UITemplateManager),new InputGestureCollection(new InputGesture[]{ new KeyGesture(Key.S,ModifierKeys.Control|ModifierKeys.Shift) }));
		static public readonly ICommand CmdViewEditor = new RoutedCommand("View Table/Field Editor",typeof(UITemplateManager),new InputGestureCollection(new InputGesture[]{new KeyGesture(Key.E,ModifierKeys.Control)}));
		static public readonly ICommand CmdUpdateSelectedElement = new RoutedCommand();
		#endregion
		
		#region Properties
		
		public TemplateService sTemplates = null;
		public DatabaseService sConfig = null;
		
		#region WPF UI Elements
		
		internal readonly Window1 BoundWindow;
		System.Windows.Forms.TreeNode IUIFactory.SelectedTreeNode { get { return BoundTree.SelectedNode; } }
		internal System.Windows.Forms.TreeView BoundTree { get { return BoundWindow.treeMain.TreeView; } }
		
		/// <summary>WPF UI</summary>
		internal TemplateEditor _docTpl { get { return BoundWindow.ucAvalonEditor; } }
		/// <summary>WPF UI</summary>
		internal ComboBox _c0 { get { return BoundWindow.ribbon.comboTemplateType0; } }
		/// <summary>WPF UI</summary>
		internal ComboBox _c1 { get { return BoundWindow.ribbon.comboTemplateType1; } }
		/// <summary>WPF UI</summary>
		internal TextEditor _editor { get { return BoundWindow.ucAvalonEditor.avalonTextEditor; } }
		
		#endregion
		
		#if TREEV
		/// <summary>
		/// System.Windows.Forms.TreeView
		/// </summary>
		public DatabaseCollection SelectedCollectionTree {
			get { return new DatabaseCollection(BoundWindow.treeMain.TreeView); }
			set { SelectedCollection.ToTree(BoundWindow.treeMain.TreeView, false); }
		}
		#endif
		/// <summary>
		/// This override allows for an event to be triggered binding a
		/// call to <see cref="ComboTemplateGroupUpdated" />().
		/// </summary>
		public override string SelectedTemplateGroup {
			get { return base.SelectedTemplateGroup; }
			set {
				base.SelectedTemplateGroup = value;
				this.ComboTemplateGroupUpdated();
			}
		}

		#endregion

		internal void ComboTemplateGroupUpdated()
		{
			Logger.LogY("ComboTemplateGroupUpdated",SelectedTemplateGroup);
			
			this.ItemsTable.DefaultView.Sort = "Group,Alias";
			this.ItemsTable.DefaultView.RowFilter=string.Format("Group = '{0}'",SelectedTemplateGroup);
			this._editor.Text = string.Empty;
			
			_c0.SelectedValue = SelectedTemplateGroup;
			BoundWindow.paneSelection.comboTemplateGroup.SelectedValue = SelectedTemplateGroup;
		}

		#region Database Configuration Methods
		public override DatabaseCollection CreateConfig()
		{
			DatabaseCollection dc = base.CreateConfig();
			SelectedCollection.ToTree(BoundWindow.treeMain.TreeView,false);
			return dc;
		}
		#endregion

		#region Template Configuration Actions (Empty Region)
		#endregion
		
		#region Actions: AddField
		public void AddByteHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.TinyInt); }
		public void AddInt32Handler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.BigInt); }
		public void AddBigIntHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.Int); }
		public void AddNVarChar50Handler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.NVarChar,50); }
		public void AddNVarCharHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.NVarChar); }
		public void AddDecimalHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.Decimal); }
		public void AddFloatHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.Float); }
		public void AddDateTimeHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.DateTime); }
		public void AddBoolHandler(object sender, RoutedEventArgs args) { BoundWindow.treeMain.AddSqlType(SqlDbType.Bit); }
		#endregion
		
		#region Actions: Template
		
		/// <summary>
		/// Iterates the collection collecting all GroupNames. Udated whenever a InitializeCombos is called
		/// </summary>
		public void RefreshTemplateGroups()
		{
			Logger.LogC("template-binding","RefreshTemplateGroups()");
			
			if (this.TemplateGroups!=null)
			{
				this.TemplateGroups.Clear();
				this.TemplateGroups = null;
			}
			
			#if WPF4
			this.TemplateGroups = new ObservableCollection<string>();
			#else
			this.TemplateGroups = new List<string>();
			#endif
			
			if (this.Templates==null) return;
			if (this.ItemsTable==null) return;
			
			using (DataView view = this.ItemsTable.AsDataView())
			{
				view.Sort = "Group, Alias";
				foreach (DataRowView drv in view)
				{
					if (this.TemplateGroups.Contains(drv["Group"] as string)) continue;
					this.TemplateGroups.Add(drv["Group"] as string);
				}
			}
			
		}
		
		#endregion
		
		#region Actions: Simple
		
		#region Switch Visible Tab-Page
		
		/// <summary></summary>
		void ViewSqlHandler(object sender, RoutedEventArgs args) { BoundWindow.tabCtl.SelectedItem = BoundWindow.tabSql; }
		
		/// <summary></summary>
		void ViewTemplateHandler(object sender, RoutedEventArgs args) { BoundWindow.tabCtl.SelectedItem = BoundWindow.tabTpl; }
		
		/// <summary></summary>
		void ViewEditorHandler(object sender, RoutedEventArgs args) { BoundWindow.tabCtl.SelectedItem = BoundWindow.tabEdit; }
		
		#endregion

		/// <summary>Show the Combine-Table Form</summary>
		internal void RunTableCombineToolAction() { using (CombineTablesForm xd = new CombineTablesForm(this)) xd.ShowDialog(BoundWindow); }
		internal void RunCreateViewToolAction() { using (CombineTablesViewForm xd = new CombineTablesViewForm(this)) xd.ShowDialog(BoundWindow); }
		
		/// <summary></summary>
		void RunTableCombineToolHandler(object sender, RoutedEventArgs args)
		{
			args.Handled=true; RunTableCombineToolAction();
		}
		
		/// <summary>
		/// Begin an instance of combo box data, so the Data/UI=Model/View starting here?
		/// </summary>
		/// <remarks>this also is called by the TemplateService.LoadTemplates</remarks>
		public void InitializeCombos()
		{
			Logger.LogY("TemplateContext","InitializeCombos: RefeshGroups—called internally (next)");
			
			RefreshTemplateGroups();
			
			BoundWindow.paneSelection.comboTemplateGroup.SelectionChanged -= this.ComboSelectedTemplateGroupChangeAction;
			this._c0.SelectionChanged -= this.ComboTemplateGroupChangeAction;
			
			this._c0.DisplayMemberPath=null;
			this._c0.ItemsSource=null;
			this._c0.ItemsSource= this.TemplateGroups;
			
			this.BoundWindow.paneSelection.comboTemplateGroup.DisplayMemberPath = null;
			this.BoundWindow.paneSelection.comboTemplateGroup.ItemsSource = null;
			this.BoundWindow.paneSelection.comboTemplateGroup.ItemsSource = this.TemplateGroups;
			
			this._c0.SelectionChanged += this.ComboTemplateGroupChangeAction;
			this.BoundWindow.paneSelection.comboTemplateGroup.SelectionChanged +=this.ComboSelectedTemplateGroupChangeAction;
			this._c1.SelectionChanged -= this.ComboGroupChange1Action;
			
			this._c1.ItemsSource = null;
			this._c1.ItemsSource = this.ItemsTable.DefaultView;
			this._c1.DisplayMemberPath="Alias";
			
			this.BoundWindow.paneSelection.comboTemplateAlias.ItemsSource = null;
			this.BoundWindow.paneSelection.comboTemplateAlias.ItemsSource = this.ItemsTable.DefaultView;
			this.BoundWindow.paneSelection.comboTemplateAlias.DisplayMemberPath="Alias";

			if (this.SelectedTemplateRow!=null)
				if (this.SelectedTemplateRow["SyntaxLanguage"]!=DBNull.Value)
					this.BoundWindow.paneSelection.comboTemplateSyntaxLanguage.Text = this.SelectedTemplateRow.Row.Field<string>("SyntaxLanguage");

			this._c1.SelectionChanged += this.ComboGroupChange1Action;
		}

		/// <summary></summary>
		void RegenerateSelectedViewHandlerCan(object sender, CanExecuteRoutedEventArgs args) { if (this.SelectedTemplateRow != null) args.CanExecute=true; }

		/// <summary></summary>
		void SaveCurrentTextHandler(object sender, RoutedEventArgs e) { Global.statM("Save Current Text Triggered"); }
		
		#endregion
		
		#region Action: UpdateSelectedElement
		/// <summary>
		/// Checks to see if a SelectedTable if present, then continues to select
		/// the element via BindToData()
		/// </summary>
		void UpdateSelectedElementAction()
		{
			Logger.LogY("TemplateContext","UpdateSelectedElementAction");
			if (SelectedTable==null) return;
			else
			{
				BoundWindow.ucSpec.BindToData(this);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		internal void UpdateSelectedElementHandler(object sender, RoutedEventArgs args) { UpdateSelectedElementAction(); }
		#endregion
		
		/// <summary>
		/// A template group name had changed
		/// </summary>
		internal void ComboTemplateGroupChangeAction(object sender, SelectionChangedEventArgs args)
		{
			Logger.LogY("ComboTemplateGroupChangeAction",".");
			if (_c0.SelectedItem==null) return;
			SelectedTemplateGroup = _c0.SelectedItem as string;
		}
		
		/// <summary>
		/// A template group name had changed
		/// </summary>
		internal void ComboSelectedTemplateGroupChangeAction(object sender, SelectionChangedEventArgs args)
		{
			Logger.LogY("ComboSelectedTemplateGroupChangeAction",".");
			if (BoundWindow.paneSelection.comboTemplateGroup.SelectedItem==null) return;
			SelectedTemplateGroup = BoundWindow.paneSelection.comboTemplateGroup.SelectedItem as string;
		}

		/// <summary>A template was selected</summary>
		internal void ComboGroupChange1Action(object sender, SelectionChangedEventArgs args)
		{
			Logger.LogY("ComboGroupChange1Action","starting");
			if (_c1.SelectedItem==null) return;
			this.SelectedTemplateRow = _c1.SelectedItem as DataRowView;
			this.BoundWindow.paneSelection.comboTemplateAlias.SelectedValue = _c1.SelectedItem;
			this.sTemplates.TemplateToUIAction();
			OnUpdateTemplateContextRequest();
			OnUpdateDatabaseContextRequest();
			BoundWindow.paneSelection.headerTemplateContext.DataContext = SelectedTemplateRow;
		}

		public UITemplateManager(Window1 config) : base()
		{
			BoundWindow = config;
//			TemplateGroups = new ObservableCollection<string>();
			#if WPF4
			this.TemplateGroups = new ObservableCollection<string>();
			#else
			this.TemplateGroups = new List<string>();
			#endif
			SelectionType = TemplateType.TableTemplate;
			Templates = null;
			SelectedTemplateRow = null;
			ItemsTable = null;
			
			// NOTE: Generator will not build with a WinForms.Version.
			
			#region Commands
			//
			BoundWindow.CommandBindings.Add(new CommandBinding(CmdViewTemplate,ViewTemplateHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(CmdViewSql,ViewSqlHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(CmdViewEditor,ViewEditorHandler));
			//
			sTemplates = new TemplateService(this.BoundWindow);
			sConfig = new DatabaseService(this.BoundWindow);
			//
			BoundWindow.CommandBindings.Add(new CommandBinding(CmdUpdateSelectedElement,UpdateSelectedElementHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(CmdCombineTableTool,RunTableCombineToolHandler));
			// FIELDS
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddByteAction,AddByteHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddInt32Action,AddInt32Handler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddBigintAction,AddBigIntHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddNVarChar50Action,AddNVarChar50Handler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddNVarCharAction,AddNVarCharHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddDecimalAction,AddDecimalHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddFloatAction,AddFloatHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddDateTimeAction,AddDateTimeHandler));
			BoundWindow.CommandBindings.Add(new CommandBinding(UITemplateManager.AddBoolAction,AddBoolHandler));
			#endregion

		}

	}
}
