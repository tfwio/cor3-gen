using System;
using System.Linq;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Generator.Elements;
namespace GeneratorTool.Views
{
	public class FieldCutCmd : BasicCommand
	{
		public MoxiView View {
			get;
			set;
		}
		
		protected override void OnExecute(object parameter)
		{
			var field = parameter as FieldElement;
			if (field == null) {
				ModernDialog.ShowMessage("no field detected.", "error", MessageBoxButton.OK);
				return;
			}
			var parent = field.Parent;
			View.Model.ClipboardItem = FieldElement.Clone(field);
			parent.Fields.Remove(field);
			parent.Fields = parent.Fields;
		}
	}
	
	
	
}


