namespace PoS.Application.Filters
{
    public class StaffFilter : BaseFilter
    {
        public Guid? BusinessId { get; set; }
        public string? RoleName { get; set; }
    }
}
