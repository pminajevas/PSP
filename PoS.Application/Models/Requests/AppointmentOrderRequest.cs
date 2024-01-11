using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Requests
{
    public class AppointmentOrderRequest
    {
        [Required]
        public Guid AppointmentId { get; set; }

        [Required]
        public Guid TaxId { get; set; }

    }
}
