using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface ITaxService
    {
        public Task<TaxResponse> AddTaxAsync(TaxRequest createRequest);

        public Task<List<TaxResponse>> GetTaxesAsync(TaxFilter filter);

        public Task<TaxResponse?> GetTaxByIdAsync(Guid taxId);

        public Task<TaxResponse?> UpdateTaxByIdAsync(Guid taxId, TaxRequest updateRequest);

        public Task<bool> DeleteTaxByIdAsync(Guid taxId);
    }
}
