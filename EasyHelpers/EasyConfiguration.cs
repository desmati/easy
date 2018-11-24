using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
	public static class EasyConfiguration
	{
		public static object GlobalLock = new object();
		public static Dictionary<string, object> Current { get; set; }
		public static string WWWRootPath { get; set; }
		public static string ContentRootPath { get; set; }
		public static bool IsDebug => GetBoolean("IsDebug");

		public static string Get(string Key)
		{
			if (Current == null)
			{
				Current = new Dictionary<string, object>();
			}
			return Current.ContainsKey(Key)
				? Current[Key] + ""
				: "";
		}

		public static bool GetBoolean(string Key)
		{
			return new string[] { "true", "1", "yes" }.Contains(Get(Key).ToLower());
		}

		public static int GetInt32(string Key)
		{
			int.TryParse(Get(Key), out var result);
			return result;
		}

		public static long GetLong(string Key)
		{
			long.TryParse(Get(Key), out var result);
			return result;
		}

		public static T Get<T>(string Key)
		{
			return Get(Key).FromJson<T>();
		}

		public static void Set(string Key, object Value)
		{
			if (Current == null)
			{
				Current = new Dictionary<string, object>();
			}

			if (Current.ContainsKey(Key))
			{
				Current[Key] = Value;
			}
			else
			{
				Current.Add(Key, Value);
			}
		}
	}
}
