using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Application.Models.Responses
{
    public class ServiceResponse
    {
        public Guid Id { get; set; }

        public string ServiceName { get; set; } = string.Empty;

        public string? ServiceDescription { get; set; }

        public double Duration { get; set; }

        public double Price { get; set; }
    }
}
