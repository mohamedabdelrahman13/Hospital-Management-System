using AutoMapper;
using Hospital_system.Data;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_system.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IBaseRepository<Doctor> doctorRepo;
        private readonly IMapper mapper;

        public DoctorService(IBaseRepository<Doctor> doctor
            , IMapper mapper)
        {
            this.doctorRepo = doctor;
            this.mapper = mapper;
        }

        public async Task<GeneralResponse> AddDoctor(AddDoctorDTO doctor)
        {
            var doctorDB = mapper.Map<Doctor>(doctor);
            if (doctorDB == null) 
            {
                return new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "not Found",
                };
            }

            await doctorRepo.AddAsync(doctorDB);
            await doctorRepo.SaveAsync();
            return new GeneralResponse
            {
                StatusCode = 200,
                Message = "Saved Successfully"
            };
            
        }

        public async Task<List<DoctorDTO>?> GetAllDoctors()
        {
            var DoctorsFromDB = await doctorRepo.GetAll().ToListAsync();
            if (DoctorsFromDB != null)
            {
                var doctorsDTOs = mapper.Map<List<DoctorDTO>>(DoctorsFromDB);
                return doctorsDTOs;
            }
            return null;
        }

        public async Task<DoctorDTO> GetDoctorById(string id)
        {
            var doctor = await doctorRepo.GetByID(id);
            var doctorDTO = mapper.Map<DoctorDTO>(doctor);  
            return doctorDTO;
        }

        public async Task<List<DoctorDTO>> GetDoctorBySpeciality(string speciality)
        {
            var SpecialityDoctorsFromDb = await doctorRepo.GetAll().Where(d => d.Department.Name == speciality).ToListAsync();
            if (SpecialityDoctorsFromDb != null)
            {
                var doctorsDTOs = mapper.Map<List<DoctorDTO>>(SpecialityDoctorsFromDb);
                return doctorsDTOs;
            }
            return null;
        }
    }
}
