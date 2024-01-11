using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Responses
{
    public class ItemResponse
    {
        public Guid Id { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public string? ItemDescription { get; set; }
        
        public double Price { get; set; }

        public Guid DiscountId { get; set; }
    }
}
