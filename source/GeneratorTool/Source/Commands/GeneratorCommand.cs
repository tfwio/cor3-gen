/* oio : 1/21/2014 9:33 AM */
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using Generator.Elements;
namespace GeneratorTool.Views
{
	public class GeneratorCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			if (CanExecuteChanged!=null) CanExecuteChanged(this, new EventArgs());
			return true;
		}

		virtual public void Execute(object parameter)
		{
		}
	}
}





