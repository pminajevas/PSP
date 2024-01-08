using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CustomerFilter customerFilter);

        public Task<CustomerResponse> AddCustomerAsync(CustomerRequest createRequest);

        public Task<CustomerResponse> UpdateCustomerAsync(Guid id, CustomerRequest createRequest);

        public Task<bool> DeleteCustomerAsync(Guid id);

        public Task<CustomerResponse> GetCustomerByIdAsync(Guid id);
    }
}
