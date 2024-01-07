namespace PoS.Services.Filters
{
    public class DiscountFilter : BaseFilter
    {
        public DateTime? ValidUntil { get; set; } = null;
    }
}
