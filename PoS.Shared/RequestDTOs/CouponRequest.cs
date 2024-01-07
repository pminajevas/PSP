using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.RequestDTOs
{
    public class CouponRequest
    {
        [Required]
        public Guid? BusinessId { get; set; }
        [Required]
        public double? Amount { get; set; }
        public enum ValidityEnum
        {
            TrueEnum = 0,
            FalseEnum = 1
        }
        public ValidityEnum? Validity { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
