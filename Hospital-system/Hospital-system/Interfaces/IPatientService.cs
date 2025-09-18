using Hospital_system.DTOs;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IPatientService
    {
        public Task AddPatient(CreatePatientDTO createPatientDTO);
        public Task<List<PatientDTO>> GetAllPatients();
        public Task<PatientDTO?> GetPatientByID(string id);
        public Task<List<Patient>?> SearchPatientsByName(string queryText);
        public Task EditPatient(UpdatePatientDTO updatePatientDTO);
    }
}
