using Hospital_system.Models;

namespace Hospital_system.DTOs
{
    public class AddDoctorDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string DepartmentID { get; set; }
        public string Address { get; set; }
        public decimal Cost { get; set; }
    }
}
