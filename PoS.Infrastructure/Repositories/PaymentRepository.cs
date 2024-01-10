using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Core.Entities;
using PoS.Core.Enums;
using PoS.Infrastructure.Context;

namespace PoS.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        IPoSDBContext _context;
        public PaymentRepository(IPoSDBContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<double> GetTotalPaidAmount(Guid orderId)
        {
            return await _context.Payments
                                 .Where(p => p.OrderId == orderId && p.Status == PaymentStatusEnum.Paid)
                                 .SumAsync(p => p.Amount);
        }
    }
}
