using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Domain.Interface
{
    public interface ICourseRepository
    {
        public Task AddCourse(Course course);
        public Task UpdateCourse(string courseid, Course course);
        public Task DeleteCourse(string id);
        public Task<Course> GetCourse(string Name);
        public Task<List<Course>> GetAll();
        public Task UpdateCapacity(string courseid);
        public Task AssignGrades(string studentid, string courseid, string grade);
        public Task UnAvailCourse(string courseid);


    }
}
