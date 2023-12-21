using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using PoS.Services.Services;
using PoS.Shared.Utilities;
using PoS.API.Helpers;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using PoS.Data;
using System.Security.Claims;

namespace PoS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class BusinessApiController : ControllerBase
    {
        private IBusinessService _businessService;
        private IUserService _userService;
        private IFilterValidator _validator;
        public BusinessApiController(IBusinessService businessService, IUserService userService, IFilterValidator validator)
        {
            _businessService = businessService;
            _validator = validator;
            _userService = userService;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Business/Business/{businessId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteBusinessAsync([FromRoute][Required]Guid businessId)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessIdFromToken) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessIdFromToken) &&
                    businessId == businessIdFromToken))
                {

                        if (await _businessService.DeleteBusinessAsync(businessId) == true)
                        {
                            return Ok();
                        }
                        return NotFound();

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Business/Business/{businessId}")]
        [ActionName("GetBusiness")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetBusinessAsync([FromRoute][Required]Guid businessId)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessIdFromToken) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessIdFromToken) &&
                    businessId == businessIdFromToken))
                {
                    var result = await _businessService.GetBusinessByIdAsync(businessId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Business/Business/{businessId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateBusinessAsync([FromRoute][Required]Guid businessId, [FromBody]BusinessRequest business)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessIdFromToken) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessIdFromToken) &&
                    businessId == businessIdFromToken))
                {
                    var updatedBusiness = await _businessService.UpdateBusinessAsync(business, businessId);
                    if (updatedBusiness != null)
                    {
                        return Ok(updatedBusiness);
                    }
                    return NotFound();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Business/Business")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBusinessAsync([FromBody]BusinessRequest business)
        {
            try
            {
                var newBusiness = await _businessService.AddBusinessAsync(business);
                return CreatedAtAction("GetBusiness", new { businessId = newBusiness.Id }, newBusiness);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Business/Businesses")]
        public async Task<IActionResult> GetBusinessesAsync([FromQuery]string? location = null, [FromQuery]string? orderBy = null, [FromQuery]string? sorting = null, [FromQuery]int? pageIndex = null, [FromQuery]int? pageSize = null)
        {
            Filter filter = new Filter();

            // Add supported parameters using the AddParameter method
            filter.AddParameter("Location", location);
            filter.AddParameter("OrderBy", orderBy);
            filter.AddParameter("Sorting", sorting);
            filter.AddParameter("PageIndex", pageIndex);
            filter.AddParameter("PageSize", pageSize);
            if (_validator.ValidateFilter(filter))
            {
                try
                {
                    return Ok(await _businessService.GetAllBusinessesAsync(filter));
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
                
            }
            return BadRequest("Incorrect filters");
            
        }
    }
}
