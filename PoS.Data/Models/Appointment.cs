using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Appointment
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? CustomerId { get; set; }

        [Required]
        public Guid? ServiceId { get; set; }

        public Guid? EmployeeId { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }

        [Required]
        public DateTime? ReservationTime { get; set; }

        public double? Duration { get; set; }
    }
}
