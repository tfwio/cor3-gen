using System;
using System.Linq;
using System.Windows.Input;
namespace GeneratorTool.Views
{
	// based on modern-ui CommandBase?
	abstract public class BasicCommand : ICommand
	{
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		
		public void OnCanExecuteChanged() { CommandManager.InvalidateRequerySuggested(); }
		
		public virtual bool CanExecute(object parameter) { return true; }
		
		public void Execute(object parameter) { if (!this.CanExecute(parameter)) { return; } this.OnExecute(parameter); }
		
		protected abstract void OnExecute(object parameter);
	}
}
