using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class PaymentMethod
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string MethodName { get; set; }

        [MaxLength(500)]
        public string MethodDescription { get; set; }
    }
}
