namespace Hospital_system.DTOs
{
    public class ConsultationHourDTO
    {
        public string Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

    }
}
