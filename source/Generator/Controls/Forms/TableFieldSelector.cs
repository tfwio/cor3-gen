/*
 * User: oio
 * Date: 2/8/2013
 * Time: 5:49 PM
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Generator.Classes;
using Generator.Elements;

namespace Generator.Controls
{
	public class CombinedViewInfo
	{
		public UITemplateManager Host { get; set; }
		public List<ProvidedViewInfo> Infos {
			get { return infos; }
			set { infos = value; }
		} List<ProvidedViewInfo> infos = new List<ProvidedViewInfo>();
	}
	
	public class ProvidedViewInfo
	{
		
		public string Database { get;set; }
		
		public string Table { get;set; }
		
		public string TableAlias { get;set; }
		
		/// <summary>
		/// the Local Field ID being linked to the table-ref.
		/// </summary>
		public string LinkFromField { get;set; }
		
		/// <summary>
		/// Reference Table.
		/// </summary>
		public string LinkTable { get;set; }
		
		/// <summary>
		/// this is the “[table].[field]” being referenced.
		/// </summary>
		public string LinkToField { get;set; }
		
		/// <summary>
		/// </summary>
		public string Fields { get;set; }
	}
	
	/// <summary>Description of TableFieldSelector.</summary>
	public partial class TableFieldSelector : UserControl
	{
		UITemplateManager	Host				{ get; set; }
		DatabaseElement		SelectedDatabase	{ get { return cbDataSource.SelectedValue==null?null:cbDataSource.SelectedValue as DatabaseElement; } }
		TableElement		SelectedTable		{ get { return cbTableSrc.SelectedValue==null?null:cbTableSrc.SelectedValue as TableElement; } }
		
		public void InnerTriggerFocus(object sender, EventArgs e)
		{
			if (OuterFocus!=null)
				OuterFocus(this,EventArgs.Empty);
		}
		
		public event EventHandler OuterFocus;
		
		void Event_CheckAll(object sender, EventArgs args)
		{
			for (int i=0; i < this.listFieldSource.Items.Count; i++)
				if (!this.listFieldSource.GetItemChecked(i))
					this.listFieldSource.SetItemChecked(i,true);
		}
		
		void Event_CheckNone(object sender, EventArgs args)
		{
			for (int i=0; i < this.listFieldSource.Items.Count; i++)
				if (this.listFieldSource.GetItemChecked(i))
					this.listFieldSource.SetItemChecked(i,false);
		}
		
		void Event_CheckInvert(object sender, EventArgs args)
		{
			for (int i=0; i < this.listFieldSource.Items.Count; i++)
				this.listFieldSource.SetItemChecked(i,!this.listFieldSource.GetItemChecked(i));
		}
		
		public ProvidedViewInfo ProvidedView
		{
			get {
				return new ProvidedViewInfo {
					Database=SelectedDatabase.Name,
					Table=SelectedTable.Name,
					TableAlias = string.IsNullOrEmpty(tAlias.Text) ? "[please provide]" : tAlias.Text,
					LinkTable = tLinkOn.Text, // ref
					LinkToField = tLinkOn.Text, // ref table-alias, field
					LinkFromField = tLinkId.Text, //local
					Fields = GetFields()
				};
			}
		}
		public DataViewLink ProvidedLink
		{
			get {
				return new DataViewLink {
					Alias=string.IsNullOrEmpty(tAlias.Text) ? "[please provide]" : tAlias.Text,
					Database=SelectedDatabase.Name,
					Fields=GetFields(),
					Table=SelectedTable.Name,
					From = tLinkId.Text, //local
					On = tLinkOn.Text, // ref
				};
			}
		}
		
		public string GetFields()
		{
			List<string> items = new List<string>();
			foreach (FieldElement field in this.listFieldSource.CheckedItems)
			{
				items.Add(field.DataName);
			}
			return string.Join(",",items.ToArray());
		}
		
		List<DatabaseElement> databases =	new List<DatabaseElement>();
		List<TableElement> tables =			new List<TableElement>();
		List<FieldElement> fields =			new List<FieldElement>();
		
		public void RebaseControl(UITemplateManager manager)
		{
			Host = manager;
			foreach (DatabaseElement db in Host.SelectedCollection.Databases) databases.Add(db);
			cbDataSource.DataSource = null;
			cbDataSource.DataSource = databases;
			cbDataSource.DisplayMember = "Name";
			cbDataSource.SelectedIndexChanged += DatabaseChanged;
			RebaseTables();
		}
		
		void DatabaseChanged(object sender, EventArgs e) { RebaseTables(); }
		
		public void RebaseTables()
		{
			cbTableSrc.SelectedIndexChanged -= TableChanged;
			{
				tables.Clear();
				if (cbDataSource.SelectedValue==null) return;
				var selectedDatabase = cbDataSource.SelectedValue as DatabaseElement;
				foreach (TableElement table in selectedDatabase.Items) tables.Add(table);
				cbTableSrc.DataSource = null;
				cbTableSrc.DataSource = tables;
				cbTableSrc.DisplayMember = "Name";
			}
			this.Name = string.Format("{0}",cbDataSource.SelectedText);
			cbTableSrc.SelectedIndexChanged += TableChanged;
			
		}
		
		void TableChanged(object sender, EventArgs e) { RebaseFields(); }
		
		public void RebaseFields()
		{
			Name = string.Format("{0}.{1}",cbDataSource.SelectedText,cbTableSrc.SelectedText);
			if (this.cbTableSrc.SelectedValue==null) return;
			{
				fields.Clear();
				var selectedTable = cbTableSrc.SelectedValue as TableElement;
				foreach (FieldElement field in selectedTable.Fields) fields.Add(field);
				listFieldSource.DataSource = null;
				listFieldSource.DataSource = fields;
				listFieldSource.DisplayMember = "DataName";
			}
		}
		
		public TableFieldSelector()
		{
			InitializeComponent();
		}
	}
}
