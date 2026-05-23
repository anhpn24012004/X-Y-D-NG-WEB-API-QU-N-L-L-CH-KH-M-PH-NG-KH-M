using CliniApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CliniApi.Infrastructure.Data;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }

    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<MedicalService> MedicalServices { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentService> AppointmentServices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.HasKey(e => e.SpecialtyId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Description)
                .HasMaxLength(500);
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Email)
                .HasMaxLength(150);

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.Property(e => e.Phone)
                .HasMaxLength(30);

            entity.Property(e => e.IsActive)
                .IsRequired();

            entity.HasOne(d => d.Specialty)
                .WithMany(e => e.Doctors)
                .HasForeignKey(e => e.SpecialtyId);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date");

            entity.Property(e => e.Gender)
                .HasMaxLength(20);

            entity.Property(e => e.Phone)
                .HasMaxLength(30);

            entity.Property(e => e.Address)
                .HasMaxLength(300);
        });

        modelBuilder.Entity<MedicalService>(entity =>
        {
            entity.HasKey(e => e.ServiceId);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(e => e.IsActive)
                .IsRequired();
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId);

            entity.Property(e => e.AppointmentTime)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(e => e.Reason)
                .HasMaxLength(500);

            entity.Property(e => e.Note)
                .HasMaxLength(500);
            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.HasOne(e => e.Patient)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.PatientId);

            entity.HasOne(e => e.Doctor)
                .WithMany(e => e.Appointments)
                .HasForeignKey(e => e.DoctorId);
        });

        modelBuilder.Entity<AppointmentService>(entity =>
        {
            entity.HasKey(e => new
            {
                e.AppointmentId,
                e.ServiceId
            });

            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.HasOne(e => e.Appointment)
                .WithMany(e => e.AppointmentServices)
                .HasForeignKey(e => e.AppointmentId);

            entity.HasOne(e => e.MedicalService)
                .WithMany(e => e.AppointmentServices)
                .HasForeignKey(e => e.ServiceId);
        });

        // Seed Specialties
        modelBuilder.Entity<Specialty>().HasData(
            new Specialty { SpecialtyId = 1, Name = "General Medicine", Description = "General health examination" },
            new Specialty { SpecialtyId = 2, Name = "Cardiology", Description = "Heart and blood vessel care" },
            new Specialty { SpecialtyId = 3, Name = "Pediatrics", Description = "Medical care for children" },
            new Specialty { SpecialtyId = 4, Name = "Radiology", Description = "Imaging and diagnostic services" }
        );

        // Seed Doctors
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { DoctorId = 1, FullName = "Dr. Nguyen Van An", Email = "an.nguyen@clinic.com", Phone = "0901000001", SpecialtyId = 1, IsActive = true },
            new Doctor { DoctorId = 2, FullName = "Dr. Tran Thi Binh", Email = "binh.tran@clinic.com", Phone = "0901000002", SpecialtyId = 2, IsActive = true },
            new Doctor { DoctorId = 3, FullName = "Dr. Le Minh Chau", Email = "chau.le@clinic.com", Phone = "0901000003", SpecialtyId = 3, IsActive = true },
            new Doctor { DoctorId = 4, FullName = "Dr. Pham Quoc Dung", Email = "dung.pham@clinic.com", Phone = "0901000004", SpecialtyId = 4, IsActive = true },
            new Doctor { DoctorId = 5, FullName = "Dr. Hoang Thu Ha", Email = "ha.hoang@clinic.com", Phone = "0901000005", SpecialtyId = 1, IsActive = true },
            new Doctor { DoctorId = 6, FullName = "Dr. Do Manh Khoa", Email = "khoa.do@clinic.com", Phone = "0901000006", SpecialtyId = 2, IsActive = false }
        );

        // Seed Patients
        modelBuilder.Entity<Patient>().HasData(
            new Patient { PatientId = 1, FullName = "Nguyen Minh Duc", DateOfBirth = new DateTime(1998, 5, 12), Gender = "Male", Phone = "0912000001", Address = "Ha Noi" },
            new Patient { PatientId = 2, FullName = "Tran Thi Lan", DateOfBirth = new DateTime(2000, 8, 20), Gender = "Female", Phone = "0912000002", Address = "Hai Phong" },
            new Patient { PatientId = 3, FullName = "Le Hoang Nam", DateOfBirth = new DateTime(1995, 3, 15), Gender = "Male", Phone = "0912000003", Address = "Da Nang" },
            new Patient { PatientId = 4, FullName = "Pham Ngoc Mai", DateOfBirth = new DateTime(2002, 11, 3), Gender = "Female", Phone = "0912000004", Address = "Ho Chi Minh City" },
            new Patient { PatientId = 5, FullName = "Hoang Gia Bao", DateOfBirth = new DateTime(2015, 6, 18), Gender = "Male", Phone = "0912000005", Address = "Can Tho" },
            new Patient { PatientId = 6, FullName = "Do Thanh Tam", DateOfBirth = new DateTime(1988, 1, 25), Gender = "Female", Phone = "0912000006", Address = "Bac Ninh" }
        );

        // Seed MedicalServices
        modelBuilder.Entity<MedicalService>().HasData(
            new MedicalService { ServiceId = 1, Name = "General Check-up", Price = 200000, IsActive = true },
            new MedicalService { ServiceId = 2, Name = "Blood Test", Price = 150000, IsActive = true },
            new MedicalService { ServiceId = 3, Name = "Ultrasound", Price = 300000, IsActive = true },
            new MedicalService { ServiceId = 4, Name = "Electrocardiogram", Price = 250000, IsActive = true },
            new MedicalService { ServiceId = 5, Name = "X-ray", Price = 350000, IsActive = true },
            new MedicalService { ServiceId = 6, Name = "Pediatric Consultation", Price = 180000, IsActive = true }
        );

        // Seed Appointments
        modelBuilder.Entity<Appointment>().HasData(
            new Appointment { AppointmentId = 1, PatientId = 1, DoctorId = 1, AppointmentTime = new DateTime(2026, 6, 1, 8, 0, 0), Status = "Scheduled", Reason = "Regular health check", Note = "First visit", CreatedAt = new DateTime(2026, 5, 22, 9, 0, 0) },
            new Appointment { AppointmentId = 2, PatientId = 2, DoctorId = 2, AppointmentTime = new DateTime(2026, 6, 1, 9, 0, 0), Status = "Scheduled", Reason = "Chest pain", Note = "Need ECG", CreatedAt = new DateTime(2026, 5, 22, 9, 10, 0) },
            new Appointment { AppointmentId = 3, PatientId = 3, DoctorId = 1, AppointmentTime = new DateTime(2026, 6, 1, 10, 0, 0), Status = "Completed", Reason = "Fever", Note = "Completed successfully", CreatedAt = new DateTime(2026, 5, 22, 9, 20, 0) },
            new Appointment { AppointmentId = 4, PatientId = 4, DoctorId = 3, AppointmentTime = new DateTime(2026, 6, 2, 8, 30, 0), Status = "Cancelled", Reason = "Child cough", Note = "Patient cancelled", CreatedAt = new DateTime(2026, 5, 22, 9, 30, 0) },
            new Appointment { AppointmentId = 5, PatientId = 5, DoctorId = 3, AppointmentTime = new DateTime(2026, 6, 2, 9, 30, 0), Status = "Scheduled", Reason = "Pediatric consultation", Note = "Bring previous record", CreatedAt = new DateTime(2026, 5, 22, 9, 40, 0) },
            new Appointment { AppointmentId = 6, PatientId = 1, DoctorId = 4, AppointmentTime = new DateTime(2026, 6, 3, 14, 0, 0), Status = "Scheduled", Reason = "Abdominal ultrasound", Note = "Fasting required", CreatedAt = new DateTime(2026, 5, 22, 9, 50, 0) },
            new Appointment { AppointmentId = 7, PatientId = 2, DoctorId = 5, AppointmentTime = new DateTime(2026, 6, 3, 15, 0, 0), Status = "Completed", Reason = "Follow-up check", Note = "Stable condition", CreatedAt = new DateTime(2026, 5, 22, 10, 0, 0) },
            new Appointment { AppointmentId = 8, PatientId = 6, DoctorId = 2, AppointmentTime = new DateTime(2026, 6, 4, 8, 0, 0), Status = "Scheduled", Reason = "Heart check", Note = "Monitor blood pressure", CreatedAt = new DateTime(2026, 5, 22, 10, 10, 0) }
        );

        // Seed AppointmentServices
        modelBuilder.Entity<AppointmentService>().HasData(
            new AppointmentService { AppointmentId = 1, ServiceId = 1, Quantity = 1, UnitPrice = 200000 },
            new AppointmentService { AppointmentId = 1, ServiceId = 2, Quantity = 1, UnitPrice = 150000 },

            new AppointmentService { AppointmentId = 2, ServiceId = 4, Quantity = 1, UnitPrice = 250000 },
            new AppointmentService { AppointmentId = 3, ServiceId = 1, Quantity = 1, UnitPrice = 200000 },
            new AppointmentService { AppointmentId = 4, ServiceId = 6, Quantity = 1, UnitPrice = 180000 },
            new AppointmentService { AppointmentId = 5, ServiceId = 6, Quantity = 1, UnitPrice = 180000 },
            new AppointmentService { AppointmentId = 6, ServiceId = 3, Quantity = 1, UnitPrice = 300000 },
            new AppointmentService { AppointmentId = 7, ServiceId = 1, Quantity = 1, UnitPrice = 200000 },
            new AppointmentService { AppointmentId = 8, ServiceId = 4, Quantity = 1, UnitPrice = 250000 }
        );
    }
}
