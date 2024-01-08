using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
