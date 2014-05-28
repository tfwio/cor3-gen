using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace GeneratorTool.Common
{

	static class NetContentFilter
	{
		/// <summary>
		/// Tries to imply name and ext from provided content.
		/// </summary>
		/// <param name="regularExpression"></param>
		/// <param name="lastOutputFile"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		static public string TryExpressionFileName(
			string regularExpression,
			string lastOutputFile,
			params string[] content)
		{
			string outputpath = null;
			string outputvalue = null;
			if (string.IsNullOrEmpty(regularExpression)) return string.Join("",content);
			{
				try {
					outputpath = Path.GetDirectoryName(lastOutputFile);
				} catch {
					
				}
				string name=null, extension=null;
				try
				{
					MatchCollection mc = Regex.Matches(string.Join("",content),regularExpression);
					foreach (Match m in mc) {
						if (m.Groups["ext"] !=null)
						{
							string ex = m.Groups["ext"].Value;
							if (!String.IsNullOrEmpty(ex)) extension = ex;
						}
						if (m.Groups["title"] !=null)
						{
							string ex = m.Groups["title"].Value;
							if (!String.IsNullOrEmpty(ex)) name = ex;
						}
					}
					if ((!string.IsNullOrEmpty(name))|(!string.IsNullOrEmpty(extension)))
						outputvalue = string.Format(
							"{2}\\{0}{1}",
							name ?? "[title-error]",
							extension ?? "[ext-error]",
							outputpath ?? "[directory-error]");
					else
					{
						System.Console.Beep(520,1200);
					}
				}
				catch
				{
					MessageBox.Show(Resource.MsgRegexError,Resource.MsgRegexError_Caption);
				}
			}
			return outputvalue;
		}
		
		/// <summary>
		/// Search a set of content from indexed search locations.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="searchStart"></param>
		/// <param name="searchEnd"></param>
		/// <returns></returns>
		static public string GetSubstring(this string content, string searchStart, string searchEnd)
		{
			int start = -1, end = -1;
			try
			{
				start = content.IndexOf(searchStart);
				end = content.IndexOf(searchEnd,start+searchStart.Length);
			} catch (NullReferenceException generalError) {
				return ("General Error.  You probably selected the wrong target.");
			} catch (Exception generalError) {
//				MessageBox.Show("Unknown Error.  You probably selected the wrong target.");
				return "Unknown Error.";
			}
			
			start+=searchStart.Length;
			
			if (end==-1 || start == -1) {
				System.Windows.MessageBox.Show(
					"Nothing was found",
					"Please check your parser",
					System.Windows.MessageBoxButton.OK,
					System.Windows.MessageBoxImage.Exclamation
				);
				return null;
			}
			return content.Substring(start,end-start);
		}
		
		/// <summary>
		/// Unicode Char-Code to utf-8 character.
		/// \u#### entity to character conversion.
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		static public string UnicodeCharacterMatch(Match match)
		{
			var code = match.Groups["code"].Value;
			int value = Convert.ToInt32(code, 16);
			return ((char) value).ToString();
		}
		
		/// <summary>
		/// Matches HEX string characters such as '%####'.
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		static public string UriHexStringMatch(Match match)
		{
			var code = match.Groups["code"].Value;
			int value = int.Parse(code, NumberStyles.AllowHexSpecifier);
			System.Diagnostics.Debug.Print("{0}",Convert.ToChar(value));
			return ((char) value).ToString();
		}

		static string ExpressionFilter(string name, string input, string expression, MatchEvaluator evaluator)
		{
			string str1 = input;
			try { str1 = Regex.Replace(str1, expression, evaluator); }
			catch (Exception err) { System.Diagnostics.Debug.Print("Error Parsing {0};\n{1}", name, err); }
			return str1;
		}
		
		static public string UriFilter(this string input)
		{
			return FilterEntry(
				input,
				NetContentFilterType.Dec562F,
				NetContentFilterType.DecEscape,
				NetContentFilterType.DecHtmlEntity,
				NetContentFilterType.DecUnicodeChars,
				NetContentFilterType.DecUriChars);
		}
		
		static public string FilterEntry(this string input, params NetContentFilterType[] filters)
		{
			string str1 = input;
			foreach (NetContentFilterType filter in filters)
			{
				if (filter==NetContentFilterType.Dec562F)				str1 = str1.Replace("\\/", "/");
				else if (filter==NetContentFilterType.DecEscape)		str1 = Uri.UnescapeDataString(str1);
				else if (filter==NetContentFilterType.DecHtmlEntity)	str1 = System.Web.HttpUtility.HtmlDecode(str1);
				else if (filter==NetContentFilterType.DecUnicodeChars)	str1 = ExpressionFilter("dec-uni",str1, @"\\u(?<code>[0-9]{2,4})", UnicodeCharacterMatch);
				else if (filter==NetContentFilterType.DecUriChars) 		str1 = ExpressionFilter("dec-uri",str1, @"\\u(?<code>[0-9]{2,4})", UriHexStringMatch);
			}
			return str1;
		}
	}
}
