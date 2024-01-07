using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;

namespace PoS.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Item?> CreateItemAsync(Item item)
        {
            return await _itemRepository.InsertAsync(item);
        }

        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            return await _itemRepository.DeleteAsync(itemId);
        }

        public async Task<Item?> GetItemByIdAsync(Guid itemId)
        {
            return await _itemRepository.GetByIdAsync(itemId);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _itemRepository.GetAsync();
        }

        public async Task<Item?> UpdateItemAsync(Guid itemId, Item itemUpdate)
        {
            itemUpdate.Id = itemId;

            return await _itemRepository.UpdateAsync(itemUpdate);
        }
    }
}
