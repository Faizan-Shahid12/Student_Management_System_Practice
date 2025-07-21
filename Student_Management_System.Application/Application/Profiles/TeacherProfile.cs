using AutoMapper;
using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();

        }
    }
}
