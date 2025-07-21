namespace Student_Management_System.Domain.Entities
{
    public class Teacher : User
    {
        public string TeacherId { get; set; } = string.Empty;
        public DateTime HireDate { get; set; } = DateTime.Now;
        public string Designation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string? OfficeLocation { get; set; } = string.Empty;

        public Course CoursesTaught { get; set; }
    }
}
