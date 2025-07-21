using AutoMapper;
using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDTO>().ReverseMap();
        }
    }
}
