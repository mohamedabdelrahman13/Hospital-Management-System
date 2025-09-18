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

            CreateMap<CreatePatientDTO, Patient>()
                .ForMember(dest => dest.CreatedOn, o => o.Ignore());

            CreateMap<UpdatePatientDTO, Patient>();

            CreateMap<Patient , PatientDTO>()
                .ForMember(dest => dest.invoices, o => o.MapFrom(src=>src.invoices));

            CreateMap<InvoiceModel, InvoiceDTO>()
                 .ForMember(dest => dest.Payments,
                     opt => opt.MapFrom(src => src.Payments))
                 .ForMember(dest => dest.DoctorName, o => o.MapFrom(src => src.doctorUser.UserName));
            

            CreateMap<Payment, PaymentDTO>();

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

            CreateMap<AddDoctorDTO, Doctor>()
                .ForMember(dest => dest.UserId, o => o.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ConsultationHours , o=> o.MapFrom(src => src.consultationHourDTOs));

            CreateMap<ApplicationUser, UserWithDoctorDTO>()
                 .ForMember(dest => dest.DoctorProfile,opt => opt.MapFrom(src => src.DoctorProfile));

            CreateMap<RegisterDTO, ApplicationUser>();


            CreateMap<Appointment, AppScheduleDTO>()
                .ForMember(dest => dest.PatientName, o => o.MapFrom(src => src.Patient.Name))
                .ForMember(dest => dest.DoctorName, o => o.MapFrom(src => src.doctorUser.UserName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                (src.Date.HasValue && src.EndTime.HasValue &&
                 DateTime.Now > src.Date.Value.ToDateTime(src.EndTime.Value))
                    ? "Completed"
                    : src.Status));
          
        }
    }
}
