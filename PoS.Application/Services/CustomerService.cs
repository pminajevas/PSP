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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IBusinessRepository _businessRepository;
        private readonly ILoyaltyProgramRepository _loyaltyProgramRepository;
        private readonly IRoleRepository _roleRepository;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IBusinessRepository businessRepository,
            ILoyaltyProgramRepository loyaltyProgramRepository,
            IRoleRepository roleRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _businessRepository = businessRepository;
            _loyaltyProgramRepository = loyaltyProgramRepository;
            _roleRepository = roleRepository;
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

            var role = await _roleRepository.GetFirstAsync(x => x.RoleName == "Customer");

            if (role == null)
            {
                throw new PoSException($"Internal error. Customer role not created", System.Net.HttpStatusCode.BadRequest);
            }

            customer.RoleId = role.Id;

            if (!await _businessRepository.Exists(x => x.Id == customer.BusinessId))
            {
                throw new PoSException($"Business with id - {customer.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (customer.LoyaltyId != null)
            {
                if (!await _loyaltyProgramRepository.Exists(x => x.Id == customer.LoyaltyId))
                {
                    throw new PoSException($"Loyalty with id - {customer.LoyaltyId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }

            if (await _customerRepository.Exists(x => x.LoginName == customer.LoginName
                && x.BusinessId == customer.BusinessId))
            {
                throw new PoSException($"Customer with login name - {customer.LoginName} and business id - {customer.BusinessId} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);

            customer = await _customerRepository.InsertAsync(customer);

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> UpdateCustomerAsync(Guid id, CustomerRequest createRequest)
        {
            var customerUpdated = _mapper.Map<Customer>(createRequest);
            customerUpdated.Id = id;

            var role = await _roleRepository.GetFirstAsync(x => x.RoleName == "Customer");

            if (role == null)
            {
                throw new PoSException($"Internal error. Customer role not created", System.Net.HttpStatusCode.BadRequest);
            }

            customerUpdated.RoleId = role.Id;

            if (!await _businessRepository.Exists(x => x.Id == customerUpdated.BusinessId))
            {
                throw new PoSException($"Business with id - {customerUpdated.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (customerUpdated.LoyaltyId != null)
            {
                if (!await _loyaltyProgramRepository.Exists(x => x.Id == customerUpdated.LoyaltyId))
                {
                    throw new PoSException($"Loyalty with id - {customerUpdated.LoyaltyId} does not exist", System.Net.HttpStatusCode.BadRequest);
                }
            }

            var oldCustomer = await _customerRepository.GetFirstAsync(x => x.Id == id) ??
                throw new PoSException($"Customer with id - {id} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldCustomer.LoginName != customerUpdated.LoginName || oldCustomer.BusinessId != customerUpdated.BusinessId)
            {
                if (await _customerRepository.Exists(x => x.LoginName == customerUpdated.LoginName
                    && x.BusinessId == customerUpdated.BusinessId))
                {
                    throw new PoSException($"Customer with login name - {customerUpdated.LoginName} and business id - {customerUpdated.BusinessId} already exists",
                        System.Net.HttpStatusCode.BadRequest);
                }
            }

            customerUpdated.Password = BCrypt.Net.BCrypt.HashPassword(customerUpdated.Password);

            customerUpdated = await _customerRepository.UpdateAsync(customerUpdated);

            return _mapper.Map<CustomerResponse>(customerUpdated);
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            if (await _customerRepository.DeleteAsync(id))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Customer with id - {id} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<CustomerResponse> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                throw new PoSException($"Customer with id - {id} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}
