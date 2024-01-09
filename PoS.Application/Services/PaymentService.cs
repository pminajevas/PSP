using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Enums;
using PoS.Core.Exceptions;

namespace PoS.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<Payment?> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new PoSException($"Payment with id - {paymentId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return payment;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            if (payment == null)
            {
                throw new PoSException("Payment data is required.", System.Net.HttpStatusCode.BadRequest);
            }

            if (!await _orderRepository.Exists(x => x.Id == payment.OrderId))
            {
                throw new PoSException($"Order with id - {payment.OrderId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (!await _paymentMethodRepository.Exists(x => x.Id == payment.PaymentMethodId))
            {
                throw new PoSException($"Payment method with id - {payment.PaymentMethodId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (payment.Amount <= 0)
            {
                throw new PoSException($"Payment amount {payment.Amount} is not valid", System.Net.HttpStatusCode.BadRequest);
            }

            if (payment.PaymentDate == default(DateTime)) 
            { 
                throw new PoSException("Payment date is required", System.Net.HttpStatusCode.BadRequest);
            }

            if (!Enum.IsDefined(typeof(PaymentStatusEnum), payment.Status))
            {
                throw new PoSException($"Invalid payment status {payment.Status}", System.Net.HttpStatusCode.BadRequest);
            }

            return await _paymentRepository.InsertAsync(payment);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync(PaymentsFilter filter)
        {
            var paymentFilter = PredicateBuilder.True<Payment>();
            Func<IQueryable<Payment>, IOrderedQueryable<Payment>>? orderByPayments = null;

            if (filter.OrderId != null)
            {
                paymentFilter = paymentFilter.And(x => x.OrderId == filter.OrderId);
            }

            if (filter.PaymentMethodId != null)
            {
                paymentFilter = paymentFilter.And(x => x.PaymentMethodId == filter.PaymentMethodId);
            }

            if (filter.Status != null)
            {
                paymentFilter = paymentFilter.And(x => x.Status == filter.Status.Value);
            }

            if (filter.fromDate > filter.toDate)
            {
                throw new PoSException("Invalid date range.", System.Net.HttpStatusCode.BadRequest);
            }

            if (filter.fromDate != null)
            {
                paymentFilter = paymentFilter.And(x => x.PaymentDate >= filter.fromDate.Value);
            }

            if (filter.toDate != null)
            {
                paymentFilter = paymentFilter.And(x => x.PaymentDate <= filter.toDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                switch (filter.Sorting)
                {
                    case Sorting.asc:
                        orderByPayments = x => x.OrderBy(p => EF.Property<object>(p, filter.OrderBy));
                        break;
                    case Sorting.dsc:
                        orderByPayments = x => x.OrderByDescending(p => EF.Property<object>(p, filter.OrderBy));
                        break;
                    default:
                        break; 
                }
            }

            int skip = filter.ItemsToSkip();
            int take = filter.PageSize;

            return await _paymentRepository.GetAsync(paymentFilter, orderByPayments, skip, take);
        }

        public async Task<bool> DeletePaymentAsync(Guid paymentId)
        {
            if (await _paymentRepository.DeleteAsync(paymentId))
            {
                return true;
            }
            {
                throw new PoSException($"Payment with id - {paymentId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<Payment?> UpdatePaymentAsync(Guid paymentId, Payment paymentUpdate)
        {
            if (paymentUpdate == null)
            {
                throw new PoSException("Payment data is required.", System.Net.HttpStatusCode.BadRequest);
            }

            if (!Enum.IsDefined(typeof(PaymentStatusEnum), paymentUpdate.Status))
            {
                throw new PoSException($"Invalid payment status {paymentUpdate.Status}", System.Net.HttpStatusCode.BadRequest);
            }

            paymentUpdate.Id = paymentId;

            var oldPayment = await _paymentRepository.GetFirstAsync(x => x.Id == paymentId) ??
                throw new PoSException($"Payment with id - {paymentId} does not exist and can not be updated",
                    System.Net.HttpStatusCode.BadRequest);

            if (oldPayment.Status != paymentUpdate.Status || paymentUpdate.PaymentMethodId != oldPayment.PaymentMethodId)
            {
                if (await _paymentRepository.Exists(x => x.Status == paymentUpdate.Status && oldPayment.PaymentMethodId == paymentUpdate.PaymentMethodId))
                {
                    throw new PoSException($"Payment with status - {paymentUpdate.Status} and method id - {paymentUpdate.PaymentMethodId} already exists",
                        System.Net.HttpStatusCode.BadRequest);
                }
            }

            return await _paymentRepository.UpdateAsync(paymentUpdate);
        }
    }
}
