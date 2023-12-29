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
    }
}
