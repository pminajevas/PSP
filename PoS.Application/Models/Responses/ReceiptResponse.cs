using PoS.Core.Enums;

namespace PoS.Application.Models.Responses
{
    public class ReceiptResponse
    {
        public DateTime? ReceiptDateTime {  get; set; }
        public string? EmployeeName { get; set; }
        public double? TotalAmountBeforeDiscount { get; set; }
        public double? TotalAmountWithDiscount { get; set; }
        public double? TotalAmountWithDiscountAfterTaxes { get; set; }
        public List<PaymentLineResponse>? PaymentLines { get; set; }
        public List<ReceiptLineResponse>? ReceiptLines { get; set; }
    }
}
