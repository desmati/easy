using System.IO;

namespace System
{
	public static class EasyObject
	{
		public static string ToJson(this object obj)
		{
			try
			{
				return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
			}
			catch
			{
				return string.Empty;
			}
		}

		public static T FromJson<T>(this string json)
		{
			try
			{
				return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
			}
			catch
			{
				return default(T);
			}
		}
	}
}
