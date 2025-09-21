using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Hospital_system.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IBaseRepository<Patient> patientRepo;
        private readonly IBaseRepository<Appointment> appRepo;
        private readonly IBaseRepository<Department> deptRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardService(IBaseRepository<Patient> patientRepo 
            ,IBaseRepository<Appointment> appRepo
            ,IBaseRepository<Department> deptRepo
            ,UserManager<ApplicationUser> userManager)
        {
            this.patientRepo = patientRepo;
            this.appRepo = appRepo;
            this.deptRepo = deptRepo;
            this.userManager = userManager;
        }

        public async Task<List<AppointmentsStatsDTO>> GetAppointmentsStats(string view)
        {
            var totalNumberOfAppointments = appRepo.GetAll().Count();

            if (view == "daily")
            {
                var DailyStats = await appRepo.GetAll().GroupBy(a => a.BookedAt.Date)
                    .Select(g => new AppointmentsStatsDTO
                    {
                        Time = g.Key.ToString("yyyy-MM-dd"),
                        NumberOfAppointments = g.Count(),
                        Percentage = Math.Round((double)g.Count() / totalNumberOfAppointments * 100 , 1)
                    })
                    .ToListAsync();

                return DailyStats;
            }

            var MonthlyStats = await appRepo.GetAll().GroupBy(a => new { a.BookedAt.Year, a.BookedAt.Month })
                   .Select(g => new AppointmentsStatsDTO
                   {
                       Time = g.Key.Year + "-" + g.Key.Month.ToString("D2"),
                       NumberOfAppointments = g.Count(),
                       Percentage = (g.Count() / totalNumberOfAppointments)*100
                   })
                   .ToListAsync();

            return MonthlyStats;

        }

        public async Task<List<DepartmentsStatsDTO>> GetDeptsStats()
        {
            var depts = await appRepo.GetAll().GroupBy(a => a.doctorUser.DoctorProfile.Department.Name)
                     .Select(g => new DepartmentsStatsDTO
                     {
                         Department = g.Key,
                         NumberOfPatients = g.Count()
                     })
                     .ToListAsync();
            return depts;
        }

        public async Task<List<PatientRegisterationStatsDTO>> GetPatientsStats(string view)
        {
            var totalNumberOfPatients = await GetTotalPatients();
            if (view == "daily")
            {
                var dailyStats = await patientRepo.GetAll().GroupBy(p => p.CreatedOn)
                    .Select(g => new PatientRegisterationStatsDTO
                    {
                        Time = g.Key.ToString("yyyy-MM-dd"),
                        NumberOfPatients = g.Count(),
                        Percentage = Math.Round((double)g.Count() / totalNumberOfPatients * 100, 1)
                    })
                    .ToListAsync();

                return dailyStats;
            }


            var monthlyStats = await patientRepo.GetAll().GroupBy(p => new { p.CreatedOn.Year, p.CreatedOn.Month })
               .Select(g => new PatientRegisterationStatsDTO
               {
                   Time = g.Key.Year + "-" + g.Key.Month.ToString("D2"),
                   //Time = g.Key.ToString(),
                   NumberOfPatients = g.Count(),
                    Percentage = Math.Round((double)g.Count() / totalNumberOfPatients * 100, 1)
               })
               .ToListAsync();

            return monthlyStats;
        }

        public async Task<List<RevenueStatsDTO>> GetRevenueStats(string view)
        {
            if(view == "daily")
            {
                var DailyStats = await appRepo.GetAll().GroupBy(a => a.BookedAt.Date)
                    .Select(g => new RevenueStatsDTO
                    {
                        Time = g.Key.ToString("yyyy-MM-dd"),
                        Revenue = g.Sum(app => app.Cost)
                    })
                    .ToListAsync();

                return DailyStats;
            }

            var MonthlyStats = await appRepo.GetAll().GroupBy(a => new {a.BookedAt.Year , a.BookedAt.Month})
                   .Select(g => new RevenueStatsDTO
                   {
                       Time = g.Key.Year + "-" + g.Key.Month.ToString("D2"),
                       Revenue = g.Sum(app => app.Cost)
                   })
                   .ToListAsync();

            return MonthlyStats;
        }

        public async Task<int> GetTotalPatients()
        {
            var patientsNumber = await patientRepo.GetAll().CountAsync();
           
            return patientsNumber;
           
        }
        public async Task<int> GetTotalStaff()
        {
            var Staff = await userManager.GetUsersInRoleAsync("Staff");
            var StaffNumber = Staff.Count();
            return StaffNumber;
          
        }
        public async Task<decimal> GetAverageCost()
        {
            var AvgCost = await appRepo.GetAll().Select(a=>a.Cost).AverageAsync();

            return Math.Round(AvgCost , 2);
          
        }
        public decimal GetTotalCost()
        {
            var TotalRevenue = appRepo.GetAll().Sum(a=>a.Cost);
            return Math.Round(TotalRevenue, 2); 
        }
   


        //public async Task<List<dynamic>> GetPatientsByDay()
        //{
        //    var patients = await patientRepo.GetAll().GroupBy(p=>p.CreatedOn).ToListAsync();
        //    return patients;
        //}
    }
}
