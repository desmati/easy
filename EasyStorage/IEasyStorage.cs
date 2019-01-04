using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace System.Data
{
	public interface IEasyStorage<TEntity, TEntityId>
	{
		Task<TEntityId> SaveAsync(EasyStorableObject<TEntityId> obj);

		Task<TEntity> LoadAsync(TEntityId id);

		Task DeleteAsync(TEntityId id);

		Task<List<TEntity>> AllAsync();

		Task<EasyPaging<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber = 1, int pageSize = 20);
		Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

		TEntityId Save(EasyStorableObject<TEntityId> obj);

		TEntity Load(TEntityId id);

		void Delete(TEntityId id);

		List<TEntity> All();

		EasyPaging<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageNumber = 1, int pageSize = 20);
		List<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

		TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
	}
}
