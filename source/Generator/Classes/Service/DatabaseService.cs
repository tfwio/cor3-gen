using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using Generator.Classes;
using Generator.Elements;

namespace Generator.Service
{

	public class DatabaseService : GeneratorServiceRoot
	{
		#region temp ofd

		#if WPF4
		static readonly Microsoft.Win32.OpenFileDialog ofd_dataconfig = new Microsoft.Win32.OpenFileDialog{
			Filter=Messages.Filter_Database_Config
		};
		static readonly Microsoft.Win32.SaveFileDialog sfd_dataconfig = new Microsoft.Win32.SaveFileDialog{
			Filter=Messages.Filter_Database_Config
		};
		static readonly Microsoft.Win32.OpenFileDialog ofd_templates = new Microsoft.Win32.OpenFileDialog{
			//"Xml|*.xml|xmltpl|*.xtpl|All files|*";
			Filter=Messages.Filter_Xml_Template
		};
		#else
//		static readonly System.Windows.Forms.OpenFileDialog ofd_templates = new Microsoft.Win32.OpenFileDialog{
//			Filter=Messages.Filter_Xml_Template
//		};
		#endif
		
		string GetTemplatesFile()
		{
			#if WPF4
			string fname = ofd_templates.GetFileName();
			if (!string.IsNullOrEmpty(fname)) return fname;
			#else
			if (ofd_templates.ShowDialog()==System.Windows.Forms.DialogResult.OK)
			{
				return ofd_templates.FileName;
			}
			#endif
			return null;
		}
		#endregion
		
		static public readonly ICommand CmdOpenDatabaseConfiguration = new RoutedCommand();
		static public readonly ICommand CmdCloseDatabaseConfiguration = new RoutedCommand();
		static public readonly ICommand CmdSaveDatabaseConfiguration = new RoutedCommand();
		static public readonly ICommand CmdSaveDatabaseConfigurationAs = new RoutedCommand();
		
		#region DatabaseCollectionLoadFile
		
		public void DatabaseCollectionLoadAction()
		{
			string fname = ofd_dataconfig.GetFileName();
			if (!string.IsNullOrEmpty(fname) && System.IO.File.Exists(fname))
			{
				DatabaseCollectionLoadFile(fname);
			}
		}
		
		public void DatabaseCollectionLoadFile(string file)
		{
			DatabaseCollection dbCollection = null;
			
			try { dbCollection = DatabaseCollection.Load(file);}
			catch (Exception error) {
				MessageBox.Show(error.ToString(), Strings.ErrMsg_Invalid_Input_File);
			}
			
			if (dbCollection!=null)
			{
				Factory.SelectedCollectionTree = Factory.SelectedCollection = dbCollection;
			}
			else return;
			BoundUIElement.paneSelection.Configure(BoundUIElement);
		}
		
		internal void DatabaseCollectionLoadHandler(object sender, RoutedEventArgs e) { DatabaseCollectionLoadAction(); }
		
		#endregion
		
		#region Database Configuration Actions (Load,Save)
		
		internal void DatabaseCollectionSaveAsHandler(object sender, RoutedEventArgs e) { DatabaseCollectionSaveAsAction(); }
		
		internal void DatabaseCollectionSaveAsAction()
		{
			DatabaseCollection dc = Factory.SelectedCollectionTree;
			string fname = sfd_dataconfig.GetFileName();
			if (String.IsNullOrEmpty(fname)) return;
			fname.BackupFile();
			dc.Save(fname,dc);
			dc = null;
		}
		
		#endregion
		
		#region DatabaseCollectionSave
		
		internal void DatabaseCollectionSaveHandler(object sender, RoutedEventArgs e) { DatabaseCollectionSaveAction(); }
		
		internal void DatabaseCollectionSaveAction()
		{
			DatabaseCollection dc = Factory.SelectedCollectionTree;
			if (Factory.SelectedCollection!=null)
			{
				if (Factory.SelectedCollection.FileLoadedOrSaved!=string.Empty)
				{
					string fname = Factory.SelectedCollection.FileLoadedOrSaved;
					
					fname.BackupFile();
					dc.Save(fname,dc);
					
					Factory.SelectedCollectionTree = Factory.SelectedCollection = dc;
					return;
				}
			}
			else if (dc==null) { System.Windows.Forms.MessageBox.Show("NOT GONN-DO IT!"); }
			else dc.Save(dc);
			Factory.SelectedCollection = dc;
		}
		
		internal void NewDatabaseCollectionHandler(object sender, RoutedEventArgs e) { NewDatabaseCollectionAction(); }
		
		#endregion
		
		#region NewDatabaseCollectionAction
		
		/// <summary>Creates a new collection setting the internal SelectedCollection (DatabaseCollection) initial values.</summary>
		internal void NewDatabaseCollectionAction()
		{
			Factory.SelectedCollection = new DatabaseCollection();
			Factory.SelectedCollection.Databases = new System.Collections.Generic.List<DatabaseElement>();
			Factory.SelectedCollection.Databases.Add(new DatabaseElement("New Db"));
			Factory.SelectedCollection.Databases[0].Items = new System.Collections.Generic.List<TableElement>();
			Factory.SelectedCollection.Databases[0].Items.Add(new TableElement());
			Factory.SelectedCollection.Databases[0].Items[0].Name="new table";
			Factory.SelectedCollection.Databases[0].Items[0].Fields = new System.Collections.Generic.List<FieldElement>();
			Factory.SelectedCollectionTree = Factory.SelectedCollection;
		}
		
		#endregion

		protected override void RegisterService(Window1 window)
		{
			RegisterCommands(this.BoundUIElement = window);
		}

		protected override void RegisterCommands(Window1 window)
		{
			//
			window.CommandBindings.Add(new CommandBinding(CmdOpenDatabaseConfiguration,DatabaseCollectionLoadHandler));
			window.CommandBindings.Add(new CommandBinding(CmdSaveDatabaseConfiguration,DatabaseCollectionSaveHandler));
			window.CommandBindings.Add(new CommandBinding(CmdSaveDatabaseConfigurationAs,DatabaseCollectionSaveAsHandler));
		}
		
		public DatabaseService(Window1 window) : base(window) {}
		
	}
}
