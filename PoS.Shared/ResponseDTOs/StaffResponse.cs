using PoS.Shared.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Shared.ResponseDTOs
{
    public class StaffResponse
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid? BusinessId { get; set; }

        public string LoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? HireDate { get; set; }

        public string? RoleName { get; set; }

        public string? JwtToken { get; set; }

    }
}
