/*
 * Created by SharpDevelop.
 * User: oIo
 * Date: 2/14/2011
 * Time: 6:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Cor3.Data;
using System.Cor3.Data.Map;
using System.Cor3.Data.Map.Parser;
using System.Cor3.Data.Markup;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using AvalonEdit.Sample;
using Generator.Classes;
using Generator.Controls;
using Generator.DatabaseBindingExtension;
using Generator.Dock;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Indentation;

namespace Generator.Service
{
	public class EditorService
	{
		TempateEditorDocument editor;
		public void InitializeControl()
		{
			this.editor.Factory.BoundWindow.ribbon.comboTemplateType0.SelectionChanged += TemplateComboSelectionChangedHandler;
			editor.avalonTextEditor.InputBindings.Add(
				new KeyBinding(
					ApplicationCommands.Save,
					Key.S,
					ModifierKeys.Control)
			);
			editor.avalonTextEditor.CommandBindings.Add(
				new CommandBinding(
					ApplicationCommands.Save,
					editor.Factory.sTemplates.TemplateFromUIHandler,
					editor.Factory.sTemplates.TemplateFromUIHandlerCan)
			);
			//
			InitEntering();
			InitEntering2();
			editor.avalonTextEditor.TextChanged += TextChangedEventHandler;
			//
			this.editor.FoldingUpdateTimer = new DispatcherTimer();
			this.editor.FoldingUpdateTimer.Interval = TimeSpan.FromMilliseconds(900);
			this.editor.FoldingUpdateTimer.Tick += foldingUpdateTimer_Tick;
			this.editor.FoldingUpdateTimer.Start();
		}
		public void foldingUpdateTimer_Tick(object sender, EventArgs e)
		{
			editor.FoldingUpdateTimer.Stop();
			InitEntering1();
		}
		
		// as to be visible to the extension
		public void TextChangedEventHandler(object sender, EventArgs e)
		{
			if (editor.FoldingUpdateTimer.IsEnabled) editor.FoldingUpdateTimer.Stop();
			editor.FoldingUpdateTimer.Start();
		}
		public void InitEntering()
		{
			editor.Editor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
			editor.Editor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
		}
		public void InitEntering1()
		{
			try
			{
				if (editor.FoldingStrategy != null)
				{
					editor.FoldingStrategy.UpdateFoldings(editor.FoldingManager, editor.Editor.Document);
				}
			} catch (Exception) { }
		}
		public void InitEntering2()
		{
			if (editor.Editor.SyntaxHighlighting == null) { editor.FoldingStrategy = null; }
			else {
				switch (editor.Editor.SyntaxHighlighting.Name) {
					case "XML":
						editor.FoldingStrategy = new XmlFoldingStrategy();
						editor.Editor.TextArea.IndentationStrategy = new DefaultIndentationStrategy();
						break;
					case "C#":
					case "C++":
					case "PHP":
					case "Java":
						editor.Editor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(editor.Editor.Options);
						editor.FoldingStrategy = new Generator.AvalonEdit.Helpers.CSharpPragmaRegionFoldingStrategy();
						break;
					default:
						editor.Editor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
						editor.FoldingStrategy = null;
						break;
				}
			}
			if (editor.FoldingStrategy != null) {
				if (editor.FoldingManager == null) editor.FoldingManager = FoldingManager.Install(editor.Editor.TextArea);
				editor.FoldingStrategy.UpdateFoldings(editor.FoldingManager, editor.Editor.Document);
			} else {
				
				if (editor.FoldingManager != null) {
					FoldingManager.Uninstall(editor.FoldingManager);
					editor.FoldingManager = null;
				}
				
			}
		}

		public EditorService(TempateEditorDocument editor)
		{
			this.editor = editor;
			InitializeControl();
		}
		/// <summary>
		/// (internally) sets the selected TemplateGroup to the selected value and
		/// begins execution chain starting with OnUpdateTemplateContextRequest.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void TemplateComboSelectionChangedHandler(object sender, SelectionChangedEventArgs args)
		{
			if (editor.Factory.BoundWindow.ribbon.comboTemplateType0.SelectedValue!=null) editor.Factory.SelectedTemplateGroup =
				(string)editor.Factory.BoundWindow.ribbon.comboTemplateType0.SelectedValue;
			editor.Factory.OnUpdateTemplateContextRequest();
		}
		void UiSetAutoComplete(TextEditor control)
		{
			string[] array = null;
			if (editor.Factory.SelectionType == TemplateType.TableTemplate)
				array = TemplateFactory.ac_001.Split(';');
			else if (editor.Factory.SelectionType == TemplateType.FieldTemplate)
				array = TemplateFactory.ac_002.Split(';');
			Array.Sort(array);
			editor.CompletionWindow = new CompletionWindow(editor.avalonTextEditor.TextArea);
			IList<ICompletionData> data = editor.CompletionWindow.CompletionList.CompletionData;
			if (editor.Factory.SelectionType == TemplateType.TableTemplate)
			{
				oneline(
					data,
					NsTypes.DatabaseTypes,
					NsTypes.TableTypes,
					NsTypes.FieldTypes,
					NsTypes.AdapterTypes,
					NsTypes.Global);
				
			}
			else if (editor.Factory.SelectionType== TemplateType.FieldTemplate)
			{
				oneline(
					data,
					NsTypes.DatabaseTypes,
					NsTypes.TableTypes,
					NsTypes.FieldTypes,
					NsTypes.AdapterTypes,
					NsTypes.Global);
			}
			editor.CompletionWindow.Show();
			editor.CompletionWindow.Closed += delegate { editor.CompletionWindow = null; };
			Array.Clear(array,0,array.Length);
			array = null;
		}
		void oneline(IList<ICompletionData> data, params NsTypes[] t)
		{
			foreach (NsTypes type in t)
				foreach (string value in TemplateFactory.Group1(type))
					data.Add(
						new MyCompletionData(
							string.Format(
								TemplateFactory.fmt_field,value
							),
							MyCompletionData.UriImageFromNsType(type)));
		}
		void UiSetupAutoCompleteLists()
		{
			UiSetAutoComplete(editor.avalonTextEditor);
//			UiSetAutoComplete(richTextBox2,ac_002);
//			avalonTextEditor.IsReadOnly = true;
		}
		#region Completion Window

		internal void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
		{
			Global.statCd("TEXT ENTERED: {0}",sender);
			try {
				
				if (e.Text == "$") {
					UiSetupAutoCompleteLists();
				}
			} catch (Exception) {
//				throw;
			}
		}
		internal void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
		{
			Global.statCd("TEXT ENTERING: {0}",sender);
			try {
				
				if (e.Text.Length > 0 && editor.CompletionWindow != null) {
					if (e.Text[0]=='(') return;
					if (!char.IsLetterOrDigit(e.Text[0])) {
						// Whenever a non-letter is typed while the completion window is open,
						// insert the currently selected element.
						editor.CompletionWindow.CompletionList.RequestInsertion(e);
					}
				}
			} catch (Exception) {
			}
			// do not set e.Handled=true - we still want to insert the character that was typed
		}
		#endregion

	}
}
