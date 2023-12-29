﻿using PoS.Data.Context;
using PoS.Shared.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.Data.Repositories;
using PoS.Data;

namespace PoS.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(PoSDbContext dbContext)
        {
            _itemRepository = new ItemRepository(dbContext);
        }

        public async Task<Item?> GetItemByIdAsync(Guid itemId)
        {
            return await _itemRepository.GetItemByIdAsync(itemId);
        }
    }
}
