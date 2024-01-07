using PoS.Application.Models.Enums;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using System.Security.Claims;

namespace PoS.Application.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<LoginResponse?> LoginAsync(LoginRequest loginRequest, LoginType type);

        public Task<bool> IsUserAdminOrBusinessManager(ClaimsPrincipal user);
    }
}
