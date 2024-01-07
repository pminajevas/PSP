using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;

namespace PoS.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync(CustomerFilter customerFilter)
        {
            var filter = PredicateBuilder.True<Customer>();
            Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null;

            if (customerFilter.BusinesseId != null)
            {
                filter = filter.And(x => x.BusinessId == customerFilter.BusinesseId);
            }

            if (customerFilter.LoyaltyId != null)
            {
                filter = filter.And(x => x.LoyaltyId == customerFilter.LoyaltyId);
            }

            if (customerFilter.OrderBy != string.Empty)
            {
                switch (customerFilter.Sorting)
                {
                    case Sorting.dsc:
                        orderBy = x => x.OrderByDescending(p => EF.Property<Discount>(p, customerFilter.OrderBy));
                        break;
                    default:
                        orderBy = x => x.OrderBy(p => EF.Property<Discount>(p, customerFilter.OrderBy));
                        break;
                }
            }

            var customers = await _customerRepository.GetAsync(
                filter,
                orderBy,
                customerFilter.ItemsToSkip(),
                customerFilter.PageSize
            );

            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> AddCustomerAsync(CustomerRequest createRequest)
        {
            var customer = _mapper.Map<Customer>(createRequest);

            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

            customer = await _customerRepository.InsertAsync(customer);

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse?> UpdateCustomerAsync(Guid id, CustomerRequest createRequest)
        {
            var customerUpdated = _mapper.Map<Customer>(createRequest);
            customerUpdated.Id = id;

            customerUpdated = await _customerRepository.UpdateAsync(customerUpdated);

            if (customerUpdated is not null)
            {
                return _mapper.Map<CustomerResponse>(customerUpdated);
            }

            return null;
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            return await _customerRepository.DeleteAsync(id);
        }

        public async Task<CustomerResponse> GetCustomerByIdAsync(Guid id)
        {
            return _mapper.Map<CustomerResponse>(await _customerRepository.GetByIdAsync(id));
        }
    }
}
