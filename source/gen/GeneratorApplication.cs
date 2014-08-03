/* oio * 8/2/2014 * Time: 2:03 PM
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Generator.Core.Markup;
using Generator.Elements;
namespace GeneratorApp
{
	// feel free to optimize and/or
	
	class GeneratorApplication : GeneratorApp.Commander
	{
		public GenSettings Settings {
			get { return settings; }
			set { settings = value; }
		} GenSettings settings = new GenSettings();
		
		void Initialize()
		{
			if (Args.Contains("--help") || Args.Contains("-h")) {
				Console.Write(RX.HELPSTRING);
				return;
			}
			if (Args.Contains("-gcfg"))
			{
				settings.HasConfigFile = true;
				
				string file = this.GetValue(true,"-gcfg");
				Console.WriteLine("Reading input: {0}",string.IsNullOrEmpty(file) ? "error reading file" : file);
				
				if (file != null && File.Exists(file)) settings.GeneratorConfigurationFile = new FileInfo(file);
				else throw new ArgumentException( "Error loading configuration file.", "GeneratorConfigurationFile", new FileNotFoundException());
				
				if (Args.Contains("-dbn")) settings.DatabaseName = this.GetValue(true,"-dbn");
				if (Args.Contains("-tbln")) settings.TableName = this.GetValue(true,"-tbln");
				if (Args.Contains("-tpln")) settings.TemplateName = this.GetValue(true,"-tpln");
				if (Args.Contains("-o")) settings.OutputFile = this.GetValue(true,"-o");
			}
		}
		
		public GeneratorApplication(string[] args)
		{
			this.Args = new List<string>(args);
			this.ArgsBackup = new List<string>(args);
			Initialize();
			
			if (settings.HasConfigFile)
			{
				Console.Write(
					RX.TEST_CONFIG
						.Replace("{config}",	settings.GeneratorConfigurationFile.Name)
						.Replace("{output}",	settings.OutputFile)
						.Replace("{template}",	settings.TemplateName)
						.Replace("{db}",		settings.DatabaseName)
						.Replace("{table}",		settings.TableName)
				);
				var reader = new GeneratorReader();
				reader.Model = new GeneratorModel(){ FileName = settings.GeneratorConfigurationFile.FullName };
				reader.Initialize();
				var output = reader.Generate(
					reader.Model.Databases[settings.DatabaseName][settings.TableName],
					reader.Model.Templates[settings.TemplateName]);
				
				File.WriteAllText(settings.OutputFile,output);
			}
			
		}

	}
}


