using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoS.Application.Abstractions.Repositories;
using PoS.Application.Filters;
using PoS.Application.Models.Requests;
using PoS.Application.Models.Responses;
using PoS.Application.Services.Interfaces;
using PoS.Core.Entities;
using PoS.Core.Exceptions;

namespace PoS.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public AppointmentService(
            IAppointmentRepository appointmentRepository,
            ICustomerRepository customerRepository,
            IServiceRepository serviceRepository,
            IStaffRepository staffRepository,
            IBusinessRepository businessRepository,
            IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _serviceRepository = serviceRepository;
            _staffRepository = staffRepository;
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<AppointmentResponse> AddAppointmentAsync(AppointmentRequest createRequest)
        {
            var appointment = _mapper.Map<Appointment>(createRequest);

            var service = await _serviceRepository.GetFirstAsync(x => x.Id == appointment.ServiceId);
            var business = await _businessRepository.GetFirstAsync(x => x.Id == appointment.BusinessId);

            if (!await _customerRepository.Exists(x => x.Id == appointment.CustomerId))
            {
                throw new PoSException($"Customer with id - {appointment.CustomerId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (service is null)
            {
                throw new PoSException($"Service with id - {appointment.ServiceId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (!await _staffRepository.Exists(x => x.Id == appointment.StaffId))
            {
                throw new PoSException($"Staff with id - {appointment.StaffId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (business is null)
            {
                throw new PoSException($"Business with id - {appointment.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            DateTime startTime = appointment.ReservationTime;
            DateTime endTime = appointment.ReservationTime.AddMinutes(service.Duration);

            int requestTimeAsMinutes = startTime.Hour * 60 + startTime.Minute;
            int requestEndTimeAsMinutes = endTime.Hour * 60 + endTime.Minute;

            if (!(requestTimeAsMinutes >= business.WorkingHoursStart && requestEndTimeAsMinutes < business.WorkingHoursEnd))
            {
                throw new PoSException($"Requested time is not during business's working hours", System.Net.HttpStatusCode.BadRequest);
            }

            if (await _appointmentRepository.Exists(x =>
                (x.ReservationTime >= startTime && x.EndTime >= endTime)
                || (x.ReservationTime >= startTime && x.ReservationTime < endTime)
                || (x.EndTime > startTime && x.EndTime <= endTime)
            ))
            {
                throw new PoSException($"Requested time is not free", System.Net.HttpStatusCode.BadRequest);
            }

            appointment.EndTime = endTime;
            appointment.Duration = service.Duration;

            return _mapper.Map<AppointmentResponse>(await _appointmentRepository.InsertAsync(appointment));
        }

        public async Task<List<AppointmentResponse>> GetAppointmentsAsync(AppointmentFilter appointmentFilter)
        {
            var filter = PredicateBuilder.True<Appointment>();
            Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>>? orderBy = null;

            if (appointmentFilter.BusinessId != null)
            {
                filter = filter.And(x => x.BusinessId == appointmentFilter.BusinessId);
            }

            if (appointmentFilter.StaffId != null)
            {
                filter = filter.And(x => x.StaffId == appointmentFilter.StaffId);
            }

            if (appointmentFilter.ServiceId != null)
            {
                filter = filter.And(x => x.ServiceId == appointmentFilter.ServiceId);
            }

            if (appointmentFilter.CustomerId != null)
            {
                filter = filter.And(x => x.CustomerId == appointmentFilter.CustomerId);
            }

            if (appointmentFilter.ReservationTimeFrom != null)
            {
                filter = filter.And(x => x.ReservationTime >=  appointmentFilter.ReservationTimeFrom);
            }

            if (appointmentFilter.ReservationTimeUntil != null)
            {
                filter = filter.And(x => x.EndTime <= appointmentFilter.ReservationTimeUntil);
            }

            if (appointmentFilter.OrderBy != string.Empty)
            {
                switch (appointmentFilter.Sorting)
                {
                    case Sorting.dsc:
                        orderBy = x => x.OrderByDescending(p => EF.Property<Appointment>(p, appointmentFilter.OrderBy));
                        break;
                    default:
                        orderBy = x => x.OrderBy(p => EF.Property<Appointment>(p, appointmentFilter.OrderBy));
                        break;
                }
            }

            var appointments = await _appointmentRepository.GetAsync(
                filter,
                orderBy,
                appointmentFilter.ItemsToSkip(),
                appointmentFilter.PageSize
            );

            return _mapper.Map<List<AppointmentResponse>>(appointments);
        }

        public async Task<AppointmentResponse?> GetAppointmentByIdAsync(Guid appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment is null)
            {
                throw new PoSException($"Appointment with id - {appointmentId} does not exist", System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<AppointmentResponse>(appointment);
        }

        public async Task<AppointmentResponse?> UpdateAppointmentByIdAsync(Guid appointmentId, AppointmentRequest updateRequest)
        {
            var appointment = _mapper.Map<Appointment>(updateRequest);

            var service = await _serviceRepository.GetFirstAsync(x => x.Id == appointment.ServiceId);
            var business = await _businessRepository.GetFirstAsync(x => x.Id == appointment.BusinessId);

            if (!await _customerRepository.Exists(x => x.Id == appointment.CustomerId))
            {
                throw new PoSException($"Customer with id - {appointment.CustomerId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (service is null)
            {
                throw new PoSException($"Service with id - {appointment.ServiceId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (!await _staffRepository.Exists(x => x.Id == appointment.StaffId))
            {
                throw new PoSException($"Staff with id - {appointment.StaffId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            if (business is null)
            {
                throw new PoSException($"Business with id - {appointment.BusinessId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }

            DateTime startTime = appointment.ReservationTime;
            DateTime endTime = appointment.ReservationTime.AddMinutes(service.Duration);

            int requestTimeAsMinutes = startTime.Minute;
            int requestEndTimeAsMinutes = endTime.Minute;

            if (requestTimeAsMinutes < business.WorkingHoursStart
                || requestEndTimeAsMinutes > business.WorkingHoursEnd)
            {
                throw new PoSException($"Requested time is not during business's working hours", System.Net.HttpStatusCode.BadRequest);
            }

            if (await _appointmentRepository.Exists(x =>
                (x.ReservationTime >= startTime && x.EndTime >= endTime)
                || (x.ReservationTime >= startTime && x.ReservationTime < endTime)
                || (x.EndTime > startTime && x.EndTime <= endTime)
                && x.Id != appointmentId
            ))
            {
                throw new PoSException($"Requested time is not free", System.Net.HttpStatusCode.BadRequest);
            }

            appointment.EndTime = endTime;
            appointment.Duration = service.Duration;

            return _mapper.Map<AppointmentResponse>(await _appointmentRepository.InsertAsync(appointment));
        }

        public async Task<bool> DeleteAppointmentByIdAsync(Guid appointmentId)
        {
            if (await _appointmentRepository.DeleteAsync(appointmentId))
            {
                return true;
            }
            else
            {
                throw new PoSException($"Appointment with id - {appointmentId} does not exist", System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
