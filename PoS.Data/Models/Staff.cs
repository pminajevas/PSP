using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{
    public class Staff
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime? HireDate { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }

        public string RolesAsString
        {
            get => string.Join(",", Roles.Select(role => role.ToString()));
            set => Roles = value.Split(',').Select(Guid.Parse).ToList();
        }

        [NotMapped]
        public List<Guid> Roles { get; set; }
    }
}
