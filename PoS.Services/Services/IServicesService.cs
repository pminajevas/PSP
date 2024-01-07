using PoS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
