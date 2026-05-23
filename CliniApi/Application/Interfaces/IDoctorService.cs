using CliniApi.Application.Common;
using CliniApi.Application.DTOs;

namespace CliniApi.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<Result<IEnumerable<DoctorDto>>> GetAllAsync();
        Task<Result<DoctorDto>> GetByIdAsync(int id);
        Task<Result<DoctorDto>> CreateAsync(CreateDoctorDto dto);
        Task<Result<DoctorDto>> UpdateAsync(int id, UpdateDoctorDto dto);
        Task<Result> DeleteAsync(int id);
    }
}
