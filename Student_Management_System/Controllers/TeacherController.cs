using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Application.DTO;
using Student_Management_System.Application.Interfaces;
using System.Threading.Tasks;

namespace Student_Management_System.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private ITeacherService TeachService { get; set; }


        public TeacherController(ITeacherService teacher)
        {
            TeachService = teacher;
        }

        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            return Ok(await TeachService.GetAllTeacher());
        }

        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet]
        public async Task<IActionResult> GetTeacherById([FromQuery] string id)
        {
            return Ok(await TeachService.GetTeacher(id));
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet]
        public async Task<IActionResult> CheckCourse(string teacherid)
        {
            return Ok(await TeachService.CheckCourse(teacherid));
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<IActionResult> AssignCourse(string teacherid, string courseid)
        {
            return Ok(await TeachService.AssignCourse(teacherid, courseid));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task AddNewTeacher([FromQuery] TeacherDTO value)
        {
            await TeachService.AddTeacher(value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task UpdateTeacher(string id, [FromQuery] TeacherDTO value)
        {
            await TeachService.UpdateTeacher(id, value);
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut]
        public async Task AssignGrades([FromQuery] string studentid, [FromQuery] string courseid, [FromQuery] string grade)
        {
            await TeachService.AssignGrades(studentid, courseid, grade);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task DeleteTeacher(string id)
        {
            await TeachService.DeleteTeacher(id);
        }
    }
}
