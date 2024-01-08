using System.ComponentModel.DataAnnotations;

namespace PoS.Core.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }

        [Required]
        public Guid StaffId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public DateTime ReservationTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public double Duration { get; set; }
    }
}
