/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 5/30/2011
 * Time: 3:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Generator.Elements;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using Generator.Elements.Basic;
using AvalonDock;
using Generator;
using Generator.Classes;

namespace Generator.Content
{
	/// <summary>
	/// Interaction logic for SelectionPane.xaml
	/// </summary>
	public partial class SelectionPane : DockableContent
	{
		GeneratorContext GeneratorContext;
		
//		void ClearSelection()
//		{
//			this.Context.UnbindDatabaseContext();
//			this.Context = null;
//		}
		internal void RefreshSelection()
		{
			DataContext = null;
			DataContext = GeneratorContext;
		}
		internal void Configure(Window1 context)
		{
			Logger.LogG("SelectionPane","Configure");
			this.GeneratorContext = new GeneratorContext(context);
			RefreshSelection();
		}
		
		void Find<TElement>(TElement element)
			where TElement:DataMapElement
		{
			string elementName = null;
			if (element is DatabaseElement)   elementName = (element as DatabaseElement).Name;
			else if (element is TableElement) elementName = (element as TableElement).Name;
			else if (element is FieldElement) elementName = (element as FieldElement).DataName;
			else if (element is QueryElement) elementName = (element as QueryElement).name;
			
			if (string.IsNullOrEmpty(elementName)) return;
			
			System.Windows.Forms.TreeNode[] node = GeneratorContext.win.treeMain.TreeView.Nodes.Find(elementName,true);
			foreach (System.Windows.Forms.TreeNode act in node)
			{
				if (act.Name == elementName) 
				{
					GeneratorContext.win.treeMain.NodeSelected(GeneratorContext.win.treeMain.TreeView.SelectedNode = act);
					return;
				}
			}
		}
		
		void ComboDatabaseSelectionHandler(object sender, SelectionChangedEventArgs args)
		{
			Logger.LogM("ComboDatabaseSelectionHandler","sender = {0}",sender);
			Find<DataMapElement>((sender as ComboBox).SelectedValue as DataMapElement);
		}
		
		public SelectionPane()
		{
			InitializeComponent();
			comboDatabase.SelectionChanged += ComboDatabaseSelectionHandler;
			comboTable.SelectionChanged += ComboDatabaseSelectionHandler;
			comboField.SelectionChanged += ComboDatabaseSelectionHandler;
		}
	}
}