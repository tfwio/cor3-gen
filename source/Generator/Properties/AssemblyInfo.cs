#region Using directives
using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Threading;
#endregion

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Generator")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Generator")]
[assembly: AssemblyCopyright("Copyright 2011")]
[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("en-US")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and
// Build Numbers by using the '*' as shown below:
//[assembly: AssemblyVersion("*")]
//[assembly: AssemblyVersion("1.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: DefaultDependency(LoadHint.Always)]
namespace Generator
{
	public static class Resources
	{
		internal const string elmTpl = "ElementTemplate";
		internal const string itmTpl = "ItemsTemplate";
		public const string tag_attribute_template = @"{name}=""{value}""";
	}
}
