#region Using directives
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("GeneratorLib")]
//[assembly: AssemblyDescription("")]
//[assembly: AssemblyConfiguration("")]
//[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("GeneratorLib")]
[assembly: AssemblyCopyright("Copyright 2012")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("en-US")]
[assembly: InternalsVisibleTo(@"Generator, PublicKey=00240000048000009400000006020000002400005253413100040000010001001fc2e9a138afaa2989d35c910f228841cd565ebc7bebecb39ee2af9ecfd8e1a6db1ac79c4d7c83ab6bb83993a9a7b8be0703a251b53d279f64305828dc29da0b63dd857dddbdd5081c491873a2b350a1eeda02fc55711daac277301b1e6c5a37a762dec6550252b8d2140926c1634a76515ac7ece4fe9419dbac3b65a0d48c86")]
[assembly: InternalsVisibleTo(@"Generator.Clone, PublicKey=00240000048000009400000006020000002400005253413100040000010001001fc2e9a138afaa2989d35c910f228841cd565ebc7bebecb39ee2af9ecfd8e1a6db1ac79c4d7c83ab6bb83993a9a7b8be0703a251b53d279f64305828dc29da0b63dd857dddbdd5081c491873a2b350a1eeda02fc55711daac277301b1e6c5a37a762dec6550252b8d2140926c1634a76515ac7ece4fe9419dbac3b65a0d48c86")]

// This sets the default COM visibility of types in the assembly to invisible.
// If you need to expose a type to COM, use [ComVisible(true)] on that type.
[assembly: ComVisible(false)]

// The assembly version has following format :
//
// Major.Minor.Build.Revision
//
// You can specify all the values or you can use the default the Revision and 
// Build Numbers by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.*")]

internal static class res
{
	internal const string elmTpl = "ElementTemplate";
	internal const string itmTpl = "ItemsTemplate";
}