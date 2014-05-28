using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using Generator.Parser;
using AvalonEdit.Sample;
using Generator.Classes;
using Generator.EditorExtension;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;

namespace Generator
{
	/// <summary>Interaction logic for UcPage1.xaml</summary>
	/// <remarks>
	/// This class could use a bit of help when it comes to showing it's content
	/// for different codes, as well as some very un-complex folding.
	/// </remarks>
	public partial class TemplateEditor : UserControl, IEditorContext
	{
		static public readonly ICommand CmdShowCompletion = new RoutedCommand();
		
		public CompletionWindow CompletionWindow { get; set; }
		public FoldingManager FoldingManager { get; set; }
		public AbstractFoldingStrategy FoldingStrategy { get; set; }
		public UITemplateManager Factory { get; set; }
		public AvalonEditor.Editor Editor { get { return avalonTextEditor; } }
		public DispatcherTimer FoldingUpdateTimer { get; set; }
		
		/// <summary>
		/// This is one of the first initialized methods in the application.
		/// </summary>
		/// <param name="config"></param>
		public void InitializeControl(UITemplateManager config)
		{
			Factory = config;

			avalonTextEditor.InputBindings.Add(new KeyBinding(ApplicationCommands.Save,Key.S,ModifierKeys.Control));
			avalonTextEditor.CommandBindings.Add(new CommandBinding(ApplicationCommands.Save,config.sTemplates.TemplateFromUIHandler,config.sTemplates.TemplateFromUIHandlerCan));

			this.Initialize_CodeCompletion_TextEnter();
			this.InitEntering2();
			this.avalonTextEditor.TextChanged += TextChangedEventHandler;
			
			this.FoldingUpdateTimer.Interval = TimeSpan.FromMilliseconds(900);
			this.FoldingUpdateTimer.Tick += this.foldingUpdateTimer_Tick;
			this.FoldingUpdateTimer.Start();
			
			Factory.BoundWindow.ribbon.comboTemplateType0.SelectionChanged += TemplateComboSelectionChangedHandler;
		}
		
		/// <summary>
		/// (internally) sets the selected TemplateGroup to the selected value and
		/// begins execution chain starting with OnUpdateTemplateContextRequest.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		void TemplateComboSelectionChangedHandler(object sender, SelectionChangedEventArgs args)
		{
			if (Factory.BoundWindow.ribbon.comboTemplateType0.SelectedValue!=null) Factory.SelectedTemplateGroup =
				(string)Factory.BoundWindow.ribbon.comboTemplateType0.SelectedValue;
			Factory.OnUpdateTemplateContextRequest();
		}
		
		public TemplateEditor()
		{
//			RegisterHelper.Registerhelper();
			InitializeComponent();
			FoldingUpdateTimer = new DispatcherTimer();
		}

		#region Folding
		
		// this method of operation is probably from a quick snipit demo of avalonedit.
		// note that extensive changes have been built into the visualeditor application.
		// … and that we should probably re-design the editor-control.
		// interestingly, it seems that visualeditor's use of avalonedit was stripped from here.
		void foldingUpdateTimer_Tick(object sender, EventArgs e)
		{
			FoldingUpdateTimer.Stop();
			this.InitEntering1();
		}
		
		#endregion

		#region Completion Window
		
		/// <summary>
		/// (Group 1) Adds completion-items to the active code-editor.
		/// <para>
		/// Using the MyCompletionData Class-Object, the items are associated
		/// with an Image.
		/// </para>
		/// <para>
		/// In the future, each element type should be stored in a resource
		/// or database complete with descriptions of each tag including documentation
		/// and/or an example of what type of data is returned for a specific template
		/// tag.
		/// </para>
		/// </summary>
		/// <param name="data"></param>
		/// <param name="t"></param>
		void oneline1(IList<ICompletionData> data, params NsTypes[] t)
		{
			foreach (NsTypes type in t)
				foreach (string value in TemplateFactory.Group1(type))
					data.Add(
						new MyCompletionData(
							string.Format(TemplateFactory.fmt_field,value),
							MyCompletionData.UriImageFromNsType(type)
						)
					);
		}
		
