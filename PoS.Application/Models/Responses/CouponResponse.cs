using PoS.Core.Enums;

namespace PoS.Application.Models.Responses
{
    public class CouponResponse
    {
        public Guid? Id { get; set; }

        public Guid? BusinessId { get; set; }

        public double? Amount { get; set; }

        public CouponValidityEnum? Validity { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
