using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace System.Data
{
	public abstract class EasyJsonFileStorage<TEntity> : IEasyStorage<TEntity, Guid>
	{
		private string _StoragePath { get; set; }
		public EasyJsonFileStorage(string StoragePath)
		{
			_StoragePath = StoragePath;
			try { if (!Directory.Exists(StoragePath)) { Directory.CreateDirectory(StoragePath); } } catch { }
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

			await File.WriteAllTextAsync(Path.Combine(_StoragePath, obj.Id + ".json"), JsonConvert.SerializeObject(obj));
			return obj.Id;
		}

		public async Task<TEntity> LoadAsync(Guid id)
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

		public async Task<TResult> LoadAsync<TResult>(Guid id)
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

		public void Delete(Guid id)
		{
			File.Move(Path.Combine(_StoragePath, id + ".json"), Path.Combine(_StoragePath, id + ".deleted"));
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

			await ids.ForEachAsync(async id => r.Add(await LoadAsync(Guid.Parse(id))));
			return r;
		}

		public async Task<List<TEntity>> FindAsync(Func<TEntity, bool> predicate)
		{
			return (await AllAsync() ?? new List<TEntity>())?.Where(predicate)?.ToList() ?? new List<TEntity>();
		}
	}
}
