using PoS.Application.Filters;
using PoS.Core.Entities;

namespace PoS.Services.Services
{
    public interface IServicesService
    {
        Task<Service?> GetServiceByIdAsync(Guid serviceId);
        Task<Service> CreateServiceAsync(Service service);
        Task<IEnumerable<Service>> GetServicesAsync(ServicesFilter servicesFilter);
        Task<bool> DeleteServiceAsync(Guid serviceId);
        Task<Service?> UpdateServiceAsync(Guid serviceId, Service serviceUpdate);
    }
}
