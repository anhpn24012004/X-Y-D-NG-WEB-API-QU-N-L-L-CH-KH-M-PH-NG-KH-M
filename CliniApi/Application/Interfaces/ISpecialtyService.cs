using CliniApi.Application.Common;
using CliniApi.Application.DTOs;

namespace CliniApi.Application.Interfaces
{
    public interface ISpecialtyService
    {
        Task<Result<IEnumerable<SpecialtyDto>>> GetAllAsync();
        Task<Result<SpecialtyDto>> GetByIdAsync(int id);
    }
}
