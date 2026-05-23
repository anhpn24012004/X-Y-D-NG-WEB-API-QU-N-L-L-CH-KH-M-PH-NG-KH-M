namespace CliniApi.Domain.Entities
{
    public class AppointmentService
    {
        public int AppointmentId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; }
        public Appointment? Appointment { get; set; }
        public MedicalService? MedicalService { get; set; }
    }
}
