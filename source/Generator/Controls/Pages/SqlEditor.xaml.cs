/*
 * User: oio
 * Date: 7/17/2011
 * Time: 3:56 AM
 */
using System;
using System.Collections.Generic;
using System.Cor3.Data.Engine;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Generator.Elements.Types;
using Generator.Data;
using Generator.Service;
using Microsoft.Win32;

namespace Generator.Controls
{
	
	/// <summary>This View needs to be consolidated down to nothing but Dependency Properties, or Property values.
	/// All interaction logic should is to be moved to SqlService.</summary>
	public partial class SqlEditor : UserControl
	{
		internal string FileName;
		
		#region OpenFileDialog: MdbFileDialog
		internal Microsoft.Win32.OpenFileDialog mdbFileDialog = new Microsoft.Win32.OpenFileDialog();
		public OpenFileDialog MdbFileDialog { get { return mdbFileDialog; } }
		#endregion
		#region OpenFileDialog: SQLiteFileDialog
		internal Microsoft.Win32.OpenFileDialog sqlFileDialog = new Microsoft.Win32.OpenFileDialog();
		public OpenFileDialog SqlFileDialog { get { return sqlFileDialog; } }
		#endregion

		/// <summary>
		/// The SqlService is responsible for communicating with the database
		/// and loading data into the DataGrid.
		/// </summary>
		public SqlService sqlService = null;

		internal const string schema_lister = "schema_table";
		internal const string query_result = "query_result";

		public DataSet ds = new DataSet();
		
		#region Factory-Data

		public SqlServerDb NewSqlServerDb { get { return new SqlServerDb(serverString.connection = cbFileSelector.Text, serverString.catalog = cbNameSelector.Text); } }
		public SqlDataFactory.SQLDataContext serverString = new SqlDataFactory.SQLDataContext("VAIO\\SQLEXPRESS", "prime");
		public SqlDataFactory.SQLDataContext ServerString { get { return serverString; } set { serverString = value; } }
		
		public Access10 NewAccess12 { get { return new Access10(AccessInfo.source = cbFileSelector.Text); } }
		public AccessDataFactory.AccessDataContext AccessInfo = new AccessDataFactory.AccessDataContext(null, null);
		
		public SQLiteDataFactory.SQLiteDataContext SQLiteInfo = new SQLiteDataFactory.SQLiteDataContext(".\\db.sqlite");
		#endregion
		
		#region Selected Operation Context-Type
		/// <summary>
		/// Provides the Operation-Context
		/// <seealso cref="Generator.Elements.Types" />
		/// </summary>
		public static readonly DatabaseType defaultOperationContext = DatabaseType.OleAccess;
		/// <summary>
		/// Provides selection of the current Operation Context
		/// </summary>
		public DatabaseType OperationContext { get { return (DatabaseType)cbConnectionType.SelectedValue; } }
		/// <summary>
		/// For reasons unknkown—regard the last Operation Context used.
		/// </summary>
		public DatabaseType LastOperationContext = defaultOperationContext;
		#endregion

		internal string convertedText = string.Empty;
		internal string originalText = string.Empty;
		
		/// <summary>
		/// This provides text to and from the main SQL Editor (Avalon-Edit) Control.
		/// </summary>
		public string EditorText { get { return avalonTextEditor.Text; } set { avalonTextEditor.Text = value; } }

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			List<object> data = new List<object>();
			foreach (object o in Enum.GetValues(typeof(DatabaseType))) data.Add(o);

			data.Remove(Generator.Elements.Types.DatabaseType.ClassObject);
			data.Remove(Generator.Elements.Types.DatabaseType.OleDb);

			cbConnectionType.InvalidateVisual();
			
			cbConnectionType.ItemsSource = null;
			cbConnectionType.ItemsSource = data;
			cbConnectionType.SelectedValue = defaultOperationContext;
			cbConnectionType.SelectionChanged += SelectionChangedHandler;
		}

