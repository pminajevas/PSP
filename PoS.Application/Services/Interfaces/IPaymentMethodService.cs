using PoS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoS.Application.Services.Interfaces
{
    public interface IPaymentMethodService
    {
       
        Task<PaymentMethod?> GetPaymentMethodByIdAsync(Guid paymentMethodId);
        Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
        Task<bool> DeletePaymentMethodAsync(Guid paymentMethodId);
        Task<PaymentMethod?> UpdatePaymentMethodAsync(Guid paymentMethodId, PaymentMethod paymentMethodUpdate);

    }
}
