using Hospital_system.Models;

namespace Hospital_system.DTOs
{
    public class DoctorDTO
    {
        public  string? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string DepartmentID { get; set; }
        public string Address { get; set; }
        public string Speciality { get; set; }
        public decimal Cost { get; set; }
        public List<ConsultationHourDTO>? ConsultationHours { get; set; }
    }
}
