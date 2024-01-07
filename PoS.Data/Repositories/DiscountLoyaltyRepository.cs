using Microsoft.EntityFrameworkCore;
using PoS.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.Data.Repositories.Interfaces;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;

namespace PoS.Data.Repositories
{
    public class DiscountLoyaltyRepository : IDiscountLoyaltyRepository
    {
        private readonly PoSDbContext _dbContext;

        public DiscountLoyaltyRepository(PoSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Discount> CreateDiscountAsync(Discount discount)
        {
            _dbContext.Discounts.Add(discount);
            await _dbContext.SaveChangesAsync();

            return discount;
        }

        public async Task<IEnumerable<Discount>> GetDiscountsAsync(
            int skipItems,
            int takeItems,
            Expression<Func<Discount, bool>>? filter = null,
            Func<IQueryable<Discount>, IOrderedQueryable<Discount>>? orderBy = null
        )
        {
            IQueryable<Discount> discountsQuery = _dbContext.Discounts;

            if (filter != null)
            {
                discountsQuery = discountsQuery.Where(filter);
            }

            if (orderBy != null)
            {
                discountsQuery = orderBy(discountsQuery);
            }

            discountsQuery = discountsQuery.Skip(skipItems).Take(takeItems);

            return await discountsQuery.ToListAsync();
        }

        public async Task<Discount?> GetDiscountById(Guid id)
        {
            return await _dbContext.Discounts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Discount?> UpdateDiscountAsync(Guid id, Discount updatedDiscount)
        {
            var discount = await _dbContext.Discounts.FindAsync(id);

            updatedDiscount.Id = id;

            if (discount != null)
            {
                _dbContext.Entry(discount).CurrentValues.SetValues(updatedDiscount);
                await _dbContext.SaveChangesAsync();

                return await _dbContext.Discounts.FindAsync(id);
            }

            return null;
        }

        public async Task<bool> DeleteDiscountByIdAsync(Guid id)
        {
            var discount = await _dbContext.Discounts.FindAsync(id);

            if (discount != null)
            {
                _dbContext.Discounts.Remove(discount);
            }
            else
            {
                throw new Exception($"Discount with id - ${id} does not exist.");
            }

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
