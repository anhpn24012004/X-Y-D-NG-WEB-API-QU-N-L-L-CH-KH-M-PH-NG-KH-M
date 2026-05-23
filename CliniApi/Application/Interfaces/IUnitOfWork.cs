using CliniApi.Domain.Entities;

namespace CliniApi.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Doctor> Doctors { get; }
        IGenericRepository<Patient> Patients { get; }
        IGenericRepository<MedicalService> MedicalServices { get; }
        IGenericRepository<Specialty> Specialties { get; }
        IAppointmentRepository Appointments { get; }
        Task<int> SaveChangesAsync();
    }
}