		/// <summary>
		/// Indirectly calls sqlService.TestConnectionAction().
		/// </summary>
		/// <param name="sen"></param>
		/// <param name="args"></param>
		public void TestHandler(object sen, RoutedEventArgs args)
		{
			sqlService.TestConnectionAction();
		}

		#region Action: (template)
		#endregion

		#region Fun(bool): ShowConvertedText
		bool showConvertedText = false;
		public bool ShowConvertedText {
			get { return showConvertedText; }
			set {
				if (ShowConvertedText) convertedText = EditorText;
				else originalText = EditorText;
				if (showConvertedText = value) EditorText = convertedText;
				else EditorText = originalText;
			}
		}
		#endregion

		#region Action: SelectionChanged
		// Database-Context-Specific
		/// <summary>
		/// toggle state driven information (show/hide controls) depending
		/// on the type of database that is active (selected).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void SelectionChangedHandler(object sender, SelectionChangedEventArgs args)
		{
			// back up the query string
			if (LastOperationContext == DatabaseType.OleAccess) AccessInfo.query = avalonTextEditor.Text;
			else if (LastOperationContext == DatabaseType.SqlServer) serverString.query = avalonTextEditor.Text;
			else if (LastOperationContext == DatabaseType.SQLite) serverString.query = avalonTextEditor.Text;
			
			// and restore the query string
			if (cbConnectionType.SelectedValue.Equals(DatabaseType.SqlServer))
			{
				avalonTextEditor.Text = serverString.query;
				btnGetMdb.Visibility = Visibility.Collapsed;
				stackTable.Visibility = Visibility.Visible;
				cbNameSelector.Visibility = Visibility.Visible;

				cbFileSelector.Text = string.IsNullOrEmpty(serverString.connection) ? "" : serverString.connection;
				cbNameSelector.Text = string.IsNullOrEmpty(serverString.catalog) ? "" : serverString.catalog;
				LastOperationContext = DatabaseType.SqlServer;
			}
			else if (cbConnectionType.SelectedValue.Equals(DatabaseType.OleAccess))
			{
				avalonTextEditor.Text = AccessInfo.query;
				btnGetMdb.Visibility = Visibility.Visible;
				stackTable.Visibility = Visibility.Collapsed;
				cbNameSelector.Visibility = Visibility.Collapsed;
				cbFileSelector.Text = AccessInfo.source == null ? "" : AccessInfo.source;
				LastOperationContext = DatabaseType.OleAccess;
			}
			sqlService.GetRootSchemaItems();
		}
		#endregion

		public void Button_Click(object sender, RoutedEventArgs e) { sqlService.SaveQueryElementAction(); }
		public void popupClicked(object sender, MouseButtonEventArgs e) { this.thepopup.IsOpen = false; }

		#region Action: MenuItemClicked (Un-Referenced action)
		public void MenuItemClicked(object o, RoutedEventArgs args)
		{
			try {
				string mode = (o as MenuItem).Header.ToString();
				// we check for an empty string for the sake of possible errors.
				if (string.IsNullOrEmpty(mode)) sqlService.GetSchemaInfo(null);
				else sqlService.GetSchemaInfo(mode);
			} catch (Exception) {

			}
		}

		#endregion

		/**
		 * Microsoft Access Specific
		 */
		public void Ace12_browseMdbFile(object sen, RoutedEventArgs args) { sqlService.Ace12_getMdb(); }


		public SqlEditor() : base()
		{
			InitializeComponent();
//			using (Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Sql.xshd"))
//				using (XmlTextReader reader = new XmlTextReader(s))
//					avalonTextEditor.SyntaxHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);

			mdbFileDialog.Filter = "Access Database (mdb/1995-1998)|*.mdb|SqlServer Database|*.mdf|All known files|*.mdb;*.mdf;*.accdb";
			sqlFileDialog.Filter = "Sql Text|*.sql";
			mConvertToggler.IsChecked = showConvertedText;
			sqlService = new SqlService(Window1.This);
		}
	}

}
