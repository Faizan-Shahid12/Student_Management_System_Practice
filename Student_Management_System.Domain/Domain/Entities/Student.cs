namespace Student_Management_System.Domain.Entities
{
    public class Student : User
    {
        public string StudentId { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        public string Department { get; set; } = string.Empty;
        public int Semester { get; set; } = 1;
        public float CGPA { get; set; } = 0.0F;

        public List<CourseGrade> courses { get; set; } = new();
    }
}
