using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IBusinessRepository _businessRepository;

        public StaffService(
            IStaffRepository staffRepository,
            IMapper mapper,
            IRoleRepository roleRepository,
            IBusinessRepository businessRepository
        )
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _businessRepository = businessRepository;
        }

        public async Task<bool> DeleteStaffByIdAsync(Guid id)
        {
            if (await _staffRepository.DeleteAsync(id))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Staff with id - {id} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<StaffResponse> GetStaffByIdAsync(Guid id)
        {
            var staff = await _staffRepository.GetByIdAsync(id);

            if (staff is null)
            {
                throw new PoSException($"Staff with id - {id} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<StaffResponse>(staff);
        }

        public async Task<StaffResponse> UpdateStaffAsync(Guid id, StaffRequest staffRequest)
        {
            var staffToUpdate = _mapper.Map<Staff>(staffRequest);
            staffToUpdate.Id = id;

            var role = await _roleRepository.GetFirstAsync(x => x.Id == staffToUpdate.RoleId);

            if (role is null)
            {
                throw new PoSException($"Role with id - {staffToUpdate.RoleId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (role.RoleName != "Admin")
            {
                if (!await _businessRepository.Exists(x => x.Id == staffToUpdate.BusinessId))
                {
                    throw new PoSException($"Business with id - {staffToUpdate.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }
            else
            {
                staffToUpdate.BusinessId = Guid.Empty;
            }

            var oldStaff = await _staffRepository.GetFirstAsync(x => x.Id == id) ??
                throw new PoSException($"Staff with id - {id} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldStaff.LoginName != staffToUpdate.LoginName || oldStaff.BusinessId != staffToUpdate.BusinessId)
            {
                if (await _staffRepository.Exists(x => x.LoginName == staffToUpdate.LoginName
                && x.BusinessId == staffToUpdate.BusinessId))
                {
                    throw new PoSException($"Staff with login name - {staffToUpdate.LoginName} and business id - {staffToUpdate.BusinessId} already exists",
                        System.Net.HttpStatusCode.BadRequest);
                }
            }

            staffToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(staffToUpdate.Password);

            staffToUpdate = await _staffRepository.UpdateAsync(staffToUpdate);

            return _mapper.Map<StaffResponse>(staffToUpdate);
        }

        public async Task<List<StaffResponse>> GetStaffAsync(StaffFilter staffFilter)
        {
            var filter = PredicateBuilder.True<Staff>();
            Func<IQueryable<Staff>, IOrderedQueryable<Staff>>? orderBy = null;

            if (staffFilter.BusinessId != null)
            {
                filter = filter.And(x => x.BusinessId == staffFilter.BusinessId);
            }

            if (staffFilter.RoleName != null)
            {
                var role = await _roleRepository.GetFirstAsync(x => x.RoleName == staffFilter.RoleName);

                if (role is not null)
                {
                    filter = filter.And(x => x.RoleId == role.Id);
                }
            }

            if (staffFilter.OrderBy != string.Empty)
            {
                switch (staffFilter.Sorting)
                {
                    case Sorting.dsc:
                        orderBy = x => x.OrderByDescending(p => EF.Property<Staff>(p, staffFilter.OrderBy));
                        break;
                    default:
                        orderBy = x => x.OrderBy(p => EF.Property<Staff>(p, staffFilter.OrderBy));
                        break;
                }
            }

            var staff = await _staffRepository.GetAsync(
                filter,
                orderBy,
                staffFilter.ItemsToSkip(),
                staffFilter.PageSize
            );

            return _mapper.Map<List<StaffResponse>>(staff);
        }

        public async Task<StaffResponse> AddStaffAsync(StaffRequest createRequest)
        {
            var staff = _mapper.Map<Staff>(createRequest);

            var role = await _roleRepository.GetFirstAsync(x => x.Id == staff.RoleId);

            if (role is null)
            {
                throw new PoSException($"Role with id - {staff.RoleId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (role.RoleName != "Admin")
            {
                if (!await _businessRepository.Exists(x => x.Id == staff.BusinessId))
                {
                    throw new PoSException($"Business with id - {staff.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }
            else
            {
                staff.BusinessId = Guid.Empty;
            }

            if (await _staffRepository.Exists(x => x.LoginName == staff.LoginName
                && x.BusinessId == staff.BusinessId
                && x.Email == staff.Email))
            {
                throw new PoSException($"Staff with login name - {staff.LoginName}, business id - {staff.BusinessId} and email - {staff.Email} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            staff.Password = BCrypt.Net.BCrypt.HashPassword(staff.Password);

            staff = await _staffRepository.InsertAsync(staff);

            return _mapper.Map<StaffResponse>(staff);
        }
    }
}
