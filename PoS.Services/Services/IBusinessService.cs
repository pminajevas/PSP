using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using PoS.Shared.Utilities;

namespace PoS.Services.Services
{
    public interface IBusinessService
    {
        Task<BusinessResponse> AddBusinessAsync(BusinessRequest business);
        Task<bool> DeleteBusinessAsync(Guid id);
        Task<IEnumerable<BusinessResponse>> GetAllBusinessesAsync(Filter filter);
        Task<BusinessResponse?> GetBusinessByIdAsync(Guid id);
        Task<BusinessResponse?> UpdateBusinessAsync(BusinessRequest updatedBusiness, Guid businessId);
    }
}