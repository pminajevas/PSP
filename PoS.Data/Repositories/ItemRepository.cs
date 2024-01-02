using Microsoft.EntityFrameworkCore;
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

        public async Task<Item> CreateItemAsync(Item item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            var deletedItem = await _dbContext.Items.FindAsync(itemId);
            if(deletedItem != null)
            {
                _dbContext.Items.Remove(deletedItem);
            }else
            {
                return false;
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Item?> UpdateItemAsync(Guid itemId, Item itemUpdate)
        {
            var item = await _dbContext.Items.FindAsync(itemId);
            if(item != null)
            {
                _dbContext.Entry(item).CurrentValues.SetValues(itemUpdate);
                await _dbContext.SaveChangesAsync();
            }

            return item;
        }

    }
}
