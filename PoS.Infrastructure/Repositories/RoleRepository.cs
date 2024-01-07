using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(PoSDBContext context) : base(context) { }
    }
}
