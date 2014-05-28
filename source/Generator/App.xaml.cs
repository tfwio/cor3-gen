using System;
using System.Collections.Generic;
using System.Configuration;
using System.Cor3.Data.Settings;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Shell;
using System.Xml;
using System.Linq;
namespace Generator
{
	using setting=SQLiteSettings.setting;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		static public bool HasGeneratorConfigurationFile;
		static public string[] Arguments;

		#region Jump List
		JumpList JumpList { get;set; }
		
		void CreateJumplists()
		{
			this.JumpList = new JumpList();
			this.JumpList.ShowRecentCategory = true;
			this.JumpList.ShowFrequentCategory = true;
			this.JumpList.JumpItemsRejected += JumpList_JumpItemsRejected;
			this.JumpList.JumpItemsRemovedByUser += JumpList_JumpItemsRemovedByUser;
			this.JumpList.JumpItems.Add(
				new JumpTask(){
					Description=@"Open Notepad",
					ApplicationPath=@"C:\Windows\notepad.exe",
					IconResourcePath=@"C:\Windows\notepad.exe"
				});
			this.JumpList.JumpItems.Add(
				new JumpTask(){
					Description="Open readme.txt in Notepad",
					ApplicationPath="write.exe",
					IconResourcePath=@"C:\Windows\System32\imageres.dll",
					IconResourceIndex=36,
					WorkingDirectory=@"C:\Users\Public\Documents",
					Arguments="readme.txt"
				});
//			this.JumpList.JumpItems.Add(
//				new JumpPath(){
//					Path=@"C:\Users\Public\Documents\readme.txt"
//				});
			JumpList.SetJumpList(this,this.JumpList);
		}

		private void JumpList_JumpItemsRejected(object sender, System.Windows.Shell.JumpItemsRejectedEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} Jump Items Rejected:\n", e.RejectionReasons.Count);
			for (int i = 0; i < e.RejectionReasons.Count; ++i)
			{
				if (e.RejectedItems[i].GetType() == typeof(JumpPath))
					sb.AppendFormat("Reason: {0}\tItem: {1}\n", e.RejectionReasons[i], ((JumpPath)e.RejectedItems[i]).Path);
				else
					sb.AppendFormat("Reason: {0}\tItem: {1}\n", e.RejectionReasons[i], ((JumpTask)e.RejectedItems[i]).ApplicationPath);
			}

			MessageBox.Show(sb.ToString());
		}
		private void JumpList_JumpItemsRemovedByUser(object sender, System.Windows.Shell.JumpItemsRemovedEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} Jump Items Removed by the user:\n", e.RemovedItems.Count);
			for (int i = 0; i < e.RemovedItems.Count; ++i)
			{
				sb.AppendFormat("{0}\n", e.RemovedItems[i]);
			}

			MessageBox.Show(sb.ToString());
		}

		#endregion
		
		protected override void OnStartup(StartupEventArgs e)
		{
			CreateJumplists();
			
			try
			{
				Stack<string> args = new Stack<string>(App.Arguments = e.Args);
				args = new Stack<string>(args.Reverse());
				Console.WriteLine("Argument Length={0}",args.Count);
				if (e.Args.Length==1)
				{
					if (System.IO.File.Exists(e.Args[0]))
					{
						if (System.IO.Path.GetExtension(e.Args[0])==".generator-config")
						{
							HasGeneratorConfigurationFile = true;
						}
					}
				}
				else if (args.Contains("-sqlite"))
				{
					SQLiteSettings appsettings = new SQLiteSettings(@"C:\Users\oio\Desktop\settings.db");
				}
				args.Clear();
				args = null;
				base.OnStartup(e);
			} catch (Exception) {
				
			}
		}
	}
}
#region old-app-setting
//	appsettings.ForceValue(new setting(){Name="app",Value=SQLiteSettings.ApplicationName});
//				setting
// testone = new setting(){Name="new setting"},
//					testtwo = new setting(){Name="app",Value=SQLiteSettings.ApplicationName};
//				MessageBox.Show(
//					appsettings.GetNumberOfSettings().ToString()+" settings counted",
//					appsettings["new setting"].Value
//				);
//				testtwo=appsettings.GetValue(testtwo);
//				MessageBox.Show(testtwo.Value,"app");

#endregion