using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Service
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        public Guid? DiscountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ServiceName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ServiceDescription { get; set; }

        [Required]
        public double Duration { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
