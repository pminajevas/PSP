using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class AppointmentRequest
    {
        public Guid? CustomerId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public DateTime ReservationTime { get; set; }
    }
}
