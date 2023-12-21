using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
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

        public enum TypeEnum
        {
            ItemEnum = 0,
            ServiceEnum = 1,
            AppointmentEnum = 2
        }

        [Required]
        public TypeEnum? Type { get; set; }

        [Required]
        public double? Quantity { get; set; }

        [Required]
        public double? Subtotal { get; set; }
    }
}
