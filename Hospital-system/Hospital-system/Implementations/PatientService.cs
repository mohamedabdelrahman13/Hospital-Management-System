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


        public async Task<List<PatientDTO>> GetAllPatients()
        {
            var patients = await patientRepo.GetAll().ToListAsync();
            var patientsDTO = mapper.Map<List<PatientDTO>>(patients);
            return patientsDTO;
        }

        public async Task<PatientDTO?> GetPatientByID(string id)
        {
            var patientFromDB =await patientRepo.GetByID(id);
            if (patientFromDB == null) 
            {
                return null;
            }
            var patientDTO = mapper.Map<PatientDTO>(patientFromDB);
            return patientDTO;
            
        }

        public async Task<List<Patient>?> SearchPatientsByName(string queryText)
        {
            var patients =await patientRepo.GetAll().Where(p => p.Name.Contains(queryText)).ToListAsync();
            if (patients.Any()) 
            {
                return patients;
            }
            return [];

        }

        public async Task AddPatient(CreatePatientDTO createpatientDTO)
        {
            //mapping 
            var PatientFromDB = mapper.Map<Patient>(createpatientDTO);
            //adding
            await patientRepo.AddAsync(PatientFromDB);
            //saving
            await patientRepo.SaveAsync();
        }

        public async Task EditPatient(UpdatePatientDTO updatePatientDTO)
        {
            var patientFromDB = await patientRepo.GetByID(updatePatientDTO.Id);
            //mapping
            var updatedPatientFromDB = mapper.Map(updatePatientDTO, patientFromDB);
            //updating
            patientRepo.Update(patientFromDB);
            //saving..
            await patientRepo.SaveAsync(); 
        }
    }
}
