/*
 * Created by SharpDevelop.
 * User: oIo
 * Date: 2/14/2011
 * Time: 6:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

using Generator.Parser;
using Generator.Classes;
using Generator.Core.Markup;

namespace Generator.Service
{
	public class TemplateService : GeneratorServiceRoot
	{
		
		Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog(){Filter=Messages.Filter_Xml_Template};
		Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog(){Filter=Messages.Filter_Xml_Template};
		
		#region Commands
		static public readonly ICommand CmdAddNewTemplate = new RoutedCommand();
		static public readonly ICommand CmdOpenTemplates = new RoutedCommand();
		static public readonly ICommand CmdSaveTemplates = new RoutedCommand();
		static public readonly ICommand CmdSaveTemplatesAs = new RoutedCommand();
		
		static public readonly ICommand CmdViewTemplateTable = new RoutedCommand("View Table Template",typeof(UITemplateManager),new InputGestureCollection(new InputGesture[]{ new KeyGesture(Key.F3) }));
		static public readonly ICommand CmdViewTemplateField = new RoutedCommand("View Field Template",typeof(UITemplateManager),new InputGestureCollection(new InputGesture[]{ new KeyGesture(Key.F4) }));
		static public readonly ICommand CmdRegenerateSelectedView = new RoutedCommand("Regenerate Template",typeof(UITemplateManager),new InputGestureCollection(new InputGesture[]{ new KeyGesture(Key.F5) }));
		#endregion
		
		#region Action: Templates

		/// <summary></summary>
		internal void TemplateFromUIHandler(object sender, RoutedEventArgs args) {
			this.TemplateFromUIAction();
		}
		
		/// <summary></summary>
		internal void TemplateFromUIHandlerCan(object sender, CanExecuteRoutedEventArgs args)
		{
			if (Factory.SelectedTemplateRow == null) args.CanExecute=false; else args.CanExecute=true;
		}
	
		/// <summary></summary>
		internal void TemplateToUIAction()
		{
			
			Logger.LogY("TemplateContext","TemplateToUIAction");
			
			if (Factory.SelectedTemplateRow==null)
			{
				Logger.Warn("TemplateToUI","no row");
				return;
			}
			
			Factory._editor.IsReadOnly = false;
			
			switch (Factory.SelectionType)
			{
				case TemplateType.TableTemplate:
					
					if (Factory.SelectedTemplateRow.Row[Resources.elmTpl]!=DBNull.Value)
					{
						Factory._editor.Text = Factory.SelectedTemplateRow.Row.Field<string>(Resources.elmTpl);
					}
					
					break;
				case TemplateType.FieldTemplate:
					
					if (Factory.SelectedTemplateRow.Row[Resources.itmTpl]!=DBNull.Value)
					{
						if (Factory.SelectedTemplateRow.Row.Field<string>(Resources.itmTpl)==null) return;
						Factory._editor.Text = Factory.SelectedTemplateRow.Row.Field<string>(Resources.itmTpl);
					}
					
					break;
				case TemplateType.TemplatePreview:
					
					Logger.LogG("TemplateToUI","Got to the preview section!");
					Factory.SelectedTemplate = new TableTemplate(Factory.SelectedTemplateRow.Row);
					TemplateGenerationAction();
					
					break;
			}
		}
		
		/// <summary></summary>
		internal void TemplateFromUIAction()
		{
			DataRowView drv = Factory.SelectedTemplateRow;
			Logger.LogY("TemplateContext","TemplateFromUIAction");
			switch (Factory.SelectionType)
			{
				case TemplateType.TableTemplate:
					Factory.SelectedTemplateRow.Row[Resources.elmTpl] = Factory._editor.Text;
					break;
				case TemplateType.FieldTemplate:
					Factory.SelectedTemplateRow[Resources.itmTpl] = Factory._editor.Text;
					break;
				default:
					break;
			}
		}
		
		/// <summary></summary>
		internal void TemplateModeHandler(object sender, RoutedEventArgs args) { this.SetFromCombosAction(); }
		
		private GeneratorParser parser { get;set; }
		
		/// <summary> this is the convert tactic </summary>
		internal void TemplateGenerationAction()
		{
			Logger.LogM("TemplateContext::TemplateGenerationAction","Generating Content…");
			parser = new GeneratorParser(){ Configuration=Factory, ResultAction=OnGotData  };
        	parser.RunWorkerAsync();
		}
		void OnGotData(string data)
		{
			Factory._editor.IsReadOnly = false;
			Factory._editor.Text = data;
			Factory._editor.IsReadOnly = true;
		}
		
		/// <summary></summary>
		internal void ShowTableHandler(object o, RoutedEventArgs r)
		{
			Factory.SelectionType = TemplateType.TableTemplate;
			SetFromCombosAction();
		}
		
		/// <summary></summary>
		internal void ShowFieldHandler(object o, RoutedEventArgs r)
		{
			Factory.SelectionType = TemplateType.FieldTemplate;
			SetFromCombosAction();
		}
		
		/// <summary></summary>
		internal void ShowGeneratedTemplateHandler(object o, RoutedEventArgs e)
		{
			Factory.SelectionType = TemplateType.TemplatePreview;
			SetFromCombosAction();
		}
	
		/// <summary></summary>
		internal void SaveCurrentHandler(object sender, RoutedEventArgs a)
		{
			if (Factory.SelectedTemplate==null) return;
			switch (Factory.SelectionType)
			{
				case TemplateType.TableTemplate:
					Factory.SelectedTemplate.ElementTemplate = Factory._editor.Text;
					return;
				case TemplateType.FieldTemplate:
					Factory.SelectedTemplate.ItemsTemplate = Factory._editor.Text;
					return;
				case TemplateType.TemplatePreview:
					TemplateGenerationAction();
					return;
			}
		}
	
		/// <summary></summary>
		internal void SetFromCombosAction()
		{
			Logger.LogY("SetFromCombosAction","starting");
			if (Factory._c1.SelectedValue==null)
			{
				Logger.Warn("SetFromCombos","no selected template?");
				return;
			}
			TemplateToUIAction();
		}
	
		/// <summary></summary>
		protected void AddNewTemplateAction()
		{
			if (Factory.SelectedGroupView==null)
			{
				MessageBox.Show("No templates are loaded.","Load one first…");
				return;
			}
			Factory.SelectedTemplateRow = Factory.SelectedGroupView.AddNew();
			Factory.SelectedTemplateRow.Row.AcceptChanges();
			TemplateToUIAction();
		}
		
		/// <summary></summary>
		protected void AddNewTemplateHandler(object sender, RoutedEventArgs e) { AddNewTemplateAction(); }
	
		/// <summary></summary>
		internal void LoadTemplatesAction()
		{
			Logger.LogY("TemplateContext","LoadTemplatesAction");
			LoadTemplatesFile(null);
		}
		
		/// <summary></summary>
		internal void LoadTemplatesFile(string file)
		{
			Logger.LogY("TemplateContext","LoadTemplatesFile({0})",file);
			
			string newfile = file;
			Factory.SelectedTemplate = null;
			TemplateCollection tc = null;
			
			#region Load the file
			if (file==null)
			{
				if (ofd.ShowDialog().Value) { tc=TemplateCollection.Load(newfile = ofd.FileName); }
			}
			else
			{
				tc=TemplateCollection.Load(newfile);
			}
			#endregion
			
			if (tc==null) { Logger.Warn("LoadTemplatesFile","Template Configuration found to be Null."); return; }
			
			// Refresh Bindings
			
			Factory.Templates = tc;
			Factory.ItemsTable = Factory.Templates.GetData().Tables["Templates"];
			Factory.SelectedGroupView = new DataView(
				Factory.ItemsTable,
				Factory.SelectedTemplate == null ? string.Empty:Factory.SelectedTemplate.Group,
				"Group,Alias",
				DataViewRowState.ModifiedCurrent|DataViewRowState.CurrentRows
			);
			
			Factory.InitializeCombos();
		}
		
		/// <summary></summary>
		internal void LoadTemplatesHandler(object sender, RoutedEventArgs args) { this.LoadTemplatesAction(); }
	
		/// <summary></summary>
		protected void SaveTemplatesAction()
		{
			Logger.LogY("TemplateContext","SaveTemplatesAction");
			SaveTemplatesFile(null);
		}
		
		/// <summary></summary>
		public void SaveTemplatesHandler(object sender, RoutedEventArgs args) { SaveTemplatesAction(); }
		
		/// <summary></summary>
		public void SaveTemplatesHandlerCan(object sender, CanExecuteRoutedEventArgs args) { args.CanExecute = Factory.CanSaveTemplatesProperty; }
		
		/// <summary></summary>
		protected void SaveTemplatesAsAction()
		{
			Logger.LogY("TemplateContext","SaveTemplatesAsAction");
			SaveTemplatesFile(Factory.Templates.FileLoadedOrSaved);
		}
		
		/// <summary></summary>
		protected void SaveTemplatesFile(string file)
		{
			Logger.LogY("TemplateContext","SaveTemplatesAsAction");
			
			TemplateCollection tc = new TemplateCollection(Factory.Templates,Factory.ItemsTable);
			
			// load openfiledialog filename if null
			if (file==null) {
				string fname = sfd.GetFileName();
				fname.BackupFile();
				tc.Save(fname,tc);
			}
			else
			{
				file.BackupFile();
				tc.Save(file,tc);
			}
			tc = null;
		}
		
		/// <summary></summary>
		public void SaveTemplatesAsHandler(object sender, RoutedEventArgs args) { SaveTemplatesAsAction(); }
		
		#endregion
		
		protected override void RegisterCommands(Window1 window)
		{
			//
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdAddNewTemplate,AddNewTemplateHandler));
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdOpenTemplates,LoadTemplatesHandler));
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdSaveTemplates,SaveTemplatesHandler,SaveTemplatesHandlerCan));
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdSaveTemplatesAs,SaveTemplatesAsHandler,SaveTemplatesHandlerCan));
			//
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdViewTemplateTable,ShowTableHandler));
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdViewTemplateField,ShowFieldHandler));
			BoundUIElement.CommandBindings.Add(new CommandBinding(CmdRegenerateSelectedView,ShowGeneratedTemplateHandler));
		}
		protected override void RegisterService(Window1 window)
		{
			RegisterCommands(BoundUIElement = window);
		}
		
		public TemplateService(Window1 window) : base(window) {}
		
	}
}
