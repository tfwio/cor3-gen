/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 1/31/2012
 * Time: 6:57 AM
 */
using System;
using System.Linq;
using System.Windows;

namespace GeneratorTool
{
	public class WindowStyleChanged : EventArgs
	{
		public WindowStyle WindowStyle { get; set; }
		public WindowStyleChanged(WindowStyle s)
		{
			this.WindowStyle = s;
		}
		public WindowStyleChanged(Window w)
		{
			this.WindowStyle = w.WindowStyle;
		}
	}
}
