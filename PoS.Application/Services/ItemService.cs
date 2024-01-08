using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            if (await _itemRepository.Exists(x => x.ItemName == item.ItemName && x.BusinessId == item.BusinessId))
            {
                throw new PoSException($"Item with name - {item.ItemName} and business id - {item.BusinessId} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            return await _itemRepository.InsertAsync(item);
        }

        public async Task<bool> DeleteItemAsync(Guid itemId)
        {
            if (await _itemRepository.DeleteAsync(itemId))
            {
                return true;
            }
            {
                throw new PoSException($"Item with id - {itemId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<Item?> GetItemByIdAsync(Guid itemId)
        {
            var item =  await _itemRepository.GetByIdAsync(itemId);

            if (item is null)
            {
                throw new PoSException($"Item with id - {itemId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(ItemsFilter filter)
        {
            var itemFilter = PredicateBuilder.True<Item>();
            Func<IQueryable<Item>, IOrderedQueryable<Item>>? orderByItems = null;

            if (filter.BusinessId != null)
            {
                itemFilter = itemFilter.And(x => x.BusinessId == filter.BusinessId);
            }

            if (filter.DiscountId != null)
            {
                itemFilter = itemFilter.And(x => x.DiscountId == filter.DiscountId);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByItems = x => x.OrderByDescending(p => EF.Property<Item>(p, filter.OrderBy));
                        break;
                    default:
                        orderByItems = x => x.OrderBy(p => EF.Property<Item>(p, filter.OrderBy));
                        break;
                }
            }

            var businesses = await _itemRepository.GetAsync(
                itemFilter,
                orderByItems,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return businesses;
        }

        public async Task<Item?> UpdateItemAsync(Guid itemId, Item itemUpdate)
        {
            itemUpdate.Id = itemId;

            if (await _itemRepository.Exists(x => x.ItemName == itemUpdate.ItemName && x.BusinessId == itemUpdate.BusinessId))
            {
                throw new PoSException($"Item with name - {itemUpdate.ItemName} and business id - {itemUpdate.BusinessId} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            itemUpdate = await _itemRepository.UpdateAsync(itemUpdate);

            return itemUpdate;
        }
    }
}
