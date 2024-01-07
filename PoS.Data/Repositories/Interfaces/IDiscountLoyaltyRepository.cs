using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Data.Repositories.Interfaces
{
    public interface IDiscountLoyaltyRepository
    {
        public Task<Discount> CreateDiscountAsync(Discount discount);

        public Task<IEnumerable<Discount>> GetDiscountsAsync(
            int skipItems,
            int takeItems,
            Expression<Func<Discount, bool>>? filter = null,
            Func<IQueryable<Discount>, IOrderedQueryable<Discount>>? orderBy = null
        );

        public Task<Discount?> GetDiscountById(Guid id);

        public Task<Discount?> UpdateDiscountAsync(Guid id, Discount discount);

        public Task<bool> DeleteDiscountByIdAsync(Guid id);
    }
}
