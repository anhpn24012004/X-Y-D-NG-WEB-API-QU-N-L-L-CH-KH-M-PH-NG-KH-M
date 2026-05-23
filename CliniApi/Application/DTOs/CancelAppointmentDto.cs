using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class CancelAppointmentDto
    {
        [MaxLength(500)]
        public string? Note { get; set; }
    }
}