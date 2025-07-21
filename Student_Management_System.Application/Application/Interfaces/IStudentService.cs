using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Interfaces
{
    public interface IStudentService
    {
        public Task<List<StudentDTO>> GetAllStudents();
        public Task<StudentDTO> GetStudent(string id);
        public Task AddStudent(StudentDTO student);
        public Task UpdateStudent(string studentid, StudentDTO student);
        public Task DeleteStudent(string studentid);
        public Task AssignCourse(string studentid, string courseid);
        public Task<List<CourseGradeDTO>> CheckGrades(string studentid);
        public Task<List<Student>> GetAllStudentsForExcel();

    }
}
