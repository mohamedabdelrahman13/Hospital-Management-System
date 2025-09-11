using Hospital_system.DTOs;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IPatientService
    {
        public Task AddPatient(PatientDTO patientDTO);
        public Task<List<Patient>> GetAllPatients();
        public Task<Patient?> GetPatientByID(string id);
        public Task<List<Patient>?> SearchPatientsByName(string queryText);
        public Task EditPatient(Patient patient);
    }
}
