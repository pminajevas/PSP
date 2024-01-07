namespace PoS.Application.Models.Requests
{
    public class CustomerRequest
    {

        public Guid BusinessId { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public DateTime? Birthday { get; set; }

        public string? Address { get; set; }

        public Guid? LoyaltyId { get; set; }

        public double? Points { get; set; }

    }
}
