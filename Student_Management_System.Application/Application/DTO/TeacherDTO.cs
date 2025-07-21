namespace Student_Management_System.Application.DTO
{
    public class TeacherDTO
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public string Designation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? OfficeLocation { get; set; } = string.Empty;
    }
}
