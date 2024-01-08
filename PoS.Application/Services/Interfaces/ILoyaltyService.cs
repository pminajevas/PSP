using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface ILoyaltyService
    {
        public Task<LoyaltyProgramResponse> AddLoyaltyAsync(LoyaltyProgramRequest loyaltyRequest);

        public Task<List<LoyaltyProgramResponse>> GetLoyaltysAsync(LoyaltyFilter filter);

        public Task<LoyaltyProgramResponse?> GetLoyaltyByIdAsync(Guid loyaltyId);

        public Task<LoyaltyProgramResponse?> UpdateLoyaltyByIdAsync(Guid loyaltyId, LoyaltyProgramRequest loyaltyUpdateRequest);

        public Task<bool> DeleteLoyaltyByIdAsync(Guid loyaltyId);
    }
}
