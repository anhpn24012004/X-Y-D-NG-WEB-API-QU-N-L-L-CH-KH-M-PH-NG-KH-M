using AutoMapper;
using CliniApi.Application.Common;
using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;

namespace CliniApi.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AppointmentDto>>> GetAllAsync()
        {
            var appointments = await _unitOfWork.Appointments.GetAllDetailsAsync();
            var data = _mapper.Map<IEnumerable<AppointmentDto>>(appointments);

            return Result<IEnumerable<AppointmentDto>>.Ok(data);
        }

        public async Task<Result<AppointmentDto>> GetByIdAsync(int id)
        {
            var appointment = await _unitOfWork.Appointments.GetDetailByIdAsync(id);

            if (appointment == null)
                return Result<AppointmentDto>.Fail("Appointment not found", StatusCodes.Status404NotFound);

            var data = _mapper.Map<AppointmentDto>(appointment);

            return Result<AppointmentDto>.Ok(data);
        }

        public async Task<Result<AppointmentDto>> CreateAsync(CreateAppointmentDto dto)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
            if (patient == null)
                return Result<AppointmentDto>.Fail("Patient not found", StatusCodes.Status404NotFound);

            var doctor = await _unitOfWork.Doctors.GetByIdAsync(dto.DoctorId);
            if (doctor == null || !doctor.IsActive)
                return Result<AppointmentDto>.Fail("Doctor not found or inactive", StatusCodes.Status404NotFound);

            if (dto.Services == null || !dto.Services.Any())
                return Result<AppointmentDto>.Fail("Appointment must have at least one service");

            if (dto.AppointmentTime <= DateTime.Now)
                return Result<AppointmentDto>.Fail("Appointment time cannot be in the past");

            var hasConflict = await _unitOfWork.Appointments.HasConflictScheduleAsync(
                dto.DoctorId,
                dto.AppointmentTime
            );

            if (hasConflict)
                return Result<AppointmentDto>.Fail("Doctor already has a scheduled appointment at this time", StatusCodes.Status409Conflict);

            var appointment = new Appointment
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                AppointmentTime = dto.AppointmentTime,
                Status = "Scheduled",
                Reason = dto.Reason,
                Note = dto.Note,
                CreatedAt = DateTime.Now
            };

            foreach (var item in dto.Services)
            {
                var service = await _unitOfWork.MedicalServices.GetByIdAsync(item.ServiceId);

                if (service == null || !service.IsActive)
                    return Result<AppointmentDto>.Fail($"Service with id {item.ServiceId} not found or inactive", StatusCodes.Status404NotFound);

                if (item.Quantity <= 0)
                    return Result<AppointmentDto>.Fail("Service quantity must be greater than 0");

                appointment.AppointmentServices.Add(new CliniApi.Domain.Entities.AppointmentService
                {
                    ServiceId = item.ServiceId,
                    Quantity = item.Quantity,
                    UnitPrice = service.Price
                });
            }

            await _unitOfWork.Appointments.AddAsync(appointment);
            await _unitOfWork.SaveChangesAsync();

            var createdAppointment = await _unitOfWork.Appointments.GetDetailByIdAsync(appointment.AppointmentId);
            var data = _mapper.Map<AppointmentDto>(createdAppointment);

            return Result<AppointmentDto>.Created(data, "Appointment created successfully");
        }

        public async Task<Result> CancelAsync(int id, CancelAppointmentDto dto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            if (appointment == null)
                return Result.Fail("Appointment not found", StatusCodes.Status404NotFound);

            if (appointment.Status != "Scheduled")
                return Result.Fail("Only scheduled appointments can be cancelled", StatusCodes.Status400BadRequest);

            appointment.Status = "Cancelled";
            appointment.Note = dto.Note;

            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Appointment cancelled successfully");
        }

        public async Task<Result> CompleteAsync(int id, CompleteAppointmentDto dto)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);

            if (appointment == null)
                return Result.Fail("Appointment not found", StatusCodes.Status404NotFound);

            if (appointment.Status != "Scheduled")
                return Result.Fail("Only scheduled appointments can be completed", StatusCodes.Status400BadRequest);

            appointment.Status = "Completed";
            appointment.Note = dto.Note;

            _unitOfWork.Appointments.Update(appointment);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Appointment completed successfully");
        }
    }
}
