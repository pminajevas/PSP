using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PoS.Services.Services;
using PoS.Core.Entities;

namespace PoS.Controllers
{
    [ApiController]
    public class ItemServiceController : ControllerBase
    {
        IItemService _itemService;
        IServicesService _servicesService;

        public ItemServiceController(IItemService itemService, IServicesService servicesService)
        {
            _itemService = itemService;
            _servicesService = servicesService;
        }

        [HttpDelete]
        [Route("/ItemService/Item/{itemId}")]
        public async Task<IActionResult> DeleteItemAsync([FromRoute][Required] Guid itemId)
        {
            bool isDeleted = await _itemService.DeleteItemAsync(itemId);
            if(isDeleted)
            {
                return Ok();
            }

            return Problem();
        }

        [HttpGet]
        [Route("/ItemService/Item/{itemId}")]
        public async Task<IActionResult> GetItemAsync([FromRoute][Required] Guid itemId)
        {
            var result = await _itemService.GetItemByIdAsync(itemId);

            if(result != null)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpPut]
        [Route("/ItemService/Item/{itemId}")]
        public async Task<IActionResult> UpdateItemAsync([FromRoute][Required]Guid itemId, [FromBody] Item body)
        {
            var item = await _itemService.UpdateItemAsync(itemId, body);
            if(item != null)
            {
                return Ok(item);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("/ItemService/Item")]
        public async Task<IActionResult> CreateItemAsync([FromBody] Item item)
        {
            var newItem = await _itemService.CreateItemAsync(item);

            if(newItem != null)
            {
                return Ok(newItem);
            }

            return Problem();
        }

        [HttpGet]
        [Route("/ItemService/Items")]
        public async Task<IActionResult> GetItemsAsync()
        {
            var items = await _itemService.GetItemsAsync();

            if(items.Any())
            {
                return Ok(items);
            }

            return NoContent();
        }

        [HttpPost]
        [Route("/ItemService/Service")]
        public async Task<IActionResult> CreateServiceAsync([FromBody] Service service)
        {
            var newService = await _servicesService.CreateServiceAsync(service);
            if(newService != null)
            {
                return Ok(newService);
            }

            return Problem();
        }

        [HttpDelete]
        [Route("/ItemService/Service/{serviceId}")]
        public async Task<IActionResult> DeleteServiceAsync([FromRoute][Required] Guid serviceId)
        {
            bool isDeleted = await _servicesService.DeleteServiceAsync(serviceId);
            if(isDeleted)
            {
                return Ok();
            }

            return Problem();

        }

        [HttpGet]
        [Route("/ItemService/Service/{serviceId}")]
        public async Task<IActionResult> GetServiceAsync([FromRoute][Required] Guid serviceId)
        {
            var service = await _servicesService.GetServiceByIdAsync(serviceId);
            if(service != null)
            {
                return Ok(service);
            }

            return NoContent();
        }

        [HttpPut]
        [Route("/ItemService/Service/{serviceId}")]
        public async Task<IActionResult> UpdateServiceAsync([FromRoute][Required] Guid serviceId, [FromBody] Service body)
        {
            var service = await _servicesService.UpdateServiceAsync(serviceId, body);
            if(service != null)
            {
                return Ok(service);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("/ItemService/Services")]
        public async Task<IActionResult> GetServicesAsync()
        {
            var services = await _servicesService.GetServicesAsync();
            if(services.Any())
            {
                return Ok(services);
            }

            return NoContent();
        }
    }
}
