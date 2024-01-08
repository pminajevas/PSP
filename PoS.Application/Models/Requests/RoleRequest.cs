using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class RoleRequest
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
