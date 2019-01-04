using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace System.Data
{
	public abstract class EasyJsonFileStorage<TEntity, TKey> : IEasyStorage<TEntity, TKey>
	{
		private string _StoragePath { get; set; }
		public EasyJsonFileStorage(string StoragePath)
		{
			_StoragePath = StoragePath;
			try { if (!Directory.Exists(StoragePath)) { Directory.CreateDirectory(StoragePath); } } catch { }
		}

		public async Task<TKey> SaveAsync(EasyStorableObject<TKey> obj)
		{
			if (obj == null)
			{
				return default(TKey);
			}

			var keyType = typeof(TKey);
			if (keyType == typeof(Guid))
			{
				var objId = (Guid)Convert.ChangeType(obj.Id, typeof(Guid));
				if (objId == null || objId == Guid.Empty)
				{
					var id = Guid.NewGuid();
					obj.Id = (TKey)Convert.ChangeType(id, keyType);
				}
			}

			if (keyType == typeof(long))
			{

				var objId = (long)Convert.ChangeType(obj.Id, typeof(long));
				if (objId <= 0)
				{
					var id = EasyAutoIncreament.NextId(nameof(TEntity), _StoragePath);
					obj.Id = (TKey)Convert.ChangeType(id, keyType);
				}
			}

			await File.WriteAllTextAsync(Path.Combine(_StoragePath, obj.Id + ".json"), JsonConvert.SerializeObject(obj));
			return obj.Id;
		}

		public async Task<TEntity> LoadAsync(TKey id)
		{
			try
			{
				return JsonConvert.DeserializeObject<TEntity>(await File.ReadAllTextAsync(Path.Combine(_StoragePath, id + ".json")));
			}
			catch
			{
				return default(TEntity);
			}
		}

		public async Task<TResult> LoadAsync<TResult>(TKey id)
		{
			try
			{
				return JsonConvert.DeserializeObject<TResult>(await File.ReadAllTextAsync(Path.Combine(_StoragePath, id + ".json")));
			}
			catch
			{
				return default(TResult);
			}
		}

		public async Task DeleteAsync(TKey id)
		{
			await Task.Run(() =>
			{
				File.Move(Path.Combine(_StoragePath, id + ".json"), Path.Combine(_StoragePath, id + ".deleted"));
			});
		}

		public async Task<List<TEntity>> AllAsync()
		{
			var r = new List<TEntity>();
			var files = Directory.GetFiles(_StoragePath, "*.json", SearchOption.TopDirectoryOnly);
			var ids = new List<string>();
			if (files?.Any() == true)
			{
				ids = files.Select(x => Path.GetFileNameWithoutExtension(x))?.ToList() ?? new List<string>();
			}

			await ids.ForEachAsync(async id => r.Add(await LoadAsync((TKey)Convert.ChangeType(id, typeof(TKey)))));
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
			result.Count = rows?.Count() ?? 0;
			result.Rows = rows.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
			return result;
		}

		public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var rows = await FindAsync(predicate);
			return rows != null ? rows.FirstOrDefault() : default(TEntity);
		}

		public TKey Save(EasyStorableObject<TKey> obj)
		{
			return SaveAsync(obj).GetAwaiter().GetResult();
		}

		public TEntity Load(TKey id)
		{
			return LoadAsync(id).GetAwaiter().GetResult();
		}

		public void Delete(TKey id)
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
