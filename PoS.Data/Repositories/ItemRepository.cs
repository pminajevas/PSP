using PoS.Data.Context;
using PoS.Shared.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Data.Repositories
{
    public class ItemRepository
    {

        private readonly PoSDbContext _dbContext;

        public ItemRepository(PoSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Item?> GetItemByIdAsync(Guid id)
        {
            return await _dbContext.Items.FindAsync(id);
        }

    }
}
