﻿namespace PoS.Application.Models.Responses
{
    public class AppointmentResponse
    {
        public Guid? Id { get; set; }

        public Guid? CustomerId { get; set; }

        public Guid? ServiceId { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? BusinessId { get; set; }

        public DateTime? ReservationTime { get; set; }

        public double? Duration { get; set; }
    }
}
