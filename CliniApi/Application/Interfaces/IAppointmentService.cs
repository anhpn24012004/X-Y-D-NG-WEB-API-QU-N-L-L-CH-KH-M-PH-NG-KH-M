using CliniApi.Application.Common;
using CliniApi.Application.DTOs;

namespace CliniApi.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<Result<IEnumerable<AppointmentDto>>> GetAllAsync();
        Task<Result<AppointmentDto>> GetByIdAsync(int id);
        Task<Result<AppointmentDto>> CreateAsync(CreateAppointmentDto dto);
        Task<Result> CancelAsync(int id, CancelAppointmentDto dto);
        Task<Result> CompleteAsync(int id, CompleteAppointmentDto dto);
    }
}
