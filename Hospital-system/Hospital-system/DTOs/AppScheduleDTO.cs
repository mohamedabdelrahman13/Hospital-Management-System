namespace Hospital_system.DTOs
{
    public class AppScheduleDTO
    {
        public string Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DoctorName { get; set; }
        public string Status { get; set; }
        //public string Speciality { get; set; }
        public string PatientName { get; set; }
        //public decimal Cost { get; set; }
    }
}
