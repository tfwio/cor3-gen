using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Generator.Elements;
namespace GeneratorTool.Views
{
	/// <summary>
	/// FIXME
	/// It appears this command is not working.
	/// </summary>
	public class TableCreateCmd : BasicCommand
	{
		public MoxiView View {
			get;
			set;
		}

		public override bool CanExecute(object parameter)
		{
			if (View == null || View.Model == null) return false;
			return parameter is DatabaseElement;
		}

		protected override void OnExecute(object parameter)
		{
			var database = parameter as DatabaseElement;
			if (database == null) {
				ModernDialog.ShowMessage("no field detected.", "error", MessageBoxButton.OK);
				return;
			}
			var parent = database.Parent;
			var table = new TableElement(){
				Parent=database,
				BaseClass=null,
				DbType="SQLite",
				Description=null,
				Inherits=null,
				Name="New Table",
				Fields = new List<FieldElement>()
			};
//			table.items = new List<FieldElement>();
			table.Fields.Add(
				new FieldElement(){
					Parent=table,
					DataName="id",
					DataTypeNative="Int64",
					DataType="INTEGER"
				});
			table.PrimaryKey = "id";
			database.Children.Add(table);
			View.RefreshDataTree(database);
		}
	}
}












