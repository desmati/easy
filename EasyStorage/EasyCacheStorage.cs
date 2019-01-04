using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data
{
	public abstract class IEasyCacheStorage<TEntity> : IEasyStorage<TEntity, Guid>
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

		public async Task<TEntity> LoadAsync(Guid idstring)
		{
			return await LoadAsync<TEntity>(idstring);
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

		public async Task DeleteAsync(Guid id)
		{
			var idstring = id.ToString();
			var _lock = _locks.GetOrAdd(idstring, _ => new SemaphoreSlim(1, 1));
			try
			{
				await _lock.WaitAsync();
				_cache.Remove(idstring);
				_locks.TryRemove(idstring, out _lock);
			}
			catch { _lock.Release(); }
			finally { _lock.Release(); }
		}

		public async Task<List<TEntity>> AllAsync()
		{
			var r = new List<TEntity>();
			var keys = _locks.Keys;
			foreach (var key in keys)
			{
				r.Add(await LoadAsync(Guid.Parse(key)));
			}

			return r;
		}

		public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return (await AllAsync() ?? new List<TEntity>())?.AsQueryable()?.Where(predicate)?.ToList() ?? new List<TEntity>();
		}

		public async Task<EasyPaging<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber = 1, int pageSize = 20)
		{
			var result = new EasyPaging<TEntity>(pageNumber, pageSize);
			var rows = await FindAsync(predicate);
			result.Count = rows.Count();
			result.Rows = rows.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			return result;
		}
		public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var rows = await FindAsync(predicate);
			return rows != null ? rows.FirstOrDefault() : default(TEntity);
		}


		public Guid Save(EasyStorableObject<Guid> obj)
		{
			return SaveAsync(obj).GetAwaiter().GetResult();
		}

		public TEntity Load(Guid id)
		{
			return LoadAsync(id).GetAwaiter().GetResult();
		}

		public void Delete(Guid id)
		{
			DeleteAsync(id).Wait();
		}

		public List<TEntity> All()
		{
			return AllAsync().GetAwaiter().GetResult();
		}

		public EasyPaging<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageNumber = 1, int pageSize = 20)
		{
			return FindAsync(predicate, pageNumber, pageSize).GetAwaiter().GetResult();
		}

		public List<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return FindAsync(predicate).GetAwaiter().GetResult();
		}

		public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return FirstOrDefaultAsync(predicate).GetAwaiter().GetResult();
		}
	}
}
