/*
 * Created by SharpDevelop.
 * User: oIo
 * Date: 2/14/2011
 * Time: 6:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Input;

namespace Generator.Service
{
	/// <summary>
	/// Caller is responsible for all events, and memory
	/// </summary>
	public class FileCollectionCommands
	{
		static public void callBrowseDIrector() { BrowseDirectory.Execute(null); }
		static public readonly ICommand BrowseDirectory = new RoutedCommand();
		static public readonly ICommand OpenPathCollection = new RoutedCommand();
//		static public readonly ICommand BrowseDirectory = new RoutedCommand();
	}
}
