namespace Hospital_system.DTOs
{
    public class UserWithDoctorDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool isDeleted { get; set; }
        public DoctorDTO? DoctorProfile { get; set; }
    }
}
