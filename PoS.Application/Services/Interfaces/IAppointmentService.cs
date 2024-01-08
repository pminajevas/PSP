using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;

namespace PoS.Application.Services.Interfaces
{
    public interface IAppointmentService
    {
        public Task<AppointmentResponse> AddAppointmentAsync(AppointmentRequest createRequest);

        public Task<List<AppointmentResponse>> GetAppointmentsAsync(AppointmentFilter filter);

        public Task<AppointmentResponse?> GetAppointmentByIdAsync(Guid appointmentId);

        public Task<AppointmentResponse?> UpdateAppointmentByIdAsync(Guid appointmentId, AppointmentRequest updateRequest);

        public Task<bool> DeleteAppointmentByIdAsync(Guid appointmentId);
    }
}
