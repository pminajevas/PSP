using Microsoft.Extensions.Configuration;
using PoS.Data;
using PoS.Shared.ResponseDTOs;
using PoS.Shared.RequestDTOs;
using PoS.Shared.Utilities;

namespace PoS.Services.Services
{
    public interface IUserService
    {
        Task<bool> DeleteCustomerAsync(Guid id);
        Task<CustomerResponse?> GetCustomerByIdAsync(Guid id);
        Task<CustomerResponse?> UpdateCustomerAsync(CustomerRequest updatedCustomer);
        Task<CustomerResponse?> LoginCustomerAsync(CustomerRequest customerLogin, IConfiguration configuration);
        Task<bool> LogoutCustomerAsync(Guid businessId, string loginName);
        Task<CustomerResponse?> AddCustomerAsync(CustomerRequest customerLogin, IConfiguration configuration);
        Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(Filter filter);



        Task<bool?> DeleteRoleAsync(string RoleName);
        Task<RoleResponse?> UpdateRoleAsync(RoleRequest updatedRoleDTO);
        Task<RoleResponse?> GetRoleAsync(string RoleName);
        Task<RoleResponse?> CreateRoleAsync(RoleRequest roleDTO);
        Task<IEnumerable<RoleResponse>> GetRolesAsync(Filter filter);


        Task<StaffResponse?> LoginStaffAsync(StaffRequest employeeLogin, IConfiguration configuration);
        Task<bool> LogoutStaffAsync(Guid businessId, string loginName);
        Task<StaffResponse?> AddStaffAsync(StaffRequest employeeLogin, IConfiguration configuration);
        Task<bool> DeleteStaffAsync(Guid id);
        Task<StaffResponse?> GetStaffByIdAsync(Guid id);
        Task<StaffResponse?> UpdateStaffAsync(StaffRequest updatedCustomer);
        Task<IEnumerable<StaffResponse>> GetAllStaffAsync(Filter filter);


        Task<bool> HasAccessToBusinessAsync(string loginName, Guid businessId);
        Task<bool> IsAdminCreatedAsync();
        Task<UserResponse?> LoginAdminAsync(UserRequest adminLogin, IConfiguration configuration);
        Task<UserResponse?> CreateUserAsync(UserRequest userRequest, IConfiguration configuration);
        Task<IEnumerable<UserLoginResponse>> GetUserLoginSessionsAsync(Guid userId);

    }
}