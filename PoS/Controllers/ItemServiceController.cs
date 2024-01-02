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
using PoS.Services.Services;
using PoS.Data;

using Microsoft.AspNetCore.Authorization;

namespace PoS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ItemServiceApiController : ControllerBase
    {
        IItemService _itemService;

        public ItemServiceApiController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <response code="204">No Content</response>
        //[HttpDelete]
        //[Route("/ItemService/Item/{itemId}")]
        //public virtual IActionResult DeleteItemAsync([FromRoute][Required]Guid? itemId)
        //{ 
        //    //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(204);

        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/ItemService/Item/{itemId}")]
        public async Task<IActionResult> GetItemAsync([FromRoute][Required]Guid itemId)
        {
            var result = await _itemService.GetItemByIdAsync(itemId);

            if(result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/ItemService/Item/{itemId}")]
        public async Task<IActionResult> UpdateItemAsync([FromRoute][Required]Guid itemId, [FromBody] Item body)
        {
            var item = await _itemService.UpdateItemAsync(itemId, body);
            if(item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="body"></param>
        ///// <response code="201">Created</response>
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="businessId"></param>
        ///// <param name="discountId"></param>
        ///// <param name="orderBy"></param>
        ///// <param name="sorting"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <response code="200">Success</response>
        [HttpGet]
        [Route("/ItemService/Items")]
        public async Task<IActionResult> GetItemsAsync()
        {
            var items = await _itemService.GetItemsAsync();

            if(items.Any())
            {
                return Ok(items);
            }

            return NotFound();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="body"></param>
        ///// <response code="201">Created</response>
        //[HttpPost]
        //[Route("/ItemService/Service")]
        //[SwaggerResponse(statusCode: 201, type: typeof(Service), description: "Created")]
        //public virtual IActionResult CreateServiceAsync([FromBody]Service body)
        //{ 
        //    //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(201, default(Service));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"duration\" : 0.8008281904610115,\n  \"price\" : 6.027456183070403,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceDescription\" : \"serviceDescription\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"discountId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceName\" : \"serviceName\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<Service>(exampleJson)
        //                : default(Service);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="serviceId"></param>
        ///// <response code="204">No Content</response>
        //[HttpDelete]
        //[Route("/ItemService/Service/{serviceId}")]
        //public virtual IActionResult DeleteServiceAsync([FromRoute][Required]Guid? serviceId)
        //{ 
        //    //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(204);

        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="serviceId"></param>
        ///// <response code="200">Success</response>
        //[HttpGet]
        //[Route("/ItemService/Service/{serviceId}")]
        //[SwaggerResponse(statusCode: 200, type: typeof(Service), description: "Success")]
        //public virtual IActionResult GetServiceAsync([FromRoute][Required]Guid? serviceId)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(Service));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"duration\" : 0.8008281904610115,\n  \"price\" : 6.027456183070403,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceDescription\" : \"serviceDescription\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"discountId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceName\" : \"serviceName\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<Service>(exampleJson)
        //                : default(Service);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="serviceId"></param>
        ///// <param name="body"></param>
        ///// <response code="200">Success</response>
        //[HttpPut]
        //[Route("/ItemService/Service/{serviceId}")]
        //[SwaggerResponse(statusCode: 200, type: typeof(Service), description: "Success")]
        //public virtual IActionResult UpdateServiceAsync([FromRoute][Required]string serviceId, [FromBody]Service body)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(Service));
        //    string exampleJson = null;
        //    exampleJson = "{\n  \"duration\" : 0.8008281904610115,\n  \"price\" : 6.027456183070403,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceDescription\" : \"serviceDescription\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"discountId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceName\" : \"serviceName\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<Service>(exampleJson)
        //                : default(Service);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="businessId"></param>
        ///// <param name="staffId"></param>
        ///// <param name="discountId"></param>
        ///// <param name="orderBy"></param>
        ///// <param name="sorting"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <response code="200">Success</response>
        //[HttpGet]
        //[Route("/ItemService/Services")]
        //[SwaggerResponse(statusCode: 200, type: typeof(List<Service>), description: "Success")]
        //public virtual IActionResult GetServicesAsync([FromQuery]Guid? businessId, [FromQuery]Guid? staffId, [FromQuery]Guid? discountId, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        //{ 
        //    //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        //    // return StatusCode(200, default(List<Service>));
        //    string exampleJson = null;
        //    exampleJson = "[ {\n  \"duration\" : 0.8008281904610115,\n  \"price\" : 6.027456183070403,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceDescription\" : \"serviceDescription\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"discountId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceName\" : \"serviceName\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}, {\n  \"duration\" : 0.8008281904610115,\n  \"price\" : 6.027456183070403,\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceDescription\" : \"serviceDescription\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"discountId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"serviceName\" : \"serviceName\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n} ]";

        //                var example = exampleJson != null
        //                ? JsonConvert.DeserializeObject<List<Service>>(exampleJson)
        //                : default(List<Service>);            //TODO: Change the data returned
        //    return new ObjectResult(example);
        //}
    }
}
