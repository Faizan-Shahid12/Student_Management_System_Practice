using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Domain.Interface;

namespace Student_Management_System.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private StudentDbContext DbContext { get; set; }

        public TeacherRepository(StudentDbContext dbContext1)
        {
            DbContext = dbContext1;
        }

        public async Task AddTeacher(Teacher teacher)
        {
            teacher.TeacherId = await GenerateNextStudentIdAsync();
            await DbContext.Teachers.AddAsync(teacher);
            await DbContext.SaveChangesAsync();
        }

        public async Task AssignCourses(Teacher teacher)
        {
            DbContext.Update(teacher);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteTeacher(string id)
        {
            var teach = await DbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);
            teach.CoursesTaught = new Course();
            DbContext.Teachers.Remove(teach);
            await DbContext.SaveChangesAsync();
        }

        public async Task<List<Teacher>> GetAll()
        {
            return await DbContext.Teachers.Include(x => x.CoursesTaught).ToListAsync();
        }

        public async Task<Teacher> GetTeacher(string id)
        {
            return await DbContext.Teachers.Include(x => x.CoursesTaught)
                                           .FirstOrDefaultAsync(x => x.TeacherId == id);
        }

        public async Task UpdateTeacher(string id, Teacher teacher)
        {
            var Teach = await DbContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);

            if (Teach == null)
            {
                return; 
            }

            Teach.FirstName = teacher.FirstName;
            Teach.LastName = teacher.LastName;
            Teach.OfficeLocation = teacher.OfficeLocation;
            Teach.Designation = teacher.Designation;
            Teach.Department = teacher.Department;
            Teach.Phone = teacher.Phone;

            //DbContext.Update(Teach);
            await DbContext.SaveChangesAsync();
        }

        private async Task<string> GenerateNextStudentIdAsync()
        {
            var lastIdStr = await DbContext.Teachers
                .OrderByDescending(s => Convert.ToInt32(s.TeacherId))
                .Select(s => s.TeacherId)
                .FirstOrDefaultAsync();

            int nextId = 1;

            if (!string.IsNullOrEmpty(lastIdStr) && int.TryParse(lastIdStr, out int lastId))
            {
                nextId = lastId + 1;
            }

            return nextId.ToString();
        }
    }
}
