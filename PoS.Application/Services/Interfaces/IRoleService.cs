using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface IRoleService
    {
        public Task DeleteRoleByIdAsync(Guid id);

        public Task<RoleResponse> GetRoleByRoleIdAsync(Guid id);

        public Task<RoleResponse> UpdateRoleAsync(Guid id, RoleRequest updateRequest);

        public Task<List<RoleResponse>> GetRolesAsync();

        public Task<RoleResponse> AddRoleAsync(RoleRequest roleRequest);
    }
}
