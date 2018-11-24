using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Data
{
	public interface IEasyStorage<TEntity, TEntityId>
	{
		Task<TEntityId> SaveAsync(IEasyStorableObject<TEntityId> obj);

		Task<TEntity> LoadAsync(TEntityId id);

		Task<TResult> LoadAsync<TResult>(TEntityId id);

		void Delete(TEntityId id);

		Task<List<TEntity>> AllAsync();

		Task<List<TEntity>> FindAsync(Func<TEntity, bool> predicate);
	}
}
