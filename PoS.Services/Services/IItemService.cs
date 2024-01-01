using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.Shared.ResponseDTOs;
using PoS.Data;

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
