using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;
using CliniApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniApi.Infrastructure.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ClinicDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Doctor>> GetAllWithSpecialtyAsync()
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .ToListAsync();
        }

        public async Task<Doctor?> GetByIdWithSpecialtyAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(d => d.DoctorId == id);
        }
    }
}
