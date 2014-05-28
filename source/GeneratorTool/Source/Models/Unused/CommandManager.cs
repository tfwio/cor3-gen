/* oio : 1/31/2012 : 6:57 AM */
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GeneratorTool
{
	public abstract class CommandManager<TContainer> where TContainer:FrameworkElement
	{
		internal TContainer Container { get;set; }
		internal CommandBindingCollection Commands;
		
		protected internal CommandManager(TContainer container)
		{
			this.Container = container;
			this.Commands = new CommandBindingCollection();
			this.InitializeCommands();
			this.AttachCommands();
		}
		
		abstract public void InitializeCommands();
		
		virtual public void AttachCommands()
		{
			foreach (CommandBinding binding in Commands)
			{
				if (!this.Container.CommandBindings.Contains(binding))
					this.Container.CommandBindings.Add(binding);
			}
		}
		virtual public void RemoveCommands()
		{
			foreach (CommandBinding binding in Commands)
			{
				if (this.Container.CommandBindings.Contains(binding))
					this.Container.CommandBindings.Remove(binding);
			}
		}
	}
}
