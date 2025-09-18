namespace Hospital_system.DTOs
{
    public class CreatePatientDTO
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string phone { get; set; }
    }
}
