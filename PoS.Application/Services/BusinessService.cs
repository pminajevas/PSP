using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;

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
            return _mapper.Map<BusinessResponse>(await _businessRepository.GetByIdAsync(id));
        }

        public async Task<BusinessResponse> AddBusinessAsync(BusinessRequest businessRequest)
        {
            var business = _mapper.Map<Business>(businessRequest);

            return _mapper.Map<BusinessResponse>(await _businessRepository.InsertAsync(business));
        }

        public async Task<BusinessResponse?> UpdateBusinessAsync(BusinessRequest updatedBusiness, Guid businessId)
        {
            var businessToUpdate = _mapper.Map<Business>(updatedBusiness);
            businessToUpdate.Id = businessId;

            businessToUpdate =  await _businessRepository.UpdateAsync(businessToUpdate);

            return _mapper.Map<BusinessResponse>(businessToUpdate);
        }

        public async Task<bool> DeleteBusinessAsync(Guid id)
        {
            return await _businessRepository.DeleteAsync(id);
        }
    }
}
