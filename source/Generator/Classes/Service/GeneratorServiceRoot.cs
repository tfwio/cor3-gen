/*
 * Created by SharpDevelop.
 * User: oIo
 * Date: 2/14/2011
 * Time: 6:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Generator.Classes;

namespace Generator.Service
{
	public class GeneratorServiceRoot : GeneratorService<Window1>
	{
		protected UITemplateManager Factory { get { return BoundUIElement.TemplateFactory; } }

		protected override void RegisterService(Window1 element)
		{
			RegisterCommands(element);
		}

		protected override void RegisterCommands(Window1 element)
		{
		}

		public GeneratorServiceRoot(Window1 window) : base(window) { RegisterService(BoundUIElement); }
	}
}
