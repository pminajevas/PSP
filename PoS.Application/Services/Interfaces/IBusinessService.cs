using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface IBusinessService
    {
        Task<BusinessResponse> AddBusinessAsync(BusinessRequest business);
        Task<bool> DeleteBusinessAsync(Guid id);
        Task<IEnumerable<BusinessResponse>> GetAllBusinessesAsync(BusinessesFilter businessFilter);
        Task<BusinessResponse?> GetBusinessByIdAsync(Guid id);
        Task<BusinessResponse?> UpdateBusinessAsync(BusinessRequest updatedBusiness, Guid businessId);
    }
}