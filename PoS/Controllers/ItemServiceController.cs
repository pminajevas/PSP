using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PoS.Services.Services;
using PoS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using PoS.Application.Filters;

namespace PoS.Controllers
{
    [ApiController]
    public class ItemServiceController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IServicesService _servicesService;

        public ItemServiceController(IItemService itemService, IServicesService servicesService)
        {
            _itemService = itemService;
            _servicesService = servicesService;
        }

        [HttpDelete]
        [Route("/ItemService/Item/{itemId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute][Required] Guid itemId)
        {
            await _itemService.DeleteItemAsync(itemId);

            return NoContent();
        }

        [HttpGet]
        [Route("/ItemService/Item/{itemId}")]
        [ActionName("GetItem")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetItemAsync([FromRoute][Required] Guid itemId)
        {
            return Ok(await _itemService.GetItemByIdAsync(itemId));
        }

        [HttpPut]
        [Route("/ItemService/Item/{itemId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> UpdateItemAsync([FromRoute][Required]Guid itemId, [FromBody] Item body)
        {
            return Ok(await _itemService.UpdateItemAsync(itemId, body));
        }

        [HttpPost]
        [Route("/ItemService/Item")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> CreateItemAsync([FromBody] Item item)
        {
            var newItem = await _itemService.CreateItemAsync(item);

            return CreatedAtAction("GetItem", new { itemId = newItem.Id }, newItem);
        }

        [HttpGet]
        [Route("/ItemService/Items")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetItemsAsync([FromQuery] ItemsFilter itemsFilter)
        {
            return Ok(await _itemService.GetItemsAsync(itemsFilter));
        }

        [HttpPost]
        [Route("/ItemService/Service")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> CreateServiceAsync([FromBody] Service service)
        {
            var newService = await _servicesService.CreateServiceAsync(service);

            return CreatedAtAction("GetService", new { serviceId = newService.Id }, newService);
        }

        [HttpDelete]
        [Route("/ItemService/Service/{serviceId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> DeleteServiceAsync([FromRoute][Required] Guid serviceId)
        {
            await _servicesService.DeleteServiceAsync(serviceId);

            return NoContent();

        }

        [HttpGet]
        [Route("/ItemService/Service/{serviceId}")]
        [ActionName("GetService")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetServiceAsync([FromRoute][Required] Guid serviceId)
        {
            return Ok(await _servicesService.GetServiceByIdAsync(serviceId));
        }

        [HttpPut]
        [Route("/ItemService/Service/{serviceId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> UpdateServiceAsync([FromRoute][Required] Guid serviceId, [FromBody] Service body)
        {
            return Ok(await _servicesService.UpdateServiceAsync(serviceId, body));
        }

        [HttpGet]
        [Route("/ItemService/Services")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetServicesAsync([FromQuery] ServicesFilter servicesFilter)
        {
            return Ok(await _servicesService.GetServicesAsync(servicesFilter));
        }
    }
}
