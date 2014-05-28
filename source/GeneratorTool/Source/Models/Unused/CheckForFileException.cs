using System;
using System.IO;

namespace GeneratorTool.Common
{
	static class CheckForFileException
	{
		static public void FileInput(string fileName)
		{
			if (string.IsNullOrEmpty(fileName)) throw new BadImageFormatException("Bad File Name");
			if (!File.Exists(fileName)) throw new FileNotFoundException();
		}
	}
}
