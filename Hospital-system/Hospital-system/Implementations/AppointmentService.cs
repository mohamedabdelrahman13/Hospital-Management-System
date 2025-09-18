using AutoMapper;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital_system.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IBaseRepository<Appointment> appRepo;
        private readonly IBaseRepository<Doctor> docRepo;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentService(IBaseRepository<Appointment> appRepo 
            ,IBaseRepository<Doctor> docRepo
            ,IMapper mapper
            ,UserManager<ApplicationUser> userManager)
        {
            this.appRepo = appRepo;
            this.docRepo = docRepo;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        public async Task<GeneralResponse?> BookAppointment(AppointmentDTO appDTO)
        {
            //to be modified
            var app = new Appointment()
            {
                DoctorUserID = appDTO.DoctorID,
                patientID = appDTO.PatientID,
                Date = appDTO.Date,
                StartTime = appDTO.StartTime,
                EndTime = appDTO.EndTime,
                BookedAt = DateTime.Now,
                Cost = appDTO.Cost,
            };

            await appRepo.AddAsync(app);
            await appRepo.SaveAsync();
            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Appointment is booked Successfully!"
            };

        }

        public async Task<GeneralResponse?> CheckAvailability(AppointmentDTO appDTO)
        {
            //first: check if the doctor exists
            var doctorDto = new UserWithDoctorDTO();
            var doctorFromDb = await userManager.FindByIdAsync(appDTO.DoctorID);
            //var doctorFromDb = await docRepo.GetByID(appDTO.DoctorID);
            if (doctorFromDb != null)
            {
                //map doctor to userDoctor to check its free consultation Hours
                doctorDto = mapper.Map<UserWithDoctorDTO>(doctorFromDb);
                var AppointmentDay = appDTO.Date.DayOfWeek.ToString();

                //find if doctor works on that day or not 
                var consultation = doctorDto.DoctorProfile?.ConsultationHours
                    .FirstOrDefault(ch => ch.DayOfWeek == AppointmentDay);

                if (consultation == null)
                    return new GeneralResponse
                    {
                        StatusCode = 404,
                        Message = "Doctor does not work on that Day"
                    };

                if (appDTO.StartTime < consultation.StartTime || appDTO.EndTime > consultation.EndTime)
                    return new GeneralResponse
                    {
                        StatusCode = 409,
                        Message = "Appointment time is outside consultation hours."
                    };

                //check if the appointment is booked or free 

                var Appointments = await appRepo.GetAll().ToListAsync();
                if (Appointments != null)
                {
                    var isbooked = Appointments.Any(a =>
                    a.DoctorUserID == doctorDto.Id &&
                    a.Date == appDTO.Date &&
                    ((appDTO.StartTime >= a.StartTime && appDTO.StartTime < a.EndTime) ||
                      (appDTO.EndTime > a.StartTime && appDTO.EndTime <= a.EndTime))
                    );

                    if (isbooked)
                    {
                        return new GeneralResponse
                        {
                            StatusCode = 409,
                            Message = "This time slot is already booked."
                        }
                   ;
                    }
                    return new GeneralResponse
                    {
                        StatusCode = 200,
                        Message = "Available"
                    };
                }
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "appointment not found!"
                };

            }
            return new GeneralResponse
            {
                StatusCode = 404,
                Message = "doctor not found"
            };


        }

        public async Task<List<AppScheduleDTO>> GetAppSchedulesAsync(string userId)
        {
            var apps = await appRepo.GetAll().Where(a => a.DoctorUserID == userId).ToListAsync();
            
            var appsDTO = mapper.Map<List<AppScheduleDTO>>(apps);

            return appsDTO;
        }
    }
}
