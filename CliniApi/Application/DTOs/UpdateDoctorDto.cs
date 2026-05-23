using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class UpdateDoctorDto
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(30)]
        public string? Phone {  get; set; }

        [Required]
        public int SpecialtyId { get; set; }
        public bool IsActive { get; set; }
    }
}
