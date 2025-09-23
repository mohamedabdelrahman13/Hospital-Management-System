using Hospital_system.DTOs;
using Hospital_system.Helpers;

namespace Hospital_system.Interfaces
{
    public interface IAppointmentService
    {
        public Task<List<AppScheduleDTO>> GetAppSchedulesByDateAsync(string userId , DateOnly appDate);
        public Task<List<DateOnly?>> GetAllAppsDates();
        public Task<GeneralResponse?> BookAppointment(AppointmentDTO appDTO);
        public Task<GeneralResponse?> CheckAvailability(AppointmentDTO appDTO);
        public Task<GeneralResponse?> ModifyAppStatus(string appId , string status); 
    }
}
