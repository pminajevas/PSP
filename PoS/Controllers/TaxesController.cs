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
    [ApiController]
    public class TaxesController : ControllerBase
    { 
        /*/// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Taxes/Tax")]
        [SwaggerResponse(statusCode: 201, type: typeof(Tax), description: "Created")]
        public virtual IActionResult TaxesTaxPost([FromBody]Tax body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Tax));
            string exampleJson = null;
            exampleJson = "{\n  \"taxDescription\" : \"taxDescription\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validFrom\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"category\" : \"Flat\",\n  \"value\" : 0.8008281904610115,\n  \"taxName\" : \"taxName\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Tax>(exampleJson)
                        : default(Tax);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Taxes/Tax/{taxId}")]
        public virtual IActionResult TaxesTaxTaxIdDelete([FromRoute][Required]Guid? taxId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Taxes/Tax/{taxId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Tax), description: "Success")]
        public virtual IActionResult TaxesTaxTaxIdGet([FromRoute][Required]Guid? taxId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Tax));
            string exampleJson = null;
            exampleJson = "{\n  \"taxDescription\" : \"taxDescription\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validFrom\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"category\" : \"Flat\",\n  \"value\" : 0.8008281904610115,\n  \"taxName\" : \"taxName\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Tax>(exampleJson)
                        : default(Tax);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Taxes/Tax/{taxId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Tax), description: "Success")]
        public virtual IActionResult TaxesTaxTaxIdPut([FromRoute][Required]string taxId, [FromBody]Tax body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Tax));
            string exampleJson = null;
            exampleJson = "{\n  \"taxDescription\" : \"taxDescription\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validFrom\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"category\" : \"Flat\",\n  \"value\" : 0.8008281904610115,\n  \"taxName\" : \"taxName\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Tax>(exampleJson)
                        : default(Tax);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxCategory"></param>
        /// <param name="validFrom"></param>
        /// <param name="validUntil"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Taxes/Taxes")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Tax>), description: "Success")]
        public virtual IActionResult TaxesTaxesGet([FromQuery]string taxCategory, [FromQuery]DateTime? validFrom, [FromQuery]DateTime? validUntil, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Tax>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"taxDescription\" : \"taxDescription\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validFrom\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"category\" : \"Flat\",\n  \"value\" : 0.8008281904610115,\n  \"taxName\" : \"taxName\"\n}, {\n  \"taxDescription\" : \"taxDescription\",\n  \"validUntil\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"validFrom\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"category\" : \"Flat\",\n  \"value\" : 0.8008281904610115,\n  \"taxName\" : \"taxName\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Tax>>(exampleJson)
                        : default(List<Tax>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }*/
    }
}
