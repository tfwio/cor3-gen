/*
 * User: oIo
 * Date: 11/15/2010 ? 2:49 AM
 */
#region Using
using System.ComponentModel;
using System.Globalization;
using System.Resources;
#endregion

namespace Generator
{
	public class ResourceUtil
	{
		static public string GetString(string key)
		{
			return ResourceManager.GetString(key);
		}
		static public string GetString(string key, object value)
		{
			return string.Format(ResourceManager.GetString( key, Culture/*??CultureInfo.CurrentCulture*/ ),value);
		}
		
		private static ResourceManager resourceMan;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ResourceUtil.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Generator.Messages", typeof(ResourceUtil).Assembly);
					ResourceUtil.resourceMan = resourceManager;
				}
				return ResourceUtil.resourceMan;
			}
		}
	
		private static CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ResourceUtil.resourceCulture;
			}
			set
			{
				ResourceUtil.resourceCulture = value;
			}
		}
	}
}
