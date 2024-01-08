using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{ 
    public interface ICouponService
    {
        public Task<CouponResponse> AddCouponAsync(CouponRequest couponRequest);

        public Task<List<CouponResponse>> GetCouponsAsync(CouponFilter filter);

        public Task<CouponResponse?> GetCouponByIdAsync(Guid couponId);

        public Task<CouponResponse?> UpdateCouponByIdAsync(Guid couponId, CouponRequest couponRequest);

        public Task<bool> DeleteCouponByIdAsync(Guid couponId);
    }
}
