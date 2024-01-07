using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Services.Services
{
    public interface IDiscountService
    {
        public Task<DiscountResponse> AddDiscountAsync(DiscountRequest discountRequest);

        public Task<List<DiscountResponse>> GetDiscountsAsync(DiscountFilter filter);

        public Task<DiscountResponse?> GetDiscountByIdAsync(Guid discountId);

        public Task<DiscountResponse?> UpdateDiscountByIdAsync(Guid discountId, DiscountUpdateRequest discountUpdateRequest);

        public Task<bool> DeleteDiscountByIdAsync(Guid discountId);
    }
}
