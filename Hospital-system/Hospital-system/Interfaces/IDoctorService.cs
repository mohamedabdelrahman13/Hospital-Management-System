using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IDoctorService
    {
        public  Task<List<UserWithDoctorDTO>?> GetAllDoctorsWithoutProfile();
        public  Task<List<UserWithDoctorDTO>?> GetAllDoctorsWithProfile(string speciality);
        public Task<List<DoctorDTO>> GetDoctorBySpeciality (string speciality);
        public Task<UserWithDoctorDTO> GetDoctorByUserId (string Id);
        public Task<UserWithDoctorDTO> GetDoctorById (string id);
        public Task<GeneralResponse> AddDoctor(AddDoctorDTO doctor);
    }
}
