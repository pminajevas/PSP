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
    public class LoyaltyService : ILoyaltyService
    {
        private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public LoyaltyService(
            ILoyaltyProgramRepository loyaltyProgramRepository,
            IMapper mapper,
            IBusinessRepository businessRepository)
        {
            _loyaltyProgramRepository = loyaltyProgramRepository;
            _mapper = mapper;
            _businessRepository = businessRepository;
        }

        public async Task<LoyaltyProgramResponse> AddLoyaltyAsync(LoyaltyProgramRequest loyaltyRequest)
        {
            var loyalty = _mapper.Map<LoyaltyProgram>(loyaltyRequest);

            if (!await _businessRepository.Exists(x => x.Id == loyalty.BusinessId))
            {
                throw new PoSException($"Business with id - {loyalty.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<LoyaltyProgramResponse>(await _loyaltyProgramRepository.InsertAsync(loyalty));
        }

        public async Task<bool> DeleteLoyaltyByIdAsync(Guid loyaltyId)
        {
            if (await _loyaltyProgramRepository.DeleteAsync(loyaltyId))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Loyalty with id - {loyaltyId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<LoyaltyProgramResponse?> GetLoyaltyByIdAsync(Guid loyaltyId)
        {
            var loyalty = await _loyaltyProgramRepository.GetByIdAsync(loyaltyId);

            if (loyalty is null)
            {
                throw new PoSException($"Loyalty with id - {loyaltyId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<LoyaltyProgramResponse>(loyalty);
        }

        public async Task<List<LoyaltyProgramResponse>> GetLoyaltysAsync(LoyaltyFilter filter)
        {
            var loyaltyFilter = PredicateBuilder.True<LoyaltyProgram>();
            Func<IQueryable<LoyaltyProgram>, IOrderedQueryable<LoyaltyProgram>>? orderByLoyalty = null;

            if (filter.BusinessId != null)
            {
                loyaltyFilter = loyaltyFilter.And(x => x.BusinessId == filter.BusinessId);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByLoyalty = x => x.OrderByDescending(p => EF.Property<LoyaltyProgram>(p, filter.OrderBy));
                        break;
                    default:
                        orderByLoyalty = x => x.OrderBy(p => EF.Property<LoyaltyProgram>(p, filter.OrderBy));
                        break;
                }
            }

            var loyalties = await _loyaltyProgramRepository.GetAsync(
                loyaltyFilter,
                orderByLoyalty,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return _mapper.Map<List<LoyaltyProgramResponse>>(loyalties);
        }

        public async Task<LoyaltyProgramResponse?> UpdateLoyaltyByIdAsync(Guid loyaltyId, LoyaltyProgramRequest loyaltyUpdateRequest)
        {
            var loyaltyUpdated = _mapper.Map<LoyaltyProgram>(loyaltyUpdateRequest);
            loyaltyUpdated.Id = loyaltyId;

            if (!await _businessRepository.Exists(x => x.Id == loyaltyUpdateRequest.BusinessId))
            {
                throw new PoSException($"Business with id - {loyaltyUpdateRequest.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            loyaltyUpdated = await _loyaltyProgramRepository.UpdateAsync(loyaltyUpdated);

            return _mapper.Map<LoyaltyProgramResponse>(loyaltyUpdated);
        }
    }
}
