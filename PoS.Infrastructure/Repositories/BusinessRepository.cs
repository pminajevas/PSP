using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        public BusinessRepository(PoSDBContext context) : base(context) { }
    }
}
