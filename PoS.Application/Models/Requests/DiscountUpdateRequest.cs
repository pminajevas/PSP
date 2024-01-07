using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class DiscountUpdateRequest
    {
        public string? DiscountName { get; set; }

        [Range(0, 1)]
        public double? DiscountPercentage { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
