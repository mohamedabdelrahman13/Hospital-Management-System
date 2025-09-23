using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool isDeleted { get; set; } = false;
        public DateTime DeletedAt { get; set; }
        [JsonIgnore]
        public virtual Doctor? DoctorProfile { get; set; }
    }
}
