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
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }
        public async Task<PaymentMethod?> GetPaymentMethodByIdAsync(Guid paymentMethodId)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(paymentMethodId);

            if (paymentMethod is null)
            {
                throw new PoSException($"Payment method with id {paymentMethodId} does not exist",System.Net.HttpStatusCode.NotFound);
            }

            return paymentMethod;
        }
        public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            if (await _paymentMethodRepository.Exists(x => x.MethodName == paymentMethod.MethodName))
            {
                throw new PoSException($"Payment method with name - {paymentMethod.MethodName} already exists",
                    System.Net.HttpStatusCode.BadRequest);
            }

            return await _paymentMethodRepository.InsertAsync(paymentMethod);
        }
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            return await _paymentMethodRepository.GetAsync();
        }
        public async Task<bool> DeletePaymentMethodAsync(Guid paymentMethodId)
        {
            if (await _paymentMethodRepository.DeleteAsync(paymentMethodId))
            {
                return true;
            }
            {
                throw new PoSException($"Payment method with id {paymentMethodId} does not exist", System.Net.HttpStatusCode.NotFound);
            }
        }
        public async Task<PaymentMethod?> UpdatePaymentMethodAsync(Guid paymentMethodId, PaymentMethod paymentMethodUpdate)
        {
            var oldPaymentMethod = await _paymentMethodRepository.GetFirstAsync(x => x.Id == paymentMethodId) ??
                throw new PoSException($"Payment method with id {paymentMethodId} does not exist and can not be updated", System.Net.HttpStatusCode.BadRequest);

            if (oldPaymentMethod.MethodName != paymentMethodUpdate.MethodName || oldPaymentMethod.MethodDescription != paymentMethodUpdate.MethodDescription)
            {
                if (await _paymentMethodRepository.Exists(x => x.MethodName == paymentMethodUpdate.MethodName && x.MethodDescription == paymentMethodUpdate.MethodDescription))
                {
                    throw new PoSException($"Payment method with name {paymentMethodUpdate.MethodName} and description: {paymentMethodUpdate.MethodDescription} already exists",
                        System.Net.HttpStatusCode.BadRequest);
                }
            }

            return await _paymentMethodRepository.UpdateAsync(paymentMethodUpdate);
        }

    }
}
