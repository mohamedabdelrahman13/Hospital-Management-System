namespace Hospital_system.DTOs
{
    public class AppointmentDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string DoctorID { get; set; }
        public string Speciality { get; set; }
        public string PatientID { get; set; }
        public decimal Cost { get; set; }
       
    }
}
