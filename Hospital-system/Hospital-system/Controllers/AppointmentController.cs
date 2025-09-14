using Hospital_system.DTOs;
using Hospital_system.Helpers;
using Hospital_system.Interfaces;
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

        [HttpGet("GetAppointmentByUserId/{id}")]
        public async Task<IActionResult> GetAppointmentByUserId(string id)
        {
            var apps = await appService.GetAppSchedulesAsync(id);
            return Ok(apps);
        }
    }
}
