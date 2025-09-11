using Hospital_system.Helpers;
using Hospital_system.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService deptService;

        public DepartmentController(IDepartmentService deptService)
        {
            this.deptService = deptService;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var depts = await deptService.GetAllDepartments();
            return Ok(depts);
        }

        [HttpGet("SearchDepartment/{query}")]
        public async Task<IActionResult> SearchDepartment(string query)
        {
            var dept = await deptService.SearchDepartmentByName(query);
            if (dept == null) 
            {
                return Ok(new GeneralResponse
                {
                    StatusCode = Response.StatusCode,
                    Message = "there are no departments with that Name !"
                });
            }

            return Ok(dept);
        }



    }
}
