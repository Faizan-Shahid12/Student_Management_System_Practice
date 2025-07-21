using System.ComponentModel.DataAnnotations;

namespace Student_Management_System.Application.DTO
{
    public class RegisterTeacherDTO
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Designation { get; set; } = string.Empty;
        [Required]
        public string Department { get; set; } = string.Empty;
        public string? OfficeLocation { get; set; } = string.Empty;


    }
}
