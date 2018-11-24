using Microsoft.Extensions.Caching.Memory;

namespace System
{
	public static class EasyCache
	{
		private static MemoryCache cache => new MemoryCache(new MemoryCacheOptions());
		//public static bool HasValue(this long Id)
		//{
		//    var key = "cacheitem_" + Id;
		//    return HasValueInCache(key);
		//}
		public static bool HasValue(this string Key)
		{
			return cache.Get(Key) != null;
		}

		//public static T GetFromCache<T>(this long Id)
		//{
		//    return GetFromCache<T>("cacheitem_" + Id);
		//}

		public static T GetFromCache<T>(this string Key)
		{
			try { return (T)cache.Get(Key); } catch { }
			return default(T);
			//var policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(2, 0, 0) };
			//cache.Add(key, value, policy);
		}

		//private static long NextId(string Region = "rph")
		//{
		//    var key = "cacheitemlastkey";
		//    var nextId = 0L;
		//    try { nextId = ((long)cache.Get(key)) + 1; } catch { nextId = DateTime.Now.Ticks; }
		//    cache.Add(key, nextId, DateTime.Today.AddDays(1));
		//    return nextId;
		//}
		public static string SetInCache(this object Item, string Key = "", DateTime? AbsoluteExpiration = null)
		{
			if (AbsoluteExpiration == null || !AbsoluteExpiration.HasValue)
			{
				AbsoluteExpiration = DateTime.Now.AddMinutes(30);
			}

			if (string.IsNullOrEmpty(Key))
			{
				var type = Item.GetType();
				var typeName = type.Name;
				var propertyKey = type.GetProperty("Key");
				var propertyId = type.GetProperty("Id");
				Key =
					propertyId?.GetValue(Item)?.ToString() ??
					propertyKey?.GetValue(Item)?.ToString() ??
					Guid.NewGuid().ToString("n");
				Key = typeName + Key;
			}
			cache.Set(Key, Item, AbsoluteExpiration.Value);
			return Key;
		}
		//public static long SetInCache(this object Item)
		//{
		//    var id = NextId(Region);
		//    var key = "cacheitem_" + id;
		//    SetInCache(Item, key);
		//    return id;
		//}

		public static void UpdateCacheValue(this object Item, string Key)
		{
			RemoveFromCache(Key);
			Item.SetInCache(Key);
		}
		//public static void UpdateCacheValue(this long Id, object Item)
		//{
		//    var key = "cacheitem_" + Id;
		//    Item.UpdateCacheValue(key);
		//}

		public static void RemoveFromCache(this string Key)
		{
			cache.Remove(Key);
		}
		//public static void RemoveFromCache(this long Id)
		//{
		//    var key = "cacheitem_" + Id;
		//    RemoveFromCache(key);
		//}

		//public static IEnumerable<long> AddItemsToCache<T>(this IEnumerable<T> Items)
		//{
		//    foreach (var item in Items) yield return item.SetInCache(Region);
		//}
	}
}
