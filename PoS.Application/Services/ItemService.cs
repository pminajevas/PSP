using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Services.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _businessRepository;
        private readonly IDiscountRepository _discountRepository;

        public ItemService(
            IItemRepository itemRepository,
            IMapper mapper,
            IBusinessRepository businessRepository,
            IDiscountRepository discountRepository)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _businessRepository = businessRepository;
            _discountRepository = discountRepository;
        }

        public async Task<ItemResponse> CreateItemAsync(ItemRequest itemRequest)
        {
            var item = _mapper.Map<Item>(itemRequest);

            if (!await _businessRepository.Exists(x => x.Id == item.BusinessId))
            {
                throw new PoSException($"Business with id - {item.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (item.DiscountId != null)
            {
                if (!await _discountRepository.Exists(x => x.Id == item.DiscountId))
                {
                    throw new PoSException($"Discount with id - {item.DiscountId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }

            if (await _itemRepository.Exists(x => x.ItemName == item.ItemName && x.BusinessId == item.BusinessId))
            {
                throw new PoSException($"Item with name - {item.ItemName} and business id - {item.BusinessId} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            item.Price = Math.Round(item.Price, 2);

            return _mapper.Map<ItemResponse>(await _itemRepository.InsertAsync(item));
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

        public async Task<ItemResponse?> GetItemByIdAsync(Guid itemId)
        {
            var item =  await _itemRepository.GetByIdAsync(itemId);

            if (item is null)
            {
                throw new PoSException($"Item with id - {itemId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<ItemResponse>(item);
        }

        public async Task<IEnumerable<ItemResponse>> GetItemsAsync(ItemsFilter filter)
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

            return _mapper.Map<IEnumerable<ItemResponse>>(businesses);
        }

        public async Task<ItemResponse?> UpdateItemAsync(Guid itemId, ItemRequest itemUpdate)
        {
            var item = _mapper.Map<Item>(itemUpdate);
            item.Id = itemId;

            if (itemUpdate.DiscountId != null)
            {
                if (!await _discountRepository.Exists(x => x.Id == itemUpdate.DiscountId))
                {
                    throw new PoSException($"Discount with id - {itemUpdate.DiscountId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }

            var oldItem = await _itemRepository.GetFirstAsync(x => x.Id == itemId) ??
                throw new PoSException($"Item with id - {itemId} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldItem.ItemName != item.ItemName || oldItem.BusinessId != item.BusinessId)
            {
                if (await _itemRepository.Exists(x => x.ItemName == item.ItemName && x.BusinessId == item.BusinessId))
                {
                    throw new PoSException($"Item with name - {item.ItemName} and business id - {item.BusinessId} already exists",
                        System.Net.HttpStatusCode.BadRequest);
                }
            }
            
            if (oldItem.Price != itemUpdate.Price)
            {
                itemUpdate.Price = Math.Round(itemUpdate.Price, 2);
            }

            return _mapper.Map<ItemResponse>(await _itemRepository.UpdateAsync(item));
        }
    }
}
