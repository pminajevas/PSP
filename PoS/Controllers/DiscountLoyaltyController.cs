using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PoS.Services.Services;
using PoS.Application.Models.Requests;
using PoS.Application.Filters;

namespace PoS.Controllers
{
    [ApiController]
    public class DiscountLoyaltyController : ControllerBase
    {
        private IDiscountService _discountLoyaltyService;

        public DiscountLoyaltyController(IDiscountService discountLoyaltyService)
        {
            _discountLoyaltyService = discountLoyaltyService;
        }

        [HttpPost]
        [Route("/DiscountLoyalty/Discount")]
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountRequest discountRequest)
        {
            return CreatedAtAction("CreateDiscount", await _discountLoyaltyService.AddDiscountAsync(discountRequest));
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Discounts")]
        public async Task<IActionResult> GetDiscounts([FromQuery] DiscountFilter filter)
        {
            return Ok(await _discountLoyaltyService.GetDiscountsAsync(filter));
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        public async Task<IActionResult> GetDiscountById([FromRoute] Guid discountId)
        {
            return Ok(await _discountLoyaltyService.GetDiscountByIdAsync(discountId));
        }

        [HttpPut]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        public async Task<IActionResult> UpdateDiscountById([FromRoute][Required] Guid discountId, [FromBody] DiscountUpdateRequest discountRequest)
        {
            return Ok(await _discountLoyaltyService.UpdateDiscountByIdAsync(discountId, discountRequest));
        }

        [HttpDelete]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        public async Task<IActionResult> DeleteDiscountById([FromRoute] Guid discountId)
        {
            if (await _discountLoyaltyService.DeleteDiscountByIdAsync(discountId))
            {
                return NoContent();
            }

            return Problem();
        }

        [HttpPost]
        [Route("/DiscountLoyalty/LoyaltyProgram")]
        public async Task<IActionResult> CreateLoyaltyProgram([FromBody] LoyaltyProgramRequest loyaltyProgramRequest)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/DiscountLoyalty/LoyaltyPrograms")]
        public async Task<IActionResult> GetLoyaltyPrograms([FromQuery] Guid? businessId, [FromQuery] string orderBy, [FromQuery] string sorting, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        public async Task<IActionResult> GetLoyaltyProgramById([FromRoute][Required] Guid? loyaltyProgramId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        public async Task<IActionResult> UpdateLoyaltyProgramById([FromRoute][Required] string loyaltyProgramId, [FromBody] LoyaltyProgramRequest loyaltyProgramRequest)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        public async Task<IActionResult> DeleteLoyaltyProgramById([FromRoute][Required] Guid? loyaltyProgramId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/DiscountLoyalty/Coupon")]
        public async Task<IActionResult> CreateCoupon([FromBody] CouponRequest couponRequest)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Coupons")]
        public async Task<IActionResult> GetCoupons([FromQuery] Guid? businessId, [FromQuery] string validity, [FromQuery] DateTime? validUntil, [FromQuery] string orderBy, [FromQuery] string sorting, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        public async Task<IActionResult> GetCouponById([FromRoute][Required] Guid? couponId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        public async Task<IActionResult> UpdateCouponById([FromRoute][Required] string couponId, [FromBody] CouponRequest couponRequest)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        public async Task<IActionResult> DeleteCouponById([FromRoute][Required]Guid couponId)
        { 
            throw new NotImplementedException();
        }
    }
}
