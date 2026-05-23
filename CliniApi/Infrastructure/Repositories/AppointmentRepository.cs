using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;
using CliniApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CliniApi.Infrastructure.Repositories
{
    public class AppointmentRepository
        : GenericRepository<Appointment>,
        IAppointmentRepository
    {
        public AppointmentRepository(ClinicDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAllDetailsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Include(a => a.AppointmentServices)
                    .ThenInclude(s => s.MedicalService)
                .ToListAsync();
        }

        public async Task<Appointment?> GetDetailByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                    .ThenInclude(d => d.Specialty)
                .Include(a => a.AppointmentServices)
                    .ThenInclude(s => s.MedicalService)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<bool> HasConflictScheduleAsync(int doctorId, DateTime appointmentTime)
        {
            return await _context.Appointments.AnyAsync(a =>
                a.DoctorId == doctorId &&
                a.Status == "Scheduled" &&
                a.AppointmentTime == appointmentTime
            );
        }
    }
}
