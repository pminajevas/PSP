using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(PoSDBContext context) : base(context) { }
    }
}
