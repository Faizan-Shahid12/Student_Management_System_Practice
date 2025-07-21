using Microsoft.EntityFrameworkCore;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Domain.Interface;

namespace Student_Management_System.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private  StudentDbContext dbContext { get; set; }

        public CourseRepository(StudentDbContext dbContext1)
        {
            dbContext = dbContext1;
        }

        public async Task AddCourse(Course course)
        {
            try
            {
                await dbContext.AddAsync(course);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("The Id of the course is same as one", ex);
            }
        }

        public async Task DeleteCourse(string id)
        {
            var course = dbContext.Courses.FirstOrDefault(c => c.CourseId == id);
            dbContext.Courses.Remove(course);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll()
        {
            var course =  await dbContext.Courses.Include(x => x.students).ThenInclude(x => x.Students).Include(x => x.Teacher).ToListAsync();
            return course;
        }

        public async Task<Course> GetCourse(string courseId)
        {
            var course = await dbContext.Courses.Where(x => x.CourseId == courseId).FirstOrDefaultAsync();
            return course;
        }

        public async Task UpdateCapacity(string courseid)
        {
            var course = await GetCourse(courseid);
            if (course != null)
            {
                course.CurrentCapacity++;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCourse(string courseid, Course course1)
        {
            var course =  await dbContext.Courses.FindAsync(courseid);
            course.Title = course1.Title;
            course.Credits = course1.Credits;
            course.Department = course1.Department;

            dbContext.Update(course);
            await dbContext.SaveChangesAsync();
        }

        public async Task UnAvailCourse(string courseid)
        {
            var course =await GetCourse(courseid);
            course.IsAvailable = false;
            dbContext.Update(course);
            await dbContext.SaveChangesAsync();
        }

        private async Task<string> GenerateNextStudentIdAsync()
        {
            var lastIdStr =  await dbContext.Courses
                .OrderByDescending(s => Convert.ToInt32(s.CourseId))  // Ensure numeric ordering
                .Select(s => s.CourseId)
                .FirstOrDefaultAsync();

            int nextId = 1;

            if (!string.IsNullOrEmpty(lastIdStr) && int.TryParse(lastIdStr, out int lastId))
            {
                nextId = lastId + 1;
            }

            return nextId.ToString();  
        }

        public async Task AssignGrades(string studentid, string courseid,string grade)
        {
            var grades = await dbContext.CourseGrades.FirstOrDefaultAsync(x => x.CourseId == courseid && x.StudentId == studentid);
            grades.Grade = grade;
            dbContext.Update(grades);
            await dbContext.SaveChangesAsync();
        }
    }
}
