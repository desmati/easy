using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data
{
	public abstract class IEasyCacheStorage<T> : IEasyStorage<T, Guid>
	{
		readonly MemoryCache _cache;
		static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new ConcurrentDictionary<string, SemaphoreSlim>();
		public IEasyCacheStorage()
		{
			_cache = new MemoryCache(new MemoryCacheOptions());
		}
		private string _AreaName { get; set; }
		public IEasyCacheStorage(string AreaName)
		{
			_AreaName = AreaName;
		}

		public async Task<Guid> SaveAsync(EasyStorableObject<Guid> obj)
		{
			if (obj == null)
			{
				return Guid.Empty;
			}

			if (obj.Id.Equals(Guid.Empty))
			{
				obj.Id = Guid.NewGuid();
			}

			var idstring = obj.Id.ToString();
			var _lock = _locks.GetOrAdd(idstring, _ => new SemaphoreSlim(1, 1));
			try
			{
				await _lock.WaitAsync();
				_cache.Set(idstring, obj,
				  new MemoryCacheEntryOptions()
				  .SetAbsoluteExpiration(TimeSpan.FromMinutes(15))
				  .RegisterPostEvictionCallback(
						(key, value, reason, substate) => _lock.Release()
					));
			}
			catch { _lock.Release(); }
			finally { _lock.Release(); }
			return obj.Id;
		}

		public async Task<T> LoadAsync(Guid idstring)
		{
			return await LoadAsync<T>(idstring);
		}

		public async Task<TResult> LoadAsync<TResult>(Guid id)
		{
			var idstring = id.ToString();
			var _lock = _locks.GetOrAdd(idstring, _ => new SemaphoreSlim(1, 1));
			try
			{
				await _lock.WaitAsync();
				return _cache.Get<TResult>(idstring);
			}
			catch
			{
				_lock.Release();
				return default(TResult);
			}
			finally
			{
				_lock.Release();
			}
		}

		public void Delete(Guid id)
		{
			var idstring = id.ToString();
			var _lock = _locks.GetOrAdd(idstring, _ => new SemaphoreSlim(1, 1));
			try
			{
				_lock.Wait();
				_cache.Remove(idstring);
				_locks.TryRemove(idstring, out _lock);
			}
			catch { _lock.Release(); }
			finally { _lock.Release(); }
		}

		public async Task<List<T>> AllAsync()
		{
			var r = new List<T>();
			var keys = _locks.Keys;
			foreach (var key in keys)
			{
				r.Add(await LoadAsync(Guid.Parse(key)));
			}

			return r;
		}

		public async Task<List<T>> FindAsync(Func<T, bool> predicate)
		{
			return (await AllAsync() ?? new List<T>())?.Where(predicate)?.ToList() ?? new List<T>();
		}
	}
}
