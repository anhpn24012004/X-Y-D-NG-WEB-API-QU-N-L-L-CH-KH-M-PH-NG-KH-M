namespace CliniApi.Application.DTOs
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email {  get; set; }
        public string? Phone {  get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public bool IsActive { get; set; }
    }
}
