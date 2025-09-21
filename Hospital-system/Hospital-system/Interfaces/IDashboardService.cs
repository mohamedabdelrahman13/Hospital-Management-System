using Hospital_system.DTOs;
using Hospital_system.Models;

namespace Hospital_system.Interfaces
{
    public interface IDashboardService
    {
        Task<List<PatientRegisterationStatsDTO>> GetPatientsStats(string view);
        Task<List<RevenueStatsDTO>> GetRevenueStats(string view);
        Task<List<AppointmentsStatsDTO>> GetAppointmentsStats(string view);
        Task<List<DepartmentsStatsDTO>> GetDeptsStats();
        Task<int> GetTotalPatients();
        Task<int> GetTotalStaff();
        Task<decimal> GetAverageCost();
        public decimal GetTotalCost();
    }
}
