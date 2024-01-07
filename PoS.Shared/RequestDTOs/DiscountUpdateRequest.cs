using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.RequestDTOs
{
    public class DiscountUpdateRequest
    {
        public string? DiscountName { get; set; }

        [Range(0, 1)]
        public double? DiscountPercentage { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
