namespace Student_Management_System.Domain.Entities
{
    public class CourseGrade
    {
        public int Id { get; set; }

        public string CourseId { get; set; }

        public Course Courses { get; set; }

        public string StudentId { get; set; }

        public Student Students { get; set; }

        public string Grade {  get; set; }

        public DateTime Enrolledat { get; set; }
    }
}
