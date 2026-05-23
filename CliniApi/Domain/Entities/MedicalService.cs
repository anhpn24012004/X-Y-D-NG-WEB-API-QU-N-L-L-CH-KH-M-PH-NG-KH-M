namespace CliniApi.Domain.Entities
{
    public class MedicalService
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }  
        public bool IsActive { get; set; } = true;
        public ICollection<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
    }
}
