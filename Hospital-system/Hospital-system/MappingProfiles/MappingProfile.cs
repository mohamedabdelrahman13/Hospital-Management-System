using AutoMapper;
using Hospital_system.DTOs;
using Hospital_system.Models;

namespace Hospital_system.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDTO>()
                .ForMember(dest => dest.doctors, o => o.MapFrom(src => src.doctors));
                
            CreateMap<PatientDTO , Patient>();

            CreateMap<ConsultationHour, ConsultationHourDTO>()
                .ForMember(dest => dest.DayOfWeek, o => o.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, o => o.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, o => o.MapFrom(src => src.EndTime));

            CreateMap<ConsultationHourDTO, ConsultationHour>()
                .ForMember(dest => dest.DayOfWeek, o => o.MapFrom(src => src.DayOfWeek.ToString()))
                .ForMember(dest => dest.StartTime, o => o.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, o => o.MapFrom(src => src.EndTime));


            // Map Doctor → DoctorDTO
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.ConsultationHours, o => o.MapFrom(src => src.ConsultationHours))
                .ForMember(dest => dest.Speciality, o => o.MapFrom(src => src.Department.Name));

            CreateMap<AddDoctorDTO, Doctor>();

            CreateMap<RegisterDTO, ApplicationUser>();
        }
    }
}
