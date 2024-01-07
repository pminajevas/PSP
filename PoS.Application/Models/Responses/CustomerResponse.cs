using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Responses
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public Guid? LoyaltyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LoginName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public double? Points { get; set; }
    }
}
