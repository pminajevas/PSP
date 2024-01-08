using AutoMapper;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

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
                if (!(await _roleRepository.DeleteAsync(roleId)))
                {
                    throw new PoSException($"Role with id - {roleId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }
            else
            {
                throw new PoSException($"Role with id - {roleId} is being used and can not be deleted", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<RoleResponse> GetRoleByRoleIdAsync(Guid roleId)
        {
            var role = await _roleRepository.GetAsync(x => x.Id == roleId);

            if (role is null)
            {
                throw new PoSException($"Role with id - {roleId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<RoleResponse>(role);
        }

        public async Task<RoleResponse> UpdateRoleAsync(Guid id, RoleRequest updateRequest)
        {
            var roleToUpdate = _mapper.Map<Role>(updateRequest);
            roleToUpdate.Id = id;

            var oldRole = await _roleRepository.GetFirstAsync(x => x.Id == id) ??
                throw new PoSException($"Role with id - {id} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldRole.RoleName != roleToUpdate.RoleName)
            {
                if (await _roleRepository.Exists(x => x.RoleName == roleToUpdate.RoleName))
                {
                    throw new PoSException($"Role with name - {roleToUpdate.RoleName} already exists", System.Net.HttpStatusCode.BadRequest);
                }
            }

            roleToUpdate = await _roleRepository.UpdateAsync(roleToUpdate);

            return _mapper.Map<RoleResponse>(roleToUpdate);
        }

        public async Task<List<RoleResponse>> GetRolesAsync()
        {
            var roles = await _roleRepository.GetAsync();

            return _mapper.Map<List<RoleResponse>>(roles);
        }

        public async Task<RoleResponse> AddRoleAsync(RoleRequest roleRequest)
        {
            var role = _mapper.Map<Role>(roleRequest);

            if (await _roleRepository.Exists(x => x.RoleName == role.RoleName))
            {
                throw new PoSException($"Role with name - {role.RoleName} already exists", System.Net.HttpStatusCode.BadRequest);
            }

            role = await _roleRepository.InsertAsync(role);

            return _mapper.Map<RoleResponse>(role);
        }
    }
}
