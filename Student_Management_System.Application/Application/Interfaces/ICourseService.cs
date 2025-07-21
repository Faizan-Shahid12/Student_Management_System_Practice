using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Interfaces
{
    public interface ICourseService
    {
        public Task<List<CourseDTO>> GetAllCourses();
        public Task<CourseDTO> GetCourse(string id);
        public Task AddCourse(CourseDTO courseid);
        public Task UpdateCourse(string courseid, CourseDTO course);
        public Task DeleteCourse(string courseid);
    }
}
