using PoS.Core.Enums;

namespace PoS.Application.Models.Responses
{
    public class OrderResponse
    {
        public Guid? Id { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? BusinessId { get; set; }

        public Guid? StaffId { get; set; }

        public Guid? TaxId { get; set; }

        public OrderStatusEnum? Status { get; set; }

        public DateTime? Date { get; set; }

        public double? TotalAmount { get; set; }

        public double? Tip { get; set; }
    }
}
