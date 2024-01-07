using AutoMapper;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;

namespace PoS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IStaffRepository _staffRepository;

        public RoleService(
            IRoleRepository roleRepository,
            IStaffRepository staffRepository,
            ICustomerRepository customerRepository,
            IMapper mapper
        )
        {
            _customerRepository = customerRepository;
            _staffRepository = staffRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task DeleteRoleByIdAsync(Guid roleId)
        {
            if (await _customerRepository.Exists(x => x.RoleId == roleId) 
                && await _staffRepository.Exists(x => x.RoleId == roleId))
            {
                await _roleRepository.DeleteAsync(roleId);
            }
        }

        public async Task<RoleResponse> GetRoleByRoleIdAsync(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(x => x.Id == roleId);

            return _mapper.Map<RoleResponse>(role);
        }

        public async Task<RoleResponse> UpdateRoleAsync(Guid id, RoleRequest updateRequest)
        {
            var roleToUpdate = _mapper.Map<Role>(updateRequest);
            roleToUpdate.Id = id;

            return _mapper.Map<RoleResponse>(await _roleRepository.UpdateAsync(roleToUpdate));
        }

        public async Task<List<RoleResponse>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetAsync();

            return _mapper.Map<List<RoleResponse>>(roles);
        }

        public async Task<RoleResponse> AddRoleAsync(RoleRequest roleRequest)
        {
            var role = _mapper.Map<Role>(roleRequest);

            role = await _roleRepository.InsertAsync(role);

            return _mapper.Map<RoleResponse>(role);
        }
    }
}
