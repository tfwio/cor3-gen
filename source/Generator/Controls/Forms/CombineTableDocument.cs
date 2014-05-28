/*
 * oIo ? 12/24/2010 ? 9:42 PM
 */
using System;
using System.Collections.Generic;
using System.Cor3;
using Generator.Elements;
using Generator.Elements.Types;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Generator.Classes;

namespace Generator.Controls
{

	/// <summary>
	/// Description of DocTableConverterTool.
	/// </summary>
	public partial class CombineTablesForm : Form
	{
		internal readonly UITemplateManager Host;
//		Delegate Initialization, ToClipboard;
		DICT<string,int> DICVALS;
		
		DataSet FormContext = new DataSet("formcontext");
		DataTable dbSrc { get { return FormContext.Tables["dbSrc"]; } }
		DataTable dbDst { get { return FormContext.Tables["dbDst"]; } }
		DataTable tblSrc { get { return FormContext.Tables["tblSrc"]; } }
		DataTable tblDst { get { return FormContext.Tables["tblDst"]; } }
		DataTable fSrc { get { return FormContext.Tables["fSrc"]; } }
		DataTable fDst { get { return FormContext.Tables["fDst"]; } }

		DatabaseCollection Collection { get { return Host.SelectedCollection; } }
		enum selContext { source, destination }
		enum selType { database, table }
		string sDbSrc,sDbDst,sTblSrc,sTblDest;

		string this[selContext ctx, selType typ]
		{
			get
			{
				if (ctx== selContext.destination)
				{
					if (typ== selType.database) return sDbDst;
					return sTblDest;
				}
				if (typ== selType.database) return sDbSrc;
				return sTblSrc;
			}
			set
			{
				if (ctx== selContext.destination)
				{
					if (typ== selType.database) sDbDst = value;
					else sTblDest = value;
					return;
				}
				if (typ== selType.database) sDbSrc = value;
				else sTblSrc = value;
			}
		}

		void EnumerateTables(selContext ctx, ComboBox boxP, ComboBox box)
		{
			if (boxP.SelectedItem==null) { this[ctx, selType.database] = null; return; }
			box.DataSource = null;
			if (ctx== selContext.source)
			{
				tblSrc.Clear();
				box.DataSource = tblSrc;
			}
			else
			{
				tblDst.Clear();
				box.DataSource = tblDst;
			}
			box.DisplayMember = "name";
			foreach (TableElement table in Collection[ctx== selContext.source ? sDbSrc : sDbDst].Items)
			{
				if (ctx== selContext.source) tblSrc.Rows.Add(table.Name);
				else tblDst.Rows.Add(table.Name);
			}
		}

		void dbSrcSelected(object sender, EventArgs e)
		{
			this[selContext.source,selType.database] = (cbDataSource.SelectedItem as DataRowView)["name"] as string;
			EnumerateTables(selContext.source,cbDataSource,cbTableSrc);
		}
		void dbDstSelected(object sender, EventArgs e)
		{
			this[selContext.destination,selType.database] = (cbDataDest.SelectedItem as DataRowView)["name"] as string;
			EnumerateTables(selContext.destination,cbDataDest,cbTableDest);
		}

		void tSrcSelected(object sender, EventArgs e)
		{
			if (cbTableSrc.SelectedValue==null) { return; }
			sTblSrc = (cbTableSrc.SelectedValue as DataRowView)["name"] as string;
			listTables(selContext.source);
		}
		void tDstSelected(object sender, EventArgs e)
		{
			if (cbTableDest.SelectedValue==null) { return; }
			sTblDest = (cbTableDest.SelectedValue as DataRowView)["name"] as string;
			listTables(selContext.destination);
		}

		DataTable fTable(string tblName)
		{
			if (FormContext.Tables.Contains(tblName))
			{
				FormContext.Tables[tblName].Clear();
				FormContext.Tables.Remove(tblName);
			}
			FormContext.Tables.Add(tblName);
			FormContext.Tables[tblName].Columns.Add("name",typeof(string));
			return FormContext.Tables[tblName];
		}
		DataTable fTable(selContext ctx) { return ctx== selContext.source ? fTable("fSrc") : fTable("fDst"); }

		void listTables(selContext ctx)
		{
			if (ctx== selContext.source)
			{
				listFieldSource.DataBindings.Clear();
				listFieldSource.DataSource = null;
				fTable(ctx);
				foreach (FieldElement elm in Collection[sDbSrc][sTblSrc].Fields)
					fSrc.Rows.Add(elm.DataName);
				listFieldSource.DataSource = fSrc;
				listFieldSource.DisplayMember = "name";
				listFieldSource.Refresh();
			}
			else
			{
				listFieldDest.DataBindings.Clear();
				listFieldDest.DataSource = null;
				fTable(ctx);
				foreach (FieldElement elm in Collection[sDbDst][sTblDest].Fields)
					fDst.Rows.Add(elm.DataName);
				listFieldDest.DataSource = fDst;
				listFieldDest.DisplayMember = "name";
				listFieldDest.Refresh();
			}
			RefreshDictionary();
		}
		void RefreshDictionary()
		{
			if (DICVALS==null) DICVALS = new DICT<string,int>();
			DICVALS.Clear();
			if (!FormContext.Tables.Contains("fSrc"))
			{
				Global.statR("table didn't exist");
				return;
			}
			foreach (DataRowView row in fSrc.DefaultView)
			{
				string key = r(row,"name");
				if (!DICVALS.ContainsKey(key))
					DICVALS.Add(key, -1);
			}
			Global.statG("ADDED: {0}",DICVALS.Count);
		}
		
		string List1Field { get { return (listFieldSource.SelectedItem as DataRowView)["name"] as string; } }
		string List2Field { get { return (listFieldDest.SelectedItem as DataRowView)["name"] as string; } }
		
