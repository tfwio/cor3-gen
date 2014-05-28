/*
 * User: oIo
 * Date: 1/31/2011 – 1:47 AM
 */
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shell;

using Generator.Classes;
using Generator.Elements;
using Generator.Elements.Types;
using IWin = System.Windows.Forms.IWin32Window;
using OFD = Microsoft.Win32.OpenFileDialog;
using SFD = Microsoft.Win32.SaveFileDialog;
using WindowInteropHelper = System.Windows.Interop.WindowInteropHelper;

namespace Generator
{

	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Fluent.RibbonWindow, IWin
	{
		#region ICommand
		static public readonly ICommand CmdSaveLayout = new RoutedCommand();
		static public readonly ICommand CmdRestoreLayout = new RoutedCommand();
		static public readonly ICommand ConfigurationSaveCommand = new RoutedCommand();
		static public readonly ICommand ConfigurationLoadCommand = new RoutedCommand();
		#endregion
		
		internal static Window1 This;
		
		#region Winforms-WindowInteropHelper
		/// <summary>
		/// Provides access to System.Windows.Forms tactics
		/// </summary>
		readonly WindowInteropHelper windowInteropHandler = null;
		/// <summary>
		/// Provide a Windows.Forms Handle for the Window.  This feature is
		/// provided inderectly via <see cref="IWin" />.
		/// <seealso cref="IWin" />
		/// </summary>
		IntPtr IWin.Handle { get { return this.windowInteropHandler.Handle; } }
		#endregion

		public UITemplateManager TemplateFactory { get;set; }
		GeneratorConfig currentconfig { get;set; }

		#region Microsoft.Win32.Open/Save-FileDialog
		// created/destroyed with the window.
		OFD ofd_appconfig = new OFD { Filter=Messages.Filter_Generator_Config };
		SFD sfd_appconfig = new SFD { Filter=Messages.Filter_Generator_Config };
		#endregion
		
		#region Action: LoadApplicationFile
		/// <summary>
		/// Main Loader: Application-Configuration File is loaded.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void LoadApplicationFileAction(object sender, RoutedEventArgs e)
		{
			if (currentconfig!=null)
			{
				MessageBoxResult result = MessageBox.Show(
					Strings.Msg_SaveAppConfig_Title,
					Strings.Msg_SaveAppConfig,
					MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK) (ConfigurationSaveCommand as RoutedCommand).Execute(null,this);
			}
			
			bool? show = ofd_appconfig.ShowDialog();
			if (show.HasValue&&show.Value) LoadApplicationFileAction(ofd_appconfig.FileName);
			
		}
		void LoadApplicationFileAction(string theFile)
		{
			GeneratorConfig config;
			config = GeneratorConfig.Load(theFile);
			//
			if (config==null)
			{
				throw new ArgumentException ( "Configuration file is required (when loading configuration)." );
			}
			//
			Logger.LogG("current configuration is", theFile);
			currentconfig = config;
			//
			Logger.LogG("loading database configuration",config.datafile);
			TemplateFactory.sConfig.DatabaseCollectionLoadFile(config.datafile);
			htCurrentDataFile.Text = Path.GetFileName(config.datafile);
			//
			Logger.LogG("loading templates",config.templatefile);
			TemplateFactory.sTemplates.LoadTemplatesFile(config.templatefile);
			htCurrentTemplateFile.Text = Path.GetFileName(config.templatefile);
			
//			JumpList/*.GetJumpList(Application.Current).*/.AddToRecentCategory(theFile);
			JumpList.AddToRecentCategory(new JumpPath(){ CustomCategory="App-Configuration",Path=theFile});
		}

		#endregion
		#region Action: SaveApplicationFile
		/// <summary>
		/// Save the main Application-Configuration-File.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void SaveApplicationFileAction(object sender, RoutedEventArgs e)
		{
			GeneratorConfig config = new GeneratorConfig(TemplateFactory);
			bool? show = sfd_appconfig.ShowDialog();
			if (show.HasValue&&show.Value)
			{
				config.Save(sfd_appconfig.FileName,config);
			}
		}
		#endregion
		
		void ToggleViewChanged(object o, RoutedEventArgs e)
		{
			MessageBox.Show(string.Format("{0}",o));
		}

		#region Action: SqlSelectionChanged
		/// <summary>
		/// SQL UI Control Context-Changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void SqlSelectionChangedHandler(object sender, SelectionChangedEventArgs args)
		{
			// 1.	back up the query string per context (in the future, let the context handle this)
			// 		Access
			if (ucSql.LastOperationContext== DatabaseType.OleAccess) ucSql.AccessInfo.query = ucSql.avalonTextEditor.Text;
			// 		SQLServer
			else if (ucSql.LastOperationContext== DatabaseType.SqlServer) ucSql.serverString.query = ucSql.avalonTextEditor.Text;
			
			// 2.	Restore the Context-Specific QueryString
			// 		SQLServer
			if (ucSql.cbConnectionType.SelectedValue.Equals(DatabaseType.SqlServer))
			{
				ucSql.avalonTextEditor.Text = ucSql.serverString.query;
				ucSql.btnGetMdb.Visibility = Visibility.Collapsed;
				ucSql.stackTable.Visibility = Visibility.Visible;
				ucSql.cbNameSelector.Visibility = Visibility.Visible;

				ucSql.cbFileSelector.Text = string.IsNullOrEmpty(ucSql.serverString.connection)?"":ucSql.serverString.connection;
				ucSql.cbNameSelector.Text = string.IsNullOrEmpty(ucSql.serverString.catalog)?"":ucSql.serverString.catalog;
				ucSql.LastOperationContext = DatabaseType.SqlServer;
			}
			// 		Access
			else if (ucSql.cbConnectionType.SelectedValue.Equals(DatabaseType.OleAccess))
			{
				ucSql.avalonTextEditor.Text = ucSql.AccessInfo.query;
				ucSql.btnGetMdb.Visibility = Visibility.Visible;
				ucSql.stackTable.Visibility = Visibility.Collapsed;
				ucSql.cbNameSelector.Visibility = Visibility.Collapsed;
				ucSql.cbFileSelector.Text = ucSql.AccessInfo.source==null?"":ucSql.AccessInfo.source;
				ucSql.LastOperationContext = DatabaseType.OleAccess;
			}
			// 3.	Refresh The Schema Information
			GetRootSchemaItems();
		}
		#endregion

		#region Action: GetRootSchemaItems
		/// <summary>
		/// Firstly, calls on GetSchemaInfo, which obtains a Schema
		/// using SqlServer or OleDb Access Driver (Ace v12).
		/// </summary>
		void GetRootSchemaItems()
		{
			ucSql.sqlService.GetSchemaInfo(null);
			if (ucSql.ds.Tables.Count > 0)
			{
				DataView dv = ucSql.ds.Tables[0].DefaultView;
				dv.AllowNew = false;
			}
		}
		#endregion

		public Window1()
		{
			This = this;
			windowInteropHandler = new WindowInteropHelper(this);
			InitializeComponent();
		}

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			
			CommandBindings.Add(new CommandBinding(ConfigurationSaveCommand,SaveApplicationFileAction));
			CommandBindings.Add(new CommandBinding(ConfigurationLoadCommand,LoadApplicationFileAction));

			#region Try Loading Application Configuration File
			try {
				if (File.Exists("app.config")) {
					AppConfig ac = AppConfig.Load("app.config");
					AppConfig.ToWindow(this,ac);
					ac = null;
				}
			} catch {
				
			}
			#endregion
			
			ucAvalonEditor.InitializeControl(TemplateFactory = new UITemplateManager(this));
			
			treeMain.InitializeTree(this);
			
			ribbon.btnCfg.Checked += ToggleViewChanged;
			ribbon.btnTpl.Checked += ToggleViewChanged;
			ribbon.btnSql.Checked += ToggleViewChanged;

			if (App.HasGeneratorConfigurationFile)
			{
				LoadApplicationFileAction(App.Arguments[0]);
			}

		}
	}
}