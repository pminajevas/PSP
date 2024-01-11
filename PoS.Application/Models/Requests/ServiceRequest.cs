using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Requests
{
    public class ServiceRequest
    {
        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        public Guid? DiscountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ServiceName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? ServiceDescription { get; set; }

        [Required]
        public double Duration { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
