using Hospital_system.Models;

namespace Hospital_system.DTOs
{
    public class DepartmentDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ar_Name { get; set; }
        public List<Doctor> doctors { get; set; }
    }
}
