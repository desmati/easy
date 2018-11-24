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

		public static void SaveAsJson(this object obj, string Path)
		{
			var json = obj.ToJson();
			File.WriteAllText(Path, json);
		}

		public static T JsonFileToObject<T>(this string Path)
		{
			if (string.IsNullOrEmpty(Path) || !File.Exists(Path))
			{
				return default(T);
			}

			var json = File.ReadAllText(Path);
			if (string.IsNullOrEmpty(json))
			{
				return default(T);
			}

			try
			{
				return json.FromJson<T>();
			}
			catch
			{
				return default(T);
			}
		}
	}
}
