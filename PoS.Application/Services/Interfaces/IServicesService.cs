using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Core.Entities;

namespace PoS.Application.Services.Interfaces
{
    public interface IServicesService
    {
        Task<ServiceResponse?> GetServiceByIdAsync(Guid serviceId);
        Task<ServiceResponse> CreateServiceAsync(ServiceRequest serviceRequest);
        Task<IEnumerable<ServiceResponse>> GetServicesAsync(ServicesFilter servicesFilter);
        Task<bool> DeleteServiceAsync(Guid serviceId);
        Task<ServiceResponse?> UpdateServiceAsync(Guid serviceId, ServiceRequest serviceUpdate);
    }
}
