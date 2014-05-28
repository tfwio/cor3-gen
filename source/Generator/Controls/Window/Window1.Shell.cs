#region Using
/*
 * User: oIo
 * Date: 1/31/2011 – 1:47 AM
 */
using System;
using System.ComponentModel;
using Generator.Elements;
using Generator.Elements.Types;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

using Generator.Classes;
using IWin = System.Windows.Forms.IWin32Window;
using OFD = Microsoft.Win32.OpenFileDialog;
using SFD = Microsoft.Win32.SaveFileDialog;
using WindowInteropHelper = System.Windows.Interop.WindowInteropHelper;

#endregion
namespace Generator
{
	public partial class Window1: Fluent.RibbonWindow
	{
		private void AddTask(object sender, RoutedEventArgs e)
		{
			// Configure a new JumpTask.
			JumpTask jumpTask1 = new JumpTask();
			// Get the path to Calculator and set the JumpTask properties.
			jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
			jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
			jumpTask1.Title = "Calculator";
			jumpTask1.Description = "Open Calculator.";
			jumpTask1.CustomCategory = "User Added Tasks";
			// Get the JumpList from the application and update it.
			JumpList jumpList1 = JumpList.GetJumpList(App.Current);
			jumpList1.JumpItems.Add(jumpTask1);
			JumpList.AddToRecentCategory(jumpTask1);
			jumpList1.Apply();
		}
		private void ClearJumpList(object sender, RoutedEventArgs e)
		{
			JumpList jumpList1 = JumpList.GetJumpList(App.Current);
			jumpList1.JumpItems.Clear();
			jumpList1.Apply();
		}
		private void SetNewJumpList(object sender, RoutedEventArgs e)
		{
			//Configure a new JumpTask
			JumpTask jumpTask1 = new JumpTask();
			// Get the path to WordPad and set the JumpTask properties.
			jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "write.exe");
			jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "write.exe");
			jumpTask1.Title = "WordPad";
			jumpTask1.Description = "Open WordPad.";
			jumpTask1.CustomCategory = "Jump List 2";
			// Create and set the new JumpList.
			JumpList jumpList2 = new JumpList();
			jumpList2.JumpItems.Add(jumpTask1);
			JumpList.SetJumpList(App.Current, jumpList2);
		}
	}
}
