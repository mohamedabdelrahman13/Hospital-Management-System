using Hospital_system.DTOs;
using Hospital_system.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Hospital_system.Hubs
{
    public class DashboardHub:Hub
    {
        private readonly IDashboardService dashboardService;

        public DashboardHub(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        } 

        public override async Task OnConnectedAsync()
        {
            //Broadcast data when connection is started
            var dashboardData = await dashboardService.CreateDashboardData();
            var dashboardStats = await dashboardService.CreateDashboardStats("daily");

            await Clients.All.SendAsync("updateDashboard" , dashboardData);
            await Clients.All.SendAsync("updateDashboardGraphs" , dashboardStats);

        }

        public async Task BroadcastNewGraphStats(string view)
        {
            var dashboardStats = await dashboardService.CreateDashboardStats(view);
            await Clients.All.SendAsync("updateDashboardGraphs" , dashboardStats);
        }
    }
}
