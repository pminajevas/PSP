namespace PoS.Application.Filters
{
    public class ItemsFilter : BaseFilter
    {
        public Guid? BusinessId { get; set; }
        public Guid? DiscountId { get; set; }
    }
}
