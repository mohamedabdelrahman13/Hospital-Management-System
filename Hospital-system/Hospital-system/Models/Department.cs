using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class Department
    {
        public string Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string ar_Name { get; set; }

        [JsonIgnore]
        public virtual List<Doctor> doctors { get; set; }
    }
}
