using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class CompleteAppointmentDto
    {
        [MaxLength(500)]
        public string? Note { get; set; }
    }
}