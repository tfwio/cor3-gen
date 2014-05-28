/* oio : 1/21/2014 9:33 AM */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using Generator.Elements;
using Generator.Elements.Types;
using Generator.Core.Markup;

namespace GeneratorTool.Views
{
	public class MyCommand : CommandBase
	{
		public ICommand Command { get; set; }
		
		protected override void OnExecute(object parameter)
		{
			// TODO: implement command execution
			if (Command != null && Command.CanExecute(parameter)) Command.Execute(parameter);
		}
		
	}
}
