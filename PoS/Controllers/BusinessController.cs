using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using PoS.Services.Services;
using PoS.Application.Services.Interfaces;
using PoS.Application.Models.Requests;
using PoS.Application.Filters;

namespace PoS.Controllers
{
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private IBusinessService _businessService;
        private Application.Services.Interfaces.IAuthorizationService _authorizationService;

        public BusinessController(
            IBusinessService businessService,
            Application.Services.Interfaces.IAuthorizationService authorizationService
        )
        {
            _businessService = businessService;
            _authorizationService = authorizationService;
        }

        [HttpDelete]
        [Route("/Business/Business/{businessId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBusinessAsync([FromRoute][Required]Guid businessId)
        {
            if (await _businessService.DeleteBusinessAsync(businessId))
            {
                return Ok();
            }

            return Problem();
        }

        [HttpGet]
        [Route("/Business/Business/{businessId}")]
        [ActionName("GetBusiness")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetBusinessAsync([FromRoute][Required]Guid businessId)
        {
            if (await _authorizationService.IsUserAdminOrBusinessManager(User))
            {
                var result = await _businessService.GetBusinessByIdAsync(businessId);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return Forbid();
        }

        [HttpPut]
        [Route("/Business/Business/{businessId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateBusinessAsync([FromRoute][Required]Guid businessId, [FromBody]BusinessRequest business)
        {
            if (await _authorizationService.IsUserAdminOrBusinessManager(User))
            {
                var updatedBusiness = await _businessService.UpdateBusinessAsync(business, businessId);

                if (updatedBusiness != null)
                {
                    return Ok(updatedBusiness);
                }

                return NotFound();
            }

            return Forbid();

    }

        [HttpPost]
        [Route("/Business/Business")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBusinessAsync([FromBody]BusinessRequest business)
        {
            var newBusiness = await _businessService.AddBusinessAsync(business);

            return CreatedAtAction("GetBusiness", new { businessId = newBusiness.Id }, newBusiness);
        }

        [HttpGet]
        [Route("/Business/Businesses")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetBusinessesAsync([FromQuery] BusinessesFilter businessFilter)
        {
            if (await _authorizationService.IsUserAdminOrBusinessManager(User))
            {
                return Ok(await _businessService.GetAllBusinessesAsync(businessFilter));
            }

            return Forbid();
        }
    }
}
