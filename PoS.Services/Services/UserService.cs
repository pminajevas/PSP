using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PoS.Data;
using PoS.Data.Context;
using PoS.Data.Mapper;
using PoS.Data.Repositories;
using PoS.Shared.InnerDTOs;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using PoS.Shared.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PoS.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(PoSDbContext dbContext)
        {
            _userRepository = new UserRepository(dbContext);

        }
        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            return await _userRepository.DeleteCustomerAsync(id);
        }

        public async Task<CustomerResponse?> GetCustomerByIdAsync(Guid id)
        {
            return await _userRepository.GetCustomerByIdAsync(id);
        }

        public async Task<CustomerResponse?> UpdateCustomerAsync(CustomerRequest updatedCustomer)
        {
            return await _userRepository.UpdateCustomerAsync(updatedCustomer);
        }

        public async Task<bool> LogoutCustomerAsync(Guid businessId, string loginName)
        {
            return await _userRepository.LogoutUserAsync(businessId, loginName);
        }

        public async Task<CustomerResponse?> LoginCustomerAsync(CustomerRequest customerLogin, IConfiguration configuration)
        {
            var password = customerLogin.Password;
            var loginName = customerLogin.LoginName;
            var businessId = customerLogin.BusinessId;

            if (!await VerifyAccount(businessId, loginName, password))
            {
                return null;
            }
            else
            {
                var user = new UserInner()
                {
                    LoginName = loginName,
                    BusinessId = businessId,
                    RoleName = "Customer"
                };
                var jwtToken = CreateToken(user, configuration);
                var customer = await _userRepository.LoginCutomerWithLoginNameAsync(businessId, loginName);
                if (customer == null)
                {
                    return null;
                }
                else
                {
                    customer.LoginName = loginName;
                    customer.JwtToken = jwtToken;
                    return customer;
                }
            }
        }

        public async Task<CustomerResponse?> AddCustomerAsync(CustomerRequest customerLogin, IConfiguration configuration)
        {
            var password = customerLogin.Password;
            var loginName = customerLogin.LoginName;
            var businessId = customerLogin.BusinessId;

            if (await _userRepository.GetUserByLoginNameAndBusiness(businessId, loginName) == null)
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var userDto = new UserInner()
                {
                    LoginName = loginName,
                    BusinessId = businessId,
                    RoleName = "Customer"
                };
                var jwtToken = CreateToken(userDto, configuration);
                var customer = await _userRepository.AddCustomerAsync(customerLogin, passwordHash, passwordSalt);
                if (customer == null)
                {
                    return null;
                }
                else
                {
                    customer.JwtToken = jwtToken;
                    return customer;
                }

            }
            return null;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(Filter filter)
        {
            return await _userRepository.GetAllCustomersAsync(filter);
        }



        public async Task<bool?> DeleteRoleAsync(string RoleName)
        {
            if (!await _userRepository.IsRoleUsedAsync(RoleName))
            {
                if(await _userRepository.DeleteRoleAsync(RoleName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<RoleResponse?> CreateRoleAsync(RoleRequest roleDTO)
        {
            return await _userRepository.CreateRoleAsync(roleDTO);
        }

        public async Task<RoleResponse?> UpdateRoleAsync(RoleRequest updatedRoleDTO)
        {
            return await _userRepository.UpdateRoleAsync(updatedRoleDTO);
        }
        public async Task<RoleResponse?> GetRoleAsync(string RoleName)
        {
            return await _userRepository.GetRoleAsync(RoleName);
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesAsync(Filter filter)
        {
            return await _userRepository.GetRolesAsync(filter);
        }


        public async Task<StaffResponse?> LoginStaffAsync(StaffRequest employeeLogin, IConfiguration configuration)
        {
            var password = employeeLogin.Password;
            var loginName = employeeLogin.LoginName;
            var businessId = employeeLogin.BusinessId;

            if (!await VerifyAccount(businessId, loginName, password))
            {
                return null;
            }
            else
            {
                var user = new UserInner()
                {
                    LoginName = loginName,
                    BusinessId = businessId,
                    RoleName = employeeLogin.RoleName
                };
                var jwtToken = CreateToken(user, configuration);
                var employee = await _userRepository.LoginStaffAsync(businessId, loginName);
                if (employee == null)
                {
                    return null;
                }
                else
                {
                    employee.LoginName = loginName;
                    employee.JwtToken = jwtToken;
                    return employee;
                }
            }
        }


        public async Task<StaffResponse?> AddStaffAsync(StaffRequest employeeLogin, IConfiguration configuration)
        {
            var password = employeeLogin.Password;
            var loginName = employeeLogin.LoginName;
            var businessId = employeeLogin.BusinessId;

            if (await _userRepository.GetUserByLoginNameAndBusiness(businessId, loginName) == null)
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var userDto = new UserInner()
                {
                    LoginName = loginName,
                    BusinessId = businessId,
                    RoleName = "Customer"
                };
                var jwtToken = CreateToken(userDto, configuration);
                var employee = await _userRepository.AddStaffAsync(employeeLogin, passwordHash, passwordSalt);
                if (employee == null)
                {
                    return null;
                }
                else
                {
                    employee.JwtToken = jwtToken;
                    return employee;
                }

            }
            return null;
        }

        public async Task<bool> LogoutStaffAsync(Guid businessId, string loginName)
        {
            return await _userRepository.LogoutUserAsync(businessId, loginName);
        }

        public async Task<bool> DeleteStaffAsync(Guid id)
        {
            return await _userRepository.DeleteStaffAsync(id);
        }

        public async Task<StaffResponse?> GetStaffByIdAsync(Guid id)
        {
            return await _userRepository.GetStaffByIdAsync(id);
        }

        public async Task<StaffResponse?> UpdateStaffAsync(StaffRequest updatedCustomer)
        {
            return await _userRepository.UpdateStaffAsync(updatedCustomer);
        }

        public async Task<IEnumerable<StaffResponse>> GetAllStaffAsync(Filter filter)
        {
            return await _userRepository.GetAllStaffAsync(filter);
        }

        public async Task<IEnumerable<UserLoginResponse>> GetUserLoginSessionsAsync(Guid userId)
        {
            return await _userRepository.GetUserLoginSessionsAsync(userId);
        }

        public async Task<UserResponse?> CreateUserAsync(UserRequest userRequest, IConfiguration configuration)
        {
            var password = userRequest.Password;
            var loginName = userRequest.LoginName;

            if (await _userRepository.GetAdminAsync() == null)
            {
                byte[] passwordHash;
                byte[] passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var userDto = new UserInner()
                {
                    LoginName = loginName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleName = "Admin"
                };
                var jwtToken = CreateToken(userDto, configuration);
                var newUser = await _userRepository.CreateUserAsync(userDto);
                if (newUser == null)
                {
                    return null;
                }
                else
                {
                    newUser.JwtToken = jwtToken;
                    return newUser;
                }

            }
            return null;
        }

        public async Task<bool> IsAdminCreatedAsync()
        {
            if(await _userRepository.GetAdminAsync() == null)
            {
                return false;
            }
            return true;
        }

        public async Task<UserResponse?> LoginAdminAsync(UserRequest adminLogin, IConfiguration configuration)
        {
            var password = adminLogin.Password;
            var loginName = adminLogin.LoginName;
            if (!await VerifyAdmin(loginName, password))
            {
                return null;
            }
            else
            {
                var user = new UserInner()
                {
                    LoginName = loginName,
                    RoleName = adminLogin.RoleName
                };
                var jwtToken = CreateToken(user, configuration);
                var admin = await _userRepository.GetAdminAsync();
                if (admin == null)
                {
                    return null;
                }
                else
                {
                    admin.JwtToken = jwtToken;
                    return admin;
                }
            }
        }


        public async Task<bool> HasAccessToBusinessAsync(string loginName, Guid businessId)
        {
            var user = await _userRepository.GetUserByLoginNameAndBusiness(businessId, loginName);
            if (user == null) {
                return false;
            }
            return true;
        }


        private async Task<bool> VerifyAdmin(string loginName, string password)
        {

            var admin = await _userRepository.GetUserByLoginNameAndRole("Admin", loginName);

            if (admin == null || admin.PasswordHash == null || admin.PasswordSalt == null)
            {
                return false;
            }
            else
            {
                using (var hmac = new HMACSHA512(admin.PasswordSalt!))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(admin.PasswordHash);
                }
            }
        }



        private async Task<bool> VerifyAccount(Guid businessId, string loginName, string password)
        {

            var user = await _userRepository.GetUserByLoginNameAndBusiness(businessId, loginName);

            if (user == null || user.PasswordHash == null || user.PasswordSalt == null) {
                return false;
            }
            else {
                using (var hmac = new HMACSHA512(user.PasswordSalt!))
                {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    return computedHash.SequenceEqual(user.PasswordHash);
                }
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateToken(UserInner user, IConfiguration _configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.LoginName),
                new Claim(ClaimTypes.Role, user.RoleName!),                          
            };
            if (user.BusinessId != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.BusinessId.ToString()!));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);


            return jwt;
        }
    }
}
