using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Hospital_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
           var result = await patientService.GetAllPatients();
           return Ok(result);
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
        {
            await patientService.AddPatient(patientDTO);
            return Ok(new GeneralResponse
            {
                StatusCode = Response.StatusCode,
                Message = "Patient Created !"
            });
        }

        [HttpGet("SearchPatientsByName/{query}")]
        public async Task<IActionResult> SearchPatientsByName(string query)
        {
           var result = await patientService.SearchPatientsByName(query);

            if (result == null) 
            {
                return Ok("no patients with that name");
            }
            return Ok(result);
        }


        [HttpGet("GetPatientByID/{id}")]
        public async Task<IActionResult> GetPatientByID(string id)
        {
            var patient = await patientService.GetPatientByID(id);
            return Ok(patient);
        }


        [HttpPut("EditPatient")]
        public async Task<IActionResult> EditPatient(Patient patient)
        {
            await patientService.EditPatient(patient);
            return Ok(new GeneralResponse
            {
                StatusCode = Response.StatusCode,
                Message = "Updated successfully !"
            });
        }
    }
}
