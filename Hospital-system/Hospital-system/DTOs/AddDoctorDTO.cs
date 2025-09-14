using Hospital_system.Models;

namespace Hospital_system.DTOs
{
    public class AddDoctorDTO
    {
        public string DepartmentID { get; set; }
        public string UserId { get; set; }
        public decimal Cost { get; set; }
        public List<ConsultationHourDTO> consultationHourDTOs { get; set; }
    }
}
