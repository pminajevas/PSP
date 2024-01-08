namespace PoS.Application.Models.Responses
{
    public class ReceiptLineResponse
    {
        public string? ItemName { get; set; }
        public double? UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public double? DiscountAmount { get; set; }
        public double? TotalLineAmount { get; set; }
    }
}
