using AutoMapper;
using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Profiles
{
    public class CourseGradeDTOProfile : Profile
    {
        public CourseGradeDTOProfile()
        {
            CreateMap<CourseGrade, CourseGradeDTO>().
                ForMember(x => x.CourseTitle,y => y.MapFrom(y => y.Courses.Title))
               .ForMember(x => x.CourseGrade,y => y.MapFrom(y => y.Grade));
        }
    }
}
