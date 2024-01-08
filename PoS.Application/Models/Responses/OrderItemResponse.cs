using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Responses
{
    public class OrderItemResponse
    {
        public Guid? Id { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ItemId { get; set; }

        public double? UnitPrice { get; set; }

        public double? UnitPriceDiscount { get; set; }

        public OrderItemTypeEnum? Type { get; set; }

        public double? Quantity { get; set; }

        public double? Subtotal { get; set; }
    }
}
