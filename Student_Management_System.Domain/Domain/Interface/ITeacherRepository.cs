using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Domain.Interface
{
    public interface ITeacherRepository
    {
        public Task AddTeacher(Teacher teacher);
        public Task UpdateTeacher(string id, Teacher teacher);
        public Task DeleteTeacher(string id);
        public Task AssignCourses(Teacher teacher);
        public Task<Teacher> GetTeacher(string id);
        public Task<List<Teacher>> GetAll();

    }
}
