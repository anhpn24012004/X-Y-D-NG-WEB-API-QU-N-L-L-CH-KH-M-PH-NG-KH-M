using System.ComponentModel.DataAnnotations;

namespace CliniApi.Application.DTOs
{
    public class UpdatePatientDto
    {
        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(20)]
        public string? Gender { get; set; }

        [MaxLength(30)]
        public string? Phone {  get; set; }

        [MaxLength(300)]
        public string? Address { get; set; }
    }
}
