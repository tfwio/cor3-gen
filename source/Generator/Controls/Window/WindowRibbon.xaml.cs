/*
 * Created by SharpDevelop.
 * User: oio
 * Date: 7/29/2011
 * Time: 6:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

namespace Generator.Controls
{

	/// <summary>
	/// Interaction logic for WindowRibbon.xaml
	/// </summary>
	public partial class WindowRibbon : Fluent.Ribbon
	{
		Window1 appwindow { get { return Window1.This; } }
		static public double ButtonDefaultWidth { get { return 128; } }
		
		static Uri OxyUri(string input) { return new Uri(Oxy(input),UriKind.RelativeOrAbsolute); }
		static string Oxy(string image) { return "pack://application:,,,/System.Cor3.Stock;component/images/{file}".Replace("{file}",image); }
		public static readonly BitmapImage methodM = new BitmapImage(OxyUri("methodM.png"));
		public static readonly BitmapImage dbCfg = new BitmapImage(OxyUri("oxygen/db.cfg.png"));
		public static readonly BitmapImage kexi = new BitmapImage(OxyUri("oxygen/kexi.png"));
		public static readonly BitmapImage fileNew = new BitmapImage(OxyUri("oxygen/filenew.png"));
		public static readonly BitmapImage fileSave = new BitmapImage(OxyUri("oxygen/fileSave.png"));
		public static readonly BitmapImage fileSaveAs = new BitmapImage(OxyUri("oxygen/fileSaveAs.png"));
		public static readonly BitmapImage fileImport = new BitmapImage(OxyUri("oxygen/fileImport.png"));
		public static readonly BitmapImage template = new BitmapImage(OxyUri("template.png"));
		public static readonly BitmapImage bolt16 = new BitmapImage(OxyUri("bolt.png"));
		public static readonly BitmapImage bolt32 = new BitmapImage(OxyUri("bolt32.png"));
		public static readonly BitmapImage application_x_desktop = new BitmapImage(OxyUri("oxygen/application_x_desktop.png"));
//		public static readonly Image fileNew = new BitmapImage(OxyUri("oxygen/filenew.png"));
//		public static readonly Bitmap fileNew = new BitmapImage(OxyUri("oxygen/filenew.png"));
		//xfile
		static public Fluent.ScreenTip TipTemplateGroup = new Fluent.ScreenTip(){ Title="Template Group Name", Text="Conjugation the template into a group/category" } ;
		static public Fluent.ScreenTip TipTemplateAlias = new Fluent.ScreenTip(){ Title="Template Alias", Text="The name of the template (or actually the “Alias”)" } ;
		static public Fluent.ScreenTip TipSyntaxLanguage = new Fluent.ScreenTip(){ Title="Syntax Language", Text="Syntax/Language" } ;
		static public Fluent.ScreenTip TipTags = new Fluent.ScreenTip(){ Title="Tags", Text="..." } ;
		static public Fluent.ScreenTip TipSqlUtil = new Fluent.ScreenTip(){ Title="§QL Section", Text="Click to show the SQL Editor section—And clicke the drop-down for additional actions." } ;
		static public Fluent.ScreenTip TipMiscToolSet = new Fluent.ScreenTip(){ Title="", Text="Provides additional actions and tools — Eventntually plugin-hosted actions will be provided here." } ;
		static public Fluent.ScreenTip TipActions = new Fluent.ScreenTip(){ Title="Additional Actions", Text="Displays any miscelanious actions not present in any other section." } ;
		static public Fluent.ScreenTip TipViewDConfig = new Fluent.ScreenTip(){ Title="View DatabaseConfiguration Source", Text="Views the source of the DatabaseConfiguration (XML) file." } ;
		static public Fluent.ScreenTip TipViewDConfigSource = new Fluent.ScreenTip(){ Title="View", Text="This is some text for the temporary Key" } ;
		static public Fluent.ScreenTip TipAddTableField = new Fluent.ScreenTip(){ Title="Add Table Field", Text="Add default elements to the Selected Table.  If no table is selected, then nothing happens." } ;
		static public Fluent.ScreenTip TipDatabaseConfiguration = new Fluent.ScreenTip(){ Title="Database Configuration", Text="Click to switch to the DatabaseConfiguration Element editor – and additional options." } ;
		static public Fluent.ScreenTip TipTemplateConfiguration = new Fluent.ScreenTip(){ Title="Template Configuration", Text="Click to view the Template Editor – and see additional tasks." } ;
//		static public Fluent.ScreenTip Tip = new Fluent.ScreenTip(){ Title="", Text="" } ;
		void CheckPragmaFolding(object sender, RoutedEventArgs args)
		{
			AvalonEdit.Helpers.CSharpPragmaRegionFoldingStrategy.UsePragmaFolding = true;
		}
		void UncheckPragmaFolding(object sender, RoutedEventArgs args)
		{
			AvalonEdit.Helpers.CSharpPragmaRegionFoldingStrategy.UsePragmaFolding = false;
		}
		OpenFileDialog ofd = new OpenFileDialog();
		SaveFileDialog sfd = new SaveFileDialog();
		void XData2SQLite_BrowseSQLite(object sender, RoutedEventArgs args)
		{
			sfd.Filter = "SQLite (db)|*.db";
			if (sfd.ShowDialog().Value) tbSqliteFile.Text = sfd.FileName;
		}
		void XData2SQLite_BrowseXData(object sender, RoutedEventArgs args)
		{
			ofd.Filter = "Xml Data-Configuration (xdata)|*.xdata";
			if (ofd.ShowDialog().Value) tbXmlDataFile.Text = ofd.FileName;
		}
		void XData2SQLite_ButtonClick(object sender, RoutedEventArgs args)
		{
			SQLiteOperations.DropDataConfigurationTable(tbSqliteFile.Text,true);
			SQLiteOperations.XmlToDatabaseConfiguration(tbXmlDataFile.Text,tbSqliteFile.Text);
		}
		
		public WindowRibbon()
		{
			InitializeComponent();
		}
	}
}