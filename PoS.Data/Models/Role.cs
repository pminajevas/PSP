using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Role
    {
        [Key]
        public Guid? Id { get; set; }

        public enum UserRoleEnum
        {
            AdministratorEnum = 0,
            ManagerEnum = 1,
            StaffEnum = 2,
            WaiterEnum = 3,
            HairdresserEnum = 4,
            ChefEnum = 5
        }

        [Required]
        public UserRoleEnum? UserRole { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
