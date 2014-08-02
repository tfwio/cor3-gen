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
	public class GenSettings
	{
		public FileInfo GeneratorConfigurationFile {
			get;
			set;
		}

		public FileInfo TemplatesFile {
			get;
			set;
		}

		public FileInfo SchemaFile {
			get;
			set;
		}

		GeneratorConfig GenConfiguration {
			get;
			set;
		}

		TemplateCollection TemplatesCollection {
			get;
			set;
		}
	}
}


