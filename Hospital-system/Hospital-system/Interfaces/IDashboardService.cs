using Hospital_system.DTOs;

namespace Hospital_system.Interfaces
{
    public interface IDashboardService
    {
        Task<List<PatientRegisterationStatsDTO>> GetPatientsStats(string view);
        Task<List<RevenueStatsDTO>> GetRevenueStats(string view);
        Task<List<AppointmentsStatsDTO>> GetAppointmentsStats(string view);
        Task<List<DepartmentsStatsDTO>> GetDeptsStats();
    }
}
