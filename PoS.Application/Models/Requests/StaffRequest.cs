namespace PoS.Application.Models.Requests
{
    public class StaffRequest
    {

        public Guid BusinessId { get; set; }

        public Guid RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime? HireDate { get; set; }
    }
}
