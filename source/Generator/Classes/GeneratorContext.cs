#region Using
/*
 * Created by SharpDevelop.
 * User: oIo
 * Date: 2/10/2011
 * Time: 9:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;

using Generator.Export;
using Generator.Elements;
using Generator.Core.Markup;

#if WPF4
using System.Collections.ObjectModel;
#else
using System.Collections.Generic;
#endif
#endregion
namespace Generator.Classes
{
	public interface IGeneratorService<TUIElement> where TUIElement:Window { TUIElement BoundUIElement { get; } }
	public interface IDatabaseService<TUIElement> where TUIElement:UIElement { }
	public class GeneratorContextClass<TUIControl> : IGeneratorContext where TUIControl:System.Windows.Window
	{
		internal readonly TUIControl win;
		internal IFactory iconfig = null;
		//public bool UseNamespaces { get;set; }
		
		#region Database Context
		public DatabaseCollection DataCollection { get; set; }
		#if WPF4
		public ObservableCollection<DatabaseElement> Databases { get; set; }
		public ObservableCollection<TableElement> Tables { get; set; }
		public ObservableCollection<FieldElement> Fields { get; set; }
		#else
		public List<DatabaseElement> Databases { get; set; }
		public List<TableElement> Tables { get; set; }
		public List<FieldElement> Fields { get; set; }
		#endif
		#endregion
		
		#region Template Context
		public TemplateCollection TemplateCollection { get; set; }
		#if WPF4
		public ObservableCollection<string> TemplateGroups { get; set; }
		public ObservableCollection<TableTemplate> Templates { get; set; }
		#else
		public List<string> TemplateGroups { get; set; }
		public List<TableTemplate> Templates { get; set; }
		#endif
		#endregion
		
		#region ASMREF
		#if ASMREF
		public string AssemblySectionName { get; set; }
		public ObservableCollection<ReferenceAssemblyElement> AssemblyReferences { get; set; }
		#endif
		#endregion
		
		public GeneratorContextClass(TUIControl win)
		{
			this.win = win;
		}
	}
	public class GeneratorContext : GeneratorContextClass<Window1>
	{
		public void BindDatabase() { this.BindDatabaseContext(win.TemplateFactory); }
		public void BindTemplates() { this.BindTemplatesContext(win.TemplateFactory); }

		#region BindTemplates
		
		/// <summary>Apply Template Confguration to the View</summary>
		public void BindTemplatesContextAction()
		{
			Logger.LogY("template-binding","BindTemplatesContext()");

			// stop any event triggers
			this.win.TemplateFactory.UpdateTemplateContextRequest -= this.TemplateContextChangedHandler;
			if (this.iconfig.Templates==null)
			{
				Logger.Warn("template-binding","context.templates = null;");
				Logger.Warn("                ","nothing to do;");
				return;
			}
			if (this.TemplateCollection==null)
			{
				Logger.Warn("template-binding","collection.TemplateCollection was null");
				return;
			}
			#if WPF4
			this.Templates = new ObservableCollection<TableTemplate>(this.TemplateCollection.Templates);
			#else
			this.Templates = new List<TableTemplate>(this.TemplateCollection.Templates);
			#endif
			this.win.TemplateFactory.RefreshTemplateGroups();
			this.TemplateCollection = this.iconfig.Templates;
			
			Logger.Log(MessageType.White,"	","BindTemplatesContext()");
			this.win.paneSelection.comboTemplateAlias.ItemsSource = null;
			this.win.paneSelection.comboTemplateGroup.ItemsSource = null;
			#if WPF4
			this.win.paneSelection.comboTemplateAlias.ItemsSource = this.Templates = (this.iconfig.Templates!=null) ? new ObservableCollection<TableTemplate>(this.iconfig.Templates.Templates) : null;
			#else
			this.win.paneSelection.comboTemplateAlias.ItemsSource = this.Templates = (this.iconfig.Templates!=null) ? new List<TableTemplate>(this.iconfig.Templates.Templates) : null;
			#endif
			this.win.paneSelection.comboTemplateGroup.ItemsSource = this.TemplateGroups;

			this.win.paneSelection.comboTemplateAlias.SelectedValue = this.iconfig.SelectedTemplate;
			this.win.paneSelection.comboTemplateSyntaxLanguage.Text = this.iconfig.SelectedTemplate.SyntaxLanguage;
			
			#if ASMREF
			AssemblyReferences = new ObservableCollection<ReferenceAssemblyElement>(collection.Assemblies.Assemblies);
			#endif
			
			// restart any event triggers
			this.win.TemplateFactory.UpdateTemplateContextRequest += this.TemplateContextChangedHandler;
		}
		
		/// <summary>
		/// Apply Template Confguration to the View
		/// </summary>
		public void BindTemplatesContext(IUIFactory config)
		{
			this.iconfig = config;
			
			this.win.paneSelection.buttonAddTemplate.Click -= this.win.TemplateFactory.AddNewTemplateHandler;
			
			this.win.TemplateFactory.UpdateTemplateContextRequest -= this.TemplateContextChangedHandler;
			
			Logger.LogY("template-binding","BindTemplatesContext(IUIFactory)");
			
			this.iconfig = config;
			
			this.win.TemplateFactory.UpdateTemplateContextRequest += this.TemplateContextChangedHandler;
			
			this.BindTemplatesContextAction();
			
			this.win.paneSelection.buttonAddTemplate.Click += this.win.TemplateFactory.AddNewTemplateHandler;
		}

		public void TemplateContextChangedHandler(object sender, EventArgs args)
		{
			Logger.LogM("template-binding","Handler");
			this.BindTemplatesContext(this.win.TemplateFactory);
		}

		#endregion
		
		#region BindDatabase
		
		internal void UnbindDatabaseContext()
		{
			Logger.LogY("database-binding","Unbind()");
			this.win.TemplateFactory.UpdateDatabaseContextRequest -= this.DatabaseContextChangedHandler;
			this.DataCollection = null;
			this.Databases = null;
			this.Tables = null;
			this.Fields = null;
		}
		internal void BindDatabaseContext(IUIFactory collection1)
		{
			this.win.TemplateFactory.UpdateDatabaseContextRequest -= this.DatabaseContextChangedHandler;
			Logger.LogY("database-binding","BindDatabaseContext(IFactory)");
			this.iconfig = collection1;
			this.win.TemplateFactory.UpdateDatabaseContextRequest += this.DatabaseContextChangedHandler;
			this.BindDatabaseContext();
		}
		
		/// <summary>
		/// Apply Database Confguration to the View
		/// <para>Set paneSelection-comboDatabase, comboTable, comboField ItemsSource.SekectedValue</para>
		/// </summary>
		/// <remarks>Notice that each ItemsSource property is set/binding on a new List or ObservableCollection.</remarks>
		internal void BindDatabaseContext()
		{
			Logger.LogY("DatabaseCollectionConveter","BindDatabaseContext()");
			// stop any event triggers
			this.win.TemplateFactory.UpdateDatabaseContextRequest -= this.DatabaseContextChangedHandler;

			if (this.iconfig.SelectedCollection==null) return;
			if (this.iconfig.SelectedCollection.Databases==null) return;

			this.DataCollection = this.iconfig.SelectedCollection;
//			UseNamespaces = collection.UseNamespaces;
			this.win.paneSelection.comboDatabase.ItemsSource = null;
			this.win.paneSelection.comboTable.ItemsSource = null;
			this.win.paneSelection.comboField.ItemsSource = null;
			
			#region PRAGMA DEF: 'WPF4' vs ''
			#if WPF4
			this.win.paneSelection.comboDatabase.ItemsSource = this.Databases = (this.iconfig.SelectedCollection.Databases!=null) ? new ObservableCollection<DatabaseElement>(this.iconfig.SelectedCollection.Databases) : null;
			this.win.paneSelection.comboTable.ItemsSource = this.Tables = (this.iconfig.SelectedDatabase!=null) ? new ObservableCollection<TableElement>(this.iconfig.SelectedDatabase.Items) : null;
			this.win.paneSelection.comboField.ItemsSource = this.Fields = (this.iconfig.SelectedTable!=null) ? new ObservableCollection<FieldElement>(this.iconfig.SelectedTable.Fields) : null;
			#else
			this.win.paneSelection.comboDatabase.ItemsSource = this.Databases = (this.iconfig.SelectedCollection.Databases!=null) ? new List<DatabaseElement>(this.iconfig.SelectedCollection.Databases) : null;
			this.win.paneSelection.comboTable.ItemsSource = this.Tables = (this.iconfig.SelectedDatabase!=null) ? new List<TableElement>(this.iconfig.SelectedDatabase.Items) : null;
			this.win.paneSelection.comboField.ItemsSource = this.Fields = (this.iconfig.SelectedTable!=null) ? new List<FieldElement>(this.iconfig.SelectedTable.Fields) : null;
			#endif
			#endregion
			
			this.win.paneSelection.comboDatabase.SelectedValue = this.iconfig.SelectedDatabase;
			this.win.paneSelection.comboTable.SelectedValue = this.iconfig.SelectedTable;
			this.win.paneSelection.comboField.SelectedValue = this.iconfig.SelectedField;
			
//			AssemblyReferences = new ObservableCollection<ReferenceAssemblyElement>(collection.Assemblies.Assemblies);
			// restart any event triggers
			this.win.TemplateFactory.UpdateDatabaseContextRequest += this.DatabaseContextChangedHandler;
		}
		
		void DatabaseContextChangedHandler(object sender, EventArgs args)
		{
			Logger.LogY("database-binding","Handler");
			this.BindDatabaseContext(this.win.TemplateFactory);
		}
		
		#endregion
		
		public GeneratorContext(Window1 win) : base(win)
		{
			this.BindDatabase();
			this.BindTemplates();
		}
	}
}
