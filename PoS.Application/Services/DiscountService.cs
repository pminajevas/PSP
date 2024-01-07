using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Models.Responses;
using PoS.Application.Models.Requests;
using PoS.Application.Mapper;
using PoS.Core.Entities;
using PoS.Application.Filters;
using AutoMapper;

namespace PoS.Services.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<DiscountResponse> AddDiscountAsync(DiscountRequest discountRequest)
        {
            var discount = _mapper.Map<Discount>(discountRequest);

            discount = await _discountRepository.InsertAsync(discount);

            return _mapper.Map<DiscountResponse>(discount);
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

            return _mapper.Map<List<DiscountResponse>>(discounts);
        }

        public async Task<DiscountResponse?> GetDiscountByIdAsync(Guid discountId)
        {
            var discount = await _discountRepository.GetByIdAsync(discountId);

            if (discount is not null)
            {
                return _mapper.Map<DiscountResponse>(discount); 
            }

            return null;
        }

        public async Task<DiscountResponse?> UpdateDiscountByIdAsync(Guid discountId, DiscountRequest discountUpdateRequest)
        {
            var discountUpdated = _mapper.Map<Discount>(discountUpdateRequest);
            discountUpdated.Id = discountId;

            discountUpdated = await _discountRepository.UpdateAsync(discountUpdated);

            if (discountUpdated is not null)
            {
                return _mapper.Map<DiscountResponse>(discountUpdated);
            }

            return null;
        }

        public async Task<bool> DeleteDiscountByIdAsync(Guid discountId)
        {
            return await _discountRepository.DeleteAsync(discountId);
        }
    }
}
