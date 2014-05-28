using System;
using System.Linq;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Generator.Elements;
namespace GeneratorTool.Views
{
	public class TableCopyCmd : BasicCommand
	{
		public MoxiView View {
			get;
			set;
		}

		protected override void OnExecute(object parameter)
		{
			var table = parameter as TableElement;
			if (table == null) {
				ModernDialog.ShowMessage("no field detected.", "error", MessageBoxButton.OK);
				return;
			}
			View.Model.ClipboardItem = new TableElement(table);
		}
	}
}






