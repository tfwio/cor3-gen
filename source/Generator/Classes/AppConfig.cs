/**
 * oIo * 2/15/2011 11:12 AM
 **/
using System;
using System.IO;
using System.Xml.Serialization;

using Generator.Core.Markup;

namespace Generator.Classes
{
	/// <summary>
	/// Description of AppConfig.
	/// </summary>
	public class AppConfig : SerializableClass<AppConfig>
	{
		string databases;
		[XmlAttribute]
		public string DatabasesFile {
			get { return databases; }
			set { databases = value; }
		}
		string templates;
		[XmlAttribute]
		public string TemplatesFile {
			get { return templates; }
			set { templates = value; }
		}

		public AppConfig()
		{
		}
		public AppConfig(Window1 win)
		{
			if (win.TemplateFactory.SelectedCollection!=null)
			{
				Logger.LogG("app-config",win.TemplateFactory.SelectedCollection.FileLoadedOrSaved);
				databases = win.TemplateFactory.SelectedCollection.FileLoadedOrSaved;
			}
			if (win.TemplateFactory.Templates!=null)
			{
				Logger.LogG("app-config",win.TemplateFactory.Templates.FileLoadedOrSaved);
				templates = win.TemplateFactory.Templates.FileLoadedOrSaved;
			}
		}
		static public void ToWindow(Window1 win, AppConfig config)
		{
			if (config.DatabasesFile!=null && File.Exists(config.DatabasesFile))
			{
				Logger.LogG("app-config","loading data");
				Generator.Elements.DatabaseCollection.Load( config.DatabasesFile, win.TemplateFactory.SelectedCollection);
				win.TemplateFactory.SelectedCollectionTree = win.TemplateFactory.SelectedCollection;
			}
			if (config.TemplatesFile!=null)
			{
				Logger.LogG("app-config","loading templates");
				TemplateCollection.Load(config.TemplatesFile, win.TemplateFactory.Templates);
				win.TemplateFactory.InitializeCombos();
			}
		}
	}
}
