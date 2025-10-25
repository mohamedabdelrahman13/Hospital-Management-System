using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Hubs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Hospital_system.Controllers
{
    [Authorize(Roles = "Staff")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService patientService;
        private readonly IHubContext<DashboardHub> dHub;
        private readonly IDashboardService dashboardService;

        public PatientController(IPatientService patientService,
            IHubContext<DashboardHub> dHub
            ,IDashboardService dashboardService)
        {
            this.patientService = patientService;
            this.dHub = dHub;
            this.dashboardService = dashboardService;
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
           var result = await patientService.GetAllPatients();
           return Ok(result);
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient(CreatePatientDTO patientDTO)
        {
            await patientService.AddPatient(patientDTO);
            await dHub.Clients.All.SendAsync("patientsNumber", await dashboardService.GetTotalPatients());
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

            if (result.Count() == 0) 
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 404,
                    Message = "No patient with specified name"
                });
            }

            return Ok(new GeneralResponse
            {
                StatusCode = 200 ,
                Message = "data retrieved",
                Data = result               
            });
        }


        [HttpGet("GetPatientByID/{id}")]
        public async Task<IActionResult> GetPatientByID(string id)
        {
            var patient = await patientService.GetPatientByID(id);
            return Ok(patient);
        }


        [HttpPut("EditPatient")]
        public async Task<IActionResult> EditPatient(UpdatePatientDTO patient)
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
