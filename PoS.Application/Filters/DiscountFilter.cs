namespace PoS.Application.Filters
{
    public class DiscountFilter : BaseFilter
    {
        public DateTime? ValidUntil { get; set; } = null;
    }
}
