using PoS.Core.Enums;

namespace PoS.Application.Models.Responses
{
    public class PaymentLineResponse
    {
        public string? PaymentMethod { get; set; }
        public DateTime? PaymentDateTime { get; set; }
        public PaymentStatusEnum? PaymentStatus { get; set; }
        public double? PaymentAmount { get; set; }
    }
}
