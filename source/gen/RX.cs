/* oio * 8/2/2014 * Time: 2:03 PM
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Generator.Core.Markup;
using Generator.Elements;

namespace GeneratorApp
{
	class RX
	{
		public const string HELPSTRING = @"
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
	  -dbn:   (string) The name of the database
	  -tbln: (string) The name of the database->table
	  -tpln: (string) The name of the template.
	
	Generator-Configuration-File Basis
	----------------------------------------------------------
	
	options
	
	  -i  (file) input file
	  -o  (file) output file
	
	Via this mode, the input file would contain a quick data-config and
	also the Template and possibly even specify the output-file.
	";
		public const string TEST_CONFIG = @"
Configuration  = {config}
OutputFile     = {output}
TemplateName   = {template}
db.table       = {db}.{table}
";
	}
}
