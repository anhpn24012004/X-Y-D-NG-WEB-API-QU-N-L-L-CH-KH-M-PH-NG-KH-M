using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class CreateAppointmentDto
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [MaxLength(500)]
        public string? Reason { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Appointment must have at least one service.")]
        public List<CreateAppointmentServiceItemDto> Services { get; set; } = new();
    }
}