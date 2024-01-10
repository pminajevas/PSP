using PoS.Core.Enums;
using System.Text.Json.Serialization;

namespace PoS.Application.Filters
{
    public class PaymentsFilter : BaseFilter
    {
        public Guid? OrderId { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public PaymentStatusEnum? Status { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}
