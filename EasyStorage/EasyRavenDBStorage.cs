using Raven.Client.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.Data
{
	public abstract class EasyRavenDBStorage<TEntity> : IEasyStorage<TEntity, string>
	{
		public EasyRavenDBStorage(string Url, string Database)
		{
			RavenDbDocumentStoreHolder.Init(Url, Database);
		}

		public async Task<string> SaveAsync(EasyStorableObject<string> obj)
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

		public async Task<TEntity> LoadAsync(string id)
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

		public async Task<TResult> LoadAsync<TResult>(string id)
		{
			try
			{
				using (var ctx = RavenDbDocumentStoreHolder.Store.OpenAsyncSession())
				{
					return await ctx.LoadAsync<TResult>(id);
				}
			}
			catch
			{
				return default(TResult);
			}
		}

		public void Delete(string id)
		{
			using (var ctx = RavenDbDocumentStoreHolder.Store.OpenSession())
			{
				var doc = ctx.Load<TEntity>(id);
				ctx.Delete(id);
				ctx.SaveChanges();
			}
		}

		public async Task<List<TEntity>> AllAsync()
		{
			var r = new List<TEntity>();
			await Task.Run(() =>
			{
				using (var ctx = RavenDbDocumentStoreHolder.Store.OpenAsyncSession())
				{
					r = ctx.Query<TEntity>()?.Take(9999)?.ToList() ?? new List<TEntity>();
				}
			});
			return r;
		}

		public async Task<List<TEntity>> FindAsync(Func<TEntity, bool> predicate)
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

		public async Task<List<TResult>> FindAsync<TResult>(Func<TEntity, bool> predicate)
		{
			var r = new List<TResult>();
			var list = await FindAsync(predicate);
			return list.Select<TEntity, TResult>(x => EasyMapper.MapTo<TResult>(x))?.ToList() ?? new List<TResult>();
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
	}


}
