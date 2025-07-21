using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Domain.Interface
{
    public interface IStudentRepository
    {
        public Task AddStudent(Student student);
        public Task UpdateStudent(string id, Student student);
        public Task DeleteStudent(string id);
        public Task<Student> GetStudent(string id);
        public Task<List<Student>> GetAll();
        public Task AssignCourse(CourseGrade course);

    }
}
