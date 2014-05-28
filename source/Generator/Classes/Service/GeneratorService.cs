/*
 * Created by SharpDevelop.
 * User: oIo
 * Date: 2/14/2011
 * Time: 6:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using Generator.Classes;

namespace Generator.Service
{
	public abstract class GeneratorService<TUIElement> : IGeneratorService<TUIElement> where TUIElement:Window1
	{
		abstract protected void RegisterService(TUIElement element);
		abstract protected void RegisterCommands(TUIElement element);
		
		public TUIElement BoundUIElement { get; set; }
	
		public GeneratorService(TUIElement element)
		{
			BoundUIElement = element;
		}
	}
}
