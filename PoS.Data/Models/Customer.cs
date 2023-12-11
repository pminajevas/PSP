using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Customer
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }

        public Guid? LoyaltyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        public double? Points { get; set; }
    }
}