		/// <summary>
		/// (Group 1) Adds completion-items to the active code-editor.
		/// <para>
		/// Using the MyCompletionData Class-Object, the items are associated
		/// with an Image.
		/// </para>
		/// <para>
		/// In the future, each element type should be stored in a resource
		/// or database complete with descriptions of each tag including documentation
		/// and/or an example of what type of data is returned for a specific template
		/// tag.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="t"></param>
		void oneline2(IList<ICompletionData> data, params NsTypes[] t)
		{
			foreach (NsTypes type in t)
				foreach (string value in TemplateFactory.Group2(type))
					data.Add(
						new MyCompletionData(
							string.Format(TemplateFactory.fmt_field,value),
							MyCompletionData.UriImageFromNsType(type)
						)
					);
		}

		void UiSetAutoComplete(TextEditor control)
		{
//			string[] array = null;
//			if (Factory.SelectionType == TemplateType.TableTemplate) array = TemplateFactory.ac_001.Split(';');
//			else if (Factory.SelectionType == TemplateType.FieldTemplate) array = TemplateFactory.ac_002.Split(';');
//			Array.Sort(array);
			CompletionWindow = new CompletionWindow(avalonTextEditor.TextArea);
			IList<ICompletionData> data = CompletionWindow.CompletionList.CompletionData;
			if (Factory.SelectionType == TemplateType.FieldTemplate)
			{
				oneline1(
					data,
					NsTypes.DatabaseTypes,
					NsTypes.TableTypes,
//					NsTypes.FieldTypes,
					NsTypes.AdapterTypes,
					NsTypes.Global);
			}
			else if (Factory.SelectionType== TemplateType.TableTemplate)
			{
				oneline1(
					data,
					NsTypes.DatabaseTypes,
					NsTypes.TableTypes,
					NsTypes.FieldTypes,
					NsTypes.AdapterTypes,
					NsTypes.Global);
			}
			CompletionWindow.Show();
			CompletionWindow.Closed += delegate { CompletionWindow.Close(); CompletionWindow = null; };
//			Array.Clear(array,0,array.Length);
//			array = null;
		}
		
		void UiSetupAutoCompleteLists()
		{
			UiSetAutoComplete(avalonTextEditor);
//			UiSetAutoComplete(richTextBox2,ac_002);
//			avalonTextEditor.IsReadOnly = true;
		}

