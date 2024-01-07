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
    public class OrdersController : ControllerBase
    { 
        /*/// <summary>
        /// 
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Orders/Appointment/{appointmentId}")]
        public virtual IActionResult OrdersAppointmentAppointmentIdDelete([FromRoute][Required]Guid? appointmentId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/Appointment/{appointmentId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Appointment), description: "Success")]
        public virtual IActionResult OrdersAppointmentAppointmentIdGet([FromRoute][Required]Guid? appointmentId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Appointment));
            string exampleJson = null;
            exampleJson = "{\n  \"duration\" : 0.8008281904610115,\n  \"reservationTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"employeeId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"endTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"serviceId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Appointment>(exampleJson)
                        : default(Appointment);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Orders/Appointment/{appointmentId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Appointment), description: "Success")]
        public virtual IActionResult OrdersAppointmentAppointmentIdPut([FromRoute][Required]string appointmentId, [FromBody]Appointment body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Appointment));
            string exampleJson = null;
            exampleJson = "{\n  \"duration\" : 0.8008281904610115,\n  \"reservationTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"employeeId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"endTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"serviceId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Appointment>(exampleJson)
                        : default(Appointment);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="employeeId"></param>
        /// <param name="reservationTimeFrom"></param>
        /// <param name="reservationTimeUntil"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/Appointment/FreeTimes")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<DateTime?>), description: "Success")]
        public virtual IActionResult OrdersAppointmentFreeTimesGet([FromQuery]Guid? businessId, [FromQuery]Guid? employeeId, [FromQuery]DateTime? reservationTimeFrom, [FromQuery]DateTime? reservationTimeUntil, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<DateTime?>));
            string exampleJson = null;
            exampleJson = "[ \"2000-01-23T04:56:07.000+00:00\", \"2000-01-23T04:56:07.000+00:00\" ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<DateTime?>>(exampleJson)
                        : default(List<DateTime?>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Orders/Appointment")]
        [SwaggerResponse(statusCode: 201, type: typeof(Appointment), description: "Created")]
        public virtual IActionResult OrdersAppointmentPost([FromBody]Appointment body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Appointment));
            string exampleJson = null;
            exampleJson = "{\n  \"duration\" : 0.8008281904610115,\n  \"reservationTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"employeeId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"endTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"serviceId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<Appointment>(exampleJson)
                        : default(Appointment);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="employeeId"></param>
        /// <param name="serviceId"></param>
        /// <param name="customerId"></param>
        /// <param name="reservationTimeFrom"></param>
        /// <param name="reservationTimeUntil"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/Appointments")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Appointment>), description: "Success")]
        public virtual IActionResult OrdersAppointmentsGet([FromQuery]Guid? businessId, [FromQuery]Guid? employeeId, [FromQuery]Guid? serviceId, [FromQuery]Guid? customerId, [FromQuery]DateTime? reservationTimeFrom, [FromQuery]DateTime? reservationTimeUntil, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Appointment>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"duration\" : 0.8008281904610115,\n  \"reservationTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"employeeId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"endTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"serviceId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n}, {\n  \"duration\" : 0.8008281904610115,\n  \"reservationTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"employeeId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"endTime\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"serviceId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Appointment>>(exampleJson)
                        : default(List<Appointment>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Orders/OrderItem/{orderItemId}")]
        public virtual IActionResult OrdersOrderItemOrderItemIdDelete([FromRoute][Required]Guid? orderItemId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/OrderItem/{orderItemId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(OrderItem), description: "Success")]
        public virtual IActionResult OrdersOrderItemOrderItemIdGet([FromRoute][Required]Guid? orderItemId)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(OrderItem));
            string exampleJson = null;
            exampleJson = "{\n  \"unitPrice\" : 0.8008281904610115,\n  \"itemId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"quantity\" : 6.027456183070403,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"subtotal\" : 1.4658129805029452,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"type\" : \"Item\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<OrderItem>(exampleJson)
                        : default(OrderItem);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Orders/OrderItem/{orderItemId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(OrderItem), description: "Success")]
        public virtual IActionResult OrdersOrderItemOrderItemIdPut([FromRoute][Required]string orderItemId, [FromBody]OrderItem body)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(OrderItem));
            string exampleJson = null;
            exampleJson = "{\n  \"unitPrice\" : 0.8008281904610115,\n  \"itemId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"quantity\" : 6.027456183070403,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"subtotal\" : 1.4658129805029452,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"type\" : \"Item\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<OrderItem>(exampleJson)
                        : default(OrderItem);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Orders/OrderItem")]
        [SwaggerResponse(statusCode: 201, type: typeof(OrderItem), description: "Created")]
        public virtual IActionResult OrdersOrderItemPost([FromBody]OrderItem body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(OrderItem));
            string exampleJson = null;
            exampleJson = "{\n  \"unitPrice\" : 0.8008281904610115,\n  \"itemId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"quantity\" : 6.027456183070403,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"subtotal\" : 1.4658129805029452,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"type\" : \"Item\"\n}";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<OrderItem>(exampleJson)
                        : default(OrderItem);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="type"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/OrderItems")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<OrderItem>), description: "Success")]
        public virtual IActionResult OrdersOrderItemsGet([FromQuery]Guid? orderId, [FromQuery]string type, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<OrderItem>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"unitPrice\" : 0.8008281904610115,\n  \"itemId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"quantity\" : 6.027456183070403,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"subtotal\" : 1.4658129805029452,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"type\" : \"Item\"\n}, {\n  \"unitPrice\" : 0.8008281904610115,\n  \"itemId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"quantity\" : 6.027456183070403,\n  \"orderId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"subtotal\" : 1.4658129805029452,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"type\" : \"Item\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<OrderItem>>(exampleJson)
                        : default(List<OrderItem>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Orders/Order/{orderId}")]
        public virtual IActionResult OrdersOrderOrderIdDelete([FromRoute][Required]Guid? orderId)
        { 
            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);

            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/Order/{orderId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Order), description: "Success")]
        public virtual IActionResult OrdersOrderOrderIdGet([FromRoute][Required]Guid? orderId)
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
        /// <param name="orderId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Orders/Order/{orderId}")]
        [SwaggerResponse(statusCode: 200, type: typeof(Order), description: "Success")]
        public virtual IActionResult OrdersOrderOrderIdPut([FromRoute][Required]string orderId, [FromBody]Order body)
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
        [Route("/Orders/Order")]
        [SwaggerResponse(statusCode: 201, type: typeof(Order), description: "Created")]
        public virtual IActionResult OrdersOrderPost([FromBody]Order body)
        { 
            //TODO: Uncomment the next line to return response 201 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(201, default(Order));
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
        /// <param name="customerId"></param>
        /// <param name="businessId"></param>
        /// <param name="staffId"></param>
        /// <param name="taxId"></param>
        /// <param name="status"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Orders/Orders")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Order>), description: "Success")]
        public virtual IActionResult OrdersOrdersGet([FromQuery]Guid? customerId, [FromQuery]Guid? businessId, [FromQuery]Guid? staffId, [FromQuery]Guid? taxId, [FromQuery]string status, [FromQuery]DateTime? fromDate, [FromQuery]DateTime? toDate, [FromQuery]string orderBy, [FromQuery]string sorting, [FromQuery]int? pageIndex, [FromQuery]int? pageSize)
        { 
            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Order>));
            string exampleJson = null;
            exampleJson = "[ {\n  \"date\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"totalAmount\" : 0.8008281904610115,\n  \"taxId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"tip\" : 6.027456183070403,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"status\" : \"Paid\"\n}, {\n  \"date\" : \"2000-01-23T04:56:07.000+00:00\",\n  \"totalAmount\" : 0.8008281904610115,\n  \"taxId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"customerId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"businessId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"tip\" : 6.027456183070403,\n  \"id\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"staffId\" : \"046b6c7f-0b8a-43b9-b35d-6489e6daee91\",\n  \"status\" : \"Paid\"\n} ]";
            
                        var example = exampleJson != null
                        ? JsonConvert.DeserializeObject<List<Order>>(exampleJson)
                        : default(List<Order>);            //TODO: Change the data returned
            return new ObjectResult(example);
        }*/
    }
}
