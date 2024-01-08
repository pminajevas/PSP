using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class StaffRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public DateTime? HireDate { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
