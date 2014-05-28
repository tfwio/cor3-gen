using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

namespace GeneratorTool.Views
{
		
	public class MuiMessageCommand : BasicCommand
	{
		static public readonly MuiMessageCommand ShowMessageCommand = new MuiMessageCommand{};
		
		public string TestParameter { get; set; }
		
		protected override void OnExecute(object parameter) {
			ModernDialog.ShowMessage(string.Format("{{ Null={0}, Value: {1} }}", parameter==null, parameter),"Message Title",MessageBoxButton.OK);
		}
	}
}
