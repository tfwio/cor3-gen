/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 1/31/2012
 * Time: 6:57 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;

namespace System.Cor3.GeneratorTool
{
	public class LocalWindowManager
	{
		readonly Stack<WindowState> states = new Stack<WindowState>();
		readonly Stack<WindowStyle> styles = new Stack<WindowStyle>();
		
//		public event EventHandler<WindowStyleChanged> WindowStyleChanged;
		
		protected readonly Window win;
//		public WindowState State { get { return this.win.WindowState; } set { this.win.WindowState = value; } }
//		public WindowStyle WindowStyle { get { return this.win.WindowStyle; } set { WindowStateChangedHandler(this,EventArgs.Empty); } }
		
//		virtual protected void WindowStateChangedHandler(object o, EventArgs a)
//		{
//			this.states.Push(this.win.WindowState);
//		}
//		virtual protected void OnStyleChanged(WindowStyleChanged e)
//		{
//			if (WindowStyleChanged!=null) WindowStyleChanged(this,e);
//			this.styles.Push(this.win.WindowStyle);
//		}
		
//		virtual protected void Boo(object sender, EventArgs a) {}
		public LocalWindowManager(Window win)
		{
			this.win = win;
//			this.win.StateChanged += new EventHandler(WindowStateChangedHandler);
		}
	}
}
