using PoS.Data;
using PoS.Data.Context;
using PoS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Services.Services
{
    public class ServicesService : IServicesService
    {

        private readonly ServicesRepository _servicesRepository;

        public ServicesService(PoSDbContext dbContext)
        {
            _servicesRepository = new ServicesRepository(dbContext);
        }

        public async Task<Service?> CreateServiceAsync(Service service)
        {
            return await _servicesRepository.CreateServiceAsync(service);
        }

        public async Task<bool> DeleteServiceAsync(Guid serviceId)
        {
            return await _servicesRepository.DeleteServiceAsync(serviceId);
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _servicesRepository.GetServicesAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(Guid serviceId)
        {
            return await _servicesRepository.GetServiceByIdAsync(serviceId);
        }

        public async Task<Service?> UpdateServiceAsync(Guid serviceId, Service serviceUpdate)
        {
            return await _servicesRepository.UpdateServiceAsync(serviceId, serviceUpdate);
        }
    }
}
