using PoS.Application.Filters;
using PoS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoS.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment?> GetPaymentByIdAsync(Guid paymentId);
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<IEnumerable<Payment>> GetPaymentsAsync(PaymentsFilter filter);
        Task<bool> DeletePaymentAsync(Guid paymentId);
        Task<Payment?> UpdatePaymentAsync(Guid paymentId, Payment paymentUpdate);

        Task<Payment?> ConfirmPaymentAsync(Guid confirmationId);
    }
}
