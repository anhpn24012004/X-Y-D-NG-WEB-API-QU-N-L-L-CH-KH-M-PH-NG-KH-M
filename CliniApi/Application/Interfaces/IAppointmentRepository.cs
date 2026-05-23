using CliniApi.Domain.Entities;

namespace CliniApi.Application.Interfaces
{
    public interface IAppointmentRepository
        : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAllDetailsAsync();

        Task<Appointment?> GetDetailByIdAsync(int id);

        Task<bool> HasConflictScheduleAsync(
            int doctorId,
            DateTime appointmentTime
        );
    }
}