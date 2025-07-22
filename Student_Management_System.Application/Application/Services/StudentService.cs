using AutoMapper;
using FluentValidation;
using Student_Management_System.Application.Application.Validators;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Domain.Interface;

namespace Student_Management_System.Application.Services
{
    public class StudentService : IStudentService
    {
        public IStudentRepository StudRepo { get; set; }
        public ICourseRepository CourseRepo { get; set; }

        private IMapper mapper {  get; set; }

        private IValidator<Student> validator {  get; set; } 

        public StudentService(IStudentRepository repo1, ICourseRepository repo2, IMapper mapper1,IValidator<Student> validator1)
        {
            StudRepo = repo1;
            CourseRepo = repo2;
            mapper = mapper1;
            validator = validator1;
        }

        public async Task AddStudent(StudentDTO student)
        {
            var student1 = mapper.Map<Student>(student);

            var result = validator.Validate(student1);

            if(! result.IsValid)
            {
                Console.WriteLine(result.Errors.ToString());
                return;
            }

            await StudRepo.AddStudent(student1);
        }

        public async Task AssignCourse(string studentid, string courseid)
        {
            var student = await StudRepo.GetStudent(studentid);
            var course = await CourseRepo.GetCourse(courseid);

            if (student == null || course == null) return;

            if (course.CurrentCapacity < course.MaxCapacity)
            {
                CourseGrade course1 = new CourseGrade()
                {
                    CourseId = course.CourseId,
                    Courses = course,
                    StudentId = student.StudentId,
                    Students = student,
                    Enrolledat = DateTime.Now,
                    Grade = "I"
                };

                await StudRepo.AssignCourse(course1);
                await CourseRepo.UpdateCapacity(courseid);
            }
        }

        public async Task<List<CourseGradeDTO>> CheckGrades(string studentid)
        {
            var registered = await StudRepo.GetStudent(studentid);
            if (registered == null) return null;

            return mapper.Map<List<CourseGradeDTO>>(registered.courses);
        }

        public async Task DeleteStudent(string studentid)
        {
            await StudRepo.DeleteStudent(studentid);
        }

        public async Task<List<StudentDTO>> GetAllStudents()
        {
            var student1 = await StudRepo.GetAll();
            return mapper.Map<List<StudentDTO>>(student1);
        }

        public async Task<List<Student>> GetAllStudentsForExcel()
        {
            return await StudRepo.GetAll();
        }

        public async Task<StudentDTO> GetStudent(string id)
        {
            var student1 = await StudRepo.GetStudent(id);
            return mapper.Map<StudentDTO>(student1);
        }

        public async Task UpdateStudent(string studentid, StudentDTO student)
        {
            var student1 = mapper.Map<Student>(student);
            await StudRepo.UpdateStudent(studentid, student1);
        }
    }
}
