using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class CreateAppointmentServiceItemDto
    {
        [Required]
        public int ServiceId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; } = 1;
    }
}
