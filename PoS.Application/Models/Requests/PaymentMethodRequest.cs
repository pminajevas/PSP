using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Requests
{
    public class PaymentMethodRequest
    {
        [Required]
        [MaxLength(50)]
        public string MethodName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? MethodDescription { get; set; }
    }
}
