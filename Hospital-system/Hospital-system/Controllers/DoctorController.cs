using Hospital_system.Data;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IDoctorService docService;

        //to be modified 
        public DoctorController(ApplicationDbContext context
            ,IDoctorService docService)
        {
            this.context = context;
            this.docService = docService;
        }
        [HttpGet("GetAllDoctorsWithoutProfile")]
        public async Task<IActionResult> GetAllDoctorsWithoutProfile()
        {
            var doctors = await docService.GetAllDoctorsWithoutProfile();
            if (doctors?.Count == 0) 
            {
                return Ok(doctors);
            }
            return Ok(doctors);
        }
        [HttpGet("GetAllDoctorsWithProfile/{speciality}")]
        public async Task<IActionResult> GetAllDoctorsWithProfile(string speciality)
        {
            var doctors = await docService.GetAllDoctorsWithProfile(speciality);
            if (doctors.Count == 0) 
            {
                return Ok(doctors);
            }
            return Ok(doctors);
        }

        [HttpGet("GetDoctorById/{id}")]
        public async Task<IActionResult> GetDoctorById(string id)
        {

            var doc = await docService.GetDoctorById(id);
            return Ok(doc);
        }

        [HttpGet("GetDoctorByUserID/{id}")]
        public async Task<IActionResult> GetDoctorByUserID(string id)
        {
            var doctor = await docService.GetDoctorByUserId(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }


        [Authorize(Roles ="Admin")]
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctor(AddDoctorDTO doctorDTO)
        {
            var result = await docService.AddDoctor(doctorDTO);
            return Ok(new GeneralResponse
            {
                StatusCode = result.StatusCode,
                Message = result.Message
            });
        }
    }
}
