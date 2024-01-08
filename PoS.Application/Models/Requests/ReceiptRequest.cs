using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class ReceiptRequest
    {
        [Required]
        public Guid OrderId { get; set; }
    }
}
