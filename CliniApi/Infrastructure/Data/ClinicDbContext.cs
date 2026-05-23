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
    }
}
