using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class OrderRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [Required]
        public Guid TaxId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
