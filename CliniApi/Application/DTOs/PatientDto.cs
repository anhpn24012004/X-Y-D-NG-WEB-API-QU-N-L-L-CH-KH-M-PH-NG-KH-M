namespace CliniApi.Application.DTOs
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
