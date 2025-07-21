using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Interfaces
{
    public interface ITeacherService
    {
        public Task<List<TeacherDTO>> GetAllTeacher();
        public Task<TeacherDTO> GetTeacher(string id);
        public Task AddTeacher(TeacherDTO teacher);
        public Task UpdateTeacher(string teacherid, TeacherDTO teacher);
        public Task DeleteTeacher(string teacherid);
        public Task<string> AssignCourse(string teacherid, string courseid);
        public Task<CourseDTO> CheckCourse(string teacherid);
        public Task AssignGrades(string studentid, string courseid, string grade);


    }
}
