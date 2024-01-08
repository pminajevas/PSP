using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Responses
{
    public class BusinessResponse
    {
        public Guid? Id { get; set; }

        public string? BusinessName { get; set; }

        public string? Location { get; set; }

        public int? WorkingHoursStart { get; set; }

        public int? WorkingHoursEnd { get; set; }
    }
}
