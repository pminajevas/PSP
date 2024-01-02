using PoS.Data.Context;
using PoS.Shared.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.Data.Repositories;
using PoS.Data;
using Microsoft.EntityFrameworkCore;

namespace PoS.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(PoSDbContext dbContext)
        {
            _itemRepository = new ItemRepository(dbContext);
        }

        public async Task<Item?> CreateItemAsync(Item item)
        {
            return await _itemRepository.CreateItemAsync(item);
        }

        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            return await _itemRepository.DeleteItemAsync(itemId);
        }

        public async Task<Item?> GetItemByIdAsync(Guid itemId)
        {
            return await _itemRepository.GetItemByIdAsync(itemId);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemRepository.GetItemsAsync();
        }

        public async Task<Item?> UpdateItemAsync(Guid itemId, Item itemUpdate)
        {
            return await _itemRepository.UpdateItemAsync(itemId, itemUpdate);
        }
    }
}
