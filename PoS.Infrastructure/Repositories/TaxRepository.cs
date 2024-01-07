using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class TaxRepository : GenericRepository<Tax>, ITaxRepository
    {
        public TaxRepository(PoSDBContext context) : base(context) { }
    }
}
