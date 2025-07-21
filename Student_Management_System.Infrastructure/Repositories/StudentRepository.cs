using Microsoft.EntityFrameworkCore;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Domain.Interface;

namespace Student_Management_System.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public StudentDbContext dbContext { get; set; }

        public StudentRepository(StudentDbContext dbContext1)
        {
            dbContext = dbContext1;
        }

        public async Task AddStudent(Student student)
        {
            student.StudentId = await GenerateNextStudentIdAsync();
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(string id)
        {
            var Student = await dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            if (Student != null)
            {
                dbContext.Students.Remove(Student);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Student>> GetAll()
        {
            return await dbContext.Students
                .Include(x => x.courses)
                .ThenInclude(x => x.Courses)
                .ToListAsync();
        }

        public async Task<Student> GetStudent(string id)
        {
            var Student = await dbContext.Students
                .Include(x => x.courses)
                .ThenInclude(x => x.Courses)
                .FirstOrDefaultAsync(x => x.StudentId == id);

            return Student;
        }

        public async Task UpdateStudent(string id, Student student)
        {
            var Student = await dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            if (Student != null)
            {
                Student.FirstName = student.FirstName;
                Student.LastName = student.LastName;
                Student.EnrollmentDate = student.EnrollmentDate;
                Student.Semester = student.Semester;
                Student.Department = student.Department;
                Student.Phone = student.Phone;
                Student.CGPA = student.CGPA;

                dbContext.Update(Student);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AssignCourse(CourseGrade course)
        {
            await dbContext.CourseGrades.AddAsync(course);
            await dbContext.SaveChangesAsync();
        }

        private async Task<string> GenerateNextStudentIdAsync()
        {
            var lastIdStr = await dbContext.Students
                .OrderByDescending(s => Convert.ToInt32(s.StudentId))
                .Select(s => s.StudentId)
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
