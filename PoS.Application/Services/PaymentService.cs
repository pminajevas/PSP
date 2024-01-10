using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Enums;
using PoS.Core.Exceptions;
using System.Net;

namespace PoS.Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly ICouponService _couponService;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IPaymentMethodRepository paymentMethodRepository, ICouponRepository couponRepository, ICouponService couponService)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _couponRepository = couponRepository;
            _couponService = couponService;
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
            ValidatePayment(payment);

            await ValidateOrderExists(payment.OrderId);

            await HandleCouponLogic(payment);

            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(payment.PaymentMethodId);
            if (paymentMethod == null)
            {
                throw new PoSException($"Payment method with id - {payment.PaymentMethodId} does not exist", HttpStatusCode.BadRequest);
            }

            SetPaymentStatus(payment, paymentMethod);

            var insertedPayment = await _paymentRepository.InsertAsync(payment);

            await UpdateOrderStatusIfNecessary(payment);

            return insertedPayment;
        }

        private async Task ValidateOrderExists(Guid orderId)
        {
            if (!await _orderRepository.Exists(x => x.Id == orderId))
            {
                throw new PoSException($"Order with id - {orderId} does not exist", HttpStatusCode.BadRequest);
            }
        }

        private void ValidatePayment(Payment payment)
        {
            if (payment == null)
            {
                throw new PoSException("Payment data is required.", HttpStatusCode.BadRequest);
            }

            if (payment.Amount <= 0)
            {
                throw new PoSException($"Payment amount {payment.Amount} is not valid", HttpStatusCode.BadRequest);
            }

            if (payment.PaymentDate == default)
            {
                throw new PoSException("Payment date is required", HttpStatusCode.BadRequest);
            }
        }

        private async Task HandleCouponLogic(Payment payment)
        {
            if (payment.CouponId != null)
            {
                var coupon = await _couponRepository.GetByIdAsync(payment.CouponId);
                if (coupon == null || coupon.Validity != CouponValidityEnum.True)
                {
                    throw new PoSException($"Coupon with the id {payment.CouponId} does not exist or is no longer valid", HttpStatusCode.BadRequest);
                }

                AdjustPaymentAmountWithCoupon(payment, coupon);
            }
        }

        private void AdjustPaymentAmountWithCoupon(Payment payment, Coupon coupon)
        {
            if (coupon.Amount > payment.Amount)
            {
                coupon.Amount -= payment.Amount;
                payment.Status = PaymentStatusEnum.Paid;
                _couponRepository.UpdateAsync(coupon);
            }
            else
            {
                payment.Amount -= coupon.Amount;
                _couponRepository.DeleteAsync(coupon);
            }
        }

        private void SetPaymentStatus(Payment payment, PaymentMethod paymentMethod)
        {
            if (paymentMethod.MethodName.Equals("cash", StringComparison.OrdinalIgnoreCase))
            {
                payment.Status = PaymentStatusEnum.Paid;
            }
            else
            {
                payment.Status = PaymentStatusEnum.Processing;
                payment.ConfirmationId = GenerateConfirmationId();
            }
        }

        private async Task UpdateOrderStatusIfNecessary(Payment payment)
        {
            if (payment.Status == PaymentStatusEnum.Paid)
            {
                var order = await _orderRepository.GetByIdAsync(payment.OrderId);
                if (order != null)
                {
                    var totalPaid = await _paymentRepository.GetTotalPaidAmount(order.Id);
                    Console.WriteLine(totalPaid);
                    if (totalPaid >= (order.TotalAmount + order.Tip))
                    {
                        order.Status = OrderStatusEnum.Invoiced;
                        order.Tip = totalPaid - order.TotalAmount;
                        await _orderRepository.UpdateAsync(order);
                    }
                }
            }
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
            var payment = await _paymentRepository.GetByIdAsync(paymentId);
            if (payment == null)
            {
                throw new PoSException($"Payment with id - {paymentId} does not exist", HttpStatusCode.NotFound);
            }

            await _paymentRepository.DeleteAsync(paymentId);
            return true;
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

        //Future implementation using the payment operator
        private Guid GenerateConfirmationId()
        {
            return Guid.NewGuid();
        }

        public async Task<Payment?> ConfirmPaymentAsync(Guid confirmationId)
        {
            var payment = await _paymentRepository.GetFirstAsync(x => x.ConfirmationId == confirmationId);
            if (payment == null)
            {
                throw new PoSException("Payment does not exist", HttpStatusCode.BadRequest);
            }

            payment.Status = PaymentStatusEnum.Paid;
            var confirmedPayment = await _paymentRepository.UpdateAsync(payment);

            var order = await _orderRepository.GetFirstAsync(x => x.Id == payment.OrderId);
            var totalPaid = await _paymentRepository.GetTotalPaidAmount(order.Id);
            if (totalPaid >= (order.TotalAmount + order.Tip))
            {
                order.Status = OrderStatusEnum.Invoiced;
                order.Tip = totalPaid - order.TotalAmount;
                await _orderRepository.UpdateAsync(order);
            }

            return confirmedPayment;
        }


    }
}
