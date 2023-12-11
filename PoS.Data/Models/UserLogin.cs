using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class UserLogin
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string LoginName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime? LoginDate { get; set; }

        [Required]
        public DateTime? LogoutDate { get; set; }
    }
}
