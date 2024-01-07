using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class LoyaltyProgramRepository : GenericRepository<LoyaltyProgram>, ILoyaltyProgramRepository
    {
        public LoyaltyProgramRepository(PoSDBContext context) : base(context) { }
    }
}
