using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_system.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int FailedLoginAttempts { get; set; } = 0;
        [JsonIgnore]
        public virtual Doctor DoctorProfile { get; set; }
    }
}
