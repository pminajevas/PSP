using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Models.Enums;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;

namespace PoS.Application.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthorizationService(
            IConfiguration configuration,
            ICustomerRepository customerRepository,
            IStaffRepository staffRepository,
            IRoleRepository roleRepository
        )
        {
            _configuration = configuration;
            _customerRepository = customerRepository;
            _staffRepository = staffRepository;
            _roleRepository = roleRepository;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest loginRequest, LoginType type)
        {
            Guid? businessId = null;
            string? roleName = null;

            switch (type)
            {
                case LoginType.Staff:
                    {
                        var staff = await _staffRepository.GetFirstAsync(x => x.LoginName == loginRequest.LoginName);

                        if (staff is not null)
                        {
                            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, staff.Password))
                            {
                                throw new AuthenticationException("Invalid password");
                            }

                            var role = await _roleRepository.GetFirstAsync(x => x.Id == staff.RoleId);

                            if (role is null)
                            {
                                throw new AuthenticationException("No role assigned to user");
                            }

                            roleName = role.RoleName;
                            businessId = staff.BusinessId;
                        }

                        break;
                    }
                case LoginType.Customer:
                    {
                        var customer = await _customerRepository.GetFirstAsync(x => x.LoginName == loginRequest.LoginName);

                        if (customer is not null)
                        {
                            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, customer.Password))
                            {
                                throw new AuthenticationException("Invalid password");
                            }

                            var role = await _roleRepository.GetFirstAsync(x => x.Id == customer.RoleId);

                            if (role is null)
                            {
                                throw new AuthenticationException("No role assigned to user");
                            }

                            roleName = role.RoleName;
                            businessId = customer.BusinessId;
                        }

                        break;
                    }
                default:
                    throw new Exception("Invalid login type");
            }

            if (roleName is not null && businessId is not null)
            {
                var jwtToken = CreateToken(loginRequest.LoginName, roleName, businessId.ToString() ?? throw new ArgumentException("Could not convert bussines id"));

                return new LoginResponse { Token = jwtToken };
            }

            throw new Exception("Could not successfully check identity");
        }

        private string CreateToken(string loginName, string roleName, string businessId)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginName),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(ClaimTypes.NameIdentifier, businessId)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                (_configuration.GetSection("AppSettings:Token").Value ?? throw new ApplicationException("Token settings not set"))));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<bool> IsUserAdminOrBusinessManager(ClaimsPrincipal user)
        {
            bool managerCanViewBusiness = false;
            var businessIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            var loginName = user.FindFirst(ClaimTypes.Name);

            if (businessIdClaim is not null && loginName is not null)
            {
                Guid bussinessId;
                Guid.TryParse(businessIdClaim.Value, out bussinessId);

                managerCanViewBusiness = await HasAccessToBusinessAsync(loginName.Value, bussinessId);
            }

            return user.IsInRole("Admin") || managerCanViewBusiness;
        }

        private async Task<bool> HasAccessToBusinessAsync(string loginName, Guid businessId)
        {
            var staff = await _staffRepository.GetFirstAsync(x => x.BusinessId == businessId && x.LoginName == loginName);
            var customer = await _customerRepository.GetFirstAsync(x => x.BusinessId == businessId && x.LoginName == loginName);

            if (staff == null && customer == null)
            {
                return false;
            }

            return true;
        }
    }
}
