using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(PoSDBContext context) : base(context) { }
    }
}
