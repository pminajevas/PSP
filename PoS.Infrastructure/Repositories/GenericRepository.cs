using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Infrastructure.Context;
using System.Linq.Expressions;

namespace PoS.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly PoSDBContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(PoSDBContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? itemsToSkip = null,
            int? itemsToTake = null
        )
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (itemsToSkip != null)
            {
                query = query.Skip((int)itemsToSkip);
            }

            if (itemsToTake != null)
            {
                query = query.Take((int)itemsToTake);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetFirstAsync(
            Expression<Func<TEntity, bool>>? filter = null
        )
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            TEntity? entity;

            try
            {
                entity = await query.FirstAsync();
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await DbSet.AnyAsync(filter);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public int Count(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
            {
                return DbSet.Count(filter);
            }

            return DbSet.Count();
        }
    }
}
