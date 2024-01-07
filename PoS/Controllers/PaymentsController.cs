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
    public class PaymentsController : ControllerBase
    { 
/*        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentMethodId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Payments/PaymentMethod/{paymentMethodId}")]
        public virtual IActionResult PaymentsPaymentMethodPaymentMethodIdDelete([FromRoute][Required]Guid? paymentMethodId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentMethodId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Payments/PaymentMethod/{paymentMethodId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaymentMethodDTO), description: "Success")]
        public virtual IActionResult PaymentsPaymentMethodPaymentMethodIdGet([FromRoute][Required]Guid? paymentMethodId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(PaymentMethod));
            string exampleJson = null;
            exampleJson = "{\n  \"methodDescription\" : \"methodDescription\",\n  \"methodName\" : \"methodName\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<PaymentMethod>(exampleJson)
                        : default(PaymentMethod);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentMethodId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Payments/PaymentMethod/{paymentMethodId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaymentMethodDTO), description: "Success")]
        public virtual IActionResult PaymentsPaymentMethodPaymentMethodIdPut([FromRoute][Required]string paymentMethodId, [FromBody]PaymentMethod body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(PaymentMethod));
            string exampleJson = null;
            exampleJson = "{\n  \"methodDescription\" : \"methodDescription\",\n  \"methodName\" : \"methodName\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<PaymentMethod>(exampleJson)
                        : default(PaymentMethod);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Payments/PaymentMethod")]
        [SwaggerResponse(statusCode: 201, type: typeof(PaymentMethodDTO), description: "Created")]
        public virtual IActionResult PaymentsPaymentMethodPost([FromBody] PaymentMethodDTO body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(PaymentMethod));
            string exampleJson = null;
            exampleJson = "{\n  \"methodDescription\" : \"methodDescription\",\n  \"methodName\" : \"methodName\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<PaymentMethod>(exampleJson)
                        : default(PaymentMethod);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Payments/Payment/{paymentId}")]
        public virtual IActionResult PaymentsPaymentPaymentIdDelete([FromRoute][Required]Guid? paymentId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Payments/Payment/{paymentId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaymentDTO), description: "Success")]
        public virtual IActionResult PaymentsPaymentPaymentIdGet([FromRoute][Required]Guid? paymentId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Payment));
            string exampleJson = null;
            exampleJson = "{\n  \"amount\" : 0.8008281904610115,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentMethodId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"Paid\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Payment>(exampleJson)
                        : default(Payment);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Payments/Payment/{paymentId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(PaymentDTO), description: "Success")]
        public virtual IActionResult PaymentsPaymentPaymentIdPut([FromRoute][Required]string paymentId, [FromBody]Payment body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Payment));
            string exampleJson = null;
            exampleJson = "{\n  \"amount\" : 0.8008281904610115,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentMethodId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"Paid\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Payment>(exampleJson)
                        : default(Payment);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Payments/Payment")]
        [SwaggerResponse(statusCode: 201, type: typeof(Payment), description: "Created")]
        public virtual IActionResult PaymentsPaymentPost([FromBody]Payment body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Payment));
            string exampleJson = null;
            exampleJson = "{\n  \"amount\" : 0.8008281904610115,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentMethodId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"Paid\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Payment>(exampleJson)
                        : default(Payment);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="paymentMethodId"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Payments/Payments")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Payment>), description: "Success")]
        public virtual IActionResult PaymentsPaymentsGet([FromQuery]Guid? orderId, [FromQuery]Guid? paymentMethodId, [FromQuery]string status, [FromQuery]DateTime? fromDate, [FromQuery]DateTime? toDate, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Payment>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"amount\" : 0.8008281904610115,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentMethodId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"Paid\"\n}, {\n  \"amount\" : 0.8008281904610115,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentMethodId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"paymentDate\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"status\" : \"Paid\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Payment>>(exampleJson)
                        : default(List<Payment>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }*/
    }
}
