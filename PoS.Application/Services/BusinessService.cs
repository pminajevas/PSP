using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Services.Services
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public BusinessService(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BusinessResponse>> GetAllBusinessesAsync(BusinessesFilter filter)
        {
            var businessFilter = PredicateBuilder.True<Business>();
            Func<IQueryable<Business>, IOrderedQueryable<Business>>? orderByBusiness = null;

            if (filter.Location != null)
            {
                businessFilter = businessFilter.And(x => x.Location == filter.Location);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByBusiness = x => x.OrderByDescending(p => EF.Property<Business>(p, filter.OrderBy));
                        break;
                    default:
                        orderByBusiness = x => x.OrderBy(p => EF.Property<Business>(p, filter.OrderBy));
                        break;
                }
            }

            var businesses = await _businessRepository.GetAsync(
                businessFilter,
                orderByBusiness,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return _mapper.Map<List<BusinessResponse>>(businesses);
        }

        public async Task<BusinessResponse?> GetBusinessByIdAsync(Guid id)
        {
            var business = await _businessRepository.GetByIdAsync(id);

            if (business is null)
            {
                throw new PoSException($"Business with id - {id} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<BusinessResponse>(business);
        }

        public async Task<BusinessResponse> AddBusinessAsync(BusinessRequest businessRequest)
        {
            var business = _mapper.Map<Business>(businessRequest);

            if (await _businessRepository.Exists(x => x.BusinessName == businessRequest.BusinessName && x.Location == businessRequest.Location))
            {
                throw new PoSException($"Business with name - {businessRequest.BusinessName} and location - {businessRequest.Location} already exists", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<BusinessResponse>(await _businessRepository.InsertAsync(business));
        }

        public async Task<BusinessResponse?> UpdateBusinessAsync(BusinessRequest updatedBusiness, Guid businessId)
        {
            var businessToUpdate = _mapper.Map<Business>(updatedBusiness);
            businessToUpdate.Id = businessId;

            var oldBusiness = await _businessRepository.GetFirstAsync(x => x.Id == businessId) ??
                throw new PoSException($"Business with id - {businessId} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldBusiness.BusinessName != businessToUpdate.BusinessName || oldBusiness.Location != businessToUpdate.Location)
            {
                if (await _businessRepository.Exists(x => x.BusinessName == updatedBusiness.BusinessName && x.Location == updatedBusiness.Location))
                {
                    throw new PoSException($"Business with name - {updatedBusiness.BusinessName} and location - {updatedBusiness.Location} already exists", System.Net.HttpStatusCode.BadRequest);
                }
            }

            businessToUpdate =  await _businessRepository.UpdateAsync(businessToUpdate);

            return _mapper.Map<BusinessResponse>(businessToUpdate);
        }

        public async Task<bool> DeleteBusinessAsync(Guid id)
        {
            if (await _businessRepository.DeleteAsync(id))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Business with id - {id} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
