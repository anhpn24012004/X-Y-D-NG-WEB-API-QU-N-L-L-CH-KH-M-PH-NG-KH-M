using CliniApi.Domain.Entities;

namespace CliniApi.Application.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<IEnumerable<Doctor>> GetAllWithSpecialtyAsync();

        Task<Doctor?> GetByIdWithSpecialtyAsync(int id);
    }
}
