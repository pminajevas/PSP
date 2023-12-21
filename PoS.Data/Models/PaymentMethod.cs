using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{
    public class PaymentMethod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string MethodName { get; set; }

        [MaxLength(500)]
        public string MethodDescription { get; set; }
    }
}
