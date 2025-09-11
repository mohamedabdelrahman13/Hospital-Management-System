using System.ComponentModel.DataAnnotations;

namespace Hospital_system.Models
{
    public class Patient
    {
        public string Id { get; set; }
        [MaxLength(40)]
        [MinLength(10)]
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public int Age { get; set; }
        public DateOnly CreatedOn { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Phone { get; set; }

        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be either Male or Female")]
        public string Gender { get; set; }

    }
}
