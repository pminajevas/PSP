using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PoS.Application.Models.Requests;
using PoS.Application.Filters;
using Microsoft.AspNetCore.Authorization;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;

namespace PoS.Controllers
{
    [ApiController]
    public class DiscountLoyaltyController : ControllerBase
    {
        private IDiscountService _discountService;
        private ILoyaltyService _loyaltyService;
        private ICouponService _couponService;

        public DiscountLoyaltyController(
            IDiscountService discountService,
            ILoyaltyService loyaltyService,
            ICouponService couponService)
        {
            _discountService = discountService;
            _loyaltyService = loyaltyService;
            _couponService = couponService;
        }

        [HttpPost]
        [Route("/DiscountLoyalty/Discount")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountRequest discountRequest)
        {
            var newDiscount = await _discountService.AddDiscountAsync(discountRequest);

            return CreatedAtAction("GetDiscount", new { discountId = newDiscount.Id }, newDiscount);
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Discounts")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetDiscounts([FromQuery] DiscountFilter filter)
        {
            return Ok(await _discountService.GetDiscountsAsync(filter));
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        [ActionName("GetDiscount")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetDiscountById([FromRoute] Guid discountId)
        {
            return Ok(await _discountService.GetDiscountByIdAsync(discountId));
        }

        [HttpPut]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateDiscountById([FromRoute][Required] Guid discountId, [FromBody] DiscountRequest discountRequest)
        {
            return Ok(await _discountService.UpdateDiscountByIdAsync(discountId, discountRequest));
        }

        [HttpDelete]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteDiscountById([FromRoute] Guid discountId)
        {
            await _discountService.DeleteDiscountByIdAsync(discountId);

            return NoContent();
        }

        [HttpPost]
        [Route("/DiscountLoyalty/LoyaltyProgram")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateLoyaltyProgram([FromBody] LoyaltyProgramRequest loyaltyProgramRequest)
        {
            var newLoyaltyProgram = await _loyaltyService.AddLoyaltyAsync(loyaltyProgramRequest);

            return CreatedAtAction("GetDiscount", new { discountId = newLoyaltyProgram.Id }, newLoyaltyProgram);
        }

        [HttpGet]
        [Route("/DiscountLoyalty/LoyaltyPrograms")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetLoyaltyPrograms([FromQuery] LoyaltyFilter loyaltyFilter)
        {
            return Ok(await _loyaltyService.GetLoyaltysAsync(loyaltyFilter));
        }

        [HttpGet]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        [ActionName("GetLoyaltyProgram")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetLoyaltyProgramById([FromRoute][Required] Guid loyaltyProgramId)
        {
            return Ok(await _loyaltyService.GetLoyaltyByIdAsync(loyaltyProgramId));
        }

        [HttpPut]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateLoyaltyProgramById([FromRoute][Required] Guid loyaltyProgramId, [FromBody] LoyaltyProgramRequest loyaltyProgramRequest)
        {
            return Ok(await _loyaltyService.UpdateLoyaltyByIdAsync(loyaltyProgramId, loyaltyProgramRequest));
        }

        [HttpDelete]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteLoyaltyProgramById([FromRoute][Required] Guid loyaltyProgramId)
        {
            await _loyaltyService.DeleteLoyaltyByIdAsync(loyaltyProgramId);

            return NoContent();
        }

        [HttpPost]
        [Route("/DiscountLoyalty/Coupon")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponRequest couponRequest)
        {
            var newCoupon = await _couponService.AddCouponAsync(couponRequest);

            return CreatedAtAction("GetCoupon", new { couponId = newCoupon.Id }, newCoupon);
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Coupons")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetCoupons([FromQuery] CouponFilter couponFilter)
        {
            return Ok(await _couponService.GetCouponsAsync(couponFilter));
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        [ActionName("GetCoupon")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetCouponById([FromRoute][Required] Guid couponId)
        {
            return Ok(await _couponService.GetCouponByIdAsync(couponId));
        }

        [HttpPut]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateCouponById([FromRoute][Required] Guid couponId, [FromBody] CouponRequest couponRequest)
        {
            return Ok(await _couponService.UpdateCouponByIdAsync(couponId, couponRequest));
        }

        [HttpDelete]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteCouponById([FromRoute][Required]Guid couponId)
        {
            await _couponService.DeleteCouponByIdAsync(couponId);

            return NoContent();
        }
    }
}
