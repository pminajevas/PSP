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

using Microsoft.AspNetCore.Authorization;
using PoS.Data;
using PoS.Data.Context;
using PoS.Data.Repositories;
using PoS.API.Helpers;
using PoS.Services.Services;
using System.Security.Cryptography.X509Certificates;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using PoS.Shared.Utilities;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;

namespace PoS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class UsersApiController : ControllerBase
    {

        private IUserService _userService;
        private IFilterValidator _validator;
        private IConfiguration _configuration;
        public UsersApiController(IUserService userService, IFilterValidator validator, IConfiguration configuration)
        {
            _userService = userService;
            _validator = validator;
            _configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <response code="200">No Content</response>
        /// 


        [HttpPost]
        [Route("/Users/Admin/Create")]
        public async Task<IActionResult> CreateAdmin([FromBody] UserRequest user)
        {
            try
            {
                if(!await _userService.IsAdminCreatedAsync())
                {
                    user.RoleName= "Admin";
                    var newUser = await _userService.CreateUserAsync(user, _configuration);
                    return Ok(newUser);
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("/Users/Admin/Login")]
        public async Task<IActionResult> LoginAsAdmin([FromBody] UserRequest user)
        {
            try
            {
                if (await _userService.IsAdminCreatedAsync())
                {
                    user.RoleName = "Admin";
                    var newUser = await _userService.LoginAdminAsync(user, _configuration);
                    return Ok(newUser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete]
        [Route("/Users/Customer/{customerId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer([FromRoute][Required]Guid customerId)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null && 
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    if (await _userService.DeleteCustomerAsync(customerId) == true)
                    {
                        return NoContent();
                    }
                    else return NotFound();
                } 
                else return Forbid();


            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Users/Customer/{customerId}")]
        [ActionName("GetCustomerAsync")]
        [Authorize]
        [SwaggerResponse(statusCode: 200, type: typeof(CustomerRequest), description: "Success")]
        public async Task<IActionResult> GetCustomerAsync([FromRoute][Required]Guid customerId)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    var result = await _userService.GetCustomerByIdAsync(customerId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else return Forbid();             
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Users/Customer")]
        [Authorize]
        [SwaggerResponse(statusCode: 200, type: typeof(Customer), description: "Success")]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody]CustomerRequest customer)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    var updatedCustomer = await _userService.UpdateCustomerAsync(customer);
                    if (updatedCustomer != null)
                    {
                        return Ok(updatedCustomer);
                    }
                    return NotFound();
                }
                else return Forbid();               
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("/Users/Customer/Login")]
        [SwaggerResponse(statusCode: 200, type: typeof(CustomerResponse), description: "Success")]
        public async Task<IActionResult> LoginCustomerAsync([FromBody] CustomerRequest customerLogin)
        {
            try
            {
                var cutomer = await _userService.LoginCustomerAsync(customerLogin, _configuration);
                if(cutomer != null)
                {
                    return Ok(cutomer);
                }
                else
                {
                    return BadRequest("Customer not found or haven't logged out");
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("/Users/Customer/Logout/")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> LogoutCustomerAsync()
        {
            try
            {
                var businessId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;

                if (await _userService.LogoutCustomerAsync(businessId, loginName) == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Customer not found or haven't logged in");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Users/Customer")]
        [SwaggerResponse(statusCode: 201, type: typeof(CustomerRequest), description: "Created")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerRequest customer)
        {
            try
            {
                //Validator.ValidateObject(business, new ValidationContext(business), true);
                var newCustomer = await _userService.AddCustomerAsync(customer, _configuration);
                if(newCustomer != null)
                {
                    return CreatedAtAction("GetCustomerAsync", new { customerId = newCustomer.Id }, newCustomer);
                }
                return BadRequest("LoginName already taken");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="loyaltyId"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Users/Customers")]
        [Authorize(Roles = "Admin,Manager,Staff")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Customer>), description: "Success")]
        public async Task<IActionResult> GetCustomersAsync([FromQuery] Guid? businessId = null, [FromQuery] Guid? loyaltyId = null, [FromQuery] string? orderBy = null, [FromQuery] string? sorting = null, [FromQuery] int? pageIndex = null, [FromQuery] int? pageSize = null)
        {
            Filter filter = new Filter();

            // Add supported parameters using the AddParameter method

            filter.AddParameter("BusinessId", businessId);
            filter.AddParameter("LoyaltyId", loyaltyId);
            filter.AddParameter("OrderBy", orderBy);
            filter.AddParameter("Sorting", sorting);
            filter.AddParameter("PageIndex", pageIndex);
            filter.AddParameter("PageSize", pageSize);
            if (_validator.ValidateFilter(filter))
            {
                try
                {
                    return Ok(await _userService.GetAllCustomersAsync(filter));
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }

            }
            return BadRequest("Incorrect filters");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Users/Role/{RoleName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute][Required] string RoleName)
        {
            try
            {
                var status = await _userService.DeleteRoleAsync(RoleName);
                if (status != null)
                {
                    if(status == true)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return StatusCode(405, "Some users are stil using this role");
                    }
                        
                }
                else return NotFound();

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Users/Role/{RoleName}")]
        [ActionName("GetRoleAsync")]
        [SwaggerResponse(statusCode: 200, type: typeof(RoleRequest), description: "Success")]
        public async Task<IActionResult> GetRoleAsync([FromRoute][Required] string RoleName)
        {
            try
            {
                var result = await _userService.GetRoleAsync(RoleName);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Users/Role/")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 201, type: typeof(RoleRequest), description: "Success")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] RoleRequest body)
        {
            try
            {
                var updatedRole = await _userService.UpdateRoleAsync(body);
                if (updatedRole != null)
                {
                    return Ok(updatedRole);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Users/Role")]
        [SwaggerResponse(statusCode: 201, type: typeof(Role), description: "Created")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleRequest role)
        {
            try
            {
                var newRole = await _userService.CreateRoleAsync(role);
                if (newRole != null)
                {
                    return CreatedAtAction("GetRoleAsync", new { roleName = newRole.RoleName }, newRole);
                }
                return BadRequest("Role already exists");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Users/Roles")]
        [SwaggerResponse(statusCode: 201, type: typeof(Role), description: "Created")]
        public async Task<IActionResult> GetRolesAsync([FromQuery] Guid? roleId = null, [FromQuery] string? roleName = null, [FromQuery] string? orderBy = null, [FromQuery] string? sorting = null, [FromQuery] int? pageIndex = null, [FromQuery] int? pageSize = null)
        {
            Filter filter = new Filter();

            // Add supported parameters using the AddParameter method

            filter.AddParameter("RoleId", roleId);
            filter.AddParameter("RoleName", roleName);
            filter.AddParameter("OrderBy", orderBy);
            filter.AddParameter("Sorting", sorting);
            filter.AddParameter("PageIndex", pageIndex);
            filter.AddParameter("PageSize", pageSize);
            if (_validator.ValidateFilter(filter))
            {
                try
                {
                    return Ok(await _userService.GetRolesAsync(filter));
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }

            }
            return BadRequest("Incorrect filters");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("/Users/Staff/Login")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Role>), description: "Success")]
        public async Task<IActionResult> LoginStaffAsync([FromBody] StaffRequest employeeLogin)
        {
            try
            {
                var employeeResponse = await _userService.LoginStaffAsync(employeeLogin, _configuration);
                if (employeeResponse != null)
                {
                    return Ok(employeeResponse);
                }
                else
                {
                    return BadRequest("Staff member not found or haven't logged out");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <response code="200">Success</response>
        [HttpPost]
        [Route("/Users/Staff/Logout/")]
        [Authorize]
        public async Task<IActionResult> LogoutStaffAsync()
        {
            try
            {
                var businessId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;

                if (await _userService.LogoutStaffAsync(businessId, loginName) == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Staff member not found or haven't logged in");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <response code="201">Created</response>
        [HttpPost]
        [Route("/Users/Staff")]
        [Authorize(Roles="Admin,Mananger,Staff")]
        [SwaggerResponse(statusCode: 201, type: typeof(StaffRequest), description: "Created")]
        public async Task<IActionResult> CreateStaffMemberAsync([FromBody] StaffRequest employee)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    var newEmployee = await _userService.AddStaffAsync(employee, _configuration);
                    if (newEmployee != null)
                    {
                        return CreatedAtAction("GetStaffMemberAsync", new { staffId = newEmployee.Id }, newEmployee);
                    }
                    return BadRequest("LoginName already taken");
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <response code="204">No Content</response>
        [HttpDelete]
        [Route("/Users/Staff/{staffId}")]
        [Authorize(Roles = "Admin,Mananger")]
        public async Task<IActionResult> DeleteStaffAsync([FromRoute][Required] Guid staffId)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    if (await _userService.DeleteStaffAsync(staffId) == true)
                    {
                        return NoContent();
                    }
                    else return NotFound();
                }
                else return Forbid();


            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Users/Staff/{staffId}")]
        [ActionName("GetStaffMemberAsync")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        [SwaggerResponse(statusCode: 200, type: typeof(StaffRequest), description: "Success")]
        public async Task<IActionResult> GetStaffMemberAsync([FromRoute][Required] Guid staffId)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    var result = await _userService.GetStaffByIdAsync(staffId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="body"></param>
        /// <response code="200">Success</response>
        [HttpPut]
        [Route("/Users/Staff")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        [SwaggerResponse(statusCode: 200, type: typeof(StaffRequest), description: "Success")]
        public async Task<IActionResult> UpdateStaffAsync([FromBody] StaffRequest employee)
        {
            try
            {
                var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                if (User.IsInRole("Admin") ||
                    (businessIdClaim != null &&
                    Guid.TryParse(businessIdClaim.Value, out var businessId) &&
                    await _userService.HasAccessToBusinessAsync(loginName, businessId)))
                {
                    var updatedEmployee = await _userService.UpdateStaffAsync(employee);
                    if (updatedEmployee != null)
                    {
                        return Ok(updatedEmployee);
                    }
                    return NotFound();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="role"></param>
        /// <param name="orderBy"></param>
        /// <param name="sorting"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Users/Staffs")]
        [Authorize(Roles = "Admin,Mananger,Staff")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<StaffRequest>), description: "Success")]
        public async Task<IActionResult> GetAllStaffAsync(
        [FromQuery] Guid? businessId = null,
        [FromQuery] string? role = null,
        [FromQuery] string? firstName = null,
        [FromQuery] string? lastName = null,
        [FromQuery] string? phoneNumber = null,
        [FromQuery] string? email = null,
        [FromQuery] string? address = null,
        [FromQuery] DateTime? hireDate = null,
        [FromQuery] string? orderBy = null,
        [FromQuery] string? sorting = null,
        [FromQuery] int? pageIndex = null,
        [FromQuery] int? pageSize = null)
        {
         
            Filter filter = new Filter();

            filter.AddParameter("BusinessId", businessId);
            filter.AddParameter("RoleName", role);
            filter.AddParameter("FirstName", firstName);
            filter.AddParameter("LastName", lastName);
            filter.AddParameter("PhoneNumber", phoneNumber);
            filter.AddParameter("Email", email);
            filter.AddParameter("HireDate", hireDate);
            filter.AddParameter("OrderBy", orderBy);
            filter.AddParameter("Sorting", sorting);
            filter.AddParameter("PageIndex", pageIndex);
            filter.AddParameter("PageSize", pageSize);

            if (_validator.ValidateFilter(filter))
            {
                try
                {
                    var businessIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                    var loginName = User.FindFirst(ClaimTypes.Name)!.Value;
                    if (User.IsInRole("Admin") ||
                        (businessIdClaim != null &&
                        Guid.TryParse(businessIdClaim.Value, out var businessIdFromClaim) &&
                        await _userService.HasAccessToBusinessAsync(loginName, businessIdFromClaim)))
                    {
                        return Ok(await _userService.GetAllStaffAsync(filter));
                    }
                    else return Forbid();
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }

            }
            return BadRequest("Incorrect filters");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginId"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/Users/UserLogin/{userId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(statusCode: 200, type: typeof(Data.UserLogin), description: "Success")]
        public async Task<IActionResult> GetUserLoginSessionsAsync([FromRoute][Required] Guid userId)
        {
            try
            {
                var result = await _userService.GetUserLoginSessionsAsync(userId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
