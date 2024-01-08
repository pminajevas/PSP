using PoS.Core.Enums;

namespace PoS.Application.Filters
{
    public class CouponFilter : BaseFilter
    {
        public Guid? BusinessId { get; set; }
        public CouponValidityEnum? Validity { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
