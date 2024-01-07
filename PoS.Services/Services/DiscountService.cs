using PoS.Data.Mapper;
using PoS.Data;
using PoS.Data.Repositories.Interfaces;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoS.Shared.Utilities;
using PoS.Services.Filters;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace PoS.Services.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountLoyaltyRepository _discountLoyaltyRepository;

        public DiscountService(IDiscountLoyaltyRepository discountLoyaltyRepository)
        {
            _discountLoyaltyRepository = discountLoyaltyRepository;
        }

        public async Task<DiscountResponse> AddDiscountAsync(DiscountRequest discountRequest)
        {
            var discount = Mapping.Mapper.Map<DiscountRequest, Discount>(discountRequest);

            discount = await _discountLoyaltyRepository.CreateDiscountAsync(discount);

            return Mapping.Mapper.Map<Discount, DiscountResponse>(discount);
        }

        public async Task<List<DiscountResponse>> GetDiscountsAsync(DiscountFilter filter)
        {
            var discountFilter = PredicateBuilder.True<Discount>();
            Func<IQueryable<Discount>, IOrderedQueryable<Discount>>? orderByDiscount = null;

            if (filter.ValidUntil != null)
            {
                discountFilter = discountFilter.And(x => x.ValidUntil >= filter.ValidUntil);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByDiscount = x => x.OrderByDescending(p => EF.Property<Discount>(p, filter.OrderBy));
                        break;
                    default:
                        orderByDiscount = x => x.OrderBy(p => EF.Property<Discount>(p, filter.OrderBy));
                        break;
                }
            }

            var discounts = await _discountLoyaltyRepository.GetDiscountsAsync(
                filter.ItemsToSkip(),
                filter.PageSize,
                discountFilter,
                orderByDiscount
            );

            return Mapping.Mapper.Map<IEnumerable<Discount>, List<DiscountResponse>>(discounts);
        }

        public async Task<DiscountResponse?> GetDiscountByIdAsync(Guid discountId)
        {
            var discount = await _discountLoyaltyRepository.GetDiscountById(discountId);

            if (discount is not null)
            {
                return Mapping.Mapper.Map<Discount, DiscountResponse>(discount); 
            }

            return null;
        }

        public async Task<DiscountResponse?> UpdateDiscountByIdAsync(Guid discountId, DiscountUpdateRequest discountUpdateRequest)
        {
            var updatedDiscount = await _discountLoyaltyRepository.UpdateDiscountAsync(
                discountId,
                Mapping.Mapper.Map<DiscountUpdateRequest, Discount>(discountUpdateRequest)
            );

            if (updatedDiscount is not null)
            {
                return Mapping.Mapper.Map<Discount, DiscountResponse>(updatedDiscount);
            }

            return null;
        }

        public async Task<bool> DeleteDiscountByIdAsync(Guid discountId)
        {
            return await _discountLoyaltyRepository.DeleteDiscountByIdAsync(discountId);
        }
    }
}
