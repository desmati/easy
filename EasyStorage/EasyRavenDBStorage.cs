using Raven.Client.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace System.Data
{
	public abstract class EasyRavenDBStorage<TEntity> : IEasyStorage<TEntity, string>
	{
		public EasyRavenDBStorage(string Url, string Database)
		{
			RavenDbDocumentStoreHolder.Init(Url, Database);
		}

		public virtual async Task<string> SaveAsync(EasyStorableObject<string> obj)
		{
			if (obj == null)
			{
				return string.Empty;
			}

			if (obj.Id?.Trim() == "")
			{
				obj.Id = null;
			}

			using (var ctx = RavenDbDocumentStoreHolder.Store.OpenAsyncSession())
			{
				await ctx.StoreAsync(obj);
				await ctx.SaveChangesAsync();
			}
			return obj.Id;
		}

		public virtual async Task<TEntity> LoadAsync(string id)
		{
			try
			{
				using (var ctx = RavenDbDocumentStoreHolder.Store.OpenAsyncSession())
				{
					return await ctx.LoadAsync<TEntity>(id);
				}
			}
			catch
			{
				return default(TEntity);
			}
		}

		public virtual async Task DeleteAsync(string id)
		{
			using (var ctx = RavenDbDocumentStoreHolder.Store.OpenAsyncSession())
			{
				var doc = ctx.LoadAsync<TEntity>(id);
				ctx.Delete(id);
				await ctx.SaveChangesAsync();
			}
		}

		public virtual async Task<List<TEntity>> AllAsync()
		{
			var r = new List<TEntity>();
			await Task.Run(() =>
			{
				using (var ctx = RavenDbDocumentStoreHolder.Store.OpenSession())
				{
					r = ctx.Query<TEntity>()?.Take(9999)?.ToList() ?? new List<TEntity>();
				}
			});
			return r;
		}

		public virtual async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var r = new List<TEntity>();
			await Task.Run(() =>
			{
				using (var ctx = RavenDbDocumentStoreHolder.Store.OpenSession())
				{
					r = ctx.Query<TEntity>().Where(predicate)?.Take(9999)?.ToList() ?? new List<TEntity>();
				}
			});
			return r;
		}

		public virtual async Task<EasyPaging<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber = 1, int pageSize = 20)
		{
			var r = new EasyPaging<TEntity>(pageNumber, pageSize);
			var list = await FindAsync(predicate);
			r.Count = list.Count;
			r.Rows = list
				?.Skip((pageNumber - 1) * pageSize)
				?.Take(pageSize)
				?.ToList()
			?? new List<TEntity>();
			return r;
		}

		public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var rows = await FindAsync(predicate);
			return rows != null ? rows.FirstOrDefault() : default(TEntity);
		}

		public virtual string Save(EasyStorableObject<string> obj)
		{
			return SaveAsync(obj).GetAwaiter().GetResult();
		}

		public virtual TEntity Load(string id)
		{
			return LoadAsync(id).GetAwaiter().GetResult();
		}

		public virtual void Delete(string id)
		{
			DeleteAsync(id).Wait();
		}

		public virtual List<TEntity> All()
		{
			return AllAsync().GetAwaiter().GetResult();
		}

		public virtual EasyPaging<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageNumber = 1, int pageSize = 20)
		{
			return FindAsync(predicate, pageNumber, pageSize).GetAwaiter().GetResult();
		}

		public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return FindAsync(predicate).GetAwaiter().GetResult();
		}

		public IDocumentStore Store => RavenDbDocumentStoreHolder.Store;

		private class RavenDbDocumentStoreHolder
		{
			private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

			public static void Init(string Url, string Database)
			{
				if (!inited)
				{
					url = Url;
					database = Database;
					inited = true;
				}
			}
			private static string url { get; set; }
			private static string database { get; set; }
			private static bool inited { get; set; } = false;

			public static IDocumentStore Store => store.Value;

			private static IDocumentStore CreateStore()
			{
				IDocumentStore store = new DocumentStore()
				{
					Urls = new[] { url },
					Database = database
				}.Initialize();

				return store;
			}
		}

		public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
		{
			return FirstOrDefaultAsync(predicate).GetAwaiter().GetResult();
		}
	}
}
