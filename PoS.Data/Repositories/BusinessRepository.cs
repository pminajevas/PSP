using Microsoft.EntityFrameworkCore;
using PoS.Data.Context;
using PoS.Data.Mapper;
using PoS.Shared.RequestDTOs;
using PoS.Shared.ResponseDTOs;
using PoS.Shared.Utilities;
using System.Linq.Dynamic.Core;

namespace PoS.Data.Repositories
{
    public class BusinessRepository
    {
        private readonly PoSDbContext _dbContext;

        public BusinessRepository(PoSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BusinessResponse>> GetAllBusinessesAsync(Filter filter)
        {
            IQueryable<Business> query = _dbContext.Businesses;

            foreach (var filterName in filter.Parameters.Keys)
            {
                if (filterName == "Location")
                {
                    query = query.Where(b => EF.Functions.Like(b.Location, $"%{(string)filter.Parameters[filterName]}%"));
                }
            }

            if (filter.Contains("Sorting") && filter.Parameters["Sorting"] is string sorting)
            {
                if(!new[] { "asc", "desc", "ascending", "descending"}.Contains(sorting))
                {
                    sorting = "asc";
                }
                if (filter.Contains("OrderBy") && filter.Parameters["OrderBy"] is string fieldName)
                {
                    var propertyMap = new Dictionary<string, string>
                    {
                        { "location", "Location" },
                        { "businessname", "BusinessName" },
                    };

                    if (propertyMap.TryGetValue(fieldName.ToLower(), out var propertyName))
                    {
                        var orderExpression = $"{propertyName} {sorting.ToLower()}";
                        query = query.OrderBy(orderExpression);
                    }
                }
            }

            if (filter.Contains("PageIndex") && filter.Parameters["PageIndex"] is int pageIndex)
            {
                query = query.Skip(pageIndex);
            }

            if (filter.Contains("PageSize") && filter.Parameters["PageSize"] is int pageSize)
            {
                query = query.Take(pageSize);
            }

            var businessList = await query.ToListAsync();
            return Mapping.Mapper.Map<List<Business>, List<BusinessResponse>>(businessList);
        }


        public async Task<BusinessResponse?> GetBusinessByIdAsync(Guid id)
        {
            var business =  await _dbContext.Businesses.FindAsync(id);
            return Mapping.Mapper.Map<BusinessResponse>(business);

        }

        public async Task<BusinessResponse> AddBusinessAsync(BusinessRequest businessDTO)
        {
            var business = Mapping.Mapper.Map<Business>(businessDTO);
            _dbContext.Businesses.Add(business);
            await _dbContext.SaveChangesAsync();
            return Mapping.Mapper.Map<BusinessResponse>(business);
        }

        public async Task<BusinessResponse?> UpdateBusinessAsync(BusinessRequest updatedbusinessDTO, Guid businessId)
        {
            var updatedbusiness = Mapping.Mapper.Map<Business>(updatedbusinessDTO);
            updatedbusiness.Id = businessId;
            var existingBusiness = await _dbContext.Businesses.FindAsync(updatedbusiness.Id);
            if (existingBusiness != null)
            {
                _dbContext.Entry(existingBusiness).CurrentValues.SetValues(updatedbusiness);
                await _dbContext.SaveChangesAsync();
                return Mapping.Mapper.Map<BusinessResponse>(existingBusiness);
            }
            return null;

        }

        public async Task<bool> DeleteBusinessAsync(Guid id)
        {
            var business = await _dbContext.Businesses.FindAsync(id);
            if (business != null)
            {
                _dbContext.Businesses.Remove(business);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
