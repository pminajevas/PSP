using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Models.Responses;
using PoS.Application.Models.Requests;
using PoS.Application.Mapper;
using PoS.Core.Entities;
using PoS.Application.Filters;

namespace PoS.Services.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<DiscountResponse> AddDiscountAsync(DiscountRequest discountRequest)
        {
            var discount = Mapping.Mapper.Map<DiscountRequest, Discount>(discountRequest);

            discount = await _discountRepository.InsertAsync(discount);

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

            var discounts = await _discountRepository.GetAsync(
                discountFilter,
                orderByDiscount,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return Mapping.Mapper.Map<IEnumerable<Discount>, List<DiscountResponse>>(discounts);
        }

        public async Task<DiscountResponse?> GetDiscountByIdAsync(Guid discountId)
        {
            var discount = await _discountRepository.GetByIdAsync(discountId);

            if (discount is not null)
            {
                return Mapping.Mapper.Map<Discount, DiscountResponse>(discount); 
            }

            return null;
        }

        public async Task<DiscountResponse?> UpdateDiscountByIdAsync(Guid discountId, DiscountUpdateRequest discountUpdateRequest)
        {
            var discountUpdated = Mapping.Mapper.Map<DiscountUpdateRequest, Discount>(discountUpdateRequest);
            discountUpdated.Id = discountId;

            discountUpdated = await _discountRepository.UpdateAsync(discountUpdated);

            if (discountUpdated is not null)
            {
                return Mapping.Mapper.Map<Discount, DiscountResponse>(discountUpdated);
            }

            return null;
        }

        public async Task<bool> DeleteDiscountByIdAsync(Guid discountId)
        {
            return await _discountRepository.DeleteAsync(discountId);
        }
    }
}
