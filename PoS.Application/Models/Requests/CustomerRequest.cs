using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class CustomerRequest
    {
        [Required]
        public Guid BusinessId { get; set; }

        public Guid? LoyaltyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public DateTime? Birthday { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }
    }
}
