using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.RequestDTOs
{
    public class DiscountRequest
    {
        [Required]
        public string DiscountName { get; set; } = String.Empty;

        [Required]
        [Range(0, 1)]
        public double DiscountPercentage { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
