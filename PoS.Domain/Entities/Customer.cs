using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        public Guid? LoyaltyId { get; set; }

        public Guid RoleId {  get; set; }

        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public double? Points { get; set; }
    }
}
