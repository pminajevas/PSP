using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class BusinessApiController : ControllerBase
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Business/Business/{businessId}")]
        public virtual IActionResult BusinessBusinessBusinessIdDelete([FromRoute][Required]Guid? businessId)
        {
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Business/Business/{businessId}")]
        public virtual IActionResult BusinessBusinessBusinessIdGet([FromRoute][Required]Guid? businessId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Business));
            string exampleJson = null;
            exampleJson = "{\n  \"businessName\" : \"businessName\",\n  \"location\" : \"location\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Business>(exampleJson)
                        : default(Business);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Business/Business/{businessId}")]
        public virtual IActionResult BusinessBusinessBusinessIdPut([FromRoute][Required]string businessId, [FromBody]Business body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Order));
            string exampleJson = null;
            exampleJson = "{\n  \"date\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"totalAmount\" : 0.8008281904610115,\n  \"taxId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"tip\" : 6.027456183070403,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"status\" : \"Paid\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Order>(exampleJson)
                        : default(Order);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Business/Business")]
        public virtual IActionResult BusinessBusinessPost([FromBody]Business body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Business));
            string exampleJson = null;
            exampleJson = "{\n  \"businessName\" : \"businessName\",\n  \"location\" : \"location\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Business>(exampleJson)
                        : default(Business);            //TODO: Change the data returned
            return new ObjectResult(example);
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
        public virtual IActionResult BusinessBusinessesGet([FromQuery]string location, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Business>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"businessName\" : \"businessName\",\n  \"location\" : \"location\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}, {\n  \"businessName\" : \"businessName\",\n  \"location\" : \"location\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Business>>(exampleJson)
                        : default(List<Business>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
