using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using PoS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using PoS.Application.Services.Interfaces;
using PoS.Application.Filters;

namespace PoS.Controllers
{
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentsController(IPaymentService paymentService, IPaymentMethodService paymentMethodService)
        {
            _paymentService = paymentService;
            _paymentMethodService = paymentMethodService;
        }

        [HttpPost]
        [Route("/Payments/Payment")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] Payment payment)
        {
            var newPayment = await _paymentService.CreatePaymentAsync(payment);
            return CreatedAtAction("GetPayment", new { paymentId = newPayment.Id }, newPayment);
        }

        [HttpGet]
        [Route("/Payments/Payment/{paymentId}")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetPaymentAsync([FromRoute][Required] Guid paymentId)
        {
            return Ok(await _paymentService.GetPaymentByIdAsync(paymentId));
        }

        [HttpGet]
        [Route("/Payments/Payment")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetPaymentsAsync([FromQuery] PaymentsFilter paymentsFilter)
        {
            return Ok(await _paymentService.GetPaymentsAsync(paymentsFilter));
        }

        [HttpPut]
        [Route("/Payments/Payment/{paymentId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> UpdatePaymentAsync([FromRoute][Required] Guid paymentId, [FromBody] Payment payment)
        {
            return Ok(await _paymentService.UpdatePaymentAsync(paymentId, payment));
        }

        [HttpDelete]
        [Route("/Payments/Payment/{paymentId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> DeletePaymentAsync([FromRoute][Required] Guid paymentId)
        {
            await _paymentService.DeletePaymentAsync(paymentId);
            return NoContent();
        }

        [HttpPut]
        [Route("/Payments/Confirmation/{confirmationId}")]
        public async Task<IActionResult> ConfirmPaymentAsync([FromRoute][Required] Guid confirmationId)
        {
            return Ok(await _paymentService.ConfirmPaymentAsync(confirmationId));
        }

        [HttpPost]
        [Route("/Payments/PaymentMethod")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> CreatePaymentMethodAsync([FromBody] PaymentMethod paymentMethod)
        {
            var newPaymentMethod = await _paymentMethodService.CreatePaymentMethodAsync(paymentMethod);
            return CreatedAtAction("GetPaymentMethod", new { paymentMethodId = newPaymentMethod.Id }, newPaymentMethod);
        }

        [HttpGet]
        [Route("/Payments/PaymentMethod/{paymentMethodId}")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetPaymentMethodAsync([FromRoute][Required] Guid paymentMethodId)
        {
            return Ok(await _paymentMethodService.GetPaymentMethodByIdAsync(paymentMethodId));
        }

        [HttpGet]
        [Route("/Payments/PaymentMethod")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        public async Task<IActionResult> GetPaymentMethodsAsync()
        {
            return Ok(await _paymentMethodService.GetPaymentMethodsAsync());
        }

        [HttpPut]
        [Route("/Payments/PaymentMethod/{paymentMethodId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> UpdatePaymentMethodAsync([FromRoute][Required] Guid paymentMethodId, [FromBody] PaymentMethod paymentMethod)
        {
            return Ok(await _paymentMethodService.UpdatePaymentMethodAsync(paymentMethodId, paymentMethod));
        }

        [HttpDelete]
        [Route("/Payments/PaymentMethod/{paymentMethodId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> DeletePaymentMethodAsync([FromRoute][Required] Guid paymentMethodId)
        {
            await _paymentMethodService.DeletePaymentMethodAsync(paymentMethodId);
            return NoContent();
        }

    }
}
