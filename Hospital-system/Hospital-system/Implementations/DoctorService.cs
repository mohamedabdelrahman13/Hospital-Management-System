using AutoMapper;
using Hospital_system.Data;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital_system.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IBaseRepository<Doctor> doctorRepo;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public DoctorService(IBaseRepository<Doctor> doctor
            , IMapper mapper
            ,UserManager<ApplicationUser> userManager)
        {
            this.doctorRepo = doctor;
            this.mapper = mapper;
            this.userManager = userManager;
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

        public async Task<List<UserWithDoctorDTO>?> GetAllDoctorsWithoutProfile()
        {

            var doctorsUsersDB = await userManager.GetUsersInRoleAsync("Doctor");

            if (doctorsUsersDB == null)
                return null;

            var doctorsWithProfiles = doctorsUsersDB.Where(d => d.DoctorProfile == null).ToList();

            if(doctorsWithProfiles == null)
                return null;
             var doctorsDTOs = mapper.Map<List<UserWithDoctorDTO>>(doctorsWithProfiles);


            return doctorsDTOs;

        }
        public async Task<List<UserWithDoctorDTO>?> GetAllDoctorsWithProfile(string speciality)
        {

            var doctorsUsersDB = await userManager.GetUsersInRoleAsync("Doctor");

            if (doctorsUsersDB == null)
                return null;
            var doctorsWithProfiles = doctorsUsersDB.Where(d => d.DoctorProfile != null && d.DoctorProfile.Department.Name == speciality).ToList();

            if(doctorsWithProfiles == null)
                return null;
             var doctorsDTOs = mapper.Map<List<UserWithDoctorDTO>>(doctorsWithProfiles);


            return doctorsDTOs;
        }

        public async Task<UserWithDoctorDTO> GetDoctorById(string id)
        {
            var doctor = await doctorRepo.GetByID(id);
            var doctorDTO = mapper.Map<UserWithDoctorDTO>(doctor);  
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

        public async Task<UserWithDoctorDTO> GetDoctorByUserId(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var doctorDetails = mapper.Map<UserWithDoctorDTO>(user);
         

            return doctorDetails;

        }
    }
}
