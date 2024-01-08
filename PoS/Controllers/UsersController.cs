using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using PoS.Application.Models.Requests;
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

        [HttpGet]
        [Route("/Users/Customers")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> GetCustomersAsync([FromQuery] CustomerFilter customerFilter)
        {
            return Ok(await _customerService.GetAllCustomersAsync(customerFilter));
        }

        [HttpPost]
        [Route("/Users/Customer")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerRequest customer)
        {
            var newCustomer = await _customerService.AddCustomerAsync(customer);

            return CreatedAtAction("GetCustomerAsync", new { customerId = newCustomer.Id }, newCustomer);
        }

        [HttpPut]
        [Route("/Users/Customer/{customerId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> UpdateCustomerAsync([FromRoute][Required] Guid customerId, [FromBody] CustomerRequest customer)
        {
            return Ok(await _customerService.UpdateCustomerAsync(customerId, customer));
        }

        [HttpDelete]
        [Route("/Users/Customer/{customerId}")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> DeleteCustomer([FromRoute][Required] Guid customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
            
            return NoContent();
        }

        [HttpGet]
        [Route("/Users/Customer/{customerId}")]
        [ActionName("GetCustomerAsync")]
        [Authorize(Roles = "Admin,Manager,Staff,Customer")]
        [SwaggerResponse(statusCode: 200, type: typeof(CustomerRequest), description: "Success")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute][Required] Guid customerId)
        {
            return Ok(await _customerService.GetCustomerByIdAsync(customerId));
        }

        [HttpDelete]
        [Route("/Users/Staff/{staffId}")]
        [Authorize(Roles = "Admin,Mananger")]
        public async Task<IActionResult> DeleteStaffAsync([FromRoute][Required] Guid staffId)
        {
            await _staffService.DeleteStaffByIdAsync(staffId);

            return NoContent();
        }

        [HttpGet]
        [Route("/Users/Staff/{staffId}")]
        [ActionName("GetStaffMemberAsync")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        public async Task<IActionResult> GetStaffMemberAsync([FromRoute][Required] Guid staffId)
        {
            return Ok(await _staffService.GetStaffByIdAsync(staffId));
        }

        [HttpPut]
        [Route("/Users/Staff/{staffId}")]
        [Authorize(Roles = "Admin,Mananger")]
        public async Task<IActionResult> UpdateStaffAsync([FromRoute][Required] Guid staffId, [FromBody] StaffRequest employee)
        {
            return Ok(await _staffService.UpdateStaffAsync(staffId, employee));
        }

        [HttpGet]
        [Route("/Users/Staffs")]
        [Authorize(Roles = "Admin,Mananger")]
        public async Task<IActionResult> GetAllStaffAsync(StaffFilter filter)
        {
            return Ok(await _staffService.GetStaffAsync(filter));
        }

        [HttpPost]
        [Route("/Users/Staff")]
        [Authorize(Roles = "Admin,Mananger")]
        public async Task<IActionResult> CreateStaffMemberAsync([FromBody] StaffRequest employee)
        {
            var newStaff = await _staffService.AddStaffAsync(employee);

            return CreatedAtAction("GetStaffMemberAsync", new { staffId = newStaff.Id }, newStaff);
        }

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute][Required] Guid roleId)
        {
            return Ok(await _roleService.GetRoleByRoleIdAsync(roleId));
        }

        [HttpPut]
        [Route("/Users/Role/{roleId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoleAsync([FromRoute][Required] Guid roleId, [FromBody] RoleRequest roleRequest)
        {
            return Ok(await _roleService.UpdateRoleAsync(roleId, roleRequest));
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRolesAsync()
        {
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpPost]
        [Route("/Users/Customer/Login")]
        public async Task<IActionResult> LoginCustomerAsync([FromBody] LoginRequest loginRequest)
        {
            return Ok(await _authorizationService.LoginAsync(loginRequest, LoginType.Customer));
        }

        [HttpPost]
        [Route("/Users/Customer/Logout/")]
        [Authorize(Roles = "Customer")]
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
        [Authorize(Roles = "Manager,Admin,Staff")]
        public IActionResult LogoutStaffAsync()
        {
            return Ok();
        }
    }
}
