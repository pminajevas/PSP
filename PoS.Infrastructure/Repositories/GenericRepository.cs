using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly PoSDBContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public GenericRepository(PoSDBContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<string>? requiredFields = null
        )
        {
            return QueryBase(filter, orderBy, requiredFields).ToListAsync();
        }

        public async Task<TEntity?> GetFirstAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            List<string>? requiredFields = null
        )
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (requiredFields != null)
            {
                query = requiredFields.Aggregate(query, (current, requiredField) => current.Include(requiredField));
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

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await DbSet.AnyAsync(filter);
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(object id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException();
            }

            DbSet.Remove(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();

            return entity;
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

        private IQueryable<TEntity> QueryBase(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            IReadOnlyCollection<string>? requiredFields = null
        )
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (requiredFields != null)
            {
                query = requiredFields.Aggregate(query, (current, requiredField) => current.Include(requiredField));
            }

            return orderBy != null ? orderBy(query) : query;
        }
    }
}
