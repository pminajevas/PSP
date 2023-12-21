/*
 * PoS
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;

namespace PoS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DiscountLoyaltyApiController : ControllerBase
    { 
        /*/// <summary>
        /// 
        /// </summary>
        /// <param name="couponId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        public virtual IActionResult DiscountLoyaltyCouponCouponIdDelete([FromRoute][Required]Guid? couponId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couponId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(CouponDTO), description: "Success")]
        public virtual IActionResult DiscountLoyaltyCouponCouponIdGet([FromRoute][Required]Guid? couponId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Coupon));
            string exampleJson = null;
            exampleJson = "{\n  \"amount\" : 0.8008281904610115,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validity\" : \"True\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Coupon>(exampleJson)
                        : default(Coupon);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="couponId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/DiscountLoyalty/Coupon/{couponId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(CouponDTO), description: "Success")]
        public virtual IActionResult DiscountLoyaltyCouponCouponIdPut([FromRoute][Required]string couponId, [FromBody] CouponDTO body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Coupon));
            string exampleJson = null;
            exampleJson = "{\n  \"amount\" : 0.8008281904610115,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validity\" : \"True\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Coupon>(exampleJson)
                        : default(Coupon);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/DiscountLoyalty/Coupon")]
        [SwaggerResponse(statusCode: 201, type: typeof(CouponDTO), description: "Created")]
        public virtual IActionResult DiscountLoyaltyCouponPost([FromBody] CouponDTO body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Coupon));
            string exampleJson = null;
            exampleJson = "{\n  \"amount\" : 0.8008281904610115,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validity\" : \"True\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Coupon>(exampleJson)
                        : default(Coupon);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="validity"></param>
        /// <param name="validUntil"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/DiscountLoyalty/Coupons")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Coupon>), description: "Success")]
        public virtual IActionResult DiscountLoyaltyCouponsGet([FromQuery]Guid? businessId, [FromQuery]string validity, [FromQuery]DateTime? validUntil, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Coupon>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"amount\" : 0.8008281904610115,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validity\" : \"True\"\n}, {\n  \"amount\" : 0.8008281904610115,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validity\" : \"True\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Coupon>>(exampleJson)
                        : default(List<Coupon>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        public virtual IActionResult DiscountLoyaltyDiscountDiscountIdDelete([FromRoute][Required]Guid? discountId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(DiscountDTO), description: "Success")]
        public virtual IActionResult DiscountLoyaltyDiscountDiscountIdGet([FromRoute][Required]Guid? discountId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Discount));
            string exampleJson = null;
            exampleJson = "{\n  \"discountPercentage\" : 0.8008281904610115,\n  \"discountName\" : \"discountName\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Discount>(exampleJson)
                        : default(Discount);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="discountId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/DiscountLoyalty/Discount/{discountId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(DiscountDTO), description: "Success")]
        public virtual IActionResult DiscountLoyaltyDiscountDiscountIdPut([FromRoute][Required]string discountId, [FromBody] DiscountDTO body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Discount));
            string exampleJson = null;
            exampleJson = "{\n  \"discountPercentage\" : 0.8008281904610115,\n  \"discountName\" : \"discountName\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Discount>(exampleJson)
                        : default(Discount);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/DiscountLoyalty/Discount")]
        [SwaggerResponse(statusCode: 201, type: typeof(DiscountDTO), description: "Created")]
        public virtual IActionResult DiscountLoyaltyDiscountPost([FromBody] DiscountDTO body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Discount));
            string exampleJson = null;
            exampleJson = "{\n  \"discountPercentage\" : 0.8008281904610115,\n  \"discountName\" : \"discountName\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Discount>(exampleJson)
                        : default(Discount);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validUntil"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/DiscountLoyalty/Discounts")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Discount>), description: "Success")]
        public virtual IActionResult DiscountLoyaltyDiscountsGet([FromQuery]DateTime? validUntil, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Discount>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"discountPercentage\" : 0.8008281904610115,\n  \"discountName\" : \"discountName\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}, {\n  \"discountPercentage\" : 0.8008281904610115,\n  \"discountName\" : \"discountName\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Discount>>(exampleJson)
                        : default(List<Discount>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loyaltyProgramId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        public virtual IActionResult DiscountLoyaltyLoyaltyProgramLoyaltyProgramIdDelete([FromRoute][Required]Guid? loyaltyProgramId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loyaltyProgramId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(LoyaltyProgramDTO), description: "Success")]
        public virtual IActionResult DiscountLoyaltyLoyaltyProgramLoyaltyProgramIdGet([FromRoute][Required]Guid? loyaltyProgramId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(LoyaltyProgram));
            string exampleJson = null;
            exampleJson = "{\n  \"redemptionRules\" : \"redemptionRules\",\n  \"specialBenefits\" : \"specialBenefits\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"pointsPerPurchase\" : 0.8008281904610115\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<LoyaltyProgramDTO>(exampleJson)
                        : default(LoyaltyProgramDTO);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loyaltyProgramId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/DiscountLoyalty/LoyaltyProgram/{loyaltyProgramId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(LoyaltyProgramDTO), description: "Success")]
        public virtual IActionResult DiscountLoyaltyLoyaltyProgramLoyaltyProgramIdPut([FromRoute][Required]string loyaltyProgramId, [FromBody] LoyaltyProgramDTO body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(LoyaltyProgram));
            string exampleJson = null;
            exampleJson = "{\n  \"redemptionRules\" : \"redemptionRules\",\n  \"specialBenefits\" : \"specialBenefits\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"pointsPerPurchase\" : 0.8008281904610115\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<LoyaltyProgramDTO>(exampleJson)
                        : default(LoyaltyProgramDTO);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/DiscountLoyalty/LoyaltyProgram")]
        [SwaggerResponse(statusCode: 201, type: typeof(LoyaltyProgramDTO), description: "Created")]
        public virtual IActionResult DiscountLoyaltyLoyaltyProgramPost([FromBody] LoyaltyProgramDTO body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(LoyaltyProgram));
            string exampleJson = null;
            exampleJson = "{\n  \"redemptionRules\" : \"redemptionRules\",\n  \"specialBenefits\" : \"specialBenefits\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"pointsPerPurchase\" : 0.8008281904610115\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<LoyaltyProgram>(exampleJson)
                        : default(LoyaltyProgram);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/DiscountLoyalty/LoyaltyPrograms")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<LoyaltyProgramDTO>), description: "Success")]
        public virtual IActionResult DiscountLoyaltyLoyaltyProgramsGet([FromQuery]Guid? businessId, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<LoyaltyProgram>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"redemptionRules\" : \"redemptionRules\",\n  \"specialBenefits\" : \"specialBenefits\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"pointsPerPurchase\" : 0.8008281904610115\n}, {\n  \"redemptionRules\" : \"redemptionRules\",\n  \"specialBenefits\" : \"specialBenefits\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"pointsPerPurchase\" : 0.8008281904610115\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<LoyaltyProgram>>(exampleJson)
                        : default(List<LoyaltyProgram>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }*/
    }
}
