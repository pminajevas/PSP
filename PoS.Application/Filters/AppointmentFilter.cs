namespace PoS.Application.Filters
{
    public class AppointmentFilter : BaseFilter
    {
        public Guid? CustomerId { get; set; }

        public Guid? ServiceId { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? BusinessId { get; set; }

        public DateTime? ReservationTimeFrom { get; set; }

        public DateTime? ReservationTimeUntil { get; set; }
    }
}
