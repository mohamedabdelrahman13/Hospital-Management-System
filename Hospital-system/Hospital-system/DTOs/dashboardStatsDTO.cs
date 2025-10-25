namespace Hospital_system.DTOs
{
    public class DashboardStatsDTO
    {
        public List<AppointmentsStatsDTO> AppointmentsStats { get; set; } = new List<AppointmentsStatsDTO>();
        public List<DepartmentsStatsDTO> DepartmentsStats { get; set; } = new List<DepartmentsStatsDTO>();
        public List<PatientRegisterationStatsDTO> patientRegisterationStats { get; set; } = new List<PatientRegisterationStatsDTO>();
        public List<RevenueStatsDTO> revenueStats { get; set; } = new List<RevenueStatsDTO>();
    }
}
