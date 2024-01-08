namespace PoS.Application.Models.Responses
{
    public class StaffResponse
    {
        public Guid? Id { get; set; }

        public Guid? BusinessId { get; set; }

        public string? LoginName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public DateTime? HireDate { get; set; }

        public Guid? RoleId { get; set; }
    }
}
