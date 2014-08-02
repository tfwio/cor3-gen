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
			get {
				return settings;
			}
			set {
				settings = value;
			}
		}

		GenSettings settings = new GenSettings();

		public GeneratorApplication(string[] args)
		{
			this.Args = new List<string>(args);
			this.ArgsBackup = new List<string>(args);
			
			string tablename = "";
			string templatename ="";
			
			if (Args.Contains("--help") || Args.Contains("-h"))
			{
				Console.Write(@"
Generator Command Appliaction
==========================================================

There are two modes of operation in this application.

Generator-Configuration-File Basis
----------------------------------------------------------

Here, a generator-configuraion file as its basis.  Here, you
supply a path to your configuration-file and supply a name of
the template you intend to use and a name for the generation
process. 

    gen -gcfg [gen-cfg] -tbln [table-name] -tpln [template-name]

  -gcfg: (file)   Generator Configuration File
  -tbln: (string) The name of the database->table
  -tpln: (string) The name of the template.

Generator-Configuration-File Basis
----------------------------------------------------------

options

  -i  (file) input file
  -o  (file) output file

Via this mode, the input file would contain a quick data-config and
also the Template and possibly even specify the output-file.
");
				return;
			}
			
			if (this.HasFlag("-gcfg",false))
			{
				var file = this.GetValues("-gcfg").FirstOrDefault();
				if (file != null && File.Exists(file)) settings.GeneratorConfigurationFile = new FileInfo(file);
				else throw new ArgumentException( "Error loading configuration file.", "GeneratorConfigurationFile", new FileNotFoundException());
			}
			if (this.HasFlag("-tbln",false)) tablename = this.GetValues("-tbln").FirstOrDefault();
			if (this.HasFlag("-tpln",false)) templatename = this.GetValues("-tpln").FirstOrDefault();
		}
	}
}


