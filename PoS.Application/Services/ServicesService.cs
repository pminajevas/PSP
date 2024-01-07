using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;

namespace PoS.Services.Services
{
    public class ServicesService : IServicesService
    {

        private readonly IServiceRepository _servicesRepository;

        public ServicesService(IServiceRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public async Task<Service?> CreateServiceAsync(Service service)
        {
            return await _servicesRepository.InsertAsync(service);
        }

        public async Task<bool> DeleteServiceAsync(Guid serviceId)
        {
            return await _servicesRepository.DeleteAsync(serviceId);
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _servicesRepository.GetAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            return await _servicesRepository.GetByIdAsync(serviceId);
        }

        public async Task<Service?> UpdateServiceAsync(Guid serviceId, Service serviceUpdate)
        {
            serviceUpdate.Id = serviceId;

            return await _servicesRepository.UpdateAsync(serviceUpdate);
        }
    }
}
