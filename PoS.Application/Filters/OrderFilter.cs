using PoS.Core.Enums;

namespace PoS.Application.Filters
{
    public class OrderFilter : BaseFilter
    {
        public Guid? CustomerId { get; set; }

        public Guid? BusinessId { get; set; }

        public Guid? StaffId { get; set; }

        public Guid? TaxId { get; set; }

        public OrderStatusEnum? Status { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
