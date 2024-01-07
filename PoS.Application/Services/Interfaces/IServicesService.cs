using PoS.Core.Entities;

namespace PoS.Services.Services
{
    public interface IServicesService
    {
        Task<Service?> GetServiceByIdAsync(Guid serviceId);
        Task<Service?> CreateServiceAsync(Service service);
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<bool> DeleteServiceAsync(Guid serviceId);
        Task<Service?> UpdateServiceAsync(Guid serviceId, Service serviceUpdate);
    }
}
