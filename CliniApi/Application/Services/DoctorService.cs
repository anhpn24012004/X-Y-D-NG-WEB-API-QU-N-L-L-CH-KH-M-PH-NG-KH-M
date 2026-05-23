using AutoMapper;
using CliniApi.Application.Common;
using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;

namespace CliniApi.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DoctorDto>>> GetAllAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync();
            var data = _mapper.Map<IEnumerable<DoctorDto>>(doctors);

            return Result<IEnumerable<DoctorDto>>.Ok(data);
        }

        public async Task<Result<DoctorDto>> GetByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
                return Result<DoctorDto>.Fail("Doctor not found", StatusCodes.Status404NotFound);

            return Result<DoctorDto>.Ok(_mapper.Map<DoctorDto>(doctor));
        }

        public async Task<Result<DoctorDto>> CreateAsync(CreateDoctorDto dto)
        {
            var specialty = await _unitOfWork.Specialties.GetByIdAsync(dto.SpecialtyId);

            if (specialty == null)
                return Result<DoctorDto>.Fail("Specialty not found", StatusCodes.Status404NotFound);

            var doctor = _mapper.Map<Doctor>(dto);

            await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            return Result<DoctorDto>.Created(
                _mapper.Map<DoctorDto>(doctor),
                "Doctor created successfully"
            );
        }

        public async Task<Result<DoctorDto>> UpdateAsync(int id, UpdateDoctorDto dto)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
                return Result<DoctorDto>.Fail("Doctor not found", StatusCodes.Status404NotFound);

            var specialty = await _unitOfWork.Specialties.GetByIdAsync(dto.SpecialtyId);

            if (specialty == null)
                return Result<DoctorDto>.Fail("Specialty not found", StatusCodes.Status404NotFound);

            _mapper.Map(dto, doctor);

            _unitOfWork.Doctors.Update(doctor);
            await _unitOfWork.SaveChangesAsync();

            return Result<DoctorDto>.Ok(
                _mapper.Map<DoctorDto>(doctor),
                "Doctor updated successfully"
            );
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);

            if (doctor == null)
                return Result.Fail("Doctor not found", StatusCodes.Status404NotFound);

            var appointments = await _unitOfWork.Appointments.GetAllAsync();

            var hasScheduledAppointment = appointments.Any(a =>
                a.DoctorId == id && a.Status == "Scheduled"
            );

            if (hasScheduledAppointment)
                return Result.Fail("Cannot delete doctor because doctor has scheduled appointments", StatusCodes.Status409Conflict);

            _unitOfWork.Doctors.Delete(doctor);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok("Doctor deleted successfully");
        }
    }
}
