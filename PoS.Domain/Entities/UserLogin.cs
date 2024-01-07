using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class UserLogin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public DateTime? LoginDate { get; set; }

        [Required]
        public DateTime? LogoutDate { get; set; }
    }
}
