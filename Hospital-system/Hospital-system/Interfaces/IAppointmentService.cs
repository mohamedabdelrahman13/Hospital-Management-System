using Hospital_system.DTOs;
using Hospital_system.Helpers;

namespace Hospital_system.Interfaces
{
    public interface IAppointmentService
    {
        public Task<List<AppScheduleDTO>> GetAppSchedulesAsync(string userId);
        public Task<GeneralResponse?> BookAppointment(AppointmentDTO appDTO);

    }
}
