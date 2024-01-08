using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace PoS.Controllers
{
    [ApiController]
    public class TaxesController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxesController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpPost]
        [Route("/Taxes/Tax")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateTax([FromBody]TaxRequest createRequest)
        {
            var newTax = await _taxService.AddTaxAsync(createRequest);

            return CreatedAtAction("GetTax", new { taxId = newTax.Id }, newTax);
        }

        [HttpDelete]
        [Route("/Taxes/Tax/{taxId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteTax([FromRoute][Required]Guid taxId)
        {
            await _taxService.DeleteTaxByIdAsync(taxId);

            return NoContent();
        }

        [HttpGet]
        [Route("/Taxes/Tax/{taxId}")]
        [ActionName("GetTax")]
        [Authorize(Roles = "Admin, Manager, Staff, Customer")]
        public async Task<IActionResult> GetTaxById([FromRoute][Required]Guid taxId)
        {
            return Ok(await _taxService.GetTaxByIdAsync(taxId));
        }

        [HttpPut]
        [Route("/Taxes/Tax/{taxId}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateTax([FromRoute][Required]Guid taxId, [FromBody]TaxRequest updateRequest)
        {
            return Ok(await _taxService.UpdateTaxByIdAsync(taxId, updateRequest));
        }

        [HttpGet]
        [Route("/Taxes/Taxes")]
        [Authorize(Roles = "Admin, Manager, Staff, Customer")]
        public async Task<IActionResult> GetTaxes([FromQuery]TaxFilter taxFilter)
        {
            return Ok(await _taxService.GetTaxesAsync(taxFilter));
        }
    }
}
