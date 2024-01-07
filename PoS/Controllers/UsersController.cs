using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PoS.Application.Models.Requests;
using PoS.Core.Entities;
using PoS.Application.Filters;
using PoS.Application.Services.Interfaces;
using PoS.Application.Models.Enums;

namespace PoS.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IStaffService _staffService;
        private readonly IRoleService _roleService;
        private readonly Application.Services.Interfaces.IAuthorizationService _authorizationService;

        public UsersController(
            ICustomerService customerService,
            IStaffService staffService,
            IRoleService roleService,
            Application.Services.Interfaces.IAuthorizationService auhorizationService
        )
        {
            _customerService = customerService;
            _staffService = staffService;
            _roleService = roleService;
            _authorizationService = auhorizationService;
        }

        // ******************* CUSTOMERS BEGIN ************************
        [HttpGet]
        [Route("/Users/Customers")]
        [Authorize(Roles = "Admin, Manager, Staff")]
        public async Task<IActionResult> GetCustomersAsync([FromQuery] CustomerFilter customerFilter)
        {
            return Ok(await _customerService.GetAllCustomersAsync(customerFilter));
        }

        [HttpPost]
        [Route("/Users/Customer")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerRequest customer)
        {
            var newCustomer = await _customerService.AddCustomerAsync(customer);

            return CreatedAtAction("GetCustomerAsync", new { customerId = newCustomer.Id }, newCustomer);
        }

        [HttpPut]
        [Route("/Users/Customer/{customerId}")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomerAsync([FromRoute][Required] Guid customerId, [FromBody] CustomerRequest customer)
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(customerId, customer);

            if (updatedCustomer != null)
            {
                return Ok(updatedCustomer);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("/Users/Customer/{customerId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer([FromRoute][Required] Guid customerId)
        {
            if (await _customerService.DeleteCustomerAsync(customerId) == true)
            {
                return NoContent();
            }
            
            return NotFound();
           
        }

        [HttpGet]
        [Route("/Users/Customer/{customerId}")]
        [ActionName("GetCustomerAsync")]
        [Authorize]
        [SwaggerResponse(statusCode: 200, type: typeof(CustomerRequest), description: "Success")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute][Required] Guid customerId)
        {
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // ******************* CUSTOMERS END ************************

        // ******************* STAFF BEGIN ************************

        [HttpDelete]
        [Route("/Users/Staff/{staffId}")]
        [Authorize(Roles = "Admin,Mananger")]
        public async Task<IActionResult> DeleteStaffAsync([FromRoute][Required] Guid staffId)
        {
            if (await _staffService.DeleteStaffByIdAsync(staffId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/Users/Staff/{staffId}")]
        [ActionName("GetStaffMemberAsync")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        public async Task<IActionResult> GetStaffMemberAsync([FromRoute][Required] Guid staffId)
        {
            var result = await _staffService.GetStaffByIdAsync(staffId);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("/Users/Staff/{staffId}")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        public async Task<IActionResult> UpdateStaffAsync([FromRoute][Required] Guid staffId, [FromBody] StaffRequest employee)
        {
            var updatedEmployee = await _staffService.UpdateStaffAsync(staffId, employee);

            if (updatedEmployee != null)
            {
                return Ok(updatedEmployee);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("/Users/Staffs")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        public async Task<IActionResult> GetAllStaffAsync(StaffFilter filter)
        {
            return Ok(await _staffService.GetStaffAsync(filter));
        }

        [HttpPost]
        [Route("/Users/Staff")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        public async Task<IActionResult> CreateStaffMemberAsync([FromBody] StaffRequest employee)
        {
            var newStaff = await _staffService.AddStaffAsync(employee);

            return CreatedAtAction("GetStaffMemberAsync", new { staffId = newStaff.Id }, newStaff);
        }

        // ******************* STAFF END ************************

        // ******************* ROLES START ************************

        [HttpDelete]
        [Route("/Users/Role/{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute][Required] Guid roleId)
        {
            await _roleService.DeleteRoleByIdAsync(roleId);

            return NoContent();
        }

        [HttpGet]
        [Route("/Users/Role/{roleId}")]
        [ActionName("GetRoleAsync")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute][Required] Guid roleId)
        {
            return Ok(_roleService.GetRoleByRoleIdAsync(roleId));
        }

        [HttpPut]
        [Route("/Users/Role/{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute][Required] Guid roleId, [FromBody] RoleRequest roleRequest)
        {
            var updatedRole = await _roleService.UpdateRoleAsync(roleId, roleRequest);

            if (updatedRole != null)
            {
                return Ok(updatedRole);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("/Users/Role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleRequest roleRequest)
        {
            var newRole = await _roleService.AddRoleAsync(roleRequest);

            return CreatedAtAction("GetRoleAsync", new { newRole = newRole.Id }, newRole);
        }

        [HttpGet]
        [Route("/Users/Roles")]
        public async Task<IActionResult> GetRolesAsync()
        {
            return Ok(await _roleService.GetRolesAsync());
        }

        // ******************* ROLES END ************************

        // ******************* AUTHORIZATION START ************************

        [HttpPost]
        [Route("/Users/Customer/Login")]
        public async Task<IActionResult> LoginCustomerAsync([FromBody] LoginRequest loginRequest)
        {
            return Ok(await _authorizationService.LoginAsync(loginRequest, LoginType.Customer));
        }

        [HttpPost]
        [Route("/Users/Customer/Logout/")]
        [Authorize]
        public IActionResult LogoutCustomerAsync()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/Users/Admin/Login")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequest loginRequest)
        {
            return Ok(await _authorizationService.LoginAsync(loginRequest, LoginType.Staff));
        }

        [HttpPost]
        [Route("/Users/Staff/Login")]
        public async Task<IActionResult> LoginStaffAsync([FromBody] LoginRequest loginRequest)
        {
            return Ok(await _authorizationService.LoginAsync(loginRequest, LoginType.Staff));
        }

        [HttpPost]
        [Route("/Users/Staff/Logout/")]
        [Authorize]
        public IActionResult LogoutStaffAsync()
        {
            return Ok();
        }

        // ******************* AUTHORIZATION END ************************
    }
}
