using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Hospital_system.DTOs
{
    public class PatientDTO
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string phone {  get; set; }
    }
}
