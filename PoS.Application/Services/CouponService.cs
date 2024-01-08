using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Application.Services
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponService(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        public async Task<CouponResponse> AddCouponAsync(CouponRequest couponRequest)
        {
            var coupon = _mapper.Map<Coupon>(couponRequest);

            return _mapper.Map<CouponResponse>(await _couponRepository.InsertAsync(coupon));
        }

        public async Task<bool> DeleteCouponByIdAsync(Guid couponId)
        {
            if (await _couponRepository.DeleteAsync(couponId))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Coupon with id - {couponId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<CouponResponse?> GetCouponByIdAsync(Guid couponId)
        {
            var coupon = await _couponRepository.GetByIdAsync(couponId);

            if (coupon is null)
            {
                throw new PoSException($"Coupon with id - {couponId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<CouponResponse>(coupon);
        }

        public async Task<List<CouponResponse>> GetCouponsAsync(CouponFilter filter)
        {
            var couponFilter = PredicateBuilder.True<Coupon>();
            Func<IQueryable<Coupon>, IOrderedQueryable<Coupon>>? orderByCoupons = null;

            if (filter.BusinessId != null)
            {
                couponFilter = couponFilter.And(x => x.BusinessId == filter.BusinessId);
            }

            if (filter.Validity != null)
            {
                couponFilter = couponFilter.And(x => x.Validity == filter.Validity);
            }

            if (filter.ValidUntil != null)
            {
                couponFilter = couponFilter.And(x => x.ValidUntil >= filter.ValidUntil);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByCoupons = x => x.OrderByDescending(p => EF.Property<Coupon>(p, filter.OrderBy));
                        break;
                    default:
                        orderByCoupons = x => x.OrderBy(p => EF.Property<Coupon>(p, filter.OrderBy));
                        break;
                }
            }

            var coupons = await _couponRepository.GetAsync(
                couponFilter,
                orderByCoupons,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return _mapper.Map<List<CouponResponse>>(coupons);
        }

        public async Task<CouponResponse?> UpdateCouponByIdAsync(Guid couponId, CouponRequest couponRequest)
        {
            var couponUpdated = _mapper.Map<Coupon>(couponRequest);
            couponUpdated.Id = couponId;

            couponUpdated = await _couponRepository.UpdateAsync(couponUpdated);

            return _mapper.Map<CouponResponse>(couponUpdated);
        }
    }
}
