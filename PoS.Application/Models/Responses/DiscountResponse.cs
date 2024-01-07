namespace PoS.Application.Models.Responses
{
    public class DiscountResponse
    {
        public Guid Id { get; set; }
        public string DiscountName { get; set; } = String.Empty;
        public double DiscountPercentage { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
