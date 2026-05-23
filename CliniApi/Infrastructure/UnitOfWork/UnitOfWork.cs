using CliniApi.Application.Interfaces;
using CliniApi.Domain.Entities;
using CliniApi.Infrastructure.Data;
using CliniApi.Infrastructure.Repositories;

namespace CliniApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicDbContext _context;
        public IGenericRepository<Doctor> Doctors { get; }

        public IGenericRepository<Patient> Patients { get; }

        public IGenericRepository<MedicalService> MedicalServices { get; }
        public IGenericRepository<Specialty> Specialties { get; }

        public IAppointmentRepository Appointments { get; }
        public UnitOfWork(ClinicDbContext context)
        {
            _context = context;

            Doctors = new GenericRepository<Doctor>(context);
            Patients = new GenericRepository<Patient>(context);
            MedicalServices = new GenericRepository<MedicalService>(context);
            Specialties = new GenericRepository<Specialty>(context);
            Appointments = new AppointmentRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
