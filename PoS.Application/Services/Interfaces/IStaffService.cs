using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface IStaffService
    {
        public Task<bool> DeleteStaffByIdAsync(Guid id);

        public Task<StaffResponse> GetStaffByIdAsync(Guid id);

        public Task<StaffResponse> UpdateStaffAsync(Guid id, StaffRequest staffRequest);

        public Task<List<StaffResponse>> GetStaffAsync(StaffFilter staffFilter);

        public Task<StaffResponse> AddStaffAsync(StaffRequest createRequest);
    }
}
