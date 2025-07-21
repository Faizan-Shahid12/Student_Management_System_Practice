namespace Student_Management_System.Application.DTO
{
    public class CourseDTO
    {
        public string CourseId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}
