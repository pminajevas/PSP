using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using PoS.Services.Services;
using PoS.Application.Services.Interfaces;
using PoS.Application.Models.Requests;
using PoS.Application.Filters;
using PoS.Core.Exceptions;

namespace PoS.Controllers
{
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly Application.Services.Interfaces.IAuthorizationService _authorizationService;

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
            await _businessService.DeleteBusinessAsync(businessId);

            return NoContent();
        }

        [HttpGet]
        [Route("/Business/Business/{businessId}")]
        [ActionName("GetBusiness")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetBusinessAsync([FromRoute][Required]Guid businessId)
        {
            if (await _authorizationService.IsUserAdminOrBusinessManager(User))
            {
                return Ok(await _businessService.GetBusinessByIdAsync(businessId));
            }
            else
            {
                throw new PoSException("Insufficient permissions for the action", System.Net.HttpStatusCode.Unauthorized);
            }
        }

        [HttpPut]
        [Route("/Business/Business/{businessId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateBusinessAsync([FromRoute][Required]Guid businessId, [FromBody]BusinessRequest business)
        {
            if (await _authorizationService.IsUserAdminOrBusinessManager(User))
            {
                return Ok(await _businessService.UpdateBusinessAsync(business, businessId));
            }
            else
            {
                throw new PoSException("Insufficient permissions for the action", System.Net.HttpStatusCode.Unauthorized);
            }

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
            else
            {
                throw new PoSException("Insufficient permissions for the action", System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}
