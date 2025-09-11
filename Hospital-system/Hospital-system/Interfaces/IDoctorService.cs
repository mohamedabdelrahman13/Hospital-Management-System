using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IDoctorService
    {
        public  Task<List<DoctorDTO>>? GetAllDoctors();
        public Task<List<DoctorDTO>> GetDoctorBySpeciality (string speciality);
        public Task<DoctorDTO> GetDoctorById (string id);
        public Task<GeneralResponse> AddDoctor(AddDoctorDTO doctor);
    }
}
