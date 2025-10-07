using AutoMapper;
using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Hospital_system.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly IBaseRepository<Patient> patientRepo;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public PatientService( IBaseRepository<Patient> patientRepo
            ,IMapper mapper
            ,IMemoryCache cache)
        {
            this.patientRepo = patientRepo;
            this.mapper = mapper;
            this.cache = cache;
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
            var cacheKey = "AllPatients";
            var patients = new List<Patient>();

            if (cache.TryGetValue(cacheKey, out List<Patient>? cachedPatients))
            {
                patients = cachedPatients;
            }
            else
            {
                patients = await patientRepo.GetAll().ToListAsync();
            }

            var filteredPatients = patients.Where(p => p.Name.ToLower().Contains(queryText.ToLower())).Take(5).ToList();

            if (filteredPatients.Any())
            {
                //cachw for 2 hours...
                var cacheOptions = new MemoryCacheEntryOptions()
                  .SetAbsoluteExpiration(TimeSpan.FromHours(2));

                cache.Set(cacheKey, patients, cacheOptions);

                return filteredPatients;
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

            cache.Remove("AllPatients");
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
