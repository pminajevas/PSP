using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? OrderId { get; set; }

        [Required]
        public Guid? ItemId { get; set; }

        [Required]
        public double? UnitPrice { get; set; }

        [Required]
        public OrderItemTypeEnum? Type { get; set; }

        [Required]
        public double? Quantity { get; set; }

        [Required]
        public double? Subtotal { get; set; }
    }
}
