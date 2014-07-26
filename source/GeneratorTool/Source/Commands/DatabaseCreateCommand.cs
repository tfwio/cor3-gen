using System;
using System.Linq;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Generator.Elements;
namespace GeneratorTool.Views
{
	/// <summary>
	/// The content here was taken from a table-copy command.
	/// It is merely temporary.
	/// </summary>
	public class DatabaseCreateCommand : BasicCommand
	{
		public MoxiView View {
			get;
			set;
		}

		public override bool CanExecute(object parameter)
		{
			if (View == null || View.Model == null || View.Model.ClipboardItem == null)
				return false;
			return View.Model.ClipboardItem is TableElement;
		}

		protected override void OnExecute(object parameter)
		{
			var table = parameter as TableElement;
			if (table == null) {
				ModernDialog.ShowMessage("no field detected.", "error", MessageBoxButton.OK);
				return;
			}
			//	else ModernDialog.ShowMessage(parameter.ToString(), "Good!", MessageBoxButton.OK);
			var parent = table.Parent;
			int index = parent.Children.IndexOf(table);
			var elm = new TableElement(View.Model.ClipboardItem as TableElement);
			//			elm.Name = string.Format("{0} (copy)", elm.Name);
			elm.Parent = parent;
			parent.Insert(index, elm);
			View.RefreshDataTree(parent);
		}
	}
	
}










