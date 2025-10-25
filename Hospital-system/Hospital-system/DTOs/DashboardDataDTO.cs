namespace Hospital_system.DTOs
{
    public class DashboardDataDTO
    {
        public int TotalPatients { get; set; }
        public int TotalStaff { get; set; }
        public decimal AverageCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
