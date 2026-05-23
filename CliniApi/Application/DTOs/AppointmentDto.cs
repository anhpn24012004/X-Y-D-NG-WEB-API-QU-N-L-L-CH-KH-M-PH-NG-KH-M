namespace CliniApi.Application.DTOs
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string SpecialtyName {  get; set; } = string.Empty;
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Reason {  get; set; } 
        public string? Note {  get; set; }
        public decimal TotalAmount { get; set; }
        public List<AppointmentServiceDto> Services { get; set; } = new();
    }
}
