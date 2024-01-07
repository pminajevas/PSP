using PoS.Services.Filters;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
