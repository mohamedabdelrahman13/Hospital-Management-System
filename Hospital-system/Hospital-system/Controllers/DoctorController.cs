using Hospital_system.Data;
using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {

            var doctors = await docService.GetAllDoctors();
            if (doctors.Count == 0) 
            {
                return NotFound();
            }
            return Ok(doctors);
        }

        [HttpGet("GetDoctorById/{id}")]
        public async Task<IActionResult> GetDoctorById(string id)
        {

           var doc = await docService.GetDoctorById(id);
            return Ok(doc);
        }


        [HttpGet("GetDoctorsBySpeciality")]
        public async Task<IActionResult> GetDoctorsBySpeciality(string speciality)
        {
            var doctors = await docService.GetDoctorBySpeciality(speciality);
            if (doctors.Count == 0) 
            {
                return NotFound();
            }
            return Ok(doctors);
        }

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
