/* oio : 7/17/2011 : 3:56 AM */
using System;
using System.Collections.Generic;
using System.Cor3.Data;
using System.Cor3.Data.Engine;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using Generator.Assets;
using Generator.Parser;
using Cor3Extensions;
using Generator.Controls;
using Generator.Elements;
using Generator.Elements.Types;
using Generator.Data;
using Generator.Extensions;
using TreeNode = System.Windows.Forms.TreeNode;

namespace Generator.Service
{
	
	//Editor.thepopup is the most recent modification where the class was removed from the project.
	class SqlEditorCommands
	{
		public static ICommand ExportSqlInsertCommand = new RoutedCommand("Export SQL INSERT Statement", typeof(SqlEditor));
		public static ICommand PopupSqlSaveCommand = new RoutedCommand();
		public static ICommand ExecuteSqlCommand = new RoutedCommand("Execute SQL Command", typeof(SqlEditor), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F5) }));
		public static ICommand ExecuteSqlTables = new RoutedCommand("Execute SQL Tables", typeof(SqlEditor), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F6) }));
		public static ICommand ExecuteSqlColumns = new RoutedCommand("Execute SQL Columns", typeof(SqlEditor), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F7) }));
		public static ICommand ExecuteSqlIndex = new RoutedCommand("Execute SQL Index", typeof(SqlEditor), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F8) }));
		public static ICommand ExecuteRootInfo = new RoutedCommand("Schema Root Info", typeof(SqlEditor), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F9) }));
		public static ICommand ExecuteSqlDataTypes = new RoutedCommand("Schema Data Types", typeof(SqlEditor), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F10) }));
		public static ICommand ExecuteQSqlServerPages = new RoutedCommand();
		public static ICommand ItemClicked = new RoutedCommand();
		public static ICommand LoadSqlCommand = new RoutedCommand();
		public static ICommand TestImpl = new RoutedCommand();
		public static ICommand ToggleConvertedTextCommand = new RoutedCommand();
		public static ICommand ConvertCommand = new RoutedCommand("test popup", typeof(Window1), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.F12) }));
	}
	public class SqlService : GeneratorServiceRoot
	{
		
		SqlEditor Editor { get { return BoundUIElement.ucSql; } }
		
		public void Ace12_getMdb() { if (Editor.mdbFileDialog.ShowDialog().Value) Ace12_setFilename(Editor.mdbFileDialog.FileName); }
		public void Ace12_setFilename(string fileName)
		{
			Editor.FileName = null;
			if (System.IO.File.Exists(fileName)) Editor.FileName = fileName;
			else { Editor.AccessInfo = null; return; }
			Editor.AccessInfo.source = Editor.cbFileSelector.Text = fileName;
			GetRootSchemaItems();
			//Ace12_initialize();
		}
		
		#region QueryElement
		// NOTE: Unused
		// Database-Context-Specific
		/// <summary>
		/// This method is unused as of yet.  It is designed to assert a collection of Queries
		/// into your configuration files.
		/// </summary>
		/// <returns></returns>
		public QueryElement CreateQueryElement()
		{
			QueryElement q = null;
			if (Editor.OperationContext == DatabaseType.SqlServer) {
//				q = new QueryElement(Editor.thepopup.the.Text, (Editor.ShowConvertedText) ? Editor.originalText : Editor.avalonTextEditor.Text, Editor.ServerString.connection, Editor.ServerString.catalog);
				q.mode = SchemaExtension.db_sql2005;
			} else {
//				q = new QueryElement(Editor.thepopup.the.Text, (Editor.ShowConvertedText) ? Editor.originalText : Editor.avalonTextEditor.Text, Editor.AccessInfo.name, null);
				q.mode = SchemaExtension.ole_ace12;
			}
			return q;
		}

		// Unused
		// Tree-Dependant
		/// <summary>
		/// Query element actions are not currently implemented.
		/// </summary>
		public void SaveQueryElementAction()
		{
			QueryElement q = null;
			// checks on popup text?
			if (!string.IsNullOrEmpty(Editor.thepopup.the.Text)) {
				q = CreateQueryElement();
				if (Window1.This.TemplateFactory.SelectedCollection == null) {
					Window1.This.TemplateFactory.CreateConfig();
				}
				{
					string queryContainer = DatabaseCollection.queryContainer as string;
					bool found = false;
					TreeNode tn = null;
					
					TreeNode root = Window1.This.treeMain.TreeView.Nodes[0];
					foreach (TreeNode Node in root.Nodes) {
						if (Node.Tag == DatabaseCollection.queryContainer) {
							tn = Node;
							found = true;
						}
						//						MessageBox.Show(Node.Text);m,oky6r3eaZ [-0
					}
					if (!found) {
						tn = new TreeNode(queryContainer);
						tn.ImageKey = tn.SelectedImageKey = ImageKeyNames.SqlCollection;
						tn.Name = queryContainer;
						tn.Tag = DatabaseCollection.queryContainer;
						root.Nodes.Add(tn);
					}
					q.ToTree(tn);
				}
				//			afterloop:
				Editor.thepopup.IsOpen = false;
				//				MessageBox.Show(q.sql0);
			}
			if (q != null)
				Window1.This.TemplateFactory.SelectedQuery = q;
		}
		// Unused
		/// <summary>
		/// This method seems connected with the QueryElement actions
		/// </summary>
		/// <param name="sen"></param>
		/// <param name="args"></param>
		public void TestImplAction(object sen, RoutedEventArgs args) { Editor.thepopup.IsOpen = true; }
		#endregion

		#region Sql File
		/// <summary>
		/// Load a sql file to the main SQL Editor Control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public void LoadSqlFileAction(object sender, RoutedEventArgs args)
		{
			if (Editor.sqlFileDialog.ShowDialog().Value) {
				Editor.EditorText = System.Text.Encoding.UTF8.GetString(File.ReadAllBytes(Editor.sqlFileDialog.FileName));
			}
		}
		
		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <remarks>
		/// This method currently hides a not-shown popup!
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void SaveSQL(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("you want to save");
			Editor.thepopup.IsOpen = false;
		}
		#endregion
		
		/**
		 * The following sections (until TestConnectionAction) support the
		 * TestConnectionAction method.
		 * ——————————————————————————————————————————————————————————
		 * Create
		 * Test
		 * GetSchemaInfo
		 */
		#region Access Specific
		// Connection Tests
		public bool CheckAccess12ConnectionAction()
		{
			bool fail = false;
			//			if (cbConnectionType.SelectedValue.Equals(DatabaseType.OleAccess))
			{
				if (File.Exists(Editor.AccessInfo.source)) {
					Access10 ac10 = new Access10(Editor.cbFileSelector.Text);
					ac10.SetUser("");
					using (OleDbConnection c = ac10.Connection) {
						try {
							c.Open();
							c.Close();
						} catch (Exception) {
							fail = true;
						}
					}
					ac10 = null;
				} else
					return false;
			}
			return fail == false;
		}
		public void TestAccess12ConnectionAction()
		{
			Access10 ac10 = new Access10(Editor.cbFileSelector.Text);
			ac10.SetUser("");
			if (!CheckAccess12ConnectionAction()) MessageBox.Show(ac10.ConnectionString, "failed");
			else MessageBox.Show("connected", "OLE.Access.ACE.12 Test");
			ac10 = null;
		}
		// Schema
		public void GetAccess12SchemaInfo(string action, bool updateGrid)
		{
			ClearGridView();
			ResetDatasetAction();
			DataTable t = null;
			Access10 ac10 = Editor.NewAccess12;
			using (OleDbConnection c = ac10.Connection) {
				using (OleDbDataAdapter a = new OleDbDataAdapter()) {
					if (checkConnection(c)) {
						c.Open();
						if (string.IsNullOrEmpty(action))
							t = c.GetSchema();
						else {
							try {
								t = c.GetSchema(action);
							} catch {
							}
						}
						try {
							t.TableName = SqlEditor.schema_lister;
							Editor.ds.Tables.Add(t);
						} catch (Exception) {
						}
						c.Close();
					}
				}
			}
			if (updateGrid)
				InvalidateDatagridView(Editor.ds.Tables[SqlEditor.schema_lister]);
			ac10 = null;
		}
		#endregion
		#region SqlServer Specific
		public bool CheckSqlServerConnectionAction()
		{
			bool fail = false;
			//			if (OperationMode==DatabaseType.OleAccess)
			{
				SqlServerDb ac10 = new SqlServerDb(Editor.ServerString.connection, Editor.ServerString.catalog);
				using (SqlConnection c = ac10.Connection) {
					try {
						c.Open();
						c.Close();
					} catch (Exception) {
						fail = true;
					}
				}
				ac10 = null;
			}
			return fail == false;
		}
		public void TestSqlServerConnectionAction()
		{
			SqlServerDb ac10 = new SqlServerDb(Editor.ServerString.connection, Editor.ServerString.catalog);
			if (!CheckSqlServerConnectionAction())
				MessageBox.Show(ac10.ConnectionString, "failed");
			else
				MessageBox.Show("connected", "SqlServer Test");
			ac10 = null;
		}
		// Schema
		public void GetSqlSchemaInfo(string action, bool updateGrid)
		{
			ClearGridView();
			ResetDatasetAction();
			DataTable t = null;
			SqlServerDb sqldb = Editor.NewSqlServerDb;
			using (SqlConnection c = sqldb.Connection) {
				using (SqlDataAdapter a = new SqlDataAdapter()) {
					if (checkConnection(c)) {
						bool flag1 = false, flag2 = false;
						c.Open();
						if (string.IsNullOrEmpty(action))
							t = c.GetSchema();
						else {
							try {
								t = c.GetSchema(action);
							} catch {
								flag1 = true;
							}
						}
						try {
							t.TableName = SqlEditor.schema_lister;
							Editor.ds.Tables.Add(t);
						} catch (Exception) {
							flag2 = true;
						}
						c.Close();
						if (flag1 || flag2)
							MessageBox.Show(string.Format("an error occured {0} {1}", flag1, flag2), "sql schema");
					}
				}
			}
			if (updateGrid)
				InvalidateDatagridView(Editor.ds.Tables[SqlEditor.schema_lister]);
			sqldb = null;
		}
		#endregion

		#region Not Implemented Checks
		// FIXME: NOT IMPLEMENTED
		public bool CheckSqlite3ConnectionAction()
		{
			return true;
		}
		// ================================
		// FIXME: NOT IMPLEMENTED
		public void TestSqliteConnectionAction()
		{
			
		}
		#endregion

		#region Test Connection
		// Database-Context-Specific
		/// <summary>
		/// The following method checks either ACCESS or SQLSERVER connections
		/// for a PASS/FAIL.
		/// <para>
		/// This method is provided to the UI for the user's ability to check weather
		/// or not the provided database is working or is not.
		/// </para>
		/// <para>
		/// See also the checkConnection method.
		/// </para>
		/// </summary>
		public void TestConnectionAction()
		{
			if (Editor.OperationContext == DatabaseType.OleAccess) TestAccess12ConnectionAction();
			else if (Editor.OperationContext == DatabaseType.SqlServer) TestSqlServerConnectionAction();
		}
		#endregion

		/// <summary>
		/// completely depends on IDbConnection interface.
		/// Used by 'GetSchemaInfo' method.
		/// <para>
		/// This method is used by the GetSchemaInfo methods:
		/// GetAccess12SchemaInfo and GetSqlSchemaInfo
		/// </para>
		/// </summary>
		/// <param name="connection"></param>
		/// <returns></returns>
		public bool checkConnection(IDbConnection connection)
		{
			bool passed = true;
			try { connection.Open(); connection.Close(); }
			catch (Exception) { passed = false; }
			return passed;
		}
		
		/// <summary>
		/// Firstly, calls on GetSchemaInfo, which obtains a Schema
		/// using SqlServer or OleDb Access Driver (Ace v12).
		/// </summary>
		public void GetRootSchemaItems()
		{
			GetSchemaInfo(null);
			if (Editor.ds.Tables.Count > 0) {
				DataView dv = Editor.ds.Tables[0].DefaultView;
				//				mSchemaItems.DisplayMemberPath = ds.Tables[0].Columns[0].ColumnName;
				Editor.mSchemaItems.ItemsSource = null;
				Editor.mSchemaItems.ItemsSource = dv;
				
				dv.AllowNew = false;
			}
		}
		
		#region GetSchemaInfo()
		// Database-Context-Specific
		/// <summary>
		/// See the other overload for more info.
		/// This overload forces the Grid to update.
		/// </summary>
		/// <param name="action"></param>
		public void GetSchemaInfo(string action) { GetSchemaInfo(action, true); }
		// Database-Context-Specific
		/// <summary>
		/// This method checks the Editor.OperationContext to see
		/// weather to load Access database schema, or SqlServer schema-info.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="updateGrid"></param>
		public void GetSchemaInfo(string action, bool updateGrid)
		{
			//MessageBox.Show(action,OperationMode.ToString());
			if (Editor.OperationContext == DatabaseType.SqlServer) GetSqlSchemaInfo(action, updateGrid);
			else if (Editor.OperationContext == DatabaseType.OleAccess) GetAccess12SchemaInfo(action, updateGrid);
		}
		#endregion
		
		#region Conversion of commands such as {list-tables-[DBNAME]}
		
		/// <summary>
		/// <para>
		/// This method converts SchemaInfo provided by a DataSet to a sample
		/// Database-Configuration (XML).
		/// </para>
		/// <para>
		/// This method checks the Editor.OperationContext to see weather
		/// to load Access or SqlServer schema-information from the provided
		/// SchemaExtension.GetSqlServerSchemas and SchemaExtension.GetAccessSchemas
		/// methods.
		/// </para>
		/// <para>
		/// It then performs it's conversion routine and sends it's text to the
		/// 'converted' text vs the query that had been typed.
		/// TODO: list the possible queries.
		/// </para>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public void ConvertAction(object sender, RoutedEventArgs args)
		{
			// back up the selection's text if there is any.
			if (!Editor.ShowConvertedText) Editor.originalText = Editor.EditorText;
			
			// prepare a dataset for getting the schema
			DataSet dx = null;
			if (Editor.OperationContext == DatabaseType.SqlServer) dx = SchemaExtension.GetSqlServerSchemas(Editor.ServerString = new SqlDataFactory.SQLDataContext(Editor.cbFileSelector.Text, Editor.cbNameSelector.Text));
			else if (Editor.OperationContext == DatabaseType.OleAccess) dx = SchemaExtension.GetAccessSchemas(Editor.AccessInfo = new AccessDataFactory.AccessDataContext(Editor.cbFileSelector.Text, null));
			
			//
			if (dx == null) {
				MessageBox.Show("Unable to connect to database", "Error");
				return;
			}
			//
			//	convertedText = SqlTemplateParser.parse(originalText);
			Editor.convertedText = SqlTemplateParser.ParseSqlTemplate(Editor.originalText, dx);
			Editor.mConvertToggler.IsChecked = Editor.ShowConvertedText = true;
		}
		#endregion

		#region Actions
		public void ResetDatasetAction()
		{
			Editor.ds.Tables.Clear();
			Editor.ds.Dispose();
			Editor.ds = null;
			Editor.ds = new DataSet();
		}

		
		/// <summary>
		/// clear the UI GridView (name=“mainGrid”)
		/// </summary>
		public void ClearGridView()
		{
			Editor.mainGrid.DataContext = null;
			Editor.mainGrid.DataContext = typeof(DataView);
			Editor.mainGrid.AutoGenerateColumns = false;
			Editor.mainGrid.Columns.Clear();
		}
		
		/// <summary>
		/// we're taking a Data-Table and propagating the UI-DataGrid with the
		/// Table after a call to clear it with ‘ClearGridView()’.
		/// </summary>
		/// <param name="table"></param>
		public void InvalidateDatagridView(DataTable table)
		{
			try {
				ClearGridView();
				foreach (DataColumn col in table.Columns) {
					DataGridTextColumn colm = new DataGridTextColumn { Header = col.ColumnName };
					colm.Foreground = Brushes.Black;
					colm.Binding = new Binding("[$$$]".Replace("$$$", col.ColumnName)) {
					};
					Editor.mainGrid.Columns.Add(colm);
				}
				Editor.mainGrid.ItemsSource = null;
				Editor.mainGrid.ItemsSource = table.DefaultView;
				Editor.mainGrid.InvalidateVisual();
			} catch (Exception) {
			}
		}

		/// <summary>
		/// nothing as of yet
		/// </summary>
		public void InitializeDatabase()
		{
			
		}
		#endregion

		#region Execute SQL QUERY Action
		public void ExecuteAccessSqlAction(bool updateGrid, bool updateText)
		{
			ClearGridView(); // this is redundant, called from the InvalidateGridView function
			ResetDatasetAction();
			if (!CheckAccess12ConnectionAction())
				return;
			Access10 accessdb = Editor.NewAccess12;
			using (OleDbConnection c = accessdb.Connection) {
				using (OleDbDataAdapter a = new OleDbDataAdapter()) {
					c.Open();
					try {
						a.SelectCommand = new OleDbCommand(Editor.avalonTextEditor.Text, c);
						a.SelectCommand.ExecuteNonQuery();
						a.Fill(Editor.ds, SqlEditor.query_result);
					} catch (Exception err) {
						Logger.LogM("Error",err.ToString());
					}
					c.Close();
				}
			}
			accessdb = null;
			if (updateGrid)
				InvalidateDatagridView(Editor.ds.Tables[SqlEditor.query_result]);
			
		}
		
		/// <summary>
		/// Execute SQL-Server SQL Command
		/// <para>
		/// This SQL-SERVER specific method is responsible for more then just executing the query.
		/// It also handles conversion of the (tab-item: Sql Template) re-writing of SQL Queries.
		/// </para>
		/// </summary>
		/// <param name="updateGrid"></param>
		/// <param name="updateText"></param>
		public void ExecuteSqlServerAction(bool updateGrid, bool updateText)
		{
			ClearGridView(); // this is redundant, called from the InvalidateGridView function
//			MessageBox.Show("The selected Tab-Page is {}".Replace("{}",string.Format("{0}",Editor.tabCtl.SelectedItem)));
			ResetDatasetAction();
			if (!CheckSqlServerConnectionAction()) return;
			bool textFlag = (Editor.tabCtl.SelectedItem as TabItem).Header.ToString()=="txt";
			
			AutoWrapString = Editor.ckAutoQuote.IsChecked.Value;
			AutoWrapStringSingleQ = Editor.ckAutoQuoteSingle.IsChecked.Value;
			
			SqlServerDb sqldb = Editor.NewSqlServerDb;
			using (SqlConnection c = sqldb.Connection) {
				using (SqlDataAdapter a = new SqlDataAdapter()) {
					c.Open();
					try {
						a.SelectCommand = new SqlCommand(Editor.avalonTextEditor.Text, c);
						a.SelectCommand.ExecuteNonQuery();
						a.Fill(Editor.ds, SqlEditor.query_result);
					} catch (Exception err) {
						Logger.LogM("Error",err.ToString());
					}
					c.Close();
				}
			}
			sqldb = null;
			if (textFlag) this.ParseSqlTemplate();
			if (updateGrid) InvalidateDatagridView(Editor.ds.Tables[SqlEditor.query_result]);
		}
		
		void ParseSqlTemplate()
		{
			
			List<string> list = new List<string>();
			string rowConst = Editor.sqlRow.Text;
			foreach (DataRowView row in Editor.ds.Tables[SqlEditor.query_result].DefaultView)
			{
				string currentRow = rowConst;
				foreach (DataColumn dc in Editor.ds.Tables[SqlEditor.query_result].Columns)
				{
					currentRow = currentRow
						.Replace(dc.Caption.QCurly(),GetDataString(row[dc.ColumnName]));
				}
				list.Add(currentRow);
			}
			Editor.avalonTextEditorO.Text = string.Join("\r\n",list.ToArray());
			list.Clear();
			list = null;
		}
		
		public bool AutoWrapString;
		public bool AutoWrapStringSingleQ;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public string GetDataString(object o)
		{
			if (o == DBNull.Value) return "NULL";
			else if (o==null) return "NULL";
			else if ((o is string) && (AutoWrapString && AutoWrapStringSingleQ)) return string.Format("{0}",o).SQuote();
			else if ((o is string) && AutoWrapString) return string.Format("{0}",o).DQuote();
			else return string.Format("{0}",o);
		}
		public void ExecuteHandler(object sen, RoutedEventArgs args)
		{
			ExecuteAction(true,false);
		}
		public void ExecuteAction()
		{
			ExecuteAction(true,false);
		}
		public void ExecuteAction(bool updateGrid, bool updateText)
		{
			if (Editor.OperationContext == DatabaseType.OleAccess)
				ExecuteAccessSqlAction(updateGrid,updateText);
			else if (Editor.OperationContext == DatabaseType.SqlServer)
				ExecuteSqlServerAction(updateGrid,updateText);
		}
		#endregion

		#region Sqlite
		public void ExecuteSqliteSqlAction(bool updateGrid, bool updateText)
		{
			ClearGridView(); // this is redundant, called from the InvalidateGridView function
			ResetDatasetAction();
			if (!CheckAccess12ConnectionAction())
				return;
			Access10 accessdb = Editor.NewAccess12;
			using (OleDbConnection c = accessdb.Connection) {
				using (OleDbDataAdapter a = new OleDbDataAdapter()) {
					c.Open();
					try {
						a.SelectCommand = new OleDbCommand(Editor.avalonTextEditor.Text, c);
						a.SelectCommand.ExecuteNonQuery();
						a.Fill(Editor.ds, SqlEditor.query_result);
					} catch (Exception) {
					}
					c.Close();
				}
			}
			accessdb = null;
			if (updateGrid)
				InvalidateDatagridView(Editor.ds.Tables[SqlEditor.query_result]);
		}
		#endregion
		
		public void ExecuteIndexAction(object sen, RoutedEventArgs arg) { GetSchemaInfo(Strings.Schema_Indexes); }
		public void ExecuteDataTypesAction(object sen, RoutedEventArgs arg) { GetSchemaInfo(Strings.Schema_DataTypes); }
		public void ExecuteRootInfoHandler(object sender, RoutedEventArgs args) { GetSchemaInfo(null); }
		public void GetTablesAction(object sen, RoutedEventArgs args) { GetSchemaInfo(Strings.Schema_Tables); }
		public void GetColumnsAction(object sen, RoutedEventArgs args) { GetSchemaInfo(Strings.Schema_Columns); }

		#region SqlServer Query ? Actions
		void SqlServerQPagesAction()
		{
			
		}
		void SqlServerQPagesHandler(object sen, RoutedEventArgs args)
		{
			SqlServerQPagesAction();
		}
		#endregion
		
		public void ToggleConvertedTextAction(object sender, RoutedEventArgs args)
		{
			Editor.ShowConvertedText = !Editor.ShowConvertedText;
			Editor.mConvertToggler.IsChecked = Editor.ShowConvertedText;
		}
		
		// • Here, we would be generating a table's content to a Text window where
		// we would like to see some INSERT COMMANDs based on the selected table
		// if a table is selected.
		// • Also, content would be generated when a sql command has been generated.
		//		here, the command would contain (commented) the query, and the result
		//		in the form of an insert statement.
		public void ExportSqlInsertAction(object sender, RoutedEventArgs args)
		{
			
		}
		

		protected override void RegisterCommands(Window1 window)
		{
			base.RegisterCommands(window);
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExportSqlInsertCommand, ExportSqlInsertAction));
			
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteSqlCommand, ExecuteHandler));
			
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteRootInfo, ExecuteRootInfoHandler));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteSqlTables, GetTablesAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteSqlColumns, GetColumnsAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteSqlIndex, ExecuteIndexAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteSqlDataTypes, ExecuteDataTypesAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ExecuteQSqlServerPages, SqlServerQPagesHandler));
			
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.LoadSqlCommand, LoadSqlFileAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.TestImpl, TestImplAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ToggleConvertedTextCommand, ToggleConvertedTextAction));
			window.CommandBindings.Add(new CommandBinding(SqlEditorCommands.ConvertCommand, ConvertAction));
			
			window.InputBindings.Add(new KeyBinding(SqlEditorCommands.ConvertCommand, Key.F3, ModifierKeys.None));
			window.InputBindings.Add(new KeyBinding(SqlEditorCommands.ToggleConvertedTextCommand, Key.F2, ModifierKeys.None));
		}
		protected override void RegisterService(Window1 window)
		{
			RegisterCommands(this.BoundUIElement = window);
		}

		public SqlService(Window1 window) : base(window)
		{
		}
	}
}
