using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class OrderItemRequest
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ItemId { get; set; }

        [Required]
        public double Quantity { get; set; }
    }
}
