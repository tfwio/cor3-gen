using System;
using GeneratorTool.Common;
using System.IO;
using System.Net;
using System.Windows;

using Microsoft.Win32;

namespace GeneratorTool
{
	/// <summary>
	/// Description of ContentLoader.
	/// (this may just be here for prior reference)
	/// </summary>
	static class ContentLoader
	{
		static SaveFileDialog sfd = new SaveFileDialog();
		#region LoadVeetleUrlContent
		/// <summary>
		/// Good for small files.
		/// <para>
		/// This is a simple UrlContent Loader where teh content is loaded as a string.
		/// This method is depreceated: use HttpContext.
		/// </para>
		/// </summary>
		/// <remarks>
		/// Should probably have a funciton to test out weather we
		/// have an internet connection or not.
		/// <para>
		/// Error checking is a little better then it was, but there
		/// is room for errors.
		/// </para>
		/// </remarks>
		/// <param name="isLocalCondition">Tells which uri to load (unless there is no network).</param>
		/// <param name="url">The url to veetle's Json page.</param>
		/// <param name="urlFallback">
		/// A local fallback Url that points to File|HTTP.
		/// <para>• http://127.0.0.1/curly/?c=vtl</para>
		/// </param>
		/// <returns>
		/// Returns the content, or null if there was any sort of error.
		/// </returns>
		static public string LoadVeetleUrlContent(bool isLocalCondition, string url, string urlFallback)
		{
			string sresponse = null;
			HttpWebRequest r = null;
			Uri uri = new Uri(url,UriKind.Absolute);
			uri.LeaveDotsAndSlashesEscaped();
			uri.LeaveMultipleSlashesAsIs();
			r = (HttpWebRequest)System.Net.WebRequest.Create(uri);
			r.AllowAutoRedirect = true;
			r.MaximumAutomaticRedirections = 50;
			bool hasError = false;
			Exception error = null;
			try {
				using (HttpWebResponse response = (HttpWebResponse)r.GetResponse())
					using (Stream dataStream = response.GetResponseStream())
						using(StreamReader reader = new StreamReader(dataStream))
							sresponse = reader.ReadToEnd();
			} catch (Exception err) {
				hasError = true;
				error = err;
			}
			
			hasError = hasError||string.IsNullOrEmpty(sresponse);
			if (hasError && System.Windows.MessageBox.Show(
				Resource.MsgLoadVeetleContent,
				Resource.MsgLoadVeetleContent_Caption,
				System.Windows.MessageBoxButton.OKCancel,
				System.Windows.MessageBoxImage.Error
			) == MessageBoxResult.OK ) throw error;
			
			if (string.IsNullOrEmpty(sresponse)) return null;
			return sresponse;
		}
		#endregion
		#region GetLineFromText
		static public string GetLineFromText(string content, string searchStart, string searchEnd)
		{
			if (content==null) {
				System.Windows.MessageBox.Show(
					Resource.MsgGetLineFromText,
					Resource.MsgGetLineFromText_Caption,
					System.Windows.MessageBoxButton.OK,
					System.Windows.MessageBoxImage.Exclamation
				);
				return null;
			}
			int start = content.IndexOf(searchStart);
			int end = content.IndexOf(searchEnd,start+searchStart.Length);
			start+=searchStart.Length;
			if (end==-1) {
				System.Windows.MessageBox.Show(
					Resource.MsgNothingFound,
					Resource.MsgGetLineFromText_Caption,
					System.Windows.MessageBoxButton.OK,
					System.Windows.MessageBoxImage.Exclamation
				);
				return null;
			}
			return content.Substring(start,end-start);
		}
		#endregion

	}

}
