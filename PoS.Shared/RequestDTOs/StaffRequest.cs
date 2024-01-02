using PoS.Shared.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PoS.Shared.RequestDTOs.UserRequest;

namespace PoS.Shared.RequestDTOs
{
    public class StaffRequest
    {

        public Guid BusinessId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? HireDate { get; set; }

        public string RoleName { get; set; }
    }
}