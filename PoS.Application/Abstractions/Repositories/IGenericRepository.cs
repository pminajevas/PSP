using System.Linq.Expressions;

namespace PoS.Application.Abstractions.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity> InsertAsync(TEntity entity);

        public Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null
        );

        public Task<TEntity?> GetFirstAsync(
            Expression<Func<TEntity, bool>>? filter = null
        );

        public Task<TEntity?> GetByIdAsync(object id);

        public Task<bool> Exists(Expression<Func<TEntity, bool>> filter);

        public Task<bool> DeleteAsync(object id);

        public Task DeleteAsync(TEntity entity);

        public Task<TEntity> UpdateAsync(TEntity entity);

        public int Count(Expression<Func<TEntity, bool>>? filter = null);
    }
}
