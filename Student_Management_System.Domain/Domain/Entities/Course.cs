namespace Student_Management_System.Domain.Entities
{
    public class Course
    {

        public string CourseId { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string Department { get; set; } = string.Empty;
        public int MaxCapacity { get; set; } = 2;
        public int CurrentCapacity { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;

        public string? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<CourseGrade> students { get; set; } = new();
    }
}
