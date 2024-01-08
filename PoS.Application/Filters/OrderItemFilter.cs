using PoS.Core.Enums;

namespace PoS.Application.Filters
{
    public class OrderItemFilter : BaseFilter
    {
        public Guid? OrderId { get; set; }

        public OrderItemTypeEnum? Type { get; set; }
    }
}
