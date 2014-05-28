using System;

namespace GeneratorTool.Common
{
	/// <summary>
	/// The general idea of this enumeration would be decoding.
	/// </summary>
	public enum NetContentFilterType
	{
		/// <summary>
		/// '%56%2F' to '%2F' or '\/' to '/' where 56 and 2F are hex.
		/// This is usually paired with DecodeUniChar
		/// </summary>
		Dec562F,
		EncHtmlEntity,
		DecHtmlEntity,
	//			EncodeUni,
		DecUnicodeChars,
		EncEscape,
		DecEscape,
	//			Encode,
		DecUriChars,
	}
}
