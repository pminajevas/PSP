using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Core.Exceptions;
using PoS.Infrastructure.Context;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Reflection;

namespace PoS.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly IPoSDBContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(IPoSDBContext context)
        {
            Context = context;
            DbSet = context.Instance.Set<TEntity>();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.Instance.SaveChangesAsync();

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
            await Context.Instance.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.Instance.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            PropertyInfo idProp = entity.GetType().GetProperty("Id") ?? throw new PoSException("Internal error. Code 99", System.Net.HttpStatusCode.InternalServerError);

            var currentState = await DbSet.FindAsync(idProp.GetValue(entity)) ?? throw new PoSException("Internal error. Code 98", System.Net.HttpStatusCode.InternalServerError);

            Context.Instance.Entry(currentState).CurrentValues.SetValues(entity);

            await Context.Instance.SaveChangesAsync();

            return currentState;
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
