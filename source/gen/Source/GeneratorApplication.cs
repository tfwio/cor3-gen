﻿/* oio * 8/2/2014 * Time: 2:03 PM
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
		
		const string emptyFile = "";
		
		FileInfo GetFileFromArgument(string argument)
		{
			if (!Args.Contains(argument)) return null;
			var val = GetValue(true,argument);
			var f = new FileInfo(val);
			return new FileInfo(val);
		}
		
		int FirstNonSpace(string input)
		{
			int i=0;
			while (true) {
				if (char.IsWhiteSpace(input[i])) { i++; continue; }
				break;
			}
			return i;
		}
//		string reformatToConsoleWindow(string input)
//		{
//			
//		}
		void Initialize()
		{
			if (Args.Contains("--help") || Args.Contains("-h")) {
				int width = Console.BufferWidth;
				using (var str = new StringReader(RX.HELPSTRING))
				{
					string line = str.ReadLine();
					int firstChar = FirstNonSpace(line);
					
				}
				Console.Write(RX.HELPSTRING);
				return;
			}
			
			settings.FileConfig = GetFileFromArgument("-gcfg");
			settings.FileTemplates = GetFileFromArgument("-it");
			settings.FileSchema = GetFileFromArgument("-is");
			settings.JsonConfig = GetFileFromArgument("-ij");
			settings.FileIn = GetFileFromArgument("-i");
			settings.FileOut = GetFileFromArgument("-o");
			
			if (Args.Contains("-dbn")) settings.DatabaseName = this.GetValue(true,"-dbn");
			if (Args.Contains("-tbln")) settings.TableName = this.GetValue(true,"-tbln");
			if (Args.Contains("-tpln")) settings.TemplateName = this.GetValue(true,"-tpln");
			if (Args.Contains("-r")) settings.ReplacementTag = this.GetValue(true,"-r");
			
		}
		
		public GeneratorApplication(string[] args)
		{
			this.Args = new List<string>(args);
			this.ArgsBackup = new List<string>(args);
			Initialize();
			
			if (settings.HasConfigFile || settings.HasSchemaAndTemplate)
			{
				Console.Write(
					RX.TEST_CONFIG
					.Replace("{config}",	settings.FileConfig==null ? "null" : settings.FileConfig.FullName)
					.Replace("{input}",		settings.FileIn==null ? "null" : settings.FileIn.FullName)
					.Replace("{output}",	settings.FileOut==null ? "null" : settings.FileOut.FullName)
					.Replace("{template}",	settings.TemplateName ?? "null")
					.Replace("{db}",		settings.DatabaseName ?? "null")
					.Replace("{table}",		settings.TableName ?? "null")
				);
				
				var reader = new GeneratorReader()
				{
					Model=settings.FileConfig != null ?
						new GeneratorModel(settings.FileConfig.FullName) :
						new GeneratorModel(settings.FileSchema.FullName,settings.FileTemplates.FullName)
				};
				reader.Initialize();
				
				var output = reader.Generate(
					reader.Model.Databases[settings.DatabaseName][settings.TableName],
					reader.Model.Templates[settings.TemplateName]);
				
				string input = null;
				if (settings.FileIn != null) input = File.ReadAllText(settings.FileIn.FullName);
				
				if (!string.IsNullOrEmpty(settings.ReplacementTag) && !string.IsNullOrEmpty(input))
					output = input.Replace(settings.ReplacementTag,output);
				
				File.WriteAllText(settings.FileOut.FullName,output);
			}
			
		}

	}
}


