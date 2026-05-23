namespace CliniApi.Domain.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int SpecialtyId { get; set; }
        public bool IsActive { get; set; } = true;
        public Specialty? Specialty { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
