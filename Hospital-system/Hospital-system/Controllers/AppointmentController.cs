using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService appService;

        public AppointmentController(IAppointmentService appService)
        {
            this.appService = appService;
        }

        [Authorize(Roles ="Staff")]
        [HttpPost("BookAppointment")]
        public async Task<IActionResult> BookAppointment([FromBody]AppointmentDTO appointment)
        {
            var result = await appService.BookAppointment(appointment);

            return Ok(new GeneralResponse
            {
                StatusCode = result.StatusCode,
                Message = result.Message
            });

        }
        [Authorize(Roles = "Staff")]
        [HttpPost("CheckAvailability")]
        public async Task<IActionResult> CheckAvailability([FromBody]AppointmentDTO appointment)
        {
            var result = await appService.CheckAvailability(appointment);

            return Ok(new GeneralResponse
            {
                StatusCode = result.StatusCode,
                Message = result.Message
            });

        }

        //[Authorize(Roles = "Doctor")]
        [HttpGet("GetAppointmentByUserId/{id}/{appDate}")]
        public async Task<IActionResult> GetAppointmentByUserId(string id , DateOnly appDate)
        {
            var apps = await appService.GetAppSchedulesByDateAsync(id , appDate);
            return Ok(apps);
        }
        [HttpGet("GetAllAppsDates")]
        public async Task<IActionResult> GetAllAppointmentsDates()
        {
            var appsDates = await appService.GetAllAppsDates();
            return Ok(appsDates);
        }


        [HttpPut("ModifyAppointmentStatus/{appId}/{newStatus}")]
        public async Task<IActionResult> ModifyAppointmentStatus(string appId , string newStatus)
        {
            var result = await appService.ModifyAppStatus(appId , newStatus);
            if (result != null) 
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = 200,
                    Message = result.Message
                });
            }

            return Ok(new GeneralResponse
            {
                StatusCode = 400,
                Message = "Failed to update status"
            });
        }
    }
}
