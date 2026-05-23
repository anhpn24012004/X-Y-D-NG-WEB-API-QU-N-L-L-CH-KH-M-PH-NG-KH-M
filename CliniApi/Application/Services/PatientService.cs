using AutoMapper;
using CliniApi.Application.Common;
using CliniApi.Application.DTOs;
using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;

namespace CliniApi.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PatientDto>>> GetAllAsync()
        {
            var patients = await _unitOfWork.Patients.GetAllAsync();
            var data = _mapper.Map<IEnumerable<PatientDto>>(patients);

            return Result<IEnumerable<PatientDto>>.Ok(data);
        }

        public async Task<Result<PatientDto>> GetByIdAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                return Result<PatientDto>.Fail(
                    "Patient not found",
                    StatusCodes.Status404NotFound
                );
            }

            var data = _mapper.Map<PatientDto>(patient);

            return Result<PatientDto>.Ok(data);
        }

        public async Task<Result<PatientDto>> CreateAsync(CreatePatientDto dto)
        {
            var patient = _mapper.Map<Patient>(dto);

            await _unitOfWork.Patients.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync();

            var data = _mapper.Map<PatientDto>(patient);

            return Result<PatientDto>.Created(data, "Patient created successfully");
        }

        public async Task<Result<PatientDto>> UpdateAsync(int id, UpdatePatientDto dto)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                return Result<PatientDto>.Fail(
                    "Patient not found",
                    StatusCodes.Status404NotFound
                );
            }

            _mapper.Map(dto, patient);

            _unitOfWork.Patients.Update(patient);
            await _unitOfWork.SaveChangesAsync();

            var data = _mapper.Map<PatientDto>(patient);

            return Result<PatientDto>.Ok(data, "Patient updated successfully");
        }
    }
}