		internal void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
		{
			Global.statCd("TEXT ENTERED: {0}",sender);
			try {
				if (e.Text == "$") {
					UiSetupAutoCompleteLists();
					#region Clipping
//					// open code completion after the user has pressed dot:
//					completionWindow = new CompletionWindow(this.avalonTextEditor.TextArea);
//					// provide AvalonEdit with the data:
//					IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
//					if (Factory.SelectionType== TemplateType.TableTemplate)
//					{
//						data.Clear();
//						data.Add(new MyCompletionData("$(TableTemplate:NAME)"));
//						data.Add(new MyCompletionData("$(FieldValues)"));
//						data.Add(new MyCompletionData("$(FieldValues)"));
//						data.Add(new MyCompletionData("$(FieldValues,Cdf)"));
//						data.Add(new MyCompletionData("$(FieldValuesNK)"));
//						data.Add(new MyCompletionData("$(FieldValuesNK,Cdf)"));
//						data.Add(new MyCompletionData("$(DbType)"));
//						data.Add(new MyCompletionData("$(FriendlyName)"));
//						data.Add(new MyCompletionData("$(TableName)"));
//						data.Add(new MyCompletionData("$(TableNameC)"));
//						data.Add(new MyCompletionData("$(TableNameClean)"));
//						data.Add(new MyCompletionData("$(tablenameclean)"));
//						data.Add(new MyCompletionData("$(TableNameCClean)"));
//						data.Add(new MyCompletionData("$(TableType)"));
//						data.Add(new MyCompletionData("$(tabletype)"));
//						data.Add(new MyCompletionData("$(PK)"));
//						data.Add(new MyCompletionData("$(pk)"));
//						data.Add(new MyCompletionData("$(PrimaryKey)"));
//						data.Add(new MyCompletionData("$(AdapterNs)"));
//						data.Add(new MyCompletionData("$(AdapterT)"));
//						data.Add(new MyCompletionData("$(AdapterNsT)"));
//						data.Add(new MyCompletionData("$(CommandNs)"));
//						data.Add(new MyCompletionData("$(CommandT)"));
//						data.Add(new MyCompletionData("$(CommandNsT)"));
//						data.Add(new MyCompletionData("$(ConnectionT)"));
//						data.Add(new MyCompletionData("$(ConnectionNsT)"));
//						data.Add(new MyCompletionData("$(ConnectionNs)"));
//						data.Add(new MyCompletionData("$(ReaderNs)"));
//						data.Add(new MyCompletionData("$(ReaderT)"));
//						data.Add(new MyCompletionData("$(ReaderNsT)"));
					////						data.Sort();
//					}
//					else
//					{
//						data.Clear();
//						data.Add(new MyCompletionData("$(Date)"));
//						data.Add(new MyCompletionData("$(Time)"));
//						data.Add(new MyCompletionData("$(DateTime)"));
//						data.Add(new MyCompletionData("$(CleanName)"));
//						data.Add(new MyCompletionData("$(FriendlyName)"));
//						data.Add(new MyCompletionData("$(FriendlyNameC)"));
//						data.Add(new MyCompletionData("$(FieldIndex)"));
//						data.Add(new MyCompletionData("$(DataType)"));
//						data.Add(new MyCompletionData("$(datatype)"));
//						data.Add(new MyCompletionData("$(DataTypeNative)"));
//						data.Add(new MyCompletionData("$(datatypenative)"));
//						data.Add(new MyCompletionData("$(DataName)"));
//						data.Add(new MyCompletionData("$(dataname)"));
//						data.Add(new MyCompletionData("$(DataNameC)"));
//						data.Add(new MyCompletionData("$(FormatString)"));
//						data.Add(new MyCompletionData("$(MaxLMAX)"));
//						data.Add(new MyCompletionData("$(nmax)"));
//						data.Add(new MyCompletionData("$(smax)"));
//						data.Add(new MyCompletionData("$(MaxLength)"));
//						data.Add(new MyCompletionData("$(IsString)"));
//						data.Add(new MyCompletionData("$(IsBool)"));
//						data.Add(new MyCompletionData("$(IsNum)"));
//						data.Add(new MyCompletionData("$(Native)"));
//						data.Add(new MyCompletionData("$(max)"));
//						data.Add(new MyCompletionData("$(UseFormat)"));
//						data.Add(new MyCompletionData("$(IsNullable)"));
//						data.Add(new MyCompletionData("$(Description)"));
//						data.Add(new MyCompletionData("$(DefaultValue)"));
//
//						data.Add(new MyCompletionData("$(TableName)"));
//						data.Add(new MyCompletionData("$(TableNameC)"));
//						data.Add(new MyCompletionData("$(TableNameClean)"));
//						data.Add(new MyCompletionData("$(tablenameclean)"));
//						data.Add(new MyCompletionData("$(TableNameCClean)"));
//						data.Add(new MyCompletionData("$(TableType)"));
//						data.Add(new MyCompletionData("$(tabletype)"));
//						data.Add(new MyCompletionData("$(PK)"));
//						data.Add(new MyCompletionData("$(pk)"));
//						data.Add(new MyCompletionData("$(PrimaryKey)"));
//						data.Add(new MyCompletionData("$(AdapterNs)"));
//						data.Add(new MyCompletionData("$(AdapterT)"));
//						data.Add(new MyCompletionData("$(AdapterNsT)"));
//						data.Add(new MyCompletionData("$(CommandNs)"));
//						data.Add(new MyCompletionData("$(CommandT)"));
//						data.Add(new MyCompletionData("$(CommandNsT)"));
//						data.Add(new MyCompletionData("$(ConnectionT)"));
//						data.Add(new MyCompletionData("$(ConnectionNsT)"));
//						data.Add(new MyCompletionData("$(ConnectionNs)"));
//						data.Add(new MyCompletionData("$(ReaderNs)"));
//						data.Add(new MyCompletionData("$(ReaderT)"));
//						data.Add(new MyCompletionData("$(ReaderNsT)"));
//					}
//					completionWindow.Show();
//					completionWindow.Closed += delegate {
//						completionWindow = null;
//					};
					#endregion
				}
			} catch (Exception) {
//				throw;
			}
		}
		
		internal void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
		{
			Global.statCd("TEXT ENTERING: {0}",sender);
			try {
				
				if (e.Text.Length > 0 && CompletionWindow != null) {
					if (e.Text[0]=='(') return;
					if (!char.IsLetterOrDigit(e.Text[0])) {
						// Whenever a non-letter is typed while the completion window is open,
						// insert the currently selected element.
						CompletionWindow.CompletionList.RequestInsertion(e);
					}
				}
			} catch (Exception) {
			}
			// do not set e.Handled=true - we still want to insert the character that was typed
		}
		
		#endregion

		internal void TextChangedEventHandler(object sender, EventArgs e)
		{
			if (FoldingUpdateTimer.IsEnabled) FoldingUpdateTimer.Stop();
			FoldingUpdateTimer.Start();
		}
	}
}