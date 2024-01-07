namespace PoS.Application.Filters
{
    public class CustomerFilter : BaseFilter
    {
        public Guid? BusinesseId { get; set; }

        public Guid? LoyaltyId { get; set; }
    }
}
