using CliniApi.Application.Common;
using CliniApi.Application.DTOs;

namespace CliniApi.Application.Interfaces
{
    public interface IPatientService
    {
        Task<Result<IEnumerable<PatientDto>>> GetAllAsync();
        Task<Result<PatientDto>> GetByIdAsync(int id);
        Task<Result<PatientDto>> CreateAsync(CreateDoctorDto dto);
        Task<Result<PatientDto>> UpdateAsync(int id, UpdateDoctorDto dto);
    }
}