		void eFieldSrcSelected(object sender, EventArgs e)
		{
			if (listFieldSource.SelectedItem==null) {
				Global.statG("listFieldSource value was null?");
				return;
			}
			string key = r(listFieldSource.SelectedItem,"name");
//			Global.statG("{0} › {1}",List1Field,List2Field);
			try {
				listFieldDest.SelectedIndex = DICVALS[key];
			} catch (Exception) {
				Global.statR("couldn't add");
			}
		}
		void eFieldDstSelected(object sender, EventArgs e)
		{
			if (listFieldSource.SelectedItem==null)
			{
				Global.statG("Error Selecting from listFieldSource");
				return;
			}
			string key = r(listFieldSource.SelectedItem,"name");
			DICVALS[key] = listFieldDest.SelectedIndex;
		}

		string r(object row, string name)
		{
			if (row is DataRowView) return (row as DataRowView)[name] as string;
			return (row as DataRowView)[name] as string;
		}
		
		void ConfigDataSelectors()
		{
			ConfigDataSelections("dbSrc","name");
			ConfigDataSelections("dbDst","name");
			ConfigDataSelections("tblSrc","name");
			ConfigDataSelections("tblDst","name");
		}
		void ConfigDataSelections(string table, string field)
		{
			if (FormContext.Tables.Contains(table))
			{
				FormContext.Tables[table].Clear();
				FormContext.Tables.Remove(table);
			}
			FormContext.Tables.Add(table);
			FormContext.Tables[table].Columns.Add(field,typeof(string));
		}

		void InitializeInspection(Type property)
		{
//			PropertyInfo[] apinfo = property.GetProperties();
//			foreach (System.Reflection.PropertyInfo pinfo in apinfo)
//			{
//				pinfo.Name
//			}
		}
		void Initialize()
		{
			if (Host.SelectedCollection==null)
			{
				MessageBox.Show("you must have opened a database collection");
				return;
			}
			
			ConfigDataSelectors();
			cbDataSource.DataSource = null;
			cbDataDest.DataSource = null;
			cbTableSrc.DataSource = null;
			cbTableDest.DataSource = null;
			cbDataSource.SelectedIndex = -1;
			cbDataDest.SelectedIndex = -1;
			cbTableSrc.SelectedIndex = -1;
			cbTableDest.SelectedIndex = -1;
			
			foreach (DatabaseElement elm in Host.SelectedCollection.Databases)
			{
				dbSrc.Rows.Add(elm.Name);
				dbDst.Rows.Add(elm.Name);
			}
			
			cbDataSource.DataSource = dbSrc;
			cbDataDest.DataSource = dbDst;
			
			cbDataSource.DisplayMember = "name";
			cbDataDest.DisplayMember = "name";

			List<PropertyInfo> props = Mirror.GetProperties<FieldElement>(Mirror.CheckSetter);
			comboBox1.DataSource = props.ToArray();
			comboBox1.DisplayMember = "Name";
			comboBox2.DataSource = props.ToArray();
			comboBox2.DisplayMember = "Name";
		}
		
		public CombineTablesForm(UITemplateManager host) : base()
		{
			Host = host;
			InitializeComponent();
			Initialize();
		}

		void Button2Click(object sender, EventArgs e)
		{
			RefreshDictionary();
			Global.statB("{0}",DICVALS.Count);
			int i=0;
			foreach (string vp in DICVALS.KeyArray) DICVALS[vp] = i++;
		}
		/// <summary>
		/// <para>• Iterates through DICVALS</para>
		/// <para>•  KeyName,FieldName = checkBox.Checked ? … : … ;</para>
		/// <para>• if </para>
		/// <para>• </para>
		/// <para>• </para>
		/// </summary>
		/// <param name="dbn"></param>
		/// <param name="tbn"></param>
		void DestValues(string dbn, string tbn) // sDbDst,sTblDest
		{
			TableElement te = new TableElement();
			te.Name = "Generated Table Element";
			te.DbType = "ClassObject";
			te.Fields = new List<FieldElement>();
			foreach (KeyValuePair<string,int> vp in DICVALS)
			{
				string fstr = string.Empty;
				if (vp.Value==-1) continue;
				else if (fDst.Rows.Count <= vp.Value) continue;
				else
				{
					string refname = r(listFieldDest.Items[vp.Value],"name");
					string kname = (checkBox1.Checked) ? vp.Key : refname;
					string fname = (!checkBox1.Checked) ? vp.Key : refname;
					// 233 The Tree Of Life Mozart Concerto D# 466
					fstr = (string)fDst.Rows[vp.Value]["name"];
					FieldElement fe = new FieldElement();
					fe.DataName = Collection[dbn][tbn][kname].DataName;
					fe.DataType = Collection[dbn][tbn][kname].DataType;
					fe.DefaultValue = Collection[dbn][tbn][kname].DefaultValue;
					fe.FormatString = Collection[dbn][tbn][kname].FormatString;
					fe.IsNullable = Collection[dbn][tbn][kname].IsNullable;
					fe.IsArray = Collection[dbn][tbn][kname].IsArray;
					fe.MaxLength = Collection[dbn][tbn][kname].MaxLength;
					fe.UseFormat = Collection[dbn][tbn][kname].UseFormat;
					fe.DataTypeNative = Collection[dbn][tbn][kname].DataTypeNative;
					fe.Description = fname;
					te.Fields.Add(fe);
				}
			}
			Host.BoundWindow.treeMain.SetClipboardItem(te.ToNode());
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			if (checkBox1.Checked) DestValues(sDbSrc,sTblSrc);
			else DestValues(sDbDst,sTblDest);
		}
		
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			listFieldDest.SelectedIndex = -1;
		}
	}
}
