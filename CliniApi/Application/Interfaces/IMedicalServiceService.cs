using CliniApi.Application.Common;
using CliniApi.Application.DTOs;

namespace CliniApi.Application.Interfaces
{
    public interface IMedicalServiceService
    {
        Task<Result<IEnumerable<MedicalServiceDto>>> GetAllActiveAsync();
        Task<Result<MedicalServiceDto>> GetByIdAsync(int id);
        Task<Result<MedicalServiceDto>> CreateAsync(CreateMedicalServiceDto dto);
        Task<Result<MedicalServiceDto>> UpdateAsync (int id, UpdateMedicalServiceDto dto);
        Task<Result> DeleteAsync(int id);
    }
}
