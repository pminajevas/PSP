using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using PoS.Application.Models.Requests;
using PoS.Application.Filters;
using PoS.Application.Services.Interfaces;

namespace PoS.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAppointmentService _appointmentService;

        public OrdersController(IOrderService orderService, IAppointmentService appointmentService)
        {
            _orderService = orderService;
            _appointmentService = appointmentService;
        }

        [HttpPost]
        [Route("/Orders/Order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest createRequest)
        {
            var newOrder = await _orderService.AddOrderAsync(createRequest);

            return CreatedAtAction("GetOrder", new { orderId = newOrder.Id }, newOrder);
        }

        [HttpPost]
        [Route("/Orders/Receipt")]
        public async Task<IActionResult> GenerateReceipt([FromBody] ReceiptRequest receiptRequest)
        {
            return Ok(await _orderService.GenerateReceipt(receiptRequest));
        }

        [HttpGet]
        [Route("/Orders/Orders")]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilter orderFilter)
        {
            return Ok(await _orderService.GetOrdersAsync(orderFilter));
        }

        [HttpGet]
        [Route("/Orders/Order/{orderId}")]
        [ActionName("GetOrder")]
        public async Task<IActionResult> GetOrderById([FromRoute][Required] Guid orderId)
        {
            return Ok(await _orderService.GetOrderByIdAsync(orderId));
        }

        [HttpPut]
        [Route("/Orders/Order/{orderId}")]
        public async Task<IActionResult> UpdateOrder([FromRoute][Required] Guid orderId, [FromBody] OrderRequest updateRequest)
        {
            return Ok(await _orderService.UpdateOrderByIdAsync(orderId, updateRequest));
        }

        [HttpDelete]
        [Route("/Orders/Order/{orderId}")]
        public async Task<IActionResult> DeleteOrder([FromRoute][Required] Guid orderId)
        {
            await _orderService.DeleteOrderByIdAsync(orderId);

            return NoContent();
        }

        [HttpPost]
        [Route("/Orders/OrderItem")]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemRequest createRequest)
        {
            var newOrderItem = await _orderService.AddOrderItemAsync(createRequest);

            return CreatedAtAction("GetOrderItem", new { orderItemId = newOrderItem.Id }, newOrderItem);
        }

        [HttpGet]
        [Route("/Orders/OrderItems")]
        public async Task<IActionResult> GetOrderItems([FromQuery] OrderItemFilter orderItemFilter)
        {
            return Ok(await _orderService.GetOrderItemsAsync(orderItemFilter));
        }

        [HttpGet]
        [Route("/Orders/OrderItem/{orderItemId}")]
        [ActionName("GetOrderItem")]
        public async Task<IActionResult> GetOrderItemById([FromRoute][Required] Guid orderItemId)
        {
            return Ok(await _orderService.GetOrderItemByIdAsync(orderItemId));
        }

        [HttpPut]
        [Route("/Orders/OrderItem/{orderItemId}")]
        public async Task<IActionResult> UpdateOrderItem([FromRoute][Required] Guid orderItemId, [FromBody] OrderItemRequest updateRequest)
        {
            return Ok(await _orderService.UpdateOrderItemByIdAsync(orderItemId, updateRequest));
        }

        [HttpDelete]
        [Route("/Orders/OrderItem/{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItem([FromRoute][Required] Guid orderItemId)
        {
            await _orderService.DeleteOrderItemByIdAsync(orderItemId);

            return NoContent();
        }

        [HttpPost]
        [Route("/Orders/Appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest createRequest)
        {
            var newAppointment = await _appointmentService.AddAppointmentAsync(createRequest);

            return CreatedAtAction("GetAppointment", new { appointmentId = newAppointment.Id }, newAppointment);
        }

        [HttpGet]
        [Route("/Orders/Appointments")]
        public async Task<IActionResult> GetAppointments([FromQuery] AppointmentFilter appointmentFilter)
        {
            return Ok(await _appointmentService.GetAppointmentsAsync(appointmentFilter));
        }

        [HttpGet]
        [Route("/Orders/Appointment/{appointmentId}")]
        [ActionName("GetAppointment")]
        public async Task<IActionResult> GetAppointmentById([FromRoute][Required] Guid appointmentId)
        {
            return Ok(await _appointmentService.GetAppointmentByIdAsync(appointmentId));
        }

        [HttpPost]
        [Route("/Orders/AppointmentOrder/{appointmentId}")]
        public async Task<IActionResult> CreateAppointmentOrder([FromRoute][Required] Guid appointmentId, [FromBody][Required] Guid taxId)
        {
            var newOrder = await _orderService.AddOrderAsync(appointmentId, taxId);

            return CreatedAtAction("CreateAppointmentOrder", new { orderId = newOrder.Id }, newOrder);
        }

        // TODO : Implement
        //[HttpGet]
        //[Route("/Orders/Appointment/FreeTimes")]
        //public async Task<IActionResult> GetFreeTimes([FromQuery] Guid businessId, [FromQuery] Guid? employeeId, [FromQuery] DateTime? reservationTimeFrom, [FromQuery] DateTime? reservationTimeUntil, [FromQuery] string orderBy, [FromQuery] string sorting, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPut]
        [Route("/Orders/Appointment/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute][Required] Guid appointmentId, [FromBody] AppointmentRequest updateRequest)
        {
            return Ok(await _appointmentService.UpdateAppointmentByIdAsync(appointmentId, updateRequest));
        }

        [HttpDelete]
        [Route("/Orders/Appointment/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment([FromRoute][Required]Guid appointmentId)
        {
            await _appointmentService.DeleteAppointmentByIdAsync(appointmentId);

            return NoContent();
        }
    }
}
