using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.ResponseDTOs
{
    public class DiscountResponse
    {
        public Guid Id { get; set; }
        public string DiscountName { get; set; } = String.Empty;
        public double DiscountPercentage { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
