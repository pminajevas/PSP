using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Responses
{
    public class UserLoginResponse
    {

        public Guid? UserId { get; set; }

        [Required]
        public DateTime? LoginDate { get; set; }

        [Required]
        public DateTime? LogoutDate { get; set; }
    }
}
