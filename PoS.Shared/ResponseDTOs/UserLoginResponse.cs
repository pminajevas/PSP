
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PoS.Shared.ResponseDTOs
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
