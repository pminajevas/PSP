using PoS.Data.Repositories;
using PoS.Data.Context;
using PoS.Shared.Utilities;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;

namespace PoS.Services.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly BusinessRepository _businessRepository;

        public BusinessService(PoSDbContext dbContext)
        {
            _businessRepository = new BusinessRepository(dbContext);
        }

        public async Task<IEnumerable<BusinessResponse>> GetAllBusinessesAsync(Filter filter)
        {
            return await _businessRepository.GetAllBusinessesAsync(filter);
        }

        public async Task<BusinessResponse?> GetBusinessByIdAsync(Guid id)
        {
            return await _businessRepository.GetBusinessByIdAsync(id);
        }

        public async Task<BusinessResponse> AddBusinessAsync(BusinessRequest business)
        {
            return await _businessRepository.AddBusinessAsync(business);
        }

        public async Task<BusinessResponse?> UpdateBusinessAsync(BusinessRequest updatedBusiness, Guid businessId)
        {
            return await _businessRepository.UpdateBusinessAsync(updatedBusiness, businessId);
        }

        public async Task<bool> DeleteBusinessAsync(Guid id)
        {
            return await _businessRepository.DeleteBusinessAsync(id);
        }
    }
}
