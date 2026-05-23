using AutoMapper;
using CliniApi.Application.DTOs;
using CliniApi.Domain.Entities;

namespace CliniApi.Application.Mappings
{
    public class ClinicProfile : Profile
    {
        public ClinicProfile()
        {
            CreateMap<Specialty, SpecialtyDto>();

            CreateMap<Doctor, DoctorDto>()
                .ForMember(dest => dest.SpecialtyName,
                opt => opt.MapFrom(src => src.Specialty != null ? src.Specialty.Name : string.Empty));

            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>();

            CreateMap<Patient, PatientDto>();
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();

            CreateMap<MedicalService, MedicalServiceDto>();
            CreateMap<CreateMedicalServiceDto, MedicalService>();
            CreateMap<UpdateMedicalServiceDto, MedicalService>();

            CreateMap<AppointmentService, AppointmentServiceDto>()
                .ForMember(dest => dest.ServiceName,
                opt => opt.MapFrom(src => src.MedicalService != null ? src.MedicalService.Name : string.Empty));

            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.Patient != null ? src.Patient.FullName : string.Empty))
                .ForMember(dest => dest.DoctorName,
                    opt => opt.MapFrom(src => src.Doctor != null ? src.Doctor.FullName : string.Empty))
                .ForMember(dest => dest.SpecialtyName,
                    opt => opt.MapFrom(src => src.Doctor != null && src.Doctor.Specialty != null ? src.Doctor.Specialty.Name : string.Empty))
                .ForMember(dest => dest.Services,
                    opt => opt.MapFrom(src => src.AppointmentServices))
                .ForMember(dest => dest.TotalAmount,
                    opt => opt.MapFrom(src => src.AppointmentServices.Sum(s => s.Quantity * s.UnitPrice)));

            CreateMap<CancelAppointmentDto, Appointment>();
        }
    }
}
