using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Services.Services
{
    public class ServicesService : IServicesService
    {

        private readonly IServiceRepository _servicesRepository;

        public ServicesService(IServiceRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public async Task<Service> CreateServiceAsync(Service service)
        {
            if (await _servicesRepository.Exists(x => x.ServiceName == service.ServiceName
                && x.BusinessId == service.BusinessId
                && x.StaffId == service.StaffId))
            {
                throw new PoSException($"Service with name - {service.ServiceName}, business id - {service.BusinessId} and staff id - {service.StaffId} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            return await _servicesRepository.InsertAsync(service);
        }

        public async Task<bool> DeleteServiceAsync(Guid serviceId)
        {
            if (await _servicesRepository.DeleteAsync(serviceId))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Service with id - {serviceId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IEnumerable<Service>> GetServicesAsync(ServicesFilter filter)
        {
            var servicesFilter = PredicateBuilder.True<Service>();
            Func<IQueryable<Service>, IOrderedQueryable<Service>>? orderByServices = null;

            if (filter.BusinessId != null)
            {
                servicesFilter = servicesFilter.And(x => x.BusinessId == filter.BusinessId);
            }

            if (filter.StaffId != null)
            {
                servicesFilter = servicesFilter.And(x => x.StaffId == filter.StaffId);
            }

            if (filter.DiscountId != null)
            {
                servicesFilter = servicesFilter.And(x => x.DiscountId == filter.DiscountId);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByServices = x => x.OrderByDescending(p => EF.Property<Service>(p, filter.OrderBy));
                        break;
                    default:
                        orderByServices = x => x.OrderBy(p => EF.Property<Service>(p, filter.OrderBy));
                        break;
                }
            }

            var businesses = await _servicesRepository.GetAsync(
                servicesFilter,
                orderByServices,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return businesses;
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            var service = await _servicesRepository.GetByIdAsync(serviceId);
            
            if (service is null)
            {
                throw new PoSException($"Service with id - {serviceId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return service;
        }

        public async Task<Service?> UpdateServiceAsync(Guid serviceId, Service serviceUpdate)
        {
            serviceUpdate.Id = serviceId;

            if (await _servicesRepository.Exists(x => x.ServiceName == serviceUpdate.ServiceName
                && x.BusinessId == serviceUpdate.BusinessId
                && x.StaffId == serviceUpdate.StaffId))
            {
                throw new PoSException($"Service with name - {serviceUpdate.ServiceName}, business id - {serviceUpdate.BusinessId} and staff id - {serviceUpdate.StaffId} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            serviceUpdate = await _servicesRepository.UpdateAsync(serviceUpdate);

            return serviceUpdate;
        }
    }
}
