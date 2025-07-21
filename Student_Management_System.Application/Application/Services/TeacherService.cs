using AutoMapper;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;
using Student_Management_System.Domain.Entities;
using Student_Management_System.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student_Management_System.Application.Services
{
    public class TeacherService : ITeacherService
    {
        public ITeacherRepository TeachRepo { get; set; }
        public ICourseRepository CourseRepo { get; set; }

        private IMapper Mapper { get; set; }

        public TeacherService(ITeacherRepository Repo1, ICourseRepository Repo2, IMapper map)
        {
            TeachRepo = Repo1;
            CourseRepo = Repo2;
            Mapper = map;
        }

        public async Task AddTeacher(TeacherDTO teacher)
        {
            var teach = Mapper.Map<Teacher>(teacher);
            await TeachRepo.AddTeacher(teach);
        }

        public async Task<string> AssignCourse(string teacherid, string courseid)
        {
            var teach = await TeachRepo.GetTeacher(teacherid);

            if (teach.CoursesTaught == null)
            {
                var course = await CourseRepo.GetCourse(courseid);
                teach.CoursesTaught = course;

                await TeachRepo.AssignCourses(teach);
                await CourseRepo.UnAvailCourse(courseid);

                return "Succesful";
            }
            else
            {
                return "The Teacher already has a Course";
            }
        }

        public async Task<CourseDTO> CheckCourse(string teacherid)
        {
            var teacher = await TeachRepo.GetTeacher(teacherid);
            var course = Mapper.Map<CourseDTO>(teacher?.CoursesTaught);

            return course == null ? null : course;
        }

        public async Task DeleteTeacher(string teacherid)
        {
            await TeachRepo.DeleteTeacher(teacherid);
        }

        public async Task<List<TeacherDTO>> GetAllTeacher()
        {
            var teach = Mapper.Map<List<TeacherDTO>>(await TeachRepo.GetAll());
            return teach;
        }

        public async Task<TeacherDTO> GetTeacher(string id)
        {
            var teach = Mapper.Map<TeacherDTO>(await TeachRepo.GetTeacher(id));
            return teach;
        }

        public async Task UpdateTeacher(string teacherid, TeacherDTO teacher)
        {
            var teach = Mapper.Map<Teacher>(teacher);
            await TeachRepo.UpdateTeacher(teacherid, teach);
        }

        public async Task AssignGrades(string studentid, string courseid, string grades)
        {
            await CourseRepo.AssignGrades(studentid, courseid, grades);
        }
    }
}
