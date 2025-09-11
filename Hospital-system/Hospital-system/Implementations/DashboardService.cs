using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Hospital_system.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Hospital_system.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IBaseRepository<Patient> patientRepo;
        private readonly IBaseRepository<Appointment> appRepo;
        private readonly IBaseRepository<Department> deptRepo;

        public DashboardService(IBaseRepository<Patient> patientRepo 
            ,IBaseRepository<Appointment> appRepo
            ,IBaseRepository<Department> deptRepo)
        {
            this.patientRepo = patientRepo;
            this.appRepo = appRepo;
            this.deptRepo = deptRepo;
        }

        public async Task<List<AppointmentsStatsDTO>> GetAppointmentsStats(string view)
        {
            if (view == "daily")
            {
                var DailyStats = await appRepo.GetAll().GroupBy(a => a.BookedAt.Date)
                    .Select(g => new AppointmentsStatsDTO
                    {
                        Time = g.Key.ToString("yyyy-MM-dd"),
                        NumberOfAppointments = g.Count()
                    })
                    .ToListAsync();

                return DailyStats;
            }

            var MonthlyStats = await appRepo.GetAll().GroupBy(a => a.BookedAt.Date)
                   .Select(g => new AppointmentsStatsDTO
                   {
                       Time = g.Key.Year + "-" + g.Key.Month.ToString("D2"),
                       NumberOfAppointments = g.Count(),
                   })
                   .ToListAsync();

            return MonthlyStats;

        }

        public async Task<List<DepartmentsStatsDTO>> GetDeptsStats()
        {
            var depts = await appRepo.GetAll().GroupBy(a => a.Doctor.Department.Name)
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
            if (view == "daily")
            {
                var dailyStats = await patientRepo.GetAll().GroupBy(p => p.CreatedOn)
                    .Select(g => new PatientRegisterationStatsDTO
                    {
                        Time = g.Key.ToString("yyyy-MM-dd"),
                        NumberOfPatients = g.Count()
                    })
                    .ToListAsync();

                return dailyStats;
            }


            var monthlyStats = await patientRepo.GetAll().GroupBy(p => p.CreatedOn.Month)
               .Select(g => new PatientRegisterationStatsDTO
               {
                   Time = g.Key.ToString(),
                   NumberOfPatients = g.Count()
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


        //public async Task<List<dynamic>> GetPatientsByDay()
        //{
        //    var patients = await patientRepo.GetAll().GroupBy(p=>p.CreatedOn).ToListAsync();
        //    return patients;
        //}
    }
}
