using Hospital_system.Data;
using Hospital_system.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService) 
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("GetPatientsStats/{view}")]
        public async Task<IActionResult> GetPatientStats(string view)
        {
             var stats = await dashboardService.GetPatientsStats(view);
            return Ok(stats);
        }


        [HttpGet("GetRevenueStats/{view}")]
        public async Task<IActionResult> GetRevenueStats(string view)
        {
             var stats = await dashboardService.GetRevenueStats(view);
            return Ok(stats);
        }

        [HttpGet("GetAppointmentsStats/{view}")]
        public async Task<IActionResult> GetAppointmentsStats(string view)
        {
             var stats = await dashboardService.GetAppointmentsStats(view);
            return Ok(stats);
        }

        [HttpGet("GetDepartmentsStats")]
        public async Task<IActionResult> GetDepartmentsStats()
        {
             var stats = await dashboardService.GetDeptsStats();
            return Ok(stats);
        }
    }
}
