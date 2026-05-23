using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class CreateMedicalServiceDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
