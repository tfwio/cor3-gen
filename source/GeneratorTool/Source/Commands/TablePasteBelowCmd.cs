using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;
using Generator.Elements;
namespace GeneratorTool.Views
{
	public class TablePasteBelowCmd : BasicCommand
	{
		public MoxiView View {
			get;
			set;
		}
		
		public override bool CanExecute(object parameter)
		{
			if (View == null || View.Model == null || View.Model.ClipboardItem == null) return false;
			return View.Model.ClipboardItem is TableElement;
		}
		
		protected override void OnExecute(object parameter)
		{
			var table = parameter as TableElement;
			var parent = table.Parent;
			if (table == null) {
				ModernDialog.ShowMessage("no field detected.", "error", MessageBoxButton.OK);
				return;
			}
			int index = parent.Children.IndexOf(table)+1;
			var elm = new TableElement(View.Model.ClipboardItem as TableElement);
//			elm.Name = string.Format("{0} (copy)", elm.Name);
			elm.Parent = parent;
			parent.Insert(index, elm);
			View.RefreshDataTree(parent);
		}
	}
	
}






