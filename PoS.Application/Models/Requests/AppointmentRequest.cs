using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class AppointmentRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public Guid EmployeeId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public DateTime ReservationTime { get; set; }

        [Required]
        public double Duration { get; set; }
    }
}
