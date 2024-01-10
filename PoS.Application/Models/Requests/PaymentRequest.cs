using PoS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Requests
{
    public class PaymentRequest
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid PaymentMethodId { get; set; }

        [Required]
        public double Amount { get; set; }

    }
}
