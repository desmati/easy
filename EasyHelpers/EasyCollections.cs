using System.Linq;

namespace System.Collections.Generic
{
	public static class EasyCollections
	{
		public static void Add(this List<System.EasyPair> KeyValuePairs, string Key, string Value)
		{
			(KeyValuePairs ?? (KeyValuePairs = new List<System.EasyPair>()))
.Add(new System.EasyPair(Key, Value));
		}

		public static bool Contains(this List<System.EasyPair> KeyValuePairs, string Key)
		{
			return KeyValuePairs?.Any(x => x.Key == Key) == true;
		}

		public static List<System.EasyPair> AddOrUpdate(this List<System.EasyPair> KeyValuePairs, string Key, string Value)
		{
			KeyValuePairs = KeyValuePairs ?? new List<System.EasyPair>();
			var Result = new List<System.EasyPair>();
			if (KeyValuePairs.Contains(Key))
			{
				Result = KeyValuePairs.Select(x =>
				{
					return new System.EasyPair(x.Key, x.Key == Key ? Value : x.Value);
				})?.ToList() ?? new List<System.EasyPair>();
			}
			else
			{
				KeyValuePairs.Add(Key, Value);
				Result = KeyValuePairs;
			}
			KeyValuePairs = Result;
			return Result;
		}

		public static string TryGetValue(this List<System.EasyPair> KeyValuePairs, string Key)
		{
			return KeyValuePairs?.FirstOrDefault(x => x.Key == Key)?.Value;
		}

		public static string TryGetValue(this Dictionary<string, string> parameters, string Key)
		{
			return (parameters != null && parameters.Count() > 0 && parameters.ContainsKey(Key)) ? parameters[Key] : null;
		}

		public static string TryGetValue(this Dictionary<string, string> parameters, params string[] keys)
		{
			foreach (var key in keys)
			{
				var value = parameters.TryGetValue(key);
				if (value != null)
				{
					return value;
				}
			}
			return null;
		}

		public static IList<T> ToIList<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.ToList();
		}
	}
}
