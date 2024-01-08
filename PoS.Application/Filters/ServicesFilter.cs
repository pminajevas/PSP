namespace PoS.Application.Filters
{
    public class ServicesFilter : BaseFilter
    {
        public Guid? BusinessId { get; set; }
        public Guid? StaffId { get; set; }
        public Guid? DiscountId { get; set; }
    }
}
