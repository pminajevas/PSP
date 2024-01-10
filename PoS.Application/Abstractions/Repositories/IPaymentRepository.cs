using Microsoft.EntityFrameworkCore;
using PoS.Core.Entities;
using PoS.Core.Enums;

namespace PoS.Application.Abstractions.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        public Task<double> GetTotalPaidAmount(Guid orderId);
    }
}
