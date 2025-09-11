using AutoMapper;
using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Hospital_system.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IBaseRepository<Patient> patientRepo;
        private readonly IMapper mapper;

        public PatientService( IBaseRepository<Patient> patientRepo
            ,IMapper mapper)
        {
            this.patientRepo = patientRepo;
            this.mapper = mapper;
        }


        public async Task<List<Patient>> GetAllPatients()
        {
            var patients = await patientRepo.GetAll().ToListAsync();
            return patients;
        }
        public async Task<Patient?> GetPatientByID(string id)
        {
            var patient =await patientRepo.GetByID(id);
            if (patient == null) 
            {
                return null;
            }
            return patient;
            
        }
        public async Task<List<Patient>?> SearchPatientsByName(string queryText)
        {
            var patients =await patientRepo.GetAll().Where(p => p.Name.Contains(queryText)).ToListAsync();
            if (patients!=null) 
            {
                return patients;
            }
            return null;

        }
        public async Task AddPatient(PatientDTO patientDTO)
        {
            //mapping 
            var PatientFromDB = mapper.Map<Patient>(patientDTO);
            //adding
            await patientRepo.AddAsync(PatientFromDB);
            //saving
            await patientRepo.SaveAsync();
        }

        public async Task EditPatient(Patient patient)
        {
            //updating
            patientRepo.Update(patient);
            //saving..
            await patientRepo.SaveAsync(); 
        }
    }
}
