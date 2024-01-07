using PoS.Core.Entities;

namespace PoS.Services.Services
{
    public interface IItemService
    {
        Task<Item?> GetItemByIdAsync(Guid itemId);
        Task<Item?> CreateItemAsync(Item item);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<bool> DeleteItemAsync(Guid itemId);
        Task<Item?> UpdateItemAsync(Guid itemId, Item itemUpdate);
    }
}
