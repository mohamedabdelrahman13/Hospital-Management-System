using Hospital_system.DTOs;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentDTO>> GetAllDepartments();
        public Task<DepartmentDTO> SearchDepartmentByName(string query);
    }
}
