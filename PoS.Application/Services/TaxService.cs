using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Application.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;

        public TaxService(ITaxRepository taxRepository, IMapper mapper)
        {
            _taxRepository = taxRepository;
            _mapper = mapper;
        }

        public async Task<TaxResponse> AddTaxAsync(TaxRequest createRequest)
        {
            var tax = _mapper.Map<Tax>(createRequest);

            if (await _taxRepository.Exists(x => x.TaxName == createRequest.TaxName))
            {
                throw new PoSException($"Tax with name - {createRequest.TaxName} already exists", System.Net.HttpStatusCode.BadRequest);
            }

            return _mapper.Map<TaxResponse>(await _taxRepository.InsertAsync(tax));
        }

        public async Task<bool> DeleteTaxByIdAsync(Guid taxId)
        {
            if (await _taxRepository.DeleteAsync(taxId))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Tax with id - {taxId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<TaxResponse?> GetTaxByIdAsync(Guid taxId)
        {
            var tax = await _taxRepository.GetByIdAsync(taxId);

            if (tax is null)
            {
                throw new PoSException($"Tax with id - {taxId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<TaxResponse>(tax);
        }

        public async Task<List<TaxResponse>> GetTaxesAsync(TaxFilter filter)
        {
            var taxFilter = PredicateBuilder.True<Tax>();
            Func<IQueryable<Tax>, IOrderedQueryable<Tax>>? orderByTaxes = null;

            if (filter.Category != null)
            {
                taxFilter = taxFilter.And(x => x.Category == filter.Category);
            }

            if (filter.ValidFrom != null)
            {
                taxFilter = taxFilter.And(x => x.ValidFrom <= filter.ValidFrom);
            }

            if (filter.ValidUntil != null)
            {
                taxFilter = taxFilter.And(x => x.ValidUntil >= filter.ValidUntil);
            }

            if (filter.OrderBy != string.Empty)
            {
                switch (filter.Sorting)
                {
                    case Sorting.dsc:
                        orderByTaxes = x => x.OrderByDescending(p => EF.Property<Discount>(p, filter.OrderBy));
                        break;
                    default:
                        orderByTaxes = x => x.OrderBy(p => EF.Property<Discount>(p, filter.OrderBy));
                        break;
                }
            }

            var taxes = await _taxRepository.GetAsync(
                taxFilter,
                orderByTaxes,
                filter.ItemsToSkip(),
                filter.PageSize
            );

            return _mapper.Map<List<TaxResponse>>(taxes);
        }

        public async Task<TaxResponse?> UpdateTaxByIdAsync(Guid taxId, TaxRequest updateRequest)
        {
            var taxUpdated = _mapper.Map<Tax>(updateRequest);
            taxUpdated.Id = taxId;

            var oldTax = await _taxRepository.GetFirstAsync(x => x.Id == taxId) ??
                throw new PoSException($"Tax with id - {taxId} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldTax.TaxName != taxUpdated.TaxName)
            {
                if (await _taxRepository.Exists(x => x.TaxName == updateRequest.TaxName))
                {
                    throw new PoSException($"Tax with name - {updateRequest.TaxName} already exists", System.Net.HttpStatusCode.BadRequest);
                }
            }

            taxUpdated = await _taxRepository.UpdateAsync(taxUpdated);

            return _mapper.Map<TaxResponse>(taxUpdated);
        }
    }
}
