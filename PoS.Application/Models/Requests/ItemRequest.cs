using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Requests
{
    public class ItemRequest
    {
        [Required]
        public Guid BusinessId { get; set; }

        public Guid? DiscountId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ItemDescription { get; set; }

        [Required]
        public double Price { get; set; }

    }
}
