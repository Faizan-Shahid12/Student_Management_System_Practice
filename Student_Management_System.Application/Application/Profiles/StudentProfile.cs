using AutoMapper;
using Student_Management_System.Application.DTO;
using Student_Management_System.Domain.Entities;

namespace Student_Management_System.Application.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
