using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid? CustomerId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [Required]
        public Guid TaxId { get; set; }

        public Guid? DiscountId { get; set; }

        [Required]
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Draft;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public double? Tip { get; set; }
    }
}
