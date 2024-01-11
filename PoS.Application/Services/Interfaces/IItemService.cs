using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Core.Entities;

namespace PoS.Application.Services.Interfaces
{
    public interface IItemService
    {
        Task<ItemResponse?> GetItemByIdAsync(Guid itemId);
        Task<ItemResponse> CreateItemAsync(ItemRequest itemRequest);
        Task<IEnumerable<ItemResponse>> GetItemsAsync(ItemsFilter itemsFilter);
        Task<bool> DeleteItemAsync(Guid itemId);
        Task<ItemResponse?> UpdateItemAsync(Guid itemId, ItemRequest itemUpdate);
    }
}
