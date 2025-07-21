using AutoMapper;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Domain.Interface;
using Student_Management_System.Infrastructure.Repositories;

namespace Student_Management_System.Application.Services
{
    public class CourseService : ICourseService
    {
        private ICourseRepository CourseRepo { get; set; }

        private IMapper mapper { get; set; }

        public CourseService(ICourseRepository c1, IMapper mapper1)
        {
            CourseRepo = c1;
            mapper = mapper1;
        }

        public async Task AddCourse(CourseDTO courseid)
        {
            var course = mapper.Map<Course>(courseid);
            await CourseRepo.AddCourse(course);
        }

        public async Task DeleteCourse(string courseid)
        {
            await CourseRepo.DeleteCourse(courseid);
        }

        public async Task<List<CourseDTO>> GetAllCourses()
        {
            var courses = await CourseRepo.GetAll();
            var coursesDTO = mapper.Map<List<CourseDTO>>(courses);

            return coursesDTO;
        }

        public async Task<CourseDTO> GetCourse(string id)
        {
            var course = await CourseRepo.GetCourse(id);
            var courseDTO = mapper.Map<CourseDTO>(course);
            return courseDTO;
        }

        public async Task UpdateCourse(string courseid, CourseDTO course)
        {
            var course1 = mapper.Map<Course>(course);
            await CourseRepo.UpdateCourse(courseid,course1);
        }
    }
}
